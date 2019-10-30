using Assets.Scripts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Behaviour
{
	public class Furniture : MonoBehaviour, IPointerClickHandler
	{
		const string OptionWindowName = "OptionDialogue";

		[SerializeField]
		IDictionary<Option, Delegate> Options;

		[SerializeField]
		bool Passable;

		Optionwindow OWindow;
		Room Parent;

		void Start()
		{
			OWindow = GameObject.Find(OptionWindowName).GetComponent<Optionwindow>();
		}

		void Update()
		{

		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			// DO NOT touch this lambda function, it's very ugly, but the best way to do it
			OWindow.AddOption(Options.Where(x => x.Key.Prerequisite == null ||
					((Character)collision.gameObject.GetComponent(typeof(Character))).skills.Contains(x.Key.Prerequisite.Value)).ToDictionary(x => x.Key, x => x.Value));
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				// TODO move character here
			}
		}
	}
}
