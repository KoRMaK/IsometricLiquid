using UnityEngine;
using System.Collections;

public class ScreenDrawer : MonoBehaviour {

	public Texture2D tex;
	public int tex_size = 20;
	public GameObject some_cube;
	
	// Use this for initialization
	void Start () {
		tex = new Texture2D(Screen.width, Screen.height);
		for(int i = 0; i < Screen.width; i++)
		{
			for(int _x = 10; _x < 20; _x++)
			{ 
				tex.SetPixel(i, _x, Color.cyan);
			}
		}
		tex.SetPixel(2, 2, Color.cyan);
		tex.SetPixel(3, 2, Color.cyan);
		tex.SetPixel(4, 2, Color.cyan);
		tex.SetPixel(2, 3, Color.cyan);
		tex.SetPixel(3, 3, Color.cyan);
		tex.SetPixel(4, 3, Color.cyan);
		tex.Apply();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Camera cam = GetComponent<Camera>();
		
		Vector3 screen_coords = cam.WorldToScreenPoint(some_cube.transform.position);
		
		for(int i = 0; i < 20; i++)
		{
			for(int _x = 10; _x < 20; _x++)
			{ 
				tex.SetPixel((int)screen_coords.x + i, ((int)screen_coords.y) + _x, Color.cyan);
			}
		}
		tex.Apply();
		
	}
	
	void OnGUI() {
		if (!tex) {
			Debug.LogError("Assign a Texture in the inspector.");
			return;
		}
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex, ScaleMode.StretchToFill, true, 0);
	}
	
}
