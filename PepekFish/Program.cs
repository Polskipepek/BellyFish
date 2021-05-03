using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Gameplay;
using System;
using System.Windows.Forms;

namespace BellyFish {
    static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main () {
            Application.SetHighDpiMode (HighDpiMode.SystemAware);
            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault (false);
            // Application.Run(new Form1());
            CheckerboardDisplay checkerboardDisplay = new ();
            Application.Run (checkerboardDisplay);
        }
    }
}
