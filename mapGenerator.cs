using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class mapGenerator : MonoBehaviour {

	public string filePath;
	public float cellSize;
	List<GameObject[]> board;
	public float topLeftX;
	public float topLeftY;

	public GameObject wall;
	public GameObject emptyCell;
	// Use this for initialization
	void Start () {
		StreamReader inp_strm = new StreamReader (filePath);
		board = new List<GameObject[]>();
		int boardHeight = 0;
		while (!inp_strm.EndOfStream) {
			string line = inp_strm.ReadLine ();
			board.Add (new GameObject[line.Length]);
			for (int i = 0; i < line.Length; ++i) {
				char c = line [i];
				if (c == 'W') {
					board [boardHeight] [i] = Instantiate (wall);
				}
				else if (c == '.') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				}
				else if (c == 'I') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				}
				else if (c == 'P') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				}
				else if (c == 'B') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				}
				else if (c == 'C') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				}
				else if (c == 'M') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				}
				board [boardHeight] [i].transform.position = new Vector3 (topLeftX + cellSize * i, topLeftY - cellSize * boardHeight);
			}
			boardHeight++;
		}
		inp_strm.Close();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
