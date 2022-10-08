# those-3-words

A fun little web game which involves a bit of luck and geographical knowledge.

## So what is this about?

This game was inspired from the following websites:
* [what3words](https://what3words.com) : Gives every 3 metre square of the world a unique combination of three words.
* [GeoGuessr](https://geoguessr.com) : Guess region based of Google Street View.
* [Worldle](https://worldle.teuteuf.fr) : Wordle, but for country/territory

In a round, a player should perform the following:
1. Enter three valid words. They get a point if the words entered form a valid 3 Word Address (3WA), otherwise they get to choose any one of three 3WAs.
2. Guess where the coordinates landed on: terrestrial or aquatic surface. They get a point if correctly guessed.
3. If on land:
  * The game master confirms on which continent do the coordinates reside. 
  * Multiple attempts are given to the user to guess the country/territory in which our coordinates reside. If correctly guessed in the first attempt, 3 points will be awarded.
  * After each incorrect guess, points awarded are decreased by a margin and a hint is revealed to the player. Now, it can either be the position of the country/territory inside 
  the given continent, or it's relative position with respect to the incorrect guess or a distinguishing aspect of it.
  * Exception case of Antarctica: you get 3 points cause that's pretty cool
4. If on water:
  * The game master confirms whether the type of water body is oceanic or inland.
  * The point awarding system remains similiar except the hints given to the player change a bit. If it's an ocean, neighbouring continents are disclosed one by one. If it's an
  inland water body however, rules pertaining to country/territory are followed.
  
