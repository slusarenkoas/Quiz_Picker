using UnityEngine;

[CreateAssetMenu(menuName = "Create CardBundleData", fileName = "CardBundleData", order = 0)]
public class CardBundleData : ScriptableObject
{
    [field:SerializeField] public CardData[] Cards { get; private set; }
}