using UnityEngine;
using System.Collections;

public class RectangleCollisionChecker : MonoBehaviour {

	public static bool intersects(Rect rect0, Rect rect1){
		return (rect0.x <= rect1.x + rect1.width && rect1.x <= rect0.x + rect0.width && rect0.y <= rect1.y + rect1.height && rect1.y <= rect0.y + rect0.height);
	}

	public static Rect BoundsToScreenRect(Bounds bounds)
	{
		// Get mesh origin and farthest extent (this works best with simple convex meshes)
		Vector3 origin = Camera.main.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.max.y, 0f));
		Vector3 extent = Camera.main.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.min.y, 0f));
		
		// Create rect in screen space and return - does not account for camera perspective
		return new Rect(origin.x - (origin.x - extent.x), Screen.height - origin.y, origin.x - extent.x, origin.y - extent.y);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
