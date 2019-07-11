namespace Exemplo.Models.Products
{
    public interface IProduct
    {
        int Id { get; }
        // Melhor transformar esses métodos em propriedades, vamos pensar em outras formas para para fazer overload
        decimal GetUnitPrice();
        string GetDescription();
    }
}
