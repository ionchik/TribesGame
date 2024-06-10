using UnityEngine;

public class Woman : Human
{
    [SerializeField] private Tribe _weddingLand;

    public override bool IsOwned(Player player)
    {
        return player.GetTribe() == _weddingLand || base.IsOwned(player);
    }
}
