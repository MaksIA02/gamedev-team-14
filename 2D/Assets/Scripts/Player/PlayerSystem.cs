
using Assets.Scripts.StatsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
	public class PlayerSystem : IDisposable
	{
		private readonly StatsController _statsController;
		private readonly HeroKnight _heroKnight;
		private readonly PlayerBrain _playerBrain;
		private readonly List<IDisposable> _disposables;


		public PlayerSystem(HeroKnight heroKnight, List<IEntityInputSource> inputSources)
		{
			_disposables = new List<IDisposable>();

			var statStorage = Resources.Load<StatsStorage>($"Player/{nameof(StatsStorage)}");
			var stats = statStorage.Stats.Select(stat => stat.GetCopy()).ToList();
			_statsController = new StatsController(statStorage.Stats);
			_disposables.Add(_statsController);


			_heroKnight = heroKnight;
			_heroKnight.Initialize(_statsController);


			_playerBrain = new PlayerBrain(_heroKnight, inputSources);
			_disposables.Add(_playerBrain);
		}

		public void Dispose()
		{
			foreach (var disposable in _disposables)
				disposable.Dispose();
		}

	}
}
