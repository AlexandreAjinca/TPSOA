using Newtonsoft.Json;
using StockSDK;
using System;
using System.Collections.Generic;
using UserSDK;

namespace BillSDK
{
    public class Bill
    {
        User m_user;
        List<BillLine> m_billLines;
        double m_sousTotal;
        double m_Total;

        public Bill()
        {
            m_billLines = new List<BillLine>();
        }

        public Bill(User user, List<BillLine> billLines, double sousTotal, double total)
        {
            m_user = user;
            m_billLines = billLines;
            m_sousTotal = sousTotal;
            m_Total = total;
        }

        public override string ToString()
        {
            string result = "User : " + m_user.ToString() + "\n";
            foreach(BillLine line in m_billLines)
            {
                result += line.ToString() + "\n";
            }
            result += "Sous-total : " + m_sousTotal + "\n";
            result += "Total : " + m_Total;

            return result;
        }
        public void setUser(User user) { m_user = user; }
        public User getUser() { return m_user; }
        public void setBillLines(List<BillLine> billLines) { m_billLines = billLines; }
        public List<BillLine> getBillLines() { return m_billLines; }
        public void setSousTotal(double sousTotal ) { m_sousTotal = sousTotal; }
        public double getSousTotal() { return m_sousTotal; }
        public void setTotal(double total ) { m_Total = total; }
        public double getTotal() { return m_Total; }

        public static Bill createBill(User user,List<ItemLine> lines)
        { 
            /*string json = JsonConvert.SerializeObject(user);
            json += JsonConvert.SerializeObject(lines);*/

            /* On aurait dû utiliser des propriétés dans les classes parce que sans 
             * ça Newtoonsoft ne peut pas sérialiser les objets automatiquement*/

            Bill bill = new Bill();
            bill.setUser(user);
            double ssTotal = 0;
            foreach(ItemLine il in lines)
            {
                BillLine bl = new BillLine(il.getItem(), il.getQuantite(), il.getItem().getPrix() * il.getQuantite());
                bill.getBillLines().Add(bl);
                ssTotal += bl.getSousTotal();
            }
            bill.setSousTotal(ssTotal);
            bill.setTotal(Math.Round(ssTotal*1.149975,2));
            return bill;
        }
    }
}
