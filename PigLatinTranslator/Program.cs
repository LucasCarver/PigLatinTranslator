using System;

namespace PigLatinTranslator
{
    class Program
    {
        private static string SYMBOL_LIST = @"0123456789!@#$%^&*()_+-=[]{}\|:<>,.'""";
        private static string VOWEL_LIST = "aeiou";

        static void Main(string[] args)
        {
            string bigInput;
            string bigOutput;
            bool isEmpty;

            Console.WriteLine("Pig Latin Translator");

            do
            {
                isEmpty = false;
                bigInput = PromptUser("Enter a line to be translated.");
                if (String.IsNullOrEmpty(bigInput))
                {
                    Console.WriteLine("Input is null or empty.");
                    isEmpty = true;
                }
                else
                {
                    bigOutput = PigLatinSentence(bigInput);
                    Console.WriteLine(bigOutput.Trim());
                }
            } while (isEmpty||!ExitLoop("Translate again?"));
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

            if (GetFirstVowel(input.ToCharArray()) == 0)
            {
                output = input + "way";
            }
            else
            {
                beginningConsonants = GetBeginningConsonants(input);
                int firstVowelIndex = beginningConsonants.Length;
                output = input.Substring(firstVowelIndex, input.Length - beginningConsonants.Length) + beginningConsonants + "ay";
            }
            return output;
        }

        public static string PigLatinWord(string input, bool noVowels)
        {
            string output = "";
            string beginningConsonants;
            int firstVowelIndex;
            if (noVowels)
            {
                beginningConsonants = GetBeginningConsonants(input, true);
                firstVowelIndex = beginningConsonants.Length;
                output = String.Concat(input.Substring(firstVowelIndex, input.Length - beginningConsonants.Length), beginningConsonants + "ay");
            }
            else if (ContainsSymbol(input, VOWEL_LIST))
            {
                output = String.Concat(input, "way");
            }
            else
            {
                beginningConsonants = GetBeginningConsonants(input);
                firstVowelIndex = beginningConsonants.Length;
                output = input.Substring(firstVowelIndex, input.Length - beginningConsonants.Length) + beginningConsonants + "ay";
            }
            return output;
        }

        public static int GetFirstVowel(char[] word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                char letter = word[i];
                if (IsVowel(letter))
                {
                    return i;
                }
            }
            return -1;
        }

        public static string GetBeginningConsonants(string input)
        {
            string consonants = "";
            char testChar;
            bool upperCase;
            bool breakLoop = true;
            for (int i = 0; (i < input.Length) || breakLoop != true; i++)
            {
                upperCase = char.IsUpper(input[i]);
                testChar = char.ToLower(input[i]);
                if (IsVowel(testChar))
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

        public static string GetBeginningConsonants(string input, bool noVowels)
        {
            string consonants = "";
            char testChar;
            bool breakLoop = true;
            if (noVowels)
            {

                for (int i = 0; (i < input.Length) || breakLoop != true; i++)
                {
                    testChar = char.ToLower(input[i]);
                    if (testChar == 'y')
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
            else
            {
                for (int i = 0; (i < input.Length) || breakLoop != true; i++)
                {
                    testChar = char.ToLower(input[i]);
                    if (ContainsSymbol(input, VOWEL_LIST))
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
        }

        public static string PigLatinSentence(string sentence)
        {

            string punctList = @".?!,:;-()[]'""";
            string outSentence = "";
            string[] words = sentence.Split(" ");
            bool punctFlag;
            for (int i = 0; i < words.Length; i++)
            {
                punctFlag = false;
                if (ContainsSymbol(words[i], punctList))
                {
                    foreach (char c in punctList)
                    {
                        if (words[i].EndsWith(c))
                        {
                            words[i] = PigLatinWord(words[i].Substring(0, words[i].Length - 1));
                            outSentence += words[i] + c + " ";
                            punctFlag = true;
                        }
                    }
                }

                if (punctFlag)
                {
                    continue;
                }

                if (int.TryParse(words[i], out int num))
                {
                    words[i] = num.ToString();
                    outSentence += words[i] + " ";
                }
                else if (words[i].EndsWith("'t") || words[i].EndsWith("'ve") || words[i].EndsWith("'s") || words[i].EndsWith("'re") || words[i].EndsWith("'d") ||
                        words[i].EndsWith("'ll") || words[i].EndsWith("'m") || words[i].EndsWith("'d") || words[i].EndsWith("'all") || words[i].EndsWith("'a") ||
                        words[i].EndsWith("'am") || words[i].EndsWith("'ye") || words[i].EndsWith("'er"))
                {
                    words[i] = PigLatinWord(words[i]);
                    outSentence += words[i] + " ";
                }
                else if (ContainsSymbol(words[i], SYMBOL_LIST))
                {
                    outSentence += words[i] + " ";
                }
                else if (!ContainsSymbol(words[i], VOWEL_LIST))
                {
                    words[i] = PigLatinWord(words[i], true);
                    outSentence += words[i] + " ";
                }
                else
                {
                    words[i] = PigLatinWord(words[i]);
                    outSentence += words[i] + " ";
                }

            }
            return outSentence;
        }

        public static bool ContainsSymbol(string word, string specialChars)
        {

            foreach (char c in specialChars)
            {
                if (word.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsVowel(char letter)
        {
            foreach (char c in VOWEL_LIST)
            {
                if (letter == c)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
