namespace StoreChain;

public interface IShop
{
    public Guid Id { get; set; }
    public ShopType Type { get; set; }
    public string Name { get; set; }
    public Dictionary<IProduct, int> Products { get; set; }

    public void AddProduct<T>(T product, int count) where T : IProduct;
}