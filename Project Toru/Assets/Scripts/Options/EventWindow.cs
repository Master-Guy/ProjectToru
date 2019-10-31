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
	public class EventWindow : MonoBehaviour
	{
		List<Event> EventQueue;
		List<Option> current;

		public void Start()
		{
			EventQueue = new List<Event>();
			GetComponent<TextMeshProUGUI>().enabled = false;
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
			GetComponent<TextMeshProUGUI>().enabled = true;
			// TODO slow down time

			GetComponent<TextMeshProUGUI>().text = EventQueue[0].Description + System.Environment.NewLine;

			int i = 0;
			current = EventQueue[0].GetOptions();
			foreach (var o in current)
				GetComponent<TextMeshProUGUI>().text += "<link=" + i++ + ">" + o.getInfo() + "</link>" + System.Environment.NewLine;

		}
		public void OnMouseDown()
		{
			int LinkIndex = TMP_TextUtilities.FindIntersectingLink(GetComponent<TextMeshProUGUI>(), Input.mousePosition, Camera.current);
			Debug.Log(Input.mousePosition);
			Debug.Log(LinkIndex);
			if (LinkIndex == -1)
				return;

			current[LinkIndex].Activate();
			EventQueue.RemoveAt(0);

			if (EventQueue.Count != 0)
			{
				GetComponent<TextMeshProUGUI>().enabled = false;
				// TODO speed up time
			}
			else
				DisplayNextOptions();
		}

		public void OnMouseOver()
		{
			int LinkIndex = TMP_TextUtilities.FindIntersectingLink(GetComponent<TextMeshProUGUI>(), Input.mousePosition, Camera.current);
			Debug.Log(Input.mousePosition);
			Debug.Log(LinkIndex);
			if (LinkIndex == -1)
				return;

			// TODO highlight text
		}
	}
}
