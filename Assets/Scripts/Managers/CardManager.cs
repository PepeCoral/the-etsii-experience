using System.Collections.Generic;

[System.Serializable]
public class CardManager
{

    Queue<Card> mainCardQueue;
    Queue<Card> priorityCardQueue;
    public Card currentCard;

    public delegate void _OnCardUpdated();
    public static event _OnCardUpdated OnCardUpdated;
    public CardManager(List<Card> cards)
    {
        mainCardQueue = new Queue<Card>(cards);
        priorityCardQueue = new Queue<Card>();
    }

    public void Initialize()
    {
        currentCard = nextCard();
    }



    private Card nextCard() {

        if (priorityCardQueue.Count > 0) 
        {
            return priorityCardQueue.Dequeue();
        }
        return mainCardQueue.Dequeue();
    }

    private bool hasCards() {
        return mainCardQueue.Count + priorityCardQueue.Count > 0; 
    }


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
        if (OnCardUpdated != null) OnCardUpdated();
    }


    public void ForceCardUpdate() {
        if (OnCardUpdated != null) OnCardUpdated();

    }




}
