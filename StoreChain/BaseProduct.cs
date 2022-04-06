namespace StoreChain;

public abstract class BaseProduct : IProduct
{
    protected BaseProduct(string name, ProductType type, decimal price)
    {
        this.Name = name;
        this.Type = type;
        this.Price = price;
    }

    public ProductType Type { get; set; }
    public string Name { get; set; }
    public Guid? SerialNumber { get; set; }
    public decimal Price { get; set; }
}