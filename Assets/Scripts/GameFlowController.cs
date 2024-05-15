using System;
using System.Linq;
using VContainer.Unity;

public class GameFlowController : IStartable, IDisposable
{
    private readonly LevelController _levelController;
    private readonly CardBundlesProvider _cardBundlesProvider;
    private readonly UsedTargetsController _usedTargetsController;


    public GameFlowController(
        LevelController levelController,
        CardBundlesProvider cardBundlesProvider,
        UsedTargetsController usedTargetsController)
    {
        _levelController = levelController;
        _cardBundlesProvider = cardBundlesProvider;
        _usedTargetsController = usedTargetsController;

        _levelController.AllLevelsCompleted += OnAllLevelsCompleted;
    }
    
    public void Dispose()
    {
        _levelController.AllLevelsCompleted -= OnAllLevelsCompleted;
    }
    
    private void OnAllLevelsCompleted()
    {
        SelectRandomBundleAndStartLevel();
    }

    public void Start()
    {
        SelectRandomBundleAndStartLevel();
    }

    private void SelectRandomBundleAndStartLevel()
    {
        var randomValidBundle = _cardBundlesProvider.GetAllCardsBundles()
            .Where(bundle => _usedTargetsController.HasUnusedTarget(bundle))
            .ToList()
            .GetRandomElements();
        
        _levelController.Initialize(randomValidBundle);
        _levelController.StartFirstLevel();
    }
}