
internal interface IOrderService
{
    Task<string?> GetOrderDetailsAsync(int id);
}