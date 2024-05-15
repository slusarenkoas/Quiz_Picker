using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Identifier { get; private set; }
}