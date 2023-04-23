using Assets.Scripts.Core.Services.Updater;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBrain : IDisposable
{
	private readonly HeroKnight _heroKnight;
	private readonly DirectionalMover _directionalMover;
	private readonly List<IEntityInputSource> _inputSources;

   public PlayerBrain(HeroKnight heroKnight, List<IEntityInputSource> inputSources)
	{
		_heroKnight = heroKnight;
		_inputSources = inputSources;
		ProjectUpdater.Instance.FixedUpdateCalled += OnFixedUpdate;
	}

	public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnFixedUpdate;

	public void OnFixedUpdate()
	{
		_heroKnight.PlayerMovement(GetHorizontalDirection());
		if (IsJump)
			_heroKnight.Jump();

		if(IsAttack)
			_heroKnight.Attack();

		foreach (var inputSource in _inputSources)
			inputSource.ResetOneTimeAction();
	}

	private float GetHorizontalDirection()
	{
		foreach (var inputSource in _inputSources)
		{
			if (inputSource.HorizontalDirection == 0)
				continue;

			return inputSource.HorizontalDirection;
		}
		return 0;
	}
	private float GetVerticalDirection()
	{
		foreach (var inputSource in _inputSources)
		{
			if (inputSource.VerticalDirection == 0)
				continue;

			return inputSource.VerticalDirection;
		}
		return 0;
	}

	private bool IsJump => _inputSources.Any(source => source.Jump);
	private bool IsAttack => _inputSources.Any(source => source.Attack);
}
