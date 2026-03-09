using System;
using DungeonCrawler.Characters;
using DungeonCrawler.Items;

namespace DungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a player and an enemy for testing
            Player p = new Player("Kyle");
            Enemy goblin = new Enemy("Goblin", 20, 5, 2, 50);

            // Create items for testing
            Weapon sword = new Weapon("Iron Sword", "A basic iron sword", 10, 5, 10);
            Armor leather = new Armor("Leather Armor", "Basic leather armor", 10, 5);
            Potion potion = new Potion("Lesser Health Potion", "Small potion that heals for 10 HP", 5, 10);

            // Add test items to player's inventory
            p.AddItemToInventory(sword);
            p.AddItemToInventory(leather);
            p.AddItemToInventory(potion);

            // Equip the weapon and armor
            Console.WriteLine("\n--- Equiping Items ---");
            sword.Use(p);
            leather.Use(p);

            // Display player stats
            Console.WriteLine("\n--- Player Stats ---");
            Console.WriteLine($"Name: {p.Name}");
            Console.WriteLine($"Health: {p.CurrentHealth}/{p.MaxHealth}");
            Console.WriteLine($"Attack: {p.AttackPower}");
            Console.WriteLine($"Defense: {p.Defense}");

            // Give the player some damage to test healing
            p.TakeDamage(50);
            Console.WriteLine("\n--- Using Potion ---");
            potion.Use(p);

            // Simulate a battle
            while (p.IsAlive() && goblin.IsAlive())
            {
                // Player attacks first
                Console.WriteLine("\nYour turn!");
                p.Attack(goblin);

                if (!goblin.IsAlive())
                {
                    Console.WriteLine(value: $"\nYou defeated the {goblin.Name}!");
                    p.GainExperience(goblin.GetExperienceReward());
                    break;
                }

                // Enemy attacks
                Console.WriteLine("\nEnemy turn!");
                goblin.Attack(p);

                if (!p.IsAlive())
                {
                    Console.WriteLine("\nYou have been defeated! Game Over.");
                    break;
                }

                // Player heals for testing purposes (you can replace this with actual player input later)
                if (p.CurrentHealth < 96)
                {
                    potion.Use(p);
                }
            }
        }
    }
}