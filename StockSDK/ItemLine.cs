﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StockSDK
{
    public class ItemLine
    {
        Item m_item;
        int m_quantite;

        public ItemLine()
        {
            
        }
        public ItemLine(Item item, int quantite)
        {
            m_item = item;
            m_quantite = quantite;
        }

        public override string ToString()
        {
            string result = m_item.ToString() + ", " + m_quantite.ToString() + " exemplaires.";
            return result;
        }
        public Item getItem() { return m_item; }
        public int getQuantite() { return m_quantite; }
        public void setItem(Item item){m_item = item;}
        public void setQuantite(int quantite){m_quantite = quantite;}

    }
}
