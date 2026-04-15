using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;
    public void DisplayProduct()
    {
        Console.WriteLine($"{Id}. Product: {Name} | Price: {Price} | Stock: {RemainingStock}");
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

        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Apple", Price = 25, RemainingStock = 10 },
            new Product { Id = 2, Name = "Banana", Price = 55, RemainingStock = 7 },
            new Product { Id = 3, Name = "Grape", Price = 150, RemainingStock = 8 },
        };
         Cart[] cart = new Cart[5];
         int cartcount = 0;

         bool continueShopping = true;

         while (continueShopping)
         {
            Console.WriteLine("\nMenu:");
            foreach (var product in products)
            {
               product.DisplayProduct();
            }
            
             Console.Write("\nEnter product code: ");
             int productChoice;
             if (!int.TryParse(Console.ReadLine(), out productChoice))
             {
                Console.WriteLine("Invalid Input");
                continue;
            }

            if (productChoice < 1 || productChoice > products.Length)
            {
               Console.WriteLine("Invalid Input");
               continue;
            }

            Product selected = products[productChoice - 1];

            Console.Write("Enter quantity: ");
            int quantities;
            if (!int.TryParse(Console.ReadLine(), out quantities) || quantities <= 0)
            {
               Console.WriteLine("Invalid quantity.");
               continue;
            }

            if (!selected.SufficientStock(quantities))
            {
               Console.WriteLine("Not enough stock.");
               continue;
            }

         }
    }
}
