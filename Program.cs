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
            //Startovací vrchol
            int start = 0;                         // cesta mezi vrcholy     A  B  C  D  E
            int[][] vrcholyCesty = new int[][] {    /**vrchol A */new int[] { 0, 4, 2, 0, 0 },   
                                                    /**vrchol B */new int[] { 0, 0, 3, 2, 3 },
                                                    /**vrchol C */new int[] { 0, 1, 0, 4, 5 },
                                                    /**vrchol D */new int[] { 0, 0, 0, 0, 0 },
                                                    /**vrchol E */new int[] { 0, 0, 0,-5, 0 } };

            var početVrcholu = vrcholyCesty[0].Length;
            int[] distance = Enumerable.Repeat(int.MaxValue, početVrcholu).ToArray();
            int[] parent = Enumerable.Repeat(-1, početVrcholu).ToArray();
            distance[start] = 0;
            List<Edge> edges = new List<Edge>();

            //Doplnění všech cest do listu
            for (int i = 0; i < vrcholyCesty[0].Length; i++)
            {
                for (int j = 0; j < vrcholyCesty[1].Length; j++)
                {
                    if (vrcholyCesty[i][j] != 0)
                        edges.Add(new Edge() { From = i, To = j, Weight = vrcholyCesty[i][j] });
                }
            }

            //Zavoláme Bellman Ford algoritmus
            if(BellmanFord(edges, početVrcholu, distance, parent))
            {
                Console.WriteLine("od vrcholu A do vrcholu B");
                VykresliCestu(0, 1, distance, parent);
                Console.WriteLine("od vrcholu A do vrcholu C");
                VykresliCestu(0, 2, distance, parent);
                Console.WriteLine("od vrcholu A do vrcholu D");
                VykresliCestu(0, 3, distance, parent);
                Console.WriteLine("od vrcholu A do vrcholu E");
                VykresliCestu(0, 4, distance, parent);
            } else
            {
                Console.WriteLine("Negativní cyklus");
            }


            Console.ReadKey();
        }

        static void Relax(int u, int v, int weight, int[] distance, int[] parent)
        {
            if (distance[u] != int.MaxValue && distance[v] > distance[u] + weight)
            {
                distance[v] = distance[u] + weight;
                parent[v] = u;
            }
        }

        static bool BellmanFord(List<Edge> edges, int pocetVrcholu, int[] distance, int[] parent)
        {
            //Relaxing each edge by traversing from each vertex and so complexity is O(V.E)
            for (int i = 1; i < pocetVrcholu; i++)
            {
                foreach (Edge edge in edges)
                {
                    Relax(edge.From, edge.To, edge.Weight, distance, parent);
                }
            }

            //Checking if no negative-weight cycle exist
            foreach (Edge edge in edges)
            {
                if (distance[edge.From] != int.MaxValue && distance[edge.To] > distance[edge.From] + edge.Weight)
                {
                    return false;
                }
            }

            return true;
        }

        public static void VykresliCestu(int from, int to, int[] distance, int[] parent)
        {
            if (to < 0 || from < 0)
            {
                return;
            }
            if (to != from)
            {
                VykresliCestu(from, parent[to], distance, parent);
                Console.WriteLine("Vrchol {0} hodnota: {1}", convertToChar(to), distance[to]);
            }
            else
                Console.WriteLine("Vrchol {0} hodnota: {1}", convertToChar(to), distance[to]);
        }

        public static string convertToChar(int to)
        {
            switch(to)
            {
                case 0:
                    return "A";
                case 1:
                    return "B";
                case 2:
                    return "C";
                case 3:
                    return "D";
                case 4:
                    return "E";
                default:
                    return "";
            }
        }
    }

    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }
    }
}
