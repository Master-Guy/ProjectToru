using System;
using Assets.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Application
{
	/// <summary>
	/// This class provides the layout for a room
	/// </summary>
	/// <remarks>
	/// This class is at the heart of the levels, it has a position, background and furniture
	/// a room also has a variable denoting if it has been uncovered,
	/// in game it will also store which characters are currently present.
	/// </remarks>
	class Room
	{
		// Constants //
		private const int maxCharacters = 10;

		// Variables //
		private int positionX;
		private int positionY;
		private int sizeX = 1;
		private int sizeY = 1;
		private string name;
		private string bg;
		private bool lightsOn = true;
		private bool uncovered = false;
		private Character[] characters;
		Dictionary<short, Furniture> objects;
		Dictionary<Furniture, Dictionary<string, Delegate>> options;


		// public Functions //
		/// <summary>
		/// Constructor with custom size
		/// </summary>
		/// <param name="posX"></param>
		/// <param name="posY"></param>
		/// <param name="xSize"></param>
		/// <param name="ySize"></param>
		/// <param name="background"></param>
		Room(int posX, int posY, int xSize, int ySize, string background)
		{
			sizeX = xSize;		sizeY = ySize;
			positionX = posX; positionY = posY;
			bg = background;
			objects = new Dictionary<short, Furniture>();
			options = new Dictionary<Furniture, Dictionary<string, Delegate>>();
			characters = new Character[maxCharacters * sizeX];
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
		Room(int posX, int posY, string background) : this(1, 1, posX, posY, background) { }


		/// <summary>
		/// draws the room on screen
		/// </summary>
		/// <returns>
		/// returns 0 if succesfull, otherwise error code
		/// </returns>
		int draw()
		{
			// TODO
			return 0;
		}

		/// <summary>
		/// returns all the options in the room
		/// </summary>
		/// <returns></returns>
		Dictionary<string, Delegate> getOptions()
		{
			Dictionary<string, Delegate> retOptions = new Dictionary<string, Delegate>();
			if (options.Count == 0)
			{
				buildOptions();
			}
			foreach (KeyValuePair<Furniture, Dictionary<string, Delegate>> entry in options)
				retOptions.Concat(entry.Value);
			return retOptions;
		}


		// Private functions //
		/// <summary>
		/// fills the options variable with 
		/// </summary>
		private void buildOptions()
		{
				foreach (KeyValuePair<short, Domain.FurnitureInterfaceOld> entry in objects)
				{
					options.Add(entry.Value, entry.Value.getOptions());
					// options.Concat(entry.Value.getOptions(characters));
				}

		}
	}

	enum RoomErrors
	{
		drawError
	}
}
