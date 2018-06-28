using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2DGame
{
    /// <summary>
    /// Данный класс описывает такой объект игры как монстр
    /// </summary>
    abstract class Monster : AnimatedSprite
    {
        bool isFacingL = true;
        protected int animationState = 50;
        int hitC = 0;

        public int hitCount { get { return hitC; } set { hitC = value; } }

        public bool isOnTheGround { get; set; }
        public bool isHit { get; set; }
        public int hitFlashCounter { get; set; }
        public Point collisionOffset { get; protected set; }
        public int bottomResetPoint { get; protected set; }

        // Свойства здоровья
        public int maxHP { get; protected set; }
        public int currentHP { get; set; }
        //Положение относительно игрока
        bool isLeftOfP= false;
        bool isRightOfP = false;

        public bool isFacingLeft { get { return isFacingL; } set { isFacingL = value; } }
        public bool isLeftOfPlayer { get { return isLeftOfP; } set { isLeftOfP = value; } }
        public bool isRightOfPlayer { get { return isRightOfP; } set { isRightOfP = value; } }

        public Monster(Texture2D textureImage, Vector2 position, Point frameSize, Point sheetSize, int millisecondsPerFrame,
            Vector2 velocity, Color tint, int bottomResetP, int maxHp)
            : base(textureImage, position, 0.5f, frameSize, sheetSize, millisecondsPerFrame, velocity, tint)
        {
            bottomResetPoint = bottomResetP;
            maxHP = maxHp;
            currentHP = maxHP;
            isHit = false;
        }

        public override void Update(GameTime gameTime)
        {
            RunAIandUpdateVelocity();
            ChangeTexture();
            FlashIfHit();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public abstract Rectangle frontBox { get; }
        public abstract Rectangle backBox { get; }
        public abstract Rectangle bottomBox { get; }
        public abstract Rectangle topBox { get; }

        public abstract int currentAnimation { get; set; }
        protected abstract void ChangeTexture();
        protected abstract void RunAIandUpdateVelocity();

        /// <summary>
        /// С помощью этого метода текстура монстра подсвечивается зеленым цветом, когда ему наносится урон
        /// </summary>
        void FlashIfHit()
        {
            if (isHit)
            {
                ++hitFlashCounter;
                if (hitFlashCounter > 0 && hitFlashCounter < 5)
                    tintOfObj = Color.LightGreen;
                else
                    tintOfObj = Color.White;
                if (hitFlashCounter > 7)
                {
                    hitFlashCounter = 0;
                    isHit = false;
                }
            }
        }

    }
}
