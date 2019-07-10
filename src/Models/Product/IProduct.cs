namespace Exemplo.Models.Products
{
    public interface IProduct
    {
        int Id { get; }
        decimal GetUnitPrice();
        string GetDescription();
    }
}