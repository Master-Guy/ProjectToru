using System;
using Assets.Domain;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scriptables
{
	[CreateAssetMenu(fileName = "new Room", menuName = "Room")]
	/// <summary>
	/// This class provides the layout for a room
	/// </summary>
	/// <remarks>
	/// This class is at the heart of the levels, it has a position, background and furniture
	/// a room also has a variable denoting if it has been uncovered,
	/// in game it will also store which characters are currently present.
	/// </remarks>
	class Room : Drawable
	{
		// Constants //
		private const int maxCharacters = 10;

		// Variables //
		private int positionX;
		private int positionY;
		private int sizeX = 1;
		private int sizeY = 1;
		private string name;
		private bool lightsOn = true;
		private List<Character> characters;
		short charactersPresent;
		Dictionary<short, Furniture> furniture;


		// public Functions //
		/// <summary>
		/// Constructor with custom size
		/// </summary>
		/// <param name="posX"></param>
		/// <param name="posY"></param>
		/// <param name="xSize"></param>
		/// <param name="ySize"></param>
		/// <param name="background"></param>
		Room(int posX, int posY, int xSize, int ySize, string background) : base(background)
		{
			sizeX = xSize;		sizeY = ySize;
			positionX = posX; positionY = posY;
			furniture = new Dictionary<short, Furniture>();
			characters = new List<Character>();
		}
		/// <summary>
		/// Constructor with standard size
		/// </summary>
		/// <remarks>
		/// This function simply calls the custom constructor with 1 as size
		/// </remarks>
		/// <param name="posX"></param>
		/// <param name="posY"></param>
		/// <param name="background"></param>
		Room(int posX, int posY, string background) : this(posX, posY, 2, 1, background) { }
		
		/// <summary>
		/// draws the room on screen
		/// </summary>
		public override void Draw()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// adds a piece of furniture to the room
		/// </summary>
		/// <param name="furn"></param>
		/// <param name="loc"></param>
		void addFurniture(Furniture furn, short loc)
		{
			furniture.Add(loc, furn);
		}

		/// <summary>
		/// if the room isn't overcrowded adds a character to the room
		/// </summary>
		/// <param name=""></param>
		/// <param name=""></param>
		/// <returns></returns>
		int addCharacter(Character character)
		{
			if (charactersPresent < maxCharacters)
			{
				characters.Add(character);
				++charactersPresent;
				return 0;
			}
			return -1;
		}

		/// <summary>
		/// removes a character from the room if present
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		int removeCharacter(Character character)
		{
			if (characters.Remove(character))
				return 0;
			return -1;
		}
	}

	enum RoomErrors
	{
		eDrawError,
		eRoomFull,
		eCharacterNotFound
	}
}
