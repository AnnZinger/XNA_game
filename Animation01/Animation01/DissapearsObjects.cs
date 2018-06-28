using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2DGame
{
    /// <summary>
    /// Класс описывает статичные объекты игры
    /// </summary>
    class DissapearsObjects : Sprite
    {

        static public Texture2D objTexture;

        public DissapearsObjects(Vector2 position)
            : this(position, Color.White)
        { }

        public DissapearsObjects(Vector2 position, Color tint)
            : base(objTexture, position, 0.3f, Vector2.Zero, tint)
        { }
    
    }
}