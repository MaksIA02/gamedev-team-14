
using Assets.Scripts.StatsSystem.Enum;

namespace Assets.Scripts.StatsSystem
{
	public interface IStatValueGiver
	{
		float GetStatValue(StatType statType);
	}
}
