using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace CrowEngine.Core.Helpers;

public static class ColourHelpers
{
    public static Color ColorFromID(int id)
    {
        byte r = (byte)((id >> 16) & 0xFF);
        byte g = (byte)((id >> 8) & 0xFF);
        byte b = (byte)(id & 0xFF);
        
        return new Color(r,g,b);
    }
    
    public static int IDFromColor(Color color)
    {
        return (color.R << 16) | (color.G << 8) | color.B; 
    }
}
