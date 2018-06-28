using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2DGame
{
    class BoxWithObject : Sprite
    {
        static public Texture2D boxImage;
        public BoxWithObject(Vector2 position)
            : this(position, Color.White)
        { }

        public BoxWithObject(Vector2 position, Color tint)
            : base(boxImage, position, 0.3f, Vector2.Zero, tint)
        {
            Foreground.wallBoxList.Add(new Rectangle((int)xPos - 7, (int)yPos + 0, 17, 1));
            Foreground.wallBoxList.Add(new Rectangle((int)xPos + 46, (int)yPos + 10, 20, 1));
            Foreground.floorBoxList.Add(new Rectangle((int)xPos + 0, (int)yPos + 30, 53, 1));
            // Левая вкрхняя часть
            Foreground.floorBoxList.Add(new Rectangle((int)xPos + 0, (int)yPos + 0, 10, 1)); 
            // Правая верхняя часть
            Foreground.floorBoxList.Add(new Rectangle((int)xPos + 46, (int)yPos + 0, 7, 1)); 
            Foreground.ceilingBoxList.Add(new Rectangle((int)xPos + 0, (int)yPos + 40, 53, 1));
        }     
    }
}

