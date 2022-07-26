using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman
{
	public override bool[,] PossibleMove()
	{
		bool[,] p = new bool[8, 8];
		Chessman c;

		//up
		for (int i = CurrentY + 1; i < 8; i++)
		{
			c = BoardScript.instance.Chessmans[CurrentX, i];
			if (c == null )
			{
					p[CurrentX, i] = true;
			}
			else {
				if (c.isWhite != isWhite)
				{
					p[CurrentX, i] = true;
				}
				break;
			}
				
		}

		//down
		for (int i = CurrentY - 1; i >= 0; i--)
		{
			c = BoardScript.instance.Chessmans[CurrentX, i];
			if (c == null)
			{
				p[CurrentX, i] = true;
			}
			else
			{
				if (c.isWhite != isWhite)
				{
					p[CurrentX, i] = true;
				}
				break;
			}

		}

		//right
		for (int i = CurrentX + 1; i < 8; i++)
		{
			c = BoardScript.instance.Chessmans[i, CurrentY];
			if (c == null)
			{
				p[i, CurrentY] = true;
			}
			else
			{
				if (c.isWhite != isWhite)
				{
					p[i, CurrentY] = true;
				}
				break;
			}

		}

		//left
		for (int i = CurrentX - 1; i >= 0; i--)
		{
			c = BoardScript.instance.Chessmans[i, CurrentY];
			if (c == null)
			{
					p[i, CurrentY] = true;
			}

			else
			{
				if (c.isWhite != isWhite)
				{
					p[i, CurrentY] = true;
				}
				break;
			}

		}

		return p;
	}
}
