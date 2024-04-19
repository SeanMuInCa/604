// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Globalization;

Console.WriteLine("Hello, World!");
Graph aGraph = new Graph();
aGraph.AddVertex("1");
aGraph.AddVertex("2");
aGraph.AddVertex("3");
aGraph.AddVertex("4");
aGraph.AddVertex("5");
aGraph.AddVertex("6");
aGraph.AddVertex("7");
aGraph.AddVertex("8");
aGraph.AddVertex("9");
aGraph.AddEdge(0, 1, 7);
aGraph.AddEdge(0, 2, 6);
aGraph.AddEdge(1, 6, 4);
aGraph.AddEdge(2, 3, 2);
aGraph.AddEdge(2, 4, 8);
aGraph.AddEdge(2, 6, 11);
aGraph.AddEdge(3, 4, 5);
aGraph.AddEdge(4, 5, 4);
aGraph.AddEdge(5, 6, 3);
aGraph.AddEdge(5, 8, 5);
aGraph.AddEdge(6, 7, 9);
aGraph.AddEdge(7, 8, 7);

aGraph.DijkstraSPF(0);
/*graph.AddVertex("0");
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
graph.ShowGraph();*/

/*graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");
graph.AddVertex("D");
graph.AddVertex("E");
graph.AddVertex("F");
graph.AddVertex("G");
graph.AddVertex("H");
graph.AddVertex("I");
graph.AddVertex("J");
graph.AddVertex("K");
graph.AddVertex("L");
graph.AddVertex("M");

graph.AddEdge(0, 1);
graph.AddEdge(0, 4);
graph.AddEdge(0, 7);
graph.AddEdge(0, 10);
graph.AddEdge(1, 2);
graph.AddEdge(2, 3);
graph.AddEdge(4, 5);
graph.AddEdge(5, 6);
graph.AddEdge(7, 8);
graph.AddEdge(8, 9);
graph.AddEdge(10, 11);
graph.AddEdge(11, 12);

graph.DepthFirstTraversal(0);
Console.WriteLine();
graph.BreathFirstTraversal(0);*/
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

    public int GetAdjUnvisitedVertex(int v)
    { 
        for (int i = 0; i < NUM_VERTICES;i++) 
        {
            if ((adjMatrix[v,i] == 1) && vertices[i].WasVisited == false) {
                return i;
            }
        }
        return -1;
    }

    public void DepthFirstTraversal(int v)
    {
        vertices[v].WasVisited = true;
        ShowVertex(v);
        stack.Push(v);
        int vertex;
        while (stack.Count > 0) 
        {
            vertex = GetAdjUnvisitedVertex((int)stack.Peek());
            if (vertex == -1)
            {
                stack.Pop();
            }
            else 
            {
                vertices[vertex].WasVisited = true;
                ShowVertex(vertex);
                stack.Push(vertex);
            }
        }
        ResetVertices();
    }

    public void BreathFirstTraversal(int v)
    {
        vertices[v].WasVisited = true;
        ShowVertex(v);
        queue.Enqueue(v);
        int v1, v2;
        while (queue.Count > 0) 
        { 
            v1 = (int) queue.Dequeue();
            v2 = GetAdjUnvisitedVertex(v1);
            while (v2 != -1) {
                vertices[v2].WasVisited = true;
                ShowVertex(v2);
                queue.Enqueue(v2);
                v2 = GetAdjUnvisitedVertex(v1);
            }
        }
        ResetVertices();
    }

    private void ResetVertices()
    {
        for (int i = 0; i < NUM_VERTICES; i++)
        {
            if (vertices[i] != null) {
                vertices[i].WasVisited = false;
            }
        }
    }

    public void DijkstraSPF(int source)//index
    {
        int[] distance = new int[NUM_VERTICES];
        int[] parent = new int[NUM_VERTICES];
        bool[] processed = new bool[NUM_VERTICES];

        for (int i = 0; i < numVerts; i++)
        {
            distance[i] = int.MaxValue;
            processed[i] = false;
            parent[i] = int.MaxValue;
        }

        distance[source] = 0;
        parent[source] = -1;


        //main loop
        for (int i = 0; i < numVerts - 1; i++)
        {
            int u = MinimumDistance(distance, processed);
            processed[u] = true;

            //check u,v
            for (int v = 0; v < numVerts; v++)
            {
                if (!processed[v] && adjMatrix[u, v] != 0 && distance[u] + adjMatrix[u, v] < distance[v])
                {
                    distance[v] = distance[u] + adjMatrix[u,v];
                    parent[v] = u;
                }
            }
        }
        Print(distance, parent);
    }
    private int MinimumDistance(int[] distance, bool[] processed)
    {
        int min = int.MaxValue;
        int minIndex = 0;

        for (int i = 0; i < numVerts; i++)
        {
            if (!processed[i] && distance[i] <= min)
            { 
                min = distance[i];
                minIndex = i;
            }
        }
        return minIndex;
    }
    private void Print(int[] distance, int[] parent)
    {
        Console.WriteLine("Vertex\t Dist.\t Parent");
        for (int i = 0;i < numVerts;i++) 
        {
            if (parent[i] < 0)//the source position
            {
                Console.WriteLine("{0}\t {1}\t {2}", i, distance[i], parent[i]);
            }
            else 
            {
                Console.WriteLine("{0}\t {1}\t {2}", i, distance[i], vertices[parent[i]].Label);    
            }
        }
    }
}