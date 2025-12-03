using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registroEletronico
{
    internal class Voto
    {
        public int IdVoto { get; set; }
        public string Materia { get; set; }

        public Voto(int idVoto, string materia)
        {
            IdVoto = idVoto;
            Materia = materia;
        }

        public override string ToString()
        {
            return $"{Materia}: {IdVoto}";
        }
    }
}