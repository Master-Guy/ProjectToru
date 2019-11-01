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
	[RequireComponent(typeof(TMP_Text))]
	public class EventWindow : MonoBehaviour, IPointerClickHandler
	{
		List<Event> EventQueue;
		List<Option> current;

		public void Start()
		{
			EventQueue = new List<Event>();
			gameObject.SetActive(false);
		}

		public void Update()
		{

		}

		public void AddEvent(Event NewEvent)
		{
			foreach (var e in EventQueue)
				if (e.Merge(NewEvent)) {
				DisplayNextOptions();
				return;
				}
			EventQueue.Add(NewEvent);
			DisplayNextOptions();
		}

		void DisplayNextOptions()
		{
			gameObject.SetActive(true);
			Time.timeScale = 0.2f;

			GetComponent<TextMeshProUGUI>().text = EventQueue[0].Description + System.Environment.NewLine;

			current = EventQueue[0].GetOptions();
			foreach (var o in current)
				GetComponent<TextMeshProUGUI>().text += "<link>" + o.getInfo() + "</link>" + System.Environment.NewLine;
		}

		public void OnMouseOver()
		{
			int LinkIndex = TMP_TextUtilities.FindIntersectingLink(GetComponent<TextMeshProUGUI>(), Input.mousePosition, Camera.main);
			if (LinkIndex == -1)
				return;

			// TODO highlight text
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			int LinkIndex = TMP_TextUtilities.FindIntersectingLink(GetComponent<TextMeshProUGUI>(), Input.mousePosition, Camera.main);
			if (LinkIndex == -1)
				return;

			current[LinkIndex].Activate();
			EventQueue.RemoveAt(0);

			if (EventQueue.Count == 0)
			{
				gameObject.SetActive(false);
				Time.timeScale = 1.0f;
			}
			else
				DisplayNextOptions();
		}
	}
}
