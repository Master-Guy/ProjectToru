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
	public class Optionwindow : MonoBehaviour, IPointerClickHandler
	{
		List<KeyValuePair<String, List<Option>>> OptionQueue;

		public void Start()
		{
			OptionQueue = new List<KeyValuePair<String, List<Option>>>();
			GetComponent<TextMeshPro>().text = string.Empty;
		}

		public void Update()
		{

			if (!GetComponent<TextMeshPro>().isActiveAndEnabled)
				if (OptionQueue.Count != 0)
				{
					DisplayNextOptions();
				}

	}

		public void AddOption(string text, List<Option> Options)
		{
			OptionQueue.Add(new KeyValuePair<string, List<Option>>(text, Options));
		}

		public void DisplayNextOptions()
		{
			GetComponent<TextMeshPro>().text = OptionQueue[0] + System.Environment.NewLine;


			int i = 0;
			foreach(var o in OptionQueue[0].Value)
				GetComponent<TextMeshPro>().text += "<link=" + i++ + ">" + o.getInfo() + "</link>" + System.Environment.NewLine;

			OptionQueue.RemoveAt(0);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			TMP_TextUtilities.FindIntersectingLink(GetComponent<TextMeshPro>(), eventData.position, null);
			GetComponentInChildren<MeshRenderer>().enabled =
			GetComponent<TextMeshPro>().enabled = false;
		}
	}
}
