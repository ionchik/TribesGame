using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TapArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _longTapTime;

    public UnityEvent Tapped;
    public UnityEvent LongTapped;

    private IEnumerator _timer;
    private Vector2 _tapPosition;
    private bool _isLongTap;

    private void Awake()
    {
        Tapped = new UnityEvent();
        LongTapped = new UnityEvent();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _timer = Timer();
        _isLongTap = false;
        StartCoroutine(_timer);
        _tapPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(_timer);
        if (_tapPosition != eventData.position) return;
        if (_isLongTap == false) Tapped?.Invoke();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(_longTapTime);
        _isLongTap = true;
        LongTapped?.Invoke();
    }
}
