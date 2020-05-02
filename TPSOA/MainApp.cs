using System;
using UserSDK;
using StockSDK;
using BillSDK;
using System.Collections.Generic;

namespace TPSOA
{
    class MainApp
    {
        static void Main(string[] args)
        {
            string username="";
            Console.WriteLine("Enter your username or \"Q\" to leave: ");
            username = Console.ReadLine();
            while (username != "Q")
            {
                Console.WriteLine("Vous avez rentré : " + username + " \n Recherche dans la base de données");
                User u = User.GetUser(username);
                if (u != null)
                {
                    string choice = "";
                    int quantity = 0;
                    string itemName = "";
                    List<ItemLine> panier = new List<ItemLine>();
                    while (choice != "T")
                    {
                        Console.WriteLine("Voulez-vous [A]jouter un objet à votre panier, [V]ider votre panier ou [T]erminer les achats?");
                        choice = Console.ReadLine();
                        if (choice == "A")
                        {
                            ItemLine inStock = new ItemLine();
                            Console.WriteLine("Entrez le nom du produit : ");
                            itemName = Console.ReadLine();
                            inStock = StockManager.ReserveItem(itemName, 0);
                            if (inStock == null)
                            {
                                continue;
                            }
                            Console.WriteLine("Entrez la quantité : ");
                            quantity = Int32.Parse(Console.ReadLine());

                            inStock = StockManager.ReserveItem(itemName, quantity);
                            panier.Add(new ItemLine(inStock.getItem(),quantity));
                            Console.WriteLine("Panier actuel : ");
                            foreach (ItemLine itemLine in panier)
                            {
                                Console.WriteLine(itemLine);
                            }
                        }
                        if(choice == "V"){
                            foreach(ItemLine itemLine in panier)
                            {
                                StockManager.ReleaseItem(itemLine);
                            }
                            panier.Clear();
                            Console.WriteLine("Panier vidé");
                        }
                    }
                    if (panier.Count != 0)
                    {
                        Bill bill = Bill.createBill(u, panier);
                        Console.WriteLine(bill.ToString());
                        panier.Clear();
                    }
                }
                Console.WriteLine("Enter your username or \"Q\" to leave: ");
                username = Console.ReadLine();
            }
        }
    }
}
