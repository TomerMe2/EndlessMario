using System;

namespace EndlessMarioRebornGit
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static bool shouldRestart;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            shouldRestart = true;
            while (shouldRestart)
            {
                shouldRestart = false;
                using (var game = new MrioGame())
                    game.Run();
            }
        }

        public static void PrepareForRestart()
        {
            shouldRestart = true;
        }
    }
#endif
}
