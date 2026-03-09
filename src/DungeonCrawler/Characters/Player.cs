using System;

namespace DungeonCrawler.Characters
{
    public class Player : Character
    {
        // Player-specific properties
        protected int experience;
        protected int experienceToNextLevel;
        // protected Inventory inventory;

        // Constructor
        public Player(string name)
            : base(name, 100, 10, 5)
        {
            this.experience = 0;
            this.experienceToNextLevel = 100; // Example value for leveling up
            // this.inventory = new Inventory(10); // Uncomment in Phase 2 when Inventory class is implemented
        }

        // Method to gain experience
        public void GainExperience(int expAmount)
        {
            Console.WriteLine($"Gained {expAmount} experience!");

            experience += expAmount;

            // Check for level up
            while (experience >= experienceToNextLevel)
            {
                LevelUp();
            }
        }

        // Method to level up
        public void LevelUp()
        {
            // Increase player level
            level++;

            // Subtract the experience needed for leveling up
            experience -= experienceToNextLevel; 

            // Increase stats
            maxHealth += 10; 
            attackPower += 2; 
            defense += 2;

            // Fully heal player on level up
            currentHealth = maxHealth;

            // Calculate new experience needed for the next level (increase by 20% each level)
            experienceToNextLevel = (int)(experienceToNextLevel * 1.2);

            Console.WriteLine("╔═══════════════════════╗");
            Console.WriteLine("║    LEVEL UP!          ║");
            Console.WriteLine("╚═══════════════════════╝");
            Console.WriteLine($"You are now level {level}");
            Console.WriteLine($"Max Health: {maxHealth}");
            Console.WriteLine($"Attack Power: {attackPower}");
            Console.WriteLine($"Defense: {defense}");
        }
    }
}
