using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFontWindow.Element
{
    /// <summary>
    /// An element representing a font.
    /// </summary>
    public class ImageFont
    {
        #region Publics
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id => texId;
        /// <summary>
        /// Gets the width of the font.
        /// </summary>
        /// <value>
        /// The width of the font.
        /// </value>
        public int FontWidth => fontWidth;
        /// <summary>
        /// Gets the height of the font.
        /// </summary>
        /// <value>
        /// The height of the font.
        /// </value>
        public int FontHeight => fontHeight;
        #endregion
        #region Privates
        /// <summary>
        /// The texture identifier
        /// </summary>
        private readonly int texId;
        /// <summary>
        /// The font width
        /// </summary>
        private readonly int fontWidth;
        /// <summary>
        /// The font height
        /// </summary>
        private readonly int fontHeight;
        /// <summary>
        /// The font file height
        /// </summary>
        private readonly int fontFileHeight;
        /// <summary>
        /// The font file width
        /// </summary>
        private readonly int fontFileWidth;
        /// <summary>
        /// The rows
        /// </summary>
        private int rows;
        /// <summary>
        /// The cols
        /// </summary>
        private int cols;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFont"/> class.
        /// </summary>
        /// <param name="texId">The tex identifier.</param>
        /// <param name="fontWidth">Width of the font.</param>
        /// <param name="fontHeight">Height of the font.</param>
        /// <param name="viewportWidth">Width of the viewport.</param>
        /// <param name="viewportHeight">Height of the viewport.</param>
        public ImageFont(int texId, int fontWidth, int fontHeight, int viewportWidth, int viewportHeight)
        {
            this.texId = texId;
            this.fontWidth = fontWidth;
            this.fontHeight = fontHeight;
            fontFileWidth = fontWidth * 16;
            fontFileHeight = fontHeight * 16;
            cols = viewportWidth / fontWidth;
            rows = viewportHeight / fontHeight;
        }

        /// <summary>
        /// Writes the specified text with default color.
        /// </summary>
        /// <param name="yRow">The y row.</param>
        /// <param name="xCol">The x col.</param>
        /// <param name="text">The text.</param>
        public void Write(double yRow, double xCol, string text)
        {
            Write(yRow, xCol, text, Color4.White, Color4.Black, false);
        }

        /// <summary>
        /// Writes the specified text.
        /// </summary>
        /// <param name="yRow">The y row.</param>
        /// <param name="xCol">The x col.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="bgColor">Color of the bg.</param>
        /// <param name="pixelUnit">if set to <c>true</c> [pixel unit].</param>
        public void Write(double yRow, double xCol, string text, Color4 color, Color4 bgColor, bool pixelUnit = false)
        {
            // TODO : upgrade this to OpenGL 3+
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.BindTexture(TextureTarget.Texture2D, texId);
            GL.Begin(PrimitiveType.Quads);

            foreach (var ch in text)
            {
                WriteCharacter(ch, xCol, yRow, pixelUnit);
                xCol++;
            }

            GL.End();
        }

        /// <summary>
        /// Writes the character.
        /// </summary>
        /// <param name="ch">The ch.</param>
        /// <param name="xCol">The x col.</param>
        /// <param name="yRow">The y row.</param>
        /// <param name="pixelUnit">if set to <c>true</c> [pixel unit].</param>
        private void WriteCharacter(char ch, double xCol, double yRow, bool pixelUnit = false)
        {
            if (xCol < 0 || yRow < 0)
                return;

            if (xCol > cols - 1 || yRow > rows - 1)
                return;

            byte ascii;
            unchecked { ascii = (byte)ch; }

            double rowFont = ascii >> 4;
            double colFont = ascii & 0x0F;

            // flip rows because openGL use another coordinate system (GL.Ortho?)
            yRow = rows - 1 - yRow;

            colFont *= fontWidth;
            rowFont *= fontHeight;

            if (!pixelUnit)
            {
                xCol *= fontWidth;
                yRow *= fontHeight;
            }

            // TODO : upgrade this to OpenGL 3+
            // positions on font sprite
            var left = colFont / fontFileWidth;
            var right = (colFont + fontWidth) / fontFileWidth;
            var top = rowFont / fontFileHeight;
            var bottom = (rowFont + fontHeight) / fontFileHeight;
            // draw
            GL.TexCoord2(left, top); GL.Vertex2(xCol, yRow + fontHeight); // TOP LEFT
            GL.TexCoord2(right, top); GL.Vertex2(xCol + fontWidth, yRow + fontHeight); // TOP RIGHT
            GL.TexCoord2(right, bottom); GL.Vertex2(xCol + fontWidth, yRow); // BOTTOM RIGHT
            GL.TexCoord2(left, bottom); GL.Vertex2(xCol, yRow); // BOTTOM LEFT
        }
    }
}
