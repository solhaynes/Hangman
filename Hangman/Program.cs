using System;

public class Program
{
  public static void Main()
  {
    //string array containing all words which can be used in the game.
    string[] wordLibrary = {"rock", "alphabet", "jumbo", "dragon", "array", "console", "billiards", "swimming", "github"};  
    int librarySize = wordLibrary.Length;
    // Choose random word from the library of words.
    string word = ChooseWord(wordLibrary, librarySize);

    //Create a char array using the word randomly selected
    char[] targetArray = word.ToCharArray(0, word.Length);
    
    // Create an empty char array with the same length as the first char array
    // BlankLetters changes the value of each index to an '_' to give the appearance of a hidden character
    char[] blankArray = new char[targetArray.Length];
    BlankLetters(blankArray);

    // A list of characters that will contain wrong entries from the user.
    List<char> incorrectLettersGuessed = new List<char>();

    // Run the code
    PlayTheGame(targetArray, blankArray, incorrectLettersGuessed);  
  }

  // Chooses a random word from a string array.  Returns the word selected.
  static string ChooseWord(string[] list, int arrayLength)
  {
    Random rand = new Random();
    string word = list[rand.Next(arrayLength - 1)];

    return word;
  }

  // Prints a welcome message.
  static void WelcomeMessage()
  {
    System.Console.Clear();
    System.Console.WriteLine("Welcome to our hangman game!  Enter one letter at a time to complete the hidden word.  Wrong guesses " +
     "result in an additional body part being added to the gallows.  Choose your letters wisely, or you'll pay the price WITH YOUR LIFE!");
    System.Console.WriteLine("If you wish to exit the game at any point, enter 0.");
    System.Console.WriteLine();
  }

  // Changes all of characters in a char array to '_' to give hidden appearance for the game.
  static void BlankLetters(char[] array)
  {
    for( int i = 0; i < array.Length; i++){
      array[i] = '_';
    }
  }

  // Displays the values of a char array.
  static void DisplayWord(char[] array)
  {
    for( int i = 0; i < array.Length; i++){
      System.Console.Write(" " + array[i] + " "); 
    }
    System.Console.Write("\n");
  }

  // Displays the list of incorrect guesses so the player can see which letters didn't work
  static void DisplayList(List<char> list)
{
  foreach (var character in list)
  {
    System.Console.Write(character + " ");
  }
  System.Console.WriteLine(" ");
}
  
  // Prompts the user to input a string and returns it as a character
  static char GetUserInput()
  {
    bool valid = false;
    string userString = "";

    System.Console.Write("Enter a letter: ");

    // While loop to confirm that the user only input one letter.
    while (valid == false)
    {
      userString = Console.ReadLine();
      if (userString.Length > 1)
      {
        System.Console.WriteLine("Only enter one letter at a time.  Please try again...");
      }
      else if (userString.Length == 0)
      {
        System.Console.WriteLine("You failed to enter a letter.  Please try again...");
      }
      else
      {
        valid = true;
      }
    }
    char userChar = Convert.ToChar(userString);
    
    return userChar;
  }

  // Loops through the array with the correct answer.  If the user input matches any of the letters in the char array, it adds that letter in
  // the respective location in the hidden array.  Returns a boolean to indicate if the guess was correct.
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
      System.Console.WriteLine("\n\nGood guess!");
    }
    else
    {
      System.Console.WriteLine("\n\nYou chose poorly. There are no " + input + "'s in this word...");
      list.Add(input);
    }

    return correctGuess;
  }

  // Method which loops through the game until you've won, lost, or entered 0 to exit.
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

      DisplayGallows(livesLeft);
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
      
      // Check to see if the user won
      wonTheGame = DidYouWin(hiddenArray);
      if (wonTheGame)
      {
        System.Console.Write("\nYOU WIN! The word was: ");
        DisplayWord(wordArray);
        System.Console.WriteLine("Congratulations! You live to fight another day!\n");
        break;
      }
      
      // If they didn't win, show them their incorrect guesses so far
      System.Console.Write("\nIncorrect Guesses: ");
      DisplayList(incorrectGuesses);
      System.Console.WriteLine();

      
      // Check to see if all lives are gone.  If so, tell them.
      if (livesLeft == 0)
      {
        DisplayGallows(livesLeft);
        System.Console.Write("\nYOU LOSE.  The correct word was: ");
        DisplayWord(wordArray);
        System.Console.WriteLine();
      }
    }
  }
  // Checks to see if the hidden array has any '_' remaining.  Returns a boolean indicating if the user has won the game.
  static bool DidYouWin(char[] array)
  {
    bool wonTheGame = false;
    foreach (char c in array)
    {
      if (c == '_')
      {
        wonTheGame = false;
        break;
      }
      else
      {
        wonTheGame = true;
      }
    }
    return wonTheGame;
  }
 
  // Display the hangman status visually
  static void DisplayGallows(int livesRemaining)
  {
    string [] attempts = {
            @"
               
            ==========
            ||       |
            ||     
            ||       
            ||       
            ||      
            ||
            ========== ",      

            @"
               
            ==========
            ||       |
            ||       O 
            ||       
            ||       
            ||      
            ||
            ========== ",    

             @"
               
            ==========
            ||       |
            ||     \ O 
            ||       
            ||       
            ||       
            ||
            ========== ",    

              @"
               
            ==========
            ||       |
            ||     \ O /
            ||       
            ||       
            ||        
            ||
            ========== ",    

             @"
               
            ==========
            ||       |
            ||     \ O /
            ||       |
            ||       |
            ||       
            ||
            ========== ",    

             @"
               
            ==========
            ||       |
            ||     \ O /
            ||       |
            ||       |
            ||        \ 
            ||
            ========== ",  

             @"
               
            ==========
            ||       |
            ||     \ O /
            ||       |
            ||       |
            ||      / \ 
            ||
            ========== ",     
         };

    switch(livesRemaining)
    {
      case 6:
        System.Console.WriteLine(attempts[0]);
        break;
      case 5:
        System.Console.WriteLine(attempts[1]);
        break;
      case 4:
        System.Console.WriteLine(attempts[2]);
        break;
      case 3:
        System.Console.WriteLine(attempts[3]);
        break;
      case 2:
        System.Console.WriteLine(attempts[4]);
        break;
      case 1:
        System.Console.WriteLine(attempts[5]);
        break;
      case 0:
        System.Console.WriteLine(attempts[6]);
        break;
    }
  }
}