namespace Exemplo.Models.Products
{
    public interface IProduct
    {
        int Id { get; }
        decimal UnitPrice { get; }
        int Stock { get; set; }
        string Description { get; }
    }
}
