using System.Collections.Generic;
using System.Linq;

public class UsedTargetsController
{
    private readonly HashSet<CardData> _usedCards = new();

    public bool HasUnusedTarget(CardBundleData cardBundle)
    {
        return cardBundle.Cards.Any(card => !IsUsed(card));
    }

    public bool IsUsed(CardData card)
    {
        return _usedCards.Contains(card);
    }

    public void MarkAsUsed(CardData card)
    {
        _usedCards.Add(card);
    }
}