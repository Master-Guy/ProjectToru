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
		public Vector2 size = new Vector2(1, 1);

		[SerializeField]
		Sprite sprite;

		[SerializeField]
		List<Option> options;

		[SerializeField]
		bool passable;

		Room Parent;

		void Start()
		{
			
		}

		void Update()
		{
			
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			// TODO if this is character destination check
			foreach(Option o in options)
			{
				// TODO if not passable stop character movement/vault character
				o.getInfo();
			}
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
