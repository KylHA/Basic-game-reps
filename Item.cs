using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Creating A class for Basic needs of an item
public class Item
{
    public int ID;
    public string Name;
    public string Description;
    public Dictionary<string, int> stats = new Dictionary<string, int>();// Item Stats will be kept as dict.

    public Item(int ID, string Name, string Description, Dictionary<string, int> stats)
    {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
        this.stats = stats;
    }

    //Constructor for copy item duplicating
    public Item(Item item)
    {
        this.ID = item.ID;
        this.Name = item.Name;
        this.Description = item.Description;
        this.stats = item.stats;
    }
}