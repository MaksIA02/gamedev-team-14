using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.StatsSystem.Enum;
using UnityEngine;
using Assets.Scripts.Core.Services.Updater;

namespace Assets.Scripts.StatsSystem
{
	public class StatsController : IDisposable, IStatValueGiver
	{
		private readonly List<Stat> _currentStats;
		private readonly List<StatModificator> _activeModificators;

		public StatsController(List<Stat> currentStats)
		{
			_currentStats = currentStats;
			_activeModificators = new List<StatModificator>();
			ProjectUpdater.Instance.UpdateCalled += OnUpdate;
		}

		

		public float GetStatValue(StatType statType) => 
			_currentStats.Find(stat => stat.Type == statType).Value;

		public void ProcessModificator(StatModificator statModificator)
		{
			var statToChange = _currentStats.Find(stat => stat.Type == statModificator.Stat.Type);
			if (statToChange == null)
				return;

			var addedValue = statModificator.StatModificatorType == StatModificatorType.Additive ? 
				statToChange + statModificator.Stat : statToChange * statModificator.Stat;

			statToChange.SetStatValue(statToChange + addedValue);
			if (statModificator.Duration < 0)
				return;

			if (_activeModificators.Contains(statModificator))
			{
				_activeModificators.Remove(statModificator);
			}
			else
			{
				var addedStat = new Stat(statModificator.Stat.Type, -addedValue);
				var tempModificator = new StatModificator(addedStat, StatModificatorType.Additive, statModificator.Duration, Time.time);
				_activeModificators.Add(tempModificator);
			}
		}

		private void OnUpdate()
		{
			if (_activeModificators.Count == 0)
				return;

			var expiredModificator = _activeModificators.Where(statModificator => statModificator.Duration >= Time.time);

			foreach (var statModificator in expiredModificator)
				ProcessModificator(statModificator);
		}
		public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
		
	}
}
