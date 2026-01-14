using System;

class Player
{
    // auto property
    public Room CurrentRoom { get; set; }

    // fields
    private int health;

    // constructor
    public Player()
    {
        CurrentRoom = null;
        health = 100;
    }

    // methods
    public void Damage(int amount) // speler verliest health
    {
        health -= amount;
        if (health < 0) health = 0;
    }

    public void Heal(int amount) // speler krijgt health
    {
        health += amount;
        if (health > 100) health = 100; // assuming max health is 100
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