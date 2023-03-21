using Shops.Entities;

namespace Shops.Models;

public class RestockOrderBuilder
{
    private readonly List<ProductStock> _productStocks = new List<ProductStock>();

    public RestockOrderBuilder AddProduct(Product product, int quantity, decimal price)
    {
        ProductStock? stock = _productStocks.Find(stock => stock.Product == product);

        if (stock is null)
        {
            _productStocks.Add(new ProductStock(product, quantity, price));
        }
        else
        {
            stock.IncreaseQuantity(quantity);
            stock.ChangePrice(price);
        }

        return this;
    }

    public StockOrder ToOrder()
    {
        return new StockOrder(_productStocks);
    }
}
