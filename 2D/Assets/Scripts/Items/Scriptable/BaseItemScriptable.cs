
using Assets.Items.Data;
using UnityEngine;

namespace Assets.Scripts.Items.Scriptable
{
	public abstract class BaseItemScriptable : ScriptableObject
	{
		public abstract ItemDescriptor ItemDescriptor { get; }
	}
}
