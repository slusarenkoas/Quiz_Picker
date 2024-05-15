using UnityEngine;

[CreateAssetMenu(menuName = "Create LevelConfig", fileName = "LevelConfig", order = 0)]
public class LevelsConfig : ScriptableObject
{
    [field: SerializeField] public LevelData[] Levels { get; private set; }
}