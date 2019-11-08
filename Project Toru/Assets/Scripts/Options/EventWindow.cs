using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Options
{
	public enum EventTextType
	{
		options,
		characters,
		extraAction
	}

	[RequireComponent(typeof(TMP_Text))]
	public class EventWindow : MonoBehaviour, IPointerClickHandler
	{
		List<Event> EventQueue;
		TextMeshProUGUI TMP;
		EventTextType TextType;
		int OptionIndex;

		public void Start()
		{
			EventQueue = new List<Event>();
			gameObject.SetActive(false);
			TMP = GetComponent<TextMeshProUGUI>();
		}

		public void Update()
		{

		}

		public void AddEvent(Event NewEvent)
		{
			foreach (var e in EventQueue)
				if (e.Merge(NewEvent))
				{
					DisplayNextOptions();
					return;
				}
			EventQueue.Add(NewEvent);
			DisplayNextOptions();
		}

		public void RemoveEvent(GameObject g, Character c)
		{
			int temp;
			for (int i = 0; i < EventQueue.Count; i++) {
				temp = EventQueue[i].Remove(g, c);
				if (temp > 0)
					break;
				if (temp == 0)
					EventQueue.RemoveAt(i);
			}

			if (EventQueue.Count == 0)
			{
				gameObject.SetActive(false);
				Time.timeScale = 1.0f;
			}
			else
				DisplayNextOptions();
		}

		public void RemoveEvent(Event E)
		{
			EventQueue.Remove(E);

			if (EventQueue.Count == 0)
			{
				gameObject.SetActive(false);
				Time.timeScale = 1.0f;
			}
			else
				DisplayNextOptions();
		}

		void DisplayNextOptions()
		{
			if (EventQueue.Count == 0)
			{
				gameObject.SetActive(false);
				Time.timeScale = 1.0f;
				return;
			}
			gameObject.SetActive(true);
			Time.timeScale = 0.2f;

			TMP.text = EventQueue[0].GetOptionText();
			TextType = EventTextType.options;
		}

		public void OnMouseOver()
		{
			int LinkIndex = TMP_TextUtilities.FindIntersectingLink(TMP, Input.mousePosition, null); //Camera.main);
			if (LinkIndex == -1)
				return;

			// TODO highlight text
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			int LinkIndex = TMP_TextUtilities.FindIntersectingLink(TMP, Input.mousePosition, null); //Camera.main);
			if (LinkIndex == -1)
				return;

			switch (TextType)
			{
				case EventTextType.options:
					if (EventQueue[0].ActivateOption(LinkIndex))
						goto default;

					TMP.text = EventQueue[0].GetActorText();
					TextType = EventTextType.characters;
					OptionIndex = LinkIndex;
					break;
				case EventTextType.characters:
					EventQueue[0].ActivateOption(OptionIndex, LinkIndex);
					goto default;

					// TODO add possibility to do another action
				default:
					EventQueue.RemoveAt(0);

					DisplayNextOptions();

					break;
			}
		}
	}
}
