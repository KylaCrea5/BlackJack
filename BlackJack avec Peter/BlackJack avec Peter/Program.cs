using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPBlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            //Introduction du Joueur et demande du prénom.
            string prenom;
            Console.WriteLine("???: Bonjour cher(e) Ami(e)");
            Thread.Sleep(1000);
            Console.WriteLine("???:Asseyez vous je vous prie(e)");
            Thread.Sleep(1000);
            Console.WriteLine("???: Avant de Commencé a Jouez, je me présente");
            Thread.Sleep(1000);
            Console.WriteLine("Peter: Je M'appelle Peter");
            Thread.Sleep(1000);
            Console.WriteLine("Peter: Il y a pas a dire, je suis plutot connue dans le coins");
            Thread.Sleep(1000);
            Console.WriteLine("Peter: cependant, je vous ai jamais vu(e), comment vous appelez vous ?");
            Thread.Sleep(500);
            Console.Write("Comment vous appelez vous : ");
            prenom = Console.ReadLine();

            //Si le prénom est le même que l'adversaire 𐑀lors il fait une remarque dessus ou si aucun prénom, 𐑀lors il choisi votre prénom
            if (prenom.ToLower() == "peter")
            {
                Console.WriteLine("\nPeter: Oh, quel Coincidence, on dirait presque que c'est fait expret ahah");
                Thread.Sleep(750);
            }
            else if (prenom == "" || prenom == " ")
            {
                Console.WriteLine("\nPeter : Si vous souhaiter pas me donner votre prénom, laissez moi vous appelez Dimansy");
                Thread.Sleep(1250);
                Console.WriteLine("Peter: Sa me rappélera des Souvenir");
                Thread.Sleep(750);
                prenom = "Dimansy";
            }


            //rencontre
            string pret;
            Console.WriteLine($"\nPeter: heureux de vous rencontre!");
            Thread.Sleep(1000);
            Console.WriteLine($"Peter: Prêt(e) a jouer, {prenom} ?");
            Thread.Sleep(500);

            //Demande si il est prêt et fait une remarque si la réponse est inatendable
            Console.Write("Prêt(e) (o/n) : ");
            pret = Console.ReadLine();
            while (pret != "o" && pret != "n")
            {
                Console.WriteLine("\nPeter: Je.. Vous êtes sur de jouer dans cet état ? ");
                Console.Write("Prêt(e) (o/n) : ");
                pret = Console.ReadLine();
                if (pret == "o" || pret == "n")
                {
                    Console.WriteLine("\nPeter: Comme vous souhaité(e)");
                    break;
                }
            }

            //si pas prêt alors dis de repasser plus tard
            if (pret == "n")
            {
                Console.WriteLine("\nPeter: Bien, Merci de la conversation");
                Console.WriteLine("Peter: Si jamais vous voudriez jouer au blackJack un jour, au plaisir de vour revoir");
            }

            //si prêt, alors demande avec combien de paquet
            else
            {
                bool siPaquet;
                int nbpaquet = 0;
                Console.WriteLine("\nPeter: Ah bien commençon, je m'occuppe des cartes");
                Console.WriteLine($"Peter: A vos cartes {prenom}");
                Thread.Sleep(1000);
                Console.WriteLine("\nPeter: A ce propos, j'ai 6 paquets");
                Thread.Sleep(1000);
                Console.WriteLine("Peter: Vous voulez jouée avec combien de paquet ?");
                
                //Demande combien de paquet, si la réponse est pas valide, redemande et ce moque
                Console.Write("Combien de Paquet (1 a 6) : ");
                siPaquet = int.TryParse(Console.ReadLine(), out nbpaquet);

                while (nbpaquet < 1 || nbpaquet > 6 || siPaquet == false)
                {
                    Console.WriteLine("\nPeter: Je crain ne pas pouvoir vous proposé ses regles");
                    Console.Write("Combien de Paquet (1 à 6) : ");
                    siPaquet = int.TryParse(Console.ReadLine(), out nbpaquet);
                    if (nbpaquet > 0 && nbpaquet < 7)
                    {
                        break;
                    }
                }
                Console.WriteLine();

                //création de la valeur des carte avec une boucle
                Dictionary<string, int> ValeurCarte = new Dictionary<string, int> { };
                for (int Arf = 1; Arf < 11; Arf++)
                {
                    ValeurCarte[Arf.ToString()] = Arf;
                    if (Arf == 10)
                    {
                        ValeurCarte["V"] = 10;
                        ValeurCarte["D"] = 10;
                        ValeurCarte["R"] = 10;
                    }
                }

                //création du paquet avec le nombre de paquet + le nombre de couleur et les valeur de carte existente
                List<string> paquet = new List<string> { };
                for (int i = 1; i < nbpaquet + 1; i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        foreach (string cart in ValeurCarte.Keys)
                        {
                            paquet.Add(cart);
                        }
                    }
                }
                
                
                //création de la boucle de si on veut rejouer ou non
                var ran = new Random();
                bool end = false;

                while (end == false)
                {

                    //création du paquet mélanger et des main des joeur
                    List<string> JoueurH = new List<string> { };
                    List<string> JoueurO = new List<string> { };
                    var paquetShuf = paquet.OrderBy(x => ran.Next()).ToList();
                    for (int ri = 1; ri < 3; ri++)
                    {
                        JoueurH.Add(paquetShuf.Last());
                        paquetShuf.RemoveAt(paquetShuf.Count() - 1);
                        JoueurO.Add(paquetShuf.Last());
                        paquetShuf.RemoveAt(paquetShuf.Count() - 1);
                    }


                    //création des variable d'arrête des deux joueur et de la boucle qui jouer une Partie
                    bool StopJoueur = false;
                    bool StopOrdi = false;
                    bool FinParti = false;

                    while (FinParti == false)
                    {

                        //Comptabilisation des Scores des joeur et de l'affichage des cartes grace a une boucle qui regarde tout les élément des main
                        int ScoreJ = 0;
                        int ScoreO = 0;
                        string jeuJ = "";
                        string jeuO = "";
                        foreach (string cj in JoueurH) 
                        { 
                            jeuJ = jeuJ + " |" + cj + "|";
                            //si le Joueur a un As et un score inférieur ou égale a 10 alors L'as vaut 11
                            if(cj == "1" && ScoreJ <= 10)
                            {
                                ScoreJ += 11;
                            }
                            else {
                                ScoreJ += ValeurCarte[cj];
                            }
                        }
                        for (int recarto = 0; recarto < JoueurO.Count(); recarto++)
                        {
                            if (recarto == 0) jeuO += " |?|";
                            else jeuO = jeuO + " |" + JoueurO[recarto] + "|";
                            //si le Joueur a un As et un score inférieur ou égale a 10 alors L'as vaut 11
                            if (JoueurO[recarto] == "1" && ScoreO <= 10)
                            {
                                ScoreO += 11;
                            }
                            else
                            {
                                ScoreO += ValeurCarte[JoueurO[recarto]];
                            }
                        }
                        //Affichage du jeu
                        Console.WriteLine($"({ScoreJ}pts) {prenom} :{jeuJ}                 (?pts) Peter :{jeuO}");


                        //si joueur Humain et l'ordinateur on blackjack en même temps alors remarque spécial et fin de Parti
                        if (BlackJack(ScoreJ, JoueurH) == true && BlackJack(ScoreO, JoueurO) == true)
                        {
                            Console.WriteLine("Peter: ...Je n'en revient pas mes yeux.");
                            Thread.Sleep(1000);
                            Console.WriteLine("Peter: On n'aurait tout les deux blackjack... inatendu.");
                            Thread.Sleep(1000);
                            Console.WriteLine("Vous Avez fait Égalité\n");
                            Thread.Sleep(3000);
                            Console.WriteLine("... Mais Woah quand même.");
                            Thread.Sleep(1000);
                            break;
                        }

                        //si Joueur Humain mais pas Ordi a un Blackjact alors remarque, gagné, fin de parti
                        if (BlackJack(ScoreJ, JoueurH) == true)
                        {
                            Console.WriteLine("Peter: BlackJack... il semblerais que vous avez gagné");
                            Thread.Sleep(1000);
                            Console.WriteLine("Peter: On peut vraiment dire que la chance sourri au debutant");
                            Thread.Sleep(1000);
                            Console.WriteLine("Vous Avez Gagner\n");
                            Thread.Sleep(3000);
                            break;
                        }

                        //sinon si Score du Joueur au dessus de 21 alors remarque, perd, fin de parti
                        else if (ScoreJ > 21)
                        {
                            Console.WriteLine("Peter: Ah, je crois que c'est Echec et Mat pour vous");
                            Thread.Sleep(1000);
                            Console.WriteLine("Peter: en même temps, vous avez surement un surplus d'audace");
                            Thread.Sleep(1000);
                            Console.WriteLine("Vous Avez Perdu\n");
                            Thread.Sleep(3000);
                            break;
                        }

                        //si Joueur Ordinateur mais pas Humain a un Blackjack alors remarque, perdu, fin de parti
                        if (BlackJack(ScoreO, JoueurO) == true)
                        {
                            Console.WriteLine("Peter: J'ai Gagné hehe");
                            Thread.Sleep(1000);
                            Console.WriteLine("Peter: J'ai un BlackJack, ce n'est que du CALCUL");
                            Thread.Sleep(1000);
                            Console.WriteLine("Vous Avez Perdu\n");
                            Thread.Sleep(3000);
                            break;
                        }

                        //sinon si le Score de L'ordinateur est supérieur a 21 alors remarque, gagné, fin de partie
                        else if (ScoreO > 21)
                        {
                            Console.WriteLine($"Peter: Orf.. j'ai Perdu je crois bien, en même temps {ScoreO}pts!");
                            Thread.Sleep(1000);
                            Console.WriteLine("Peter: J'ai du faire une erreur de calcul.. vous trichez pas ?");
                            Thread.Sleep(1000);
                            Console.WriteLine("Vous Avez Gagné\n");
                            Thread.Sleep(3000);
                            break;
                        }

                        //si Joueur Arrête de Piocher et L'ordinateur aussi, alors, fin de parti.
                        if (StopJoueur == true && StopOrdi == true)
                        {
                            FinParti = true;
                            Console.WriteLine("\nPeter: Ah c'est fini, voyons qui a gagné dans ce cas");
                            Thread.Sleep(2000);

                            //si le Score du joueur est plus grand que celui de l'ordinateur alors remarque, gagné, fin de parti
                            if (ScoreJ > ScoreO)
                            {
                                Console.WriteLine($"Peter: On dirait que vous avez gagné de pas grand choses, j'avais {ScoreO}pts");
                                Thread.Sleep(1000);
                                Console.WriteLine("Peter: même pour cet petite victoir, Felicitation");
                                Thread.Sleep(1000);
                                Console.WriteLine("Vous Avez Gagné\n");
                                Thread.Sleep(3000);
                            }

                            //sinon si le Score de l'ordinateur est plus grand que celui du Joueur alors remarque, gagné, fin de parti
                            else if (ScoreO > ScoreJ)
                            {
                                Console.WriteLine($"Peter: Ah et enfin nous y voila, ma Victoire avec {ScoreO}pts");
                                Thread.Sleep(1000);
                                Console.WriteLine("Peter: Ce fus serrer mais me battre serrai miraculé");
                                Thread.Sleep(1000);
                                Console.WriteLine("Vous Avez Perdu\n");
                                Thread.Sleep(3000);
                            }

                            //sinon, alors remarque, égalité, fin de parti
                            else
                            {
                                Console.WriteLine("Peter: Oh.. une fin un peu dommage");
                                Thread.Sleep(1000);
                                Console.WriteLine("Peter:  c'est assez rare mais on dirait Match Nul");
                                Thread.Sleep(1000);
                                Console.WriteLine("Vous Avez fait Égalité\n");
                                Thread.Sleep(3000);
                            }
                            break;
                        }


                        //si le joueur continu de pioché alors demande si il veut pioché cet fois ci
                        if (StopJoueur == false)
                        {
                            string choix;
                            Console.WriteLine($"\nPeter: {prenom}, A vous de Jouez");
                            Console.WriteLine("Peter: Souhaité vous pioché une carte ?");
                            Console.Write("Pioché (o/n) : ");
                            choix = Console.ReadLine();

                            while (choix != "o" && choix != "n")
                            {
                                Console.WriteLine("\nPeter: Vous êtes la ?");
                                Console.Write("Pioché (o/n) : ");
                                choix = Console.ReadLine();
                                if (choix != "o" && choix != "n")
                                {
                                    break;
                                }
                            }

                            //si il pioche alors rajoute une carte et enlève la carte du paquet
                            if (choix == "o")
                            {
                                Console.WriteLine($"\n{prenom}: Je Pioche ! ");
                                JoueurH.Add(paquetShuf.Last());
                                paquetShuf.RemoveAt(paquetShuf.Count() - 1);
                                Thread.Sleep(1000);
                                Console.WriteLine($"Peter: ahah voila qui est osé!");
                            }

                            //sinon, alors arrêter de pioché et s'ârrête de pioché
                            else
                            {
                                Console.WriteLine($"\n{prenom}: Je m'arrête la ");
                                StopJoueur = true;
                                Thread.Sleep(1000);
                                Console.WriteLine("Peter: Peut être votre Erreur Fatal ahah");
                            }
                        }

                        //si l'ordinateur continu de pioché alors demande si il veut pioché cet fois ci
                        if (StopOrdi == false)
                        {
                            Console.WriteLine("\nPeter: A Moi de jouer!");
                            Thread.Sleep(1500);

                            //compte son score et si score inférieur alors pioche sinon non
                            int Persoordi = 0;
                            string ChoixOrdi;
                            foreach (string cardordi in JoueurO)
                            {
                                Persoordi += ValeurCarte[cardordi];
                            }

                            if (Persoordi < 17)
                            {
                                ChoixOrdi = "o";
                            }
                            else
                            {
                                ChoixOrdi = "n";
                            }

                            //si il pioche alors rajoute une carte et enlève la carte du paquet
                            if (ChoixOrdi == "o")
                            {
                                Console.WriteLine($"Peter: Je vais Pioché");
                                JoueurO.Add(paquetShuf.Last());
                                paquetShuf.RemoveAt(paquetShuf.Count() - 1);
                                Thread.Sleep(1000);
                                Console.WriteLine($"Peter: Et voila qu'y est jouer\n");
                            }

                            // sinon, alors arrêter de pioché et s'ârrête de pioché
                            else
                            {
                                Console.WriteLine($"Peter: Je ne pioche pas");
                                StopOrdi = true;
                                Thread.Sleep(1000);
                                Console.WriteLine("Peter: ce serra bien sufisant pour vous battre \n");
                            }
                        }

                        //Comptabilisation des Scores de l'ordinateur et de l'affichage des cartes grace a une boucle qui regarde tout les élément des main
                        for (int recarto = 0; recarto < JoueurO.Count(); recarto++)
                        {
                            if (recarto == 0) jeuO += " |?|";
                            else jeuO = jeuO + " |" + JoueurO[recarto] + "|";
                            if (JoueurO[recarto] == "1" && ScoreO <= 10)
                            {
                                ScoreO += 11;
                            }
                            else
                            {
                                ScoreO += ValeurCarte[JoueurO[recarto]];
                            }
                        }
                    }

                    //Après la premier parti, demande si il veut continuer a jouer
                    string cont = "";
                    Console.WriteLine($"\nPeter: Alors cher(e) {prenom}, On continue ça ?");
                    Console.Write("Continuez (o/n) : ");
                    cont = Console.ReadLine();

                    while (cont != "o" && cont != "n")
                    {
                        Console.WriteLine("\nPeter: ...peut être que vous deviez vous arrêter ?");
                        Console.Write("Continuez (o/n) : ");
                        cont = Console.ReadLine();
                        if (cont == "o" || cont == "n")
                        {
                            break;
                        }
                    }

                    //si oui alors, lance une remarque sinon, sort de la boucle "end"
                    if (cont == "o")
                    {
                        Console.WriteLine($"\n{prenom}: Je vais continuez a joué");
                        Thread.Sleep(1000);
                        Console.WriteLine("Peter: Parfait ça, laissez moi les remelangé et on est reparti\n");
                    }
                    else
                    {
                        Console.WriteLine($"\n{prenom}: si vous me l'accordez, je vais arrêter la");
                        Thread.Sleep(1000);
                        Console.WriteLine($"Peter: Dans ce cas, bonne journée {prenom}");
                        Thread.Sleep(1000);
                        Console.WriteLine("Peter: Et la prochaine fois, j'espère vous voir venir avec de l'argent ahah");
                        Thread.Sleep(1000);
                        Console.WriteLine("Peter: Ciao!");
                        break;
                    }
                }
            }
        }


        //Fonction pour detecter un BlackJack
        public static bool BlackJack(int Score, List<string> Joueur)
        {
            //si le joueur posséde 2 carte et une as et une figure
            if (Joueur.Count() == 2)
            {
                if (Joueur.Contains("1"))
                {
                    if (Joueur.Contains("V")) { return true; }
                    else if (Joueur.Contains("D")) { return true; }
                    else if (Joueur.Contains("R")) { return true; }
                }
            }

            //si le joueur a un score de 21
            if (Score == 21) return true;

            return false;
        }
    }
}