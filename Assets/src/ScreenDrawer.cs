using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenDrawer : MonoBehaviour {

	public Texture2D tex;
	public GameObject some_cube;
	public static int quadtree_max_depth = 5;
	public static int resolution = 64;
	//create a 16x16 array of booleans
	public bool[,] bool_values = new bool[resolution, resolution];
	public Vector2[][] marching_cube_templates = new Vector2[16][];
	
	public GameObject cube_prefab;
	public List<GameObject> cubes;

	private QuadTreeNode quadtree_for_this_update;
	
	// Use this for initialization
	void Start () {
		
		cubes = new List<GameObject>();
		tex = new Texture2D(Screen.width, Screen.height);
		TextureDraw.ClearTexture(tex);
		init_cube_templates();
		init_terrain_data();

		//draw_marched_squares();
		
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
		//TextureDraw.DrawLine(tex, 10, 10, 30, 30, Color.yellow);
		tex.Apply();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Camera cam = GetComponent<Camera>();
		
		//Vector3 screen_coords = cam.WorldToScreenPoint(some_cube.transform.position);
		TextureDraw.ClearTexture(tex);

		//set everything to false
		for(int i = 0; i<  resolution; i++)
		{
			for(int j = 0; j<  resolution; j++)
			{
				bool_values[i, j] =  false;
				
			}
		}

		quadtree_for_this_update = new QuadTreeNode(new Rect(0,0,Screen.width,Screen.height), bool_values, cubes, quadtree_max_depth, 1);
		determine_terrain_data();
		draw_marched_squares();
		//draw_cubes();
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
	
		int _multi_val_x = Screen.width / (resolution);
		int _multi_val_y = Screen.height / (resolution);

		/*

		QuadTreeNode quad_tree = quadtree_for_this_update;
		//first, carve up the region and parition them into areas with stuff
		//given an XxY array
		
		
		//now traverse the quad tree and only do checks for areas that have hits in them
		List<Rect> _rects_to_search = quad_tree.pluck_leaves();
		
		foreach(Rect _rect in _rects_to_search)
		{
			//TextureDraw.DrawRectangle(tex, _rect, Color.yellow);

			int start_x = 0;//(int)_rect.x/_multi_val_x;
			if( start_x > 1)
				start_x -= 1;

			for(int i = start_x; i<  Mathf.CeilToInt((_rect.x +_rect.width)/_multi_val_x) && i < resolution - 1; i++)
			{
				for(int j = (int)_rect.y/_multi_val_y; j<  Mathf.CeilToInt((_rect.y +_rect.height)/_multi_val_y) && j < resolution - 1; j++)
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

					//ok we got our index, now draw it
					if(marching_cube_templates[index] != null)
					{
						for(int idx = 0; idx + 1< marching_cube_templates[index].Length; idx += 2)
						{
							TextureDraw.DrawLine(tex, (int)(marching_cube_templates[index][idx].x * _multi_val_x) + (i * _multi_val_x) + (_multi_val_x/2), (int)(marching_cube_templates[index][idx].y * _multi_val_y) + (j * _multi_val_y) + (_multi_val_y/2), (int)(marching_cube_templates[index][idx + 1].x * _multi_val_x) + (i * _multi_val_x) + (_multi_val_x/2), (int)(marching_cube_templates[index][idx + 1].y * _multi_val_y) + (j * _multi_val_y) + (_multi_val_y/2), Color.cyan);
						}
					}

				}
			}
		}

		*/

	
				//now that the values are set, lets draw them
		for(int i = 0; i<  resolution - 1; i++)
		{
			for(int j = 0; j < resolution - 1; j++)
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
					
				
				//ok we got our index, now draw it
				if(marching_cube_templates[index] != null)
				{
					for(int idx = 0; idx + 1< marching_cube_templates[index].Length; idx += 2)
					{
						TextureDraw.DrawLine(tex, (int)(marching_cube_templates[index][idx].x * _multi_val_x) + (i * _multi_val_x) + (_multi_val_x/2), (int)(marching_cube_templates[index][idx].y * _multi_val_y) + (j * _multi_val_y) + (_multi_val_y/2), (int)(marching_cube_templates[index][idx + 1].x * _multi_val_x) + (i * _multi_val_x) + (_multi_val_x/2), (int)(marching_cube_templates[index][idx + 1].y * _multi_val_y) + (j * _multi_val_y) + (_multi_val_y/2), Color.cyan);
					}
				}					
			
			}
		}

	}
	
	void init_cube_templates()
	{
		marching_cube_templates[0] = null; //new 2(0, 0);
		marching_cube_templates[1] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f)};
		marching_cube_templates[2] = new Vector2[] {new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[3] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[4] = new Vector2[] {new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[5] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[6] = new Vector2[] {new Vector2(0.5f, 1.0f), new Vector2(0.5f, 0.0f)};
		marching_cube_templates[7] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f)};
		marching_cube_templates[8] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f)};
		marching_cube_templates[9] = new Vector2[] {new Vector2(0.5f, 1.0f), new Vector2(0.5f, 0.0f)};
		marching_cube_templates[10] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f), new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[11] = new Vector2[] {new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[12] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[13] = new Vector2[] {new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f)};
		marching_cube_templates[14] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f)};
		marching_cube_templates[15] = new Vector2[] {new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f)};//, new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.2f), new Vector2(0.0f, 0.7f), new Vector2(1.0f, 0.7f) }; //new Vector2[] {new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f), new Vector2(0.0f, 1.0f), new Vector2(1.0f, 0.0f)};
		
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

		/*
		//randomize the inner ones
		for(int i = 1; i<  resolution - 1; i++)
		{
			for(int j = 1; j<  resolution - 1; j++)
			{
				bool_values[i, j] =  (Random.value > 0.5f) ;
				
			}
		}
		*/
		
	}
	
	void determine_terrain_data()
	{
		//for each subdivision (so thats a for by for loop
		// for each cube (n^3 now i think)
		//see if the cube is within the bounds of this cube, and if it is, set its terrain data to true.
		//break out of the loop if the data is true for this cube
		
		int _multi_val_x = Screen.width / (resolution);
		int _multi_val_y = Screen.height / (resolution );
		
		Camera cam = GetComponent<Camera>();

		QuadTreeNode quad_tree = quadtree_for_this_update;
		//first, carve up the region and parition them into areas with stuff
		//given an XxY array


		//now traverse the quad tree and only do checks for areas that have hits in them
		List<Rect> _rects_to_search = quad_tree.pluck_leaves();

		foreach(Rect _rect in _rects_to_search)
		{
			//TextureDraw.DrawRectangle(tex, _rect, Color.yellow);

			for(int i = (int)_rect.x/_multi_val_x; i<  Mathf.CeilToInt((_rect.x +_rect.width)/_multi_val_x) && i < resolution; i++)
			{
				for(int j = (int)_rect.y/_multi_val_y; j<  Mathf.CeilToInt((_rect.y +_rect.height)/_multi_val_y) && j < resolution; j++)
				{
					Rect _r0 = new Rect(new Vector2(i * _multi_val_x, j * _multi_val_y), new Vector2(_multi_val_x, _multi_val_y));
					//TextureDraw.DrawRectangle(tex, _r0, Color.blue);
					float _metaball_value = 0;


					foreach(GameObject _cube in cubes)
					{

						if( bool_values[i, j] ==  true)
							break;


						
						
						Vector3 screen_coords = cam.WorldToScreenPoint(_cube.transform.position);


						Rect _r1 = RectangleCollisionChecker.BoundsToScreenRect(_cube.GetComponent<Renderer>().bounds);
						//TextureDraw.DrawRectangle(tex, _r1, Color.yellow);

						float r = (_r1.width );
						float _threshold_value = (Mathf.Pow(r, 2))/( Mathf.Pow(( _r1.center.x - _r0.center.x), 2) + Mathf.Pow(( _r1.center.y - _r0.center.y), 2) );
						_metaball_value += _threshold_value;

						//_r1.y = Screen.height - _r1.y;
						//Rect _r11 = new Rect(new Vector2(screen_coords.x, screen_coords.y), new Vector2(50, 50));
						//Debug.Log(" bounds _r1 bounds " + _r1.ToString());
						//Debug.Log(" bounds _r11 " + _r11.ToString());
						if( _metaball_value >= 1f) //if( RectangleCollisionChecker.intersects(_r0, _r1))
						{
							bool_values[i, j] =  true;
							break;
						}


						
					}



				}
			}
		}


		

		/*
		for(int i = 0; i<  resolution; i++)
		{
			for(int j = 0; j<  resolution; j++)
			{
				Rect _r0 = new Rect(new Vector2(i * _multi_val_x, j * _multi_val_y), new Vector2(_multi_val_x, _multi_val_y));
				TextureDraw.DrawRectangle(tex, _r0, Color.blue);
				
				foreach(GameObject _cube in cubes)
				{
					if( bool_values[i, j] ==  true)
						break;
					
					
					Vector3 screen_coords = cam.WorldToScreenPoint(_cube.transform.position);
					
					Rect _r1 = RectangleCollisionChecker.BoundsToScreenRect(_cube.GetComponent<Renderer>().bounds);
					TextureDraw.DrawRectangle(tex, _r1, Color.yellow);
					//_r1.y = Screen.height - _r1.y;
					//Rect _r11 = new Rect(new Vector2(screen_coords.x, screen_coords.y), new Vector2(50, 50));
					//Debug.Log(" bounds _r1 bounds " + _r1.ToString());
					//Debug.Log(" bounds _r11 " + _r11.ToString());
					if( RectangleCollisionChecker.intersects(_r0, _r1))
					{
						bool_values[i, j] =  true;
						break;
					}					
					
				}
				
			}
		}
		*/
		
		
	}

	void draw_cubes()
	{
		foreach (GameObject _cube in cubes) 
		{
			Rect _r1 = RectangleCollisionChecker.BoundsToScreenRect (_cube.GetComponent<Renderer> ().bounds);
			Rect _r1_tex = new Rect(_r1.x, _r1.y, _r1.width, _r1.height);
			TextureDraw.DrawRectangle(tex, _r1_tex, Color.green);
			//TextureDraw.DrawLine(tex, (int)(_r1.x), (int)(_r1.y), (int)(_r1.xMax), (int)(_r1.y), Color.cyan);
			//Rect _r2 = RectangleCollisionChecker.BoundsToScreenRect(_cube.GetComponent<Renderer>().bounds);
			//TextureDraw.DrawLine(tex, (int)(_r1.x), (int)(_r1.y), (int)(_r1.xMax), (int)(_r1.yMax), Color.cyan);
		}
		//tex.Apply();
	}
	
	float ScreenYToTextureY(float screen_y)
	{
		return Screen.height - screen_y;
	}
	
}
