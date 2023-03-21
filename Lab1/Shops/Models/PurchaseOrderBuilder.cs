using Shops.Entities;

namespace Shops.Models;

public class PurchaseOrderBuilder
{
    private readonly List<ProductStock> _productStocks = new List<ProductStock>();

    public PurchaseOrderBuilder AddProduct(Product product, int quantity)
    {
        ProductStock? stock = _productStocks.Find(stock => stock.Product == product);

        if (stock is null)
        {
            _productStocks.Add(new ProductStock(product, quantity));
        }
        else
        {
            stock.IncreaseQuantity(quantity);
        }

        return this;
    }

    public StockOrder ToOrder()
    {
        return new StockOrder(_productStocks);
    }
}
