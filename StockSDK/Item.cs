﻿using System;

namespace StockSDK
{
    public class Item
    {
        string m_nom;
        double m_prix;

        public Item()
        {

        }
        public Item(string nom, double prix)
        {
            m_nom = nom;
            m_prix = prix;
        }

        public override string ToString()
        {
            string result = m_nom + ", prix : " + m_prix.ToString() + "$ (H.T.)";
            return result;
        }

        public string getNom() { return m_nom; }
        public double getPrix() { return m_prix; }
        public void setNom(string nom){m_nom = nom;}
        public void setPrix(double prix) { m_prix = prix; }

    }
}
