using System;
using System.Collections.Generic;
using System.Text;

namespace JeuDomino
{
    /// <summary>
    /// La classe qui représente un Joueur
    /// </summary>
    public class Joueur
    {
        /// <summary>
        /// Le nom du joueur (ne peut pas etre modifié apres construction)
        /// </summary>
        private readonly string nomJoueur;

        /// <summary>
        /// La liste des domino du joueur
        /// </summary>
        private List<Domino> jeu;
              
        /// <summary>
        /// La score du joueur
        /// </summary>
        private int score;

        /// <summary>
        /// domino qui on garde après tester si est adjacent o double 
        /// </summary>
        private Domino dominoSorti;

        /// <summary>
        /// pour enregistrer la position où se trouve le domino adjacent ou le double six
        /// </summary>
        private int positionDomino;

              

        /// <summary>
        /// La liste des domino du joueur
        /// </summary>
        public List<Domino> Jeu
        {
            get
            {
                return jeu;
            }
            set
            {
                jeu = value;
            }
        }

        /// <summary>
        /// le nom du joueur
        /// </summary>
        public string NomJoueur
        {
            get
            {
                return nomJoueur;
            }
        }

        public int Score { get => score; set => score = value; }

        /// <summary>
        /// Le constructeur avec simplement le nom du joueur
        /// </summary>
        /// <param name="nomJoueur">s</param>
        public Joueur(string nomJoueur)
        {
            this.nomJoueur = nomJoueur;
            this.score = 0;
        }

        /// <summary>
        /// Affiche le nom du joueur
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return nomJoueur + " : " + DominoUtil.AffichageJeu(Jeu.ToArray());
        }
        
        /// <summary>
        /// La valeur totale du jeu du joueur 
        /// </summary>
        /// <returns>La valeur totale</returns>
        public int Valeur()
        {
            int score = 0;
            foreach (Domino domino in Jeu)
            {
                score += domino.Valeur();
            }
            return score;
        }

        /// <summary>
        /// pour savoir si le double-six se trouve dans la liste du domino de jouer
        /// </summary>
        /// <returns>true, false</returns>
        public bool DoubleSix()
        {
            bool doubleSix = false;

            foreach (Domino domino in Jeu)
            {
                if (domino.Valeur() == 24)
                {
                    doubleSix = true;
                    positionDomino = jeu.IndexOf(domino);
                }
            }
            return doubleSix;
        }


        /// <summary>
        /// Pour savoir si le joueur à un domino adjacent a un domino x
        /// </summary>
        /// <param name="table"></param>
        /// <returns> true, false</returns>
        public bool AvoirDomino(Domino table)
        {
            bool avoirDomino = false;

            foreach (Domino domino in Jeu)
            {
                if (domino.IsAdjacent(table))
                {
                    avoirDomino = true;
                    positionDomino = jeu.IndexOf(domino);
                    break;
                }
            }
            return avoirDomino;
        }

        /// <summary>
        /// Méthode pour jouer le domino, qui on garde après tester si est adjacent o double
        /// </summary>
        /// <returns>Domino</returns>
        public Domino DominoSorti()
        {
            dominoSorti = Jeu[positionDomino];
            jeu.Remove(jeu[positionDomino]);
            return dominoSorti;
        }


    }
}
