using System.Collections.Generic;
using UnityEngine;

namespace GamePhase.Professions
{
    public interface IResourceful
    {
        public void AddResources(List<Card> resource, Transform area);
	}
}