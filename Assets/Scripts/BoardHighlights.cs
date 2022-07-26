using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour
{
	public static BoardHighlights instance { set; get; }

	public GameObject highlightPrefab;
	private List<GameObject> highlights;

	private void Start()
	{
		instance = this;
		highlights = new List<GameObject>();
	}

	private GameObject GetHighlightsObject()
	{
		GameObject go = highlights.Find(g => !g.activeSelf);
		if(go == null)
		{
			go = Instantiate(highlightPrefab);
			highlights.Add(go);
			
		}
		return go;
	}

	public void HighlightsAllowedMoves(bool[,] moves)
	{
		for(int i = 0; i < 8; i++)
		{
			for(int j = 0; j < 8; j++)
			{
				if(moves[i, j])
				{
					GameObject go = GetHighlightsObject();
					go.SetActive(true);
					go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
				}
			}
		}
	}

	public void HideHighlights()
	{
		foreach(GameObject go in highlights)
		{
			go.SetActive(false);
		}
	}

}
