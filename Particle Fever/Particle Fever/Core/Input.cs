using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;

namespace Particle_Fever
{
    public class Input
    {
        public enum KeyState
        {
            PRESSED = 0,
            DOWN,
            UP,
            NONE
        }

        private static KeyState[] _keys;
        private static bool[] _hasBeenReleased;

        public Input()
        {
            
        }

        public static void OnLoad(EventArgs e)
        {
            _keys = new KeyState[(int)Key.LastKey];
            _hasBeenReleased = new bool[(int)Key.LastKey];

            for (int i = 0; i < (int)Key.LastKey; i++)
            {
                _keys[i] = KeyState.NONE;
                _hasBeenReleased[i] = false;
            }

            SetupInputEvents();
        }

        public static void OnUpdateFrame(FrameEventArgs e)
        {

        }

        public static void OnRenderFrame(FrameEventArgs e)
        {
            for (int i = 0; i < (int)Key.LastKey; i++)
            {
                if (_keys[i] == KeyState.PRESSED)
                {
                    if (!_hasBeenReleased[i])
                    {
                        _keys[i] = KeyState.DOWN;
                    }
                }

                if (_keys[i] == KeyState.UP)
                    _keys[i] = KeyState.NONE;

                if (_hasBeenReleased[i])
                    _hasBeenReleased[i] = false;
            }
        }

        public static bool IsKeyDown(Key key)
        {
            return _keys[(int)key] == KeyState.DOWN || _keys[(int)key] == KeyState.PRESSED;
        }

        public static bool IsKeyPressed(Key key)
        {
            return _keys[(int)key] == KeyState.PRESSED;
        }

        public static bool IsKeyUp(Key key)
        {
            return _keys[(int)key] == KeyState.UP;
        }

        private static void SetupInputEvents()
        {
            Program.Game.Keyboard.KeyDown += Keyboard_KeyDown;
            Program.Game.Keyboard.KeyUp += Keyboard_KeyUp;
            Program.Game.Keyboard.KeyRepeat = true;

            Program.Game.Mouse.ButtonDown += Mouse_ButtonDown;
            Program.Game.Mouse.ButtonUp += Mouse_ButtonUp;
            Program.Game.Mouse.Move += Mouse_Move;
            Program.Game.Mouse.WheelChanged += Mouse_Wheel;
        }

        private static void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (_keys[(int)e.Key] == KeyState.PRESSED)
            {
                _keys[(int)e.Key] = KeyState.DOWN;
                return;
            }
            if(_keys[(int)e.Key] == KeyState.NONE)
            {
                _keys[(int)e.Key] = KeyState.PRESSED;
                return;
            }
        }

        private static void Keyboard_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            _keys[(int)e.Key] = KeyState.UP;
            _hasBeenReleased[(int)e.Key] = true;
        }

        private static void Mouse_ButtonDown(object sender, MouseButtonEventArgs args)
        {
            
        }

        private static void Mouse_ButtonUp(object sender, MouseButtonEventArgs args)
        {
            
        }

        private static void Mouse_Move(object sender, MouseMoveEventArgs args)
        {
            
        }

        private static void Mouse_Wheel(object sender, MouseWheelEventArgs args)
        {
            
        }
    }
}
