//#define LineA
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TextureDraw {

	public static void DrawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color col)
	{
		//first, lets flop the y's because they shuld be in screen space
		y0 = Screen.height - y0;
		y1 = Screen.height - y1;


#if(LineA)

		float x,y;
		float dy = y1-y0;    
		float dx = x1-x0;    
		float m = dy/dx; 
		float dy_inc = 1;    
		
		if(  dy < 0 ) 
			dy = -1;    
		
		float dx_inc = 1;    
		if(  dx < 0 ) 
			dx = -1; 
		
		if( Mathf.Abs(dy) > Mathf.Abs(dx) )
		{

			if(float.IsInfinity(m))
				m = 0;

			for( y = y1; y < y0; y += dy_inc )
			{                
				x = x0 + ( y - y0 ) * m;
				//Debug.Log("Setting Pixel at: x=" + x + ", y=" + y);
				tex.SetPixel((int)(x), (int)(y), col);       
			}

		}
		else
		{

			for( x = x0; x < x1; x +=  dx_inc  ) 
			{
				y = y0 + ( x - x1 ) * m;
				//Debug.Log("Setting Pixel at: x=" + x + ", y=" + y);
				tex.SetPixel((int)(x), (int)(y), col);           
			}

		}


#else


		int dy = (int)(y1-y0);
		int dx = (int)(x1-x0);
		int stepx, stepy;
		
		if (dy < 0)
		{
			dy = -dy; stepy = -1;
		}else {
			stepy = 1;
		}

		if (dx < 0) {
			dx = -dx; stepx = -1;
		}else {
			stepx = 1;
		}

		dy <<= 1;
		dx <<= 1;
		
		float fraction = 0;
		
		tex.SetPixel(x0, y0, col);
		if (dx > dy) {
			fraction = dy - (dx >> 1);
			while (Mathf.Abs(x0 - x1) > 1) {
				if (fraction >= 0) {
					y0 += stepy;
					fraction -= dx;
				}
				x0 += stepx;
				fraction += dy;
				tex.SetPixel(x0, y0, col);
			}
		}
		else {
			fraction = dx - (dy >> 1);
			while (Mathf.Abs(y0 - y1) > 1) {
				if (fraction >= 0) {
					x0 += stepx;
					fraction -= dy;
				}
				y0 += stepy;
				fraction += dx;
				tex.SetPixel(x0, y0, col);
			}
		}
#endif


	}
	
	public static void DrawLine(Texture2D tex, float x0, float y0, float x1, float y1, Color col)
	{
		DrawLine(tex, (int)x0, (int)y0, (int)x1, (int)y1, col);
	}
	
	public static void DrawRectangle(Texture2D tex, Rect rect, Color color)
	{
		//top left -> top right
		//top left -> bottom left
		//top right -> bottom right
		//bottom left -> bottom right
		DrawLine(tex, rect.xMin, rect.yMin, rect.xMax, rect.yMin, color);
		DrawLine(tex, rect.xMin, rect.yMin, rect.xMin, rect.yMax, color);
		DrawLine(tex, rect.xMax, rect.yMin, rect.xMax, rect.yMax, color);
		DrawLine(tex, rect.xMin, rect.yMax, rect.xMax, rect.yMax, color);
		
	}

	public static Color[] clearfillColorArray;

	public static void InitClearTexture(Texture2D tex)
	{
		Color fillColor  = Color.clear;

		IEnumerable<Color> colors = Enumerable.Repeat(fillColor, tex.width * tex.height);
		clearfillColorArray = colors.ToArray();
	}
	
	public static void ClearTexture(Texture2D tex)
	{
		/*
		for(int x = 0; x < Screen.width; x++)
		{
			for(int y = 0; y < Screen.height; y++)
			{
				tex.SetPixel(x, y, Color.clear);
			}
		}
		*/

		//Color fillColor  = Color.clear;

		//Color[] fillColorArray =  tex.GetPixels();



		//IEnumerable<Color> colors = Enumerable.Repeat(fillColor, tex.width * tex.height);
		//Color[] fillColorArray = colors.ToArray();
		/*
		for(var i = 0; i < fillColorArray.Length; ++i)
		{
			fillColorArray[i] = fillColor;
		}
		*/
		
		tex.SetPixels(clearfillColorArray );

	}
	
}
