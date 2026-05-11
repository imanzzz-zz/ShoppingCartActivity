using System;

namespace ShoppingCartActivity
{
    class CartItem
    {
        private Product product;
        private int quantity;
        private double total;

        public Product Product { get { return product; } set { product = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public double Total { get { return total; } set { total = value; } }
    }
    class OrderHistory
    {
        private string receiptNumber;
        private DateTime date;
        private double finalTotal;
        private double payment;
        private double change;
        private double discount;
        private CartItem[] items;

        public string ReceiptNumber { get { return receiptNumber; } set { receiptNumber = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
        public double FinalTotal { get { return finalTotal; } set { finalTotal = value; } }
        public double Payment { get { return payment; } set { payment = value; } }
        public double Change { get { return change; } set { change = value; } }
        public double Discount { get { return discount; } set { discount = value; } }
        public CartItem[] Items { get { return items; } set { items = value; } }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the INCONVENIENCE STORE!");

            Product[] products = new Product[]
            {
                new Product { Id = 1, Name = "Coke", Category = "Drinks", Price = 5000, RemainingStock = 10 },
                new Product { Id = 2, Name = "Rebisco", Category = "Snacks", Price = 45, RemainingStock = 7 },
                new Product { Id = 3, Name = "Potato Chips", Category = "Snacks", Price = 25, RemainingStock = 8 },
                new Product { Id = 4, Name = "C2", Category = "Drinks", Price = 36, RemainingStock = 9 },
                new Product { Id = 5, Name = "Nescafe", Category = "Drinks", Price = 35, RemainingStock = 10 },
                new Product { Id = 6, Name = "Mochi", Category = "Snacks", Price = 85, RemainingStock = 5 },
                new Product { Id = 7, Name = "Chocomucho", Category = "Snacks", Price = 65, RemainingStock = 10 },
                new Product { Id = 8, Name = "Milk", Category = "Drinks", Price = 100, RemainingStock = 8 }
            };
            CartItem[] cart = new CartItem[5];

            int cartcount = 0;

            OrderHistory[] history = new OrderHistory[20];
            int historyCount = 0;
            int receiptNumber = 1;

            bool shopping = true;

            while (shopping)
            {
                Console.WriteLine("\n==== MAIN MENU ====");
                Console.WriteLine("1. Products Menu");
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
                                case 1:
                                    AddProduct(products, cart, ref cartcount);
                                    CartMenu(products, cart, ref cartcount, history, ref historyCount, ref receiptNumber);
                                    break;
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
                                    if (!found) Console.WriteLine("Product not found!");
                                    break;
                                case 3:
                                    Console.WriteLine("\n== Enter category to filter ==");
                                    Console.WriteLine("1. Snacks");
                                    Console.WriteLine("2. Drinks");
                                    Console.Write("Category: ");
                                    string searchCategory = Console.ReadLine().ToLower();
                                    string selectedCategory = "";
                                    switch (searchCategory)
                                    {
                                        case "1": selectedCategory = "Snacks"; break;
                                        case "2": selectedCategory = "Drinks"; break;
                                        default: Console.WriteLine("Invalid category!"); break;
                                    }
                                    if (selectedCategory != "")
                                    {
                                        Console.WriteLine($"\n=== {selectedCategory} ===");
                                        foreach (Product p in products)
                                        {
                                            if (p.Category == selectedCategory)
                                                p.DisplayProduct();
                                        }
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine("Returning to main menu...");
                                    break;
                            }
                            break;
                        }
                    case 2:
                        CartMenu(products, cart, ref cartcount, history, ref historyCount, ref receiptNumber);
                        break;
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
                                Console.WriteLine("\n*****************************************");
                                Console.WriteLine($"Receipt #{history[i].ReceiptNumber}");
                                Console.WriteLine($"Date: {history[i].Date}");
                                Console.WriteLine("\n================= ITEMS =================");
                                if (history[i].Items != null)
                                {
                                    for (int j = 0; j < history[i].Items.Length; j++)
                                    {
                                        Console.WriteLine($"{history[i].Items[j].Product.Name} x {history[i].Items[j].Quantity} = {history[i].Items[j].Total}");
                                    }
                                }
                                Console.WriteLine("\n----------------------------------");
                                if (history[i].Discount > 0)
                                    Console.WriteLine($"Discount: {history[i].Discount}");
                                else
                                    Console.WriteLine("Discount: Not Applicable");
                                Console.WriteLine($"Final Total: {history[i].FinalTotal}");
                                Console.WriteLine("\n----------------------------------");
                                Console.WriteLine($"Payment: {history[i].Payment}");
                                Console.WriteLine($"Change: {history[i].Change}");
                            }
                        }
                        break;
                    case 4:
                        shopping = false;
                        Console.WriteLine("Thank you for shopping with us!");
                        break;
                }
            }
        }

        static void AddProduct(Product[] products, CartItem[] cart, ref int cartcount)
        {
            bool buying = true;
            while (buying)
            {
                Console.WriteLine("\n=== PRODUCTS ===");
                foreach (Product p in products)
                    p.DisplayProduct();

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
                    if (cart[i].Product.Id == selected.Id)
                    {
                        existing = i;
                        break;
                    }
                }

                if (existing != -1)
                {
                    cart[existing].Quantity += stock;
                    cart[existing].Total = cart[existing].Product.GetItemTotal(cart[existing].Quantity);
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
                        Product = selected,
                        Quantity = stock,
                        Total = selected.GetItemTotal(stock)
                    };
                    cartcount++;
                }

                selected.DeductStock(stock);
                Console.WriteLine("Added to Cart!");

                string choice;
                while (true)
                {
                    Console.Write("Add More Items? (Y/N): ");
                    choice = Console.ReadLine().ToUpper();
                    if (choice == "Y" || choice == "N") break;
                    Console.WriteLine("Invalid Input");
                }
                if (choice == "N") buying = false;
            }
        }

        static void CartMenu(Product[] products, CartItem[] cart, ref int cartcount, OrderHistory[] history, ref int historyCount, ref int receiptNumber)
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

                if (menu == 1)
                {
                    if (cartcount == 0)
                        Console.WriteLine("Cart is Empty");
                    else
                    {
                        Console.WriteLine("\n=== YOUR CART ===");
                        for (int i = 0; i < cartcount; i++)
                            Console.WriteLine($"{i + 1}. {cart[i].Product.Name} x {cart[i].Quantity} = {cart[i].Total}");
                    }
                }
                else if (menu == 2)
                {
                    if (cartcount == 0) { Console.WriteLine("\nCart is Empty"); continue; }
                    Console.WriteLine("\n=== YOUR CART ===");
                    for (int i = 0; i < cartcount; i++)
                        Console.WriteLine($"{i + 1}. {cart[i].Product.Name} x {cart[i].Quantity} = {cart[i].Total}");

                    Console.Write("Enter item number to remove: ");
                    int removeItem;
                    if (!int.TryParse(Console.ReadLine(), out removeItem) || removeItem < 1 || removeItem > cartcount)
                    {
                        Console.WriteLine("Invalid Item");
                        continue;
                    }
                    int index = removeItem - 1;
                    cart[index].Product.RestoreStock(cart[index].Quantity);
                    for (int i = index; i < cartcount - 1; i++)
                        cart[i] = cart[i + 1];
                    cart[cartcount - 1] = null;
                    cartcount--;
                    Console.WriteLine("Item removed from cart!");
                }
                else if (menu == 3)
                {
                    if (cartcount == 0) { Console.WriteLine("Cart is Empty"); continue; }
                    Console.WriteLine("\n=== YOUR CART ===");
                    for (int i = 0; i < cartcount; i++)
                        Console.WriteLine($"{i + 1}. {cart[i].Product.Name} x {cart[i].Quantity} = {cart[i].Total}");

                    Console.Write("Enter item number to update: ");
                    int updateItem;
                    if (!int.TryParse(Console.ReadLine(), out updateItem) || updateItem < 1 || updateItem > cartcount)
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
                    Product product = cart[index].Product;
                    int oldQuantity = cart[index].Quantity;
                    int difference = newQuantity - oldQuantity;

                    if (difference > 0 && difference > product.RemainingStock)
                    {
                        Console.WriteLine("Insufficient Stock");
                        continue;
                    }
                    if (difference > 0) product.DeductStock(difference);
                    else if (difference < 0) product.RestoreStock(-difference);

                    cart[index].Quantity = newQuantity;
                    cart[index].Total = product.GetItemTotal(newQuantity);
                    Console.WriteLine("Cart Updated!");
                }
                else if (menu == 4)
                {
                    if (cartcount == 0) { Console.WriteLine("Cart is already Empty"); continue; }
                    for (int i = 0; i < cartcount; i++)
                        cart[i].Product.RestoreStock(cart[i].Quantity);
                    cartcount = 0;
                    Console.WriteLine("Cart Cleared!");
                }
                else if (menu == 5)
                {
                    if (cartcount == 0) { Console.WriteLine("Cart is Empty"); continue; }

                    double finalTotal = 0;
                    double discount = 0;

                    Console.WriteLine("\n================ RECEIPT ================");
                    Console.WriteLine("Receipt No: " + receiptNumber.ToString("0000"));
                    Console.WriteLine("Date: " + DateTime.Now);
                    Console.WriteLine("----------------------------------------");

                    for (int i = 0; i < cartcount; i++)
                    {
                        Console.WriteLine($"{i + 1}. {cart[i].Product.Name} x {cart[i].Quantity} = {cart[i].Total}");
                        finalTotal += cart[i].Total;
                    }

                    if (finalTotal >= 5000) discount = finalTotal * 0.10;
                    double discountedTotal = finalTotal - discount;

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine($"Grand Total: {finalTotal}");
                    if (discount > 0) Console.WriteLine($"Discount: {discount}");
                    else Console.WriteLine("Discount: Not Applicable");
                    Console.WriteLine($"Final Total: {discountedTotal}");

                    double payment;
                    while (true)
                    {
                        Console.Write("Enter payment: ");
                        if (!double.TryParse(Console.ReadLine(), out payment)) { Console.WriteLine("Invalid input"); continue; }
                        if (payment < discountedTotal) { Console.WriteLine("Insufficient payment."); continue; }
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
                            Product = cart[i].Product,
                            Quantity = cart[i].Quantity,
                            Total = cart[i].Total
                        };
                    }

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
                    if (!lowStockFound) Console.WriteLine("No low stock products.");

                    for (int i = 0; i < cartcount; i++) cart[i] = null;
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
