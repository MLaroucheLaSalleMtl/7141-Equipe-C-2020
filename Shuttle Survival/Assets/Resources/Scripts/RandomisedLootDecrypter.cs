using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomisedLootDecrypter
{
    private static RandomisedLootDecrypter instance;

    float modifier = 1;

    private RandomisedLootDecrypter()
    {
        
    }

    public static RandomisedLootDecrypter GetInstance()
    {
        if (RandomisedLootDecrypter.instance == null)
        {
            RandomisedLootDecrypter.instance = new RandomisedLootDecrypter();
        }
        return instance;
    }

    public ResourcesPack DecryptRandomisedLoot(RandomisedLoot randomisedLoot)
    {        
        ItemStack[] itemStacks = new ItemStack[randomisedLoot.resourcesPossible.Length];
        for (int i = 0; i < randomisedLoot.resourcesPossible.Length; i++)
        {
            
            float rand = UnityEngine.Random.Range(0.00f, 1.00f);
            if (randomisedLoot.chancesToGive[i] >= rand)
            {
                int quantity = UnityEngine.Random.Range(Mathf.RoundToInt(randomisedLoot.resourcesPossible[i].Quantite * (1 - randomisedLoot.resourcesVariation[i]) * modifier),
                                                        Mathf.RoundToInt(randomisedLoot.resourcesPossible[i].Quantite * (1 + randomisedLoot.resourcesVariation[i])* modifier));
                if (quantity == 0) quantity = 1;
                ItemStack loot = new ItemStack(quantity, randomisedLoot.resourcesPossible[i].Item);
                itemStacks[i] = loot;
            }
        }
        ItemStack[] cleanedUpItemStacks = itemStacks.Where(i => i != null).ToArray();
        ResourcesPack rolledLoot = new ResourcesPack();
        rolledLoot.resources = cleanedUpItemStacks;
        return rolledLoot;
    }

    public void ActivateModifier()
    {
        modifier = 1.5f;
    }
}
