using System.Collections.Generic;
using UnityEngine;
using Option = Assets.Domain.Option;

namespace Assets.Scriptables
{
	[CreateAssetMenu(fileName = "new Furniture", menuName = "Furniture/Furniture")]
	class Furniture : ScriptableObject
	{
		public new string name;
		public Sprite sprite;
		public List<Option> options;
		//private List<> items { get; set; }       //items implementeren
		public int sizeX;
		public int sizeY;

		public Furniture(Sprite sprite)
		{
			this.sprite = sprite;
		}

		public List<Option> getOptions()
		{
			return this.options;
		}

	}

}