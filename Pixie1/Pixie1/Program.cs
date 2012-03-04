using System;

namespace Pixie1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PixieGame game = new PixieGame())
            {
                game.Run();
            }
        }
    }
#endif
}

