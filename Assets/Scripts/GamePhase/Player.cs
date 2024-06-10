using System;
using System.Collections.Generic;
using System.Linq;
using GamePhase.Professions;

public class Player
{
	private readonly string _name;
	private readonly Tribe _tribe;
    private readonly PlayerView _view;
    
    private readonly ElderGroup _elders = new();
    private readonly ShamanGroup _shamans = new();
    private readonly WarriorGroup _warriors = new();
    private readonly HunterGroup _hunters = new();
    private readonly FishermanGroup _fishermen = new();
    private readonly FarmerGroup _farmers = new();
    private readonly ScoutGroup _scouts = new();
    
    public Player(string name, Tribe tribe, PlayerView view)
    {
        _name = name;
        _tribe = tribe;
        _view = view;
    }

    public int GetPoints()
    {
        int allPoints = 0;

        allPoints += _elders.Workers.Sum(worker => worker.GetPoints(this));
		allPoints += _shamans.Workers.Sum(worker => worker.GetPoints(this));
		allPoints += _warriors.Workers.Sum(worker => worker.GetPoints(this));
		allPoints += _hunters.Workers.Sum(worker => worker.GetPoints(this));
		allPoints += _fishermen.Workers.Sum(worker => worker.GetPoints(this));
		allPoints += _farmers.Workers.Sum(worker => worker.GetPoints(this));
		allPoints += _scouts.Workers.Sum(worker => worker.GetPoints(this));

		allPoints += _shamans.PickedCards.Sum(worker => worker.GetPoints(this));
		allPoints += _warriors.PickedCards.Count();
		allPoints += _hunters.PickedCards.Sum(worker => worker.GetPoints(this));
		allPoints += _fishermen.PickedCards.Sum(worker => worker.GetPoints(this));
		allPoints += _farmers.PickedCards.Sum(worker => worker.GetPoints(this));

		return allPoints;
    }

	public string GetName() => _name;
	public Tribe GetTribe() => _tribe;

    public void AddWorkers(List<Tuple<ProfessionType, Human>> workers)
    {
        foreach (Tuple<ProfessionType,Human> worker in workers)
        {
            switch (worker.Item1)
            {
                case ProfessionType.Elder:
                    _elders.AddWorker(worker.Item2, _view.EldersView.WorkersArea.transform);
                    break;
                case ProfessionType.Shaman:
                    _shamans.AddWorker(worker.Item2, _view.ShamansView.WorkersArea.transform);
                    break;
                case ProfessionType.Warrior:
                    _warriors.AddWorker(worker.Item2, _view.WarriorsView.WorkersArea.transform);
                    break;
                case ProfessionType.Hunter:
                    _hunters.AddWorker(worker.Item2, _view.HuntersView.WorkersArea.transform);
                    break;
                case ProfessionType.Fisherman:
                    _fishermen.AddWorker(worker.Item2, _view.FishermenView.WorkersArea.transform);
                    break;
                case ProfessionType.Farmer:
                    _farmers.AddWorker(worker.Item2, _view.FarmersView.WorkersArea.transform);
                    break;
                case ProfessionType.Scout:
                    _scouts.AddWorker(worker.Item2, _view.ScoutsView.WorkersArea.transform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void TakeResources(List<Card> resources)
    {
        switch (resources[0].GetProfessionType())
        {
            case ProfessionType.Shaman:
                _shamans.AddResources(resources, _view.ShamansView.ResourcesArea.transform);
                break;
            case ProfessionType.Warrior:
                _warriors.AddResources(resources, _view.WarriorsView.ResourcesArea.transform);
                break;
            case ProfessionType.Hunter:
                _hunters.AddResources(resources, _view.HuntersView.ResourcesArea.transform);
                break;
            case ProfessionType.Fisherman:
                _fishermen.AddResources(resources, _view.FishermenView.ResourcesArea.transform);
                break;
            case ProfessionType.Farmer:
                _farmers.AddResources(resources, _view.FarmersView.ResourcesArea.transform);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public Dictionary<ProfessionType, Tuple<int, int>> GetCounters()
    {
        return new Dictionary<ProfessionType,  Tuple<int, int>>() {
            { ProfessionType.Elder, _elders.GetCounters()},
            { ProfessionType.Shaman, _shamans.GetCounters()},
            { ProfessionType.Warrior, _warriors.GetCounters()},
            { ProfessionType.Hunter, _hunters.GetCounters()},
            { ProfessionType.Fisherman, _fishermen.GetCounters()},
            { ProfessionType.Farmer, _farmers.GetCounters()},
            { ProfessionType.Scout, _scouts.GetCounters()}
        };
    }

    public int GetTargetPoints(ProfessionType targetType)
    {
        switch (targetType)
		{
            case ProfessionType.Farmer: return _farmers.GetCounters().Item2;
            case ProfessionType.Scout: return _scouts.GetCounters().Item1;
            case ProfessionType.Warrior: return _warriors.GetCounters().Item1;
            case ProfessionType.Hunter: return _hunters.GetCounters().Item2;
            case ProfessionType.Fisherman: return _fishermen.GetCounters().Item2;
            case ProfessionType.Shaman: return _shamans.GetCounters().Item1;
            case ProfessionType.Elder: return _elders.GetCounters().Item1;
            default: throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null);
		}
    }
    
    public void ShowProfession(ProfessionType professionType)
    {
        switch (professionType)
        {
            case ProfessionType.Elder:
                _view.EldersView.gameObject.SetActive(true);
                break;
            case ProfessionType.Shaman:
                _view.ShamansView.gameObject.SetActive(true);
                break;
            case ProfessionType.Warrior:
                _view.WarriorsView.gameObject.SetActive(true);
                break;
            case ProfessionType.Hunter:
                _view.HuntersView.gameObject.SetActive(true);
                break;
            case ProfessionType.Fisherman:
                _view.FishermenView.gameObject.SetActive(true);
                break;
            case ProfessionType.Farmer:
                _view.FarmersView.gameObject.SetActive(true);
                break;
            case ProfessionType.Scout:
                _view.ScoutsView.gameObject.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(professionType), professionType, null);
        }
    }
}
