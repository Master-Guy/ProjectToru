﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Domain
{
	abstract class Drawable : MonoBehaviour
	{
		bool uncovered = false;
		Sprite sprite;

		public Drawable(string sprite)
		{
			// Todo sprite = stringToSprite;
		}

		/// <summary>
		/// uncovers this
		/// </summary>
		void uncover()
		{
			uncovered = true;
		}

		public abstract void Draw();
	}
}
