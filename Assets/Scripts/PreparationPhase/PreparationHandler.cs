using UnityEngine;

public class PreparationHandler : MonoBehaviour
{
    [SerializeField] private PlayersChooser _chooser;

    private int _playersNumber;

    private void Start()
    {
        _chooser.PlayersChanged.AddListener(SetPlayersNumber);
    }

    private void OnDestroy()
    {
        _chooser.PlayersChanged.RemoveListener(SetPlayersNumber);
    }

    private void SetPlayersNumber(int value)
    {
        _playersNumber = value;
    }

}
