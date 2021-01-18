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
            /* if (builder.Length > 4)
            {
                builder.Append('0', 4 - builder.Length);
            }
            else if (builder.Length < 4)
            {
                builder.Remove(4, builder.Length - 4);
            } */

            return builder.ToString();
        }
    }
}
