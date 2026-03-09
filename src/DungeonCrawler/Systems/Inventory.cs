using System;
using DungeonCrawler.Items;
using DungeonCrawler.Characters;

namespace DungeonCrawler.Systems
{
    public class Inventory
    {
        // Properties
        private List<Item> items;
        private Weapon? equippedWeapon;
        private Armor? equippedArmor;
        private int maxCapacity;

        // Public properties to access equipped items
        public Weapon? EquippedWeapon => equippedWeapon;
        public Armor? EquippedArmor => equippedArmor;

        // Constructor
        public Inventory(int maxCapacity)
        {
            this.items = new List<Item>();
            this.equippedWeapon = null;
            this.equippedArmor = null;
            this.maxCapacity = maxCapacity;
        }

        // AddItem method
        public bool AddItem(Item item)
        {
            int currentCount = items.Count;

            // Don't count equipped items towards the inventory limit
            if (equippedWeapon != null)
            {
                currentCount--;
            }
            if (equippedArmor != null)
            {
                currentCount--;
            }

            // Check if there's space in the inventory
            if (currentCount >= maxCapacity)
            {
                Console.WriteLine("Inventory is full. Cannot add item.");
                return false;
            }

            // Add the item to the inventory
            items.Add(item);

            return true;
        }

        // RemoveItem method 

        // FindItemInInventory method
        private Item? FindItemInInventory(string itemName)
        {
            foreach (Item item in items) 
            {
                if (item.Name.ToLower() == itemName.ToLower())
                {
                    return item;
                }

            }
            return null;
        }

        // UseItem method
        public void UseItem(Player p, string itemName)
        {
            // Find the item in the inventory
            Item? item = FindItemInInventory(itemName);


            if (item != null)
            {
                item.Use(p);
            }
            else
            {
                Console.WriteLine($"Item '{itemName}' not found in inventory.");
            }

            // If the item is a consumable, remove it from the inventory after use
            if (item is Potion)
            {
                items.Remove(item);
                Console.WriteLine($"Consumed {item.Name} and removed it from inventory.");
            }
        }

        // EquipWeapon method
        public void EquipWeapon(Weapon weapon)
        {
            // If weapon is already equipped, unequip it first
            if (equippedWeapon != null)
            {
                Console.WriteLine($"Unequipped {equippedWeapon.Name}.");
            }

            // Equip the new weapon
            equippedWeapon = weapon;
            Console.WriteLine($"Equipped {weapon.Name}.");
            Console.WriteLine($"Attack Bonus: {weapon.AttackBonus}");
            Console.WriteLine($"Critical Chance: {weapon.CriticalChance}");
        }

        // EquipArmor method
        public void EquipArmor(Armor armor)
        {
            // If armor is already equipped, unequip it first
            if (equippedArmor != null)
            {
                Console.WriteLine($"Unequipped {equippedArmor.Name}.");
            }
            // Equip the new armor
            equippedArmor = armor;
            Console.WriteLine($"Equipped {armor.Name}.");
            Console.WriteLine($"Defense Bonus: {armor.DefenseBonus}");
        }

        // DisplayInventory method
    }
}
