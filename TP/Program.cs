﻿using System;
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

            Vertice v1 = new Vertice(), v2 = new Vertice(), v3 = new Vertice();
            v1.valor = 1;
            v1.nome = "V1";
            v2.valor = 2;
            v2.nome = "V2";
            v3.valor = 3;
            v3.nome = "V3";
            List<Vertice> adj1 = new List<Vertice>();
            List<Vertice> adj2 = new List<Vertice>();
            List<Vertice> adj3 = new List<Vertice>();

            adj1.Add(v2);
            adj1.Add(v3);
            adj2.Add(v1);
            adj2.Add(v3);
            adj3.Add(v1);
            adj3.Add(v2);

            //--- grafo direcionado
            //adj1.Add(v1);
            //adj2.Add(v1);
            //adj2.Add(v2);
            //adj2.Add(v3);
            //adj2.Add(v3);
            //adj3.Add(v1);




            g.grafo.Add(v1, adj1);
            g.grafo.Add(v2, adj2);
            g.grafo.Add(v3, adj3);

            if ()//colocar o método que quer ser testado
            {
                Console.WriteLine("ye");
            }
            else
            {
                Console.WriteLine("nay");
            }





            objArquivo.LerArquivo();
            Console.ReadKey();
        }
    }
}


//    , v4 = new Vertice(), v5 = new Vertice();
//v1.valor = 1;
//            v1.nome = "V1";
//            v2.valor = 2;
//            v2.nome = "V2";
//            v3.valor = 3;
//            v3.nome = "V3";
//            v4.valor = 4;
//            v4.nome = "V4";
//            v5.valor = 5;
//            v5.nome = "V5";

//            List<Vertice> adj1 = new List<Vertice>();
//List<Vertice> adj2 = new List<Vertice>();
//List<Vertice> adj3 = new List<Vertice>();
//List<Vertice> adj4 = new List<Vertice>();
//List<Vertice> adj5 = new List<Vertice>();

//adj1.Add(v2);
//            adj1.Add(v3);

//            adj2.Add(v1);
//            adj2.Add(v3);
//            adj2.Add(v4);
//            adj2.Add(v5);


//            adj3.Add(v1);
//            adj3.Add(v2);
//            adj3.Add(v4);

//            adj4.Add(v2);
//            adj4.Add(v3);
//            adj4.Add(v5);

//            adj5.Add(v2);
//            adj5.Add(v4);



//            g.grafo.Add(v1, adj1);
//            g.grafo.Add(v2, adj2);
//            g.grafo.Add(v3, adj3);
//            g.grafo.Add(v4, adj4);
//            g.grafo.Add(v5, adj5);