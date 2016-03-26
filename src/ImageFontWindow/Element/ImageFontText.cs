using OpenTK.Graphics;

namespace ImageFontWindow.Element
{
    /// <summary>
    /// An element representing a text
    /// </summary>
    public class ImageFontText
    {
        /// <summary>
        /// The text
        /// </summary>
        public string Text;
        /// <summary>
        /// The column position
        /// </summary>
        public int Column;
        /// <summary>
        /// The row position
        /// </summary>
        public int Row;
        /// <summary>
        /// The X position in pixels
        /// </summary>
        public int X;
        /// <summary>
        /// The X position in pixels
        /// </summary>
        public int Y;
        /// <summary>
        /// The font color
        /// </summary>
        public Color4 Color;
        /// <summary>
        /// The font background color
        /// </summary>
        public Color4 BgColor;
    }
}
