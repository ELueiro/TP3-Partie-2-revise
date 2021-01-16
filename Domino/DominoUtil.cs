using System;
using System.Collections.Generic;
using System.Text;

namespace JeuDomino
{
    /// <summary>
    ///Test
    /// Classe statique contenant quelques utilitaires
    /// concernant le jeu
    /// </summary>
    public static class DominoUtil
    {
        public static string AffichageJeu(Domino[] dominos)
        {
            //StringBuilder permet d'optimiser la contruction 
            //d'une string qui doit se faire en plusiers etapes 
            //Pour eviter le code en commentaire qui alloue beaucoup
            //de memoire inutilement..
            StringBuilder jeu = new StringBuilder($"{dominos.Length} dominos: ");
            //String autreJeu = dominos.Length + " dominos: ";
            foreach (Domino domino in dominos)
            {
                jeu.Append(domino + " " );
                //autreJeu += domino + " ";
            }
            return jeu.ToString();
           // return autreJeu;
        }
    }
}
