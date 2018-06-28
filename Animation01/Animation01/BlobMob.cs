using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNA2DGame
{
    /// <summary>
    /// Данный класс описывает монстров второго типа, которых игрок может встретить на земле.
    /// Объекты этого класса не двигаются с места, пока игрок не коснется их
    /// </summary>
    class BlobMob : Monster
    {
        const float xMaxVelocity = 1.5f;                    
        const float xAcceleration = 0.5f;                   
        const float xDeceleration = 0.3f;                   
        const float jumpInitialVelocity = -3.2f;            

        public static Texture2D[] blobMobTextures = new Texture2D[4];
        const int thisMillisecondsPerFrame = 50;

        Point collisionOffs = new Point(5, 3);
        const int bottomResetPoint = 23;

        const int maxH = 1;
        /// <summary>
        /// При создании экземпляра класса указывается начальная позиция объекта
        /// </summary>
        /// <param name="position"></param>
        public BlobMob(Vector2 position)
            : base(blobMobTextures[2], position, new Point(36, 24), new Point(6, 4),
            thisMillisecondsPerFrame, Vector2.Zero, Color.White, bottomResetPoint, maxH)
        {
            currentAnimation = 2;
            isOnTheGround = true;
            collisionOffset = collisionOffs;
        }

        public override Rectangle boundingBox
        {
            get { return new Rectangle((int)position.X + 5, (int)position.Y + 3, frameSize.X - 10, frameSize.Y - 3); }
        }

        public override Rectangle textureBox
        {
            get { return new Rectangle(((currentFrame.X - 1) * frameSize.X) + 5, ((currentFrame.Y - 1) * frameSize.Y) + 3, frameSize.X - 10, frameSize.Y - 3); }
        }

        public override Rectangle frontBox
        {
            get
            {
                if (isFacingLeft)
                {
                    return new Rectangle((int)position.X + 6, (int)position.Y + 5, 2, bottomResetPoint - 5);
                }
                else
                {
                    return new Rectangle((int)position.X + 28, (int)position.Y + 5, 2, bottomResetPoint - 5);
                }
            }
        }

        public override Rectangle backBox
        {
            get
            {
                if (isFacingLeft)
                {
                    return new Rectangle((int)position.X + 28, (int)position.Y + 5, 2, bottomResetPoint - 5);
                }
                else
                {
                    return new Rectangle((int)position.X + 6, (int)position.Y + 5, 2, bottomResetPoint - 5);
                }
            }
        }

        public override Rectangle bottomBox
        {
            get
            {
                if (isFacingLeft)
                {
                    return new Rectangle((int)position.X + 6, (int)position.Y + bottomResetPoint - 2, 24, 3);
                }
                else
                {
                    return new Rectangle((int)position.X + 6, (int)position.Y + bottomResetPoint - 2, 24, 3);
                }
            }
        }

        public override Rectangle topBox
        {
            get
            {
                if (isFacingLeft)
                {
                    return new Rectangle((int)position.X + 6, (int)position.Y + 4, 24, 3);
                }
                else
                {
                    return new Rectangle((int)position.X + 6, (int)position.Y + 4, 24, 3);
                }
            }
        }

        public override int currentAnimation
        {
            get { return animationState; }
            set
            {
                if (value != animationState)
                {
                    if (value == 0 || value == 1)
                    {
                        sheetSize = new Point(6, 4);
                    }
                    else
                    {
                        sheetSize = new Point(6, 3);
                    }

                    currentFrame = new Point(1, 1);
                    animationState = value;
                    textureImage = blobMobTextures[value];
                }
            }
        }

        protected override void ChangeTexture()
        {
            if (xVel < 0)
            {
                isFacingLeft = true;
            }
            if (xVel > 0)
            {
                isFacingLeft = false;
            }

            if (yVel == 1)
            {
                if (isFacingLeft)
                {
                    currentAnimation = 0;
                }
                else
                {
                    currentAnimation = 1;
                }
            }
            else
            {
                if (isFacingLeft)
                {
                    currentAnimation = 2;
                }
                else
                {
                    currentAnimation = 3;
                }
            }
        }

        protected override void RunAIandUpdateVelocity()
        {
            xVel = MathHelper.Clamp(xVel, -xMaxVelocity, xMaxVelocity);
            if (isLeftOfPlayer)
            {
                isLeftOfPlayer = false;
                if (isOnTheGround)
                {
                    yVel = jumpInitialVelocity;
                    xVel = -1.0f;
                }
            }

            if (isRightOfPlayer)
            {
                isRightOfPlayer = false;
                if (isOnTheGround)
                {
                    yVel = jumpInitialVelocity;
                    xVel = 1.0f;
                }
            }

            if (yVel != 0)
            {
                isOnTheGround = false;
            }

            if (isOnTheGround)
            {
                yVel = 1;
                xVel = 0;
            }
            else
            {
                yVel += gravity;
            }

            if (yVel > terminalVelocity)
            {
                yVel = terminalVelocity;
            }
        }
    }
}