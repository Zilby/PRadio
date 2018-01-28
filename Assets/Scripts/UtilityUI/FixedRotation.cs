using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Don't look at me ;_;
/// </summary>
public class FixedRotation : MonoBehaviour {

	Quaternion rotation;
	Vector3 position;
	Vector3 scale;
	Transform parent;

	void Awake()
	{
		parent = transform.parent;
		transform.SetParent(transform.parent.parent.parent);
		rotation = transform.rotation;
		position = transform.position;
		scale = transform.localScale;
		transform.SetParent(parent);
	}
	void LateUpdate()
	{
		transform.SetParent(transform.parent.parent.parent);
		transform.rotation = rotation;
		transform.position = position;
		transform.localScale = scale;
		transform.SetParent(parent);
	}
}
