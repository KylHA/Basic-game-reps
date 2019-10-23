using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void BuildDatabase()
    {

        items = new List<Item>
        {
            new Item(0,"Sword","A sword",
            new Dictionary<string, int>
            {
                {"Power",10},
                {"Defence",8}
            }),
            ////////////////////////
            new Item(1,"Gun","A Gun",
            new Dictionary<string, int>
            {
                {"Power",15},
                {"Defence",6}
            })
        };
    }
    
    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(Item => Item.ID == id);
    }
    public Item GetItem(string name)
    {
        return items.Find(Item => Item.Name == name);
    }
}
