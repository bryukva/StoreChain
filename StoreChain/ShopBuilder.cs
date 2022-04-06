namespace StoreChain;

public static class ShopBuilder
{
    public static Shop AddShop(string name, ShopType type)
        => new Shop(name, type);
    
    public static Shop AddPharmacy(string name)
        => new Shop(name, ShopType.Pharmacy);
    
    public class Shop : IShop
    {
        public Shop(string name, ShopType type)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Products = new Dictionary<IProduct, int>();
            this.Type = type;
            this.Receipts = new HashSet<Receipt>();
        }

        public Guid Id { get; set; }
        public ShopType Type { get; set; }
        public string Name { get; set; }
        public Dictionary<IProduct, int> Products { get; set; }

        private HashSet<Receipt> Receipts { get; set; }
        private long ReceiptNumber { get; set; }

        public void AddProduct<T>(T product, int count) where T : IProduct
        {
            if (count < 0)
            {
                throw new CountMustBePositiveNumberException();
            }

            if (this.Type != ShopType.Pharmacy && product.Type == ProductType.Medicine) 
            {
                throw new ProductCantBeSoldHereException($"Only pharmacies can sell the medicines");
            }
            
            if (this.Type != ShopType.CornerShop && product.Type == ProductType.Cigarettes)
            {
                throw new ProductCantBeSoldHereException($"Only corner shops can sell the cigarettes");
            }

            if (this.Products.TryGetValue(product, out var cnt))
            {
                Products[product] += count;
            }
            else
            {
                Products.Add(product, count);
            }
        }
        
        public Receipt Sell(Dictionary<IProduct, int> items, DateTime operationDate, Customer customer)
        {
            //TODO make aggregation to have only distinct products in items
            if (items.Any(item => !this.Products.TryGetValue(item.Key, out var cnt) || cnt < item.Value))
                throw new NotEnoughInStockException();
            
            foreach (var (key, value) in items)
            {
                Products[key]-= value;
            }
            
            var receipt = new Receipt(ReceiptNumber++, operationDate, customer, items);
            
            Receipts.Add(receipt);
            return receipt;
        }

        public IEnumerable<IProduct> Report(DateTime from, DateTime to)
            => this.Receipts.Where(x => x.Date >= from && x.Date < to).SelectMany(x => x.Items.Keys);
    }
}