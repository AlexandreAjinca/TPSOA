using System;
using UserSDK;

namespace TPSOA
{
    class Program
    {
        static void Main(string[] args)
        {
            string username="";
            while (username != "Q")
            {
                Console.WriteLine("Enter your username or \"Q\" to leave: ");
                username = Console.ReadLine();
                Console.WriteLine("Vous avez rentré : " + username + " \n Recherche dans la base de données");
                User u = User.GetUser(username);
                if (u != null)
                    break;
            }
            
        }
    }
}
