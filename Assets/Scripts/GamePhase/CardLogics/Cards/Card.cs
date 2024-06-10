using GamePhase.CardLogics.Cards;
using GamePhase.Professions;
using UnityEngine;
using UnityEngine.Events;

public abstract class Card : MonoBehaviour
{
    [SerializeField] private int _points;
    [SerializeField] private ProfessionType _professionType;
    [SerializeField] private CardType _cardType;

    public UnityEvent<Card> Captured;
    public UnityEvent Selected;
    public UnityEvent Deselected;

    private TapArea _tapArea;
    private bool _isSelected;

    public bool IsSelected => _isSelected;
    public ProfessionType GetProfessionType() => _professionType;
    public CardType GetCardType() => _cardType;

    private void Awake()
    {
        Captured = new UnityEvent<Card>();
        Selected = new UnityEvent();
        Deselected = new UnityEvent();
    }

    private void Start()
    {
        _tapArea = GetComponent<TapArea>();
        _tapArea.Tapped.AddListener(Capture);
    }

    private void OnEnable()
    {
        _tapArea?.Tapped.AddListener(Capture);
    }

    private void OnDisable()
    {
        _tapArea.Tapped.RemoveListener(Capture);
    }

    public void Select()
    {
        _isSelected = true;
        Selected?.Invoke();
    }

    public void Deselect()
    {
        _isSelected = false;
        Deselected?.Invoke();
    }
    
    public virtual int GetPoints(Player player)
    {
        return _points;
    }

    private void Capture()
    {
        Captured?.Invoke(this);
    }
}
