using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayersChooser : MonoBehaviour
{
    [SerializeField] private Slider _numberSlider;

    public UnityEvent<int> PlayersChanged;

    private int _playersNumber = 2;

    public int PlayersNumber => _playersNumber;

    private void Awake()
    {
        PlayersChanged = new UnityEvent<int>();
    }

    private void OnEnable()
    {
        _numberSlider.onValueChanged.AddListener(ChangePlayersNumber);
    }

    private void OnDisable()
    {
        _numberSlider.onValueChanged.RemoveListener(ChangePlayersNumber);
    }

    private void ChangePlayersNumber(float value)
    {
        _playersNumber = (int) value;
        PlayersChanged?.Invoke(_playersNumber);
    }
}
