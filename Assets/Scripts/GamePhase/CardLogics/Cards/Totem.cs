using GamePhase.Professions;
using UnityEngine;
using GamePhase.CardLogics.Cards;

public class Totem : ShamanCard 
{
	[SerializeField] private ProfessionType _targetProfession;

	public override int GetPoints(Player player)
	{
		return base.GetPoints(player) * player.GetTargetPoints(_targetProfession);
	}
}
