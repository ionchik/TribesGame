using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Prairie : MonoBehaviour
{
    [SerializeField] private Deck _deck;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Game _game;

    public UnityEvent<int> CardsChanged;
    public UnityEvent<string, string, Sprite> CardInfoShown;
    public UnityEvent<List<Card>> CardsPicked;

    private List<Card> _cards;
    private List<Card> _selectedCards;

    private void Awake()
    {
        CardsChanged = new UnityEvent<int>();
        CardInfoShown = new UnityEvent<string, string, Sprite>();
        CardsPicked = new UnityEvent<List<Card>>();
    }

    private void Start()
    {
        _cards = new List<Card>();
        _selectedCards = new List<Card>();
        _deck.Picked.AddListener(AddCard);
        _nextButton.onClick.AddListener(PickCards);
        _game.TurnChanged += Refresh;
    }

    private void OnDestroy()
    {
        _deck.Picked.RemoveListener(AddCard);
        _nextButton.onClick.RemoveListener(PickCards);
        _game.TurnChanged -= Refresh;
    }

    private void AddCard(Card card)
    {
        Card cardInstance = Instantiate(card, transform);
        cardInstance.Captured.AddListener(OnCardCaptured);
        _cards.Add(cardInstance);
        CardsChanged?.Invoke(_cards.Count);
        CardInfo cardInfo = cardInstance.gameObject.GetComponent<CardInfo>();
        cardInfo.Shown.AddListener(OnCardInfoShown);
    }

    private void OnCardInfoShown(string title, string description, Sprite image)
    {
        CardInfoShown?.Invoke(title, description, image);
    }

    private void OnCardCaptured(Card card)
    {
        if (card.IsSelected) 
        { 
            card.Deselect(); 
            _selectedCards.Remove(card);
            return;
        }
        if (_selectedCards.Count > 0 && _selectedCards.First().GetCardType() != card.GetCardType())
        {
            DeselectAllCards();
        }
        if (_game.GetWorkersNumber(card.GetCardType()) > _selectedCards.Count) 
        { 
            SelectCard(card); 
        }
    }

    private void DeselectAllCards()
    {
        foreach (Card card in _selectedCards)
        {
            card.Deselect();
        }
        _selectedCards.Clear();
    }

    private void Refresh(Player player)
    {
        foreach (Card card in _selectedCards)
        {
            _cards.Remove(card);
        }
        DeselectAllCards();
        CardsChanged?.Invoke(_cards.Count);
    }

    private void SelectCard(Card card)
    {
        card.Select();
        _selectedCards.Add(card);
    }

    private void PickCards()
    {
        if (_selectedCards.Count == 0) return;
        CardsPicked?.Invoke(_selectedCards);
    }
}
