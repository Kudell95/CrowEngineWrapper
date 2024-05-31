
using System.Collections.Generic;
using System.IO;
using FontStashSharp;

namespace CosmicCrowGames.Core.Fonts
{
    public static class FontManager
    {
        public static FontSystem MainFontSystem;

        public static void Initialize()
        {
            // if(!Directory.Exists("Fonts"))
            // {
                
            // }

            string fontPath = "Content/Fonts/";


            MainFontSystem = new FontSystem();
            MainFontSystem.AddFont(File.ReadAllBytes(Path.Combine(fontPath, "Consolas.ttf")));

            
        }



    }
}