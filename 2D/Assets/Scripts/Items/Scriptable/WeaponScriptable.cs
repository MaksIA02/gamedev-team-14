using Assets.Items.Data;
using Assets.Scripts.Items.Data;
using UnityEngine;

namespace Assets.Scripts.Items.Scriptable
{
	[CreateAssetMenu(fileName ="Weapon", menuName ="ItemsSystem/Weapon")]
	public class WeaponScriptable : BaseItemScriptable
	{
		[SerializeField] private WeaponDescriptor _weaponDescriptor;
		public override ItemDescriptor ItemDescriptor => _weaponDescriptor;
		
	}
}
