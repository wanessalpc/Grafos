using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP
{
    public class Vertice
    {
        public int peso;
        public string nome;
        public string cor;
        public Vertice pred;
        public int distancia;
        public int descoberta;
        public int termino;
        public string chefe;
        public Vertice pai;

        public Vertice()
        {

        }
        public Vertice(string nome, int peso)
        {
            this.nome = nome;
            this.peso = peso;
        }

    }
}
