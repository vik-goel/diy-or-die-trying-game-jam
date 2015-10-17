using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

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

		for (int x = 0; x < mazeWidth; x++) {
			for (int y = 0; y < mazeHeight; y++) {
				Cell cell = cells[x, y] = new Cell();
				cell.left = cell.top = true;
				cell.x = x;
				cell.y = y;
			}
		}

		Cell begin = cells [mazeWidth / 2, mazeHeight / 2];
		begin.visited = true;

		while (begin != null) {
			hunt (begin);
			begin = kill ();
		}

		//generate the walls
		for (int x = 0; x < mazeWidth; x++) {
			for (int y = 0; y < mazeHeight; y++) {
				addCellToMaze(x, y, cells[x, y]);
			}
		}

		createWall (-wallLength, wallLength * (mazeHeight - 2) * 0.5f, wallThickness, wallLength * mazeHeight);
		createWall (wallLength * (mazeWidth - 2) * 0.5f, -wallLength, wallLength * mazeWidth, wallThickness);
	}

	void hunt(Cell start) {
		Assert.IsTrue(start.visited);

		List<Cell> options = new List<Cell>();

		if(start.y > 0 && start.top && !cells[start.x, start.y - 1].visited) {
			options.Add(cells[start.x, start.y - 1]);
		}
		if(start.x > 0 && cells[start.x - 1, start.y].left  && !cells[start.x - 1, start.y].visited) {
			options.Add(cells[start.x - 1, start.y]);
		}
		if(start.x + 1 < mazeWidth && start.left  && !cells[start.x + 1, start.y].visited) {
			options.Add (cells[start.x + 1, start.y]);
		}
		if(start.y + 1 < mazeHeight && cells[start.x, start.y + 1].top  && !cells[start.x, start.y + 1].visited) {
			options.Add (cells[start.x, start.y + 1]);
		}

		int numOptions = options.Count;

		if (numOptions > 0) {
			int choice = randInt (0, numOptions);
			Cell next = options [choice];

			Assert.IsFalse(next.visited);
			next.visited = true;

			int dx = next.x - start.x;
			int dy = next.y - start.y;

			if (dx == -1) {
				Assert.IsTrue(cells [start.x - 1, start.y].left);
				cells [start.x - 1, start.y].left = false;
			} else if (dx == 1) {
				Assert.IsTrue(start.left);
				start.left = false;
			} else if (dy == -1) {
				Assert.IsTrue(cells [start.x, start.y - 1].top);
				cells [start.x, start.y - 1].top = false;
			} else if (dy == 1) {
				Assert.IsTrue (start.top);
				start.top = false;
			}

			hunt (next);
		}
	}

	Cell kill() {
		for (int x = 0; x < mazeWidth; x++) {
			for (int y = 0; y < mazeHeight; y++) {
				Cell cell = cells [x, y];

				if (!cell.visited) {
					cell.visited = true;

					int[] order = new int[4];

					for(int i = 0; i < 4; i++) {
						while(true) {
							order[i] = randInt (0, 4);
							bool validInt = true;

							for(int j = 0; j < i; j++) {
								if(order[j] == order[i]) {
									validInt = false;
									break;
								}
							}

							if(validInt) {
								break;
							}
						}
					}

					for(int i = 0; i < 4; i++) {
						if(order[i] == 0 && y > 0 && cells[cell.x, cell.y - 1].visited) {
							Assert.IsTrue(cells[cell.x, cell.y - 1].top);
							cells[cell.x, cell.y - 1].top = false;
							return cells[cell.x, cell.y - 1];
						}
						if(order[i] == 1 && y < mazeHeight - 1 && cells[cell.x, cell.y + 1].visited) {
							Assert.IsTrue(cell.top);
							cell.top = false;
							return cell;
						}
						if(order[i] == 2 && x > 0 && cells[cell.x - 1, cell.y].visited) {
							Assert.IsTrue(cells[cell.x - 1, cell.y].left);
							cells[cell.x - 1, cell.y].left = false;
							return cells[cell.x - 1, cell.y];
						}
						if(order[i] == 3 && x < mazeWidth - 1 && cells[cell.x + 1, cell.y].visited) {
							Assert.IsTrue(cell.left);
							cell.left = false;
							return cell;
						}
					}
				


					cell.visited = false;
				}
			}
		}

		return null;
	}

	Cell getRandomYCell(int y) {
		int x = randInt (0, mazeWidth);
		return cells [x, y];
	}

	void addCellToMaze(int gridX, int gridY, Cell cell) {
		float x = gridX * wallLength - wallThickness * 0.5f;
		float y = gridY * wallLength - wallThickness * 0.5f;

		if (cell.left) {
			createWall (x, y - wallLength * 0.5f, wallThickness, wallLength);
		}
		if (cell.top) {
			createWall (x - wallLength * 0.5f, y, wallLength, wallThickness);
		}
	}

	void createWall(float x, float y, float width, float height) {
		GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wall.transform.Translate(new Vector3(x, 0, y));
		wall.transform.localScale = new Vector3(width, wallHeight, height);

		//wall.AddComponent<Rigidbody>();
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
	public bool visited;
	public int x, y;
}
