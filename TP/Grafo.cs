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
                if (listaDeAdj.Count() == 0 || (listaDeAdj.Count() == 1 && listaDeAdj.Contains(v1)))// se a lista estiver ou se ela conter um elemento e este for o próprio vértice(loop)
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
            if (grafo.TryGetValue(v1, out listaDeAdj))
            {
                if (listaDeAdj.Count == 1)
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
            int contagem = 0;
            //para cada vértice, pegar a lista dele e ver se nela contem os outros vértices
            foreach (Vertice v in grafo.Keys)
            {

                List<Vertice> listaDeAdj;
                if (grafo.TryGetValue(v, out listaDeAdj))
                {
                    List<Vertice> verticesFiltrados = grafo.Keys.Where(vertice => vertice != v).ToList();// lista com os outros vértices//sem ser o que está sendo lido

                    if (!listaDeAdj.Contains(v))//não tiver loops
                    {

                        if (verticesFiltrados.Count == listaDeAdj.Count && !verticesFiltrados.Except(listaDeAdj).Any())
                        {
                            contagem++;// significa que o vértice está ligado a todos os outros vértices
                        }
                    }

                }
            }
            if (contagem == grafo.Keys.Count())
            {
                isCompleto = true;
            }
            return isCompleto;

        }

        public bool IsConexo() // utilizando travessia em amplitude
        {
            return TravessiaAmplitude();
        }

        Queue<Vertice> fila = new Queue<Vertice>();
        private bool TravessiaAmplitude()
        {
            bool isConexo = true;
            foreach (Vertice v in grafo.Keys)
            {
                v.cor = "branco";
                v.pred = null;
                v.distancia = int.MaxValue;
            }
            foreach (Vertice v in grafo.Keys)
            {
                if (v.cor.Equals("branco"))
                {
                    if (!VisitaBFS(v))
                    {
                        isConexo = false;
                    }
                }
            }
            return isConexo;
        }

        private bool VisitaBFS(Vertice v)
        {
            v.cor = "azul";
            v.distancia = 0;

            fila.Clear();
            fila.Enqueue(v);
            while (fila.Count > 0)
            {
                Vertice removido = fila.Dequeue();

                List<Vertice> listaDeAdjRemovido = new List<Vertice>();
                if (grafo.TryGetValue(removido, out listaDeAdjRemovido))
                {
                    foreach (Vertice vertice in listaDeAdjRemovido)
                    {
                        if (vertice.cor.Equals("branco"))
                        {
                            vertice.cor = "azul";
                            vertice.distancia = removido.distancia + 1;
                            vertice.pred = removido;
                            fila.Enqueue(vertice);
                        }
                    }
                    v.cor = "vermelho";
                }
            }
            //Se a fila se esvaziar e ainda tiver vértices brancos do grafo, ele não é conexo
            if (grafo.Keys.Any(vr => vr.cor.Equals("branco")))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsEuleriano() // Um grafo é euleriano se possuir todos os vértices de grau par e é conexo
        {
            bool isEuleriano = true;
            if (IsConexo())
            {
                if (grafo.Values.Any(lista => lista.Count % 2 > 0)) // Grau par = lista de adj com tamanho par, no caso veremos se alguma lista tem tamanho ímpar
                {
                    isEuleriano = false;
                }
            }

            return isEuleriano;
        }

        public bool IsUnicursal()// Grafo que possui pelo menos um trajeto euleriano, usando a regra 2k = numero de vértices de grau impar
        {
            // lembrando que para ser um grafo o número de vértices de grau ímpar tem que ser par
            bool isUnicursal = false;

            int numeroVerticesImpares = grafo.Values.Where(lista => lista.Count % 2 > 0).Count();// Número de vértices de grau ímpar

            if (numeroVerticesImpares / 2 > 0)
            {
                isUnicursal = true;
            }

            return isUnicursal;

        }

        public Dictionary<Vertice, List<Vertice>> GetComplementar()// são as arestas que faltam para um G ser completo
        {
            Dictionary<Vertice, List<Vertice>> complementar = new Dictionary<Vertice, List<Vertice>>();
            if (!IsCompleto())
            {
                foreach (Vertice v in grafo.Keys)
                {
                    List<Vertice> verticesFiltrados = grafo.Keys.Where(vertice => vertice != v).ToList();// lista com os outros vértices do grafo

                    List<Vertice> listaDeAdj;

                    if (grafo.TryGetValue(v, out listaDeAdj))
                    {
                        if (verticesFiltrados.Count == listaDeAdj.Count && !verticesFiltrados.Except(listaDeAdj).Any()) // se as duas listas forem iguais o vértice está ligado aos outros 
                        {
                            complementar.Add(v, new List<Vertice>());//adicionar ele no complementar com uma lista vazia/nula
                        }
                        else
                        {
                            List<Vertice> verticesFaltam = verticesFiltrados.Except(listaDeAdj).ToList();// os vértices que o v não está ligado
                            complementar.Add(v, verticesFaltam);
                        }

                    }

                }

            }
            ImprimirGrafo(complementar);

            return complementar;
        }

        //Grafo GetAGMPrim(Vertice v1)
        //{

        //}
        //Grafo GetAGMKruskal()
        //{

        //}
        //int GetCutVertices()
        //{

        //}

        //-------------- Grafos Direcionados 

        public int GetGrauEntrada(Vertice v1)
        {
            int grauEntrada = 0;
            List<List<Vertice>> listasComVertice = grafo.Values.Where(lista => lista.Contains(v1)).ToList();// recupera as listas que contem o vértice
            listasComVertice.ForEach(lista =>
            {
                grauEntrada += lista.Count(vertice => vertice == v1);

            });

            return grauEntrada;
        }

        public int GetGrauSaida(Vertice v1)
        {
            int grauSaida = 0;
            List<Vertice> listaDeAdj;
            if (grafo.TryGetValue(v1, out listaDeAdj))
            {
                grauSaida = listaDeAdj.Count;
            }
            return grauSaida;
        }


        bool hasCiclo = false; int tempo;
        public bool HasCiclo()// travessia em profundidade, se tiver aresta de retorno o grafo é ciclico
        {
            hasCiclo = false;
            TravessiaProfundidade();
            return hasCiclo;
        }

        private void TravessiaProfundidade()
        {
            foreach (Vertice v in grafo.Keys)
            {
                v.cor = "branco";
                v.pred = null;
            }
            tempo = 0;
            foreach (Vertice v in grafo.Keys)
            {
                if (v.cor.Equals("branco"))
                {
                    Visita(v, tempo, v.cor);
                }
            }
        }

        private void Visita(Vertice v, int tempo, string cor)
        {
            v.cor = "azul";
            v.descoberta = ++tempo;
            List<Vertice> listaDeAdj;
            if(grafo.TryGetValue(v, out listaDeAdj))
            {
                foreach(Vertice vertice in listaDeAdj)
                {
                    if (vertice.cor.Equals("branco"))
                    {
                        vertice.pred = v;
                        Visita(vertice, tempo, vertice.cor);
                    }else if (vertice.cor.Equals("azul"))// esta visitando um vértice já visitado, ou seja tem ciclo
                    {
                        hasCiclo = true;
                    }
                }
                v.cor = "veremelho";
                v.termino = ++tempo;
            }
        }

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
