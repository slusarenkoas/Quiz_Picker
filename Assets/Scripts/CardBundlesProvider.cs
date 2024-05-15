using System.Collections.Generic;

public class CardBundlesProvider
{
    private readonly CardBundleData[] _cardBundles;

    public CardBundlesProvider(CardBundleData[] cardBundles)
    {
        _cardBundles = cardBundles;
    }

    public IEnumerable<CardBundleData> GetAllCardsBundles()
    {
        return _cardBundles;
    }
}