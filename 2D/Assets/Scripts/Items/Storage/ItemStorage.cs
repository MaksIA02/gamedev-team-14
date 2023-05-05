using Assets.Scripts.Items.Scriptable;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Items.Storage
{
	[CreateAssetMenu(fileName ="ItemsStorage", menuName ="ItemsSystem/ItemsStorage")]
	public class ItemStorage : ScriptableObject
	{
		[field: SerializeField] public List<BaseItemScriptable> ItemScriptables {  get; private set; }
	}
}
