using UnityEngine;
using UnityEngine.UI;

public class InfoScreen : MonoBehaviour
{
    [SerializeField] private GameObject _infoScreen;
    [SerializeField] private Prairie _prairie;
    [SerializeField] private Text _title;
    [SerializeField] private Text _description;
    [SerializeField] private Image _image;

    private void Start()
    {
        _prairie.CardInfoShown.AddListener(OnShown);
    }

    private void OnDestroy()
    {
        _prairie.CardInfoShown.RemoveListener(OnShown);
    }

    private void OnShown(string title, string description, Sprite image)
    {
        _infoScreen.SetActive(true);
        _title.text = title;
        _description.text = description;
        _image.sprite = image;
    }
}
