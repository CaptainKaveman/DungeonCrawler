using System;
using DungeonCrawler.Characters;

namespace DungeonCrawler.Items
{
    public class Armor : Item
    {
        // Additional Properties
        private int defenseBonus;

        // Public property to access defense bonus
        public int DefenseBonus => defenseBonus;

        // Constructor
        public Armor(string name, string description, int value, int defenseBonus) 
            : base(name, description, value)
        {
            this.defenseBonus = defenseBonus;
        }

        // Use method - equips the armor
        public override void Use(Player p)
        {
            p.EquipArmor(this);
        }
    }
}
