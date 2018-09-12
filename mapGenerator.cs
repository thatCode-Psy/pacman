using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class mapGenerator : MonoBehaviour {

	public string filePath;
	public float cellSize;
	List<GameObject[]> board;
	List<char[]> tileTypes;
	public float topLeftX;
	public float topLeftY;

	public GameObject wall;
	public GameObject emptyCell;
	// Use this for initialization
	void Start () {
		StreamReader inp_strm = new StreamReader (filePath);
		board = new List<GameObject[]>();
		tileTypes = new List<char[]> ();
		int boardHeight = 0;
		while (!inp_strm.EndOfStream) {
			string line = inp_strm.ReadLine ();
			board.Add (new GameObject[line.Length]);
			tileTypes.Add (new char[line.Length]);
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
				tileTypes [boardHeight] [i] = c == 'W' || c == 'G' ? 'W' : '.';
			}
			boardHeight++;
		}
		inp_strm.Close();

		for (int i = 0; i < board.Count; ++i) {
			for (int j = 0; j < board [i].Length; ++j) {

				if (tileTypes [i] [j] == 'W' && board [i] [j].tag == "wall") {

					CellScript cellScript = board [i] [j].GetComponent<CellScript> ();

					if(i > 0 && i < board.Count - 1 && tileTypes[i - 1] [j] == 'W' && tileTypes[i + 1] [j] == 'W'){
						if (j > 0 && tileTypes [i] [j - 1] != 'W') {
							//Set tile to 14
							cellScript.SetSprite(25);
							continue;
						}
						if (j < board[i].Length - 1 && tileTypes [i] [j + 1] != 'W') {
							//Set tile to 20
							cellScript.SetSprite(24);
							continue;
						}
					}if(j > 0 && j < board[i].Length - 1 && tileTypes[i] [j - 1] == 'W' && tileTypes[i] [j + 1] == 'W'){
						if (i > 0 && tileTypes [i - 1] [j] != 'W') {
							//Set tile to 25
							cellScript.SetSprite(14);
							continue;
						}
						if (i < board.Count - 1 && tileTypes [i + 1] [j] != 'W') {
							//Set tile to 24
							cellScript.SetSprite(20);
							continue;
						}
					}if(i > 0 && j > 0 && tileTypes[i - 1][j] == 'W' && tileTypes[i][j - 1] == 'W'){
						if (i < board.Count - 1 && j < board [i].Length - 1 && tileTypes [i + 1] [j] != 'W' && tileTypes [i] [j + 1] != 'W') {
							//Set tile to 40
							cellScript.SetSprite(40);
							continue;
						} else {
							//Set tile to 37
							cellScript.SetSprite(37);
							continue;
						}
					}if(i > 0 && j < board[i].Length - 1 && tileTypes[i - 1][j] == 'W' && tileTypes[i][j + 1] == 'W'){
						if (i < board.Count - 1 && j > 0 && tileTypes [i + 1] [j] != 'W' && tileTypes [i] [j - 1] != 'W') {
							//Set tile to 38
							cellScript.SetSprite(41);
							continue;
						} else {
							//Set tile to 35
							cellScript.SetSprite(36);
							continue;
						}
					}if(i < board.Count - 1 && j > 0 && tileTypes[i + 1][j] == 'W' && tileTypes[i][j - 1] == 'W'){
						if (i > 0 && j < board[i].Length && tileTypes [i - 1] [j] != 'W' && tileTypes [i] [j + 1] != 'W') {
							//Set tile to 41
							cellScript.SetSprite(38);
							continue;
						} else {
							//Set tile to 36
							cellScript.SetSprite(35);
							continue;
						}
					}if(i < board.Count - 1 && j < board[i].Length - 1 && tileTypes[i + 1][j] == 'W' && tileTypes[i][j + 1] == 'W'){
						if (i > 0 && j > 0 && tileTypes [i - 1] [j] != 'W' && tileTypes [i] [j - 1] != 'W') {
							//Set tile to 39
							cellScript.SetSprite(39);
							continue;
						} else {
							//Set tile to 34
							cellScript.SetSprite(34);
							continue;
						}
					}
				
				}

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
