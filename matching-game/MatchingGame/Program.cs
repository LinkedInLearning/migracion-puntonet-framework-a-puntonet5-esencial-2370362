using MatchingGame.Services;

using System;
using System.Windows.Forms;

namespace MatchingGame
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var soundService = new SoundService();
            Application.Run(new MainForm(soundService));
            soundService.Dispose();
        }
    }
}
