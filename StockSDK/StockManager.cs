using Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;


namespace StockSDK
{
    public class StockManager
    {
        public static ItemLine ReserveItem(string itemName, int quantity)
        {

            Console.WriteLine("Product requests : ");
            var rpcClient = new RPCClient("stock_queue");
            Console.WriteLine(" [x] Requesting {0}", itemName);

            string jsonString = rpcClient.Call(itemName);
            Console.WriteLine(" [.] Got '{0}'", jsonString);

            if (jsonString == "null")
            {
                Console.WriteLine("Cet objet n'existe pas");
                rpcClient.Close();
                return null;
            }

            JObject itemJson = JObject.Parse(jsonString);
            ItemLine il = new ItemLine();
            Item item = new Item(itemName, double.Parse(itemJson["prix"].ToString(), CultureInfo.InvariantCulture));
            int lineQuantity = Int32.Parse(itemJson["quantité"].ToString());
            il.setItem(item);
            il.setQuantite(lineQuantity);

            StreamReader file = new StreamReader("C:\\Users\\aajin\\source\\repos\\TPSOA\\StockManager\\product.json", true);
            String json = file.ReadToEnd();
            var obj = JObject.Parse(json);
            int count = 0;
            //On parcours le JSON pour récupérer la ligne de l'item
            foreach (JObject element in obj["product"])
            {
                string n = element["nom"].ToString();
                if (itemName == n)
                {
                    break;
                }
                count++;
            }
            obj["product"][count]["quantité"] = (int)obj["product"][count]["quantité"] - quantity;
            file.Close();

            File.WriteAllText("C:\\Users\\aajin\\source\\repos\\TPSOA\\StockManager\\product.json", obj.ToString());
            return il;
        }

        public static void ReleaseItem(ItemLine line)
        {
            StreamReader file = new StreamReader("C:\\Users\\aajin\\source\\repos\\TPSOA\\StockManager\\product.json", true);
            String json = file.ReadToEnd();
            var obj = JObject.Parse(json);
            int count = 0;
            //On parcours le JSON pour récupérer la ligne de l'item
            foreach (JObject element in obj["product"])
            {
                string n = element["nom"].ToString();
                if (line.getItem().getNom() == n)
                {
                    break;
                }
                count++;
            }
            file.Close();
            //On diminue les stocks du montant indiqué
            obj["product"][count]["quantité"] = (int)obj["product"][count]["quantité"] + line.getQuantite();
            File.WriteAllText("C:\\Users\\aajin\\source\\repos\\TPSOA\\StockManager\\product.json", obj.ToString());
        }
    }
}
