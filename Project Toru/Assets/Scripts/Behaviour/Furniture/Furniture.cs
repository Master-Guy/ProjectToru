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

		EventWindow OWindow;
		Room Parent;


		void Start()
		{
			Options = new List<Option>()
			{
				new getInformation()
			};
			OWindow = GameObject.Find(OptionWindowName).GetComponent<EventWindow>();
		}

		void Update()
		{

		}

		void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player") && collision.isTrigger)	// check if character has a destination, if so check if it is this
				OWindow.AddEvent(new Options.Event(Description, gameObject, Options, collision.GetComponent<Character>()));
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
