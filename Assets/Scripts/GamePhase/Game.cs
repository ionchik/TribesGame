using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using GamePhase.Professions;
using UnityEngine;
using Random = UnityEngine.Random;
using GamePhase.CardLogics.Cards;

public class Game : MonoBehaviour
{
    [SerializeField] private PauseBehaviour _pauseBehaviour;
    [SerializeField] private Prairie _prairie;
    [SerializeField] private Deck _deck;
    [SerializeField] private PlayerView[] _playerViews;
    [SerializeField] private HumanCardsBehaviour _humansBehaviour;

    [SerializeField] private TapArea _eldersTab;
    [SerializeField] private TapArea _shamansTab;
    [SerializeField] private TapArea _warriorsTab;
    [SerializeField] private TapArea _huntersTab;
    [SerializeField] private TapArea _fishermenTab;
    [SerializeField] private TapArea _farmersTab;
    [SerializeField] private TapArea _scoutsTab;
    
    public event Action<Player> TurnChanged;
    public event Action WinterStarted;
    public event Action<List<Tuple<Tribe, int>>> GameEnded;

	private List<Player> _players;
    private Player _firstPlayer;
    private GameState _currentState;
    private Player _currentPlayer;
    private int _currentPlayerIndex;

    private void Start()
    {
        _pauseBehaviour.StateChanged.AddListener(ChangeState);
        _prairie.CardsPicked.AddListener(OnCardsPicked);
        _deck.LastCardsLeft += WinterStart;
        
        _eldersTab.LongTapped.AddListener(() => ShowProfession(ProfessionType.Elder));
        _shamansTab.LongTapped.AddListener(() => ShowProfession(ProfessionType.Shaman));
        _warriorsTab.LongTapped.AddListener(() => ShowProfession(ProfessionType.Warrior));
        _huntersTab.LongTapped.AddListener(() => ShowProfession(ProfessionType.Hunter));
        _fishermenTab.LongTapped.AddListener(() => ShowProfession(ProfessionType.Fisherman));
        _farmersTab.LongTapped.AddListener(() => ShowProfession(ProfessionType.Farmer));
        _scoutsTab.LongTapped.AddListener(() => ShowProfession(ProfessionType.Scout));

        CreatePlayers();
    }

    private void OnDestroy()
    {
        _pauseBehaviour.StateChanged.RemoveListener(ChangeState);
        _prairie.CardsPicked.RemoveListener(OnCardsPicked);
        _deck.LastCardsLeft -= WinterStart;
    }

    private void ShowProfession(ProfessionType professionType)
    {
        _currentPlayer.ShowProfession(professionType);
    }

    private void ChangeTurn()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        _currentPlayer = _players[_currentPlayerIndex];
        TurnChanged?.Invoke(_currentPlayer);
    }

    private void ChangeState(GameState state)
    {
        _currentState = state;
    }

    private void CreatePlayers()
    {
		PlayerInfoTransfer playersTransfer = FindAnyObjectByType<PlayerInfoTransfer>();
        var playersInfo = playersTransfer.PlayersInfo;
		_players = new List<Player>();
        foreach (var (tribe, name) in playersInfo)
		{
			_players.Add(new Player(name, tribe, _playerViews[(int) tribe]));
		}

		_currentPlayerIndex = Random.Range(0, _players.Count);
		_currentPlayer = _players[_currentPlayerIndex];
        _firstPlayer = _currentPlayer;
		TurnChanged?.Invoke(_currentPlayer);
		Destroy(playersTransfer.gameObject);
	}

	public int GetWorkersNumber(CardType targetType) 
    {
        switch (targetType)
        {
            case CardType.Human:
				int elders = _currentPlayer.GetCounters()[ProfessionType.Elder].Item1;
				int warriors = _currentPlayer.GetCounters()[ProfessionType.Warrior].Item1;
                return elders > warriors ? elders : warriors;
            case CardType.Totem:
            case CardType.Rite:
                return _currentPlayer.GetCounters()[ProfessionType.Shaman].Item1;
			case CardType.Meat:
				return _currentPlayer.GetCounters()[ProfessionType.Hunter].Item1;
			case CardType.Fish:
				return _currentPlayer.GetCounters()[ProfessionType.Fisherman].Item1;
			case CardType.Wheat:
				return _currentPlayer.GetCounters()[ProfessionType.Farmer].Item1;
		}
        return 0;
    }

	private void OnCardsPicked(List<Card> cards)
    {
        StartCoroutine(TakeCard(cards));
    }

    private void WinterStart()
    {
        WinterStarted?.Invoke();
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitUntil(() => _currentPlayer == _firstPlayer);
        List<Tuple<Tribe, int>> winner = _players
            .Select(player => new Tuple<Tribe, int>(player.GetTribe(), player.GetPoints()))
            .OrderBy(t => t.Item2).ToList();
        GameEnded?.Invoke(winner);
    }

    private IEnumerator TakeCard(List<Card> cards)
    {
        switch (cards[0])
        {
            case Human:
                if(cards.Count > _currentPlayer.GetCounters()[ProfessionType.Elder].Item1)
                {
					_currentPlayer.TakeResources(cards);
					break;
				}
				if (cards.Count > _currentPlayer.GetCounters()[ProfessionType.Warrior].Item1)
				{
                    yield return _humansBehaviour.ChooseProfessions();
					_currentPlayer.AddWorkers(_humansBehaviour.GetWorkers());
					break;
				}
				_humansBehaviour.StartChoosing(cards);
                yield return new WaitUntil(_humansBehaviour.IsChosen);
                if(_humansBehaviour.IsKilling())
                {
                    _currentPlayer.TakeResources(cards);
                    break;
                }
                _currentPlayer.AddWorkers(_humansBehaviour.GetWorkers());
                break;
            default:
                _currentPlayer.TakeResources(cards);
                break;
        }
        ChangeTurn();
    }
}
