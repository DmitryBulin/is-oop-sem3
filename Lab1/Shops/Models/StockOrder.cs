namespace Shops.Models;

public class StockOrder
{
    public StockOrder(List<ProductStock> stocks)
    {
        Stocks = new List<ProductStock>(stocks);
    }

    public IReadOnlyList<ProductStock> Stocks { get; }
}
