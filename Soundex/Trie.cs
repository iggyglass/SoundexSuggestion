using System;
using System.Collections.Generic;
using System.Text;

namespace Soundex
{
    public class Trie
    {

        private TrieNode root = new TrieNode('$');

        public void Insert(string word)
        {
            TrieNode current = root;

            for (int i = 0; i < word.Length; i++)
            {
                if (current.Children.ContainsKey(word[i]))
                {
                    current = current.Children[word[i]];
                }
                else
                {
                    TrieNode node = new TrieNode(word[i]);
                    current.Children.Add(word[i], node);

                    current = node;
                }
            }

            current.IsWord = true;
            current.Value = word;
        }

        public bool Remove(string word)
        {
            TrieNode current = search(word);

            if (!current.IsWord) return false;

            current.IsWord = false;
            current.Value = "";

            return true;
        }

        private TrieNode search(string prefix)
        {
            TrieNode current = root;

            for (int i = 0; i < prefix.Length; i++)
            {
                if (current.Children.ContainsKey(prefix[i]))
                {
                    current = current.Children[prefix[i]];
                }
                else
                {
                    break;
                }
            }

            if (current == root) return null;

            return current;
        }

        public List<string> GetAllMatchingWords(string prefix)
        {
            TrieNode current = root;

            for (int i = 0; i < prefix.Length; i++)
            {
                if (current.Children.ContainsKey(prefix[i]))
                {
                    current = current.Children[prefix[i]];
                }
                else
                {
                    return new List<string>();
                }
            }

            List<string> words = getWords(current);   

            return words;
        }

        public List<string> GetAllWords()
        {
            return getWords(root);
        }

        private List<string> getWords(TrieNode start)
        {
            List<string> words = new List<string>();
            Queue<TrieNode> nodes = new Queue<TrieNode>();
            TrieNode current;

            nodes.Enqueue(start);

            while (nodes.Count > 0)
            {
                current = nodes.Dequeue();

                if (current.IsWord)
                {
                    words.Add(current.Value);
                }

                foreach (var kvp in current.Children)
                {
                    nodes.Enqueue(kvp.Value);
                }
            }

            return words;
        }
    }
}
