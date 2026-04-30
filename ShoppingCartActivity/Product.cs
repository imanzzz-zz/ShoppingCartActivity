using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartActivity
{
    internal class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;
        public string Category;
        public void DisplayProduct()
        {
            Console.WriteLine($"{Id}. Product: {Name} | Category: {Category} | Price: {Price} | Stock: {RemainingStock}");
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
            RemainingStock -= quantity;
        }
    }
}

