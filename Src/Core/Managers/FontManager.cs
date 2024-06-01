
using System;
using System.Collections.Generic;
using System.IO;
using FontStashSharp;

namespace CosmicCrowGames.Core.Fonts
{
    public static class FontManager
    {
        // public static FontSystem MainFontSystem;

        private static Dictionary<String,FontSystem> Fonts = new Dictionary<string, FontSystem>();

        private static string DefaultFont = AvailableFonts.ConsolasBold;

        public static void Initialize()
        {
            // if(!Directory.Exists("Fonts"))
            // {
                
            // }

            // MainFontSystem = new FontSystem();
            // MainFontSystem.AddFont(File.ReadAllBytes(Path.Combine(fontPath, "Consolas.ttf")));
            AddFont(AvailableFonts.ConsolasItalics);
            AddFont(AvailableFonts.ConsolasBold);

            if(!Fonts.ContainsKey(DefaultFont))
            {
                throw new Exception("Default Font Not Found");
            }
        }
        
        /// <summary>
        /// Adds a font to the font manager
        /// </summary>
        /// <param name="fontname">the filename of the .ttf without the extension</param>
        public static void AddFont(string fontname)
        {
            string fontPath = "Content/Fonts/";
            FontSystem fontSystem = new FontSystem();
            fontSystem.AddFont(File.ReadAllBytes(Path.Combine(fontPath, fontname + ".TTF")));
            Fonts.Add(fontname, fontSystem);

        }


        public static SpriteFontBase GetFont(string fontname, int size)
        {
            if(!Fonts.ContainsKey(fontname))
            {
                Console.WriteLine($"Warning! Font: {fontname} not found! defaulting to {DefaultFont}");
                return Fonts[DefaultFont].GetFont(size);
            }


            return Fonts[fontname].GetFont(size);
        }



    }
    

    
    public static class AvailableFonts
    {
        public static string ConsolasBold = "CONSOLAB";
        
        public static string ConsolasItalics = "consolas";
    }
}