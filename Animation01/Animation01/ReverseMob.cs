using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2DGame
{
    /// <summary>
    /// Данный класс описывает монстров первого типа, которых игрок может встретить на земле.
    /// Объекты этого класса могут двигаться по игровой карте
    /// </summary>
    class ReverseMob : Monster
    {
        // Поля, связанные с движением
        const float xMaxVelocity = 0.9f;                    
        const float xAcceleration = 0.2f;                   
        const float xDeceleration = 0.3f;                   
        const float jumpInitialVelocity = -3.5f;            

        public static Texture2D[] reverseMobTextures = new Texture2D[4];
        const int thisMillisecondsPerFrame = 50;

        Point collisionOffs = new Point(7, 4);
        const int bottomResetPoint = 47;

        const int maxH = 3;
        /// <summary>
        /// При создании экземпляра класса указывается начальная позиция объекта
        /// </summary>
        /// <param name="position"></param>
        public ReverseMob(Vector2 position)
            : base(reverseMobTextures[2], position, new Point(30, 48), new Point(6, 4),
            thisMillisecondsPerFrame, Vector2.Zero, Color.White, bottomResetPoint, maxH)
        {
            currentAnimation = 2;
            isOnTheGround = true;
            xVel = -xMaxVelocity;
            collisionOffset = collisionOffs;
        }

        public override Rectangle boundingBox
        {
            get { return new Rectangle((int)position.X + 7, (int)position.Y + 4, frameSize.X - 13, frameSize.Y - 4); }
        }

        public override Rectangle textureBox
        {
            get { return new Rectangle(((currentFrame.X - 1) * frameSize.X) + 7, ((currentFrame.Y - 1) * frameSize.Y) + 4, frameSize.X - 13, frameSize.Y - 4); }
        }

        public override Rectangle frontBox
        {
            get
            {
                if (isFacingLeft)
                {
                    return new Rectangle((int)position.X + 8, (int)position.Y + 2, 2, bottomResetPoint - 2);
                }
                else
                {
                    return new Rectangle((int)position.X + 21, (int)position.Y + 2, 2, bottomResetPoint - 2);
                }
            }
        }
        public override Rectangle backBox
        {
            get
            {
                if (isFacingLeft)
                {
                    return new Rectangle((int)position.X + 22, (int)position.Y + 2, 2, bottomResetPoint - 2);
                }
                else
                {
                    return new Rectangle((int)position.X + 7, (int)position.Y + 2, 2, bottomResetPoint - 2);
                }
            }
        }
        public override Rectangle bottomBox
        {
            get
            {
                if (isFacingLeft)
                {
                    return new Rectangle((int)position.X + 8, (int)position.Y + bottomResetPoint - 2, 16, 3);
                }
                else
                {
                    return new Rectangle((int)position.X + 7, (int)position.Y + bottomResetPoint - 2, 16, 3);
                }
            }
        }
        public override Rectangle topBox
        {
            get
            {
                if (isFacingLeft)
                {
                    return new Rectangle((int)position.X + 8, (int)position.Y + 1, 16, 2);
                }
                else
                {
                    return new Rectangle((int)position.X + 7, (int)position.Y + 1, 16, 2);
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
                        animatedTexture = false;
                        currentFrame = new Point(1, 1);
                    }
                    else
                    {
                        animatedTexture = true;
                    }
                    animationState = value;
                    textureImage = reverseMobTextures[value];
                }
            }
        }
        protected override void ChangeTexture()
        {
            if (xVel < 0)
                isFacingLeft = true;
            if (xVel > 0)
                isFacingLeft = false;

            if (xVel == 0)
            {
                if (isFacingLeft)
                    currentAnimation = 0;
                else
                    currentAnimation = 1;
            }
            else if (xVel < 0)
                currentAnimation = 2;
            else if (xVel > 0)
                currentAnimation = 3;
        }
        protected override void RunAIandUpdateVelocity()
        {

            xVel = MathHelper.Clamp(xVel, -xMaxVelocity, xMaxVelocity);

            if (yVel != 0)
                isOnTheGround = false;

            if (isOnTheGround)       
                yVel = 1;                                       
            else
                yVel += gravity;

            if (yVel > terminalVelocity)
                yVel = terminalVelocity;
        }
    }
}
