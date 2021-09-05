using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Mu_Online
{
    class Monster
    {
        public string monsterName { get; set; }
        public int damage { get; set; }
        public int health { get; set; }
        public int experience { get; set; }
        public List<string> drops { get; set; }


        public void Setup(string monster)
        {
            monsterName = monster;

            if (monster == "Sun God of Ra")
            {
                damage = 200;
                health = 4000;
                experience = 100;
            }

            if (monster == "Rat" || monster == "Worm" || monster == "Spider")
            {
                switch (monster)
                {
                    case "Rat":
                        drops = new List<string>() { "Scroll of Junon", "Scroll of Luna", "Scroll of Ra", "Regular Armor", "Regular Gloves", "Health Potion", "Mana Potion",
                        "Copper", "Ore", "Snow", "Gold"};
                        damage = 20;
                        health = 500;
                        experience = 10;
                        break;
                    case "Worm":
                        drops = new List<string>() { "Worm Liquid", "Scroll of Junon", "Scroll of Luna", "Scroll of Ra", "Regular Helmet", "Regular Boots", "Health Potion", "Mana Potion",
                        "Copper", "Ore", "Snow", "Gold"};
                        damage = 40;
                        health = 750;
                        experience = 20;
                        break;
                    case "Spider":
                        drops = new List<string>() { "Scroll of Junon", "Scroll of Luna", "Scroll of Ra", "Regular Ring", "Regular Necklace", "Health Potion", "Mana Potion",
                        "Copper", "Ore", "Snow", "Gold"};
                        damage = 50;
                        health = 900;
                        experience = 35;
                        break;
                }
            }

            if (monster == "Tiger" || monster == "Yeti" || monster == "Bear")
            {
                switch (monster)
                {
                    case "Tiger":
                        drops = new List<string>() { "Broken Fragment", "Scroll of Junon", "Scroll of Luna", "Scroll of Ra", "Powered Armor",
                            "Powered Gloves", "Health Potion", "Mana Potion", "Regular Sword", "Regular Staff", "Regular Bow" };
                        damage = 85;
                        health = 1500;
                        experience = 45;
                        break;
                    case "Yeti":
                        drops = new List<string>() { "Yeti Charm", "Scroll of Junon", "Scroll of Luna", "Scroll of Ra", "Powered Helmet",
                            "Powered Boots", "Health Potion", "Mana Potion","Regular Sword", "Regular Staff", "Regular Bow" };
                        damage = 70;
                        health = 1300;
                        experience = 35;
                        break;
                    case "Bear":
                        drops = new List<string>() { "Scroll of Junon", "Scroll of Luna", "Scroll of Ra", "Powered Ring",
                            "Powered Necklace", "Health Potion", "Mana Potion", "Regular Sword", "Regular Staff", "Regular Bow" };
                        damage = 50;
                        health = 1000;
                        experience = 20;
                        break;
                }
            }

            if (monster == "Dragon" || monster == "Obelisk" || monster == "Marbas")
            {
                switch (monster)
                {
                    case "Dragon":
                        drops = new List<string>() { "Scroll of Junon", "Scroll of Luna", "Scroll of Ra", "Enchanced Armor",
                            "Enchanced Gloves", "Health Potion", "Mana Potion", "Enchanced Sword", "Enchanced Staff", "Enchanced Bow", "Enchanced Shield", "Godlike Recipe" };
                        damage = 100;
                        health = 2000;
                        experience = 50;
                        break;
                    case "Obelisk":
                        drops = new List<string>() { "Fragment of The Eye", "Scroll of Junon", "Scroll of Luna", "Scroll of Ra", "Enchanced Helmet",
                            "Enchanced Boots", "Health Potion", "Mana Potion","Enchanced Sword", "Enchanced Staff", "Enchanced Bow", "Enchanced Shield", "Godlike Recipe" };
                        damage = 150;
                        health = 2600;
                        experience = 85;
                        break;
                    case "Marbas":
                        drops = new List<string>() { "Scroll of Junon", "Scroll of Luna", "Scroll of Ra", "Enchanced Ring",
                            "Enchanced Necklace", "Health Potion", "Mana Potion", "Enchanced Sword", "Enchanced Staff", "Enchanced Bow", "Enchanced Shield", "Godlike Recipe" };
                        damage = 125;
                        health = 2300;
                        experience = 65;
                        break;
                }
            }

        }
    }
}
