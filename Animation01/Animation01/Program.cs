using System;

namespace XNA2DGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// Точка входа в игру
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}