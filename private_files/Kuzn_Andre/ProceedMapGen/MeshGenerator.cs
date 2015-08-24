using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshGenerator : MonoBehaviour {
	
	public SquareGrid squareGrid;
	public MeshFilter awesomeWalls;
	
	List<Vector3> vertices;
	List<int> triangles;
	List<List<int>> outlines = new List<List<int>> ();
	Dictionary<int, List<Triangle>> triangleDictionary = new Dictionary<int, List<Triangle>>();
	
	HashSet<int> checkedVertices = new HashSet<int>();
	
	public void GenerateMesh(int[,] map, float squareSize) {
	
		triangleDictionary.Clear();
		outlines.Clear ();
		checkedVertices.Clear ();
	
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
		
		CreateWallMesh();
	}
	
	void CreateWallMesh() {
	
		CalculateMeshOutlines ();
	
		List<Vector3> wallVertices = new List<Vector3>();
		List<int> wallTriangles = new List<int>();
		Mesh wallMesh = new Mesh();
		float wallHeight = 5;
		
		foreach (List<int> outline in outlines) {
			for (int i = 0; i < outline.Count - 1; i ++) {
				int startIndex = wallVertices.Count;
				wallVertices.Add(vertices[outline[i]]); // left vertex
				wallVertices.Add(vertices[outline[i+1]]); // right vertex
				wallVertices.Add(vertices[outline[i]] - Vector3.up * wallHeight); // bottom vertex
				wallVertices.Add(vertices[outline[i+1]] - Vector3.up * wallHeight); // top vertex
				
				wallTriangles.Add(startIndex + 0);
				wallTriangles.Add(startIndex + 2);
				wallTriangles.Add(startIndex + 3);
				
				wallTriangles.Add(startIndex + 3);
				wallTriangles.Add(startIndex + 1);
				wallTriangles.Add(startIndex + 0);
				
			}	
		}
		wallMesh.vertices = wallVertices.ToArray();
		wallMesh.triangles = wallTriangles.ToArray();
		awesomeWalls.mesh = wallMesh;
	}
	
	void TraiangulateSquare(Square square) {
		switch(square.configuration) {
			case 0:
				break;
			case 1:
				MeshFromPoint(square.centerLeft, square.centerBottom, square.bottomLeft);
				break;
			case 2:
				MeshFromPoint(square.bottomRight, square.centerBottom, square.centerRight);
				break;
			case 4:
				MeshFromPoint(square.topRight, square.centerRight, square.centerTop);
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
				checkedVertices.Add(square.topLeft.vertexIndex);
				checkedVertices.Add(square.topRight.vertexIndex);
				checkedVertices.Add(square.bottomRight.vertexIndex);
				checkedVertices.Add(square.bottomLeft.vertexIndex);
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
		
		Triangle triangle = new Triangle (a.vertexIndex, b.vertexIndex, c.vertexIndex);
		AddTriangleToDictionary (triangle.vertexIndexA, triangle);
		AddTriangleToDictionary (triangle.vertexIndexB, triangle);
		AddTriangleToDictionary (triangle.vertexIndexC, triangle);
		
	}
	
	void AddTriangleToDictionary (int vertexIndexKey, Triangle triangle) {
		if(triangleDictionary.ContainsKey (vertexIndexKey)) {
			triangleDictionary [vertexIndexKey].Add (triangle);
		}
		else {
			List<Triangle> triangleList = new List<Triangle>();
			triangleList.Add(triangle);
			triangleDictionary.Add (vertexIndexKey, triangleList);
		}
	}
	
	void CalculateMeshOutlines() {
	
		for (int vertexIndex = 0; vertexIndex < vertices.Count; vertexIndex ++) {
			if (!checkedVertices.Contains(vertexIndex)) {
				int newOutlineVertex = GetConnectedOutlineVertex(vertexIndex);
				if (newOutlineVertex != -1) {
					checkedVertices.Add (vertexIndex);
					
					List<int> newoutline = new List<int>();
					newoutline.Add (vertexIndex);
					outlines.Add(newoutline);
					FollowOutline(newOutlineVertex, outlines.Count - 1);
					
					outlines[outlines.Count - 1].Add(vertexIndex);
				}
			}	
		}
	
	}
	
	void FollowOutline (int vertexIndex, int oulineIndex){
		outlines[oulineIndex].Add(vertexIndex);
		checkedVertices.Add(vertexIndex);
		
		int nextVertexIndex = GetConnectedOutlineVertex (vertexIndex);
		
		if(nextVertexIndex != -1) {
			FollowOutline(nextVertexIndex, oulineIndex);
		}
	}
	
	 	
	 	 	 	
	int GetConnectedOutlineVertex (int vertexIndex) {
		List<Triangle> triangelsContainingVertex = triangleDictionary [vertexIndex];
		
		for (int i = 0; i < triangelsContainingVertex.Count; i ++) {
			Triangle triangle = triangelsContainingVertex[i];
			
			for(int j = 0; j < 3; j ++){
				int vertexB = triangle[j];
				
				if (vertexB != vertexIndex && !checkedVertices.Contains(vertexB)) {
				
					if(IsOutLineEdge(vertexIndex, vertexB)){
						return vertexB;
					}
				}
			}
		}
		
		return -1;
	}
	
	bool IsOutLineEdge(int vertexA, int vertexB) {
	
		List<Triangle> trianglesContainingVertexA = triangleDictionary [vertexA];
		int sharedTriangelCount = 0;
		
		for(int i = 0; i < trianglesContainingVertexA.Count; i ++){
			if(trianglesContainingVertexA[i].Contains(vertexB)) {
				sharedTriangelCount ++;
				if(sharedTriangelCount > 1) {
					break;
				}
			}
		}
		return sharedTriangelCount == 1;
	}
	
	struct Triangle {
		public int vertexIndexA;
		public int vertexIndexB;
		public int vertexIndexC;
		int[] vertices;
		
		public Triangle(int a, int b, int c) {
			vertexIndexA = a;
			vertexIndexB = b;
			vertexIndexC = c;
			
			vertices = new int[3];
			vertices[0] = a;
			vertices[1] = b;
			vertices[2] = c;
		}
		
		public int this[int i] {
			get {
				return vertices[i];
			}
		}
		
		public bool Contains(int vertexIndex) {
			return vertexIndex == vertexIndexA || vertexIndex == vertexIndexB || vertexIndex == vertexIndexC;
		}
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
