using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ProfessionGroup
{
    private List<Human> _workers = new();

    public List<Human> Workers => _workers;

	public void AddWorker(Human worker, Transform area)
    {
        worker.transform.SetParent(area);
        _workers.Add(worker);
    }
    
    public virtual Tuple<int, int> GetCounters()
    {
        return new Tuple<int, int>(_workers.Count + 1, 0);
    }
}
