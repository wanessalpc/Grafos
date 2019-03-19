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

        public bool isAdjacente(Vertice v1, Vertice v2)
        {
            bool isAdj = false;
            grafo.ForEach(subLista =>
            {
                if (subLista.Contains(v1) && subLista.Contains(v2))
                {
                    int indexV1 = subLista.IndexOf(v1);
                    int indexV2 = subLista.IndexOf(v2);
                    if (indexV1 - 1 == indexV2 || indexV1 + 1 == indexV2)
                    {
                        isAdj = true;
                    }
                }
            });
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
