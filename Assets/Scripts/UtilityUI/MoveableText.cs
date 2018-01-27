using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Moves text across screen. 
/// </summary>
public class MoveableText : MonoBehaviour
{
	[System.NonSerialized]
	public bool skip;

	private TextMeshProUGUI text;
	private float letterDelay = 0.005f;

	// Use this for initialization
	void Start()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
	{

	}


	IEnumerator TypeText(string message)
	{
		string current = "";
		text.text = "";
		char[] m = message.ToCharArray();
		for (int i = 0; i < message.Length; i++)
		{
			if (skip)
			{
				text.text = message;
				skip = false;
				break;
			}
			current += m[i];
			text.text = current;
			text.text += "<color=#00000000>";
			for (int j = i + 1; j < message.Length; j++)
			{
				text.text += m[j];
			}
			text.text += "</color>";
			yield return new WaitForSeconds(letterDelay);
		}
	}
}
