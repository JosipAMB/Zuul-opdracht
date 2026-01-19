using System.Collections.Generic;

class Inventory
{
    // fields
    private int maxWeight;
    private Dictionary<string, Item> items;

    // constructor
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        items = new Dictionary<string, Item>();
    }

    // Zet item in inventory
    public bool Put(string itemName, Item item)
    {
        // bestaat al?
        if (items.ContainsKey(itemName))
            return false;

        // past het qua gewicht?
        if (item.Weight > FreeWeight())
            return false;

        items.Add(itemName, item);
        return true;
    }

    // Haal item uit inventory
    public Item Get(string itemName)
    {
        if (!items.ContainsKey(itemName))
            return null;

        Item item = items[itemName];
        items.Remove(itemName);
        return item;
    }

    // Totaal gewicht
    public int TotalWeight()
    {
        int total = 0;
        foreach (Item item in items.Values)
        {
            total += item.Weight;
        }
        return total;
    }

    // Vrij gewicht
    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    // Toon items
    public string Show()
    {
        if (items.Count == 0)
            return "nothing";

        string result = "";
        foreach (Item item in items.Values)
        {
            result += item.Description + " ";
        }
        return result;
    }
}