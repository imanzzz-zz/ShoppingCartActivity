using System;

class CartItem
{
    public Product product;
    public int quantity;
    public double total;
}

class OrderHistory
{
    public string ReceiptNumber;
    public DateTime Date;
    public double FinalTotal;
    public double Payment;
    public double Change;
    public double Discount;
    public CartItem[] Items;
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
        int cartcount = 0;
        
        OrderHistory[] history = new OrderHistory[20];
        int historyCount = 0;
        int receiptNumber = 1;

        //MAIN MENU
        bool run = true;
        while (shopping)
        {
            Console.WriteLine("\n==== MAIN MENU ====");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Manage Cart");
            Console.WriteLine("3. Order History");
            Console.WriteLine("4. Exit Program");
            Console.Write("Choose: ");

            int mainChoice;
            if (!int.TryParse(Console.ReadLine(), out mainChoice))
            {
              Console.WriteLine("Invalid Input");
              continue;
            }
            switch (mainChoice)
            {
                    //VIEW PRODUCTS
                case 1:
                    {
                        Console.WriteLine("\n=== PRODUCTS MENU ===");
                        Console.WriteLine("1. Buy Products");
                        Console.WriteLine("2. Search Product by Name");
                        Console.WriteLine("3. Filter Category");
                        Console.WriteLine("4. Back to Main Menu");
                        Console.Write("Choose an Option: ");

                        int case1choice;
                        if (!int.TryParse(Console.ReadLine(), out case1choice))
                        {
                            Console.WriteLine("Invalid Input");
                            continue;
                         } 
                        switch (case1choice)
                        {
                         // BUY PRODUCTS
                            case 1:
                              AddProduct(products, cart, ref cartcount);
                              CartMenu(products, cart, ref cartcount, history, ref historyCount, ref receiptNumber);
                            break;
                         //SEARCH PRODUC BY NAME
                            case 2:
                                Console.Write("\nEnter product name to search: ");
                                string searchName = Console.ReadLine().ToLower();

                                bool found = false;

                                foreach (Product p in products)
                                {
                                   if (p.Name.ToLower().Contains(searchName))
                                     {
                                        p.DisplayProduct();
                                        found = true;
                                      }
                                 }

                                   if (!found)
                                      {
                                         Console.WriteLine("Product not found!");
                                      }
                            break;
                            // FILTER CATEGORY
                            case 3:
                                Console.WriteLine("\n== Enter category to filter ==");
                                Console.WriteLine("1. Snacks");
                                Console.WriteLine("2. Drinks");
                                Console.Write("Category: ");
                                string searchCategory = Console.ReadLine().ToLower();

                                string selectedCategory = "";

                                switch (searchCategory)
                                {
                                   case "1":
                                   selectedCategory = "Snacks";
                                   break;

                                   case "2":
                                   selectedCategory = "Drinks";
                                   break;
                                   default:
                                   Console.WriteLine("Invalid category!");
                                   break;
                                }

                               if (selectedCategory != "")
                               {
                                   Console.WriteLine($"\n=== {selectedCategory} ===");
                                   foreach (Product p in products)
                                   {
                                      if (p.Category == selectedCategory)
                                        {
                                           p.DisplayProduct();
                                        }
                                   }
                               }
                                //BACK TO MAIN MENU
                            case 4:
                                Console.WriteLine("Returning to main menu...");
                                break;
                        }
                                
                                              
            
    static void AddProduct(Product[] products, CartItem[] cart, ref int cartcount)
     // ADD PRODUCT TO CART
    {
        bool buying = true;

        while (buying)
        {
        Console.WriteLine("\n=== PRODUCTS ===");
        foreach (Product p in products)
        {
            p.DisplayProduct();
        }

        Console.Write("\nEnter product number: ");
        int productid;
        if (!int.TryParse(Console.ReadLine(), out productid))
        {
            Console.WriteLine("Invalid Input");
            continue;
        }

        if (productid < 1 || productid > products.Length)
        {
            Console.WriteLine("Invalid Product");
            continue;
        }

        Product selected = products[productid - 1];

        Console.Write("Enter quantity: ");
        int stock;
        if (!int.TryParse(Console.ReadLine(), out stock) || stock <= 0)
        {
            Console.WriteLine("Invalid Quantity");
            continue;
        }

        if (!selected.SufficientStock(stock))
        {
            Console.WriteLine("Insufficient Stock");
            continue;
        }

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
        }
        else
        {
            if (cartcount >= cart.Length)
            {
                Console.WriteLine("Cart Full");
                break;
            }

            cart[cartcount] = new CartItem
            {
                product = selected,
                quantity = stock,
                total = selected.GetItemTotal(stock)
            };
            cartcount++;
        }

        selected.DeductStock(stock);
        Console.WriteLine("Added to Cart!");

        // ASK TO ADD MORE
        string choice;
        while (true)
        {
            Console.Write("Add More Items? (Y/N): ");
            choice = Console.ReadLine().ToUpper();

            if (choice == "Y" || choice == "N")
                break;

            Console.WriteLine("Invalid Input");
        }

        if (choice == "N")
            buying = false;

    }
}
                
        //DELETE
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
