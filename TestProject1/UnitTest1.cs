using CheckoutKata;

namespace TestProject1;

public class Tests
{
    private static ProductTable.Table CreateProductTable()
    {
        var table = new ProductTable.Table()
            { { "A", 65, 3, 130 }, { "B", 30, 2, 45 }, { "C", 20, null, null}, { "D", 15, null, null} };
        
        return table;
    }
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void CanCreateAProductTable()
    {
        var productTable = CreateProductTable();
        Assert.That(productTable, Has.Count.EqualTo(4));
    }

    [Test]
    public void CanAddItemsToBasket()
    {
        var productTable = CreateProductTable();
        var basket = new Basket();

        basket.Scan(productTable["A"]);

        Assert.That(basket.BasketContents, Has.Member(productTable["A"]));
    }

    [Test]
    public void CanRemoveItemFromBasket()
    {
        var productTable = CreateProductTable();
        var basket = new Basket();
        
        basket.Scan(productTable["A"]);
        Assert.That(basket.BasketContents, Has.Count.EqualTo(1));
        
        basket.RemoveFromBasket(productTable["A"]);
        Assert.That(basket.BasketContents, Has.Count.EqualTo(0));
    }

    [Test]
    public void CanGetBasketCost()
    {
        var productTable = CreateProductTable();
        var basket = new Basket();
        
        basket.Scan(productTable["B"]);
        basket.Scan(productTable["A"]);
        
        Assert.That(basket.GetTotalPrice(), Is.EqualTo(95));
    }

    [Test]
    public void CanCalculateDiscounts()
    {
        var productTable = CreateProductTable();
        var basket = new Basket();
        
        basket.Scan(productTable["A"]);
        basket.Scan(productTable["A"]);
        basket.Scan(productTable["A"]);
        
        Assert.That(basket.GetTotalPrice(), Is.EqualTo(130));
    }
    
    [Test]
    public void CanCalculateMultipleDiscounts()
    {
        var productTable = CreateProductTable();
        var basket = new Basket();
        
        basket.Scan(productTable["A"]);
        basket.Scan(productTable["A"]);
        basket.Scan(productTable["A"]);
        basket.Scan(productTable["B"]);
        basket.Scan(productTable["B"]);
        
        Assert.That(basket.GetTotalPrice(), Is.EqualTo(175));
    }
}