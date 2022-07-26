using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
	public static BoardScript instance { set; get; }
	private bool[,] allowedMoves { set; get; }

	public Chessman[,] Chessmans { get; set; }
	public Chessman selectedChessman;

    private const float TILE_SIZE = 1f;
    private const float TILE_OFFSET = 0.5f;

	private int selectionX = -1;
	private int selectionY = -1;

	public bool isWhiteTurn = true;

	public List<GameObject> chessmanPrefabs;
	private List<GameObject> activeChessman;

	private void Start()
	{
		instance = this;
		SpawnAllChessmans();
	}

	private void Update()
	{
		//Debug.Log(selectionX + "," + selectionY);
		if(selectedChessman != null)
		{
			//Debug.Log(selectedChessman.name);
		}

		UpdateSelection();
		DrawChessBoard();

		if (selectionX >= 0 && selectionY >= 0)
		{
			if (Input.GetMouseButtonDown(0))
			{
				if(selectedChessman == null)
				{
					SelectChessman(selectionX, selectionY);
				}
				else
				{
					MoveChessman(selectionX, selectionY);
				}
			}
		}
	}

	private void SelectChessman(int x, int y)
	{
		
		if (Chessmans[x, y] == null)
			return;

		if (Chessmans[x, y].isWhite != isWhiteTurn)
			return;

		bool hasAttLeastOneMove = false;
		allowedMoves = Chessmans[x, y].PossibleMove();

		for(int i = 0; i < 8; i++)
		{
			for(int j = 0; j < 8; j++)
			{
				if (allowedMoves[i, j])
				{
					hasAttLeastOneMove = true;
					break;
				}
			}
		}

		if (hasAttLeastOneMove) 
		{
			selectedChessman = Chessmans[x, y];
			BoardHighlights.instance.HighlightsAllowedMoves(allowedMoves);
		}
	}

	private void MoveChessman(int x, int y)
	{
		if (selectedChessman.PossibleMove()[x, y])
		{
			Chessman c = Chessmans[selectionX, selectionY];
			if(c != null && c.isWhite != isWhiteTurn)
			{
				if(c.GetType() == typeof(King))
				{
					//EndGame();
					return;
				}

				activeChessman.Remove(c.gameObject);
				Destroy(c.gameObject);
			}
			Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;

			if(selectedChessman.GetType() == typeof(Pawn)){
				selectedChessman.transform.position = GetTileCenter(x, y) + Vector3.up * 0.2f;
			}
			else
			{
				selectedChessman.transform.position = GetTileCenter(x, y);
			}
			selectedChessman.SetPosition(x, y);
			Chessmans[x, y] = selectedChessman;
			isWhiteTurn = !isWhiteTurn;
		}
		if(!selectedChessman.PossibleMove()[x, y]) 
		{
			Chessman c = Chessmans[selectionX, selectionY];
			if (c != null && c.isWhite == isWhiteTurn)
			{
				//Debug.Log("hello");
				selectedChessman = null;
				BoardHighlights.instance.HideHighlights();
				SelectChessman(selectionX, selectionY);
				return;
			}
		}

		BoardHighlights.instance.HideHighlights();
		selectedChessman = null;

	}

	private void DrawChessBoard()
	{
		Vector3 boardLength = Vector3.forward * 8;
		Vector3 boardWidth = Vector3.right * 8;

        for (int i = 0; i <= 8; i++)
		{
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine(start, boardWidth + start);

			for (int j = 0; j <= 8; j++) {

				start = Vector3.right * j;
				Debug.DrawLine(start, boardLength + start);

			}
		}
		if(selectionX >= 0 && selectionY >= 0)
		{
			Debug.DrawLine(
				Vector3.forward * selectionY + Vector3.right * selectionX,
				Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

			Debug.DrawLine(
				Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
				Vector3.forward * selectionY  + Vector3.right * (selectionX + 1));
		}
	}

	private void UpdateSelection()
	{
		if (!Camera.main)
			return;

		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25f, LayerMask.GetMask("ChessPlane"))) {
			
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		}
		else
		{
			selectionX = -1;
			selectionY = -1;
		}
	}

	private void SpawnChessman(int index, int x, int y)
	{
		GameObject newBorn;
		if(index == 11 || index == 5)
		{
			newBorn = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y) + Vector3.up * 0.2f, rotationNedded(index)) as GameObject;
		}
		else
		{
			newBorn = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y), rotationNedded(index)) as GameObject;
		}
		newBorn.transform.SetParent(transform);
		Chessmans[x, y] = newBorn.GetComponent<Chessman>();
		Chessmans[x, y].SetPosition(x, y);
		activeChessman.Add(newBorn);
	}

	private void SpawnAllChessmans() { 
	
		Chessmans = new Chessman[8, 8];
		activeChessman = new List<GameObject>();

		//Spawning white Pieces

		// spawn King
		SpawnChessman(6, 4, 0);

		// spawn Queen
		SpawnChessman(7, 3, 0);
		
		// spawn 2 Rooks
		SpawnChessman(8, 0, 0);
		SpawnChessman(8, 7, 0);

		// spawn 2 Bishops
		SpawnChessman(9, 2, 0);
		SpawnChessman(9, 5, 0);

		// spawn 2 Knights
		SpawnChessman(10, 1, 0);
		SpawnChessman(10, 6, 0);

		

		//Spawning Black Pieces

		// spawn King
		SpawnChessman(0, 4, 7);

		// spawn Queen
		SpawnChessman(1, 3, 7);

		// spawn 2 Rooks
		SpawnChessman(2, 0, 7);
		SpawnChessman(2, 7, 7);

		// spawn 2 Bishops
		SpawnChessman(3, 2, 7);
		SpawnChessman(3, 5, 7);

		// spawn 2 Knights
		SpawnChessman(4, 1, 7);
		SpawnChessman(4, 6, 7);


		//spawn Pawns
		for(int i = 0; i < 8; i++)
		{
			SpawnChessman(11, i, 1);
		}

		//spawn Pawns
		for (int i = 0; i < 8; i++)
		{
			SpawnChessman(5, i, 6);
		}
	}
		
	private Vector3 GetTileCenter(int x, int y)
	{
		Vector3 Origin = Vector3.zero;
		Origin.x += TILE_SIZE * x + TILE_OFFSET;
		Origin.z += TILE_SIZE * y + TILE_OFFSET;
		return Origin;
		
	}

	private Quaternion rotationNedded(int index)
	{
		if (index == 10)
			return Quaternion.Euler(-90, -90, 0);
		else
			return Quaternion.Euler(-90, 90, 0);
	}
}
