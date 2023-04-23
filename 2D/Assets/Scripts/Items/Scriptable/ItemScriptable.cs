using Assets.Items.Data;
using UnityEngine;

namespace Assets.Scripts.Items.Scriptable
{
	[CreateAssetMenu(fileName ="Item", menuName ="ItemsSystem/Item")]
	public class ItemScriptable : BaseItemScriptable
	{
		[SerializeField] private StatChangingItemDescriptor _itemdescriptor;
		public override ItemDescriptor ItemDescriptor => _itemdescriptor;

		public ItemScriptable(StatChangingItemDescriptor descriptor) 
		{ 
			_itemdescriptor = descriptor;
		}
	}
}
