using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionEncryption
{
    public class Subscription
    {
        public string Name { get; set; }
        public bool IsSubscribed { get; set; }

        public Subscription(string name, bool isSubscribed)
        {
            Name = name;
            IsSubscribed = isSubscribed;
        }
    }
}
