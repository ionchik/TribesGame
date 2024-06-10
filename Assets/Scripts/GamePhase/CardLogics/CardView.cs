using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Card _card;
    [SerializeField] private Image _image;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _deselectedColor;
    private void Start()
    {
        _card.Selected.AddListener(OnSelected);
        _card.Deselected.AddListener(OnDeselected);
    }

    private void OnDestroy()
    {
        _card.Selected.RemoveListener(OnSelected);
        _card.Deselected.RemoveListener(OnDeselected);
    }

    private void OnSelected()
    {
        _image.color = _selectedColor;
    }

    private void OnDeselected()
    {
        _image.color = _deselectedColor;
    }
}
