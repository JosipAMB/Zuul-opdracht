using System;

class Player
{
    // field
    private Inventory backpack;
    // constructor
    public Player()
    {
    // 25kg is best zwaar om de hele dag te dragen
    backpack = new Inventory(25);
    CurrentRoom = null;
    health = 100;
    }
    // methods
    public bool TakeFromChest(string itemName)
    {
    // TODO implementeer:
    // Haal het Item uit de Room
    // Zet het in je backpack
    // Bekijk de return values
    // Past het Item niet? Zet het terug in de chest
    // Laat de speler weten wat er gebeurt
    // Return true/false voor succes/mislukt
    return false;
    }
    public bool DropToChest(string itemName)
    {
    // TODO implementeer:
    // Haal Item uit je backpack
    // Zet het in de Room
    // Bekijk de return values
    // Laat de speler weten wat er gebeurt
    // Return true/false voor succes/mislukt
    return false;
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
    public int Health
    {
        get { return health; }
    }
}
