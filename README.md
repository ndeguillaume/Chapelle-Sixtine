# Chapelle Sixtine

<img src="https://i.imgur.com/l4cOzPU.png"/>

Création de la chapelle Sixtine en réalité mixte avec Unity et Blender. L'utilisateur peut interagir avec l'environnement pour se déplacer ou effectuer des actions avec les tableaux. Des structures anatomiques sont cachés dans les tableaux de la chapelle, cette application permet entre autre de les révéler.
Projet réalité à l'université de Sunderland durant mon stage de fin d'étude de DUT de 10 semaines.

## Fonctionnalités 

<ul>Commandes vocales (mot clef : fonctionnalité)
  <li>List : affiche/ferme la liste des commandes</li>
  <li>Move : déplace l'utilisateur à l'endroit où il regarde</li>
    <li>Up : déplace l'utilisateur vers le haut</li>
    <li>Down : déplace l'utilisateur vers le bas</li>
    <li>Closer : affiche une copie du tableau regardé plus proche de l'utilisateur</li>
    <li>Bigger : augmente la taille d'une copie d'un tableau regardée</li>
    <li>Smaller : réduit la taille d'une copie d'un tableau regardée</li>
    <li>Remove : supprime une copie d'un tableau regardée</li>
  <li>Face me : change l'angle de la copie d'un tableau regardée pour qu'elle soit en face de l'utilisateur  </li>
  <li>Select : commence un rectangle de sélection à l'endroit regardé d'un tableau (point en bas à gauche du rectangle de sélection)</li>
  <li>Okay : valide la sélection (position du point en haut à droite du rectangle de sélection). La partie du tableau qui est en dans le rectangle de sélection est dupliquée et affichée devant l'utilisateur</li>
    <li>Search : affiche les informations du tableau récupéré (informations récupérées depuis wikipedia)</li>
  <li>Close : ferme l'interface de texte regardé (de Search ou List)</li>
  </ul>

<ul>
  <li>Musique d'ambiance de type église</li>
  <li>Lorsque l'utilisateur effectue un Select puis un Okay sur une zone qui est une structure anatomique cachée, celle-ci s'affiche à côté du la partie copiée du tableau </li>
  </ul>
  
## Installation

Nécessite Unity 2017 pour lancer le projet. 

Clonez le repo et importez le projet dans Unity.

## Rapport

Le rapport de projet est disponible <a href="https://github.com/ndeguillaume/Chapelle-Sixtine/blob/master/sunderland_report.pdf"> ici</a>.
