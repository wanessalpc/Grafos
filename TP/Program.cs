using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TP
{
    class Program
    {

       // public static Grafo g = new Grafo();
/*
        public void LerArquivo() // Lê arquivo e cria vagas se nescessario.
        {
            if (File.Exists(@"C:\Users\Rafael Badaró\Desktop\Eng. Software\5-período\Grafos\TP\grafo.txt"))
            {
                using (StreamReader reader = new StreamReader(@"C:\Users\Rafael Badaró\Desktop\Eng. Software\5-período\Grafos\TP\grafo.txt"))
                {

                    while (!reader.EndOfStream) // Enquanto arquivo não acaba.
                    {

                        string linha = reader.ReadLine();
                        if (linha.Length > 1)
                        {
                            string[] dados = linha.Split(';');

                            Vertice v1 = new Vertice();
                            v1.nome = "V" + dados[0].TrimStart();
                            Vertice v2 = new Vertice();
                            v2.nome = "V" + dados[1].TrimStart();

                            if (dados.Length == 3)
                            {
                                g.grafo = AdicionarNoGrafo(g.grafo, v1, new Vertice(v2.nome, Int32.Parse(dados[2])), false);
                            }
                            else
                            {
                            g.isDirecionado = true;
                                if (dados[3].TrimStart().Equals("1"))//se for 1, a aresta sai de v1 e vai pra v2
                                {
                                    g.grafo = AdicionarNoGrafo(g.grafo, v1, new Vertice(v2.nome, Int32.Parse(dados[2])), true);
                                }
                                else
                                {
                                    g.grafo = AdicionarNoGrafo(g.grafo, v2, new Vertice(v1.nome, Int32.Parse(dados[2])), true);
                                }
                            }
                        }

                    }

                }
            }
        }
*/
/*
        private Dictionary<Vertice, List<Vertice>> AdicionarNoGrafo(Dictionary<Vertice, List<Vertice>> grafoParametro, Vertice key, Vertice value, bool isDirecionado)
        {
            if (key != null)
            {
                Vertice verticeNoGrafo = Contem(key, grafoParametro.Keys.ToList());
                if (verticeNoGrafo == null)
                {
                    List<Vertice> listaNova = new List<Vertice>();
                    listaNova.Add(value);
                    grafoParametro.Add(key, listaNova);
                }
                else
                {
                    if (grafoParametro.TryGetValue(verticeNoGrafo, out List<Vertice> listaAdj))
                    {
                        listaAdj.Add(value);
                    }
                }
                //--- virse versa
                if (!isDirecionado)
                {
                    verticeNoGrafo = Contem(value, grafoParametro.Keys.ToList());
                    if (verticeNoGrafo == null)
                    {
                        List<Vertice> listaNova = new List<Vertice>();
                        Vertice keyAux = new Vertice();
                        keyAux.nome = key.nome;
                        keyAux.peso = value.peso;
                        listaNova.Add(keyAux);
                        grafoParametro.Add(value, listaNova);
                    }
                    else
                    {
                        if (grafoParametro.TryGetValue(verticeNoGrafo, out List<Vertice> listaAdj))
                        {
                            Vertice keyAux = new Vertice();
                            keyAux.nome = key.nome;
                            keyAux.peso = value.peso;
                            listaAdj.Add(keyAux);
                        }
                    }
                }

            }
            else
            {
                grafoParametro.Add(value, new List<Vertice>());
            }
            return grafoParametro;
        }
*/
        //private Vertice Contem(Vertice v, List<Vertice> lista)
        //{
        //    Vertice verticeNaLista = lista.Find(vertice => vertice.nome == v.nome);
        //    if (verticeNaLista == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return g.grafo.Keys.Where(ver => ver.nome == verticeNaLista.nome).ElementAt(0);
        //    }
        //}


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






            //objArquivo.LerArquivo();
            g.ImprimirGrafo(g.GetAGMKruskal());


            //if ()//colocar o método que quer ser testado
            //{
            //    Console.WriteLine("ye");
            //}
            //else
            //{
            //    Console.WriteLine("nay");
            //}


            Console.ReadKey();
        }
    }
}
