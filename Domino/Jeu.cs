using System;
using System.Collections.Generic;
using System.Text;

namespace JeuDomino
{
    /// <summary>
    /// Classe qui modélise le jeu
    /// </summary>
    public class Jeu
    {  
        ///<summary>
        ///la constante de nombre de joueurs
        ///<summary>
        private const int NOMBRE_JOUEURS = 4;
        
        /// <summary>
        /// Peut on diviser par 4 le nombre total de domino? -oui , c'est une nouvelle constante lequelle 
        /// </summary>
        private const int NOMBRE_DOMINOS = Paquet.NOMBRE_DOMINOS / NOMBRE_JOUEURS;
        
        ///<summary>
        ///liste tableau de jeu
        ///<summary>
        private List<Domino> tableJeu;
        
        ///<summary>
        ///des champs qu'on utilise dans la classe
        ///<summary>
        private bool pas = false;
        private string message = "";
        private int turnJoueur;
        
        ///<summary>
        ///domino pour garde la valeur des droit et gauche dans la table. Pour savoir si le jouer à un domino adjacent à lui
        ///<summary>   
        private Domino point=new Domino(Domino.MAX_VALUE, Domino.MAX_VALUE);
 
        /// <summary>
        /// La liste de joueurs
        /// </summary>
        private Joueur[] joueurs;

        /// <summary>
        /// Le paquet de domino
        /// </summary>
        private Paquet paquet;

        /// <summary>
        /// La liste de joueurs
        /// </summary>
        public Joueur[] Joueurs { get => joueurs; set => joueurs = value; }
       
        /// <summary>
        /// Le constructeur du jeu qui crée un paquet et un tableau de 4 joueurs
        /// </summary>
        public Jeu()
        {
            paquet = new Paquet();
            Joueurs = new Joueur[NOMBRE_JOUEURS];

            for (int j = 0; j < NOMBRE_JOUEURS; j++)
            {
                Joueurs[j] = new Joueur($"Joueur {j + 1}");
            }

        }
           

        /// <summary>
        /// Jouer qui distribue 7 dominos à chaque joueur et les organise par valeur
        /// et affiche et commence la différent jeu
        /// </summary>
        public void Jouer()
        {
            ///<summary>
            ///des champs qu'on utilise dans ce méthode
            ///<summary>
            private int bonus = 10;
            private bool finiJeu;
            
            ///<summary>
            ///Liste pour garde les dominos sur la table
            ///<summary>
            tableJeu = new List<Domino>();
            
            for (int i = 0; i < NOMBRE_JOUEURS; i++)
            {
                Joueurs[i].Jeu = paquet.Distribue(NOMBRE_DOMINOS);
                Joueurs[i].Jeu = TrierTableau(Joueurs[i].Jeu);
            }
            
            for (int i = 0; i < NOMBRE_JOUEURS; i++)
            {
                Console.WriteLine($"{Joueurs[i]} valeur: {Joueurs[i].Valeur()}");
            }

                      
            finiJeu = false;

            for (int i = 0; ; i++)
            {
                int count = 0;
                message = "";

                //on trouve  le joueur qui débutera la partie est deplace lui a la position 0 dans la tableau de joueurs
                if (i == 0)
                {
                    PremierTurn();
                }

                for (int j = turnJoueur; j < NOMBRE_JOUEURS; j++)
                {                    
                    if (!pas)
                    {
                        ContinuationTurn();
                    }

                    //si le joueur n’a pas de domino sur les mains , il est gagnant
                    if (Joueurs[j].Jeu.Count == 0)
                    {
                        message = $"{Joueurs[j].NomJoueur} est gagnant(e)!!!";
                        Joueurs[j].Score += bonus;
                        finiJeu = true;
                        break;
                    }
                   
                    if (!pas) 
                    {
                         count++;
                        Console.WriteLine($"-> Joueur {Joueurs[j].NomJoueur} a passé son tour, count: {count} ");

                        //Les 4 joueurs ont dû passer leur tour
                        if (count == NOMBRE_JOUEURS)
                        {
                            Console.WriteLine($"Les 4 joueurs ont dû passer leur tour!!!");
                            finiJeu = true;
                        }                        
                    }
                    pas = false;
                }
                if (turnJoueur == 4)
                {
                    turnJoueur = 0;
                }
                if (finiJeu)
                {
                    AffichageValeurJou();
                    break;
                } 
            }
        }
        
        /// <summary>
        /// Boucle pour trouver le premier joueur(lui qui a le double six) avec l'utilisation de la méthode Double Six.
        /// </summary>
        private void PremierTurn()
        {
           
            for (int i = 0; i < NOMBRE_JOUEURS; i++)
            {               
                if (joueurs[i].DoubleSix())
                {
                    Domino domino = joueurs[i].DominoSorti();

                    tableJeu.Add(domino);
                    Console.WriteLine();
                    Console.WriteLine($" Joueur {i + 1} commence!!! ");

                    message=$"-> le Joueur {i + 1} a joué:  {domino}";                    
                    turnJoueur = i+1;
                    AffichageJou();
                    break;
                }
            }
          }

        /// <summary>
        /// Continuation du jeu, tester si le prochain joueur a un domino pour jouer, l'utilisation de la méthode Avoir Domino.
        /// </summary>
        private void ContinuationTurn()
         {
            //Si on arrive au dernier joueur on turne a le premier
            if (turnJoueur == 4)
            {
                turnJoueur = 0;
            }

            if (joueurs[turnJoueur].AvoirDomino(point))
            {
                Domino domino = joueurs[turnJoueur].DominoSorti();
                 message=$"->le Joueur {turnJoueur+1} a joue {domino}";

                if (domino.Gauche == tableJeu[0].Gauche)
                {
                    domino.Permute(domino);
                    tableJeu.Insert(0, domino);
                }
                else if (domino.Droite == tableJeu[0].Gauche)
                {
                    tableJeu.Insert(0, domino);
                }
                else if (domino.Droite == tableJeu[tableJeu.Count - 1].Droite)
                {
                    domino.Permute(domino);
                    tableJeu.Add(domino);
                }
                else
                {
                    tableJeu.Add(domino);
                }
                 

                AffichageJou();                 
                point.Gauche = tableJeu[0].Gauche;                
                point.Droite = tableJeu[tableJeu.Count - 1].Droite;
                // pour savoir si le joueur a joué o non
                pas = true;
            }

            //passez au prochain joueur
            turnJoueur++;
        }
        
        /// <summary>
        /// méthode qui organise les dominos par ordre de valeur, permet à l’ordinateur de jouer le domino le plus grand.
        /// </summary>
        /// <param name="test"></param>
        /// <returns>list de dominos organise pour valeur</returns>
        private List<Domino> TrierTableau(List<Domino> test)
        {
            int n = test.Count;
            int valeurMax=0;
            List < Domino > trie = new List<Domino>();
            for(int i = 0; i < n; i++)
            {
                valeurMax = test[0].Valeur();
                int index = 0;
               for(int j=1;j<test.Count;j++)
                {
                    
                    if (test[j].Valeur() > valeurMax) 
                    {
                        valeurMax = test[j].Valeur();
                        index=test.IndexOf(test[j]);
                    }
                }
                trie.Add(test[index]);
                test.Remove(test[index]);                             
            }
            return trie;
        }
        
        /// <summary>
        /// Affichage Jeu
        /// </summary>
        public void AffichageJou()
        {
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*******************JEU DOMINO******************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            for (int i = 0; i < NOMBRE_JOUEURS; i++)
            {
                Console.WriteLine($"{Joueurs[i]}");
             }
             Console.WriteLine();

            Console.WriteLine(message);
            AffichageTableJou();
        }

        /// <summary>
        /// Affichage le tableau de jeu
        /// </summary>
        public void AffichageTableJou()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("********************TABLEAU JEU***********************");
            foreach (Domino domino in tableJeu)
            {
                Console.Write(domino.ToString());
             }
            Console.WriteLine();
            Console.WriteLine("*************************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        /// <summary>
        /// affichage les valeur de jou et les scores
        /// </summary>
        public void AffichageValeurJou()
        {
            Console.WriteLine();

            for (int i = 0; i < NOMBRE_JOUEURS; i++)
            {
                Joueurs[i].Score += (100 - Joueurs[i].Valeur()) / 10;
            }
            for (int j = 0; j < NOMBRE_JOUEURS; j++)
            {

                for (int k = j; k < NOMBRE_JOUEURS; k++)
                {
                    int score = Joueurs[j].Score;

                    if (Joueurs[k].Score > score)
                    {
                        Joueur temp = Joueurs[j];
                        Joueurs[j] = Joueurs[k];
                        Joueurs[k] = temp;
                    }
                }
            }
            for (int i = 0; i < NOMBRE_JOUEURS; i++)
            {
                Console.WriteLine($"{Joueurs[i]} valeur: {Joueurs[i].Valeur()} score: {Joueurs[i].Score}");
            }
        }
    }
}
