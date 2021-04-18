using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Stopwatch stopwatch = new Stopwatch();
        string userText;
        List<string> items = new List<string>();
        List<string> itemsFound = new List<string>();
        Trie trie = new Trie();
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            userText = textBox1.Text.ToLower().ToString();
            items.Add(userText.ToLower().ToString());
            trie.Insert(userText);
            ListNames();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var stream = new StreamReader("E:/Ata/Software/Visualstudio/projects/TrieTree/TrieTree/vocabulary.txt");
            while (!stream.EndOfStream)
               items.Add(stream.ReadLine().ToLower());

            if (items.Count>1)
            {
                InsertItemsFromText(items);
                ListNames();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            itemsFound.Clear();
            listBoxSuggestion.Items.Clear();
            userText = textBox1.Text.ToLower().ToString();
            stopwatch.Start();
            var prefix = trie.Prefix(userText);
            if (!prefix.IsLeaf())
            {
                FindAllChild(prefix);
            }
            void FindAllChild(Node node)
            {
                foreach (var item in node.Children)
                {
                    if (item.FindChildNode('$') != null)
                    {
                        itemsFound.Add(item.FullValue);
                    }
                        FindAllChild(item);
                }
            }
            foreach (var item in itemsFound)
            {
                listBoxSuggestion.Items.Add(item.ToString());
            }
            
            bool foundT = prefix.Depth == userText.Length && prefix.FindChildNode('$') != null;
            stopwatch.Stop();
            label2.Text=$"Trie search in {stopwatch.ElapsedTicks} ticks found:{foundT}";
            stopwatch.Reset();
        }

        void ListNames()
        {
            listBox1.Items.Clear();
            foreach (string item in items)
            {
                listBox1.Items.Add(item.ToString());
            }
        }
        void InsertItemsFromText(List<string> item)
        {
            stopwatch.Start();
            trie.InsertRange(items);
            stopwatch.Stop();
            label1.Text = $"Trie insertion in {stopwatch.ElapsedTicks} ticks";
            stopwatch.Reset();
        }
    }
}
