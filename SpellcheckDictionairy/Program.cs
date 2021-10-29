using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

class Program
{
    public static void Main(string[] args)
    {
        // Load Data files as arrays of strings
        String[] dictionary = System.IO.File.ReadAllLines(@"F:\Visual Studio 2019\Projects\SpellcheckDictionairy\data-files\dictionary.txt");
        String aliceText = System.IO.File.ReadAllText(@"F:\Visual Studio 2019\Projects\SpellcheckDictionairy\data-files\AliceInWonderLand.txt");
        String[] aliceWords = Regex.Split(aliceText, @"\s+");

        // Menu Loop
        bool loop = true;
        while (loop)
        {
            Console.WriteLine(
                "\nMain Menu" +
                "\n1: Spell Check a Word (Linear)" +
                "\n2: Spell Check a Word (Binary)" +
                "\n3: Spell Check Alice in Wonderland (Linear)" +
                "\n4: Spell Check Alice in Wonderland (Binary)" +
                "\n5: Exit"
                );
            Console.Write("Enter Menu Selection (1-5): ");
            int uNum = Convert.ToInt32(Console.ReadLine());
            loop = ExecuteOption(uNum, dictionary, aliceWords);
        }
    }

    // Handel User's Decision
    public static bool ExecuteOption(int num, string[] dictionary, string[] alice)
    {
        // Define Variables
        string uWord;
        int badWordCount = 0;
        int searchResult;

        // Create Stopwatch to tell user how long the program took
        Stopwatch stopWatch = new Stopwatch();

        // Switch case calls the necessary functions
        switch (num)
        {
            // Use LinearSearch method with the dictionary, the user compares their input ot the contents of the dictionary
            case 1:
                Console.Write("Please enter a word: ");
                uWord = Console.ReadLine();
                TimeSpan ts1 = stopWatch.Elapsed;
                Console.WriteLine(ParseResults(LinearSearch(dictionary, uWord.ToLower()), 0, ts1, true));
                break;
            // Similar to case #1 but instead using BinarySearch method
            case 2:
                Console.Write("Please enter a word: ");
                uWord = Console.ReadLine();
                TimeSpan ts2 = stopWatch.Elapsed;
                Console.WriteLine(ParseResults(BinarySearch(dictionary, uWord.ToLower()), 0, ts2, true));
                break;
            // Use LinearSearch method to compare each word in Alice in wonderland against the dictionary, counting incorrect words
            case 3:
                stopWatch.Start();
                for (int i = 0; i < alice.Length; i++)
                {
                    searchResult = LinearSearch(dictionary, alice[i].ToLower());
                    if (searchResult < 0)
                    {
                        // Tally the incorrect words | badWordCount should be 7082
                        badWordCount++;
                    }
                }
                stopWatch.Stop();
                TimeSpan ts3 = stopWatch.Elapsed;
                Console.WriteLine(ParseResults(0, badWordCount, ts3, false));
                break;
            // Similar to case #3 but using BinarySearch method, much faster
            case 4:
                stopWatch.Start();
                for (int i = 0; i < alice.Length; i++)
                {
                    searchResult = BinarySearch(dictionary, alice[i].ToLower());
                    if (searchResult < 0)
                    {
                        // Tally the incorrect words | badWordCount should be 7082
                        badWordCount++;
                    }
                }
                stopWatch.Stop();
                TimeSpan ts4 = stopWatch.Elapsed;
                Console.WriteLine(ParseResults(0, badWordCount, ts4, false));
                break;
            // Allow user to render the 'loop' variable false, ending the program
            case 5:
                return false;
            // Error message
            default:
                Console.WriteLine("Invalid input, please try again");
                break;
        }
        return true;
    }

    // Return a string of text displaying the time the program took in 'seconds.miliseconds'
    public static string ParseResults(int uNum, int badWordCount, TimeSpan timeSpan, bool isUserInput)
    {
        // Determine if the user had input
        if (isUserInput && uNum == -1)
        {
            return "Word not found.";
        }
        else if (isUserInput && uNum != -1)
        {
            return $"Word found at position {uNum}. ({timeSpan.Seconds}.{timeSpan.Milliseconds} Seconds)";
        }
        // If not, return alice in wonderland string
        else
        {
            return $"{badWordCount} words in Alice in wonderland were NOT found in the dictionary. ({timeSpan.Seconds}.{timeSpan.Milliseconds} Seconds)";
        }
    }


    static int LinearSearch(string[] dictionary, string item)
    {
        for (int i = 0; i < dictionary.Length; i++)
        {
            if (dictionary[i] == item)
            {
                return i;
            }
        }
        return -1;
    }

    static int BinarySearch(string[] dictionary, string item)
    {
        // Define Variables
        int lowerIndex = 0;
        int middleIndex;
        int upperIndex = dictionary.Length - 1;


        while (lowerIndex <= upperIndex)
        {
            middleIndex = (lowerIndex + upperIndex) / 2;
            int result = string.Compare(item, dictionary[middleIndex]);

            if (result == 0)
            {
                return middleIndex;
            }
            else if (result < 0)
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
