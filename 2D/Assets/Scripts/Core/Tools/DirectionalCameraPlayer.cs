using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DirectionalCameraPlayer
{
	[SerializeField] private CinemachineVirtualCamera _rightCamera;
	[SerializeField] private CinemachineVirtualCamera _leftCamera;

	private Dictionary<Direction, CinemachineVirtualCamera> _directionalCameras;


	public Dictionary<Direction, CinemachineVirtualCamera> DirectionalCameras
	{
		get
		{
			if (_directionalCameras != null)
				return _directionalCameras;

			_directionalCameras = new Dictionary<Direction, CinemachineVirtualCamera>
			{
			{ Direction.Left, _leftCamera},
			{ Direction.Right, _rightCamera}
			};
			return _directionalCameras;
		}
	}
}
