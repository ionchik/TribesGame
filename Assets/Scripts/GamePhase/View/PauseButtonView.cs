using UnityEngine;
using UnityEngine.UI;

public class PauseButtonView : MonoBehaviour
{
    [SerializeField] private Sprite _pauseSprite;
    [SerializeField] private Sprite _cancelSprite;

    private PauseButton _pauseButton;
    private Image _image;

    private void Awake()
    {
        _pauseButton = GetComponent<PauseButton>();
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _pauseButton.CancelClicked.AddListener(SetPauseIcon);
        _pauseButton.PauseClicked.AddListener(SetCancelIcon);
    }

    private void OnDestroy()
    {
        _pauseButton.CancelClicked.RemoveListener(SetPauseIcon);
        _pauseButton.PauseClicked.RemoveListener(SetCancelIcon);
    }

    private void SetPauseIcon()
    {
        _image.sprite = _pauseSprite;
    }

    private void SetCancelIcon()
    {
        _image.sprite = _cancelSprite;
    }
}
