using System;
using DungeonCrawler.Systems;

namespace DungeonCrawler.Characters
{
    public abstract class Character
    {
        protected string name; 
        protected int maxHealth;
        protected int currentHealth;
        protected int attackPower; 
        protected int defense;
        protected int level;

        // Public properties to access character attributes
        public string Name => name;
        public int AttackPower => attackPower;
        public int Defense => defense;
        public int CurrentHealth => currentHealth;
        public int MaxHealth => maxHealth;

        // Constructor
        public Character(string name, int maxHealth, int attackPower,int defense)
        {
            this.name = name;
            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth; // Start with full health
            this.attackPower = attackPower;
            this.defense = defense;
            this.level = 1;
        }

        // Method to take damage
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            // Prevent health from dropping below zero
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            Console.WriteLine($"{name} has {currentHealth}/{maxHealth} HP remaining.");
        }

        // Method to check if the character is alive
        public bool IsAlive()
        {
            return currentHealth > 0;
        }

        // Virtual method to attack another character (can be overridden by subclasses)
        public virtual void Attack(Character target)
        {
            int damage = attackPower - target.Defense;

            // Ensure that damage is at least 1
            if (damage < 1)
            {
                damage = 1;
            }
            target.TakeDamage(damage);
        }

        // Method to heal the character
        public void Heal(int amount)
        {
            currentHealth += amount;
            // Prevent health from exceeding max health
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
}
