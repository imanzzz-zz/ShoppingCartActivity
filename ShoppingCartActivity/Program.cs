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
    public void DeductStock(int quantity)
    {
        RemainingStock -=  quantity;
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
        Console.WriteLine("==== Welcome to the INCONVINIENCE STORE! ====");

        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Coke (1.5L)", Price = 75, RemainingStock = 10 },
            new Product { Id = 2, Name = "Rebisco", Price = 45, RemainingStock = 7 },
            new Product { Id = 3, Name = "Potato Chips", Price = 25, RemainingStock = 8 },
            new Product { Id = 4, Name = "C2", Price = 36, RemainingStock = 9 },
            new Product { Id = 5, Name = "Nescafe", Price = 35, RemainingStock = 10 },
            new Product { Id = 6, Name = "Mochi", Price = 85, RemainingStock = 5 },
            new Product { Id = 7, Name = "Chocomucho", Price = 65, RemainingStock =10 },
            new Product { Id = 8, Name = "Milk", Price = 100, RemainingStock = 8 }

        };
        Cart[] cart = new Cart[5];
        
        double total = 0;
        int cartcount = 0;

        bool run = true;

        while (run)
        {
            Console.WriteLine("");
            foreach (Product p in products)
            {
                p.DisplayProduct();
            }

            Console.Write("\nEnter product number: ");
            int productid;
            if (!int.TryParse(Console.ReadLine(), out productid))
            {
                Console.WriteLine("\nInvalid Input");
                continue;
            }

            if (productid < 1 || productid > products.Length)
            {
                Console.WriteLine("\nInvalid Input");
                continue;
            }

            Product selected = products[productid - 1];

            Console.Write("Enter quantity: ");
            int stock;
            if (!int.TryParse(Console.ReadLine(), out stock) || stock <= 0)
            {
                Console.WriteLine("\nInvalid quantity.");
                continue;
            }

            if (!selected.SufficientStock(stock))
            {
                Console.WriteLine("\nInsufficient Stock");
                continue;
            }

            //ADD PRODUCT
            int existing = -1;
            for (int i = 0; i < cartcount; i++)
            {
                if (cart[i].product.Id == selected.Id)
                {
                    existing = i; 
                    break;
                }

            }
            if (existing != -1)
            {
                cart[existing].quantity += stock;
                cart[existing].total = cart[existing].product.GetItemTotal(cart[existing].quantity);
                selected.DeductStock(stock);
                Console.WriteLine("\nCart Updated!");
                
            }
            else
            {
                if (cartcount >= 5)
                {
                    Console.Write("Cart FUll");
                }
                else
                {
                    cart[cartcount] = new Cart
                    {
                        product = selected,
                        quantity = stock,
                        total = selected.GetItemTotal(stock)
                    };
                    cartcount++;
                    selected.DeductStock(stock);
                    Console.WriteLine("Added to Cart");
                }
            }

            string choice = "";
            while (true)
            {
                Console.Write("\nAdd More Items? (Y/N): ");
                choice = Console.ReadLine().ToUpper();

                if (choice == "Y" || choice == "N")
                {
                    break;
                }
                Console.WriteLine("Invalid Input");
            }
            if (choice == "N")
            {
                run = false;
            }


        }
        //RECEIPT
        Console.WriteLine("\n==== RECEIPT ====");
        for (int i = 0; i < cartcount; i++)
        {
            double subtotal = cart[i].total;
            int itemNo = i + 1;
            Console.WriteLine($"{itemNo}. {cart[i].product.Name} x {cart[i].quantity} = {subtotal}");
            total += subtotal;
        }
        

        //DISCOUNT
        if (total >= 5000)
        {
            double discount = total * 0.10;
            total -= discount;
            Console.WriteLine("----------------------");
            Console.WriteLine($"Discount (10%): {discount}");
            Console.WriteLine($"Total: {total}");
        }
        else
        {
            Console.WriteLine("----------------------");
            Console.WriteLine($"Grand Total: {total}");
        }
        Console.WriteLine("\n==== Updated Stock ====");
        foreach (Product p in products)
        {
            Console.WriteLine($"{p.Name}: {p.RemainingStock}");
        }
    }
}
