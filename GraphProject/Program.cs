// See https://aka.ms/new-console-template for more information
using System.Collections;

Console.WriteLine("Hello, World!");
Graph graph = new Graph();
graph.AddVertex("0");
graph.AddVertex("1");
graph.AddVertex("2");
graph.AddVertex("3");
graph.AddVertex("4");

graph.AddEdge(0, 1);
graph.AddEdge(1, 2);
graph.AddEdge(1, 3);
graph.AddEdge(3, 4);

graph.ShowVertex(3);
Console.WriteLine();
Console.WriteLine("-------------------");
graph.ShowGraph();

public class Vertex
{
    private string label;
    private bool wasVisited;
    public String Label
    {
        get { return label; }
        set { label = value; }
    }

    public bool WasVisited
    {
        get { return wasVisited; }
        set { wasVisited = value; }
    }

    public Vertex(string label) 
    { 
        this.wasVisited = false;
        this.label = label;
    }
}

public class Graph
{
    private const int NUM_VERTICES = 20;
    private Vertex[] vertices;
    private int[,] adjMatrix;
    private int numVerts;
    private Stack stack;
    private Queue queue;

    public Graph() 
    { 
        vertices = new Vertex[NUM_VERTICES];
        adjMatrix = new int[NUM_VERTICES, NUM_VERTICES];
        numVerts = 0;
        stack = new Stack();
        queue = new Queue();
        for (int i = 0; i < NUM_VERTICES; i++) 
        { 
            for (int j = 0; j < NUM_VERTICES; j++) 
            {
                adjMatrix[i,j] = 0;
            }
        }
    }
    public void AddVertex(string label)
    {
        vertices[numVerts++] = new Vertex(label);
    }
    public void AddEdge(int start, int end)
    {
        adjMatrix[start, end] = 1;//unweighted is one
        adjMatrix[end, start] = 1;//unweighted is one
    }
    public void AddEdge(int start, int end, int weight)
    {
        adjMatrix[start, end] = weight;//weighted is weight
        adjMatrix[end, start] = weight;//weighted is weight
    }
    public void ShowVertex(int v)
    {
        Console.Write(vertices[v].Label + " ");
    }
    public void ShowGraph()
    { 
        for(int i = 0;i < NUM_VERTICES;i++) 
        {
            if (vertices[i] == null)
            {
                Console.WriteLine("empty!!!");
            }
            else {
                Console.WriteLine(vertices[i].Label);
            }
        }
    }
}