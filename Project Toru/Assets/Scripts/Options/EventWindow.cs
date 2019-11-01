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
			GetComponent<TextMeshProUGUI>().text = string.Empty;
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
			// TODO slow down time

			GetComponent<TextMeshProUGUI>().text = EventQueue[0].Description + System.Environment.NewLine;

			current = EventQueue[0].GetOptions();
			foreach (var o in current)
				GetComponent<TextMeshProUGUI>().text += "<link>" + o.getInfo() + "</link>" + System.Environment.NewLine;
		}

		public void OnMouseOver()
		{
			int LinkIndex = TMP_TextUtilities.FindIntersectingLink(GetComponent<TextMeshProUGUI>(), Input.mousePosition, Camera.current);
			if (LinkIndex == -1)
				return;

			Debug.Log(Input.mousePosition);
			Debug.Log(LinkIndex);
			// TODO highlight text
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			int LinkIndex = TMP_TextUtilities.FindIntersectingLink(GetComponent<TextMeshProUGUI>(), Input.mousePosition, Camera.current);
			Debug.Log(Input.mousePosition);
			Debug.Log(LinkIndex);
			Debug.Log(GetComponent<TextMeshProUGUI>().textInfo.linkCount);
			if (LinkIndex == -1)
				return;
			// LinkIndex = 0;

			current[LinkIndex].Activate();
			EventQueue.RemoveAt(0);

			if (EventQueue.Count == 0)
			{
				gameObject.SetActive(false);
				// TODO speed up time
			}
			else
				DisplayNextOptions();
		}
	}
}
