using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;				// For the scoreboard UI text

public class Tile
{
	public GameObject tileObj;
	public string type;
	public Tile(GameObject obj, string t)
	{
		tileObj = obj;
		type = t;
	}
}

public class CreateGame : MonoBehaviour 
{
	GameObject tile1 = null;							// Tile pressed on
	GameObject tile2 = null;							// Tile released

	public GameObject[] tile;
	List<GameObject> tileBank = new List<GameObject>();	// Create Game Object List

	static int rows = 8;								// Dimensions of board (rows x cols)
	static int cols = 5;
	bool renewBoard = false;							// Set renew board check to false
	Tile[,] tiles = new Tile[cols, rows];				// Two dimensional array to keep track of tiles

	int Score;											// Score for matches
	public Text ScoreText;								// String value for text
	bool firstScore = false;

	// Randomly pull tile out of list and swap them around
	void ShuffleList() {
		System.Random rand = new System.Random();
		int count = tileBank.Count;

		while (count > 1) {
			count--;
			int next = rand.Next(count + 1);
			GameObject val = tileBank[next];
			tileBank[next] = tileBank[count];
			tileBank[count] = val;
		}
	}

	// Initialization
	void Start () {
		Score = 0;

		// Generate all map tiles and fill up list
		int numCopies = (rows * cols)/3;
		for (int i = 0; i < numCopies; i++)
		{
			for(int j = 0; j < tile.Length; j++)
			{
				GameObject o = (GameObject) Instantiate(tile[j], 	// Create by instantiating prefab (re-usable GameObject)
									new Vector3(-10, -10, 0), 		// Set them elsewhere so not visible to camera
									tile[j].transform.rotation);	// Keep original rotation
				o.SetActive(false);									// Set tile to false
				tileBank.Add(o);									// Add to our list
			}
		}

		ShuffleList();	// Shuffle created list

		// Initialize tile grid
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				Vector3 tilePos = new Vector3(c, r, 0);

				// Loop through tileBank
				for(int n = 0; n < tileBank.Count; n++)
				{
					GameObject o = tileBank[n];

					// If the tile isn't already used(active), use it
					if(!o.activeSelf)
					{
						o.transform.position = new Vector3(tilePos.x,	// Moves tile into position
														   tilePos.y,
														   tilePos.z);
						o.SetActive(true);								// Set tile to active
						tiles[c, r] = new Tile(o, o.name);				// Putting tile back into 2D array
						n = tileBank.Count + 1;							// Update tile count
					}
				}
			}
		}

		SetScoreText();			// For setting the text UI for score
	}
	
	// Update is called once per frame
	void Update () {
		CheckGrid();

		// Register click
		if (Input.GetMouseButtonDown(0))
		{
			// Shoot ray from mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1000);

			// If hit, grab hold of that object
			if (hit)
			{
				tile1 = hit.collider.gameObject;
			}

			firstScore = true;
		}else if (Input.GetMouseButtonUp(0) && tile1){		// Finger lifted is detected after initial tile has been choosen
			// Shoot ray from mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1000);

			// If hit, grab hold of that object
			if (hit)
			{
				tile2 = hit.collider.gameObject;
			}

			// Swap tile positions if both tile positions obtained
			if (tile1 && tile2)
			{
				// Check horzizontal and vertical distance to see if next to each other
				int horzDist = (int)Mathf.Abs(tile1.transform.position.x - tile2.transform.position.x);
				int vertDist = (int)Mathf.Abs(tile1.transform.position.y - tile2.transform.position.y);

				// As long as one is true
				if(horzDist == 1 ^ vertDist == 1)
				{
					// Update location in matrix
					Tile temp = tiles[(int)tile1.transform.position.x, (int)tile1.transform.position.y];
					tiles[(int)tile1.transform.position.x, (int)tile1.transform.position.y] = 
						tiles[(int)tile2.transform.position.x, (int)tile2.transform.position.y];
					tiles[(int)tile2.transform.position.x, (int)tile2.transform.position.y] = temp;

					// Swap tile positions
					Vector3 tempPos = tile1.transform.position;
					tile1.transform.position = tile2.transform.position;
					tile2.transform.position = tempPos;

					// Reset touched tiles
					tile1 = null;
					tile2 = null;
				} else {
					GetComponent<AudioSource>().Play();		// Play sound to indicate error
				}
			}
		}
	}

	// Check grid for matches
	void CheckGrid()
	{
		int counter = 1;

		// Loop through grid, checking all columns
		for (int r = 0; r < rows; r++)
		{
			counter = 1;
			for (int c = 1; c < cols; c++)
			{
				// If the tile exists
				if (tiles[c,r] != null && tiles[c-1,r] != null)
				{
					// Check neighboring tile by comparing types
					if (tiles[c,r].type == tiles[c-1,r].type)
					{
						counter++;
					} else {
						counter = 1;		// Reset counter
					}

					// Matched 3 tiles, remove them
					if (counter == 3)
					{
						if (tiles[c,r] != null)
							tiles[c,r].tileObj.SetActive(false);	// Turn off tile object

						if (tiles[c-1,r] != null)
							tiles[c-1,r].tileObj.SetActive(false);

						if (tiles[c-2,r] != null)
							tiles[c-2,r].tileObj.SetActive(false);

						// Sets position of tile in internal matrix to false
						tiles[c,r] = null;
						tiles[c-1,r] = null;
						tiles[c-2,r] = null;
						renewBoard = true;

						if (firstScore) {
							Score = Score + 1;
						}
						SetScoreText();
					}
				}
			}
		}

		// Loop through grid, checking all rows
		for (int c = 0; c < cols; c++)
		{
			counter = 1;
			for (int r = 1; r < rows; r++)
			{
				// If the tile exists
				if (tiles[c,r] != null && tiles[c,r-1] != null)
				{
					// Check neighboring tile by comparing types
					if (tiles[c,r].type == tiles[c,r-1].type)
					{
						counter++;
					} else {
						counter = 1;		// Reset counter
					}

					// Matched 3 tiles, remove them
					if (counter == 3)
					{
						if (tiles[c,r] != null)
							tiles[c,r].tileObj.SetActive(false);	// Turn off tile object

						if (tiles[c,r-1] != null)
							tiles[c,r-1].tileObj.SetActive(false);

						if (tiles[c,r-2] != null)
							tiles[c,r-2].tileObj.SetActive(false);

						// Sets position of tile in internal matrix to false
						tiles[c,r] = null;
						tiles[c,r-1] = null;
						tiles[c,r-2] = null;
						renewBoard = true;

						if (firstScore) {
							Score = Score + 1;
						}
						SetScoreText();
					}
				}
			}
		}

		// If tiles removed, drop down tiles vertically to fill board again
		if (renewBoard)
		{
			RenewGrid();
			renewBoard = false;
		}
	}

	// Update grid once match successful
	void RenewGrid()
	{
		bool anyMoved = false;
		ShuffleList();
		for (int r = 1; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				if(r == rows - 1 && tiles[c,r] == null)
				{
					Vector3 tilePos = new Vector3(c, r, 0);
					for (int n = 0; n < tileBank.Count; n++)
					{
						GameObject o = tileBank[n];
						if (!o.activeSelf)
						{
							o.transform.position = new Vector3(tilePos.x, tilePos.y, tilePos.z);
							o.SetActive(true);
							tiles[c,r] = new Tile(o, o.name);
							n = tileBank.Count + 1;
						}
					}
				}

				if (tiles[c,r] != null)
				{
					if (tiles[c,r-1] == null)
					{
						tiles[c,r-1] = tiles[c,r];
						tiles[c,r-1].tileObj.transform.position = new Vector3(c, r-1, 0);
						tiles[c,r] = null;
						anyMoved = true;
					}
				}
			}
		}

		// Keep invoking RenewGrid until grid filled
		if (anyMoved)
		{
			Invoke("RenewGrid", 0.5f);
		}
	}

	// Function for setting the score text UI
	void SetScoreText()
	{
		ScoreText.text = "Score: " + Score.ToString();
	}

}