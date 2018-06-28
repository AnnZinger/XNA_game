using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2DGame
{
    /// <summary>
    /// Класс ддвижущихся платформ
    /// </summary>
    class MotionStep : Sprite
    {
        static public Texture2D stepBoxImage;
        float speedMotion = 0.02f;

        public int xLeft { get; set; }
        public int xRight { get; set; }

        public MotionStep(Vector2 position)
            : this(position, Color.White)
        { }

        public MotionStep(Vector2 position, Color tint)
            : base(stepBoxImage, position, 0.3f, Vector2.Zero, tint)
        { }

        public Rectangle feetBox
        {
            get
            {
                return new Rectangle((int)xPos + 1, (int)yPos + 1, 27, 3);
            }
        }
        public override void Update(GameTime gameTime)
        {
            this.xPos += speedMotion;
            if (this.xPos > xRight || this.xPos < xLeft)
            {            
                speedMotion *= -1;
            }
            base.Update(gameTime);
        }

        public Rectangle frontBox
        {
            get
            {
                return new Rectangle((int)xPos + 1, (int)yPos + 2, 2, 10);
            }
        }

        public Rectangle backBox
        {
            get
            {
                return new Rectangle((int)xPos + 26, (int)yPos + 2, 2, 10);
            }
        }

        public Rectangle bottomBox
        {
            get
            {
                return new Rectangle((int)xPos + 1, (int)yPos + 10, 27, 3);
            }
        }

        public Rectangle topBox
        {
            get
            {
                return new Rectangle((int)xPos + 1, (int)yPos + 1, 27, 3);
            }
        }    
    }
}
