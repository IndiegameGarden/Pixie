// (c) 2010-2014 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt
using System;
using Game1.Core;

namespace Game1
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

