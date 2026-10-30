namespace Store.POS;

internal interface ILamMoiDuLieu
{
    void LamMoiDuLieu();
    Task<bool> LamMoiDuLieuAsync()
    {
        LamMoiDuLieu();
        return Task.FromResult(true);
    }
}
