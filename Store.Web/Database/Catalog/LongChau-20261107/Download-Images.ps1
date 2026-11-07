param()
$ErrorActionPreference = 'Stop'
$catalog = Get-Content (Join-Path $PSScriptRoot 'catalog.json') -Raw -Encoding UTF8 | ConvertFrom-Json
$webRoot = (Resolve-Path (Join-Path $PSScriptRoot '../../..')).Path
$imageDirectory = Join-Path $webRoot 'wwwroot/images/medicines/longchau'
New-Item -ItemType Directory -Force -Path $imageDirectory | Out-Null
$products = @($catalog.products | Where-Object { $_.imageUrl })
if (@($catalog.products | Where-Object action -eq 'insert').Count -ne 50) { throw 'Catalog must contain exactly 50 new medicines.' }
if (@($catalog.products.key | Select-Object -Unique).Count -ne $catalog.products.Count) { throw 'Duplicate catalog keys.' }
$results = $products | ForEach-Object {
    $ErrorActionPreference = 'Stop'
    $product = $_
    $target = Join-Path $imageDirectory ($product.key.ToLowerInvariant() + '.webp')
    try {
        # The encoded filter URL from the source must be decoded for this CDN.
        $uri = [Uri]::UnescapeDataString($product.imageUrl).Replace('/2560x0/', '/800x0/')
        if ([Uri]::new($uri).Host -ne 'cdn.nhathuoclongchau.com.vn') { throw 'Unexpected image host.' }
        if (-not (Test-Path -LiteralPath $target)) {
            Invoke-WebRequest -Uri $uri -OutFile ($target + '.partial') -TimeoutSec 30 -UseBasicParsing
            $bytes = [IO.File]::ReadAllBytes($target + '.partial')
            if ($bytes.Length -lt 100 -or $bytes.Length -gt 5MB -or
                [Text.Encoding]::ASCII.GetString($bytes,0,4) -ne 'RIFF' -or
                [Text.Encoding]::ASCII.GetString($bytes,8,4) -ne 'WEBP') { throw 'Not a valid WebP image.' }
            Move-Item -LiteralPath ($target + '.partial') -Destination $target
        }
        [PSCustomObject]@{ Key=$product.key; Ok=$true; Bytes=(Get-Item -LiteralPath $target).Length; Error='' }
    } catch {
        [PSCustomObject]@{ Key=$product.key; Ok=$false; Bytes=0; Error=$_.Exception.Message }
    }
}
$failed = @($results | Where-Object { -not $_.Ok })
$results | Sort-Object Key | Format-Table -AutoSize
if ($failed.Count) { throw "$($failed.Count) image downloads failed; database has not been changed." }
Write-Output "Verified $($results.Count) local product images."
