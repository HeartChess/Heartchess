using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshGenerator : MonoBehaviour {
	
	public SquareGrid squareGrid;
	List<Vector3> vertices;
	List<int> triangles;
	
	public void GenerateMesh(int[,] map, float squareSize) {
		squareGrid = new SquareGrid(map, squareSize);
		
		vertices = new List<Vector3>();
		triangles = new List<int>();
		
		for(int x = 0; x < squareGrid.squares.GetLength(0); x ++) {
			for(int y = 0; y < squareGrid.squares.GetLength(1); y ++) {
				TraiangulateSquare(squareGrid.squares[x,y]);
			}
		}
		
		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		
		mesh.RecalculateNormals();
	}
	
	void TraiangulateSquare(Square square) {
		switch(square.configuration) {
			case 0:
				break;
			case 1:
				MeshFromPoint(square.centerBottom, square.bottomLeft, square.centerLeft);
				break;
			case 2:
				MeshFromPoint(square.centerRight, square.bottomRight, square.centerBottom);
				break;
			case 4:
				MeshFromPoint(square.centerTop, square.topRight, square.centerRight);
				break;
			case 8:
				MeshFromPoint(square.topLeft, square.centerTop, square.centerLeft);
				break;

			// 2 points
			case 3:
				MeshFromPoint(square.centerRight, square.bottomRight, square.bottomLeft, square.centerLeft);
				break;
			case 6:
				MeshFromPoint(square.centerTop, square.topRight, square.bottomRight, square.centerBottom);
				break;
			case 9:
				MeshFromPoint(square.topLeft, square.centerTop, square.centerBottom, square.bottomLeft);
				break;
			case 12:
				MeshFromPoint(square.topLeft, square.topRight, square.centerRight, square.centerLeft);
				break;
			case 5:
				MeshFromPoint(square.centerTop, square.topRight, square.centerRight, square.centerBottom, square.bottomLeft, square.centerLeft);
				break;
			case 10:
				MeshFromPoint(square.topLeft, square.centerTop, square.centerRight, square.bottomRight, square.centerBottom, square.centerLeft);
				break;
				
			// 3 points

			case 7:
				MeshFromPoint(square.centerTop, square.topRight, square.bottomRight, square.bottomLeft, square.centerLeft);
				break;
			case 11:
				MeshFromPoint(square.topLeft, square.centerTop, square.centerRight, square.bottomRight, square.bottomLeft);
				break;
			case 13:
				MeshFromPoint(square.topLeft, square.topRight, square.centerRight, square.centerBottom, square.bottomLeft);
				break;
			case 14:
				MeshFromPoint(square.topLeft, square.topRight, square.bottomRight, square.centerBottom, square.centerLeft);
				break;
				
			// 4 point
			case 15:
				MeshFromPoint(square.topLeft, square.topRight, square.bottomRight, square.bottomLeft);
				break;
		}
	}
	
	void MeshFromPoint(params Node[] points) {
		AssignVertices(points);
		
		if(points.Length >= 3) 
			CreateTrianges(points[0], points[1], points[2]);
		if(points.Length >= 4)
			CreateTrianges(points[0], points[2], points[3]);
		if(points.Length >= 5)
			CreateTrianges(points[0], points[3], points[4]);
		if(points.Length >= 6)
			CreateTrianges(points[0], points[4], points[5]);
	}
	
	void AssignVertices(Node[] points) {
		for(int i = 0; i < points.Length; i ++) {
			if(points[i].vertexIndex == -1) {
				points[i].vertexIndex = vertices.Count;
				vertices.Add(points[i].position);
			}
		}
	}
	
	void CreateTrianges(Node a, Node b, Node c) {
		triangles.Add(a.vertexIndex);
		triangles.Add(b.vertexIndex);
		triangles.Add(c.vertexIndex);
	}
	
	void OnDrawGizmos () {
		/*
		if(squareGrid != null) {
		
			for(int x = 0; x < squareGrid.squares.GetLength(0); x ++) {
				for(int y = 0; y < squareGrid.squares.GetLength(1); y ++) {
					
					Gizmos.color = (squareGrid.squares[x, y].topLeft.active) ? Color.black : Color.white;
					Gizmos.DrawCube(squareGrid.squares[x, y].topLeft.position, Vector3.one * .4f);
					
					Gizmos.color = (squareGrid.squares[x, y].topRight.active) ? Color.black : Color.white;
					Gizmos.DrawCube(squareGrid.squares[x, y].topRight.position, Vector3.one * .4f);
					
					Gizmos.color = (squareGrid.squares[x, y].bottomRight.active) ? Color.black : Color.white;
					Gizmos.DrawCube(squareGrid.squares[x, y].bottomRight.position, Vector3.one * .4f);
					
					Gizmos.color = (squareGrid.squares[x, y].bottomLeft.active) ? Color.black : Color.white;
					Gizmos.DrawCube(squareGrid.squares[x, y].bottomLeft.position, Vector3.one * .4f);
					
					
					Gizmos.color = Color.red;
					
					Gizmos.DrawCube(squareGrid.squares[x, y].centerTop.position, Vector3.one * .25f);
					
					Gizmos.DrawCube(squareGrid.squares[x, y].centerRight.position, Vector3.one * .25f);
					
					Gizmos.DrawCube(squareGrid.squares[x, y].centerBottom.position, Vector3.one * .25f);
					
					Gizmos.DrawCube(squareGrid.squares[x, y].centerLeft.position, Vector3.one * .25f);
				}
			}
			
		}
		*/
	}
	
	public class SquareGrid {
		public Square[,] squares;
		
		public SquareGrid (int[,] map, float squareSize) {
			int nodeCountX = map.GetLength(0);
			int nodeCountY = map.GetLength(1);
			float mapWidht = nodeCountX * squareSize;
			float mapHeight = nodeCountY * squareSize;
			
			ControllNode[,] controllNodes = new ControllNode[nodeCountX, nodeCountY];
			
			for(int x = 0; x < nodeCountX; x ++) {
				for(int y = 0; y < nodeCountY; y ++) {
					Vector3 pos = new Vector3(-mapWidht/2 + x * squareSize + squareSize/2, 0, -mapHeight/2 + y * squareSize + squareSize/2);
					controllNodes[x, y] = new ControllNode(pos, map[x,y] == 1, squareSize );
					
				}
			}
			
			squares = new Square[nodeCountX - 1, nodeCountY - 1];
			
			for(int x = 0; x < nodeCountX-1; x ++) {
				for(int y = 0; y < nodeCountY-1; y ++) {
					squares[x, y] = new Square(controllNodes[x, y+1], controllNodes[x+1, y+1], controllNodes[x+1, y], controllNodes[x,y]);		
				}
			}
			
			
			
		}
	}
	
	
	public class Square {
		public ControllNode topLeft, topRight, bottomRight, bottomLeft;
		public Node centerTop, centerRight, centerBottom, centerLeft;
		public int configuration;
		
		public Square (ControllNode _topLeft, ControllNode _topRight, ControllNode _bottomRight, ControllNode _bottomLeft) {
			topLeft = _topLeft;
			topRight = _topRight;
			bottomRight = _bottomRight;
			bottomLeft = _bottomLeft;
			
			centerTop = topLeft.right;
			centerRight = bottomRight.above;
			centerLeft = bottomLeft.above;
			centerBottom = bottomLeft.right;
			
			if(topLeft.active)
				configuration += 8;
			if(topRight.active)
				configuration += 4;
			if(bottomRight.active)
				configuration += 2;
			if(bottomLeft.active)
				configuration += 1;
								
		}
	}

	public class Node {
		public Vector3 position;
		public int vertexIndex = -1;
		
		public Node(Vector3 _pos) {
			position = _pos;
		}
		
	}
	
	public class ControllNode : Node {
		public bool active;
		
		public Node above, right;
		
		public ControllNode(Vector3 _pos, bool Active, float squareSize) : base(_pos) {
			active = Active;
			above = new Node(position + Vector3.forward * squareSize/2f);
			right = new Node(position + Vector3.right * squareSize/2f);
		}
	}
	
	
}
