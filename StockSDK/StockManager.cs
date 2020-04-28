﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace StockSDK
{
    public class StockManager
    {
        public static ItemLine ReserveItem(string name, int quantity)
        {
            ItemLine il = new ItemLine();
            StreamReader file = new StreamReader("product.json", true);
            String json = file.ReadToEnd();
            var obj = JObject.Parse(json);
            int count = 0;
            //On parcours le JSON pour récupérer la ligne de l'item
            foreach (JObject element in obj["product"])
            {
                string n = element["nom"].ToString();
                if (name == n)
                {
                    Item item = new Item(name, Double.Parse(element["prix"].ToString()));
                    int lineQuantity = Int32.Parse(element["quantité"].ToString());
                    il.setItem(item);
                    il.setQuantite(lineQuantity);
                    break;
                }
                count++;
            }
            obj["product"][count]["quantite"] = (int)obj["product"][count]["quantite"] - quantity;

            return il;
        }

        public static void ReleaseItem(ItemLine line)
        {
            StreamReader file = new StreamReader("product.json", true);
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
            //On augmente les stocks du montant indiqué
            obj["product"][count]["quantite"] = (int)obj["product"][count]["quantite"] + line.getQuantite();

        }

    }
}
