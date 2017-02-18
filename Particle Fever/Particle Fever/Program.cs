using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.DirectInput;

namespace Particle_Fever
{
    class Program
    {
        public static ParticleGame game;
        static void Main(string[] args)
        {
            Input input = new Input();

            /*game = new ParticleGame();
            game.createScreen(1280, 720, "Particle Fever");

            game.initialize();

            Timer timer = new Timer();

            while(game.isRunning)
            {
                timer.start();

                Input.poll();

                game.update(GameTime.deltaTime);
                game.render();

                if (Input.isPressed(Key.Escape))
                    game.isRunning = false;

                timer.stop();
                GameTime.deltaTime = timer.duration() / Constants.toSeconds; // move somewhere else
            }

            game.unload();*/

            using (ParticleGame game = new ParticleGame(1280, 720, "Particle Fever"))
            {
                game.Run(60);
            }
        }
    }
}
