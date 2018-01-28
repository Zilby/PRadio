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
	void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	public void ClearText()
	{
		text.text = "";
	}

	public IEnumerator TypeText(string message)
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
			yield return new WaitForSecondsRealtime(letterDelay);
		}
	}
}
