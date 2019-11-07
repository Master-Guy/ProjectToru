using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour
{

	[SerializeField]
	bool activated = true;
	bool currentActivated = true;

	[SerializeField]
	CardreaderColor color = CardreaderColor.Disabled;
	CardreaderColor currentColor = CardreaderColor.Disabled;

	[SerializeField]
	SpriteRenderer StatusIndicator = null;

	[SerializeField]
	SpriteRenderer ColorIndicator = null;

	void Start()
	{
		UpdateColor();
	}

	void Update()
	{
		if ((activated != currentActivated) || (color != currentColor))
		{
			UpdateColor();
		}
	}

	void UpdateColor()
	{
		// Update Status indicator
		if (activated)
		{
			StatusIndicator.color = ColorZughy.green;
			currentActivated = activated;
		}
		else
		{
			StatusIndicator.color = ColorZughy.red;
		}
		currentActivated = activated;


		// Update Color Indicator
		switch (color)
		{
			case CardreaderColor.Blue:
				ColorIndicator.color = ColorZughy.cyan;
				break;
			case CardreaderColor.Purple:
				ColorIndicator.color = ColorZughy.purple;
				break;
			case CardreaderColor.Yellow:
				ColorIndicator.color = ColorZughy.yellow;
				break;
			default:
				ColorIndicator.color = ColorZughy.grey;
				break;
		}
		currentColor = color;
	}

	enum CardreaderColor
	{
		Disabled,
		Blue,
		Purple,
		Yellow
	}

}
