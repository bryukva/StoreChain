namespace StoreChain;

public class Product : BaseProduct, IProduct
{
    public Product(string name, ProductType type, decimal price) : base(name, type, price)
    {
    }
}

public class SerialNumberProduct : BaseProduct
{
    public SerialNumberProduct(string name, ProductType type, decimal price) : base(name, type, price)
    {
        SerialNumber = Guid.NewGuid();
    }
}

public interface IProduct
{
    public ProductType Type { get; set; }
    public string Name { get; set; }
    public Guid? SerialNumber { get; set; }
    public decimal Price { get; set; }
}