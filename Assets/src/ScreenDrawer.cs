using UnityEngine;
using System.Collections;

public class ScreenDrawer : MonoBehaviour {

	public Texture2D tex;
	public int tex_size = 20;
	public GameObject some_cube;
	//create a 16x16 array of booleans
	public bool[,] bool_values = new bool[16,16];
	public Vector2[][] marching_cube_templates = new Vector2[16][];
	
	// Use this for initialization
	void Start () {
	
		tex = new Texture2D(Screen.width, Screen.height);
		
		marching_cube_templates[0] = null; //new 2(0, 0);
		marching_cube_templates[1] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f)};
		marching_cube_templates[2] = new Vector2[] {new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[3] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[4] = new Vector2[] {new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[5] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f), new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[6] = new Vector2[] {new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f)};
		marching_cube_templates[7] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f)};
		marching_cube_templates[8] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f)};
		marching_cube_templates[9] = new Vector2[] {new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f)};
		marching_cube_templates[10] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[11] = new Vector2[] {new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[12] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[13] = new Vector2[] {new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[14] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f)};
		marching_cube_templates[15] = new Vector2[] {new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.0f), new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.0f), new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.2f), new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f), new Vector2(0.0f, 0.7f), new Vector2(1.0f, 0.7f), new Vector2(0.0f, 1.0f), new Vector2(1.0f, 1.0f)}; //new Vector2[] {new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f), new Vector2(0.0f, 1.0f), new Vector2(1.0f, 0.0f)};
		
		for(int i = 0; i<  16; i++)
		{
			for(int j = 0; j<  16; j++)
			{
				bool_values[i, j] =  (Random.value > 0.5f) ;
			
			}
		}
		
		//now that the values are set, lets draw them
		for(int i = 0; i<  15; i++)
		{
			for(int j = 0; j < 15; j++)
			{
				int index = 0;
				if(bool_values[i,j])
					index += 8;
				if(bool_values[i+1,j])
					index += 4;
				if(bool_values[i+1, j+1])
					index += 2;
				if(bool_values[i, j+1])
					index += 1;
					
				int _multi_val_x = Screen.width / 15;
				int _multi_val_y = Screen.height / 15;
				//ok we got our index, now draw it
				if(marching_cube_templates[index] != null)
				{
					for(int idx = 0; idx + 1< marching_cube_templates[index].Length; idx += 2)
					{
						TextureDraw.DrawLine(tex, (int)(marching_cube_templates[index][idx].x * _multi_val_x) + (i * _multi_val_x), (int)(marching_cube_templates[index][idx].y * _multi_val_y) + (j * _multi_val_y), (int)(marching_cube_templates[index][idx + 1].x * _multi_val_x) + (i * _multi_val_x), (int)(marching_cube_templates[index][idx + 1].y * _multi_val_y) + (j * _multi_val_y), Color.cyan);
					}
				}
					
			}
		}
		
		/*
		
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
		*/
		tex.Apply();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Camera cam = GetComponent<Camera>();
		
		Vector3 screen_coords = cam.WorldToScreenPoint(some_cube.transform.position);
		/*
		for(int i = 0; i < 20; i++)
		{
			for(int _x = 10; _x < 20; _x++)
			{ 
				tex.SetPixel((int)screen_coords.x + i, ((int)screen_coords.y) + _x, Color.cyan);
			}
		}
		tex.Apply();
		*/
	}
	
	void OnGUI() {
		if (!tex) {
			Debug.LogError("Assign a Texture in the inspector.");
			return;
		}
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex, ScaleMode.StretchToFill, true, 0);
	}
	
}
