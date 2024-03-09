using System;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int WORD_LEVEL_1 = 1, WORD_LEVEL_2 = 3, WORD_LEVEL_3 = 6, WORD_LEVEL_4 = 10, NUMBER_OF_GUESSES = 10;
            const string WORD_SPACING = "_";
            ConsoleKeyInfo userInput;
            Random rng = new Random();

            List<string> alphabet = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
            List<string> listOf5LetterWords = new List<string> {"pearl", "haste", "glint", "laxes", "newel"};
            List<string> listOf6LetterWords = new List<string> {"wackes", "eagres", "babkas", "radula", "qindar" };
            List<string> listOf7LetterWords = new List<string> {"abaculi", "fabliau", "jabroni", "macaque", "xanthic"};
            List<string> listOf8LetterWords = new List<string> {"iarovize", "kabloona", "cabanes", "tabanid", "vacuity" };
            List<string> listOf9LetterWords = new List<string> {"dactylus", "saboraim", "ubiquity", "obduracy", "zaibatsu" }; 
            List<string> randomWordsList = new List<string>();
            int playerWins = 0, difficulty = 0;

            while (true)
            {
                List<string> remainingAlphabet = new List<string>(alphabet);
                randomWordsList.Clear();

                // Add For loop method to add words from text file to each list based on word length.
               
                // Player begins game with five letter words.
                if (playerWins < WORD_LEVEL_1) 
                { 
                    foreach (string fiveLetterWord in listOf5LetterWords)
                    {
                        randomWordsList.Add(fiveLetterWord);
                    }
                }

                // Players begins game with six letter words after winning one game.
                if (playerWins >= WORD_LEVEL_1 && playerWins < WORD_LEVEL_2)
                {
                    foreach (string sixLetterWord in listOf6LetterWords)
                    {  
                        randomWordsList.Add(sixLetterWord);
                    }
                }

                // Player begins game with seven letter words after winning three games.
                if (playerWins >= WORD_LEVEL_2 && playerWins < WORD_LEVEL_3)
                {
                    foreach (string sevenLetterWord in listOf7LetterWords)
                    {
                        randomWordsList.Add(sevenLetterWord);
                    }
                }

                // Player begins game with eight letter words after winning six games.
                if (playerWins >= WORD_LEVEL_3 && playerWins < WORD_LEVEL_4)
                {
                    foreach (string eightLetterWord in listOf8LetterWords) 
                    {
                        randomWordsList.Add(eightLetterWord);
                    }
                }

                // Player begins game with nine letter words after winning ten games.
                if (playerWins >= WORD_LEVEL_4)
                {
                    foreach (string nineLetterWord in listOf9LetterWords)
                    {
                        randomWordsList.Add(nineLetterWord);
                    }
                }

                int randomWordIndex = rng.Next(randomWordsList.Count);
                string randomWord = randomWordsList[randomWordIndex];
                List<string> randomWordInList = new List<string> (new string[randomWord.Length]);

                for (int i = 0; i < randomWord.Length; i++) 
                {
                    randomWordInList[i] = WORD_SPACING;
                }

                Console.WriteLine($"Welcome to the Hangman game!\n");
                int counter = 0;

                while (counter < NUMBER_OF_GUESSES) {

                    Console.WriteLine($"Random Word: {randomWord} //DELETE IN FINAL CODE\n");
                    Console.WriteLine($"Guess Number: {counter + 1}");
                    Console.Write($"Hidden Word: ");
                    foreach (string hiddenLetter in randomWordInList)
                    { 
                        Console.Write($"{hiddenLetter} "); 
                    }
                    Console.Write("\nRemaining characters to choose from: ");
                    foreach(string a in remainingAlphabet)
                    {
                        Console.Write(a + " ");
                    }
                    Console.WriteLine("\nPlease type in a single alphabetical character (such  as 'a' or 'b'). ");
                    Console.Write("Player input: ");
                    userInput = Console.ReadKey();
                    counter++;

                    int numberOfGuessesLeft = NUMBER_OF_GUESSES - counter;

                    string userModifiedInput = userInput.Key.ToString().ToLower();
                    while (alphabet.Contains(userModifiedInput) && !remainingAlphabet.Contains(userModifiedInput))
                    {
                        Console.WriteLine("\nPlayer has selected an already picked character. Please choose a character that has not been selected.");
                        Console.Write("Remaining characters to choose from: ");
                        // Fix display
                        foreach (string a in remainingAlphabet)
                        {
                           Console.Write(a + " ");
                        }
                        Console.Write("Player input: ");
                        userInput = Console.ReadKey();
                        userModifiedInput = userInput.Key.ToString().ToLower();
                    }
                    
                    if (!alphabet.Contains(userModifiedInput))
                    {
                        Console.WriteLine("\nPlayer have selected an invalid character. Player guess should be an alphabetical character. ");
                        return;
                    }

                    int charCount = 0;

                    for (int i = 0; i < randomWord.Length; i++)
                    {
                        char userInputtedCharacter = userModifiedInput[0];
                        char randomWordCharacter = randomWord[i];
                        if (userInputtedCharacter.Equals(randomWordCharacter)) 
                        {
                            string addChar = $"{randomWordCharacter}";
                            randomWordInList[i] = addChar;
                            charCount++;
                        }
                    }

                    if (charCount > 0)
                    {
                        Console.WriteLine($"\nPlayer correctly guessed {userInput.Key.ToString().ToLower()} is in the hidden word! Remining Guesses: {numberOfGuessesLeft}\n");
                    }

                    if (charCount == 0) 
                    {
                        Console.WriteLine($"\nPlayer has incorrectly guessed {userInput.Key.ToString().ToLower()}. Remaining Guesses: {numberOfGuessesLeft}\n");
                    }

                    int hiddenWordLength = 0;

                    foreach (string a in randomWordInList)
                    {
                        if (a != WORD_SPACING) 
                        { 
                            hiddenWordLength++;
                        }
                    }

                    if (hiddenWordLength == randomWord.Length)
                    {
                        playerWins++;
                        Console.WriteLine($"Congratulations! Player has correctly guess the hidden word {randomWord}. Player has won {playerWins} games.\n");
                        break;
                    }

                    if (counter == NUMBER_OF_GUESSES) 
                    {
                        Console.WriteLine($"Game over! Player did not guess the hidden word {randomWord}. Player has won {playerWins} games.\n");
                    }

                    while (userInput.Key != ConsoleKey.Enter)
                    {
                        Console.WriteLine("Press <Enter> to continue game. Screen will refresh.");
                        userInput = Console.ReadKey(true);
                    }

                    remainingAlphabet.Remove(userModifiedInput);
                    Console.Clear();
                }
            }
        }
    }
}
