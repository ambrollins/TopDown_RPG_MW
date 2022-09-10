using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Collectibles_[] collectibleitems;

    private Dictionary<CollectableType, Collectibles_> collectibleitemDictionary =
        new Dictionary<CollectableType, Collectibles_>();

    private void Awake()
    {
        foreach (Collectibles_ item in collectibleitems)
        {
            AddItem(item);
        }
    }

    private void AddItem(Collectibles_ item)
    {
        if (!collectibleitemDictionary.ContainsKey(item.collectableType))
        {
            collectibleitemDictionary.Add(item.collectableType,item);
        }
    }

    public Collectibles_ GetItemByType(CollectableType type)
    {
        if (collectibleitemDictionary.ContainsKey(type))
        {
            return collectibleitemDictionary[type];
        }
        return null;
    }
    
}
