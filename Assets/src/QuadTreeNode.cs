using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadTreeNode {
	public QuadTreeNode northwest;
	public QuadTreeNode northeast;
	public QuadTreeNode southwest;
	public QuadTreeNode southeast;
	public Rect bounds;
	public bool[,] bool_values;
	public List<GameObject> collision_objects; 
	public int threshold;
	public int max_depth; //how deep are we allowed to go
	public int depth; //how deep are we.
	public bool b_is_occupied;
	public bool b_is_inited = false;

	public QuadTreeNode(Rect _bounds, bool[,] _bool_values, List<GameObject> _collision_objects, int _max_depth, int _depth)
	{
		bounds = _bounds;
		bool_values = _bool_values;
		collision_objects = _collision_objects;
		max_depth = _max_depth;
		depth = _depth;
		if(depth < max_depth && is_occupied())
		{
			subdivide(max_depth, depth);
		}
	}

	public bool has_leaves()
	{
		return (northwest != null || northeast != null || southeast != null || southwest != null);
	}

	public List<Rect> pluck_leaves()
	{
		List<Rect> _rects_to_search = new List<Rect>();
		if(has_leaves())
		{
			if(northwest != null)
				_rects_to_search.AddRange(northwest.pluck_leaves());
			if(northeast != null)
				_rects_to_search.AddRange(northeast.pluck_leaves());
			if(southwest != null)
				_rects_to_search.AddRange(southwest.pluck_leaves());
			if(southeast != null)
				_rects_to_search.AddRange(southeast.pluck_leaves());
		}else if(is_occupied())
		{
			_rects_to_search.Add(bounds);
		}

		return _rects_to_search;
	}

	public bool is_occupied()
	{
		if(b_is_inited == false)
		{
			b_is_occupied = false;
			foreach(GameObject _object in collision_objects)
			{			

				Rect _r1 = RectangleCollisionChecker.BoundsToScreenRect(_object.GetComponent<Renderer>().bounds);
				//TextureDraw.DrawRectangle(tex, _r1, Color.yellow);

				if( RectangleCollisionChecker.intersects(bounds, _r1))
				{
					b_is_occupied =  true;
					break;
				}					
				
			}
		}

		return b_is_occupied;
	}

	public void subdivide(int _max_depth, int _depth)
	{
		//_max_depth, how far we are going to recurse
		//_depth, the current depth we are at for this quadtree
		////for us (this node) to even exist means that it has at least one hit in its reguin, find it - NOT TRUE! neccesarily
		/// we exist, but it doesnt mean we contain anything. if any of our quadtrants exist, then they have someting in them. 

		//break the rect into 4 regions and then see if anything is in it

		//int threshold_count = 0; //increase this as we find more shit
		Rect north_west_bounds = new Rect(bounds.x, bounds.y, (int)(bounds.width/2), (int)(bounds.height/2));
		Rect north_east_bounds = new Rect(bounds.x + (int)(bounds.width/2), bounds.y, (int)(bounds.width/2), (int)(bounds.height/2));
		Rect south_east_bounds = new Rect(bounds.x + (int)(bounds.width/2), bounds.y + (int)(bounds.height/2), (int)(bounds.width/2), (int)(bounds.height/2));
		Rect south_west_bounds = new Rect(bounds.x, bounds.y + (int)(bounds.height/2), (int)(bounds.width/2), (int)(bounds.height/2));

		//for each cube
			//for each subdivision rect
			//if collision(northwest, cube)
			//if collision(northeast, cube)
			//if collision(southwest, cube)
			//if collision(southeast, cube)

		foreach(GameObject _object in collision_objects)
		{			
			
			Rect _object_bounds = RectangleCollisionChecker.BoundsToScreenRect(_object.GetComponent<Renderer>().bounds);
			//TextureDraw.DrawRectangle(tex, _r1, Color.yellow);
			if(RectangleCollisionChecker.intersects(north_west_bounds, _object_bounds))
				this.northwest = new QuadTreeNode(north_west_bounds, this.bool_values, this.collision_objects, this.max_depth, this.depth + 1);

			if(RectangleCollisionChecker.intersects(north_east_bounds, _object_bounds))
				this.northeast = new QuadTreeNode(north_east_bounds, this.bool_values, this.collision_objects, this.max_depth, this.depth + 1);
					
			if(RectangleCollisionChecker.intersects(south_east_bounds, _object_bounds))
				this.southeast = new QuadTreeNode(south_east_bounds, this.bool_values, this.collision_objects, this.max_depth, this.depth + 1);

			if(RectangleCollisionChecker.intersects(south_west_bounds, _object_bounds))
				this.southwest = new QuadTreeNode(south_west_bounds, this.bool_values, this.collision_objects, this.max_depth, this.depth + 1);
	
			/*
			if( RectangleCollisionChecker.intersects(bounds, _r1))
			{
				is_occupied =  true;
				break;
			}
			*/
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
}

