using System;

public class Program
{
  public static void Main()
  {
    //string array containing all words which can be used in the game.
    string[] wordLibrary = {"rock", "alphabet", "jumbo", "dragon", "array", "console"};  
    int librarySize = wordLibrary.Length;

    string word = ChooseWord(wordLibrary, librarySize);

    char[] targetArray = word.ToCharArray(0, word.Length);
    // System.Console.WriteLine(targetArray);

    char[] blankArray = new char[targetArray.Length];

    List<char> incorrectLettersGuessed = new List<char>();

    BlankLetters(blankArray);

    PlayTheGame(targetArray, blankArray, incorrectLettersGuessed);    
    
  }

  static string ChooseWord(string[] list, int arrayLength)
  {
    Random rand = new Random();
    string word = list[rand.Next(arrayLength - 1)];

    return word;
  }

  static void WelcomeMessage()
  {
    System.Console.WriteLine("Welcome to our hangman game!  You all know how this works.  Choose your letters wisely, or"
      + " you'll pay the price WITH YOUR LIFE!");
  }

  static void BlankLetters(char[] array)
  {
    for( int i = 0; i < array.Length; i++){
      array[i] = '_';
    }
  }

  static void DisplayWord(char[] array)
  {
    for( int i = 0; i < array.Length; i++){
      System.Console.Write(" " + array[i] + " "); 
    }
    System.Console.Write("\n");
  }

  static void DisplayList(List<char> list)
{
  foreach (var character in list)
  {
    System.Console.Write(character + " ");
  }
  System.Console.WriteLine(" ");
}
  static char GetUserInput()
  {
    System.Console.Write("Enter a letter: ");
    string userString = Console.ReadLine();
    char userChar = Convert.ToChar(userString);

    return userChar;
  }

  static bool CompareToArray(char[] array1, char[] array2, List<char> list, char input, int lives)
  {
    bool correctGuess = false;
    for (int i = 0; i < array1.Length; i++)
    {
      if (array1[i] == input)
      {
        array2[i] = input;
        correctGuess = true;
      }
    }
    if (correctGuess == true)
    {
      System.Console.WriteLine("Good guess!");
    }
    else
    {
      System.Console.WriteLine("You chose poorly. There are no " + input + "'s in this word...");
      list.Add(input);
    }

    return correctGuess;
  }

  static void PlayTheGame(char[] wordArray, char[] hiddenArray, List<char> incorrectGuesses)
  {
    bool wonTheGame = false;
    int livesLeft = 6;

    char userGuess = ' ';
    bool correct;

    WelcomeMessage();
    while ((wonTheGame == false) && (livesLeft != 0))
    {
      correct = false;

      DisplayWord(hiddenArray);
      userGuess = GetUserInput();
      
      if(userGuess == '0')
      {
        System.Console.WriteLine("Thanks for playing!");
        break;
      }

      correct = CompareToArray(wordArray, hiddenArray, incorrectGuesses, userGuess, livesLeft);
      if (correct == false)
      {
        livesLeft --;
      }
      System.Console.Clear();
      System.Console.WriteLine("Lives Remaining: " + livesLeft);
      System.Console.Write("Incorrect Guesses: ");
      DisplayList(incorrectGuesses);
      System.Console.WriteLine(" ");

      if (livesLeft == 0)
      {
        System.Console.Write("YOU LOSE.  The correct word was: ");
        DisplayWord(wordArray);
      }
    }
  }
}