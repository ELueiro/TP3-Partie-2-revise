using System;
using System.Collections.Generic;
using System.Text;

namespace JeuDomino
{
    /// <summary>
    /// Un Paquet de dominos.
    /// </summary>
    public class Paquet
    {
        ///
        /// <summary>
        /// Le nombre d'itérations pour mélanger.
        /// </summary>
        private const int ITERATION_MELANGE = 30;
        
        /// <summary>
        /// Le calcul qui mene a 28 dominos... permet de changer MAX_VALUE et d'avoir un jeu complet.
        /// </summary>
        public const int NOMBRE_DOMINOS = (Domino.MAX_VALUE+1)* (Domino.MAX_VALUE + 2)/2;
        
        /// <summary>
        /// Le tableau de tous les dominos qui va etre melangé dans le constructeur.
        /// </summary>
        private Domino[] dominos;

        /// <summary>
        /// Le tableau de tous les dominos qui va etre melangé das le constructeur.
        /// <summary>
        public Domino[] Dominos { get { return dominos; } set { dominos = value; } }

        /// <summary>
        /// L'indice du premier domino disponible, pas encore distribué...
        /// </summary>
        private int SommetPile;

        public Paquet()
        {           
            SommetPile = 0;
            //Construit le tableau de tous les dominos
            Dominos = new Domino[NOMBRE_DOMINOS];
            int k = 0;
            for (int i = 0; i <= Domino.MAX_VALUE; i++)
            {
                for (int j = i; j <= Domino.MAX_VALUE; j++)
                {
                    Domino domino = new Domino(i, j);
                    Dominos[k] = domino;
                    k++;
                }
            }
            Melanger();
        }

        public override string ToString()
        {
            return DominoUtil.AffichageJeu(Dominos);
        }

        /// <summary>
        /// Distribue le premier domino disponible sur le sommet de la pile
        /// et met le sommet de la pile au prochain
        /// </summary>
        /// <returns></returns>
        public Domino Distribue()
        {
            Domino prochain = null;
            if(SommetPile <= Dominos.Length)
            {
                prochain = Dominos[SommetPile];
                SommetPile++;
               
                if (SommetPile >= NOMBRE_DOMINOS - 1)
                {
                    SommetPile = 0;
                }
            }
            
            return prochain;
        }
      

        /// <summary>
        /// Prend les n dominos du dessus de la pile et les met dans une Liste!
        /// </summary>
        /// <param name="nombre">le nombre de domino a distribuer</param>
        /// <returns> </returns>
        public List<Domino> Distribue(int nombre)
        {
            List<Domino> prochains = new List<Domino>();
            for (int i = 0; i < nombre; i++)
            {
                prochains.Add(Distribue());
            }
            return prochains;
        }

        /// <summary>
        /// Permutation de deux domino dans le tableau
        /// </summary>
        /// <param name="i">l'indice du premier domino à echanger </param>
        /// <param name="j">l'indice du deuxième domino à echanger </param>
        private void Permuter(int i, int j)
        {
            if (0 <= i && i <= Dominos.Length && 0 <= j && j <= Dominos.Length)
            {
                Domino temp = Dominos[i];
                Dominos[i] = Dominos[j];
                Dominos[j] = temp;
            }
            
        }

        /// <summary>
        /// Melange le tableau de domino avant la distribution
        /// </summary>
        private void Melanger()
        {
            Random rand = new Random();
            for (int i = 0; i <= ITERATION_MELANGE; i++) 
            {
                int x= rand.Next(0, Dominos.Length);
                int y= rand.Next(0, Dominos.Length);
                Permuter(x, y);
            }
        }
    }
}
