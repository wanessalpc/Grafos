using System;
using System.Collections.Generic;

namespace TP
{
    public class Grafo
    {

        public Dictionary<Vertice, List<Vertice>> grafo = new Dictionary<Vertice, List<Vertice>>();

        /*      
            Key, Value -> no caso a nossa key é um vértice e o value é uma Lista de vértices
            
            [v1][v1 -> v2 -> v3]
            [v2][ListaDeAdj]
        */
     
        public bool IsAdjacente(Vertice v1, Vertice v2)
        {
            bool isAdj = false;
            List<Vertice> listaDeAdj;
            if (grafo.TryGetValue(v1, out listaDeAdj)) // O TryGetValue retorna um booleano, se encontra o value ele é passado pra variável no out
            {
                if (listaDeAdj.Contains(v2))
                {
                    isAdj = true;
                    Console.WriteLine("Os vértices : " + v1.valor + " e " + v2.valor + " são adjacentes");
                }
                else
                {
                    Console.WriteLine("Não são adjacentes :(");
                }
            }
            

            return isAdj;

        }
        public int GetGrau(Vertice v1)
        {
            List<Vertice> listaDeAdj;
            if(grafo.TryGetValue(v1, out listaDeAdj))
            {
                return listaDeAdj.Count; // para cada vértice na lista de adjacentes, o grau do vértice aumenta
            }
            else
            {
                return 0; // se não achar o valor ele não tem adjacente
            }
        }
        //bool IsIsolado(Vertice v1)
        //{

        //}
        //bool IsPendente(Vertice v1)
        //{

        //}
        //bool IsRegular()
        //{

        //}
        //bool IsNulo()
        //{

        //}
        //bool IsCompleto()
        //{

        //}
        //bool IsConexo()
        //{

        //}
        //bool IsEuleriano()
        //{

        //}
        //bool IsUnicursal()
        //{

        //}
        //Grafo GetComplementar()
        //{

        //}
        //Grafo GetAGMPrim(Vertice v1)
        //{

        //}
        //Grafo GetAGMKruskal()
        //{

        //}
        //int GetCutVertices()
        //{

        //}

        public void ImprimirGrafo(Dictionary<Vertice, List<Vertice>> grafo)
        {
            Console.WriteLine("Imprimindo os valores do grafo: ");

            foreach (Vertice v in grafo.Keys)
            {
                List<Vertice> listaDeAdj;
                Console.Write("Vértice " + v.valor + ":");
                if (grafo.TryGetValue(v, out listaDeAdj))
                {
                    if (listaDeAdj.Count > 0)
                    {
                        listaDeAdj.ForEach(vertice => Console.Write(" -> " + vertice.valor));
                    }
                    else
                    {
                        Console.Write(" -> _ ");  // indica que é uma lista vazia
                    }
                }
                else
                {
                    Console.Write(" -> _ ");
                }
                Console.WriteLine();
            }

            Console.Read();
        }

    }
}
