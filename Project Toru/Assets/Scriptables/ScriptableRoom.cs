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
	public class ScriptableRoom : ScriptableObject
	{
		// Constants //
		private const int maxCharacters = 10;

		// Variables //
		public Vector3Int position;
		public RoomTheme theme;
		public int width = 1;
		public int height = 1;

		public new string name;
		public bool lightsOn = true;
		public List<Character> characters;
		short charactersPresent;
		public Dictionary<short, ScriptableFurniture> furniture;
		//public Domain.Room room;


		// public Functions //
		/// <summary>
		/// Constructor with custom size
		/// </summary>
		/// <param name="posX"></param>
		/// <param name="posY"></param>
		/// <param name="xSize"></param>
		/// <param name="ySize"></param>
		ScriptableRoom(Vector3Int pos, int xSize, int ySize, string roomTheme)
		{
			position = pos;
			width = xSize;
			height = ySize;
			furniture = new Dictionary<short, ScriptableFurniture>();
			characters = new List<Character>();
			//this.room = new Room("hallo", this);
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
		ScriptableRoom(Vector3Int pos, string background) : this(pos, 2, 1, background) { }

		/// <summary>
		/// adds a piece of furniture to the room
		/// </summary>
		/// <param name="furn"></param>
		/// <param name="loc"></param>
		void addFurniture(ScriptableFurniture furn, short loc)
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
