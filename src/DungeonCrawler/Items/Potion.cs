using System;
using DungeonCrawler.Characters;

namespace DungeonCrawler.Items
{
    public class Potion : Item
    {
        // Additional properties specific to potions
        private int healAmount;

        // Constructor
        public Potion(string name, string description, int value, int healAmount) 
            : base(name, description, value)
        {
            this.healAmount = healAmount;
        }

        // Use method - heals the player
        public override void Use(Player p)
        {
            p.Heal(healAmount);

            Console.WriteLine($"\nYou use the {name} and heal for {healAmount} HP!");
            Console.WriteLine($"HP: {p.CurrentHealth}/{p.MaxHealth}");
        }
    }
}
