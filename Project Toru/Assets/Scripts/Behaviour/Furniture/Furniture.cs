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
		List<Option> Options;

		[SerializeField]
		bool Passable;

		[SerializeField]
		string Description = string.Empty;

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
			OWindow.AddOption(Description, Options.Where(x => x.Prerequisite == null ||
					((Character)collision.gameObject.GetComponent(typeof(Character))).skills.Contains(x.Prerequisite.Value)).ToList());
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
