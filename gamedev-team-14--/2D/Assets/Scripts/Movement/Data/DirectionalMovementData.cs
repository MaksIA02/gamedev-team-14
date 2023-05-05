using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DirectionalMovementData
{
	[field: SerializeField] public float Speed { get; set; }
	[field: SerializeField] public Direction Direction { get; set; }
}
