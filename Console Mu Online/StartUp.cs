using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Console_Mu_Online
{
    class StartUp
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Creating character, please choose a job:");
            Console.WriteLine("Knight -> Attack Damage - 100, Health - 2000, Mana - 100.");
            Console.WriteLine("Archer -> Attack Damage - 200, Health - 1500, Mana - 150.");
            Console.WriteLine("Muse -> Attack Damage - 150, Health - 1000, Mana - 250.");
            Console.Write("\nChoose a job: ");
            string input = Console.ReadLine(); // Choosing a job

            Character character = new Character();

            character.classJob = input;
            character.Setup(); // Setting up the character
            character.Skills(); // Setting up the character Skills

            string command = string.Empty;

            while (command != "END") // Executing the commands given by the User
            {
                Console.WriteLine("\nAvailable commands: Fight/Rest/Craft/Teleport/Use Potion/Open Inventory/Char Information/END.");
                Console.Write("Please enter a command: ");
                command = Console.ReadLine();

                bool validCommands = command == "Fight" || command == "Rest" || command == "Craft"
                    || command == "Teleport" || command == "Use Potion" || command == "Open Inventory" || command == "Char Information";

                if (validCommands)
                {
                    if (command == "Fight")
                    {
                        Fight(character);

                        if (character.health <= 0)
                        {
                            character.inventory.Clear();
                        }
                    }
                    else if (command == "Rest")
                    {
                        Rest(character);
                    }
                    else if (command == "Craft")
                    {
                        Crafting(character);
                    }
                    else if (command == "Teleport")
                    {
                        Teleport(character);
                    }
                    else if (command == "Use Potion")
                    {
                        UsePotions(character);
                    }
                    else if (command == "Open Inventory")
                    {
                        Inventory(character);
                    }
                    else if (command == "Char Information")
                    {
                        CharInformation(character);
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid command!");
                }
            }

            if (command == "END")
            {
                Console.WriteLine("\nYou ended the game!");
                Console.WriteLine($"Current character level: {character.level}");
                Console.WriteLine($"Current available items: {string.Join(", ", character.inventory)}");
            }


            Console.ReadKey();

        }

        static void Crafting(Character character)
        {
            Console.WriteLine("\nWelcome to the Crafting Machine!");
            Console.WriteLine("\nIn order to craft a Godlike Item, you need - Enchanced Item, 1 Copper, 1 Snow, 1 Gold and Godlike Recipe!");
            List<string> itemsAvailable = new List<string>();

            if (character.inventory.Contains("Enchanced Armor"))
            {
                itemsAvailable.Add("Armor");
            }
            if (character.inventory.Contains("Enchanced Helmet"))
            {
                itemsAvailable.Add("Helmet");
            }
            if (character.inventory.Contains("Enchanced Boots"))
            {
                itemsAvailable.Add("Boots");
            }
            if (character.inventory.Contains("Enchanced Gloves"))
            {
                itemsAvailable.Add("Gloves");
            }
            if (character.inventory.Contains("Enchanced Sword"))
            {
                itemsAvailable.Add("Sword");
            }
            if (character.inventory.Contains("Enchanced Bow"))
            {
                itemsAvailable.Add("Bow");
            }
            if (character.inventory.Contains("Enchanced Staff"))
            {
                itemsAvailable.Add("Staff");
            }
            if (character.inventory.Contains("Enchanced Shield"))
            {
                itemsAvailable.Add("Shield");
            }
            if (itemsAvailable.Count > 0)
            {
                Console.WriteLine($"\nYou can Craft the Godlike version of these items - {string.Join(", ", itemsAvailable)}");
                Console.Write("\nDo you want to craft an Item? - ");
                string inpt = Console.ReadLine();

                if (inpt == "Yes")
                {
                    Console.Write("\nPlease choose an item: ");
                    string inp = Console.ReadLine();

                    bool containsAll = character.inventory.Contains("Copper") &&
                        character.inventory.Contains("Gold") &&
                        character.inventory.Contains("Godlike Recipe") &&
                        character.inventory.Contains("Ore") &&
                        character.inventory.Contains($"Enchanced {inp}");

                    if (containsAll)
                    {
                        Console.WriteLine($"\nYou have crafted Godlike {inp}");
                        character.inventory.Remove("Copper");
                        character.inventory.Remove("Ore");
                        character.inventory.Remove("Gold");
                        character.inventory.Remove("Godlike Recipe");
                        character.inventory.Remove($"Enchanced {inp}");
                        character.inventory.Add($"Godlike {inp}");
                    }
                    else
                    {
                        Console.WriteLine("You do not have the required materials!");
                    }
                }
                else
                {
                    Console.WriteLine("You have exited the Crafting Machine!");
                }
            }
            else
            {
                Console.WriteLine("\nNothing to Craft!");
            }


        }
        static void CharInformation(Character character)
        {
            Console.WriteLine($"\nCurrent character information: {character.health}/{character.maximumHealth}HP, {character.mana}/{character.maximumMana}MP, {character.damage} damage, {character.level} level, {character.experience}EXP.");
        }
        static void Rest(Character character)
        {
            Console.Write("\nDo you want to rest: ");
            string cmd = Console.ReadLine();

            if (cmd == "Yes")
            {
                Console.WriteLine("Every second you get 5HP and 1MP.");
                Stopwatch watch = new Stopwatch();

                watch.Start();

                Console.Write("When you are ready to play, write Done: ");
                Console.ReadLine();

                watch.Stop();

                double timeRested = watch.Elapsed.TotalSeconds;

                int healedTotal = 0;
                int manaTotal = 0;

                for (int i = 0; i < timeRested; i++)
                {
                    if (character.mana + 1 > 100)
                    {
                        character.mana = 100;
                        break;
                    }
                    character.mana += 1;
                    manaTotal += 1;
                }
                for (int i = 0; i < timeRested; i++)
                {
                    if (character.health + 15 > character.maximumHealth)
                    {
                        character.health = character.maximumHealth;
                        break;
                    }
                    character.health += 15;
                    healedTotal += 15;
                }

                Console.WriteLine($"You healed for {healedTotal} and restored {manaTotal} mana!");
                CharInformation(character);
            }
            else
            {
                return;
            }
        }
        static void UsePotions(Character character)
        {
            Console.WriteLine("\nWhich potion do you want to use, Health or Mana Potion?");
            Console.Write("Please choose: ");
            string cmd = Console.ReadLine();

            if (cmd.Contains("Health") && character.inventory.Contains("Health Potion"))
            {
                if (character.health + 500 <= character.maximumHealth)
                {
                    character.health += 500;
                    Console.WriteLine("You have restored 500HP!");
                }
                else
                {
                    character.health = character.maximumHealth;
                    Console.WriteLine($"You have restored {character.maximumHealth - character.health}HP!");
                }

                character.inventory.Remove("Health Potion");
            }
            else if (cmd.Contains("Mana") && character.inventory.Contains("Mana Potion"))
            {
                int manaPotion = 50;
                if (character.mana + manaPotion <= character.maximumMana)
                {
                    character.mana += manaPotion;
                    Console.WriteLine($"You have restored {manaPotion}MP!");
                }
                else
                {
                    character.mana = character.maximumMana;
                    Console.WriteLine($"You have restored {character.maximumMana - character.mana}MP!");
                }

                character.inventory.Remove("Mana Potion");
            }
            else
            {
                Console.WriteLine($"You do not have a {cmd}!");
            }
        }
        static void Inventory(Character character)
        {
            Console.WriteLine($"\nYou currently have {character.inventory.Count} items!");
            if (character.inventory.Count > 0)
            {
                Console.WriteLine(string.Join(", ", character.inventory));
                Console.Write("\nDo you want to equip or throw an item? - ");
            }
            else
            {
                return;
            }

            string toEquip = Console.ReadLine();

            if (toEquip == "Equip")
            {
                Console.Write("\nPlease choose an item: ");
                string item = Console.ReadLine();

                Items itemm = new Items();
                Items currItem = new Items();

                itemm.Setup(item);

                if (character.inventory.Contains(item))
                {
                    if (item.Contains("Helmet"))
                    {
                        character.inventory.Add(character.helmet);
                        currItem.Setup(character.helmet);
                    }
                    else if (item.Contains("Armor"))
                    {
                        character.inventory.Add(character.armor);
                        currItem.Setup(character.armor);
                    }
                    else if (item.Contains("Gloves"))
                    {
                        character.inventory.Add(character.gloves);
                        currItem.Setup(character.gloves);
                    }
                    else if (item.Contains("Boots"))
                    {
                        character.inventory.Add(character.boots);
                        currItem.Setup(character.boots);
                    }
                    else if (item.Contains("Necklace"))
                    {
                        character.inventory.Add(character.necklace);
                        currItem.Setup(character.necklace);
                    }
                    else if (item.Contains("Ring"))
                    {
                        character.inventory.Add(character.ring);
                        currItem.Setup(character.ring);
                    }
                    else if (item.Contains("Shield") && character.classJob == "Knight")
                    {
                        character.inventory.Add(character.shield);
                        currItem.Setup(character.shield);
                    }
                    else if (item.Contains("Staff") || item.Contains("Bow") || item.Contains("Sword")) // TO DO: FIX IT
                    {
                        character.inventory.Add(character.weapon);
                        currItem.Setup(character.weapon);
                    }


                    character.inventory.Remove(item);

                    if (item.Contains("Armor"))
                    {
                        character.maximumHealth -= currItem.health;
                        character.damage -= currItem.damage;
                        character.armor = itemm.itemName;
                        character.maximumHealth += itemm.health;
                        character.damage += itemm.damage;
                    }
                    else if (item.Contains("Helmet"))
                    {
                        character.maximumHealth -= currItem.health;
                        character.damage -= currItem.damage;
                        character.helmet = itemm.itemName;
                        character.maximumHealth += itemm.health;
                        character.damage += itemm.damage;
                    }
                    else if (item.Contains("Boots"))
                    {
                        character.maximumHealth -= currItem.health;
                        character.damage -= currItem.damage;
                        character.boots = itemm.itemName;
                        character.maximumHealth += itemm.health;
                        character.damage += itemm.damage;
                    }
                    else if (item.Contains("Gloves"))
                    {
                        character.maximumHealth -= currItem.health;
                        character.damage -= currItem.damage;
                        character.gloves = itemm.itemName;
                        character.maximumHealth += itemm.health;
                        character.damage += itemm.damage;
                    }
                    else if (item.Contains("Ring"))
                    {
                        character.maximumHealth -= currItem.health;
                        character.damage -= currItem.damage;
                        character.ring = itemm.itemName;
                        character.maximumHealth += itemm.health;
                        character.damage += itemm.damage;
                    }
                    else if (item.Contains("Necklace"))
                    {
                        character.maximumHealth -= currItem.health;
                        character.damage -= currItem.damage;
                        character.necklace = itemm.itemName;
                        character.maximumHealth += itemm.health;
                        character.damage += itemm.damage;
                    }
                    else if (item.Contains("Shield") && character.classJob == "Knight")
                    {
                        character.maximumHealth -= currItem.health;
                        character.damage -= currItem.damage;
                        character.shield = itemm.itemName;
                        character.maximumHealth += itemm.health;
                        character.damage += itemm.damage;
                    }
                    else if (item.Contains("Staff") || item.Contains("Sword") || item.Contains("Bow")) 
                    {
                        bool isMuse = item.Contains("Staff") && character.classJob == "Muse";
                        bool isArcher = item.Contains("Bow") && character.classJob == "Archer";
                        bool isKnight = item.Contains("Sword") && character.classJob == "Knight";

                        if (isMuse || isArcher || isKnight)
                        {
                            character.damage -= currItem.damage;
                            character.damage += itemm.damage;
                            character.weapon = itemm.itemName;
                            Console.WriteLine($"\nYou have equipped a new item: {item}!");
                            Console.WriteLine($"Your current Damage is {character.damage}!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\nThe weapon is not for your class job!");
                            return;
                        }
                        
                    }
                    else if (item.Contains("Shield") && character.classJob != "Knight")
                    {
                        Console.WriteLine("\nYou can only equip Shield on Knight!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\nYou can only equip Armor/Boots/Gloves/Helmet/Necklace/Ring/Weapon/Shield!");
                        return;
                    }
                    Console.WriteLine($"\nYou have equipped a new item: {item}!");
                    Console.WriteLine($"Your current Maximum Health is {character.maximumHealth}HP!");

                   
                }
                else
                {
                    Console.WriteLine($"You do not have {item}!");
                }
            }
            else if (toEquip == "Throw")
            {
                Console.Write("Please choose an item: ");
                string item = Console.ReadLine();

                if (character.inventory.Contains(item))
                {
                    Console.WriteLine($"You have removed {item}!");
                    character.inventory.Remove(item);
                }
                else
                {
                    Console.WriteLine($"You do not have {item} in your inventory!");
                }
            }
        }

        static void Teleport(Character character)
        {
            Console.WriteLine("Which teleport Scroll you would like to use?");
            string teleport = Console.ReadLine();

            if (character.inventory.Contains(teleport))
            {
                string map = string.Empty;

                if (teleport == "Scroll of Ra") map = "Ra";
                else if (teleport == "Scroll of Luna") map = "Luna";
                else if (teleport == "Scroll of Junon") map = "Junon";

                if (character.map != map)
                {
                    character.map = map;
                    Console.WriteLine($"You have teleported to {character.map}!");
                    character.inventory.Remove(teleport);
                }
                else
                {
                    Console.WriteLine($"You are currently in {map}.");
                }

            }
            else if (character.map == teleport)
            {
                Console.WriteLine($"You are already in {teleport}!");
            }
            else
            {
                Console.WriteLine($"You do not have {teleport}!");
            }
        }
        static void Skill(Character character, Monster monster)
        {
            Console.WriteLine($"\nYou have 3 skills, {character.skillOne} - 10MP, {character.skillTwo} - 20MP, {character.skillThree} - 50MP.");
            Console.WriteLine($"Current MP - {character.mana}!");
            Console.Write("Which skill would you like to use: ");

            string cmd = Console.ReadLine();

            int skillShotDamage = 0;
            int skillMana = 0;

            if (cmd == character.skillOne)
            {
                skillShotDamage = 150;
                skillMana = 10;
            }
            else if (cmd == character.skillTwo)
            {
                skillShotDamage = 200;
                skillMana = 20;
            }
            else if (cmd == character.skillThree)
            {
                skillShotDamage = 350;
                skillMana = 50;
            }

            if (character.mana - skillMana < 0)
            {
                Console.WriteLine("\nYou cannot use the skill, not enough MP!");
                Console.WriteLine("Would you like to use Mana Potion?");
                Console.Write("Please choose: ");
                string inpt = Console.ReadLine();

                if (inpt == "Yes")
                {
                    if (character.inventory.Contains("Mana Potion"))
                    {
                        character.mana += 50;
                        character.inventory.Remove("Mana Potion");
                        Console.WriteLine("You have used a Mana Potion!");

                        monster.health -= skillShotDamage;
                        character.mana -= skillMana;
                    }
                    else
                    {
                        Console.WriteLine("\nYou do not have a Mana Potion!");
                        Console.WriteLine("You used a Basic Attack instead!");
                        monster.health -= character.damage;
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("You used a Basic Attack!");
                    monster.health -= character.damage;
                }
            }
            else
            {
                monster.health -= skillShotDamage;
                character.mana -= skillMana;
            }


            Console.WriteLine($"\nYour current mana is {character.mana}!");

            if (monster.health <= 0)
            {
                Console.WriteLine($"You have killed {monster.monsterName} with {cmd}!");
                return;
            }
            else
            {
                character.health -= monster.damage;
                Console.WriteLine($"You have done {skillShotDamage} damage to {monster.monsterName} and left it with {monster.health}HP!");
            }
        }
        static void Fight(Character character)
        {
            string mobOne = string.Empty;
            string mobTwo = string.Empty;
            string mobThree = string.Empty;

            if (character.map == "Junon")
            {
                mobOne = "Rat";
                mobTwo = "Bear";
                mobThree = "Dragon";
            }
            else if (character.map == "Luna")
            {
                mobOne = "Worm";
                mobTwo = "Yeti";
                mobThree = "Marbas";
            }
            else if (character.map == "Ra")
            {
                mobOne = "Spider";
                mobTwo = "Tiger";
                mobThree = "Obelisk";
            }

            Console.WriteLine($"\nMonsters in {character.map}:");
            Console.WriteLine($"  Regular: {mobOne}, 200 Health, 20 Damage, 10 Experience");
            Console.WriteLine($"  Hard: {mobTwo}, 500 Health, 50 Damage, 50 Experience");
            Console.WriteLine($"  Boss: {mobThree}, 2000 Health, 100 Damage, 100 Experience");
            Console.Write("Please choose a monster to fight: ");

            Monster monster = new Monster();

            string monsterToFight = Console.ReadLine();

            monster.Setup(monsterToFight);

            while (monster.health > 0)
            {

                Console.WriteLine("\nBasic Attack or Use a Skill?");
                Console.Write("Please choose: ");
                string cmd = Console.ReadLine();

                if (cmd.Contains("Skill"))
                {
                    Skill(character, monster);
                }
                else
                {
                    monster.health -= character.damage;
                    if (monster.health <= 0)
                    {
                        break;
                    }
                    else
                    {
                        character.health -= monster.damage;
                    }
                    Console.WriteLine($"\nYou attacked {monster.monsterName} with Basic Attack for {character.damage} damage and left it with {monster.health}HP!");
                }

                if (character.health - monster.damage <= 0)
                {
                    Console.WriteLine("You are about to die, do you want to run or use Health Potion?");
                    Console.Write("Please choose what you want to do: ");
                    string inp = Console.ReadLine();

                    if (inp.Contains("Health"))
                    {
                        if (character.inventory.Contains("Health Potion"))
                        {
                            character.health += 500;
                            character.inventory.Remove("Health Potion");
                        }
                        else
                        {
                            Console.WriteLine("You do not have Health Potion, you have to run away!");
                            return;
                        }
                    }
                    else if (inp.Contains("Run"))
                    {

                        Console.WriteLine("You do not have a Health Potion, you have to run away!");
                        return;
                    }
                    else
                    {
                        character.health -= monster.damage;

                        if (character.health <= 0)
                        {
                            Console.WriteLine($"You have died from {monster.monsterName}!");
                            Console.WriteLine("You will be reborn in Junon and your inventory will be wiped!");
                            return;
                        }
                    }
                }
            }

            Random random = new Random();

            string randomItem = monster.drops[random.Next(0, monster.drops.Count)];

            character.Inventory(randomItem);

            if (character.experience + monster.experience >= 100)
            {
                character.level++;
                character.experience = (character.experience + monster.experience - 100);

                Console.WriteLine("\nYou have leveled up!");
                Console.WriteLine($"Your current level is {character.level}!");
            }
            else
            {
                character.experience += monster.experience;
            }

            Console.WriteLine($"\nYou have killed {monster.monsterName} for {monster.experience}EXP and looted {randomItem}!");
            Console.WriteLine($"Your current Health, Mana and Experience are: {character.health}/{character.maximumHealth}HP, {character.mana}/{character.maximumMana}MP, {character.experience}/100EXP!");
        }
    }
}

