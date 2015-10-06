using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenDrawer : MonoBehaviour {

	public Texture2D tex;
	public GameObject some_cube;
	public static int resolution = 16;
	//create a 16x16 array of booleans
	public bool[,] bool_values = new bool[resolution, resolution];
	public Vector2[][] marching_cube_templates = new Vector2[16][];
	
	public GameObject cube_prefab;
	public List<GameObject> cubes;
	
	// Use this for initialization
	void Start () {
		
		cubes = new List<GameObject>();
		tex = new Texture2D(Screen.width, Screen.height);
		init_cube_templates();
		init_terrain_data();
		draw_marched_squares();
		
		//create a bunch of cubes
		for(int i = 0; i < 5; i++)
		{
			cubes.Add((GameObject)(Instantiate(cube_prefab, new Vector3(2.5f, 3f, 5f), Quaternion.identity)));
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
		
		//Vector3 screen_coords = cam.WorldToScreenPoint(some_cube.transform.position);
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
		
		tex = new Texture2D(Screen.width, Screen.height);
		determine_terrain_data();
		draw_marched_squares();
		draw_cubes();
		tex.Apply();
	}
	
	void OnGUI() {
		if (!tex) {
			Debug.LogError("Assign a Texture in the inspector.");
			return;
		}
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex, ScaleMode.StretchToFill, true, 0);
	}
	
	void draw_marched_squares()
	{
	
				//now that the values are set, lets draw them
		for(int i = 0; i<  resolution - 1; i++)
		{
			for(int j = 0; j < resolution - 1; j++)
			{
				int index = 0;
				if(bool_values[i,j])
					index += 1;
				if(bool_values[i+1,j])
					index += 2;
				if(bool_values[i+1, j+1])
					index += 4;
				if(bool_values[i, j+1])
					index += 8;
					
				int _multi_val_x = Screen.width / (resolution - 1);
				int _multi_val_y = Screen.height / (resolution - 1);
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
		tex.Apply();
	}
	
	void init_cube_templates()
	{
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
		
	}
	
	void init_terrain_data()
	{
		//set everything to false
		for(int i = 0; i<  resolution; i++)
		{
			for(int j = 0; j<  resolution; j++)
			{
				bool_values[i, j] =  false;
				
			}
		}
		
		//randomize the inner ones
		for(int i = 1; i<  resolution - 1; i++)
		{
			for(int j = 1; j<  resolution - 1; j++)
			{
				bool_values[i, j] =  (Random.value > 0.5f) ;
				
			}
		}
		
	}
	
	void determine_terrain_data()
	{
		//for each subdivision (so thats a for by for loop
		// for each cube (n^3 now i think)
		//see if the cube is within the bounds of this cube, and if it is, set its terrain data to true.
		//break out of the loop if the data is true for this cube
		
		int _multi_val_x = Screen.width / (resolution - 1);
		int _multi_val_y = Screen.height / (resolution - 1);
		
		Camera cam = GetComponent<Camera>();
		
		
		//set everything to false
		for(int i = 0; i<  resolution; i++)
		{
			for(int j = 0; j<  resolution; j++)
			{
				bool_values[i, j] =  false;
				
			}
		}
		
		
		//randomize the inner ones
		for(int i = 0; i<  resolution; i++)
		{
			for(int j = 0; j<  resolution; j++)
			{
				
				foreach(GameObject _cube in cubes)
				{
					
					
					Vector3 screen_coords = cam.WorldToScreenPoint(_cube.transform.position);

					Rect _r0 = new Rect(new Vector2(i * _multi_val_x, j * _multi_val_y), new Vector2(_multi_val_x, _multi_val_y));
					Rect _r1 = RectangleCollisionChecker.BoundsToScreenRect(_cube.GetComponent<Renderer>().bounds);
					_r1.y = Screen.height - _r1.y;
					//Rect _r11 = new Rect(new Vector2(screen_coords.x, screen_coords.y), new Vector2(50, 50));
					//Debug.Log(" bounds _r1 bounds " + _r1.ToString());
					//Debug.Log(" bounds _r11 " + _r11.ToString());
					if( RectangleCollisionChecker.intersects(_r0, _r1))
					{
						bool_values[i, j] =  true;
						//break;
					}					
					
				}
				
			}
		}
		
		
	}

	void draw_cubes()
	{
		foreach (GameObject _cube in cubes) 
		{
			Rect _r1 = RectangleCollisionChecker.BoundsToScreenRect (_cube.GetComponent<Renderer> ().bounds);
			TextureDraw.DrawLine(tex, (int)(_r1.x), (int)(Screen.height - _r1.y), (int)(_r1.xMax), (int)(Screen.height - _r1.y), Color.cyan);
			//Rect _r2 = RectangleCollisionChecker.BoundsToScreenRect(_cube.GetComponent<Renderer>().bounds);
			TextureDraw.DrawLine(tex, (int)(_r1.x), (int)(Screen.height - _r1.y), (int)(_r1.xMax), (int)(Screen.height - _r1.yMax), Color.cyan);
		}
		tex.Apply();
	}
	
}
