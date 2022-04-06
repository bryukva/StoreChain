namespace StoreChain;

public class ProductCantBeSoldHereException : Exception
{
    public ProductCantBeSoldHereException()
    {
    }

    public ProductCantBeSoldHereException(string message) : base(message)
    {
    }
}

public class CountMustBePositiveNumberException : Exception
{
    public CountMustBePositiveNumberException()
    {
    }
}

public class NotEnoughInStockException : Exception
{
    public NotEnoughInStockException()
    {
    }
}