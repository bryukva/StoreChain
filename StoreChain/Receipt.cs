namespace StoreChain;

public record Receipt(long Id, DateTime Date, Customer Customer, Dictionary<IProduct, int> Items)
{
    public decimal Total() => Items.Sum(x => x.Key.Price * x.Value);
}