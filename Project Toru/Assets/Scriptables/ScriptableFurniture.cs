using System.Collections.Generic;
using UnityEngine;
using Option = Assets.Domain.Option;

namespace Assets.Scriptables
{
	[CreateAssetMenu(fileName = "new Furniture", menuName = "Furniture/Furniture")]
	public class ScriptableFurniture : ScriptableObject
	{
		public new string name;
		public Sprite sprite;
		public List<Option> options;
		private List<Item> items;   //items implementeren
		public int sizeX;
		public int sizeY;
		public Domain.Furniture furniture;

		public ScriptableFurniture(Sprite sprite)
		{
			this.sprite = sprite;
			this.furniture = new Domain.Furniture("hallo", this);
		}

		public List<Option> getOptions()
		{
			return this.options;
		}

		public List<Item> GetItems()
		{
			return this.items;
		}

	}

}