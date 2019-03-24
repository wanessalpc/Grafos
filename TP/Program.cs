using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP
{
    class Program
    {
        public static void Main(string[] args)
        {
            Grafo g = new Grafo();

            Vertice v1 = new Vertice() , v2 = new Vertice() , v3 = new Vertice();
            v1.valor = 1;
            v2.valor = 2;
            v3.valor = 3;
            List<Vertice> adj1 = new List<Vertice>();
            List<Vertice> adj2 = new List<Vertice>();
            List<Vertice> adj3 = new List<Vertice>();

            adj1.Add(v2);
            adj1.Add(v3);
            adj2.Add(v1);
            adj2.Add(v3);
            adj3.Add(v1);
            adj3.Add(v2);

            g.grafo.Add(v1, adj1);
            g.grafo.Add(v2, adj2);
            g.grafo.Add(v3, adj3);

           
        }
    }
}
