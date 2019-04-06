using System;
using System.Collections.Generic;
using System.Linq;

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
            if (grafo.TryGetValue(v1, out listaDeAdj))
            {
                return listaDeAdj.Count; // para cada vértice na lista de adjacentes, o grau do vértice aumenta
            }
            else
            {
                return 0; // se não achar o valor ele não tem adjacente
            }
        }

        public bool IsIsolado(Vertice v1)
        {
            bool isIsolado = false;
            List<Vertice> listaDeAdj;
            if (grafo.TryGetValue(v1, out listaDeAdj))
            {
                if(listaDeAdj.Count() == 0 || (listaDeAdj.Count() == 1 && listaDeAdj.Contains(v1)))// se a lista estiver ou se ela conter um elemento e este for o próprio vértice(loop)
                {
                    isIsolado = true;
                }
            }
            return isIsolado;
        }

        public bool IsPendente(Vertice v1)// vértice de grau 1, verificar se a lista de adj do vértice tem apenas 1 elemento
        {
            bool isPendente = false;
            List<Vertice> listaDeAdj;
            if(grafo.TryGetValue(v1, out listaDeAdj))
            {
                if(listaDeAdj.Count == 1)
                {
                    isPendente = true;
                }
            }

            return isPendente;
        }

        public bool IsRegular() // todos os vértices com o mesmo grau, para isso vamos ver se o tamanho da lista de adj é igual para todos os vértices
        {
            bool isRegular = false;

            int grauBase = grafo.Values.First().Count(); //  pegar o primeiro elemento e ver o tamanho da lista


            if (grafo.Values.All(lista => lista.Count == grauBase))// vê se todas as listas adjacentes tem o mesmo tamanho
            {
                isRegular = true;
            }

            return isRegular;

        }

        public bool IsNulo()// grafo sem nenhuma aresta, ou seja os véritces não podem ter lista de adjacencia/ ela deve estar vazia
        {
            bool isNulo = true;

            // grafo.Values.Any() ->  o valor sempre existe, no caso deve-se verificar se a lista está vazia( ou se existe alguma lista que não esteja vazia)
            if (grafo.Values.Any(lista => lista.Count() > 0)) 
            {
                isNulo = false;
            }

            return isNulo;
        }

        public bool IsCompleto()// a partir de um vértice é possivel alcançar todos os outros,
        {
            bool isCompleto = false;
            int contagemGrande = 0;
            //para cada vértice, pegar a lista dele e ver se nela contem os outros vértices
            foreach (Vertice v in grafo.Keys)
            {
                int contagem = 0;
                List<Vertice> listaDeAdj;
                if (grafo.TryGetValue(v, out listaDeAdj))
                {
                   IEnumerable<Vertice> verticesFiltrados = grafo.Keys.Where(vertice => vertice != v).ToList();// lista com os outros vértices//sem ser o que está sendo lido
                   IEnumerable<Vertice> listaSemLoop = listaDeAdj.Where(vertice => vertice != v).ToList();// lista de adj removendo o loop

                    //verificar se as duas listas são iguais/se a listaSemLoop contem os verticesFiltrados 
                    listaSemLoop.ToList().ForEach(vertice =>
                    {
                        if (verticesFiltrados.ToList().Contains(vertice))// se todos os contains derem true, então
                        {
                            contagem++;
                        }                     
                    });
                    if(contagem == verticesFiltrados.Count())
                    {
                        contagemGrande++;
                    }
                }
             }
            if(contagemGrande == grafo.Keys.Count())
            {
                isCompleto = true;
            }
            return isCompleto;
                
        }

        //bool IsConexo() // usar algoritimo de travessia
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
                        listaDeAdj.ForEach(vertice => Console.Write(" -> " + vertice.nome));
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
