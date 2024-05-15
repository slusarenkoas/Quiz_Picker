using UnityEngine;
using UnityEngine.Pool;

public class LevelSpawner
{
    private readonly CardViewFactory _cardViewFactory;
    private readonly GameObjectGrid _grid;

    public LevelSpawner(CardViewFactory cardViewFactory,GameObjectGrid grid)
    {
        _cardViewFactory = cardViewFactory;
        _grid = grid;
    }

    public void Spawn(CardBundleData cardBundle, LevelData level, CardData target)
    {
        var cardsCount = level.Columns * level.Rows;
        var targetRandomIndex = Random.Range(0, cardsCount);

        var unUsedCards = ListPool<CardData>.Get();
        unUsedCards.AddRange(cardBundle.Cards);
        unUsedCards.Remove(target);

        for (var cellIndex = 0; cellIndex < cardsCount; cellIndex++)
        {
            if (cellIndex == targetRandomIndex)
            {
                SpawnCard(target);
                continue;
            }

            var randomCard = unUsedCards.GetRandomElements();
            SpawnCard(randomCard);
            unUsedCards.Remove(randomCard);
        }
    }

    private void SpawnCard(CardData cardData)
    {
        _cardViewFactory.CreateCardView(cardData,_grid.transform);
    }

    public void Reset()
    {
        _cardViewFactory.Reset();
    }
}