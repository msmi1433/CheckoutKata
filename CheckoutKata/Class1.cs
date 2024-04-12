namespace CheckoutKata;

public class ProductTable
{
    public struct Product
    {
        public string Sku;
        public int Price;
        public int Quantity;
        public int? OfferQualQuant;
        public int? OfferPrice;
    }

    public class Table : Dictionary<string, Product>
    {
        public void Add(string sku, int price, int? offerQualQuant, int? offerPrice)
        {
            var product = new Product
            {
                Sku = sku,
                Price = price,
                OfferQualQuant = offerQualQuant,
                OfferPrice = offerPrice,
            };
            this.Add(sku, product);
        }
    }
}

public class Basket
{
    public readonly List<ProductTable.Product> BasketContents = [];
    public Dictionary<string, int> Offers = new Dictionary<string, int>();
    public void Scan(ProductTable.Product sku)
    {
        this.BasketContents.Add(sku);
    }

    public void RemoveFromBasket(ProductTable.Product sku)
    {
        this.BasketContents.Remove(sku);
    }

    public int GetTotalPrice()
    {
        var totalPrice = 0;
        
        foreach (var item in BasketContents)
        {
            if (item.OfferPrice != null)
            {
                if (!Offers.TryAdd(item.Sku, 1))
                {
                    Offers[item.Sku]++;
                }
            }
        }

        foreach (var item in BasketContents)
        {
            if (Offers[item.Sku] >= item.OfferQualQuant)
            {
                totalPrice += item.OfferPrice.GetValueOrDefault();
                break;
            }

            totalPrice += item.Price;
        }
        return totalPrice;
    }
}







