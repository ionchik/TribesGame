using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePhase.Professions
{
    public abstract class StoringProfessionGroup<TCard> : ProfessionGroup, IResourceful where TCard: Card
    {
        private List<TCard> _pickedCards = new();

	public List<TCard> PickedCards => _pickedCards;

	public override Tuple<int, int> GetCounters()
        {
            return new Tuple<int, int>(base.GetCounters().Item1, _pickedCards.Count);
        }
        
        public void AddResources(List<Card> cards, Transform area)
        {
            foreach (Card card in cards)
            {
                TCard resource = (TCard)card;
                resource.transform.SetParent(area);
                _pickedCards.Add(resource);
            }
        }
    }
}
