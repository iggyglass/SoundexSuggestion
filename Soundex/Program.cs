using System;
using System.IO;
using System.Collections.Generic;

namespace Soundex
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialization
            Trie words = new Trie();
            Dictionary<string, List<string>> soundexWords = new Dictionary<string, List<string>>();

            string[] lines = File.ReadAllLines("words_alpha.txt");

            for (int i = 0; i < lines.Length; i++)
            {
                string soundex = Soundex.StringToSoundex(lines[i]);

                if (!soundexWords.ContainsKey(soundex))
                {
                    soundexWords.Add(soundex, new List<string>());
                }

                soundexWords[soundex].Add(lines[i]);

                words.Insert(lines[i]);
            }

            // Look into https://en.wikipedia.org/wiki/Edit_distance
            // and https://en.wikipedia.org/wiki/Levenshtein_distance

            // The actual thing
            while (true)
            {
                Console.WriteLine("Gib word: ");
                string input = Console.ReadLine().ToLower();
                string soundex = Soundex.StringToSoundex(input);

                Console.WriteLine("Matches: ");
                List<string> matches = words.GetAllMatchingWords(input);

                for (int i = 0; i < matches.Count; i++)
                {
                    Console.WriteLine(matches[i]);
                }

                Console.WriteLine("Similar: ");
                if (soundexWords.ContainsKey(soundex))
                {
                    List<string> similar = soundexWords[soundex];

                    for (int i = 0; i < similar.Count; i++)
                    {
                        Console.WriteLine(similar[i]);
                    }
                }
            }
        }
    }
}
