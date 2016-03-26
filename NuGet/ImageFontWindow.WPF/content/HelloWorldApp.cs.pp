using OpenTK.Input;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ImageFontWindow;

namespace $rootnamespace$
{
    /// <summary>
    /// Hello World sample with a WPF host
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The rendering window
        /// </summary>
        ConsoleWindow window;

        /// <summary>
        /// Handles the Startup event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="StartupEventArgs"/> instance containing the event data.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // instanciate the window
            window = new HelloWorldWindow();
            // bind the closing event
            window.Closing += new EventHandler<CancelEventArgs>(OnGameWindowClose);
            // run at 60 fps
            window.Run(60);
        }

        /// <summary>
        /// Handles the Exit event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExitEventArgs"/> instance containing the event data.</param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            window.Close();
        }

        /// <summary>
        /// Called when [game window close].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private static void OnGameWindowClose(object sender, CancelEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
