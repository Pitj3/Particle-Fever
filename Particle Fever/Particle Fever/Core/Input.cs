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
        public enum ButtonState
        {
            PRESSED = 0,
            DOWN,
            UP,
            NONE
        }

        private static ButtonState[] _keys;
        private static ButtonState[] _mousebuttons;
        private static bool[] _hasKeyBeenReleased;
        private static bool[] _hasMouseBeenReleased;

        private static Vector2 _mousePosition;

        public Input()
        {
            
        }

        public static void OnLoad(EventArgs e)
        {
            _keys = new ButtonState[(int)Key.LastKey];
            _mousebuttons = new ButtonState[(int)MouseButton.LastButton];

            _hasKeyBeenReleased = new bool[(int)Key.LastKey];
            _hasMouseBeenReleased = new bool[(int)MouseButton.LastButton];

            for (int i = 0; i < (int)Key.LastKey; i++)
            {
                _keys[i] = ButtonState.NONE;
                _hasKeyBeenReleased[i] = false;
            }

            for (int i = 0; i < (int)MouseButton.LastButton; i++)
            {
                _mousebuttons[i] = ButtonState.NONE;
                _hasMouseBeenReleased[i] = false;
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
                if (_keys[i] == ButtonState.PRESSED)
                {
                    if (!_hasKeyBeenReleased[i])
                    {
                        _keys[i] = ButtonState.DOWN;
                    }
                }

                if (_keys[i] == ButtonState.UP)
                    _keys[i] = ButtonState.NONE;

                if (_hasKeyBeenReleased[i])
                    _hasKeyBeenReleased[i] = false;
            }

            for (int i = 0; i < (int)MouseButton.LastButton; i++)
            {
                if (_mousebuttons[i] == ButtonState.PRESSED)
                {
                    if (!_hasMouseBeenReleased[i])
                    {
                        _mousebuttons[i] = ButtonState.DOWN;
                    }
                }

                if (_mousebuttons[i] == ButtonState.UP)
                    _mousebuttons[i] = ButtonState.NONE;

                if (_hasMouseBeenReleased[i])
                    _hasMouseBeenReleased[i] = false;
            }
        }

        public static bool IsKeyDown(Key key)
        {
            return _keys[(int)key] == ButtonState.DOWN || _keys[(int)key] == ButtonState.PRESSED;
        }

        public static bool IsKeyPressed(Key key)
        {
            return _keys[(int)key] == ButtonState.PRESSED;
        }

        public static bool IsKeyUp(Key key)
        {
            return _keys[(int)key] == ButtonState.UP;
        }

        public static bool IsMouseButtonDown(MouseButton button)
        {
            return _mousebuttons[(int)button] == ButtonState.DOWN || _mousebuttons[(int)button] == ButtonState.PRESSED;
        }

        public static bool IsMouseButtonPressed(MouseButton button)
        {
            return _mousebuttons[(int)button] == ButtonState.PRESSED;
        }

        public static bool IsMouseButtonUp(MouseButton button)
        {
            return _mousebuttons[(int)button] == ButtonState.UP;
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
            if (_keys[(int)e.Key] == ButtonState.PRESSED)
            {
                _keys[(int)e.Key] = ButtonState.DOWN;
                return;
            }
            if(_keys[(int)e.Key] == ButtonState.NONE)
            {
                _keys[(int)e.Key] = ButtonState.PRESSED;
                return;
            }
        }

        private static void Keyboard_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            _keys[(int)e.Key] = ButtonState.UP;
            _hasKeyBeenReleased[(int)e.Key] = true;
        }

        private static void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mousePosition = new Vector2(e.X, e.Y);
            if (_mousebuttons[(int)e.Button] == ButtonState.PRESSED)
            {
                _mousebuttons[(int)e.Button] = ButtonState.DOWN;
                return;
            }
            if (_mousebuttons[(int)e.Button] == ButtonState.NONE)
            {
                _mousebuttons[(int)e.Button] = ButtonState.PRESSED;
                return;
            }
        }

        private static void Mouse_ButtonUp(object sender, MouseButtonEventArgs e)
        {
            _mousePosition = new Vector2(e.X, e.Y);
            _mousebuttons[(int)e.Button] = ButtonState.UP;
            _hasMouseBeenReleased[(int)e.Button] = true;
        }

        private static void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            _mousePosition = new Vector2(e.X, e.Y);
        }

        private static void Mouse_Wheel(object sender, MouseWheelEventArgs args)
        {
            
        }

        public static Vector2 MousePosition
        {
            get
            {
                return _mousePosition;
            }

            set
            {
                _mousePosition = value;
            }
        }
    }
}
