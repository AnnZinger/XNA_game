using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2DGame
{
    /// <summary>
    /// Базовый класс для объектов с анимацией
    /// </summary>
    class AnimatedSprite : Sprite
    {
        protected Point frameSize;
        protected Point sheetSize;
        protected Point currentFrame;
        int timeSinceLastFrame = 0;
        protected int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 33;

        protected bool animatedTexture = true;

        public AnimatedSprite(Texture2D textureImage, Vector2 position, float layerDepth, Point frameSize, Point sheetSize)
            : this(textureImage, position, layerDepth, frameSize, sheetSize, defaultMillisecondsPerFrame, Vector2.Zero, Color.White)
        { }

        public AnimatedSprite(Texture2D textureImage, Vector2 position, float layerDepth, Point frameSize, Point sheetSize,
            int millisecondsPerFrame, Vector2 velocity, Color tint)
            : base(textureImage, position, layerDepth, velocity, tint)
        {
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.currentFrame = new Point(1, 1);
        }

        public override void Update(GameTime gameTime)
        {
            if (animatedTexture)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    timeSinceLastFrame = 0;
                    ++currentFrame.X;
                    if (currentFrame.X > sheetSize.X)
                    {
                        currentFrame.X = 1;
                        ++currentFrame.Y;
                        if (currentFrame.Y > sheetSize.Y)
                        {
                            currentFrame.Y = 1;
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage, position,
                new Rectangle((currentFrame.X - 1) * frameSize.X, (currentFrame.Y - 1) * frameSize.Y, frameSize.X, frameSize.Y),
                tint, 0, Vector2.Zero, 1f, SpriteEffects.None, layerDepth);

            if (showBoundingBoxes)
            {
                DrawBoundingBox(spriteBatch);
            }
        }

        public override Rectangle boundingBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y); }
        }

        public virtual Rectangle textureBox
        {
            get { return new Rectangle((currentFrame.X - 1) * frameSize.X, (currentFrame.Y - 1) * frameSize.Y, frameSize.X, frameSize.Y); }
        }

        public Point CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }
    }
}
