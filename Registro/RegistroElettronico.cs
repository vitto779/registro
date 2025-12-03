using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registroEletronico
{
    internal class RegistroElettronico
    {
        public List<Studente> Studenti = new List<Studente>();
        public void AggiungiStudente(string nome, string cogn, int classe, string sezione)
        {
            Studente studente = new Studente(nome, cogn, classe, sezione);
            Studenti.Add(studente);

        }

        public List<Studente> GetStudenti()
        {
            foreach (var studente in Studenti)
            {
                Console.WriteLine($"Nome: {studente.StudenteNome}, Cognome: {studente.StudenteCognome}, Classe: {studente.Classe}, Sezione: {studente.Sezione} \n");
            }
            return Studenti;
        }


    }
}