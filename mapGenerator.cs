using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class mapGenerator : MonoBehaviour {

	public string filePath;
	public float cellSize;
	List<GameObject[]> board;
	List<string[]> tileTypes;
	public float topLeftX;
	public float topLeftY;

	public GameObject wall;
	public GameObject emptyCell;
	public GameObject pellet;

	public GameObject pacman;
	public GameObject blinky;
	public GameObject inky;
	public GameObject pinky;
	public GameObject clyde;

	private List<string> wallTypes;
	// Use this for initialization
	void Start () {
		StreamReader inp_strm = new StreamReader (filePath);
		board = new List<GameObject[]>();
		tileTypes = new List<string[]> ();
		wallTypes = new List<string>(13);
		wallTypes.Add ("VL");
		wallTypes.Add ("VR");
		wallTypes.Add ("HT");
		wallTypes.Add ("HB");
		wallTypes.Add ("W");
		int boardHeight = 0;
		while (!inp_strm.EndOfStream) {
			string line = inp_strm.ReadLine ();
			board.Add (new GameObject[line.Length]);
			tileTypes.Add (new string[line.Length]);
			for (int i = 0; i < line.Length; ++i) {
				char c = line [i];
				if (c == 'W') {
					board [boardHeight] [i] = Instantiate (wall);
				} else if (c == 'G') {
					board [boardHeight] [i] = Instantiate (wall);
					CellScript cellScript = board [boardHeight] [i].GetComponent<CellScript> ();
					cellScript.SetSprite (14);
				} else if (c == '.') {
					board [boardHeight] [i] = Instantiate (emptyCell);
					GameObject pelletSpawned = Instantiate (pellet);
					pelletSpawned.transform.position = new Vector3 (topLeftX + cellSize * i, topLeftY - cellSize * boardHeight);
				} else if (c == ' ') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				} else if (c == 'I') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				} else if (c == 'P') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				} else if (c == 'B') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				} else if (c == 'C') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				} else if (c == 'M') {
					board [boardHeight] [i] = Instantiate (emptyCell);
				} 
				board [boardHeight] [i].transform.position = new Vector3 (topLeftX + cellSize * i, topLeftY - cellSize * boardHeight);
				tileTypes [boardHeight] [i] = c == 'W' || c == 'G' ? "W" : ".";
			}
			boardHeight++;
		}
		inp_strm.Close();
		OrientWalls ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OrientWalls(){
		List<int> uncheckedX = new List<int> ();
		List<int> uncheckedY = new List<int> ();
		for (int i = 0; i < board.Count; ++i) {
			for (int j = 0; j < board [i].Length; ++j) {

				if (tileTypes [i] [j] == "W" && board [i] [j].tag == "wall") {

					CellScript cellScript = board [i] [j].GetComponent<CellScript> ();

					if (i > 0 && i < board.Count - 1 && wallTypes.Contains (tileTypes [i - 1] [j]) && wallTypes.Contains (tileTypes [i + 1] [j])) {
						if (j > 0 && !wallTypes.Contains (tileTypes [i] [j - 1])) {
							//Set tile to 14
							cellScript.SetSprite (25);
							tileTypes [i] [j] = "VR";
							continue;
						}
						if (j < board [i].Length - 1 && !wallTypes.Contains (tileTypes [i] [j + 1])) {
							//Set tile to 20
							cellScript.SetSprite (24);
							tileTypes [i] [j] = "VL";
							continue;
						}
					} if (j > 0 && j < board [i].Length - 1 && wallTypes.Contains (tileTypes [i] [j - 1]) && wallTypes.Contains (tileTypes [i] [j + 1])) {
						if (i > 0 && !wallTypes.Contains (tileTypes [i - 1] [j])) {
							//Set tile to 25
							cellScript.SetSprite (14);
							tileTypes [i] [j] = "HB";
							continue;
						}
						if (i < board.Count - 1 && !wallTypes.Contains (tileTypes [i + 1] [j])) {
							//Set tile to 24
							cellScript.SetSprite (20);
							tileTypes [i] [j] = "HT";
							continue;
						}
					}
					uncheckedY.Add (i);
					uncheckedX.Add (j);


					/*if(i > 0 && j > 0 && tileTypes[i - 1][j] == "W" && tileTypes[i][j - 1] == "W"){
						if (i < board.Count - 1 && j < board [i].Length - 1 && tileTypes [i + 1] [j] != "W" && tileTypes [i] [j + 1] != "W") {
							//Set tile to 40
							cellScript.SetSprite(40);
							continue;
						} else {
							//Set tile to 37
							cellScript.SetSprite(37);
							continue;
						}
					}if(i > 0 && j < board[i].Length - 1 && tileTypes[i - 1][j] == "W" && tileTypes[i][j + 1] == "W"){
						if (i < board.Count - 1 && j > 0 && tileTypes [i + 1] [j] != "W" && tileTypes [i] [j - 1] != "W") {
							//Set tile to 38
							cellScript.SetSprite(41);
							continue;
						} else {
							//Set tile to 35
							cellScript.SetSprite(36);
							continue;
						}
					}if(i < board.Count - 1 && j > 0 && tileTypes[i + 1][j] == "W" && tileTypes[i][j - 1] == "W"){
						if (i > 0 && j < board[i].Length && tileTypes [i - 1] [j] != "W" && tileTypes [i] [j + 1] != "W") {
							//Set tile to 41
							cellScript.SetSprite(38);
							continue;
						} else {
							//Set tile to 36
							cellScript.SetSprite(35);
							continue;
						}
					}if(i < board.Count - 1 && j < board[i].Length - 1 && tileTypes[i + 1][j] == "W" && tileTypes[i][j + 1] == "W"){
						if (i > 0 && j > 0 && tileTypes [i - 1] [j] != "W" && tileTypes [i] [j - 1] != "W") {
							//Set tile to 39
							cellScript.SetSprite(39);
							continue;
						} else {
							//Set tile to 34
							cellScript.SetSprite(34);
							continue;
						}
					}
					*/

				}

			}
		}
		int index = 0;
		bool changed = false;
		List<string> rightWalls = new List<string> ();
		rightWalls.Add ("HB");
		rightWalls.Add ("HT");
		rightWalls.Add ("SEO");
		rightWalls.Add ("SEI");
		rightWalls.Add ("NEO");
		rightWalls.Add ("NEI");
		List<string> leftWalls = new List<string> ();
		leftWalls.Add ("HB");
		leftWalls.Add ("HT");
		leftWalls.Add ("SWO");
		leftWalls.Add ("SWI");
		leftWalls.Add ("NWO");
		leftWalls.Add ("NWI");
		List<string> upWalls = new List<string> ();
		upWalls.Add ("VL");
		upWalls.Add ("VR");
		upWalls.Add ("SWO");
		upWalls.Add ("SWI");
		upWalls.Add ("SEO");
		upWalls.Add ("SEI");
		List<string> downWalls = new List<string> ();
		downWalls.Add ("VL");
		downWalls.Add ("VR");
		downWalls.Add ("NWO");
		downWalls.Add ("NWI");
		downWalls.Add ("NEO");
		downWalls.Add ("NEI");
		while (uncheckedX.Count > 0 && index < uncheckedX.Count) {
			int j = uncheckedX [index];
			int i = uncheckedY [index];
			CellScript cellScript = board [i] [j].GetComponent<CellScript> ();
			if (i > 0 && j > 0 && ((upWalls.Contains (tileTypes [i - 1] [j]) && leftWalls.Contains (tileTypes [i] [j - 1]))
				|| (upWalls.Contains (tileTypes [i - 1] [j]) && tileTypes [i] [j - 1] == "W") 
				|| (tileTypes [i - 1] [j] == "W" && leftWalls.Contains (tileTypes [i] [j - 1])))) {
				if (i < board.Count - 1 && j < board [i].Length - 1 && tileTypes [i + 1] [j] == "." && tileTypes [i] [j + 1] == ".") {
					//Set tile to 40
					cellScript.SetSprite (40);
					tileTypes [i] [j] = "NEI";

				} else {
					//Set tile to 37
					cellScript.SetSprite (37);
					tileTypes [i] [j] = "NEO";
				}
				changed = true;
				uncheckedX.RemoveAt (index);
				uncheckedY.RemoveAt (index);
			} else if (i > 0 && j < board [i].Length - 1 && ((upWalls.Contains (tileTypes [i - 1] [j]) && rightWalls.Contains (tileTypes [i] [j + 1]))
				|| (upWalls.Contains (tileTypes [i - 1] [j]) && tileTypes [i] [j + 1] == "W") 
				|| (tileTypes [i - 1] [j] == "W" && rightWalls.Contains (tileTypes [i] [j + 1])))) {
				if (i < board.Count - 1 && j > 0 && tileTypes [i + 1] [j] == "." && tileTypes [i] [j - 1] == ".") {
					//Set tile to 41
					cellScript.SetSprite (41);
					tileTypes [i] [j] = "NWI";
				} else {
					//Set tile to 36
					cellScript.SetSprite (36);
					tileTypes [i] [j] = "NWO";
				}
				changed = true;
				uncheckedX.RemoveAt (index);
				uncheckedY.RemoveAt (index);
			} else if (i < board.Count - 1 && j > 0 && ((downWalls.Contains (tileTypes [i + 1] [j]) && leftWalls.Contains (tileTypes [i] [j - 1]))
				|| (downWalls.Contains (tileTypes [i + 1] [j]) && tileTypes [i] [j - 1] == "W") 
				|| (tileTypes [i + 1] [j] == "W" && leftWalls.Contains (tileTypes [i] [j - 1])))) {
				if (i > 0 && j < board [i].Length - 1 && tileTypes [i - 1] [j] == "." && tileTypes [i] [j + 1] == ".") {
					//Set tile to 38
					cellScript.SetSprite (38);
					tileTypes [i] [j] = "SEI";
				} else {
					//Set tile to 35
					cellScript.SetSprite (35);
					tileTypes [i] [j] = "SEO";
				}
				changed = true;
				uncheckedX.RemoveAt (index);
				uncheckedY.RemoveAt (index);
			} else if (i < board.Count - 1 && j < board [i].Length - 1 && ((downWalls.Contains (tileTypes [i + 1] [j]) && rightWalls.Contains (tileTypes [i] [j + 1]))
				|| (downWalls.Contains (tileTypes [i + 1] [j]) && tileTypes [i] [j + 1] == "W") 
				|| (tileTypes [i + 1] [j] == "W" && rightWalls.Contains (tileTypes [i] [j + 1])))){
				if (i > 0 && j > 0 && tileTypes [i - 1] [j] == "." && tileTypes [i] [j - 1] == ".") {
					//Set tile to 39
					cellScript.SetSprite (39);
					tileTypes [i] [j] = "SWI";
				} else {
					//Set tile to 34
					cellScript.SetSprite (34);
					tileTypes [i] [j] = "SWO";
				}
				changed = true;
				uncheckedX.RemoveAt (index);
				uncheckedY.RemoveAt (index);
			} else {
				index++;

			}
			if (changed && index == uncheckedX.Count) {
				index = 0;
				changed = false;
			}
		}
	}
}
