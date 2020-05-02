using Client;
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
        public override string ToString()
        {
            string result = "Nom : " + m_nom + ", Prénom : " + m_prenom  + ", Username : " + m_username + ", Email : " + m_email;
            return result;
        }
        public void setNom(string nom){ m_nom = nom; }
        public string getNom(){ return m_nom; }
        public void setPrenom(string prenom) { m_prenom = prenom; }
        public string getPrenom() { return m_prenom; }
        public void setEmail(string email) { m_email = email; }
        public string getEmail() { return m_email; }
        public void setUsername(string username) { m_username = username; }
        public string getUsername() { return m_username; }

        public static User GetUser(string username)
        {
            Console.WriteLine("User requests : ");
            var rpcClient = new RPCClient("user_queue");
            Console.WriteLine(" [x] Requesting {0}", username);

            string jsonString = rpcClient.Call(username);
            Console.WriteLine(" [.] Got '{0}'", jsonString);

            if (jsonString == "null") {
                Console.WriteLine("Cet utilisateur n'existe pas");
                rpcClient.Close();
                return null;
            }
            JObject userJson = JObject.Parse(jsonString);

            User user = new User();
            user.setPrenom(userJson["prenom"].ToString());
            user.setNom(userJson["nom"].ToString());
            user.setEmail(userJson["mail"].ToString());
            user.setUsername(userJson["username"].ToString());

            rpcClient.Close();
            Console.WriteLine("Connection réussie, bienvenue " + user.getUsername());
            return user;
        }

    }
}
