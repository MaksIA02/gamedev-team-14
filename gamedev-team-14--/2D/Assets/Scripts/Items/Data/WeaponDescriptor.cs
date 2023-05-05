
using Assets.Items.Data;
using Assets.Items.Enum;
using Assets.Scripts.Items.Enum;
using Assets.Scripts.StatsSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items.Data
{
	public class WeaponDescriptor : StatChangingItemDescriptor
	{
		[field: SerializeField] public WeaponType WeaponType { get; private set; }

		public WeaponDescriptor(WeaponType weaponType, ItemId itemId, ItemType type, Sprite itemSprite, ItemRarity itemRarity, float price, float level, List<StatModificator> stats) :
			base(itemId, type, itemSprite, itemRarity, price, level, stats)
		{
			WeaponType = weaponType;
		}
	}
}
