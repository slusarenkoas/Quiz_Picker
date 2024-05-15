using TMPro;
using UnityEngine;
using VContainer;

public class TargetView : MonoBehaviour
{
    [SerializeField] private string _textTemplate = "Find '{0}'";
    [SerializeField] private TMP_Text _label;
    private ITargetProvider _targetProvider;

    [Inject]
    private void Construct(ITargetProvider targetProvider)
    {
        _targetProvider = targetProvider;
        _targetProvider.TargetChanged += OnTargetChanged;
        OnTargetChanged(_targetProvider.CurrentTarget);
    }

    private void OnDestroy()
    {
        _targetProvider.TargetChanged -= OnTargetChanged;
    }

    private void OnTargetChanged(CardData currentTarget)
    {
        _label.text = string.Format(_textTemplate, currentTarget?.Identifier);
    }
}