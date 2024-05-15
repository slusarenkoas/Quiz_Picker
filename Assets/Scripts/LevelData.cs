using System;
using UnityEngine;

[Serializable]
public class LevelData
{
    [field:SerializeField] public int Rows {get; private set;}
    [field:SerializeField] public int Columns {get; private set;}
}