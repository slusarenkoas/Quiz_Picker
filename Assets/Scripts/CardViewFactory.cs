using System.Collections.Generic;
using MessagePipe;
using Messages;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class CardViewFactory
{
    private readonly ISubscriber<TargetChosenMessage> _targetChosenSubscriber;
    private readonly IPublisher<TargetCardFoundMessage> _targetCardFoundPublisher;
    private readonly ObjectPool<CardView> _cardViewPool;
    private readonly List<CardView> _usedCards = new();

    public CardViewFactory(IObjectResolver container, CardView cardPrefab, 
        ISubscriber<TargetChosenMessage> targetChosenSubscriber,
        IPublisher<TargetCardFoundMessage> targetCardFoundPublisher)
    {
        _targetChosenSubscriber = targetChosenSubscriber;
        _targetCardFoundPublisher = targetCardFoundPublisher;

        _cardViewPool = new ObjectPool<CardView>(
            createFunc: () => container.Instantiate(cardPrefab),
            actionOnGet: card => card.gameObject.SetActive(true),
            actionOnRelease: card => card.gameObject.SetActive(false));
    }

    public void CreateCardView(CardData cardData, Transform parent)
    {
        var cardView = _cardViewPool.Get();
        
        cardView.Initialize(cardData,_targetChosenSubscriber,_targetCardFoundPublisher);
        cardView.transform.SetParent(parent);
        
        _usedCards.Add(cardView);
    }

    public void Reset()
    {
        foreach (var usedCard in _usedCards)
        {
            _cardViewPool.Release(usedCard);
        }
        
        _usedCards.Clear();
    }
}