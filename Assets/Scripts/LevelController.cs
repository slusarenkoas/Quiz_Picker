using System;
using MessagePipe;
using Messages;
using UnityEngine;

public class LevelController :  IDisposable
{
    public event Action AllLevelsCompleted;
    
    private readonly LevelSpawner _levelSpawner;
    private readonly LevelsProvider _levelsProvider;
    private readonly RandomTargetProvider _randomTargetProvider;
    private readonly IDisposable _subscription;
    private readonly IPublisher<TargetChosenMessage> _targetChosenPublisher;
    
    private CardBundleData _cardBundle;
    private int _index;
    private CardData _randomTarget;

    public LevelController(
        LevelSpawner levelSpawner,
        LevelsProvider levelsProvider,
        RandomTargetProvider randomTargetProvider, 
        ISubscriber<TargetCardFoundMessage> targetCardFoundSubscriber,
        IPublisher<TargetChosenMessage> targetChosenPublisher)
    {
        _levelSpawner = levelSpawner;
        _levelsProvider = levelsProvider;
        _randomTargetProvider = randomTargetProvider;
        
        _targetChosenPublisher = targetChosenPublisher;
        _subscription = targetCardFoundSubscriber.Subscribe(_ => StartNExtLevel());
    }
    
    private void StartNExtLevel()
    {
        StartLevel(++_index);
    }

    public void Initialize(CardBundleData cardBundle)
    {
        _cardBundle = cardBundle;
    }
    
    private void StartLevel(int index)
    {
        _levelSpawner.Reset();
        _index = index;

        if (!_levelsProvider.HasLevelWithIndex(index))
        {
            AllLevelsCompleted?.Invoke();
            return;
        }
        
        var level = _levelsProvider.GetLevel(index);
        _randomTarget = _randomTargetProvider.SelectNextTarget(_cardBundle);
        
        _levelSpawner.Spawn(_cardBundle,level,_randomTarget);
        
        _targetChosenPublisher.Publish(new TargetChosenMessage(_randomTarget.Identifier));
    }
    
    public void StartFirstLevel()
    {
        StartLevel(0);
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }
}