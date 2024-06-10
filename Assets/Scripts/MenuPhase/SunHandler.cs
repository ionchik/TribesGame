using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SunHandler : MonoBehaviour
{
    [SerializeField] private Button _sunButton;
    [SerializeField] private Image _sunImage;
    [SerializeField] private float _deltaTime;
    [SerializeField] private Color[] _colors;

    private Color _currentColor;
    private int _currentIndex;

    private void Start()
    {
        _currentIndex = 0;
        _currentColor = _colors[_currentIndex];
        _sunButton.onClick.AddListener(ChangeColor);
    }

    private void OnDestroy()
    {
        _sunButton.onClick.RemoveListener(ChangeColor);
    }

    private void ChangeColor()
    {
        _sunImage.color = _colors[_currentIndex];
        _currentIndex = (_currentIndex + 1) % _colors.Length;
        StopAllCoroutines();
        StartCoroutine(ColorSwipe(_colors[_currentIndex]));
    }

    private IEnumerator ColorSwipe(Color targetColor)
    {
        WaitForSeconds timer = new WaitForSeconds(_deltaTime);
        while (_currentColor != targetColor)
        {
            _currentColor = Color.Lerp(_currentColor, targetColor, Time.deltaTime);
            _sunImage.color = _currentColor;
            yield return timer;
        }
    }
}
