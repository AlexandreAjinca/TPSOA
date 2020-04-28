using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace UserSDK
{
    public class User
    {

        string m_nom;
        string m_prenom;
        string m_email;
        string m_username;

        public User()
        {
        }

        public User(string nom, string prenom, string email, string username)
        {
            m_nom = nom;
            m_prenom = prenom;
            m_email = email;
            m_username = username;
        }

        public void setNom(string nom){ m_nom = nom; }
        public string getNom(){ return m_nom; }
        public void setPrenom(string prenom) { m_prenom = prenom; }
        public string getPrenom() { return m_prenom; }
        public void setEmail(string email) { m_email = email; }
        public string getEmail() { return m_email; }
        public void setUsername(string username) { m_username = username; }
        public string getUsernae() { return m_username; }

        static User GetUser(string username)
        {
            User user = new User();

            StreamReader file = new StreamReader("users.json", true);
            String json = file.ReadToEnd();
            var obj = JObject.Parse(json);
            //On parcours le JSON pour trouver l'utilisateur correspondant au username
            foreach (JObject element in obj["users"])
            {
                string n = element["username"].ToString();
                if (username == n)
                {
                    user.setPrenom(element["prenom"].ToString());
                    user.setNom(element["nom"].ToString());
                    user.setEmail(element["email"].ToString());
                }
            }
            return user;
        }

    }
}
