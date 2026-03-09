using System;
using DungeonCrawler.Items;
using DungeonCrawler.Systems;

namespace DungeonCrawler.Characters
{
    public class Player : Character
    {
        // Player-specific properties
        protected int experience;
        protected int experienceToNextLevel;
        private Inventory inventory;

        // Public properties to access player-specific attributes
        public new int AttackPower => inventory.EquippedWeapon != null
            ? attackPower + inventory.EquippedWeapon.AttackBonus
            : attackPower;
        public new int Defense => inventory.EquippedArmor != null
            ? defense + inventory.EquippedArmor.DefenseBonus
            : defense;

        // Constructor
        public Player(string name)
            : base(name, 100, 10, 5)
        {
            this.experience = 0;
            this.experienceToNextLevel = 100; // Example value for leveling up
            this.inventory = new Inventory(20);
        }

        // Method to attack another character
        public override void Attack(Character target)
        {
            int damage = attackPower - target.Defense;

            // Ensure that damage is at least 1
            if (damage < 1)
            {
                damage = 1;
            }

            if (inventory.EquippedWeapon != null)
            {
                damage += inventory.EquippedWeapon.AttackBonus;
            }
            target.TakeDamage(damage);
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

        // Method to equip a weapon
        public void EquipWeapon(Weapon weapon)
        {
            inventory.EquipWeapon(weapon);
        }

        // Method to equip armor
        public void EquipArmor(Armor armor)
        {
            inventory.EquipArmor(armor);
        }

        // Method to add an item to the inventory
        public void AddItemToInventory(Item item)
        {
            if (inventory.AddItem(item))
            {
                Console.WriteLine($"Added {item.Name} to inventory.");
            }
            else
            {
                Console.WriteLine("Inventory is full! Cannot add item.");
            }
        }
    }
}
