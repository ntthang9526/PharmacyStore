namespace Store.POS;

internal static class CanhBaoSettings
{
    internal static string FilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PharmacyStore", "nguong-ton.txt");
    internal static int NguongTon
    {
        get
        {
            try { return int.TryParse(File.ReadAllText(FilePath), out int value) && value >= 1 && value <= 100000 ? value : 10; }
            catch (IOException) { return 10; }
            catch (UnauthorizedAccessException) { return 10; }
        }
    }
    internal static void Luu(int value)
    {
        if (value < 1 || value > 100000) throw new ArgumentOutOfRangeException(nameof(value));
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
        File.WriteAllText(FilePath + ".tmp", value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        File.Move(FilePath + ".tmp", FilePath, true);
        Store.DAL.DBConnection.NotifyDataChanged();
    }
}
