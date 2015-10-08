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
	List<GameObject> collision_objects; 
	public int threshold;
	public int max_depth; //how deep are we allowed to go
	public int depth; //how deep are we.

	public QuadTreeNode(Rect _bounds, bool[,] _bool_values, List<GameObject> collision_objects)
	{
		bounds = bounds;
		bool_values = _bool_values
	}

	public void subdivide(int _max_depth, int _depth)
	{
		//_max_depth, how far we are going to recurse
		//_depth, the current depth we are at for this quadtree
		////for us (this node) to even exist means that it has at least one hit in its reguin, find it - NOT TRUE! neccesarily
		/// we exist, but it doesnt mean we contain anything. if any of our quadtrants exist, then they have someting in them. 

		//break the rect into 4 regions and then see if anything is in it

		//int threshold_count = 0; //increase this as we find more shit

		//for each cube
			//for each subdivision rect
			//if collision(northwest, cube)
			//if collision(northeast, cube)
			//if collision(southwest, cube)
			//if collision(southeast, cube)

	}
}

