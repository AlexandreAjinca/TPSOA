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
            //TODO le calcul du total avec taxes
            bill.setTotal(ssTotal*1.15);
            return bill;
        }
    }
}
