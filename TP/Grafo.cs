using System.Collections;
using System.Collections.Generic;

namespace TP
{
    public class Grafo
    {
        List<List<Vertice>> grafo = new List<List<Vertice>>();

        /*
        grafo - [
        
        [v1, ...],
        [v2, ...],
        [v3, ...]
        ]   
        
         Vértice inicial representa a sub lista do mesmo
         v1  -> /
         v2  -> v2 -> v3
         v3  -> v1 -> v1 -> v2
        */

        Dictionary<Vertice, List<Vertice>> grafo3 = new Dictionary<Vertice, List<Vertice>>();

        public bool IsAdjacente(Vertice v1, Vertice v2)
        {
            bool isAdj = false;
            List<Vertice> subLista;
            if (grafo3.TryGetValue(v1, out subLista)) // tenta pegar o valor na lista, se não encontrar
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
