using UnityEngine;
using UnityEngine.UI;

public class PlayersNumberView : MonoBehaviour
{
    [SerializeField] private PlayersChooser _chooser;
    [SerializeField] private Text _playersNumber;

    private void Start()
    {
        _chooser.PlayersChanged.AddListener(Refresh);
    }

    private void OnDestroy()
    {
        _chooser.PlayersChanged.RemoveListener(Refresh);
    }

    private void Refresh(int value)
    {
        _playersNumber.text = value.ToString();
    }
}
