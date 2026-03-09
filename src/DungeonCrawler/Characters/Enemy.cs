using System;

namespace DungeonCrawler.Characters
{
    public class Enemy : Character
    {
        // Enemy-specific properties
        private int experienceReward;
        // protected Item dropItem = null; // Placeholder for future item drops

        // Constructor
        public Enemy(string name, int maxHealth, int attackPower, int defense, int experienceReward)
            : base(name, maxHealth, attackPower, defense)
        {
            this.experienceReward = experienceReward;
            //this.dropItem = null; // No item drops in Phase 1
        }

        // Method to get experience reward when defeated
        public int GetExperienceReward()
        {
            return experienceReward;
        }

        // Method to get drop item (currently returns null in Phase 1)
        // public Item GetDropItem() 
        // {
        //     return dropItem; // No item drops in Phase 1
        // }
        
    }
}
