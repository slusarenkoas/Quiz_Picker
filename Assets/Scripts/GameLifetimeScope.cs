using MessagePipe;
using Messages;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameObjectGrid _grid;
    [SerializeField] private CardView _prefab;
    [SerializeField] private CardBundleData[] _cardBundles;
    [SerializeField] private LevelsConfig _levelsConfig;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_grid);
        builder.RegisterInstance(_prefab);
        builder.RegisterInstance(_cardBundles);
        builder.RegisterInstance(_levelsConfig);

        builder.Register<LevelController>(Lifetime.Singleton);
        builder.Register<CardViewFactory>(Lifetime.Singleton);
        builder.Register<CardBundlesProvider>(Lifetime.Singleton);
        builder.Register<LevelsProvider>(Lifetime.Singleton);
        builder.Register<LevelSpawner>(Lifetime.Singleton);
        builder.Register<RandomTargetProvider>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf();
        builder.Register<UsedTargetsController>(Lifetime.Singleton);

        RegisterMessagePipe(builder);
        
        builder.RegisterEntryPoint<GameFlowController>();
    }

    private void RegisterMessagePipe(IContainerBuilder builder)
    {
        var option = builder.RegisterMessagePipe();
        builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

        builder.RegisterMessageBroker<TargetCardFoundMessage>(option);
        builder.RegisterMessageBroker<TargetChosenMessage>(option);
    }
}
