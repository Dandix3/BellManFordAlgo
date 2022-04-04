using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALGbellmanford
{
    class Program
    {
        static void Main(string[] args)
        {
            //Source vertex
            int source = 0;
            int[][] adjacencyMatrix = new int[][] { /**vrchol 1 */new int[] { 0, 0, 0, 3, 12 },   
                                                    /**vrchol 2 */new int[] { 0, 0, 2, 0, 0 },
                                                    /**vrchol 3 */new int[] { 0, 0, 0, -2,0 },
                                                    /**vrchol 4 */new int[] { 0, 5, -3, 0, 0 },
                                                    /**vrchol 5 */new int[] { 0, 0, -3, 0, 0 } };

            var numberOfVertex = adjacencyMatrix[0].Length;
            int[] distance = Enumerable.Repeat(int.MaxValue, numberOfVertex).ToArray();
            int[] parent = Enumerable.Repeat(-1, numberOfVertex).ToArray();
            distance[source] = 0;
            List<Edge> edges = new List<Edge>();

            //Insering all edges in list
            for (int i = 0; i < adjacencyMatrix[0].Length; i++)
                for (int j = 0; j < adjacencyMatrix[0].Length; j++)
                {
                    if (adjacencyMatrix[i][j] != 0)
                        edges.Add(new Edge() { U = i, V = j, Weight = adjacencyMatrix[i][j] });
                }
            //Calling Bellman-Ford Algorithm
            if(BellmanFord(edges, numberOfVertex, distance, parent))
            {
                PrintPath(0, 2, distance, parent);
            } else
            {
                Console.WriteLine("Negativní cyklus");
            }
            //Prints path


            Console.ReadLine();
        }

        static void Relax(int u, int v, int weight, int[] distance, int[] parent)
        {
            if (distance[u] != int.MaxValue && distance[v] > distance[u] + weight)
            {
                distance[v] = distance[u] + weight;
                parent[v] = u;
            }
        }

        static bool BellmanFord(List<Edge> edges, int vertexCount, int[] distance, int[] parent)
        {
            //Relaxing each edge by traversing from each vertex and so complexity is O(V.E)
            for (int i = 1; i < vertexCount; i++)
            {
                foreach (Edge edge in edges)
                {
                    Relax(edge.U, edge.V, edge.Weight, distance, parent);
                }
            }

            //Checking if no negative-weight cycle exist
            foreach (Edge edge in edges)
            {
                if (distance[edge.U] != int.MaxValue && distance[edge.V] > distance[edge.U] + edge.Weight)
                {
                    return false;
                }
            }

            return true;
        }

        public static void PrintPath(int u, int v, int[] distance, int[] parent)
        {
            if (v < 0 || u < 0)
            {
                return;
            }
            if (v != u)
            {
                PrintPath(u, parent[v], distance, parent);
                Console.WriteLine("Vrchol {0} hodnota: {1}", v, distance[v]);
            }
            else
                Console.WriteLine("Vrchol {0} hodnota: {1}", v, distance[v]);
        }
    }

    public class Edge
    {
        public int U { get; set; }
        public int V { get; set; }
        public int Weight { get; set; }
    }
}
