using Assets.Items.Enum;
using Assets.Scripts.StatsSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Items.Data
{
	[Serializable]
	public class StatChangingItemDescriptor : ItemDescriptor
	{
		[field: SerializeField] public float Level { get; private set; }
		[field: SerializeField] public List<StatModificator> Stats { get; private set; }

		public StatChangingItemDescriptor(ItemId itemId, ItemType type, Sprite itemSprite, ItemRarity itemRarity, float price, float level, List<StatModificator> stats) :
			base(itemId, type, itemSprite, itemRarity, price)
		{
			Level = level;
			Stats = stats;
		}
	}
}
