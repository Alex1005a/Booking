namespace HotelSevice.Api.Services
{
    public interface IIdempotencyService
    {
        string GetKey(string key);
        void SetKey(string key, string result);
    }
}
