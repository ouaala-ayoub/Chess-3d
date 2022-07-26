using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman
{
	public override bool[,] PossibleMove()
	{
		bool[,] p = new bool[8, 8];
		Chessman c;

		//up right
		for(int i = CurrentX + 1, j = CurrentY + 1; i < 8; i++, j++)
		{
			if (j == 8) break;
			
			c = BoardScript.instance.Chessmans[i, j];
			if(c == null)
			{
					p[i, j] = true;
			}
			else 
			{
				if (c.isWhite != isWhite)
				{
					p[i, j] = true;
				}
				break;
			}
		}

		//up left
		for (int i = CurrentX - 1, j = CurrentY + 1; i >= 0; i--, j++)
		{
			if (j == 8) break;

			c = BoardScript.instance.Chessmans[i, j];
			if (c == null)
			{
				p[i, j] = true;
			}
			else
			{
				if (c.isWhite != isWhite)
				{
					p[i, j] = true;
				}
				break;
			}
		}

		//down right
		for (int i = CurrentX + 1, j = CurrentY - 1; i < 8; i++, j--)
		{
			if (j < 0) break;

			c = BoardScript.instance.Chessmans[i, j];
			if (c == null)
			{
				p[i, j] = true;
			}
			else
			{
				if (c.isWhite != isWhite)
				{
					p[i, j] = true;
				}
				break;
			}
		}

		//down right
		for (int i = CurrentX - 1, j = CurrentY - 1; i >= 0; i--, j--)
		{
			if (j < 0) break;

			c = BoardScript.instance.Chessmans[i, j];
			if (c == null)
			{
				p[i, j] = true;
			}
			else
			{
				if (c.isWhite != isWhite)
				{
					p[i, j] = true;
				}
				break;
			}
		}

		return p;
	}
}
