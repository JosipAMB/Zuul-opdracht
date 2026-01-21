using System;

class Player
{
    // field
    private Inventory backpack;
    // constructor
    public Player()
    {
    backpack = new Inventory(25);
    CurrentRoom = null;
    health = 100;
    }

    public string Use(string itemName)
    // Use method
    {
        Item item = backpack.Get(itemName);

        if (item == null)
        {
            return "You don't have that item.";
        }

        if (itemName == "book")
        {
            Heal(10);
            return "You read the book and feel better (+10 health).";
        }

        if (itemName == "sword")
        {
            return "You swing the sword. Looks dangerous!";
        }

        if (itemName == "key")
        {
            return "You use the key, but nothing happens.";
        }

        return "You can't use that.";
    }
    // methods
    public bool TakeFromChest(string itemName)
    {
        // haal item uit de kamer
        Item item = CurrentRoom.Chest.Get(itemName);

        if (item == null)
        {
            Console.WriteLine("Item is not in the room.");
            return false;
        }

        // probeer in backpack te stoppen
        if (!backpack.Put(itemName, item))
        {
            Console.WriteLine("Item doesn't fit in your inventory.");
            // leg terug in de room
            CurrentRoom.Chest.Put(itemName, item);
            return false;
        }

        Console.WriteLine("You picked up the " + itemName + ".");
        return true;
    }

    public bool DropToChest(string itemName)
    {
        // haal item uit backpack
        Item item = backpack.Get(itemName);

        if (item == null)
        {
            Console.WriteLine("You don't have that item.");
            return false;
        }

        // leg in de kamer
        CurrentRoom.Chest.Put(itemName, item);
        Console.WriteLine("You dropped the " + itemName + ".");
        return true;
    }
    // auto property
    public Room CurrentRoom { get; set; }
    // fields
    private int health;
    // constructor

    // methods
    public void Damage(int amount) // speler verliest health
    {
        health -= amount;
        if (health < 0) health = 0;
    }
    public void Heal(int amount) // speler krijgt health
    {
        health += amount;
        if (health > 100) health = 100; // max health is 100
    }
    public bool IsAlive() // checkt of speler nog leeft
    {
        return health > 0;
    }
    public int Health()
    {
        { return health; }
    }
}
