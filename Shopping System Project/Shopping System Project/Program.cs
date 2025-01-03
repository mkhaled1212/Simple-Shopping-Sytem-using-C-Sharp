using System;
using System.Collections.Generic;

namespace ShoppingSystemProject
{
    internal class Program
    {
        static public List<string> CartItems = new List<string>();

        static public Dictionary<string, double> Itemprices = new Dictionary<string, double>()
        {
            {"Camera", 1500},
            {"TV", 2500},
            {"Laptop", 3000}
        };

        static Stack<string> actions = new Stack<string>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the shopping system");
                Console.WriteLine("______________________________");

                Console.WriteLine("1. Add item to cart");

                Console.WriteLine("2. View cart items");
                
                Console.WriteLine("3. Remove Item from cart");
                
                Console.WriteLine("4. Checkout");
                
                Console.WriteLine("5. Undo last action");
                
                Console.WriteLine("6. Exit");

                Console.WriteLine("Enter your choice number:");
                
                string choice = Console.ReadLine();

                int intChoice;
                if (!int.TryParse(choice, out intChoice))
                {
                    Console.WriteLine("Invalid input, please enter a number.");
                    continue;
                }

                switch (intChoice)
                {
                    case 1:
                        AddItem();
                        break;

                    case 2:
                        ViewCart();
                        break;

                    case 3:
                        RemoveItem();
                        break;

                    case 4:
                        Checkout();
                        break;

                    case 5:
                        UndoAction();
                        break;

                    case 6:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice entered, please try again.");
                        break;
                }
                Console.WriteLine(" ");

            }
        }

        private static void AddItem()
        {
            Console.WriteLine("Available Items:");
            foreach (var item in Itemprices)
            {
                Console.WriteLine($"Item: {item.Key} Price: {item.Value}");
            }
            
            Console.WriteLine("Please enter the product name:");
            string cartitem = Console.ReadLine();

            if (Itemprices.ContainsKey(cartitem))
            {
                CartItems.Add(cartitem);
               
                actions.Push($"item {cartitem} added to cart");
                
                Console.WriteLine($"Item {cartitem} added to your cart.");
            }
            else
            {
                Console.WriteLine("Item is out of stock.");
            }
        }
        private static void ViewCart()
        {
          
          Console.WriteLine("Your cart items is:");
            
            if (CartItems.Any()) 
            {
                var itemPriceCollection = GetPriceItem();
                
                foreach (var itemm in itemPriceCollection)
                {
                    Console.WriteLine($"item:{itemm.Item1}, Price {itemm.Item2}");
                }
            }
            else
            {
                Console.WriteLine("Cart is empty");
            }
           
        }
        private static IEnumerable<Tuple<string, double>> GetPriceItem()
        {
            var Cartprices=new List<Tuple<string, double>>();

            foreach (var item in CartItems)
            {
                bool finditem=Itemprices.TryGetValue(item, out double price);

                if (finditem ) 
                {
                    var itemPrice = new Tuple<string, double>(item,price);
                    
                    Cartprices.Add(itemPrice);

                }
                
            }
            return Cartprices;


        }
        private static void RemoveItem()
        {
            ViewCart();
            if (CartItems.Any()) 
            {
                Console.WriteLine("Please Select item to remove");

                string ItemToRemove=Console.ReadLine();
                
                if (CartItems.Contains(ItemToRemove))
                {
                    CartItems.Remove(ItemToRemove);
                    
                    actions.Push($"item {ItemToRemove} removed from your cart");

                    Console.WriteLine($"item {ItemToRemove} removed");

                }
                else
                {
                    Console.WriteLine($"item dosen't exist in shopping cart");

                }
            }
        }
        private static void Checkout()
        {
            if (CartItems.Any())
            {
                double totalPrice = 0;

                Console.WriteLine("Your cart items are: ");

                IEnumerable<Tuple<string,double>> ItemsInCart = GetPriceItem();
                
                foreach(var item in ItemsInCart)
                {
                    totalPrice += item.Item2;
                    
                    Console.WriteLine(item.Item1 +"..."+item.Item2);
                }
                
                Console.WriteLine($"Total price to pay: {totalPrice}");
                
                Console.WriteLine("Please procees to payment, Thank you for shopping with us");
                
                CartItems.Clear();

                actions.Push("Checkout");
            }
            else
            {
                Console.WriteLine("Your cart is empty");
            }

        }
        private static void UndoAction()
        {
            if (actions.Count > 0)
            {
                string LastAction = actions.Pop();

                Console.WriteLine($"Your last action is {LastAction}");

                var ActionsArray = LastAction.Split();

                if (LastAction.Contains("added"))
                {
                    CartItems.Remove(ActionsArray[1]);

                }
                else if (LastAction.Contains("removed"))
                {
                    CartItems.Add(ActionsArray[1]);

                }
                else
                {
                    Console.WriteLine("Checkout can't be undo");
                }
            }
        }
    }
}
