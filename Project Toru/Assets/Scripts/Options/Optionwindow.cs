using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Options
{
	public class Optionwindow : MonoBehaviour
	{
		[SerializeField]
		TextMeshPro OptionBox;

		List<IDictionary<Option, Delegate>> OptionQueue;

		public void Start()
		{
			OptionQueue = new List<IDictionary<Option, Delegate>>();
			OptionBox.text = string.Empty;
		}

		public void Update()
		{
			if (!OptionBox.isActiveAndEnabled)
			{
				DisplayNewOptions(OptionQueue[0]);
				OptionQueue.RemoveAt(0);
			}
	}

		public void AddOption(IDictionary<Option, Delegate> Options)
		{
			OptionQueue.Add(Options);
		}

		public void DisplayNewOptions(IDictionary<Option, Delegate> option)
		{

		}
	}
}
