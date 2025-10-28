using System.Collections.Generic;
using UnityEngine;

public class CardManager
{

    Queue<Card> mainCardQueue;
    Queue<Card> priorityCardQueue;


    public CardManager(List<Card> cards)
    {
        mainCardQueue = new Queue<Card>(cards);
        priorityCardQueue = new Queue<Card>();
        setCurrentCard(nextCard());
    }

    public Card currentCard { private set; get; }


    private Card nextCard() {

        if (priorityCardQueue.Count > 0) 
        {
            return priorityCardQueue.Dequeue();
        }
        return mainCardQueue.Dequeue();
    }

    public bool hasCards() => mainCardQueue.Count> 0 || priorityCardQueue.Count > 0;


    public void makeDecision(bool isLeft)
    {
        processEffects(isLeft ? currentCard.getLeftEffects() : currentCard.getRightEffects());
        setCurrentCard(nextCard());
    }

    private void processEffects(List<ICardEffect> effects) {

        effects.ForEach(effect => effect.apply());
    }


    public void AddCard(Card card) { mainCardQueue.Enqueue(card); }
    public void AddCardWithPriority(Card card) { priorityCardQueue.Enqueue(card); }


    private void setCurrentCard(Card card)
    {
        currentCard = card;
        Debug.Log(currentCard.mainText);
        Debug.Log("Left option: " +  currentCard.leftOptionName);
        Debug.Log("Right option: " +  currentCard.rightOptionName);
    }


    

}
