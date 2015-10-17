using UnityEngine;
using System.Collections;

public class GenerateMaze : MonoBehaviour {

	public int mazeWidth = 10;
	public int mazeHeight = 10;

	public float wallLength;
	public float wallThickness;
	public float wallHeight;

	Cell[,] cells;

	// Use this for initialization
	void Start () {
		cells = new Cell[mazeWidth, mazeHeight];

		//Start with all of the walls existing
		for (int x = 0; x < mazeWidth; x++) {
			for (int y = 0; y < mazeHeight; y++) {
				Cell cell = cells[x, y] = new Cell();
				cell.left = cell.top = true;
			}
		}


		//Remove a bunch of walls at the start to speed things up
		for (int i = 0; i < 20; i++) {
			removeWall();
		}

		//Keep removing walls until a path exists from the start to the exist
		while (true) {
			removeWall();

			if(pathExists()) {
				break;
			}
		}

		//generate the walls
		for (int x = 0; x < mazeWidth; x++) {
			for (int y = 0; y < mazeHeight; y++) {
				addCellToMaze(x, y, cells[x, y]);
			}
		}
	}

	void addCellToMaze(int gridX, int gridY, Cell cell) {
		float x = gridX * wallLength - wallThickness * 0.5f;
		float y = gridY * wallLength - wallThickness * 0.5f;

		if (cell.left) {
			createWall (x, y, wallThickness, wallLength);
		}
		if (cell.top) {
			createWall (x, y, wallLength, wallThickness);
		}
	}

	void createWall(float x, float y, float width, float height) {
		GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wall.transform.Translate(new Vector3(x, 0, y));
		wall.transform.localScale = new Vector3(width, wallHeight, height);

		//wall.AddComponent<Rigidbody>();
	}

	bool pathExists() {
		//TODO: Actually implement this check
		return true;
	}

	void removeWall() {
		while (true) {
			int cellX = randInt(0, mazeWidth);
			int cellY = randInt(0, mazeHeight);
			int wallIndex = randInt(0, 2);

			Cell cell = cells[cellX, cellY];
			bool wallExists = true;

			switch(wallIndex) {
			case 0:
				wallExists = cell.left;
				cell.left = false;
				break;
			case 1:
				wallExists = cell.top;
				cell.top = false;
				break;
			}

			if(wallExists) {
				break;
			}
		}
	}

	//Inclusive of min, exclusive of max
	int randInt(int min, int max) {
		return (int)Mathf.Floor(Random.Range (min, max));
	}

	// Update is called once per frame
	void Update () {
	
	}
}

class Cell {
	//True means that there is a wall there
	public bool left, top;
}
