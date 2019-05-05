using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TP
{
    class Program
    {

        public static Grafo g = new Grafo();

        public void LerArquivo() // Lê arquivo e cria vagas se nescessario.
        {
            if (File.Exists(@"C:\Users\Rafael Badaró\Desktop\Eng. Software\5-período\Grafos\TP\aciclico.txt"))
            {
                using (StreamReader reader = new StreamReader(@"C:\Users\Rafael Badaró\Desktop\Eng. Software\5-período\Grafos\TP\aciclico.txt"))
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
                        else
                        {
                            for(int i = 1; i <= Int32.Parse(linha); i++) // Cria e adiciona os vértices vazios no grafo
                            {
                                Vertice v = new Vertice();
                                v.nome = "V" + i;
                                g.grafo.Add(v, new List<Vertice>());
                            }
                        }

                    }

                }
            }
        }

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

        private Vertice Contem(Vertice v, List<Vertice> lista)
        {
            Vertice verticeNaLista = lista.Find(vertice => vertice.nome == v.nome);
            if (verticeNaLista == null)
            {
                return null;
            }
            else
            {
                return g.grafo.Keys.Where(ver => ver.nome == verticeNaLista.nome).ElementAt(0);
            }
        }


        public static void Main(string[] args)
        {

            Program objArquivo = new Program();

            objArquivo.LerArquivo();
            Grafo gCopia = g;

            gCopia.ImprimirGrafo(gCopia.grafo);
            foreach (Vertice v in gCopia.grafo.Keys)
            {
                Console.WriteLine(v.nome + "-" + gCopia.GetGrauEntrada(v));
                Console.WriteLine(v.nome + "-" + gCopia.GetGrauSaida(v));              
            }
            Console.WriteLine("CICLO");
            if (gCopia.HasCiclo())
            {
                Console.WriteLine("Sim.");
            }
            else
            {
                Console.WriteLine("Não.");
            }
            //Console.WriteLine("NULO");
            //if (gCopia.IsNulo())
            //{
            //    Console.WriteLine("Sim.");
            //}
            //else
            //{
            //    Console.WriteLine("Não.");
            //}
            //Console.WriteLine("Completo");
            //if (gCopia.IsCompleto())
            //{
            //    Console.WriteLine("Sim.");
            //}
            //else
            //{
            //    Console.WriteLine("Não.");
            //}
            //Console.WriteLine("CONEXO");
            //if (gCopia.IsConexo())
            //{
            //    Console.WriteLine("Sim.");
            //}
            //else
            //{
            //    Console.WriteLine("Não.");
            //}
            //Console.WriteLine("EULERIANO");
            //if (gCopia.IsEuleriano())
            //{
            //    Console.WriteLine("Sim.");
            //}
            //else
            //{
            //    Console.WriteLine("Não.");
            //}
            //Console.WriteLine("UNICURSAL");
            //if (gCopia.IsUnicursal())
            //{
            //    Console.WriteLine("Sim.");
            //}
            //else
            //{
            //    Console.WriteLine("Não.");
            //}
            //Console.WriteLine("COMPLEMENTAR");
            //gCopia.GetComplementar();
            //Console.WriteLine("PRIM");
            //gCopia.ImprimirGrafo(gCopia.GetAGMPrim(new Vertice("V1")));
            //Console.WriteLine("KRUSKAL");
            //gCopia.ImprimirGrafo(gCopia.GetAGMKruskal());

            //--dirigido


            Console.WriteLine();




            Console.ReadKey();
        }
    }
}
