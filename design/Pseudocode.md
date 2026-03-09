# Pseudocode - Key Algorithms

---

## 0. Class Definitions

### Character (Base Class)
> **File: `Character.cs`**
```
CLASS Character:
    // Properties
    name: string
    maxHealth: int
    currentHealth: int
    attackPower: int
    level: int

    // Constructor
    CONSTRUCTOR Character(name, maxHealth, attackPower):
        this.name = name
        this.maxHealth = maxHealth
        this.currentHealth = maxHealth
        this.attackPower = attackPower
        this.level = 1
```

### Player (extends Character)
> **File: `Player.cs`**
```
CLASS Player EXTENDS Character:
    // Additional Properties
    experience: int
    experienceToNextLevel: int
    defense: int
    inventory: Inventory

    // Constructor
    CONSTRUCTOR Player(name):
        BASE(name, maxHealth: 100, attackPower: 10)
        this.experience = 0
        this.experienceToNextLevel = 100
        this.defense = 5
        this.inventory = new Inventory(maxCapacity: 10)
```

### Enemy (extends Character)
> **File: `Enemy.cs`**
```
CLASS Enemy EXTENDS Character:
    // Additional Properties
    experienceReward: int
    dropItem: Item  // can be NULL

    // Constructor
    CONSTRUCTOR Enemy(name, maxHealth, attackPower, experienceReward):
        BASE(name, maxHealth, attackPower)
        this.experienceReward = experienceReward
        this.dropItem = NULL
```

### Item (Abstract Base Class)
> **File: `Item.cs`**
```
CLASS Item (ABSTRACT):
    // Properties
    name: string
    description: string
    value: int

    // Constructor
    CONSTRUCTOR Item(name, description, value):
        this.name = name
        this.description = description
        this.value = value

    // Abstract - must be implemented by subclasses
    ABSTRACT FUNCTION Use(player)
```

### Weapon (extends Item)
> **File: `Weapon.cs`**
```
CLASS Weapon EXTENDS Item:
    // Additional Properties
    attackBonus: int
    criticalChance: int  // percentage (0-100)

    // Constructor
    CONSTRUCTOR Weapon(name, description, value, attackBonus, criticalChance):
        BASE(name, description, value)
        this.attackBonus = attackBonus
        this.criticalChance = criticalChance

    // Use method - equips the weapon
    FUNCTION Use(player):
        player.inventory.EquipWeapon(this)
```

### Armor (extends Item)
> **File: `Armor.cs`**
```
CLASS Armor EXTENDS Item:
    // Additional Properties
    defenseBonus: int

    // Constructor
    CONSTRUCTOR Armor(name, description, value, defenseBonus):
        BASE(name, description, value)
        this.defenseBonus = defenseBonus

    // Use method - equips the armor
    FUNCTION Use(player):
        player.inventory.EquipArmor(this)
```

### Potion (extends Item)
> **File: `Potion.cs`**
```
CLASS Potion EXTENDS Item:
    // Additional Properties
    healAmount: int

    // Constructor
    CONSTRUCTOR Potion(name, description, value, healAmount):
        BASE(name, description, value)
        this.healAmount = healAmount

    // Use method - heals the player
    FUNCTION Use(player):
        player.currentHealth = player.currentHealth + this.healAmount
        IF player.currentHealth > player.maxHealth:
            player.currentHealth = player.maxHealth
        PRINT "Used {this.name}. Restored {this.healAmount} HP!"
        PRINT "HP: {player.currentHealth}/{player.maxHealth}"
```

### Inventory
> **File: `Inventory.cs`**
```
CLASS Inventory:
    // Properties
    items: List<Item>
    equippedWeapon: Weapon  // can be NULL
    equippedArmor: Armor    // can be NULL
    maxCapacity: int

    // Constructor
    CONSTRUCTOR Inventory(maxCapacity):
        this.items = new List<Item>()
        this.equippedWeapon = NULL
        this.equippedArmor = NULL
        this.maxCapacity = maxCapacity
```

### Room
> **File: `Room.cs`**
```
CLASS Room:
    // Properties
    name: string
    description: string
    enemies: List<Enemy>
    items: List<Item>
    exits: Dictionary<string, Room>  // key = direction ("north", etc.)
    isCleared: bool

    // Constructor
    CONSTRUCTOR Room(name, description):
        this.name = name
        this.description = description
        this.enemies = new List<Enemy>()
        this.items = new List<Item>()
        this.exits = new Dictionary<string, Room>()
        this.isCleared = FALSE
```

### Game
> **File: `Game.cs`**
```
CLASS Game:
    // Properties
    player: Player
    rooms: List<Room>
    currentRoom: Room
    isRunning: bool

    // Constructor
    CONSTRUCTOR Game(player, rooms, currentRoom):
        this.player = player
        this.rooms = rooms
        this.currentRoom = currentRoom
        this.isRunning = TRUE
```

---

## 1. Combat System

### Player Attack with Weapon Bonus and Critical Hits
> **File: `CombatManager.cs`**
```
FUNCTION PlayerAttack(player, enemy):
    // Calculate base damage
    baseDamage = player.attackPower
    
    // Add equipped weapon bonus
    IF player.inventory.equippedWeapon IS NOT NULL:
        weaponBonus = player.inventory.equippedWeapon.attackBonus
        baseDamage = baseDamage + weaponBonus
        
        // Check for critical hit
        critChance = player.inventory.equippedWeapon.criticalChance
        randomNumber = GenerateRandom(1, 100)
        
        IF randomNumber <= critChance:
            PRINT "Critical Hit!"
            baseDamage = baseDamage * 2
    
    // Apply damage to enemy
    enemy.TakeDamage(baseDamage)
    
    PRINT "{player.name} attacks {enemy.name} for {baseDamage} damage!"
    
    RETURN baseDamage
END FUNCTION
```

### Enemy Attack with Player Defense
> **File: `CombatManager.cs`**
```
FUNCTION EnemyAttack(enemy, player):
    // Calculate base damage
    baseDamage = enemy.attackPower
    
    // Subtract player defense
    finalDamage = baseDamage - player.defense
    
    // Ensure minimum damage of 1
    IF finalDamage < 1:
        finalDamage = 1
    
    // Apply damage to player
    player.TakeDamage(finalDamage)
    
    PRINT "{enemy.name} attacks {player.name} for {finalDamage} damage!"
    
    RETURN finalDamage
END FUNCTION
```

### Take Damage with Health Bounds
> **File: `Character.cs`**
```
FUNCTION TakeDamage(character, damage):
    character.currentHealth = character.currentHealth - damage

    // Prevent health from going below zero
    IF character.currentHealth < 0:
        character.currentHealth = 0

    PRINT "{character.name} has {character.currentHealth}/{character.maxHealth} HP remaining"
END FUNCTION
```

### Is Alive Check
> **File: `Character.cs`**
```
FUNCTION IsAlive(character):
    RETURN character.currentHealth > 0
END FUNCTION
```

### Get Experience Reward
> **File: `Enemy.cs`**
```
FUNCTION GetExperienceReward(enemy):
    RETURN enemy.experienceReward
END FUNCTION
```

### Get Drop Item
> **File: `Enemy.cs`**
```
FUNCTION GetDropItem(enemy):
    RETURN enemy.dropItem  // may be NULL
END FUNCTION
```

### Complete Combat Loop
> **File: `CombatManager.cs`**
```
FUNCTION CombatLoop(player, room):
    enemies = room.GetEnemies()
    
    IF enemies.Count == 0:
        PRINT "There are no enemies to fight."
        RETURN
    
    PRINT "Combat begins!"
    
    WHILE enemies.Count > 0 AND player.IsAlive():
        // Display enemy options
        PRINT "\nEnemies:"
        FOR each enemy IN enemies:
            PRINT "{index}. {enemy.name} ({enemy.currentHealth}/{enemy.maxHealth} HP)"
        
        // Get player target selection
        IF enemies.Count > 1:
            PRINT "Select target (enter number): "
            targetIndex = GetPlayerInput()
            selectedEnemy = enemies[targetIndex]
        ELSE:
            selectedEnemy = enemies[0]
        
        // PLAYER TURN
        PRINT "\n--- Your Turn ---"
        damageDealt = PlayerAttack(player, selectedEnemy)
        
        // Check if enemy died
        IF NOT selectedEnemy.IsAlive():
            PRINT "{selectedEnemy.name} has been defeated!"
            
            // Award experience
            expGained = selectedEnemy.GetExperienceReward()
            player.GainExperience(expGained)
            
            // Drop item
            droppedItem = selectedEnemy.GetDropItem()
            IF droppedItem IS NOT NULL:
                room.AddItem(droppedItem)
                PRINT "{selectedEnemy.name} dropped {droppedItem.name}!"
            
            // Remove enemy from room
            enemies.Remove(selectedEnemy)
            
            // Check if all enemies defeated
            IF enemies.Count == 0:
                room.isCleared = TRUE
                PRINT "\nVictory! All enemies defeated!"
                RETURN
            
            CONTINUE to next iteration
        
        // ENEMY TURN (if still alive)
        PRINT "\n--- Enemy Turn ---"
        EnemyAttack(selectedEnemy, player)
        
        // Check if player died
        IF NOT player.IsAlive():
            PRINT "\nYou have been defeated! Game Over."
            RETURN
        
        // Option to flee (advanced feature)
        PRINT "\nContinue fighting? (y/n): "
        choice = GetPlayerInput()
        IF choice == "n":
            PRINT "You flee from combat!"
            RETURN
    
    END WHILE
END FUNCTION
```

---

## 2. Experience and Leveling System

### Gain Experience with Level Up Check
> **File: `Player.cs`**
```
FUNCTION GainExperience(player, expAmount):
    PRINT "Gained {expAmount} experience!"
    
    player.experience = player.experience + expAmount
    
    // Check for level up
    WHILE player.experience >= player.experienceToNextLevel:
        LevelUp(player)
END FUNCTION
```

### Level Up Algorithm
> **File: `Player.cs`**
```
FUNCTION LevelUp(player):
    player.level = player.level + 1
    
    // Subtract experience used for level up
    player.experience = player.experience - player.experienceToNextLevel
    
    // Increase stats
    player.maxHealth = player.maxHealth + 10
    player.attackPower = player.attackPower + 2
    
    // Fully heal player
    player.currentHealth = player.maxHealth
    
    // Calculate next level requirement (exponential)
    player.experienceToNextLevel = player.experienceToNextLevel * 2
    // OR Linear: player.experienceToNextLevel = player.level * 100
    
    PRINT "╔═══════════════════════╗"
    PRINT "║    LEVEL UP!          ║"
    PRINT "╚═══════════════════════╝"
    PRINT "You are now level {player.level}!"
    PRINT "Max Health: {player.maxHealth}"
    PRINT "Attack Power: {player.attackPower}"
END FUNCTION
```

---

## 3. Inventory Management

### Add Item to Inventory
> **File: `Inventory.cs`**
```
FUNCTION AddItem(inventory, item):
    // Check if inventory is full
    currentCount = inventory.items.Count
    
    // Don't count equipped items toward capacity
    IF inventory.equippedWeapon IS NOT NULL:
        currentCount = currentCount - 1
    IF inventory.equippedArmor IS NOT NULL:
        currentCount = currentCount - 1
    
    IF currentCount >= inventory.maxCapacity:
        PRINT "Inventory is full! Cannot pick up {item.name}."
        RETURN FALSE
    
    inventory.items.Add(item)
    PRINT "Picked up {item.name}!"
    RETURN TRUE
END FUNCTION
```

### Equip Weapon
> **File: `Inventory.cs`**
```
FUNCTION EquipWeapon(inventory, weapon):
    // If weapon already equipped, unequip it first
    IF inventory.equippedWeapon IS NOT NULL:
        oldWeapon = inventory.equippedWeapon
        PRINT "Unequipped {oldWeapon.name}"
        // Old weapon stays in inventory
    
    // Equip new weapon
    inventory.equippedWeapon = weapon
    PRINT "Equipped {weapon.name}!"
    PRINT "  Attack Bonus: +{weapon.attackBonus}"
    PRINT "  Critical Chance: {weapon.criticalChance}%"
END FUNCTION
```

### Use Item from Inventory
> **File: `Inventory.cs`**
```
FUNCTION UseItem(player, itemName):
    item = FindItemInInventory(player.inventory, itemName)

    IF item IS NULL:
        PRINT "You don't have that item."
        RETURN

    // Call the item's Use method (polymorphism!)
    item.Use(player)

    // If it's a consumable (Potion), remove it
    IF item IS Potion:
        player.inventory.items.Remove(item)
END FUNCTION
```

### Find Item in Inventory
> **File: `Inventory.cs`**
```
FUNCTION FindItemInInventory(inventory, itemName):
    FOR each item IN inventory.items:
        IF item.name.ToLower() == itemName.ToLower():
            RETURN item
    RETURN NULL
END FUNCTION
```

### Equip Armor
> **File: `Inventory.cs`**
```
FUNCTION EquipArmor(inventory, armor):
    // If armor already equipped, unequip it first
    IF inventory.equippedArmor IS NOT NULL:
        oldArmor = inventory.equippedArmor
        PRINT "Unequipped {oldArmor.name}"
        // Old armor stays in inventory

    // Equip new armor
    inventory.equippedArmor = armor
    PRINT "Equipped {armor.name}!"
    PRINT "  Defense Bonus: +{armor.defenseBonus}"
END FUNCTION
```

### Display Inventory
> **File: `Display.cs`**
```
FUNCTION DisplayInventory(inventory):
    PRINT "\n╔═══ INVENTORY ═══╗"

    IF inventory.items.Count == 0:
        PRINT "  Your inventory is empty."
    ELSE:
        FOR each item IN inventory.items:
            PRINT "  - {item.name}: {item.description}"

    PRINT "\nEquipped:"
    IF inventory.equippedWeapon IS NOT NULL:
        PRINT "  Weapon: {inventory.equippedWeapon.name}"
    ELSE:
        PRINT "  Weapon: None"

    IF inventory.equippedArmor IS NOT NULL:
        PRINT "  Armor: {inventory.equippedArmor.name}"
    ELSE:
        PRINT "  Armor: None"

    PRINT "╚═════════════════╝"
END FUNCTION
```

---

## 4. Room Navigation

### Get Enemies from Room
> **File: `Room.cs`**
```
FUNCTION GetEnemies(room):
    RETURN room.enemies
END FUNCTION
```

### Get Items from Room
> **File: `Room.cs`**
```
FUNCTION GetItems(room):
    RETURN room.items
END FUNCTION
```

### Get Exit from Room
> **File: `Room.cs`**
```
FUNCTION GetExit(room, direction):
    IF room.exits.ContainsKey(direction):
        RETURN room.exits[direction]
    RETURN NULL
END FUNCTION
```

### Add Enemy to Room
> **File: `Room.cs`**
```
FUNCTION AddEnemy(room, enemy):
    room.enemies.Add(enemy)
END FUNCTION
```

### Add Item to Room
> **File: `Room.cs`**
```
FUNCTION AddItem(room, item):
    room.items.Add(item)
END FUNCTION
```

### Add Exit to Room
> **File: `Room.cs`**
```
FUNCTION AddExit(room, direction, targetRoom):
    room.exits[direction] = targetRoom
END FUNCTION
```

### Move to New Room
> **File: `GameManager.cs`**
```
FUNCTION MoveToRoom(game, direction):
    currentRoom = game.currentRoom
    
    // Check if exit exists
    nextRoom = currentRoom.GetExit(direction)
    
    IF nextRoom IS NULL:
        PRINT "You can't go that way."
        RETURN FALSE
    
    // Move player to new room
    game.currentRoom = nextRoom
    PRINT "\nYou move {direction}..."
    
    // Display new room
    DisplayRoom(nextRoom)
    
    // Check for enemies
    IF nextRoom.GetEnemies().Count > 0:
        PRINT "\n⚠ WARNING: Enemies detected in this room! ⚠"
    
    RETURN TRUE
END FUNCTION
```

### Display Room
> **File: `Room.cs`**
```
FUNCTION DisplayRoom(room):
    PRINT "\n╔════════════════════════════════════╗"
    PRINT "║ {room.name, centered}"
    PRINT "╚════════════════════════════════════╝"
    PRINT room.description
    
    // Show enemies
    enemies = room.GetEnemies()
    IF enemies.Count > 0:
        PRINT "\n⚔ Enemies:"
        FOR each enemy IN enemies:
            PRINT "  - {enemy.name} ({enemy.currentHealth}/{enemy.maxHealth} HP)"
    ELSE:
        PRINT "\n✓ No enemies"
    
    // Show items
    items = room.GetItems()
    IF items.Count > 0:
        PRINT "\n📦 Items:"
        FOR each item IN items:
            PRINT "  - {item.name}"
    
    // Show exits
    exits = room.exits.Keys
    PRINT "\n🚪 Exits: {join exits with ', '}"
    
    IF room.isCleared:
        PRINT "\n[Room Cleared]"
END FUNCTION
```

---

## 5. Save/Load System

### Save Game
> **File: `SaveSystem.cs`**
```
FUNCTION SaveGame(game, filename):
    TRY:
        // Create save data object
        saveData = {
            playerName: game.player.name,
            playerHealth: game.player.currentHealth,
            playerMaxHealth: game.player.maxHealth,
            playerAttack: game.player.attackPower,
            playerDefense: game.player.defense,
            playerLevel: game.player.level,
            playerExperience: game.player.experience,
            playerExpToNext: game.player.experienceToNextLevel,
            currentRoomName: game.currentRoom.name,
            inventory: {
                items: LIST of item names,
                equippedWeaponName: game.player.inventory.equippedWeapon?.name,
                equippedArmorName: game.player.inventory.equippedArmor?.name
            },
            rooms: [
                FOR each room IN game.rooms:
                {
                    name: room.name,
                    isCleared: room.isCleared,
                    enemies: LIST of remaining enemy names,
                    items: LIST of item names in room
                }
            ]
        }
        
        // Convert to JSON
        jsonString = SerializeToJson(saveData)
        
        // Write to file
        WriteToFile(filename, jsonString)
        
        PRINT "Game saved successfully!"
        
    CATCH Exception as error:
        PRINT "Failed to save game: {error.message}"
END FUNCTION
```

### Load Game
> **File: `SaveSystem.cs`**
```
FUNCTION LoadGame(filename):
    TRY:
        // Read file
        jsonString = ReadFromFile(filename)
        
        // Deserialize from JSON
        saveData = DeserializeFromJson(jsonString)
        
        // Recreate player
        player = new Player(saveData.playerName)
        player.currentHealth = saveData.playerHealth
        player.maxHealth = saveData.playerMaxHealth
        player.attackPower = saveData.playerAttack
        player.defense = saveData.playerDefense
        player.level = saveData.playerLevel
        player.experience = saveData.playerExperience
        player.experienceToNextLevel = saveData.playerExpToNext
        
        // Recreate inventory
        FOR each itemName IN saveData.inventory.items:
            item = CreateItemByName(itemName)
            player.inventory.AddItem(item)
        
        // Recreate equipped items
        IF saveData.inventory.equippedWeaponName IS NOT NULL:
            weapon = CreateWeaponByName(saveData.inventory.equippedWeaponName)
            player.inventory.EquipWeapon(weapon)
        
        // Recreate rooms (similar process)
        // ... recreate room state from saveData.rooms
        
        PRINT "Game loaded successfully!"
        RETURN game
        
    CATCH FileNotFoundException:
        PRINT "Save file not found."
        RETURN NULL
        
    CATCH Exception as error:
        PRINT "Failed to load game: {error.message}"
        RETURN NULL
END FUNCTION
```

---

## 6. Command Processing

### Pick Up Item from Room
> **File: `GameManager.cs`**
```
FUNCTION PickupItem(player, room, itemName):
    // Find item in room
    item = NULL
    FOR each roomItem IN room.GetItems():
        IF roomItem.name.ToLower() == itemName.ToLower():
            item = roomItem
            BREAK

    IF item IS NULL:
        PRINT "There is no {itemName} here."
        RETURN

    // Try to add to player inventory
    IF player.inventory.AddItem(item):
        room.items.Remove(item)
END FUNCTION
```

### Equip Item (Weapon or Armor)
> **File: `GameManager.cs`**
```
FUNCTION EquipItem(player, itemName):
    item = FindItemInInventory(player.inventory, itemName)

    IF item IS NULL:
        PRINT "You don't have {itemName}."
        RETURN

    IF item IS Weapon:
        player.inventory.EquipWeapon(item)
    ELSE IF item IS Armor:
        player.inventory.EquipArmor(item)
    ELSE:
        PRINT "{item.name} cannot be equipped."
END FUNCTION
```

### Main Command Parser
> **File: `GameManager.cs`**
```
FUNCTION ProcessCommand(game, input):
    // Parse command into parts
    parts = input.ToLower().Split(' ')
    command = parts[0]
    argument = parts.Length > 1 ? parts[1] : ""
    
    SWITCH command:
        CASE "look":
            DisplayRoom(game.currentRoom)
            
        CASE "move", "go":
            IF argument IS EMPTY:
                PRINT "Move where? (north, south, east, west)"
            ELSE:
                MoveToRoom(game, argument)
        
        CASE "attack", "fight":
            CombatLoop(game.player, game.currentRoom)
        
        CASE "take", "pickup":
            IF argument IS EMPTY:
                PRINT "Take what?"
            ELSE:
                PickupItem(game.player, game.currentRoom, argument)
        
        CASE "use":
            IF argument IS EMPTY:
                PRINT "Use what?"
            ELSE:
                UseItem(game.player, argument)
        
        CASE "equip":
            IF argument IS EMPTY:
                PRINT "Equip what?"
            ELSE:
                EquipItem(game.player, argument)
        
        CASE "inventory", "inv":
            DisplayInventory(game.player.inventory)
        
        CASE "stats":
            DisplayPlayerStats(game.player)
        
        CASE "save":
            SaveGame(game, "savegame.json")
        
        CASE "help":
            DisplayHelp()
        
        CASE "quit", "exit":
            PRINT "Save before quitting? (y/n)"
            response = GetPlayerInput()
            IF response == "y":
                SaveGame(game, "savegame.json")
            game.isRunning = FALSE
        
        DEFAULT:
            PRINT "Unknown command. Type 'help' for available commands."
END FUNCTION
```

---

## 7. Game Initialization

### Initialize New Game
> **File: `GameManager.cs`**
```
FUNCTION InitializeNewGame():
    PRINT "Enter your name: "
    playerName = GetPlayerInput()
    
    // Create player
    player = new Player(playerName)
    
    // Create rooms
    rooms = CreateDungeonRooms()
    
    // Set starting room
    currentRoom = rooms[0]  // First room (entrance)
    
    // Create game
    game = new Game(player, rooms, currentRoom)
    
    PRINT "\nWelcome to the dungeon, {playerName}!"
    DisplayRoom(currentRoom)
    
    RETURN game
END FUNCTION
```

### Create Dungeon Rooms
> **File: `DungeonBuilder.cs`**
```
FUNCTION CreateDungeonRooms():
    rooms = []
    
    // Room 1: Entrance
    entrance = new Room("Dungeon Entrance", 
        "A dark stone entrance. Torches flicker on the walls.")
    rooms.Add(entrance)
    
    // Room 2: Hallway
    hallway = new Room("Dark Hallway",
        "A narrow corridor. Water drips from the ceiling.")
    goblin = new Enemy("Goblin", 20, 5, 50)
    hallway.AddEnemy(goblin)
    potion = new Potion("Health Potion", "Restores 25 HP", 10, 25)
    hallway.AddItem(potion)
    rooms.Add(hallway)
    
    // Room 3: Armory
    armory = new Room("Ancient Armory",
        "Rusty weapons line the walls.")
    sword = new Weapon("Iron Sword", "A sturdy blade", 50, 5, 10)
    armory.AddItem(sword)
    rooms.Add(armory)
    
    // Room 4: Treasury
    treasury = new Room("Treasury",
        "Gold coins are scattered everywhere!")
    orc = new Enemy("Orc Warrior", 40, 10, 100)
    treasury.AddEnemy(orc)
    armor = new Armor("Leather Armor", "Light armor", 75, 3)
    treasury.AddItem(armor)
    rooms.Add(treasury)
    
    // Room 5: Dragon Lair (Boss)
    lair = new Room("Dragon's Lair",
        "A massive chamber. The air is scorching hot.")
    dragon = new Enemy("Dragon", 100, 25, 500)
    legendaryWeapon = new Weapon("Dragon Slayer", "Legendary sword", 500, 15, 25)
    dragon.dropItem = legendaryWeapon
    lair.AddEnemy(dragon)
    rooms.Add(lair)
    
    // Connect rooms
    entrance.AddExit("north", hallway)
    hallway.AddExit("south", entrance)
    hallway.AddExit("east", armory)
    hallway.AddExit("west", treasury)
    armory.AddExit("west", hallway)
    armory.AddExit("north", lair)
    treasury.AddExit("east", hallway)
    lair.AddExit("south", armory)
    
    RETURN rooms
END FUNCTION
```

---

## 8. Main Game Loop
> **File: `Program.cs`**

```
FUNCTION Main():
    PRINT "╔═══════════════════════════════════╗"
    PRINT "║   DUNGEON CRAWLER                 ║"
    PRINT "║   A Text-Based RPG Adventure      ║"
    PRINT "╚═══════════════════════════════════╝"
    PRINT ""
    PRINT "1. New Game"
    PRINT "2. Load Game"
    PRINT "3. Exit"
    PRINT ""
    PRINT "Enter choice: "
    
    choice = GetPlayerInput()
    
    game = NULL
    
    SWITCH choice:
        CASE "1":
            game = InitializeNewGame()
        CASE "2":
            game = LoadGame("savegame.json")
            IF game IS NULL:
                RETURN  // Failed to load
        CASE "3":
            PRINT "Goodbye!"
            RETURN
        DEFAULT:
            PRINT "Invalid choice."
            RETURN
    
    // Main game loop
    WHILE game.isRunning:
        PRINT "\n> "
        command = GetPlayerInput()
        ProcessCommand(game, command)
        
        // Check win condition
        allRoomsCleared = TRUE
        FOR each room IN game.rooms:
            IF NOT room.isCleared:
                allRoomsCleared = FALSE
                BREAK
        
        IF allRoomsCleared:
            PRINT "\n╔═══════════════════════════════════╗"
            PRINT "║   CONGRATULATIONS!                ║"
            PRINT "║   You cleared the dungeon!        ║"
            PRINT "╚═══════════════════════════════════╝"
            game.isRunning = FALSE
    
    PRINT "\nThanks for playing!"
END FUNCTION
```

---

## Helper Functions

### Get Player Input
> **File: `Helpers.cs`**
```
FUNCTION GetPlayerInput():
    // Read a line of text from the console and trim whitespace
    RETURN Console.ReadLine().Trim()
END FUNCTION
```

### Random Number Generation
> **File: `Helpers.cs`**
```
FUNCTION GenerateRandom(min, max):
    // Returns random integer between min and max (inclusive)
    random = new Random()
    RETURN random.Next(min, max + 1)
END FUNCTION
```

### Display Player Stats
> **File: `Display.cs`**
```
FUNCTION DisplayPlayerStats(player):
    PRINT "\n╔═══ PLAYER STATS ═══╗"
    PRINT "Name: {player.name}"
    PRINT "Level: {player.level}"
    PRINT "HP: {player.currentHealth}/{player.maxHealth}"
    PRINT "Attack: {player.attackPower}"
    PRINT "Defense: {player.defense}"
    PRINT "Experience: {player.experience}/{player.experienceToNextLevel}"
    
    IF player.inventory.equippedWeapon IS NOT NULL:
        weapon = player.inventory.equippedWeapon
        PRINT "Weapon: {weapon.name} (+{weapon.attackBonus} ATK, {weapon.criticalChance}% CRIT)"
    ELSE:
        PRINT "Weapon: None"
    
    IF player.inventory.equippedArmor IS NOT NULL:
        armor = player.inventory.equippedArmor
        PRINT "Armor: {armor.name} (+{armor.defenseBonus} DEF)"
    ELSE:
        PRINT "Armor: None"
    
    PRINT "╚══════════════════╝"
END FUNCTION
```

### Display Help
> **File: `Display.cs`**
```
FUNCTION DisplayHelp():
    PRINT "\n╔═══ AVAILABLE COMMANDS ═══╗"
    PRINT "║ look - View current room"
    PRINT "║ move [direction] - Move to another room (north, south, east, west)"
    PRINT "║ attack - Fight enemies in current room"
    PRINT "║ take [item] - Pick up an item"
    PRINT "║ use [item] - Use an item from inventory"
    PRINT "║ equip [item] - Equip a weapon or armor"
    PRINT "║ inventory - View your items"
    PRINT "║ stats - View your character stats"
    PRINT "║ save - Save your game"
    PRINT "║ help - Show this help menu"
    PRINT "║ quit - Exit the game"
    PRINT "╚══════════════════════════╝"
END FUNCTION
```

---

## Testing Scenarios

### Test 1: Basic Combat
```
1. Create player and enemy
2. Player attacks enemy
3. Verify damage is calculated correctly
4. Enemy attacks player
5. Verify player health decreases
6. Continue until one dies
EXPECTED: Combat resolves correctly, appropriate messages display
```

### Test 2: Leveling System
```
1. Create player at level 1
2. Award 100 experience
3. Verify level up occurs
4. Check stats increased
5. Award more experience
6. Verify exponential XP requirement
EXPECTED: Player levels up, stats increase, XP resets properly
```

### Test 3: Inventory Management
```
1. Create empty inventory (capacity: 5)
2. Add 5 items
3. Try to add 6th item
4. Verify rejection
5. Use a potion
6. Verify it's removed
7. Add another item
EXPECTED: Capacity enforced, consumables removed after use
```

### Test 4: Equipment System
```
1. Player equips weapon
2. Verify attack power increases
3. Player attacks enemy
4. Verify damage includes weapon bonus
5. Player equips different weapon
6. Verify old weapon unequipped, new one equipped
EXPECTED: Equipment modifies stats correctly
```

### Test 5: Room Navigation
```
1. Create 3 connected rooms
2. Player moves north
3. Verify in new room
4. Try invalid direction
5. Verify error message
6. Move back south
EXPECTED: Movement works, invalid moves rejected
```

This pseudocode should help you understand the logic before writing actual C# code!
