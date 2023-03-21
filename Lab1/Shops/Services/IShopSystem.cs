using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopSystem
{
    Product AddProduct(string name);
    IShop AddShop(string name, string address);
    IShop? FindShop(Guid id);
    void RenameProduct(Product product, string name);
    void ChangeProductPrice(IShop shop, Product product, decimal newPrice);
    List<IShop> FindShopsWithProducts(StockOrder products);
    IShop? FindShopWithLowestPrice(StockOrder products, out decimal price);
    void RestockShop(IShop shop, StockOrder products);
    void BuyProducts(IShop shop, StockOrder products, IBalance customer);
}
