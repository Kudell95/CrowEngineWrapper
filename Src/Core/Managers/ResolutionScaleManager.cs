using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UntitledCardGame;

namespace CosmicCrowGames.Core
{
    //TODO: this will likely need to be scrapped and redone, need to read up on how to do this properly
    public class ResolutionScaleManager
    {
        private GraphicsDeviceManager graphics;
        private Matrix scaleMatrix;
        private int virtualWidth;
        private int virtualHeight;
        private int screenWidth;
        private int screenHeight;

        public ResolutionScaleManager(GraphicsDeviceManager graphics, int virtualWidth, int virtualHeight)
        {
            this.graphics = graphics;
            this.virtualWidth = virtualWidth;
            this.virtualHeight = virtualHeight;
            ApplyResolutionSettings();
        }

        public void ApplyResolutionSettings()
        {
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;
            float scaleX = (float)screenWidth / virtualWidth;
            float scaleY = (float)screenHeight / virtualHeight;
            float scale = Math.Max(scaleX, scaleY);
            scaleMatrix = Matrix.CreateScale(scale, scale, 1f);
        }

        public Matrix GetScaleMatrix()
        {
            return scaleMatrix;
        }
        public void UpdateScaleMatrix()
        {
            int screenWidth = graphics.GraphicsDevice.PresentationParameters.BackBufferWidth;
            int screenHeight = graphics.GraphicsDevice.PresentationParameters.BackBufferHeight;

            float scaleX = (float)screenWidth / virtualWidth;
            float scaleY = (float)screenHeight / virtualHeight;
            float scale = Math.Min(scaleX, scaleY);

            scaleMatrix = Matrix.CreateScale(scale, scale, 1f);

            // Calculate the viewport dimensions while maintaining aspect ratio
            int viewportWidth = (int)(virtualWidth * scale);
            int viewportHeight = (int)(virtualHeight * scale);

            // Center the viewport within the window
            int viewportX = (screenWidth - viewportWidth) / 2;
            int viewportY = (screenHeight - viewportHeight) / 2;

            graphics.GraphicsDevice.Viewport = new Viewport(viewportX, viewportY, viewportWidth, viewportHeight);
        }

    }
}