using DungeonCrawler.Characters;
using System;

namespace DungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a player and an enemy for testing
            Player p = new Player("Kyle");
            Enemy goblin = new Enemy("Goblin", 20, 5, 2, 50);

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
            }
        }
    }
}