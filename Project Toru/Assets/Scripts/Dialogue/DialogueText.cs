﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueText
{
	public string name;

	[TextArea(4,10)]
	public string[] sentences;
}
