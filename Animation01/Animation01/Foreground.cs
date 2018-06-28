using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2DGame
{
    class Foreground : Sprite
    {
        public static List<Rectangle> floorBoxList = new List<Rectangle>();
        public static List<Rectangle> ceilingBoxList = new List<Rectangle>();
        public static List<Rectangle> wallBoxList = new List<Rectangle>();

        public Foreground(Texture2D textureImage, Vector2 position)
            : this(textureImage, position, Color.White)
        { }

        public Foreground(Texture2D textureImage, Vector2 position, Color tint)
            : base(textureImage, position, 0.2f, Vector2.Zero, tint)
        { }

        public override Rectangle boundingBox { get { return Rectangle.Empty; } }    
    }
}