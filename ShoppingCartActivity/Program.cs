using System;

namespace ShoppingCartActivity
{
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

            Console.WriteLine("Welcome to the INCONVENIENCE STORE!");

            Product[] products = new Product[]
            {
            new Product { Id = 1, Name = "Coke", Category = "Drinks", Price = 75, RemainingStock = 10 },
            new Product { Id = 2, Name = "Rebisco", Category = "Snacks", Price = 45, RemainingStock = 7 },
            new Product { Id = 3, Name = "Potato Chips", Category = "Snacks", Price = 25, RemainingStock = 8 },
            new Product { Id = 4, Name = "C2", Category = "Drinks", Price = 36, RemainingStock = 9 },
            new Product { Id = 5, Name = "Nescafe", Category = "Drinks", Price = 35, RemainingStock = 10 },
            new Product { Id = 6, Name = "Mochi", Category = "Snacks",Price = 85, RemainingStock = 5 },
            new Product { Id = 7, Name = "Chocomucho", Category = "Snacks",Price = 65, RemainingStock =10 },
            new Product { Id = 8, Name = "Milk", Category = "Drinks",Price = 100, RemainingStock = 8 }

            };
            CartItem[] cart = new CartItem[5];

            int cartcount = 0;

            OrderHistory[] history = new OrderHistory[20];
            int historyCount = 0;
            int receiptNumber = 1;

            //MAIN MENU
            bool shopping = true;

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
                    // VIEW PRODUCTS
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
                                // SEARCH PRODUCT BY NAME
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
                                    break;
                                // BACK TO MAIN MENU
                                case 4:
                                    Console.WriteLine("Returning to main menu...");
                                    break;
                            }
                            break;
                        }

                    // MANAGE CART
                    case 2:
                        CartMenu(products, cart, ref cartcount, history, ref historyCount, ref receiptNumber);
                        break;
                    // ORDER HISTORY
                    case 3:
                        Console.WriteLine("\n=== ORDER HISTORY ===");

                        if (historyCount == 0)
                        {
                            Console.WriteLine("No transactions yet.");
                        }
                        else
                        {
                            for (int i = 0; i < historyCount; i++)
                            {
                                Console.WriteLine("\n========================================");
                                Console.WriteLine($"Receipt #{history[i].ReceiptNumber}");
                                Console.WriteLine($"Date: {history[i].Date}");
                                Console.WriteLine("\n================= ITEMS =================");

                                if (history[i].Items != null)
                                {
                                    for (int j = 0; j < history[i].Items.Length; j++)
                                    {
                                        Console.WriteLine($"{history[i].Items[j].product.Name} x {history[i].Items[j].quantity} = {history[i].Items[j].total}");
                                    }
                                }
                                Console.WriteLine("\n----------------------------------");
                                Console.WriteLine($"Discount: {history[i].Discount}");
                                Console.WriteLine($"Final Total: {history[i].FinalTotal}");
                                Console.WriteLine("\n----------------------------------");
                                Console.WriteLine($"Payment: {history[i].Payment}");
                                Console.WriteLine($"Change: {history[i].Change}");
                                
                            }
                        }
                    break;
                    // EXIT PROGRAM
                    case 4:
                        shopping = false;
                        Console.WriteLine("Thank you for shopping with us!");
                        break;
                }
            }
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
        static void CartMenu(Product[] products, CartItem[] cart, ref int cartcount, OrderHistory[] history, ref int historyCount, ref int receiptNumber)
        // CART MENU
        {
            bool cartMenu = true;

            while (cartMenu)
            {
                Console.WriteLine("\n=== CART MENU ===");
                Console.WriteLine("1. View Cart (" + cartcount + " items)");
                Console.WriteLine("2. Remove Item");
                Console.WriteLine("3. Update Quantity");
                Console.WriteLine("4. Clear Cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose: ");

                int menu;
                if (!int.TryParse(Console.ReadLine(), out menu))
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }

                // VIEW CART
                if (menu == 1)
                {
                    if (cartcount == 0)
                    {
                        Console.WriteLine("Cart is Empty");
                    }
                    else
                    {
                        Console.WriteLine("\n=== YOUR CART ===");
                        for (int i = 0; i < cartcount; i++)
                        {
                            Console.WriteLine($"{i + 1}. {cart[i].product.Name} x {cart[i].quantity} = {cart[i].total}");
                        }
                    }
                }

                // REMOVE ITEM
                else if (menu == 2)
                {
                    if (cartcount == 0)
                    {
                        Console.WriteLine("\nCart is Empty");
                        continue;
                    }

                    Console.WriteLine("\n=== YOUR CART ===");
                    for (int i = 0; i < cartcount; i++)
                    {
                        Console.WriteLine($"{i + 1}. {cart[i].product.Name} x {cart[i].quantity} = {cart[i].total}");
                    }

                    Console.Write("Enter item number to remove: ");
                    int removeItem;
                    if (!int.TryParse(Console.ReadLine(), out removeItem) || removeItem < 1 || removeItem > cartcount)
                    {
                        Console.WriteLine("Invalid Item");
                        continue;
                    }
                    int index = removeItem - 1;
                    cart[index].product.RestoreStock(cart[index].quantity);
                    for (int i = index; i < cartcount - 1; i++)
                    {
                        cart[i] = cart[i + 1];
                    }

                    cart[cartcount - 1] = null;
                    cartcount--;

                    Console.WriteLine("Item removed from cart!");
                }

                // UPDATE QUANTITY
                else if (menu == 3)
                {
                    if (cartcount == 0)
                    {
                        Console.WriteLine("Cart is Empty");
                        continue;
                    }
                    Console.WriteLine("\n=== YOUR CART ===");
                    for (int i = 0; i < cartcount; i++)
                    {
                        Console.WriteLine($"{i + 1}. {cart[i].product.Name} x {cart[i].quantity} = {cart[i].total}");
                    }

                    Console.Write("Enter item number to update: ");
                    int updateItem;

                    if (!int.TryParse(Console.ReadLine(), out updateItem) ||
                        updateItem < 1 || updateItem > cartcount)
                    {
                        Console.WriteLine("Invalid Item");
                        continue;
                    }

                    Console.Write("Enter new quantity: ");
                    int newQuantity;

                    if (!int.TryParse(Console.ReadLine(), out newQuantity) || newQuantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity.");
                        continue;
                    }

                    int index = updateItem - 1;

                    Product product = cart[index].product;
                    int oldQuantity = cart[index].quantity;

                    int difference = newQuantity - oldQuantity;

                    if (difference > 0 && difference > product.RemainingStock)
                    {
                        Console.WriteLine("Insufficient Stock");
                        continue;
                    }

                    if (difference > 0)
                    {
                        product.DeductStock(difference);
                    }
                    else if (difference < 0)
                    {
                        product.RestoreStock(-difference);
                    }

                    cart[index].quantity = newQuantity;
                    cart[index].total = product.GetItemTotal(newQuantity);

                    Console.WriteLine("Cart Updated!");
                }

                // CLEAR CART
                else if (menu == 4)
                {
                    if(cartcount == 0)
                                    {
                        Console.WriteLine("Cart is already Empty");
                        continue;
                    }
                    for(int i = 0; i < cartcount; i++)
                    {
                        cart[i].product.RestoreStock(cart[i].quantity);
                    }
                    cartcount = 0;
                    Console.WriteLine("Cart Cleared!");
                }
                
                // CHECKOUT
                else if (menu == 5)
                {
                    if (cartcount == 0)
                    {
                        Console.WriteLine("Cart is Empty");
                        continue;
                    }

                    double finalTotal = 0;
                    double discount = 0;

                    Console.WriteLine("\n================ RECEIPT ================");
                    Console.WriteLine("Receipt No: " + receiptNumber.ToString("0000"));
                    Console.WriteLine("Date: " + DateTime.Now);
                    Console.WriteLine("----------------------------------------");

                    for (int i = 0; i < cartcount; i++)
                    {
                        Console.WriteLine($"{i + 1}. {cart[i].product.Name} x {cart[i].quantity} = {cart[i].total}");
                        finalTotal += cart[i].total;
                    }

                    // DISCOUNT
                    if (finalTotal >= 500)
                    {
                        discount = finalTotal * 0.10;
                    }

                    double discountedTotal = finalTotal - discount;

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine($"Grand Total: {finalTotal}");
                    Console.WriteLine($"Discount: {discount}");
                    Console.WriteLine($"Final Total: {discountedTotal}");

                    // PAYMENT VALIDATION
                    double payment;

                    while (true)
                    {
                        Console.Write("Enter payment: ");

                        if (!double.TryParse(Console.ReadLine(), out payment))
                        {
                            Console.WriteLine("Invalid input");
                            continue;
                        }

                        if (payment < discountedTotal)
                        {
                            Console.WriteLine("Insufficient payment.");
                            continue;
                        }

                        break;
                    }

                    double change = payment - discountedTotal;

                    Console.WriteLine($"Payment: {payment}");
                    Console.WriteLine($"Change: {change}");

                    CartItem[] itemsCopy = new CartItem[cartcount];

                    for (int i = 0; i < cartcount; i++)
                    {
                        itemsCopy[i] = new CartItem
                        {
                            product = cart[i].product,
                            quantity = cart[i].quantity,
                            total = cart[i].total
                        };
                    }


                    //HISTORY
                    if (historyCount < history.Length)
                    {
                        history[historyCount] = new OrderHistory
                        {
                            ReceiptNumber = receiptNumber.ToString("0000"),
                            Date = DateTime.Now,
                            FinalTotal = discountedTotal,
                            Payment = payment,
                            Change = change,
                            Discount = discount,
                            Items = itemsCopy
                        };

                        historyCount++;
                        receiptNumber++;
                    }

                    // LOW STOCK ALERT
                    Console.WriteLine("\n=== LOW STOCK ALERT ===");
                    bool lowStockFound = false;

                    for (int i = 0; i < products.Length; i++)
                    {
                        if (products[i].RemainingStock <= 2)
                        {
                            Console.WriteLine($"{products[i].Name} has only {products[i].RemainingStock} stock(s) left.");
                            lowStockFound = true;
                        }
                    }

                    if (!lowStockFound)
                    {
                        Console.WriteLine("No low stock products.");
                    }

                    
                    // CLEAR CART
                    for (int i = 0; i < cartcount; i++)
                    {
                        cart[i] = null;
                    }
                    cartcount = 0;
                    break;
                }
           
                else if (menu == 6)
                {
                    cartMenu = false;
                }
            }
        }
    }
}
