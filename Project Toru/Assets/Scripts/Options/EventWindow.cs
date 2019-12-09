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
	static class CurrentEventWindow
	{
		public static EventWindow Current;
	}

	public enum EventTextType
	{
		options,
		characters,
		extraOption,
		result
	}

	[RequireComponent(typeof(TextMeshProUGUI))]
	[RequireComponent(typeof(BoxCollider2D))]
	public class EventWindow : MonoBehaviour, IPointerClickHandler
	{
		private string ResultMessage, DoAnotherActionString = "Do you want to do anything else?" + Environment.NewLine + "  < link > yes </ link > " + Environment.NewLine + "< link > no </ link >";

		List<Event> EventQueue;
		TextMeshProUGUI TMP;
		EventTextType TextType;
		int OptionIndex, ActorCount;

		public void Start()
		{
			EventQueue = new List<Event>();
			gameObject.SetActive(false);
			TMP = GetComponent<TextMeshProUGUI>();
			CurrentEventWindow.Current = this;
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
			DisplayNextOptions();
		}

		public void RemoveEvent(Event E)
		{
			EventQueue.Remove(E);
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
			EventQueue = EventQueue.OrderBy(o => o.priority).ToList();
			
			// slows down time depending on priority in 4 steps from full speed to 20 percent speed
			Time.timeScale = 0.2f + (float)(0.8f * Math.Floor(Math.Min(EventQueue[0].priority, 4) / 4f));

			TMP.text = EventQueue[0].GetOptionText();
			TextType = EventTextType.options;
		}

		private void BuildResult()
		{
			TMP.text = ResultMessage + Environment.NewLine + "<link> continue </link>";
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
			{
				return;
			}

			switch (TextType)
			{
				case EventTextType.options:
					ActorCount = EventQueue[0].ActivateOption(LinkIndex, ref ResultMessage);
					if (ActorCount == 1)
						goto default;

					TMP.text = EventQueue[0].GetActorText();
					TextType = EventTextType.characters;
					OptionIndex = LinkIndex;
					break;
				case EventTextType.characters:
					EventQueue[0].ActivateOption(OptionIndex, LinkIndex, ref ResultMessage);
					ActorCount--;
					if(ActorCount == 0)
						goto default;

					TMP.text = DoAnotherActionString;
					TextType = EventTextType.extraOption;
					break;
				case EventTextType.extraOption:
					if (LinkIndex == 1)
						goto default;

					DisplayNextOptions();
					break;
				case EventTextType.result:
					ResultMessage = null;
					goto default;

				default:
					if (ResultMessage != null)
					{
						BuildResult();
						TextType = EventTextType.result;
						break;
					}

					EventQueue.RemoveAt(0);

					DisplayNextOptions();

					break;
			}
		}
	}
}
