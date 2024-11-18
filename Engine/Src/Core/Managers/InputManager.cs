

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CosmicCrowGames.Core
{
    public static class InputManager
    {
        static KeyboardState _CurrentKeyState;
        static KeyboardState _PreviousKeyState;


        public static KeyboardState GetKeyState()
        {
            _PreviousKeyState = _CurrentKeyState;
            _CurrentKeyState = Keyboard.GetState();
            return _CurrentKeyState;
        }


        public static void Update(GameTime gameTime)
        {
            GetKeyState();
        }


        public static bool GetKey(Keys key){
            return _CurrentKeyState.IsKeyDown(key);
        }

        public static bool GetKeyDown(Keys key){
            return _CurrentKeyState.IsKeyDown(key) && !_PreviousKeyState.IsKeyDown(key);
        }

        public static bool GetKeyUp(Keys key)
        {
            return _CurrentKeyState.IsKeyUp(key) && _PreviousKeyState.IsKeyDown(key);
        }

    }
}