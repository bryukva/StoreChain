using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StoreChain.Tests;

public class UnitTest1
{
    [Fact]
    public void AddShop()
    {
        var pharmacy1 = ShopBuilder.AddShop("firstPh", ShopType.Pharmacy);
        var sm1 = ShopBuilder.AddShop("firstSm", ShopType.Supermarket);
        var cig1 = new Product("cig1", ProductType.Cigarettes, 5m);
        var drug = new SerialNumberProduct("med", ProductType.Medicine, 17.5m);
        Assert.NotNull(drug.SerialNumber);
        sm1.AddProduct(cig1, 3);

        sm1.Products.TryGetValue(cig1, out var cigCount);
        
        Assert.Equal(3, cigCount);

        Assert.Throws<ProductCantBeSoldHereException>(() => sm1.AddProduct(drug, 13));
    }
    
    [Fact]
    public void Sell()
    {
        var pharmacy1 = ShopBuilder.AddPharmacy("firstPh");

        var drug1 = new SerialNumberProduct("med1", ProductType.Medicine, 8m);
        var drug2 = new SerialNumberProduct("med2", ProductType.Medicine, 17.5m);

        pharmacy1.AddProduct(drug1, 3);
        pharmacy1.AddProduct(drug1, 1);
        pharmacy1.AddProduct(drug2, 12);

        var customer = new Customer("John", "Jones", "+123456");
        var order1 = new Dictionary<IProduct, int>
        {
            {drug1, 1},
            {drug2, 2},
        };
        var yearAgo = new DateTime(2021, 04, 01);

        pharmacy1.Sell(order1, yearAgo, customer);
        
        var order2 = new Dictionary<IProduct, int>
        {
            {drug1, 1},
            {drug2, 1},
        };
        pharmacy1.Sell(order2, DateTime.Now, customer);
        
        var orderTooMuch = new Dictionary<IProduct, int>
        {
            {drug1, 65},
            {drug2, 99},
        };

        Assert.Throws<NotEnoughInStockException>(() => pharmacy1.Sell(orderTooMuch, DateTime.Now, customer));

        var soldThisYear = pharmacy1.Report(new DateTime(2022, 01, 01), new DateTime(2022, 12, 31));
        Assert.NotNull(soldThisYear);
        Assert.Collection(soldThisYear.OrderBy(o => o.Name), product =>
        {
            Assert.Equal("med1", product.Name);
        }, product =>
        {
            Assert.Equal("med2", product.Name);
        }  );
    }
}