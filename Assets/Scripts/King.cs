using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman
{
	public override bool[,] PossibleMove()
	{
		bool[,] p = new bool[8, 8];
		Chessman c;
		int i, j;

		//up
		i = CurrentX - 1;
		j = CurrentY + 1;
		if(CurrentY != 7)
		{
			for (int k = 0; k < 3; k++)
			{
				if (i >= 0 || i < 8)
				{
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
					}
				}
				i++;
			}
		}

		//down
		i = CurrentX - 1;
		j = CurrentY - 1;
		if (CurrentY != 0)
		{
			for (int k = 0; k < 3; k++)
			{
				if (i >= 0 || i < 8)
				{
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
					}
				}
				i++;
			}
		}
		
		//left
		if(CurrentX != 0)
		{
			c = BoardScript.instance.Chessmans[CurrentX - 1, CurrentY];
			if(c == null)
			{
				p[CurrentX - 1, CurrentY] = true;
			}
			else
			{
				if (c.isWhite != isWhite)
				{
					p[CurrentX - 1, CurrentY] = true;
				}
			}
		}

		//right
		if (CurrentX != 7)
		{
			c = BoardScript.instance.Chessmans[CurrentX + 1, CurrentY];
			if (c == null)
			{
				p[CurrentX + 1, CurrentY] = true;
			}
			else
			{
				if (c.isWhite != isWhite)
				{
					p[CurrentX + 1, CurrentY] = true;
				}
			}
		}

		return p;
	}
}
