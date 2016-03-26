using ImageFontWindow.Element;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ImageFontWindow
{
    /// <summary>
    /// OpenGL frame window, intended to be developper friendly \0/
    /// </summary>
    /// <seealso cref="OpenTK.GameWindow" />
    public abstract partial class ConsoleWindow : GameWindow
    {
        #region Privates
        /// <summary>
        /// The current assembly
        /// </summary>
        protected static readonly Assembly embedded = Assembly.GetExecutingAssembly();
        /// <summary>
        /// Computed rows
        /// </summary>
        protected int rows;
        /// <summary>
        /// Computed cols
        /// </summary>
        protected int cols;
        /// <summary>
        /// The font
        /// </summary>
        protected ImageFont font; // TODO : implement more possibles fonts
        /// <summary>
        /// The font height
        /// </summary>
        protected int fontHeight;
        /// <summary>
        /// The font width
        /// </summary>
        protected int fontWidth;
        /// <summary>
        /// The original title
        /// </summary>
        protected string originalTitle;
        #endregion
        #region Publics
        /// <summary>
        /// The texts
        /// </summary>
        public List<ImageFontText> Texts;
        /// <summary>
        /// The textures
        /// </summary>
        public List<ImageTexture> Textures;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFontWindow"/> class.
        /// </summary>
        /// <param name="viewportHeight">Height of the viewport.</param>
        /// <param name="viewportWidth">Width of the viewport.</param>
        /// <param name="title">The title.</param>
        /// <param name="fontHeight">Height of the font.</param>
        /// <param name="fontWidth">Width of the font.</param>
        protected ConsoleWindow(int viewportHeight, int viewportWidth, string title, int fontHeight = 16, int fontWidth = 16) : base (viewportWidth, viewportHeight, GraphicsMode.Default, title)
        {
            this.fontHeight = fontHeight;
            this.fontWidth = fontWidth;

            originalTitle = title;
            rows = viewportHeight / fontHeight;
            cols = viewportWidth / fontWidth;

            Texts = new List<ImageFontText>();
            Textures = new List<ImageTexture>();
        }

        /// <summary>
        /// Called after an OpenGL context has been established, but before entering the main loop.
        /// </summary>
        /// <param name="e">Not used.</param>
        protected override void OnLoad(EventArgs e)
        {
            Libs.TexUtil.InitTexturing();
            font = new ImageFont(Libs.TexUtil.CreateTextureFromBitmap(new Bitmap(embedded.GetManifestResourceStream(@"ImageFontWindow.Resources.Font.Bisasam_16x16.png"))), fontWidth, fontHeight, Width, Height);
            OnResize(null);

            GL.ClearColor(Color.Black);
        }

        /// <summary>
        /// Called when the frame is updated.
        /// Put your logic here !
        /// </summary>
        /// <param name="e">Contains information necessary for frame updating.</param>
        /// <remarks>
        /// Subscribe to the <see cref="E:OpenTK.GameWindow.UpdateFrame" /> event instead of overriding this method.
        /// </remarks>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[Key.Escape])
                Exit();
        }

        /// <summary>
        /// Called when this window is resized.
        /// </summary>
        /// <param name="e">Not used.</param>
        /// <remarks>
        /// You will typically wish to update your viewport whenever
        /// the window is resized. See the
        /// <see cref="M:OpenTK.Graphics.OpenGL.GL.Viewport(System.Int32,System.Int32,System.Int32,System.Int32)" /> method.
        /// </remarks>
        protected override void OnResize(EventArgs e)
        {
            // TODO : upgrade this to OpenGL 3+
            GL.Viewport(new Size(Width, Height));
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            // TODO : there is probably a dumb solution for the flipped rows with GL.Ortho => RTFM !!!
            GL.Ortho(0, Width, 0, Height, -1, 1);

            // TODO : iterate trough all fonts to update rows/cols
        }

        /// <summary>
        /// Called when the frame is rendered.
        /// </summary>
        /// <param name="e">Contains information necessary for frame rendering.</param>
        /// <remarks>
        /// Subscribe to the <see cref="E:OpenTK.GameWindow.RenderFrame" /> event instead of overriding this method.
        /// </remarks>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // TODO : implement a layer concept
            foreach (var text in Texts)
            {
                font.Write(text.Row, text.Column, text.Text, text.Color, text.BgColor);
            }

            // textures will always overwrite text as now
            foreach (var tex in Textures)
            {
                tex.Draw();
            }

            SwapBuffers();
        }

        /// <summary>
        /// Writes the specified text with default colors.
        /// </summary>
        /// <param name="ypos">The ypos.</param>
        /// <param name="xpos">The xpos.</param>
        /// <param name="text">The text.</param>
        public void Write(int ypos, int xpos, string text)
        {
            Write(ypos, xpos, text, Color4.White, Color4.Black);
        }

        /// <summary>
        /// Writes the specified text.
        /// </summary>
        /// <param name="ypos">The ypos.</param>
        /// <param name="xpos">The xpos.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="bgColor">Color of the bg.</param>
        public void Write(int ypos, int xpos, string text, Color4 color, Color4 bgColor)
        {
            // TODO : implement more possibles fonts
            var current = Texts.FirstOrDefault(t => t.Row == ypos && t.Column == xpos);

            if (null == current)
            {
                current = new ImageFontText();
                Texts.Add(current);
            }

            current.Row = ypos;
            current.Column = xpos;
            current.Text = text;
            current.Color = color;
            current.BgColor = bgColor;
        }

        /// <summary>
        /// Adds the texture.
        /// </summary>
        /// <param name="textureId">The texture identifier.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public ImageTexture AddTexture(int textureId, int width, int height, int x = 0, int y = 0)
        {
            var tex = new ImageTexture(textureId, width, height, x, y);
            Textures.Add(tex);
            return tex;
        }
    }
}
