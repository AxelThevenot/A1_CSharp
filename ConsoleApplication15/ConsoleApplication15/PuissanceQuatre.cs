using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication15
{
    class Program
    {
        static string NomDujoueur(string nom1, string nom2, int joueur)
        {//Voir avant la méthode ChangerDeJoueur

            string nom = nom1;//on établit un nom par défaut
            if (joueur == 1)//Si ce n'est pas le bon, le changer
            {
                nom = nom2;
            }
            return nom;
        }

        static int[,] GenererGrille(int hauteur, int nombreDeColonne)
        {//simple génération de grille

            int[,] grille = new int[hauteur, nombreDeColonne];
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    grille[i, j] = 2;//On met tout à 2 par défaut mais on aurait pu prendre n'importe quelle valeur autre que 0 et 1 qui seront les "jetons" des deux joueurs
                }
            }
            return grille;
        }

        static void AfficherGrille(int[,] grille)
        {
            // On commence par afficher les numéros des colonnes au dessus de ces dernières
            if (grille.GetLength(1) < 9)
            {//Les nombres < 10 n'ont besoin que d'un caratère pour être affichés

                for (int k = 0; k < grille.GetLength(1); k++)
                {
                    Console.Write("  " + (k + 1) + " ");
                }
            }
            else
            {
                for (int k = 0; k < 9; k++)
                {
                    Console.Write("  " + (k + 1) + " ");
                }
                for (int k = 9; k < grille.GetLength(1); k++)
                {/*Les nombres > 10 ont besoin de 2 caractères pour être affichés
                   On retire donc un espace pour qu'il n'y ait pas de décalage*/

                    Console.Write(" " + (k + 1) + " ");
                }
            }
            Console.WriteLine();

            //Un peu d'esthétisme... Enfin ce qui est possible de faire ^^
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                Console.Write("\n|");
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (grille[i, j] == 0 || grille[i, j] == 1)
                    {
                        Console.Write(" " + grille[i, j] + " |");
                    }
                    else
                    {
                        Console.Write("   |");
                    }
                }
            }
            Console.Write("\n-");
            for (int k = 0; k < grille.GetLength(1); k++)
            {
                Console.Write("----");
            }
        }

        static bool ColonneExistante(int[,] grille, int colonne)
        {
            bool existance = false;
            if (colonne < grille.GetLength(1) + 1 && colonne > 0)//On vérifie que la colonne est valide
            {
                existance = true;
            }
            return existance;
        }

        static bool ColonnePleine(int[,] grille, int colonne)
        {
            bool colonnePleine = false;
            if (grille[0, colonne - 1] != 2) //Si elle est pleine alors la case du haut n'est pas vide (gravité des jetons). Ici le vide = 2
            {
                colonnePleine = true;
            }
            return colonnePleine;
        }

        static int ChangerDeJoueur(int joueur)
        {
            if (joueur == 0)
            {
                joueur = 1;
            }
            else
            {
                joueur = 0;
            }
            return joueur;
        }

        static int[,] PlacerUnJeton(int[,] grille, int colonne, int joueur)
        {

            int index = grille.GetLength(0) - 1;
            colonne--;
            while (index >= 1 && grille[index, colonne] != 2)
            {/*On part du bas et on vérifie si la case est vide pour y faire tomber le jeton, sinon on regarde celle du dessus etc.
                   La colonne est nécessairement non pleine. Le test a été fait dans le main */

                index--;
            }

            grille[index, colonne] = joueur;//On place le jeton du joueur dans la prémière case vide en partant du bas                   
            return grille;
        }

        static bool Victoire(int[,] grille)
        {
            bool victoire = false;
            for (int i = 0; i < grille.GetLength(0) - 3; i++)
            {
                for (int j = 0; j < grille.GetLength(1) - 3; j++)
                {

                    if (grille[i, j] != 2 || grille[i + 1, j] != 2 || grille[i + 2, j] != 2 || grille[i + 3, j] != 2)
                    {//On vérifie quand même qu'il y ait au moins une valeur non vide sinon ils sont égaux mais victoire

                        if (grille[i, j] == grille[i + 1, j] && grille[i + 1, j] == grille[i + 2, j] && grille[i + 2, j] == grille[i + 3, j])
                        {//Vérification verticale

                            victoire = true;
                        }
                    }

                    if (grille[i, j] != 2 || grille[i, j + 1] != 2 || grille[i, j + 2] != 2 || grille[i, j + 3] != 2)
                    {
                        if (grille[i, j] == grille[i, j + 1] && grille[i, j + 1] == grille[i, j + 2] && grille[i, j + 2] == grille[i, j + 3])
                        {//Vérification horizontale

                            victoire = true;
                        }
                    }

                    if (grille[i, j] != 2 || grille[i + 1, j + 1] != 2 || grille[i + 2, j + 2] != 2 || grille[i + 3, j + 3] != 2)
                    {
                        if (grille[i, j] == grille[i + 1, j + 1] && grille[i + 1, j + 1] == grille[i + 2, j + 2] && grille[i + 2, j + 2] == grille[i + 3, j + 3])
                        {//DiagonaleDepuisHautGauche

                            victoire = true;
                        }
                    }

                    if (grille[i + 3, j] != 2 || grille[i + 2, j + 1] != 2 || grille[i + 1, j + 2] != 2 || grille[i, j + 3] != 2)
                    {
                        if (grille[i + 3, j] == grille[i + 2, j + 1] && grille[i + 2, j + 1] == grille[i + 1, j + 2] && grille[i + 1, j + 2] == grille[i, j + 3])
                        {//DiagonaleDepuisBasGauche

                            victoire = true;
                        }
                    }
                }
            }

            //Il manque maintenant la vérification horizontale/verticale sur les 3 dernières ligne/colonnee en bas/droite
            for (int i = grille.GetLength(0) - 3; i < grille.GetLength(0); i++)
            {//Pour la vérification des 3 dernières lignes horizontales du bas 

                for (int j = 0; j < grille.GetLength(1) - 3; j++)
                {
                    if (grille[i, j] != 2 || grille[i, j + 1] != 2 || grille[i, j + 2] != 2 || grille[i, j + 3] != 2)
                    {
                        if (grille[i, j] == grille[i, j + 1] && grille[i, j + 1] == grille[i, j + 2] && grille[i, j + 2] == grille[i, j + 3])
                        {
                            victoire = true;
                        }
                    }
                }
            }
            for (int j = grille.GetLength(1) - 3; j < grille.GetLength(1); j++)
            {//Pour la vrification des 3 dernières colonnes verticales à droite

                for (int i = 0; i < grille.GetLength(0) - 3; i++)
                {
                    if (grille[i + 3, j] != 2)
                    {//Vérification seulement de la case du bas due à la gravité du jeton

                        if (grille[i, j] == grille[i + 1, j] && grille[i + 1, j] == grille[i + 2, j] && grille[i + 2, j] == grille[i + 3, j])
                        {
                            victoire = true;
                        }
                    }
                }
            }
            return victoire;
        }

        static void AfficherVainqueur(string joueur1, string joueur2, int joueur)
        {
            Console.WriteLine();
            for (int k = 0; k < 3; k++)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("   |   |      |   |           C'est " + NomDujoueur(joueur1, joueur2, joueur) + " qui gagne!             |   |      |   |      ");
                Console.WriteLine(" |         |        |         C'est " + NomDujoueur(joueur1, joueur2, joueur) + " qui gagne!           |         |        |    ");
                Console.WriteLine("|                     |       C'est " + NomDujoueur(joueur1, joueur2, joueur) + " qui gagne!          |                     |  ");
                Console.WriteLine("  |                 |         C'est " + NomDujoueur(joueur1, joueur2, joueur) + " qui gagne!            |                 |    ");
                Console.WriteLine("    |             |           C'est " + NomDujoueur(joueur1, joueur2, joueur) + " qui gagne!              |             |      ");
                Console.WriteLine("      |         |             C'est " + NomDujoueur(joueur1, joueur2, joueur) + " qui gagne!                |         |        ");
                Console.WriteLine("        |     |               C'est " + NomDujoueur(joueur1, joueur2, joueur) + " qui gagne!                  |     |          ");
                Console.WriteLine("          | |                 C'est " + NomDujoueur(joueur1, joueur2, joueur) + " qui gagne!                    | |            ");

            }


        }

        static void Main(string[] args)
        {
            //Initialisation automatique
            int joueur = 0;
            int colonne = 0;
            int hauteur = 0;
            int nombreDeColonne = 0;

            //Choix d'initialisation grille 
            Console.WriteLine("Pour commencer une nouvelle partie veuillez entrer la taille souhaitée de grille au minimum 4x4"); //Sinon impossible de gagner
            while ( hauteur < 4)
            {
                Console.WriteLine("\nHauteur > 4 : ");
                hauteur = Convert.ToInt32(Console.ReadLine());
            }
            while (nombreDeColonne < 4)
            {
                Console.WriteLine("\nNombre de colonne > 4: ");
                nombreDeColonne = Convert.ToInt32(Console.ReadLine());
            }

            //Choix d'initialisation joueurs                
            Console.WriteLine("\nEntrer le nom du joueur 1");
            string joueur1 = Console.ReadLine();
            Console.WriteLine("\nEntrer le nom du joueur 2");
            string joueur2 = Console.ReadLine();

            //Génération du jeu
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            int[,] grille = GenererGrille(hauteur, nombreDeColonne);
            int[,] grilleprecedente = new int[grille.GetLength(0), grille.GetLength(1)];
            AfficherGrille(grille);

            // C'est lancé !

            while (Victoire(grille) == false)
            {
                Console.WriteLine("\n\n" + NomDujoueur(joueur1, joueur2, joueur) + " : Dans quelle colonne veux-tu mettre ton jeton? ");
                colonne = Convert.ToInt32(Console.ReadLine());
                if (ColonneExistante(grille, colonne))
                {
                    if (ColonnePleine(grille, colonne))
                    {//Je n'en mets beaucoup afin que ca soit aisément visible

                        joueur = ChangerDeJoueur(joueur);
                        Console.WriteLine("\n\nColonne complète : veuillez rejouer une colonne valide");
                        Console.WriteLine("Colonne complète : veuillez rejouer une colonne valide");
                        Console.WriteLine("Colonne complète : veuillez rejouer une colonne valide");
                        Console.WriteLine("Colonne complète : veuillez rejouer une colonne valide");
                        Console.WriteLine("Colonne complète : veuillez rejouer une colonne valide");
                        Console.WriteLine("Colonne complète : veuillez rejouer une colonne valide\n\n");
                    }
                    else
                    {
                        grille = PlacerUnJeton(grille, colonne, joueur);
                    }
                }
                else
                {
                    joueur = ChangerDeJoueur(joueur);
                    Console.WriteLine("\n\nColonne inexistante : veuillez rejouer une colonne valide");
                    Console.WriteLine("Colonne inexistante : veuillez rejouer une colonne valide");
                    Console.WriteLine("Colonne inexistante : veuillez rejouer une colonne valide");
                    Console.WriteLine("Colonne inexistante : veuillez rejouer une colonne valide");
                    Console.WriteLine("Colonne inexistante : veuillez rejouer une colonne valide");
                    Console.WriteLine("Colonne inexistante : veuillez rejouer une colonne valide\n\n");
                }
                AfficherGrille(grille);
                joueur = ChangerDeJoueur(joueur);
            }
            //A la fin de la boucle le joueur change, on le change à nouveau pour avoir le vainqueur
            joueur = ChangerDeJoueur(joueur);
            AfficherVainqueur(joueur1, joueur2, joueur);
            Console.ReadKey();
        }
    }
}

