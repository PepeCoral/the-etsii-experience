using HandyScripts;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager: Singleton<GameManager>
{
    [SerializeField]
    private ResourceManager resourceManager;
    [SerializeField]
    private CardManager cardManager;




    protected override void Awake()
    {
        base.Awake();
        loadCards();
        cardManager.Initialize();

        resourceManager = new ResourceManager();
    }

    public Card getCurrentCard()
    {

        return cardManager.currentCard;
    }
    public Dictionary<GameResource, int> getResources()
    {
        return resourceManager.resources;
    }
    private void loadCards() {
        List<Card> cards = Resources.LoadAll<Card>("1").ToList();
        Shuffle(cards);
        cardManager = new CardManager(cards);
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
       cardManager.makeDecision(isLeft);
    }

    public void UpdateUI()
    {
        cardManager.ForceCardUpdate();
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}
