using System;
using System.Text.RegularExpressions;

class Program
{
    public static void Main(string[] args)
    {
        // Load data files into arrays
        String[] dictionary = System.IO.File.ReadAllLines(@"F:\Visual Studio 2019\Projects\SpellcheckDictionairy\data-files\dictionary.txt");
        String aliceText = System.IO.File.ReadAllText(@"F:\Visual Studio 2019\Projects\SpellcheckDictionairy\data-files\AliceInWonderLand.txt");
        String[] aliceWords = Regex.Split(aliceText, @"\s+");

        // Print first 50 values of each list to verify contents

        Console.WriteLine("***DICTIONARY***");
        printStringArray(dictionary, 0, 50);

        Console.WriteLine("***ALICE WORDS***");
        printStringArray(aliceWords, 0, 50);

        // Make a menu loop
        // Init variables
        bool loop = true;
        while (loop)
        {
            Console.WriteLine(
                "Main Menu" +
                "\n1: Spell Check a Word (Linear)" +
                "\n2: Spell Check a Word (Binary)" +
                "\n3: Spell Check Alice in Wonderland (Linear)" +
                "\n4: Spell Check Alice in Wonderland (Binary)" +
                "\n5: Exit"
                );
            Console.Write("Enter Menu Selection (1-5): ");
            int uNum = Convert.ToInt32(Console.ReadLine());
            ExecuteOption(uNum, dictionary);

        }
    }

    // Execute user decision
    public static void ExecuteOption(int num, string[] anArray)
    {
        // def user input variable for option 1 & 2
        string uWord;
        switch (num)
        {
            // Use linearSearch on user's word refrenceing the dictionary
            case 1:
                Console.Write("Please enter a word: ");
                uWord = Console.ReadLine();
                uWord.ToLower();
                PrintSuccessFailure(LinearSearch(anArray, uWord));
                break;
            case 2:

                break;
            default:
                Console.WriteLine(-1);
                break;
        }
    }


    // Register if dictionary search works and *where* it was found
    // Examle of code: printSuccessFailure(LinearSearch(dictionary, "string"));
    public static void PrintSuccessFailure(int num)
    {
        if (num == -1)
        {
            Console.WriteLine("Word not found");
        }
        else
        {
            Console.WriteLine($"Word found at position {num}");
        }
    }

    public static void printStringArray(String[] array, int start, int stop)
    {
        // Print out array elements at index values from start to stop 
        for (int i = start; i < stop; i++)
        {
            Console.WriteLine(array[i]);
        }
    }

    // Modified The Searches to utilize strings instead of integers
    static int LinearSearch(string[] anArray, string item)
    {
        for (int i = 0; i < anArray.Length; i++)
        {
            if (anArray[i] == item)
            {
                return i;
            }
        }
        return -1;
    }

    static int BinarySearch(string[] anArray, string item)
    {
        //Init Variables 
        int lowerIndex = 0;
        int middleIndex;
        int upperIndex = anArray.Length - 1;


        while (lowerIndex <= upperIndex)
        {
            middleIndex = (lowerIndex + upperIndex) / 2;
            if (anArray[middleIndex] == item)
            {
                return middleIndex;
            }
            // Error found here, need to update the method to refrence the dictionary instead of an item
            else if (anArray[middleIndex] == item)
            {
                upperIndex = middleIndex - 1;
            }
            else
            {
                lowerIndex = middleIndex + 1;
            }
        }
        return -1;
    }
}
