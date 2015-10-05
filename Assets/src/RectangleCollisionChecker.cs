using UnityEngine;
using System.Collections;

public class RectangleCollisionChecker : MonoBehaviour {

	public static bool intersects(Rect rect0, Rect rect1){
		return (rect0.x <= rect1.x + rect1.width && rect1.x <= rect0.x + rect0.width && rect0.y <= rect1.y + rect1.height && rect1.y <= rect0.y + rect0.height);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
