using System;
using System.Linq;
using UnityEngine.Pool;

public interface ITargetProvider
{
    event Action<CardData> TargetChanged;
    CardData CurrentTarget { get; }
}

public class RandomTargetProvider : ITargetProvider
{
    public event Action<CardData> TargetChanged;
    public CardData CurrentTarget { get; private set; }

    private readonly UsedTargetsController _usedTargetsController;

    public RandomTargetProvider(UsedTargetsController usedTargetsController)
    {
        _usedTargetsController = usedTargetsController;
    }

    public CardData SelectNextTarget(CardBundleData cardBundle)
    {
        var unUsedTargets = ListPool<CardData>.Get();

        unUsedTargets.AddRange(cardBundle.Cards
            .Where(card => !_usedTargetsController.IsUsed(card)));

        CurrentTarget = unUsedTargets.GetRandomElements();
        TargetChanged?.Invoke(CurrentTarget);
        
        _usedTargetsController.MarkAsUsed(CurrentTarget);
        ListPool<CardData>.Release(unUsedTargets);

        return CurrentTarget;
    }
}