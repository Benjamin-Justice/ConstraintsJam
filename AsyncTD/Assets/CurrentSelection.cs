﻿using UnityEngine;
using System.Collections;

public class CurrentSelection : MonoBehaviour {
	public GameObject currentSelectedObject;
	public delegate void OnSelect(GameObject selectedObject);
	public delegate void OnDeselect(GameObject unselectedObject);
	public OnDeselect onDeselect=delegate{};
	public OnSelect onSelect=delegate{};
	private int mask;
	void Start () {
		onSelect += delegate (GameObject selectedObject) {
			Debug.Log ("selected " + selectedObject);
		};
		onDeselect += (unselectedObject) => Debug.Log ("unselected " + unselectedObject);
		mask = 1 << LayerMask.NameToLayer ("Selectable") | 1 << LayerMask.NameToLayer ("Enemy");
	}

	void Update () {
		if ( Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//Vector3 newDir= ray.direction*100.0f;
			//Debug.DrawRay(ray.origin,newDir ,Color.red,1f,false);

			if (Physics.Raycast (ray, out hit, 100.0f, mask)) {
				Select (hit.transform.gameObject);
			} else {
				Deselect (currentSelectedObject);
			}
		}
	}
	public void Deselect (GameObject gameObject)
	{
	    if (gameObject != null) {
			onDeselect (currentSelectedObject);
		}
		currentSelectedObject = null;
	}
	public void Select (GameObject gameObject)
	{
		if (gameObject == currentSelectedObject) {
			return;
		}
		if (currentSelectedObject != null) {
			onDeselect (currentSelectedObject);
		}
		currentSelectedObject = gameObject;
		onSelect (currentSelectedObject);
	}

}
