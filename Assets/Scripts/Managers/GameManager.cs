using HandyScripts;
using System.Linq;
using UnityEngine;

public class GameManager: Singleton<GameManager>
{
    [SerializeField]
    private ResourceManager resourceManager;
    private CardManager cardManager;




    private void Start()
    {
        loadCards();
        resourceManager = new ResourceManager();
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
