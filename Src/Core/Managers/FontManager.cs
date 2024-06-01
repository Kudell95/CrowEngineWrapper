
using System;
using System.Collections.Generic;
using System.IO;
using FontStashSharp;

namespace CosmicCrowGames.Core.Fonts
{
    //NOTE: see here: https://github.com/FontStashSharp/FontStashSharp/wiki/Rich-Text for rich text cheat sheet.


    public static class FontManager
    {

        private static Dictionary<String,FontSystem> Fonts = new Dictionary<string, FontSystem>();

        private static string DefaultFont = AvailableFonts.ConsolasBold;

        public static void Initialize()
        {
            //NOTE: remember to update available fonts when adding new fonts...
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
            //NOTE: ideally just one font system would be ideal, but although you can add multiple fonts, i can see no way of selecting fonts without knowing the font atlas (and the font atlas collection is null anyway)
            // Could just grab source and expand on the font, but for now this is easy and relatively low cost solution (except for some extra memory usage i guess?)
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