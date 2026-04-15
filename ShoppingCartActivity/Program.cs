using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;
    public void DisplayProduct()
    {
        Console.WriteLine($"{Id}. {Name} - ₱{Price} - (Stock: {RemainingStock})");
    }
    public double GetItemTotal(int quantity)
    { 
        return Price * quantity;
    }
    public bool SufficientStock(int quantity)
    {
        return RemainingStock >= quantity;
    }
}

class Cart
{
    public Product product;
    public int quantity;
    public double total;
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("== Welcome to the Inconvenience Store! ==");

        Product[] products = new Product[10];
        {
            new Product { Id = 1, Name = "Apple", Price = 25, RemainingStock = 10 };
            new Product { Id = 2, Name = "Banana", Price = 55, RemainingStock = 7 };
            new Product { Id = 3, Name = "Grape", Price = 150, RemainingStock = 8 };
        }
    }
}
