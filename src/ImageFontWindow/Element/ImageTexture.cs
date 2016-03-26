using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFontWindow.Element
{
    /// <summary>
    /// Reference a texture to be drawn on current context
    /// </summary>
    public class ImageTexture
    {
        #region Publics
        /// <summary>
        /// Current X position
        /// </summary>
        public int X;
        /// <summary>
        /// Current Y position
        /// </summary>
        public int Y;
        #endregion
        #region Privates
        /// <summary>
        /// Gets the texture identifier.
        /// </summary>
        /// <value>
        /// The texture identifier.
        /// </value>
        public int TextureId => texId;
        /// <summary>
        /// The texture identifier
        /// </summary>
        private int texId;
        /// <summary>
        /// was zoomed on last draw
        /// </summary>
        private bool wasZoomed;
        /// <summary>
        /// The width
        /// </summary>
        private int width;
        /// <summary>
        /// The height
        /// </summary>
        private int height;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTexture"/> class.
        /// </summary>
        /// <param name="textureId">The texture identifier.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="y">The y position.</param>
        /// <param name="x">The x position.</param>
        public ImageTexture(int textureId, int width, int height, int x = 0, int y = 0)
        {
            this.X = x;
            this.Y = y;

            SetTextureId(textureId, width, height);
        }

        #region Draw
        /// <summary>
        /// Draws this texture.
        /// </summary>summary>
        public void Draw()
        {
            wasZoomed = false;
            Draw(Y, X, height, width);
        }

        /// <summary>
        /// Draws this texture.
        /// </summary>
        /// <param name="y">The y position.</param>
        /// <param name="x">The x position.</param>
        public void Draw(int y, int x)
        {
            wasZoomed = false;
            Draw(y, x, height, width);
        }

        /// <summary>
        /// Draws this texture.
        /// </summary>
        /// <param name="y">The y position.</param>
        /// <param name="x">The x position.</param>
        /// <param name="zoom">The zoom factor.</param>
        public void Draw(int y, int x, double zoom)
        {
            if (Math.Abs(zoom - 1.0) > 0.0)
                wasZoomed = true;

            Draw(y, x, height * zoom, width * zoom);
        }

        /// <summary>
        /// Draws this texture.
        /// </summary>
        /// <param name="y">The y position.</param>
        /// <param name="x">The x position.</param>
        /// <param name="h">The height.</param>
        /// <param name="w">The width.</param>
        private void Draw(int y, int x, double h, double w)
        {
            this.X = x;
            this.Y = y;

            // TODO : upgrade this to OpenGL 3+
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.BindTexture(TextureTarget.Texture2D, texId);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0); GL.Vertex2(x, y + h); // TOP LEFT
            GL.TexCoord2(1, 0); GL.Vertex2(x + w, y + h); // TOP RIGHT
            GL.TexCoord2(1, 1); GL.Vertex2(x + w, y); // BOTTOM RIGHT
            GL.TexCoord2(0, 1); GL.Vertex2(x, y); // BOTTOM LEFT

            GL.End();
        }
        #endregion

        /// <summary>
        /// Sets the texture identifier.
        /// </summary>
        /// <param name="textureId">The texture identifier.</param>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        public void SetTextureId(int textureId, int w, int h)
        {
            this.texId = textureId;
            this.height = h;
            this.width = w;
        }

        /// <summary>
        /// Boundings the box.
        /// </summary>
        /// <exception cref="System.NotImplementedException">just to avoid an annoying resharper warning ^^'</exception>
        private void BoundingBox()
        {
            if (wasZoomed != null) // always true
                throw new NotImplementedException("just to avoid an annoying resharper warning ^^'");
        }
    }
}
