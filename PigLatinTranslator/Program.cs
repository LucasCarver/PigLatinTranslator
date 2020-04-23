using System;

namespace PigLatinTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            string normalWord;
            string outWord;
            Console.WriteLine("Pig Latin Translator");

            do
            {
                normalWord = PromptUser("Enter a word / sentence to translate to pig latin.");
                outWord = PigLatinSentence(normalWord);
                Console.WriteLine(outWord);
            } while (!ExitLoop("Would you like to translate another word?"));
        }

        public static string PromptUser(string prompt)
        {
            Console.WriteLine(prompt);
            string response = Console.ReadLine().Trim();
            return response;
        }

        public static bool ExitLoop(string exitMessage)
        {
            bool stopLoop = false;
            string tempString;
            do
            {
                tempString = PromptUser(exitMessage).ToLower().Trim();

                if (tempString == "y" || tempString == "n")
                {
                    stopLoop = true;
                }
                else
                {
                    Console.WriteLine("Invalid entry.\n");
                    stopLoop = false;
                }
            } while (!stopLoop);
            if (tempString == "n")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string PigLatinWord(string input)
        {
            string output = "";
            string beginningConsonants;

            if (input.StartsWith("a") || input.StartsWith("e") || input.StartsWith("i") ||
                input.StartsWith("o") || input.StartsWith("u"))
            {
                output = String.Concat(input, "way");
            }
            else
            {
                beginningConsonants = GetBeginningConsonants(input);
                int firstVowelIndex = beginningConsonants.Length;
                output = String.Concat(input.Substring(firstVowelIndex, input.Length - beginningConsonants.Length), beginningConsonants + "ay");
            }
            return output;
        }

        public static string GetBeginningConsonants(string input)
        {
            string consonants = "";
            string testChar = "";
            bool breakLoop = true;
            for(int i = 0; (i < input.Length) || breakLoop != true; i++)
            {
                testChar = input.Substring(i, 1).ToLower();
                if(testChar == "a" || testChar == "e" || testChar == "i" || testChar == "o" || testChar == "u")
                {
                    breakLoop = true;
                    break;
                }
                else
                {
                    consonants += testChar;
                }
            }
            return consonants;
        }

        public static string PigLatinSentence(string sentence)
        {
            string outSentence = "";
            string[] words = sentence.Split(" ");
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = PigLatinWord(words[i]);
                outSentence += words[i] + " ";
            }
            return outSentence;
        }
    }
}
