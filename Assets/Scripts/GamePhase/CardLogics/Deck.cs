using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Random = UnityEngine.Random;
using GamePhase.Professions;

public class Deck : MonoBehaviour
{
    [Serializable]
    private struct CardCapacity
    {
        [SerializeField] private Card _card;
        [SerializeField] private int _quantity;

        public Card GetCard() => _card;
        public int GetQuantity() => _quantity;
    }

    [SerializeField] private Game _game;
    [SerializeField] private int _startNumber;
    [SerializeField] private float _pickDeltaTime;
    [SerializeField] private Button _pickButton;
    [SerializeField] private CardCapacity[] _capacity;

    public event Action LastCardsLeft;
    public UnityEvent<Card> Picked;

    private Queue<Card> _cards;
    private int _cardsToPick;

    private void Awake()
    {
        Picked = new UnityEvent<Card>();
    }

    private void Start()
    {
        List<Card> cards = CreateCards();
        _cards = Shuffle(cards);
        StartCoroutine(StartPick());
    }

    private void OnEnable()
    {
        _pickButton.onClick.AddListener(Pick);
        _game.TurnChanged += OnTurnChanged;
    }

    private void OnDisable()
    {
        _pickButton.onClick.RemoveListener(Pick);
        _game.TurnChanged -= OnTurnChanged;
    }

    private void OnTurnChanged(Player player)
    {
        _cardsToPick = player.GetCounters()[ProfessionType.Scout].Item1;
        Pick();
    }

    private void Pick()
    {
        if (_cardsToPick == 0) return;
        if (_cards.Count == 0) return;
        Card pickedCard = _cards.Dequeue();
        Picked?.Invoke(pickedCard);
        if (_cards.Count == 10) LastCardsLeft?.Invoke();
        _cardsToPick--;
	}

    private List<Card> CreateCards()
    {
        List<Card> cards = new List<Card>();
        foreach (CardCapacity cardCapacity in _capacity) 
        {
            Card cardType = cardCapacity.GetCard();
            int quantity = cardCapacity.GetQuantity();
            List<Card> oneTypeCards = Enumerable.Repeat(cardType, quantity).ToList();
            cards.AddRange(oneTypeCards);
        }
        return cards;
    }

    private Queue<Card> Shuffle(List<Card> cards)
    {
        for (int index = 0; index < cards.Count; index++)
        {
            Card temp = cards[index];
            int randomIndex = Random.Range(index, cards.Count);
            cards[index] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
        return new Queue<Card>(cards);
    }

    private IEnumerator StartPick()
    {
        WaitForSecondsRealtime timer = new WaitForSecondsRealtime(_pickDeltaTime);
		yield return timer;
		_cardsToPick = _startNumber;
        for (int index = 0; index < _startNumber; index++)
        {
            yield return timer;
            Pick();
        }
    }
}
