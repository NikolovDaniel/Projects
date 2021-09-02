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
            character.Setup(input); // Setting up the character

            Console.Write("\nChoose a name for your character: "); // Choosing a name
            string charName = Console.ReadLine();

           
            character.nickname = charName;
            character.Skills(); // Setting up the character Skills

            QuestOne questOne = new QuestOne();
            questOne.Setup();
            QuestTwo questTwo = new QuestTwo();
            questTwo.status = "Not active";
            QuestThree questThree = new QuestThree();
            questThree.status = "Not active";
            QuestFour questFour = new QuestFour();
            questFour.status = "Not active";

            string storyQuest = string.Empty;

            string command = string.Empty;

            while (command != "END") // Executing the commands given by the User
            {
                Console.WriteLine("\nAvailable commands: Fight/Buy/Sell/Quest/Dungeon/Rest/Craft/Teleport/Use Potion/Inventory/Char Information/END.");
                Console.Write("Please enter a command: ");
                command = Console.ReadLine();

                bool validCommands = command == "Dungeon" || command == "Buy" || command == "Sell" || command == "Fight" || command == "Rest" || command == "Craft"
                    || command == "Teleport" || command == "Quest" || command == "Use Potion" || command == "Inventory" || command == "Char Information";

                if (validCommands)
                {
                    if (command == "Fight")
                    {
                        Fight(character, questOne, questTwo, questThree, questFour);

                        if (character.health <= 0)
                        {
                            character.inventory.Clear();
                        }
                    }
                    else if (command == "Dungeon")
                    {
                        Monster monster = new Monster();
                        Dungeon(character, questFour, monster);
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
                    else if (command == "Inventory")
                    {
                        Inventory(character);
                    }
                    else if (command == "Char Information")
                    {
                        CharInformation(character);
                    }
                    else if (command == "Quest")
                    {
                        if (storyQuest != "Activated")
                        {
                            Console.WriteLine("\nYou started the Main Quest Line, it consists of 4 quests with different difficulty, good luck in your journey!");
                            storyQuest = "Activated";
                        }
                        Quest(character, questOne, questTwo, questThree, questFour);
                    }
                    else if (command == "Sell")
                    {
                        Sell(character);
                    }
                    else if (command == "Buy")
                    {
                        Buy(character);
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


        static void Sell(Character character)
        {
            Console.WriteLine("\nWelcome to the Street Sell Shop!");
            Console.WriteLine("Here, you can sell your items!");
            Console.WriteLine($"\nItems in your inventory: {string.Join(", ", character.inventory)}");
            if (character.inventory.Count == 0)
            {
                Console.WriteLine("\nYour inventory is empty!");
                return;
            }
            Console.Write("\nWhat would you like to sell: ");
            string inp = Console.ReadLine();

           
            if (character.inventory.Contains(inp))
            {
                Console.WriteLine($"\nYou have successfully sold {inp} for 10 Zulies!");
                character.inventory.Remove(inp);
                character.zulies += 10;
            }
            else
            {
                Console.WriteLine("\nSorry, but you do not contain the item, please try again!");
            }
        }

        static void Buy(Character character)
        {
            List<string> shopItems = new List<string>() { "Health Potion", "Mana Potion" };
            Console.WriteLine("\nWelcome to the Street Buy Shop!");
            Console.WriteLine("Here, you can buy Health Potion for 25 Zulies and Mana Potion for 35 Zulies.");
            Console.Write($"You have {character.zulies} Zulies, what would you like to buy: ");
            string inp = Console.ReadLine();

            if (inp == "Health Potion")
            {
                Console.Write("\nHow many Health Potions, you would like to buy: ");
                int quantity = int.Parse(Console.ReadLine());

                int totalPriceRequired = quantity * 25;

                if (character.zulies >= totalPriceRequired)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        character.inventory.Add("Health Potion");
                        character.zulies -= 25;
                    }
                    Console.WriteLine($"\nYou successfully bought {quantity} Health Potions for {totalPriceRequired} Zulies.");
                }
                else
                {
                    Console.WriteLine("\nYou do not have enough Zulies, please try again!");
                }
            }
            else if (inp == "Mana Potion")
            {
                Console.Write("\nHow many Mana Potions, you would like to buy: ");
                int quantity = int.Parse(Console.ReadLine());

                int totalPriceRequired = quantity * 30;

                if (character.zulies >= totalPriceRequired)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        character.inventory.Add("Mana Potion");
                        character.zulies -= 30;
                    }
                    Console.WriteLine($"\nYou successfully bought {quantity} Mana Potions for {totalPriceRequired} Zulies.");
                }
                else
                {
                    Console.WriteLine("\nYou do not have enough Zulies, please try again!");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid item, please try again!");
            }
        }
        static void Dungeon(Character character, QuestFour questFour, Monster monster)
        {

            if (questFour.status == "Completed")
            {
                Console.WriteLine("You have finished the Main Quest and cannot enter the Dungeon!");
                return;
            }
            else if (character.level < 10 || questFour.status != "In progress")
            {
                Console.WriteLine("\nYou do not meet the requirements, you must be at least Level 10 and have started Quest(4)!");
                return;
            }
            else if (character.level >= 10 && questFour.status == "In progress")
            {
                if (character.maximumHealth >= 3000 && character.damage >= 300)
                {
                    Console.WriteLine($"\n{character.nickname} you are about to enter the Dungeon!");
                    Console.WriteLine("There are 5 Levels to complete:");
                    Console.WriteLine("First level: 3x Rats");
                    Console.WriteLine("Second level: 2x Worms, 1x Yeti, 1x Bear");
                    Console.WriteLine("Third level: 2x Bear, 2x Yeti");
                    Console.WriteLine("Fourth level: 1x Marbas, 1x Obelisk");
                    Console.WriteLine("Fifth level: 1x Sun God of Ra");

                    for (int i = 1; i <= 5; i++)
                    {
                        if (i == 1)
                        {
                            Console.WriteLine("\nWelcome to Level (1)!");
                            monster.Setup("Rat");

                            for (int j = 1; j <= 3; j++)
                            {
                                while (monster.health > 0)
                                {
                                    monster.health -= character.damage;
                                    if (monster.health <= 0)
                                    {
                                        break;
                                    }
                                    character.health -= monster.damage;
                                    if (character.health <= 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else if (i == 2)
                        {
                            Console.WriteLine("\nWelcome to Level (2)!");
                            monster.Setup("Worm");

                            for (int j = 1; j <= 2; j++)
                            {
                                while (monster.health > 0)
                                {
                                    monster.health -= character.damage;
                                    if (monster.health <= 0)
                                    {
                                        break;
                                    }
                                    character.health -= monster.damage;
                                    if (character.health <= 0)
                                    {
                                        break;
                                    }
                                }
                            }

                            monster.Setup("Yeti");

                            for (int j = 1; j <= 1; j++)
                            {
                                while (monster.health > 0)
                                {
                                    monster.health -= character.damage;
                                    if (monster.health <= 0)
                                    {
                                        break;
                                    }
                                    character.health -= monster.damage;
                                    if (character.health <= 0)
                                    {
                                        break;
                                    }
                                }
                            }

                            monster.Setup("Bear");

                            for (int j = 1; j <= 1; j++)
                            {
                                while (monster.health > 0)
                                {
                                    monster.health -= character.damage;
                                    if (monster.health <= 0)
                                    {
                                        break;
                                    }
                                    character.health -= monster.damage;
                                    if (character.health <= 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            Console.WriteLine("\nWelcome to Level(3)!");
                            monster.Setup("Bear");

                            for (int j = 1; j <= 2; j++)
                            {
                                while (monster.health > 0)
                                {
                                    monster.health -= character.damage;
                                    if (monster.health <= 0)
                                    {
                                        break;
                                    }
                                    character.health -= monster.damage;
                                    if (character.health <= 0)
                                    {
                                        break;
                                    }
                                }
                            }

                            monster.Setup("Yeti");

                            for (int j = 1; j <= 2; j++)
                            {
                                while (monster.health > 0)
                                {
                                    monster.health -= character.damage;
                                    if (monster.health <= 0)
                                    {
                                        break;
                                    }
                                    character.health -= monster.damage;
                                    if (character.health <= 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else if (i == 4)
                        {
                            Console.WriteLine("\nWelcome to Level(4)!");
                            monster.Setup("Marbas");

                            for (int j = 1; j <= 1; j++)
                            {
                                while (monster.health > 0)
                                {
                                    monster.health -= character.damage;
                                    if (monster.health <= 0)
                                    {
                                        break;
                                    }
                                    character.health -= monster.damage;
                                    if (character.health <= 0)
                                    {
                                        break;
                                    }
                                }
                            }

                            monster.Setup("Obelisk");

                            for (int j = 1; j <= 1; j++)
                            {
                                while (monster.health > 0)
                                {
                                    monster.health -= character.damage;
                                    if (monster.health <= 0)
                                    {
                                        break;
                                    }
                                    character.health -= monster.damage;
                                    if (character.health <= 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else if (i == 5)
                        {
                            Console.WriteLine("\nWelcome to Level(5)!");
                            monster.Setup("Sun God of Ra");

                            for (int j = 1; j <= 1; j++)
                            {
                                while (monster.health > 0)
                                {
                                    monster.health -= character.damage;
                                    if (monster.health <= 0)
                                    {
                                        break;
                                    }
                                    character.health -= monster.damage;
                                    if (character.health <= 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        if (character.health <= 0)
                        {
                            Console.WriteLine($"\nYou have died at Level ({i})!");
                            Console.WriteLine("You will be revived in Junon.");
                            character.map = "Junon";
                            character.health = character.maximumHealth;
                            return;
                        }

                        Console.WriteLine($"\nCongratulations, you have successed Level({i})!");
                        CharInformation(character);
                    }
                }
                else
                {
                    Console.WriteLine("You must have at least 3000HP and 300DMG in order to enter!");
                    return;
                }

                Console.WriteLine($"\nYou have defeated the Sun God of Ra and purified the whole earth, congratulations {character.nickname}!");
                character.inventory.Add("Godlike Helmet");
                character.inventory.Add("Godlike Armor");
                character.inventory.Add("Godlike Gloves");
                character.inventory.Add("Godlike Boots");
                character.inventory.Add("Godlike Ring");
                character.inventory.Add("Godlike Necklace");
                character.inventory.Add("Proof of Accomplishment");
                return;
            }
        }
        static void Quest(Character character, QuestOne questOne, QuestTwo questTwo, QuestThree questThree, QuestFour questFour)
        {
            bool isDone = questOne.status == "Completed" && questTwo.status == "Completed"
                && questThree.status == "Completed" && questFour.status == "Completed";

            if (isDone)
            {
                Console.WriteLine("\nYou have finished the Main Quest already. More quests will be added in the future!");
                return;
            }

            if (questOne.status == "Not active")
            {
                questOne.status = "In progress";
                Console.WriteLine("\nQuest (1):");
                Console.WriteLine($"\nDear {character.nickname}, nice to meet you! I am Lord Vestergard.");
                Console.WriteLine("A long and hard journey awaits you, for us to be free once again, you must help us bring the Diamond Eye with which we can purify our earth and restore everything back to what it was.");
                Console.WriteLine("Your first quest requires you to kill 5 Rats and 2 Bears. Please come back when you are done!");
                return;
            }
            else if (questOne.status == "In progress")
            {
                if (questOne.countRats < 5 && questOne.countBears < 2)
                {
                    Console.WriteLine($"\nYou need {5 - questOne.countRats} Rats and {2 - questOne.countBears} Bears.");
                    return;
                }
                else if (questOne.countBears < 2 && questOne.countRats == 5)
                {
                    Console.WriteLine($"\nYou need {2 - questOne.countBears} more Bears.");
                    return;
                }
                else if (questOne.countRats < 5 && questOne.countBears == 2)
                {
                    Console.WriteLine($"\nYou need {5 - questOne.countRats} more Rats.");
                    return;
                }
                else if (questOne.countRats == 5 && questOne.countBears == 2)
                {
                    Console.WriteLine($"\nYou have done the quest! You can proceed to the next one!");
                    Console.WriteLine($"\nYou have been rewarded with 3x Health Potions and 1x Mana Potion!");

                    for (int i = 0; i < 3; i++)
                    {
                        character.inventory.Add("Health Potion");
                    }

                    character.inventory.Add("Mana Potion");

                    questOne.status = "Completed";
                    return;
                }
            }

            if (questOne.status == "Completed" && character.level >= 3 && character.map == "Luna"
                && questTwo.status == "Not active")
            {
                Console.WriteLine($"\nCongratulations {character.nickname}, you have started Quest (2)!");
                Console.WriteLine($"\nQuest (2):");
                Console.WriteLine($"Hello {character.nickname}, first of all, welcome to Luna, it is a very cosy and cold place. This quest will require you to go hunt a few monsters, but beware, some of them are very strong and hard to kill! Good luck adventurer.");
                Console.WriteLine("Bring me 1x Yeti Charm and 1x Worm Liquid!");

                questTwo.Setup();

                return;
            }
            else if (questTwo.status == "In progress")
            {
                if (character.inventory.Contains("Yeti Charm") && !character.inventory.Contains("Worm Liquid"))
                {
                    Console.WriteLine($"\nYou do not have Worm Liquid!");
                    return;
                }
                else if (!character.inventory.Contains("Yeti Charm") && character.inventory.Contains("Worm Liquid"))
                {
                    Console.WriteLine($"\nYou do not have Yeti Charm!");
                    return;
                }
                else
                {
                    character.inventory.Remove("Yeti Charm");
                    character.inventory.Remove("Worm Liquid");

                    Console.WriteLine($"\nYou have done the quest! You can proceed to the next one!");
                    Console.WriteLine($"\nYou have been rewarded with 1x Enchanced Ring, 1x Enchanced Necklace and Fragment of the Diamond Eye!");

                    character.inventory.Add("Enchanced Ring");
                    character.inventory.Add("Enchanced Necklace");
                    character.inventory.Add("Fragment of the Diamond Eye");
                    questTwo.status = "Completed";
                    return;
                }
            }
            else if ((character.level < 3 || character.map != "Luna") && questTwo.status == "Not active")
            {
                Console.WriteLine($"\nYou do not meet the requirements. In order to start the Second Quest, you must be at least Level 3, have Quest (1) Completed and to be in map Luna!");
                return;
            }

            if (questTwo.status == "Completed" && questThree.status == "Not active"
                && character.level >= 7 && character.map == "Ra")
            {
                Console.WriteLine("\nNo way! You must be the one that everyone talk about! I see you have the Fragment of the Diamond Eye. ");
                Console.WriteLine("So... the legends must have been true. ");
                Console.WriteLine("A prophet told us that a stranger with the Fragment of the Diamond Eye would come to us, in this deserted place called Ra.");
                Console.WriteLine("We are told that the rest of the fragments are burried deep in the sand and some have been guarded by the strongest monsters.");
                Console.WriteLine("The fragment you have will guide you on your journey towards the rest. May the luck be with you stranger!");
                Console.WriteLine("Find the rest of the Fragments! Legends are saying that a big monster posses it.");

                questThree.status = "In progress";
                return;
            }
            else if (questThree.status == "In progress")
            {
                if (character.inventory.Contains("Broken Fragment") && !character.inventory.Contains("Fragment of The Eye"))
                {
                    Console.WriteLine("\nYou do not have Fragment of The Eye!");
                    return;
                }
                else if (!character.inventory.Contains("Broken Fragment") && character.inventory.Contains("Fragment of The Eye"))
                {
                    Console.WriteLine("\nYou do not have the Broken Fragment!");
                    return;
                }
                else
                {
                    character.inventory.Remove("Broken Fragment");
                    character.inventory.Remove("Fragment of the Eye");
                    character.inventory.Remove("Fragment of the Diamond Eye");

                    Console.WriteLine($"\nYou have done the quest! You can proceed to the next one!");
                    Console.WriteLine($"\nYou have been rewarded with 1x Enchanced Armor, 1x Enchanced Helmet, 1x Enchanced Gloves, 1x Enchanced Boots and Diamond Eye!");

                    character.inventory.Add("Enchanced Helmet");
                    character.inventory.Add("Enchanced Armor");
                    character.inventory.Add("Enchanced Gloves");
                    character.inventory.Add("Enchanced Boots");
                    character.inventory.Add("Diamond Eye");

                    questThree.status = "Completed";

                    return;
                }
            }
            else if ((character.level < 7 || character.map != "Ra") && questThree.status == "Not active")
            {
                Console.WriteLine($"\nYou do not meet the requirements. In order to start the Third Quest, you must be at least Level 7, have Quest(2) Completed and to be in map Ra!");
                return;
            }

            if (questThree.status == "Completed" && character.level >= 10 && questFour.status == "Not active")
            {

                Console.WriteLine("\nYou have claimed all the fragmanets and now it is time to craft the Diamond Eye. It is used to enter the Dungeon, which holds the purifier and the path to our freedom.");
                Console.WriteLine("Before entering the Dungeon, please make sure you meet the requirements for it.");

                questFour.status = "In progress";
                return;
            }
            else if (questFour.status == "In progress")
            {
                if (character.inventory.Contains("Proof of Accomplishment"))
                {
                    Console.WriteLine($"\nThank you for your service, {character.nickname}!");
                    Console.WriteLine("You have saved the earth and there is peace once again, the sun came up and the flowers blossomed up, we do not have much to offer, except for our gratitude... and a bit of zulies.");
                    Console.WriteLine("Farewell adventurer...");

                    character.zulies += 1000;
                    questFour.status = "Completed";
                    Console.WriteLine("\nYou have received 1000 Zulies.");
                    return;
                }
                else
                {
                    Console.WriteLine("\nYou do not have the required Dungeon Item!");
                }
                return;
            }
            else if (character.level < 10 && questThree.status != "Completed")
            {
                Console.WriteLine("\nYou do not meet the requirements. In order to start the Fourth Quest, you must be at least Level 10 and have Quest(3) Completed!");
                return;
            }

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
            if (character.inventory.Contains("Enchanced Necklace"))
            {
                itemsAvailable.Add("Necklace");
            }
            if (character.inventory.Contains("Enchanced Ring"))
            {
                itemsAvailable.Add("Ring");
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
            Console.WriteLine($"\nCurrent character information: {character.health}/{character.maximumHealth}HP, {character.mana}/{character.maximumMana}MP, {character.damage} damage, {character.level} level, {character.experience}EXP, map: {character.map}.");
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
                    if (character.mana + 1 > character.maximumMana)
                    {
                        character.mana = character.maximumMana;
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
                    Console.WriteLine($"You have restored {character.maximumHealth - character.health}HP!");
                    character.health = character.maximumHealth;
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
                    Console.WriteLine($"You have restored {character.maximumMana - character.mana}MP!");
                    character.mana = character.maximumMana;
                }

                character.inventory.Remove("Mana Potion");
            }
            else
            {
                Console.WriteLine($"You do not have a {cmd} Potion!");
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
            Console.WriteLine("\nWhich teleport Scroll you would like to use?");
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
        static void Fight(Character character, QuestOne questOne, QuestTwo questTwo, QuestThree questThree, QuestFour questFour)
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
            Console.WriteLine($"  Hard: {mobTwo}, 500 Health, 50 Damage, 20 Experience");
            Console.WriteLine($"  Boss: {mobThree}, 2000 Health, 100 Damage, 50 Experience");
            Console.Write("Please choose a monster to fight: ");

            Monster monster = new Monster();

            string monsterToFight = Console.ReadLine();

            monster.Setup(monsterToFight);

            if (monster.monsterName == mobOne || monster.monsterName == mobTwo || monster.monsterName == mobThree)
            {
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
            }
            else
            {
                Console.Write("\nPlease try again!");
                return;
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

            if (questOne.status == "In progress")
            {
                if (monster.monsterName == "Rat" && questOne.countRats < 5)
                {
                    questOne.countRats++;
                    Console.WriteLine($"\nQuest: {questOne.countBears}/2, {questOne.countRats}/5.");
                }
                else if (monster.monsterName == "Bear" && questOne.countBears < 2)
                {
                    questOne.countBears++;
                    Console.WriteLine($"\nQuest: {questOne.countBears}/2, {questOne.countRats}/5.");
                }
            }
        }
    }
}

