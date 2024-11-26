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
        private int m_VirtualWidth;
        private int m_VirtualHeight;
        private float m_ResolutionWidth;
        private float m_ResolutionHeight;
        
        public Viewport ScreenViewport;

        public ResolutionScaleManager(GraphicsDeviceManager graphics, int resolutionWidth, int resolutionHeight)
        {
            this.graphics = graphics;
            this.m_ResolutionWidth = resolutionWidth;
            this.m_ResolutionHeight = resolutionHeight;
            ApplyResolutionSettings();
        }

        public void ApplyResolutionSettings()
        {
            float screenWidth = graphics.GraphicsDevice.PresentationParameters.BackBufferWidth;
            float screenHeight = graphics.GraphicsDevice.PresentationParameters.BackBufferHeight;

            if (screenWidth /  m_ResolutionWidth > screenHeight / m_ResolutionHeight)
            {
                float aspect = screenHeight / m_ResolutionHeight;
                
                m_VirtualWidth = (int)(m_ResolutionWidth * aspect);
                m_VirtualHeight = (int)screenHeight;
            }
            else
            {
                float aspect = screenWidth / m_ResolutionWidth;
                m_VirtualWidth = (int)screenWidth;
                m_VirtualHeight = (int)(aspect * m_ResolutionHeight);
            }
            
            
            scaleMatrix = Matrix.CreateScale((float)(m_VirtualWidth / m_ResolutionWidth));

            ScreenViewport = new Viewport
            {
                X = 0,
                Y = 0,
                Width =  (int)screenWidth,
                Height = (int)screenHeight,
                MinDepth = 0,
                MaxDepth = 1
            };

        }

        public Matrix GetScaleMatrix()
        {
            return scaleMatrix;
        }
       

    }
}