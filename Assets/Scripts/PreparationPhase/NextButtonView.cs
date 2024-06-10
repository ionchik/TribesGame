using UnityEngine;
using UnityEngine.UI;

public class NextButtonView : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private Text _buttonText;

    [SerializeField] private float _enableAlfaChannel;
    [SerializeField] private float _disableAlfaChannel;

    private void Start()
    {
        _inputField.onEndEdit.AddListener(RefreshButton);
    }

    private void OnDestroy()
    {
        _inputField.onEndEdit.RemoveListener(RefreshButton);
    }

    private void RefreshButton(string text)
    {
        float currentAlfa = text.Length > 2 ? _enableAlfaChannel : _disableAlfaChannel;

        Color textColor = _buttonText.color;
        textColor.a = currentAlfa;
        _buttonText.color = textColor;
    }
}
