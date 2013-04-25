using System;

namespace Möbius
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Tessallation game = new Tessallation())
            {
                game.Run();
            }
        }
    }
#endif
}

