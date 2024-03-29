﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TP
{
    public class Grafo
    {
        public bool isDirecionado = false;
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
            if (grafo.TryGetValue(BuscaVerticeReal(v1), out listaDeAdj)) // O TryGetValue retorna um booleano, se encontra o value ele é passado pra variável no out
            {
                if (Contem(BuscaVerticeReal(v2), listaDeAdj) != null)
                {
                    isAdj = true;
                }
            }


            return isAdj;

        }

        public int GetGrau(Vertice v1)
        {
            List<Vertice> listaDeAdj;
            if (grafo.TryGetValue(BuscaVerticeReal(v1), out listaDeAdj))
            {
                Console.WriteLine("Grau de " + v1.nome + ": " + listaDeAdj.Count);
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
            if (grafo.TryGetValue(BuscaVerticeReal(v1), out listaDeAdj))
            {
                if (listaDeAdj.Count() == 0 || (listaDeAdj.Count() == 1 && Contem(v1, listaDeAdj) != null))// se a lista estiver ou se ela conter um elemento e este for o próprio vértice(loop)
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
            if (grafo.TryGetValue(BuscaVerticeReal(v1), out listaDeAdj))
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

                    if (Contem(v, listaDeAdj) == null)//não tiver loops
                    {

                        if (verticesFiltrados.Count == listaDeAdj.Count && verticesFiltrados.Where(v1 => listaDeAdj.Any(v2 => v2.nome.Equals(v1.nome))).Count() > 0)
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
                        Vertice verticeReal = BuscaVerticeReal(vertice);
                        if (verticeReal.cor.Equals("branco"))
                        {
                            verticeReal.cor = "azul";
                            verticeReal.distancia = removido.distancia + 1;
                            verticeReal.pred = removido;
                            fila.Enqueue(verticeReal);
                        }
                    }
                    removido.cor = "vermelho";
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

            if (numeroVerticesImpares == 2)
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

                        if (verticesFiltrados.Count == listaDeAdj.Count &&
                            verticesFiltrados.Where(v1 => listaDeAdj.Any(v2 => v2.nome.Equals(v1.nome))).Count() > 0) // se as duas listas forem iguais o vértice está ligado aos outros 
                        {
                            complementar.Add(v, new List<Vertice>());//adicionar ele no complementar com uma lista vazia/nula
                        }
                        else
                        {
                            if (IsIsolado(v))// se o vertice estiver isolado
                            {
                                complementar.Add(v, verticesFiltrados);// adiciona todos os outros vértices que ele não se liga
                            }
                            else
                            {
                                List<Vertice> verticesFaltam = verticesFiltrados.Where(v1 => !listaDeAdj.Any(v2 => v2.nome.Equals(v1.nome))).ToList();// os vértices que o v não está ligado
                                complementar.Add(v, verticesFaltam);
                            }
                        }
                    }
                }
                ImprimirGrafo(complementar);
            }
            else
            {
                Console.WriteLine("O grafo é completo");
            }


            return complementar;
        }

        public Dictionary<Vertice, List<Vertice>> GetAGMPrim(Vertice v1)
        {
            List<Vertice> incluidos = new List<Vertice>();// conjunto dos verticess já incluidos em T
            List<Vertice> borda = new List<Vertice>();// conjuntos dos vertices candidatos a serem escolhidos na proxima iteração, para serem incluidos em T
            Dictionary<Vertice, int> custo = new Dictionary<Vertice, int>();// custo da inclusão do vértice v em T
            Dictionary<Vertice, List<Vertice>> T = new Dictionary<Vertice, List<Vertice>>();
            foreach (Vertice v in grafo.Keys)
            {
                custo.Add(v, int.MaxValue);// add os vértices originais
            }
            borda.Add(BuscaVerticeReal(v1));
            custo[BuscaVerticeReal(v1)] = 0;

            while (borda.Count > 0 && custo.Count > 0)
            {
                // setar o menor custo para primeira pos   
                var menorCusto = custo.OrderBy(kvp => kvp.Value).First();// ordena o custo para o menor custo ficar em primeiro

                Vertice vComMenorCusto = borda.Find(v => v.nome == menorCusto.Key.nome);// encontra na borda onde fica o vértice com menor custo
                vComMenorCusto.peso = custo[vComMenorCusto];
                incluidos.Add(vComMenorCusto);
                Console.WriteLine("Inserido: " + vComMenorCusto.nome);
                custo.Remove(vComMenorCusto);
                borda.Remove(vComMenorCusto);

                foreach (Vertice x in grafo.Keys)
                {
                    if (Contem(x, incluidos) == null)
                    {
                        if (custo.TryGetValue(x, out int peso))
                        {
                            if (Existe(vComMenorCusto, x) && peso > Peso(vComMenorCusto, x))
                            {
                                peso = Peso(vComMenorCusto, x);
                                custo[x] = peso;
                                x.pai = vComMenorCusto;
                                borda.Add(x);
                            }
                        }
                    }
                }
            }

            foreach (Vertice v in incluidos)
            {
                T = AdicionarNoGrafo(T, v.pai, v, isDirecionado);
            }

            return T;

        }

        private bool Existe(Vertice v1, Vertice v2)// se existe aresta entre os vértices
        {
            bool existe = false;
            if (grafo.TryGetValue(v1, out List<Vertice> listaDeAdj))
            {
                if (listaDeAdj.Find(vertice => vertice.nome == v2.nome) != null)//existe aresta entre os dois vértices
                {
                    existe = true;
                }
            }

            return existe;
        }
        private int Peso(Vertice v1, Vertice v2)// o peso da aresta  
        {
            Vertice v2NaListaDeV1 = new Vertice();
            if (grafo.TryGetValue(v1, out List<Vertice> listaDeAdj))
            {
                v2NaListaDeV1 = listaDeAdj.Find(vertice => vertice.nome == v2.nome); // o vértice na lista de v1 terá um peso diferente de v2
            }

            return v2NaListaDeV1.peso;
        }

        public Dictionary<Vertice, List<Vertice>> GetAGMKruskal()
        {
            //Arestas do grafo
            List<Vertice> arestas = new List<Vertice>();
            foreach (Vertice v in grafo.Keys)
            {
                v.chefe = v.nome;
                if (grafo.TryGetValue(v, out List<Vertice> listaAdj))
                {
                    listaAdj.ForEach(vertice =>
                    {
                        vertice.pai = v;
                        vertice.chefe = vertice.nome;
                        arestas.Add(vertice);
                    });
                }
            }

            arestas.Sort((a, b) => a.peso.CompareTo(b.peso));
            Dictionary<Vertice, List<Vertice>> T = new Dictionary<Vertice, List<Vertice>>();
            int nIncluidos = 0;
            int cont = 0;

            while (nIncluidos < grafo.Keys.Count - 1)
            {
                Vertice aresta = arestas[0];

                if (!aresta.chefe.Equals(aresta.pai.chefe))
                {
                    T = AdicionarNoGrafo(T, aresta.pai, aresta, isDirecionado);
                    arestas.Remove(aresta);
                    nIncluidos++;
                }
                cont++;
            }

            return T;
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
                            if (Contem(key, listaAdj) == null)
                            {
                                Vertice keyAux = new Vertice();
                                keyAux.nome = key.nome;
                                keyAux.peso = value.peso;
                                listaAdj.Add(keyAux);
                            }
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

        /*
        public int GetCutVertices()// Remover os vértices com maior lista de adj
        {
            int numeroDeCutVertices = 0;// numero minimo de vertices que se pode tirar para tornar o grafo desconexo
            int maiorLista = Int32.MinValue;
            foreach (Vertice x in grafo.Keys)
            {
                if (grafo.TryGetValue(x, out List<Vertice> listaAdj))
                {
                    if (listaAdj.Count > maiorLista)
                    {
                        maiorLista = listaAdj.Count;//pega o tamanho da maior lista
                    }
                }
            }

            Vertice v;// vértice com lista de adj grande

            List<Vertice> verticesFiltrados = grafo.Keys.Where(vertice => vertice != v).ToList();// lista com os outros vértices do grafo

            foreach (Vertice x in verticesFiltrados)
            {
                if (grafo.TryGetValue(x, out List<Vertice> listaAdj))
                {
                    Vertice verticeNaLista = listaAdj.Find(vertice => vertice.nome.Equals(v.nome));
                    if (verticeNaLista != null)
                    {
                        listaAdj.Remove(verticeNaLista);
                        numeroDeCutVertices++;
                        ImprimirGrafo(grafo);
                        Console.WriteLine("------------------------");
                        if (!IsConexo())
                        {
                            return numeroDeCutVertices;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }


            return numeroDeCutVertices;



        }
        */
        //-------------- Grafos Direcionados 

        public int GetGrauEntrada(Vertice v1)
        {
            int grauEntrada = 0;
            List<List<Vertice>> listasComVertice = grafo.Values.Where(lista => Contem(BuscaVerticeReal(v1), lista) != null).ToList();// recupera as listas que contem o vértice
            listasComVertice.ForEach(lista =>
            {
                grauEntrada += lista.Count(vertice => vertice.nome == v1.nome);

            });
            Console.WriteLine("Grau de entrada de " + v1.nome + ": " + grauEntrada);
            return grauEntrada;
        }

        public int GetGrauSaida(Vertice v1)
        {
            int grauSaida = 0;
            List<Vertice> listaDeAdj;
            if (grafo.TryGetValue(BuscaVerticeReal(v1), out listaDeAdj))
            {
                grauSaida = listaDeAdj.Count;
            }
            Console.WriteLine("Grau de saida de " + v1.nome + ": " + grauSaida);
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
            if (grafo.TryGetValue(v, out listaDeAdj))
            {
                foreach (Vertice vertice in listaDeAdj)
                {
                    Vertice verticeReal = BuscaVerticeReal(vertice);
                    if (verticeReal.cor.Equals("branco"))
                    {
                        verticeReal.pred = v;
                        Visita(verticeReal, tempo, verticeReal.cor);
                    }
                    else if (verticeReal.cor.Equals("azul"))// esta visitando um vértice já visitado, ou seja tem ciclo
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
                Console.Write("Vértice " + v.nome + ":");
                if (grafo.TryGetValue(v, out listaDeAdj))
                {
                    if (listaDeAdj.Count > 0)
                    {
                        listaDeAdj.ForEach(vertice => Console.Write(" -> " + vertice.nome + "/" + vertice.peso));
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

        private Vertice Contem(Vertice v, List<Vertice> lista)// verifica se o vértice ve está dentro da lista e retorna o vértice original
        {
            Vertice verticeNaLista = lista.Find(vertice => vertice.nome == v.nome);
            if (verticeNaLista == null)
            {
                return null;
            }
            else
            {
                return grafo.Keys.Where(ver => ver.nome == verticeNaLista.nome).ElementAt(0);
            }
        }

        private Vertice BuscaVerticeReal(Vertice v)
        {
            return grafo.Keys.Where(vertice => vertice.nome == v.nome).ElementAt(0);
        }//busca o vértice na lista de vértices(keys)

    }
}
