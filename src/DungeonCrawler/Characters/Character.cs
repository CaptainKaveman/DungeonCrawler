using System;
using System.ComponentModel;

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

        public string Name => name;
        public int AttackPower => attackPower;

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

        // Method to attack another character
        public void Attack(Character target)
        {
            target.TakeDamage(attackPower);
        }
    }
}
