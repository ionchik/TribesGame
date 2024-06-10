using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseButton : MonoBehaviour
{
    public UnityEvent PauseClicked;
    public UnityEvent CancelClicked;

    private UnityEvent _currentStateClicked;
    private Button _buttonBase;

    private void Awake()
    {
        PauseClicked = new UnityEvent();
        CancelClicked = new UnityEvent();
        _currentStateClicked = PauseClicked;
        _buttonBase = GetComponent<Button>();
        _buttonBase.onClick.AddListener(OnStateAct);
    }

    private void OnDestroy()
    {
        _buttonBase.onClick.RemoveListener(OnStateAct);
    }

    private void OnStateAct()
    {
        _currentStateClicked?.Invoke();
        _currentStateClicked = _currentStateClicked == PauseClicked ? CancelClicked : PauseClicked;
    }
}
