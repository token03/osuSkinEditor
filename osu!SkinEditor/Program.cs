using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace osu_SkinEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            bool editing = true;
            SkinEditor editor = new SkinEditor();
            while (editing)
            {
                Console.Clear();
                editor.displaySkins();
                editor.selectEdit();
                editor.selectAsset();
                editor.replace();

                Console.WriteLine("Replacement is finished, Press Q to QUIT ");
                Console.WriteLine("\n Press any other key to continue.");
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Q: editing = false; break;
                }

            }

        }

        class SkinEditor
        {
            protected string skinRoot = @"C:\Users\jiank\AppData\Local\osu!\Skins";
            protected string assetsRoot = @"C:\Users\jiank\OneDrive\Desktop\Skin Assets";
            protected string selectedSkin, selectedEdit, selectedAsset;
            protected string[] skinFolders;
            public SkinEditor()
            {
                skinFolders = Directory.GetDirectories(skinRoot, "*.*", SearchOption.TopDirectoryOnly);
            }

            public void displaySkins()
            {
                int counter = 0;

                foreach (string skin in skinFolders)
                {
                    Console.WriteLine($"[{counter}]" + Path.GetFileName(skin));
                    counter++;
                }

                Console.WriteLine("\nSelect the skin you would like to edit:");
                selectedSkin = skinFolders[Convert.ToInt32(Console.ReadLine())];
                Console.Clear();
            }

            public void selectEdit()
            {
                string[] assets = Directory.GetDirectories(assetsRoot);
                int counter = 0;
                Console.WriteLine($"-Editing: {Path.GetFileName(selectedSkin)}-\n");
                foreach (string asset in assets)
                {
                    Console.WriteLine($"[{counter}]" + Path.GetFileName(asset));
                    counter++;
                }
                Console.WriteLine("\nSelect the asset you would like to edit:");
                selectedEdit = assets[Convert.ToInt32(Console.ReadLine())];
                Console.Clear();
            }

            public void selectAsset()
            {
                string[] possibleAssets = Directory.GetDirectories(selectedEdit);
                int counter = 0;
                Console.WriteLine($"-Editing: {Path.GetFileName(selectedEdit)}-\n");
                foreach (string asset in possibleAssets)
                {
                    Console.WriteLine($"[{counter}]" + Path.GetFileName(asset));
                    counter++;
                }
                Console.WriteLine("\nSelect the asset you would like to use:");
                selectedAsset = possibleAssets[Convert.ToInt32(Console.ReadLine())];
                Console.Clear();
            }

            public void replace()
            {
                string[] replacements = Directory.GetFiles(selectedAsset);
                string remove1 = "cursor@2x.png", remove2 = "cursortrail@2x.png";
                File.Delete(Path.Combine(selectedSkin, remove1));
                File.Delete(Path.Combine(selectedSkin, remove2));
                foreach (string asset in replacements)
                {
                    string fName = Path.GetFileName(asset);
                    File.Copy(Path.Combine(selectedAsset, fName), Path.Combine(selectedSkin, fName), true);
                }
            }
        }
    }
}
