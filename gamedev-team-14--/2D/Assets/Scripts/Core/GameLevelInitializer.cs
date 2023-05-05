using Assets.Scripts.Core.Services.Updater;
using Assets.Scripts.Player;
using Assets.Scripts.StatsSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class GameLevelInitializer : MonoBehaviour
{
	[SerializeField] private HeroKnight _heroKnight;
	[SerializeField] private GameUIInputView _gameUIInputView;
	

	private ExternalDevicesInputReader _externalDevicesInput;
	private PlayerBrain _playerBrain;
	private PlayerSystem _playerSystem;
	private ProjectUpdater _projectUpdater;

	private List<IDisposable> _disposables;

	private readonly bool _onPause = false;

	private void Awake()
	{
		_disposables = new List<IDisposable>();
		if(ProjectUpdater.Instance == null)
			_projectUpdater = new GameObject().AddComponent<ProjectUpdater>();
		else
			_projectUpdater = ProjectUpdater.Instance as ProjectUpdater;

		_externalDevicesInput = new ExternalDevicesInputReader();
		_playerBrain = new PlayerBrain(_heroKnight, new List<IEntityInputSource>
		{
			_gameUIInputView,
			_externalDevicesInput
		});

		_disposables.Add(_playerSystem);

	}

	private void Update()
	{
		if (_onPause)
			return;
		_externalDevicesInput.OnUpdate();
		if (Input.GetKeyUp(KeyCode.Escape))
			_projectUpdater.IsPaused = !_projectUpdater.IsPaused;
	}

	private void FixedUpdate()
	{
		if (_onPause)
			return;
		_playerBrain.OnFixedUpdate();
	}

}
