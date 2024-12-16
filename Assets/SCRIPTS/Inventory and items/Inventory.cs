using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();

    public void AddItem(ItemData itemToAdd)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
            }
        }
        items.Add(itemToAdd);
    }
 
}
