using System;
using Un4seen.Bass;

namespace WarshipGirl
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BassNet.Registration("28256042@qq.com", "2X123012150022");
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
