using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace registroEletronico
{
    internal class Program
    {

        static void Main(string[] args)
        {
            RegistroElettronico registro = new RegistroElettronico();
            string nomeUtente;
            bool continua = true;
            bool prof = false;

            while (continua)
            {
                Console.WriteLine("Benvenuto/a nel registro!!!");
                Console.WriteLine("Come ti chiami?");
                nomeUtente = Console.ReadLine() ?? string.Empty;
                Console.Write("sei un prof o una studente? \n ->");
                string risposta = Console.ReadLine() ?? string.Empty;
                if (risposta.ToLower() == "prof")
                {

                    bool continueProf = true;
                    while (continueProf)
                    {
                        prof = true;
                        Console.WriteLine($"Benvenuto/a professore/ssa {nomeUtente}!");
                        Console.Write("Inserisca la sua materia di insegnamento: ");
                        string materiaInsegnamento = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Cosa vuoi fare? \n 1) Aggiungi studente \n 2) Aggiungi voto \n 3) Visualizza voti studente nella tua materia  \n 4) Visualizza materie con insufficienze studente  \n 5) uscire");
                        Console.WriteLine("Inserisca il numero ddella scelta");
                        Console.Write("->");
                        string scelta = Console.ReadLine() ?? string.Empty;
                        switch (scelta)
                        {
                            case "1":
                                Console.Write("Inserisca il nome dello studente: ");
                                string nome = Console.ReadLine() ?? string.Empty;
                                Console.Write("Inserisca il cognome dello studente: ");
                                string cognome = Console.ReadLine() ?? string.Empty;
                                Console.Write("Inserisca la classe dello studente (es. 1, 2, 3, 4, 5): ");
                                if (!int.TryParse(Console.ReadLine(), out int classe))
                                {
                                    Console.WriteLine("Classe non valida.");
                                    break;
                                }
                                Console.Write("Inserisca la sezione dello studente (es. a, b, c ...): ");
                                string sezione = Console.ReadLine() ?? string.Empty;

                                registro.AggiungiStudente(nome, cognome, classe, sezione);

                                break;
                            case "2":
                                if (registro.Studenti.Count == 0)
                                {
                                    Console.WriteLine("Non ci sono studenti nel registro. Aggiungi uno studente prima di aggiungere un voto.");
                                    break;
                                }
                                Console.WriteLine("Questi sono gli studenti, di chi vuoi vedere i voti ?");
                                registro.GetStudenti();
                                Console.Write("Inserisca il nome dello studente: ");
                                string studenteNome = Console.ReadLine() ?? string.Empty;
                                Console.Write("Inserisca il cognome dello studente: ");
                                string studenteCognome = Console.ReadLine() ?? string.Empty;
                                Console.Write("Inserisca la materia: ");
                                string materia = Console.ReadLine() ?? string.Empty;
                                Console.Write("Inserisca il voto (da 1 a 10): ");
                                if (!int.TryParse(Console.ReadLine(), out int idVoto))
                                {
                                    Console.WriteLine("Voto non valido.");
                                    break;
                                }
                                var studente = registro.Studenti
                                    .FirstOrDefault(s => s.StudenteNome.Equals(studenteNome, StringComparison.OrdinalIgnoreCase) && s.StudenteCognome.Equals(studenteCognome, StringComparison.OrdinalIgnoreCase));
                                if (studente != null)
                                {
                                    studente.AggiungiVoto(idVoto, materia);
                                    Console.WriteLine("Voto aggiunto con successo.");
                                }
                                else
                                {
                                    Console.WriteLine("Studente non trovato.");
                                }
                                break;
                            case "3":
                                if (registro.Studenti.Count == 0)
                                {
                                    Console.WriteLine("Non ci sono studenti nel registro. Aggiungi uno studente prima di aggiungere un voto.");
                                    break;
                                }
                                Console.WriteLine("Questi sono gli studenti, a quale vuoi assegnare un voto ?");
                                registro.GetStudenti();
                                Console.Write("Inserisca il nome dello studente: ");
                                studenteNome = Console.ReadLine() ?? string.Empty;
                                Console.Write("Inserisca il cognome dello studente: ");
                                studenteCognome = Console.ReadLine() ?? string.Empty;
                                studente = registro.Studenti
                                    .FirstOrDefault(s => s.StudenteNome.Equals(studenteNome, StringComparison.OrdinalIgnoreCase) && s.StudenteCognome.Equals(studenteCognome, StringComparison.OrdinalIgnoreCase));
                                if (studente != null)
                                {
                                    Console.WriteLine($"Voti: \n {string.Join(", ", studente.OrdinaPerMateria(materiaInsegnamento).Select(v => v.ToString()))}");
                                }
                                else
                                {
                                    Console.WriteLine("Studente non trovato.");
                                }
                                break;
                            case "4":
                                if (registro.Studenti.Count == 0)
                                {
                                    Console.WriteLine("Non ci sono studenti nel registro. Aggiungi uno studente prima di aggiungere un voto.");
                                    break;
                                }
                                Console.WriteLine("Questi sono gli studenti, a quale vuoi assegnare un voto ?");
                                registro.GetStudenti();
                                Console.Write("Inserisca il nome dello studente: ");
                                studenteNome = Console.ReadLine() ?? string.Empty;
                                Console.Write("Inserisca il cognome dello studente: ");
                                studenteCognome = Console.ReadLine() ?? string.Empty;
                                studente = registro.Studenti
                                    .FirstOrDefault(s => s.StudenteNome.Equals(studenteNome, StringComparison.OrdinalIgnoreCase) && s.StudenteCognome.Equals(studenteCognome, StringComparison.OrdinalIgnoreCase));
                                if (studente != null)
                                {
                                    Console.WriteLine($"Materie in cui {studenteNome} è insufficente: \n {string.Join(", ", studente.materieInsufficenze())}");
                                }
                                else
                                {
                                    Console.WriteLine("Studente non trovato.");
                                }
                                break;
                            case "5":
                                Console.WriteLine("Vuoi uscire dal registro (1) o cambiare account (2) ?");
                                Console.Write("->");
                                string uscita = Console.ReadLine() ?? string.Empty;
                                if (uscita == "1")
                                {
                                    continueProf = false;
                                    continua = false;
                                }
                                else if (uscita == "2")
                                {
                                    prof = false;
                                    continueProf = false;
                                }
                                break;
                            default:
                                Console.WriteLine("Scelta non valida.");
                                break;

                        }

                    }
                }
                else
                {

                    bool continueStud = true;
                    // L'utente studente DEVE essere cercato nel registro, altrimenti non ha voti.
                    // Inoltre la sezione e la classe devono essere cercate con il tipo corretto (string/int).

                    Console.WriteLine("è da talmente tanto che mi devi rinfrescare la memoria un attimo perchè non mi ricordo");
                    Console.Write("cognome: ");
                    string cognomeUtente = Console.ReadLine() ?? string.Empty;
                    Console.Write("classe: ");
                    if (!int.TryParse(Console.ReadLine(), out int classeUtente))
                    {
                        Console.WriteLine("Classe non valida. Accesso negato.");
                        continueStud = false;
                        break;
                    }
                    Console.Write("sezione: ");
                    string sezioneUtente = Console.ReadLine() ?? string.Empty;

                    Studente studente = registro.Studenti
                        .FirstOrDefault(s => s.StudenteNome.Equals(nomeUtente, StringComparison.OrdinalIgnoreCase) &&
                                             s.StudenteCognome.Equals(cognomeUtente, StringComparison.OrdinalIgnoreCase) &&
                                             s.Classe == classeUtente &&
                                             s.Sezione.Equals(sezioneUtente, StringComparison.OrdinalIgnoreCase));

                    if (studente == null)
                    {
                        Console.WriteLine("Studente non trovato nel registro. Accesso negato.");
                        continueStud = false;
                        break;
                    }

                    while (continueStud)
                    {
                        Console.WriteLine($"Cosa è successo per farti aprire a te il registro {nomeUtente} (°_°)");
                        Console.WriteLine("cosa vuoi fare? \n 1) Visualizza i tuoi voti \n 2) Visualizza le tue materie con insufficienze \n 3) Visualizzare i voti di una materia \n 4) Visualizzare la media totale \n 5) Visualizzare le medie nelle varie materie \n 6) uscire");
                        risposta = Console.ReadLine() ?? string.Empty;
                        switch (risposta)
                        {
                            case "1":
                                if (studente.GetVoti().Count == 0)
                                {
                                    Console.WriteLine("Speravi fossero comparsi voti mentre non c'eri?");
                                }
                                Console.WriteLine($"I tuoi voti sono: \n {string.Join("\n", studente.GetVoti().Select(v => v.ToString()))}");
                                break;
                            case "2":
                                var materieInsuff = studente.materieInsufficenze();
                                if (materieInsuff.Count == 0)
                                {
                                    Console.WriteLine("Ma quante verifiche avete fatto scusa?");
                                }
                                Console.WriteLine($"Le tue materie con insufficienze sono: \n {string.Join(", ", materieInsuff)}");
                                break;
                            case "3":

                                Console.Write("Inserisca la materia di cui vuole vedere i voti: ");
                                string materia = Console.ReadLine() ?? string.Empty;
                                var votiMateria = studente.OrdinaPerMateria(materia);
                                if (votiMateria.Count == 0)
                                {
                                    Console.WriteLine("Non ti ha ancora caricato il 2 tranquillo che forse si scorda");
                                }
                                Console.WriteLine($"I tuoi voti in {materia} sono: \n {string.Join(", ", votiMateria.Select(v => v.ToString()))}");
                                break;
                            case "4":
                                double mediaTotale = studente.CalcolaMediaVoti();
                                if (mediaTotale > 6.0)
                                {
                                    Console.WriteLine($"La tua media totale è: {mediaTotale:F2}, non pensavo ci saresti mai arrivato");
                                }
                                else
                                {
                                    Console.WriteLine($"La tua media totale è: {mediaTotale:F2}, tutto nella norma");
                                }
                                break;
                            case "5":
                                Console.Write("Inserisca la materia di cui vuole vedere la media: ");
                                materia = Console.ReadLine() ?? string.Empty;
                                double mediaMateria = studente.CalcolaMediaPerMateria(materia);
                                if (mediaMateria == 0)
                                {
                                    Console.WriteLine($"Nessun voto trovato in {materia}.");
                                }
                                else if (mediaMateria > 6.0)
                                {
                                    Console.WriteLine($"La tua media in {materia} è: {mediaMateria:F2}, non pensavo ci saresti mai arrivato");
                                }
                                else
                                {
                                    Console.WriteLine($"La tua media in {materia} è: {mediaMateria:F2}, tutto nella norma");
                                }
                                break;
                            case "6":
                                Console.WriteLine("Vuoi uscire dal registro (1) o cambiare account (2) ?");
                                Console.Write("->");
                                string uscita = Console.ReadLine() ?? string.Empty;
                                if (uscita == "1")
                                {
                                    continueStud = false;
                                    continua = false;
                                }
                                else if (uscita == "2")
                                {
                                    prof = false;
                                    continueStud = false;
                                }
                                break;
                            default:
                                Console.WriteLine("Scelta non valida.");
                                break;

                        }
                    }
                }
            }
        }


    }
}