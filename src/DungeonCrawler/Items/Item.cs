using System;
using DungeonCrawler.Characters;

namespace DungeonCrawler.Items
{
    public abstract class Item
    {
        // Common properties for all items
        protected string name;
        protected string description;
        protected int value;

        // Public properties to access item attributes
        public string Name => name;

        // Constructor
        public Item(string name, string description, int value)
        {
            this.name = name;
            this.description = description;
            this.value = value;
        }

        // Abstract method to use the item
        public abstract void Use(Player p); 
    }
}
