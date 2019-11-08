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
		[SerializeField]
		bool Passable;

		[SerializeField]
		string Description = string.Empty;

		List<Option> Options;
		List<Item> items;
		Room Parent;


		void Start()
		{
			Options = GetComponentsInChildren<Option>().ToList();
			items = GetComponentsInChildren<Item>().ToList();
			foreach (Item i in items)
				i.gameObject.SetActive(false);
		}

		void Update()
		{

		}

		public void drop()
		{
			foreach (Item i in items)
				i.gameObject.SetActive(true);
		}

		void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player") && collision.isTrigger)	// check if character has a destination, if so check if it is this
				CurrentEventWindow.Current.AddEvent(new Options.Event(Description, gameObject, Options, collision.GetComponent<Character>()));
		}

		void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.CompareTag("Player") && collision.isTrigger)  // check if character has a destination, if so check if it is this
				CurrentEventWindow.Current.RemoveEvent(gameObject, collision.GetComponent<Character>());
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
