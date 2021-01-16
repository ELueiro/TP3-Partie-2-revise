using System;
using System.Collections.Generic;
using System.Text;

namespace JeuDomino
{
    /// <summary>
    /// La classe qui représente un Domino 
    /// </summary>
    public class Domino
    {
        /// <summary>
        /// La plus grande valeur sur un domino
        /// En general 6 (mais peut etre modifiee)
        /// </summary>
        public const int MAX_VALUE = 6;
        
        private int gauche;
       
       /// <summary>
       /// La valeur de gauche du domino 
       /// </summary>
        public int Gauche
        {
            get
            {
                return gauche;
            }
            set
            {
                if (0 <= value && value <= MAX_VALUE)
                {
                    gauche = value;
                }

            }
        }

        private int droite;

        /// <summary>
        /// La valeur de droite du domino 
        /// </summary>
        public int Droite
        {
            get
            {
                return droite;
            }
            set
            {
                if (0 <= value && value <= MAX_VALUE)
                {
                    droite = value;
                }

            }
        }

        /// <summary>
        /// Costructeur avec les valeurs initiales
        /// </summary>
        /// <param name="gauche">la valeur de gauche</param>
        /// <param name="droite">la valeur de droite</param>
        public Domino(int gauche, int droite) {
            Gauche = gauche;
            Droite = droite;
        }

        /// <summary>
        /// Vrai si le domino a deux valeurs identiques
        /// </summary>
        /// <returns> true si le domino est double</returns>
        public bool IsDouble()
        {
            return Gauche == Droite;
        }


        /// <summary>
        /// Verifie si le domino en parametre est adjacent au domino courant
        /// Deux dominos sont adjacents s'ils ont une valeur en commun!
        /// Attention: (2,6) est adjacent a (2,4) il faudra donc "tourner" (2,4)
        /// </summary>
        /// <param name="other">Le domino que l'on souhaite verifier </param>
        /// <returns>True si ils sont adjacents</returns>
        public bool IsAdjacent(Domino other) {
            return other.Gauche == Gauche || other.Droite == Gauche || other.Gauche == Droite || other.Droite == Droite;
        }

        /// <summary>
        /// Un descripteur du domino (2,4)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + Gauche + ";" + Droite + ")";
        }

        /// <summary>
        /// LA valeur d'un domino somme des ponts (doublés si c'est un double) 
        /// </summary>
        /// <returns></returns>
        public int Valeur()
        {
            int valeur = Gauche + Droite;
            if(IsDouble())
            {
                valeur *= 2;
            }
            return valeur;
        }

        /// <summary>
        /// Une méthode  qui permute les deux côtés de domino
        /// </summary>
        /// <param name="domino"></param>
        /// <returns>vrai ou faux</returns>
        public Domino Permute(Domino domino)
        {
            int temp = domino.Gauche;
            domino.Gauche = domino.Droite;
            domino.Droite = temp;

            return domino;

        }
     
    }
}
