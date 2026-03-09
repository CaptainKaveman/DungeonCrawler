using System;
using DungeonCrawler.Characters;

namespace DungeonCrawler.Items
{
    public class Weapon : Item
    {
        // Additional properties specific to weapons
        private int attackBonus;
        private int criticalChance; // Chance to deal double damage. Percentage chance 0-100

        // Public properties to access weapon attributes
        public int AttackBonus => attackBonus;
        public int CriticalChance => criticalChance;

        // Constructor
        public Weapon(string name, string description, int value, int attackBonus, int criticalChance) 
            : base(name, description, value)
        {
            this.attackBonus = attackBonus;
            this.criticalChance = criticalChance;
        }

        // Use method - equips the weapon (for simplicity, we just print a message)
        public override void Use(Player p)
        {
            p.EquipWeapon(this);
        }
    }
}
