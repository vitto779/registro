using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace registroEletronico
{
    internal class Studente
    {
        public string StudenteNome { get; set; }
        public string StudenteCognome { get; set; }
        public int Classe { get; set; }
        public string Sezione { get; set; }

        List<Voto> Voti = new List<Voto>();

        public Studente(string studenteNome, string studenteCognome, int classe, string sezione)
        {
            StudenteNome = studenteNome;
            StudenteCognome = studenteCognome;
            Classe = classe;
            Sezione = sezione;
        }
        public double CalcolaMediaVoti()
        {
            if (!Voti.Any()) return 0;
            return Voti.Average(v => v.IdVoto);
        }

        public void AggiungiVoto(int idVoto, string materia)
        {
            Voto voto = new Voto(idVoto, materia);
            Voti.Add(voto);
        }

        public List<Voto> OrdinaPerMateria(string materia)
        {
            return Voti
                .Where(v => string.Equals(v.Materia, materia, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public double CalcolaMediaPerMateria(string materia)
        {
            var votiPerMateria = OrdinaPerMateria(materia);
            if (!votiPerMateria.Any()) return 0;
            return votiPerMateria.Average(v => v.IdVoto);
        }

        public List<Voto> GetVoti()
        {
            return Voti;
        }

        public List<string> materieInsufficenze()
        {
            List<string> materieConInsufficienza = new List<string>();
            var tutteLeMaterie = Voti.Select(v => v.Materia).Distinct();

            foreach (var materia in tutteLeMaterie)
            {

                if (CalcolaMediaPerMateria(materia) < 6.0)
                {
                    materieConInsufficienza.Add(materia);
                }
            }

            return materieConInsufficienza;

        }




    }
}