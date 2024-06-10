using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CardInfo : MonoBehaviour
{
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private TapArea _tapArea;

    public UnityEvent<string, string, Sprite> Shown;

    private Sprite _image;

    private void Awake()
    {
        Shown = new UnityEvent<string, string, Sprite>();
        Image image = GetComponent<Image>();
        _image = image.sprite;
    }

    private void Start()
    {
        _tapArea.LongTapped.AddListener(Show);
    }

    private void OnDestroy()
    {
        _tapArea.LongTapped.RemoveListener(Show);
    }

    private void Show()
    {
        Shown?.Invoke(_title, _description, _image);
    }
}
