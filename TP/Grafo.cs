using System.Collections;
using System.Collections.Generic;

namespace TP
{
    public class Grafo
    {

        Dictionary<Vertice, List<Vertice>> grafo = new Dictionary<Vertice, List<Vertice>>();

        /*      
            Key, Value -> no caso a nossa key é um vértice e o value é uma Lista de vértices
            
            [v1][v1 -> v2 -> v3]
            [v2][ListaDeAdj]
        */

        public bool IsAdjacente(Vertice v1, Vertice v2)
        {
            bool isAdj = false;
            List<Vertice> subLista;
            if (grafo.TryGetValue(v1, out subLista)) // O TryGetValue retorna um booleano, se encontra o value ele é passado pra variável no out
            {
                if (subLista.Contains(v2))
                {
                    isAdj = true;
                }
            }

            return isAdj;

        }
        public int getGrau(Vertice v1)
        {

        }
        bool isIsolado(Vertice v1)
        {

        }
        bool isPendente(Vertice v1)
        {

        }
        bool isRegular()
        {

        }
        bool isNulo()
        {

        }
        bool isCompleto()
        {

        }
        bool isConexo()
        {

        }
        bool isEuleriano()
        {

        }
        bool isUnicursal()
        {

        }
        Grafo getComplementar()
        {

        }
        Grafo getAGMPrim(Vertice v1)
        {

        }
        Grafo getAGMKruskal()
        {

        }
        int getCutVertices()
        {

        }

    }
}
