using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Mu_Online
{
    class Items
    {
        public int health { get; set; }
        public int damage { get; set; }
        public string itemName { get; set; }

        public void Setup(string _itemName)
        {
            if (_itemName.Contains("Regular"))
            {
                health = 50;
                damage = 10;
            }
            else if (_itemName.Contains("Powered"))
            {
                health = 150;
                damage = 25;
            }
            else if (_itemName.Contains("Enchanced"))
            {
                health = 300;
                damage = 50;
            }
            else if (_itemName.Contains("Godlike"))
            {
                health = 500;
                damage = 100;
            }

            itemName = _itemName;
        }
    }
}
