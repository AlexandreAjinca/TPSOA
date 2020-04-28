using StockSDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillSDK
{
    class BillLine
    {
        Item m_item;
        int m_quantite;
        double m_sousTotal;
        public BillLine()
        {

        }

        public BillLine(Item item, int quantite, double sousTotal)
        {
            m_item = item;
            m_quantite = quantite;
            m_sousTotal = sousTotal;
        }

        public void setItem(Item item) { m_item = item; }
        public Item getItem() { return m_item; }
        public void setQuantite(int quantite) { m_quantite = quantite; }
        public int getQuantite() { return m_quantite; }
        public void setNom(double sousTotal) { m_sousTotal = sousTotal; }
        public double getNom() { return m_sousTotal; }
    }
}
