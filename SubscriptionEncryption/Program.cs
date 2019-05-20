using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SubscriptionUtility;
using System.Xml.Serialization;

namespace SubscriptionEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            SubscriptionChecker subscriptionChecker = new SubscriptionChecker();

            // Get subscriptions.
            string subscriptions = subscriptionChecker.GetSubscriptions(5);

            // Save subscriptions to file, encrypted.
            using (FileStream fs = new FileStream("EncodedSubscriptions.txt", FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(Security.Encode(subscriptions));
            }

            // Open file containing subscriptions, decrypt and print info to console.
            try
            {
                // Open file.
                using (FileStream fs = new FileStream("EncodedSubscriptions.txt", FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                {
                    // Decrypt subscriptions.
                    string decodedSubscriptions = Security.Decode(sr.ReadToEnd());

                    // Parse XML.
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(decodedSubscriptions);

                    // Get list of subscriptions.
                    XmlNodeList xmlSubscriptions = xmlDoc.GetElementsByTagName("Subscription");

                    // Iterate over subscriptions and print info.
                    foreach(XmlLinkedNode xmlSubcription in xmlSubscriptions)
                    {
                        Console.Write("Subscription Name: {0}, ", xmlSubcription["Name"].InnerText);
                        Console.WriteLine("Is Subscribed: {0}.", xmlSubcription["IsSubscribed"].InnerText);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }

            Console.ReadKey();
        }
    }
}
