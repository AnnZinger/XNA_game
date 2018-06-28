using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2DGame
{
    class TestStepBox : Sprite
    {
        static public Texture2D stepBoxImage;

        public TestStepBox(Vector2 position)
            : this(position, Color.White)
        { }

        public TestStepBox(Vector2 position, Color tint)
            : base(stepBoxImage, position, 0.3f, Vector2.Zero, tint)
        {
            Foreground.wallBoxList.Add(new Rectangle((int)xPos + 1, (int)yPos + 2, 2, 10));
            Foreground.wallBoxList.Add(new Rectangle((int)xPos + 26, (int)yPos + 2, 2, 10));
            Foreground.floorBoxList.Add(new Rectangle((int)xPos + 1, (int)yPos + 1, 27, 3));
            Foreground.ceilingBoxList.Add(new Rectangle((int)xPos + 1, (int)yPos + 10, 27, 3));
        }
    }
}
