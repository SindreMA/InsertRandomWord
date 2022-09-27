using System;
using System.IO;
using System.Linq;
using WindowsInput;
using System.Runtime.InteropServices;
using System.Threading;

namespace InsertRandomWord
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;


        public static string GetRandomText()
        {
            string path = Directory.GetCurrentDirectory();

            var words = File.ReadAllLines(@"C:\Users\Sindre\source\repos\InsertRandomWord\words.txt");
            var wordCount = new Random().Next(2, 3);

            var word = "";
            for (int i = 0; i < wordCount; i++)
            {
                var count = new Random().Next(2, 7);
                word += (words.Where(x => x.Length == count).OrderBy(x => new Random().Next()).FirstOrDefault()).FirstCharToUpper();
            }
            return word;
        }

        public static void InsertText(string text)
        {
            var helper = new InputSimulator();
            helper.Keyboard.TextEntry(text);
        }
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

                InsertText(GetRandomText());
                InsertText(GetRandomText() + new Random().Next(1000, 9999).ToString() + ".#" );
            
        }
    }
}
