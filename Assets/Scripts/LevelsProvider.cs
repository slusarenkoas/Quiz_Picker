public class LevelsProvider
{
    private readonly LevelsConfig _levelsConfig;

    public LevelsProvider(LevelsConfig levelsConfig)
    {
        _levelsConfig = levelsConfig;
    }

    public LevelData GetLevel(int index)
    {
        return _levelsConfig.Levels[index];
    }

    public bool HasLevelWithIndex(int index)
    {
        return index >= 0 && index < _levelsConfig.Levels.Length;
    }
}