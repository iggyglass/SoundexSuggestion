using System;
using System.Collections.Generic;
using System.Text;

namespace Soundex
{
    public static class Soundex // this implimentation uses american soundex because, well, I'm american
    {

        private static HashSet<char> vowels = new HashSet<char>()
        {
            'a',
            'e',
            'i',
            'o',
            'u',
            'y'
        };

        private static HashSet<char> specialCase = new HashSet<char>()
        {
            'h',
            'w'
        };

        private static Dictionary<char, char> consonantMappings = new Dictionary<char, char>()
        {
            ['b'] = '1',
            ['f'] = '1',
            ['p'] = '1',
            ['v'] = '1',

            ['c'] = '2',
            ['g'] = '2',
            ['j'] = '2',
            ['k'] = '2',
            ['q'] = '2',
            ['s'] = '2',
            ['x'] = '2',
            ['z'] = '2',

            ['d'] = '3',
            ['t'] = '3',

            ['l'] = '4',

            ['m'] = '5',
            ['n'] = '5',

            ['r'] = '6'
        };

        /// <summary>
        /// Converts a word to its corresponding soundex
        /// </summary>
        /// <param name="str">The word to convert</param>
        /// <returns>The soundex code</returns>
        public static string StringToSoundex(string str)
        {
            StringBuilder builder = new StringBuilder();
            
            str = str.ToLower();
            builder.Append(str[0]);

            bool lastVowel = vowels.Contains(str[0]); // whether the most recent character was a vowel
            char prev = lastVowel || specialCase.Contains(str[0]) ? str[0] : consonantMappings[str[0]]; // set prev to whatever number the first char corresponds to

            for (int i = 1; i < str.Length; i++)
            {
                if (consonantMappings.ContainsKey(str[i])) // letter is a consonant that isn't h or w
                {
                    char current = consonantMappings[str[i]];

                    if (!lastVowel && prev == current) // if it is preceded by a "similar" letter, dont add it
                    {
                        lastVowel = false;
                        continue;
                    }

                    builder.Append(current);
                    prev = current;
                    lastVowel = false;
                }
                else if (specialCase.Contains(str[i])) // letter is h or w
                {
                    lastVowel = false;
                }
                else // letter is vowel
                {
                    lastVowel = true;
                }
            }

            // Ensure length of 4
            if (builder.Length > 4)
            {
                builder.Append('0', 4 - builder.Length);
            }
            else if (builder.Length < 4)
            {
                builder.Remove(4, builder.Length - 4);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Takes in two words and finds how close they are (in terms of soundex) on a scale of 0 to 4 (4 being exact match)
        /// </summary>
        /// <param name="a">The first word</param>
        /// <param name="b">The second word</param>
        /// <returns>The difference</returns>
        public static int Difference(string a, string b) // using the rules outlined here: https://www.techrepublic.com/blog/software-engineer/how-do-i-implement-the-soundex-function-in-c/#:~:text=Soundex%20is%20an%20algorithm%20that,pronounced%20exactly%20the%20same%20way
        {
            string soundexA = StringToSoundex(a);
            string soundexB = StringToSoundex(b);
            int diff = 0;

            if (soundexA == soundexB) return 4;

            string soundexANums = soundexA.Substring(1);
            string soundexALastTwo = soundexA.Substring(2);
            string soundexAMiddleTwo = soundexA.Substring(1, 2);

            if (soundexB.Contains(soundexANums))
            {
                diff = 3;
            }
            else if (soundexB.Contains(soundexALastTwo))
            {
                diff = 2;
            }
            else if (soundexB.Contains(soundexAMiddleTwo))
            {
                diff = 2;
            }
            else
            {
                for (int i = 1; i < soundexA.Length; i++)
                {
                    if (soundexB.Contains(soundexA[i]))
                    {
                        diff++;
                    }
                }
            }

            if (soundexA[0] == soundexB[0])
            {
                diff++;
            }

            return diff;
        }
    }
}
