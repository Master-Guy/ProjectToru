using Assets.Domain;
using System.Collections.Generic;
using UnityEngine;
using Option = Assets.Domain.Option;

namespace Assets.Scriptables
{
	[CreateAssetMenu(fileName = "new Furniture", menuName = "Furniture/Furniture")]
	public class Furniture : Drawable
	{
		public new string name;
		public Sprite sprite;
		public List<Option> options;
		private List<Item> items;   //items implementeren
		public int sizeX;
		public int sizeY;
		public Domain.FurnitureBehaviour furniture;

		public Furniture(string sprite) : base(sprite)
		{
			this.furniture = new Domain.FurnitureBehaviour(this);
		}

		public List<Option> getOptions()
		{
			return this.options;
		}

		public List<Item> GetItems()
		{
			return this.items;
		}

		public override void Draw()
		{
			throw new System.NotImplementedException();
		}
	}

}