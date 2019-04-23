using System;
using System.Collections.Generic;
using System.IO;


namespace TP
{
    class Program
    {
        public void LerArquivo() // Lê arquivo e cria vagas se nescessario.
        {
            if (File.Exists(@"C:\Users\Wanessa\Documents\GitHub\Grafos\TP\bin\Debug\ListaArquivos.txt"))
            {
                using (StreamReader reader = new StreamReader(@"C:\Users\Wanessa\Documents\GitHub\Grafos\TP\bin\Debug\ListaArquivos.txt"))
                {
                    while (!reader.EndOfStream) // Enquanto arquivo não acaba.
                    {
                        string linha = reader.ReadLine();
                        string[] dados = linha.Split('-');
                    }
                }
            }
        }
        public static void Main(string[] args)
        {
            Grafo g = new Grafo();
            Program objArquivo = new Program();

            Vertice v1 = new Vertice(), v2 = new Vertice(), v3 = new Vertice(), v4 = new Vertice(), v5 = new Vertice(), v6 = new Vertice(); // vértices originais
            v1.nome = "V1";
            v2.nome = "V2";
            v3.nome = "V3";
            v4.nome = "V4";
            v5.nome = "V5";
            v6.nome = "V6";
            List<Vertice> adj1 = new List<Vertice>();
            List<Vertice> adj2 = new List<Vertice>();
            List<Vertice> adj3 = new List<Vertice>();
            List<Vertice> adj4 = new List<Vertice>();
            List<Vertice> adj5 = new List<Vertice>();
            List<Vertice> adj6 = new List<Vertice>();

            adj1.Add(new Vertice("V2", 4));
            adj1.Add(new Vertice("V3", 2));
            adj1.Add(new Vertice("V5", 3));

            adj2.Add(new Vertice("V1", 4));
            adj2.Add(new Vertice("V4", 5));

            adj3.Add(new Vertice("V1", 2));
            adj3.Add(new Vertice("V4", 1));
            adj3.Add(new Vertice("V5", 6));
            adj3.Add(new Vertice("V6", 3));

            adj4.Add(new Vertice("V2", 5));
            adj4.Add(new Vertice("V3", 1));
            adj4.Add(new Vertice("V6", 6));

            adj5.Add(new Vertice("V1", 3));
            adj5.Add(new Vertice("V3", 6));
            adj5.Add(new Vertice("V6", 2));

            adj6.Add(new Vertice("V3", 3));
            adj6.Add(new Vertice("V4", 6));
            adj6.Add(new Vertice("V5", 2));

            g.grafo.Add(v1, adj1);
            g.grafo.Add(v2, adj2);
            g.grafo.Add(v3, adj3);
            g.grafo.Add(v4, adj4);
            g.grafo.Add(v5, adj5);
            g.grafo.Add(v6, adj6);

            //if (g.IsUnicursal())//colocar o método que quer ser testado
            //{
            //    Console.WriteLine("ye");
            //}
            //else
            //{
            //    Console.WriteLine("nay");
            //}

            g.ImprimirGrafo(g.GetAGMPrim(v1));




            objArquivo.LerArquivo();
            Console.ReadKey();
        }
    }
}
