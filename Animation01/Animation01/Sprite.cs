using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2DGame
{
    /// <summary>
    /// Базовый класс для всех объектов, которые присутствуют в игре
    /// </summary>
    class Sprite
    {       
        protected Texture2D textureImage;
        protected Vector2 position;
        protected float layerDepth;

        protected Vector2 vel;
        protected Color tint;

        protected const float terminalVelocity = 5f;                
        protected const float gravity = 0.12f;                       

        static public Texture2D boxTexture;
        static public bool showBoundingBoxes = false;
        private Vector2 position_2;
        private Point point;
        private Point point_2;
        private Vector2 vector2;
        private Color color;

        public Sprite(Texture2D textureImage, Vector2 position, float layerDepth, Vector2 vel, Color tint)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.layerDepth = layerDepth;
            this.vel = vel;
            this.tint = tint;
        }

        public Sprite(Vector2 position_2, Point point, Point point_2, Vector2 vector2, Color color)
        {
            this.position_2 = position_2;
            this.point = point;
            this.point_2 = point_2;
            this.vector2 = vector2;
            this.color = color;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (vel != Vector2.Zero)
            {
                position += vel;
            }
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage, position, null, tint, 0, Vector2.Zero, 1f, SpriteEffects.None, layerDepth);

            if (showBoundingBoxes)
            {
                DrawBoundingBox(spriteBatch);
            }
        }

        public virtual Rectangle boundingBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, textureImage.Width, textureImage.Height); }
        }

        public Texture2D texture
        {
            get { return textureImage; }
            set { textureImage = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float xPos
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float yPos
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Vector2 velocity
        {
            get { return vel; }
            set { vel = value; }
        }

        public float xVel
        {
            get { return vel.X; }
            set { vel.X = value; }
        }

        public float yVel
        {
            get { return vel.Y; }
            set { vel.Y = value; }
        }

        public Color tintOfObj
        {
            get { return tint; }
            set { tint = value; }
        }

        protected void DrawBoundingBox(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boxTexture, boundingBox, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0.3f);
        }

    }
}
