using UnityEngine;

public class RotateOnDrag : MonoBehaviour
{
	public float speed = 120f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			float mouseDeltaX = Input.GetAxis ("Mouse X");
			float step = mouseDeltaX * speed * Time.deltaTime;
			this.transform.Rotate (0f, step, 0f);
		}
	}
}
