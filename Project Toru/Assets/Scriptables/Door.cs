using UnityEngine;

namespace Assets.Scriptables { 

	[CreateAssetMenu(fileName = "new Furniture", menuName = "Furniture/Door")]
	class Door : ScriptableFurniture
	{
		public ScriptableRoom source;
		public ScriptableRoom Destination;
		public Locklevel open;
		public Door(Sprite sprite) : base(sprite)
		{
		}

		public void move()
		{

		}
	}
	enum Locklevel
	{
		opened,
		unlocked,
		locked
	}

}
