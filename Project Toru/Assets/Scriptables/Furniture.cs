using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Option = Assets.Domain.Option;

public class Furniture : ScriptableObject
{
	private new string name { get; set; }
	private Sprite sprite { get; set; }
	private List<Option> options { get; set;}
	//private List<> items { get; set; }       //items implementeren
	private int sizeX { get; set; }
	private int sizeY { get; set; }

	public Furniture(Sprite sprite)
	{
		this.sprite = sprite;
	}



}
