using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman
{
	public override bool[,] PossibleMove()
	{
		bool[,] p = new bool[8, 8];

		Chessman c,c2;

		if (isWhite)
		{
			//move to left
			if (CurrentX != 0 && CurrentY != 7)
			{
				c = BoardScript.instance.Chessmans[CurrentX - 1, CurrentY + 1];
				if(c != null && !c.isWhite)
				{
					p[CurrentX - 1, CurrentY + 1] = true;
				}
			}
			//move to right
			if (CurrentX != 7 && CurrentY != 7)
			{
				c = BoardScript.instance.Chessmans[CurrentX + 1, CurrentY + 1];
				if(c != null && !c.isWhite)
				{
					p[CurrentX + 1, CurrentY + 1] = true;
				}
			}
			//move up 
			if(CurrentY != 7)
			{
				c = BoardScript.instance.Chessmans[CurrentX , CurrentY + 1];
				if(c == null)
				{
					p[CurrentX, CurrentY + 1] = true;
				}
			}
				//move up first move
			if (CurrentY == 1)
			{
				c = BoardScript.instance.Chessmans[CurrentX, CurrentY + 1];
				c2 = BoardScript.instance.Chessmans[CurrentX, CurrentY + 2];

				if(c == null && c2 == null)
				{
					p[CurrentX, CurrentY + 2] = true;
				}
			}
		}
		else { 
			//move to left
			if (CurrentX != 0 && CurrentY != 0)
			{
				c = BoardScript.instance.Chessmans[CurrentX - 1, CurrentY - 1];
				if (c != null && c.isWhite)
				{
					p[CurrentX - 1, CurrentY - 1] = true;
				}
			}
			//move to right
			if (CurrentX != 7 && CurrentY != 7)
			{
				c = BoardScript.instance.Chessmans[CurrentX + 1, CurrentY - 1];
				if (c != null && c.isWhite)
				{
					p[CurrentX + 1, CurrentY - 1] = true;
				}
			}
			//move up 
			if (CurrentY != 0)
			{
				c = BoardScript.instance.Chessmans[CurrentX, CurrentY - 1];
				if (c == null)
				{
					p[CurrentX, CurrentY - 1] = true;
				}
			}
			//move up first move
			if (CurrentY == 6)
			{
				c = BoardScript.instance.Chessmans[CurrentX, CurrentY - 1];
				c2 = BoardScript.instance.Chessmans[CurrentX, CurrentY - 2];

				if (c == null && c2 == null)
				{
					p[CurrentX, CurrentY - 2] = true;
				}
			}
		}

		return p;
	}
}
