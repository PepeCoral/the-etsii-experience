using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public interface ICardEffect
{
    public void apply();
}

[System.Serializable]
public class ResourceEffect {

    public int value;

    [SerializeField]
    public GameResource resource;
}

public class CardResourceEffect : ICardEffect
{
    [SerializeField]
    List<ResourceEffect> effects;
    


    public void apply()
    {
        effects.ForEach(effect => GameManager.Instance.ModifyResources(effect));
    }
}


public class AddPriorityCardEffect : ICardEffect
{
    [SerializeField]
    List<Card> cards;

    public void apply()
    {
        cards.ForEach(card => GameManager.Instance.AddCardToPriorityQueue(card));
    }
}

