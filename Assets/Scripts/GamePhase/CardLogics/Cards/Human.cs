using UnityEngine;

public abstract class Human : Card 
{
    [SerializeField] private Tribe _homeland;

    public virtual bool IsOwned(Player player)
    {
        return player.GetTribe() == _homeland;
    }

    public override int GetPoints(Player player)
    {
        return IsOwned(player) ? base.GetPoints(player) : -base.GetPoints(player);
    }
}
