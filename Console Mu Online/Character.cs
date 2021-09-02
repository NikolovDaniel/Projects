using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Mu_Online
{
    class Character
    {
        public int zulies { get; set; }
        public string nickname { get; set; }
        public string skillOne { get; set; }
        public string skillTwo { get; set; }
        public string skillThree { get; set; }
        public string classJob { get; set; }
        public int maximumHealth { get; set; }
        public int maximumMana { get; set; }
        public int health { get; set; }
        public int mana { get; set; }
        public int experience { get; set; }
        public int damage { get; set; }
        public string map { get; set; }
        public int level { get; set; }
        public List<string> inventory { get; set; }
        public string armor { get; set; }
        public string gloves { get; set; }
        public string helmet { get; set; }
        public string boots { get; set; }
        public string necklace { get; set; }
        public string ring { get; set; }
        public string weapon { get; set; }
        public string shield { get; set; }


        public void Skills()
        {
            if (classJob == "Knight")
            {
                skillOne = "Double Sword Attack";
                skillTwo = "Spin Attack";
                skillThree = "Demacia";
            }
            else if (classJob == "Muse")
            {
                skillOne = "Bolt Attack";
                skillTwo = "Ice Shot";
                skillThree = "Ice Storm";
            }
            else if (classJob == "Archer")
            {
                skillOne = "Poison Shot";
                skillTwo = "Arrow Rain";
                skillThree = "Multi Shot";
            }
        }
        public void Setup(string input)
        {
            if (classJob != "Archer" && classJob != "Knight" && classJob != "Muse")
            {

                while (input != "Archer" && input != "Knight" && input != "Muse")
                {
                    Console.WriteLine("Please choose a correct job!");
                    Console.Write("Choose a job: ");
                    input = Console.ReadLine();
                }

                classJob = input;
            }

            if (classJob == "Knight")
            {
                maximumHealth = 3000;
                maximumMana = 150;
                health = 3000;
                mana = 150;                
                damage = 150;
                shield = "Basic Shield";
                weapon = "Basic Sword";
            }
            else if (classJob == "Archer")
            {
                maximumHealth = 2500;
                maximumMana = 200;
                health = 1500;
                mana = 200;
                damage = 200;
                weapon = "Basic Bow";
            }
            else if (classJob == "Muse")
            {
                maximumHealth = 2000;
                maximumMana = 300;
                health = 1000;
                mana = 250;
                damage = 150;
                weapon = "Basic Staff";
            }
            helmet = "Basic Helmet";
            armor = "Basic Armor";
            gloves = "Basic Gloves";
            boots = "Basic Boots";
            necklace = "Basic Necklace";
            ring = "Basic Ring";
            experience = 0;            
            map = "Junon";
            level = 1;
            inventory = new List<string>();
            zulies = 50;
        }

        public void Inventory(string item)
        {
            if (inventory.Count <= 500)
            {
                inventory.Add(item);            
            }
            else
            {
                Console.Write("\nNot enough space in the inventory, do you want to throw an item? - ");
                string input = Console.ReadLine();

                if (input == "Yes")
                {
                    Console.WriteLine($"Available items to throw: {string.Join(", ", inventory)}");
                    Console.Write("Choose an item to throw: ");
                    string throwItem = Console.ReadLine();
                    if (inventory.Contains(throwItem))
                    {
                        inventory.Remove(throwItem);
                        inventory.Add(item);
                    }
                    else
                    {
                        Console.WriteLine($"{throwItem} does not exists in the inventory!");
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}
