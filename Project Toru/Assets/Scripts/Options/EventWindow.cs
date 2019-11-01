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
		int LinkIndex;

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
			// TODO slow down time

			GetComponent<TextMeshProUGUI>().text = EventQueue[0].Description + System.Environment.NewLine;

			current = EventQueue[0].GetOptions();
			foreach (var o in current)
				GetComponent<TextMeshProUGUI>().text += "<link>" + o.getInfo() + "</link>" + System.Environment.NewLine;
		}

		TextMeshProUGUI last;
		Camera lastCam;

		public void OnMouseOver()
		{
			LinkIndex = TMP_TextUtilities.FindIntersectingLink(GetComponent<TextMeshProUGUI>(), Input.mousePosition, Camera.main);
			if (LinkIndex == -1)
				return;

			if (GetComponent<TextMeshProUGUI>() != last) {
				last = GetComponent<TextMeshProUGUI>();
				Debug.Log("Changed TMPro");
				Debug.Log(last);
			}
			if (Camera.current != lastCam)
			{
				lastCam = Camera.current;
				Debug.Log("Camera changed on hover");
				Debug.Log(lastCam);
			}
			// TODO highlight text
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (GetComponent<TextMeshProUGUI>() != last)
			{
				last = GetComponent<TextMeshProUGUI>();
				Debug.Log("Changed TMPro");
				Debug.Log(last);
			}
			if (Camera.current != lastCam)
			{
				lastCam = Camera.current;
				Debug.Log("Camera changed on click");
				Debug.Log(lastCam);
			}

			if (LinkIndex == -1)
				return;

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
