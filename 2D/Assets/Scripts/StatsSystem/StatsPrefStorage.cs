using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StatsSystem
{
	internal class StatsPrefStorage : MonoBehaviour
	{
		[field: SerializeField] public List<Stat> Stats { get; private set; }
	}
}
