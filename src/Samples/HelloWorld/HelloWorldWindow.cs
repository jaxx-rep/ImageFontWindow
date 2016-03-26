using ImageFontWindow;
using OpenTK.Input;
using System.Linq;

namespace HelloWorld
{
    public class HelloWorldWindow : ConsoleWindow
    {
        /// <summary>
        /// Hello World !
        /// </summary>
        public HelloWorldWindow() : base(40 * 16, 72 * 16, "HelloWorld")
        {
            // handle key events
            KeyDown += HelloWorldWindow_KeyDown;
            /* NOTE : All members from OpenTK.GameWindow are bindable
             * http://www.opentk.com/files/doc/class_open_t_k_1_1_game_window.html */

            // init our 'screen' with
            // a text
            Write(20, 20, "Hello World !");
            // a texture (textureId 1 is the texture registred as font :)
            AddTexture(1, 256, 256, 0, 100);
        }

        /// <summary>
        /// Handle key events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelloWorldWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (Keyboard[Key.Escape])
                Exit();
        }

        /// <summary>
        /// Called when the frame is updated.
        /// Put your logic here !
        /// </summary>
        /// <param name="e">Contains information necessary for frame updating.</param>
        /// <remarks>
        /// Subscribe to the <see cref="E:OpenTK.GameWindow.UpdateFrame" /> event instead of overriding this method.
        /// </remarks>
        protected override void OnUpdateFrame(OpenTK.FrameEventArgs e)
        {
            Texts.First().Row = (Texts.First().Row + 1) % rows;
            Textures.First().X = (Textures.First().X + 1) % Width;
        }
    }
}
