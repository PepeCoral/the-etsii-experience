using HandyScripts;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager: Singleton<GameManager>
{
    [SerializeField]
    private ResourceManager resourceManager;
    private CardManager cardManager;




    protected override void Awake()
    {
        base.Awake();
        loadCards();
        resourceManager = new ResourceManager();
    }

    public Dictionary<GameResource, int> getResources()
    {
        return resourceManager.resources;
    }
    private void loadCards() {
        //TODO: Shuffle List
        cardManager = new CardManager(Resources.LoadAll<Card>("1").ToList());
    }

    public void AddCardToPriorityQueue(Card card)
    {
        cardManager.AddCardWithPriority(card);
    }

    public void ModifyResources(ResourceEffect resourceEffect)
    {
        resourceManager.modifyResource(resourceEffect);
    }

    public void MakeDecision(bool isLeft)
    {
        if(cardManager.hasCards()) cardManager.makeDecision(isLeft);
    }
}
