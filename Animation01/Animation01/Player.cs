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
    /// Данный класс предназначен для описания игрока:его анимации, скорости передвижения, 
    /// различных состояний, для обработки входных данных, которые преводят к изменению состояния игрока
    /// </summary>
    class Player : AnimatedSprite
    {
        KeyboardState oldKeyboardState;
        GamePadState oldGamepadState;       
        /// Поля для перемещения(прыжок,бег)
        const float xMaxVelocity = 2.2f;                    
        const float xAcceleration = 0.3f;                   
        const float xDeceleration = 0.3f;                  
        const float jumpInitialVelocity = -1.5f;            
        const float jumpCurveFactor = 1.5f;           
        const float minJumpVelFrames = 3;       
        const float maxJumpVelFrames = 15;
        int jumpCounter = 1;                       
        bool isJumping = false;

        /// Поля и свойства для анимации
        public static Texture2D[] playerTextures = new Texture2D[12];
        public bool isFacingLeft { get; set; }
        int animationState;                                 
        Point hoverFrame = new Point(4, 2);              
        const int jumpAnimationSpeed = 66;                  
        const float hoverVelocity = 0.30f;                  
        public bool isOnTheGround { get; set; }

        ///Поля и свойства значений здоровья
        const int maxHP = 100;
        public int currentHP { get; set; }
        public bool isInvulnerable { get; set; }
        public int invulnCounter { get; set; }

        public bool pulledTheTrigger { get; set; }
        public bool isBlaster { get; set; }
        public bool isBoomerang { get; set; }
        public bool isLauncher { get; set; }
    
        int water = 0;
        int food = 0;
        int oil = 0;
         
        public int waterCount
        { get { return water; } set { water = value; } }
        public int foodCount
        { get { return food; } set { food = value; } }
        public int oilCount
        { get { return oil; } set { oil = value; } }
         /// <summary>
         /// Конструктор
         /// </summary>
         /// <param name="position"></param>Начальная позиция игрока
         /// <param name="frameSize"></param>Размер кадра
         /// <param name="sheetSize"></param>Формат листа спрайта
        public Player(Vector2 position, Point frameSize, Point sheetSize)
            : base(playerTextures[1], position, 0.6f, frameSize, sheetSize, 50, Vector2.Zero, Color.White)
        {
            currentAnimation = 1;
            isOnTheGround = true;
            isFacingLeft = false;
            currentHP = maxHP;
            isInvulnerable = false;
            pulledTheTrigger = false;
            isBlaster = true;
            isBoomerang = false;
            isLauncher = false;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInputAndUpdateVelocity();
            ChangeTexture();

            base.Update(gameTime);  
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void PlayerIsHit()
        {
            if (!isInvulnerable) 
                currentHP -= 5; 
            isInvulnerable = true;
        }

        /// <summary>
        /// Метод используется, когда игроку наносится урон и его нужно подсветить красным цветом
        /// </summary>
        public void FlashPlayerAndUpdateVulnerability()
        {
            if (isInvulnerable)
            {
                ++invulnCounter;
                if ((invulnCounter > 0 && invulnCounter < 4) || (invulnCounter > 7 && invulnCounter < 11))
                    tintOfObj = Color.Red;
                else
                    tintOfObj = Color.White;
                if (invulnCounter > 30)
                {
                    invulnCounter = 0;
                    isInvulnerable = false;
                }
            }
        }
        /// <summary>
        /// Свойства, описывающие ограничительные рамки для игрока
        /// </summary>
        public Rectangle frontBox
        {
            get
            {
                if (isFacingLeft)
                    return new Rectangle((int)position.X + 9, (int)position.Y + 4, 2, 45);
                else
                    return new Rectangle((int)position.X + 20, (int)position.Y + 4, 2, 45);
            }
        }
        public Rectangle backBox
        {
            get
            {
                if (isFacingLeft)
                    return new Rectangle((int)position.X + 22, (int)position.Y + 14, 2, 35);
                else
                    return new Rectangle((int)position.X + 7, (int)position.Y + 14, 2, 35);
            }
        }
        public Rectangle backOfHeadBox
        {
            get
            {
                if (isFacingLeft)
                    return new Rectangle((int)position.X + 18, (int)position.Y + 4, 2, 9);
                else
                    return new Rectangle((int)position.X + 11, (int)position.Y + 4, 2, 9);
            }
        }      
        public Rectangle feetBox
        {
            get
            {
                if (isFacingLeft)
                    return new Rectangle((int)position.X + 11, (int)position.Y + 47, 12, 3);
                else
                    return new Rectangle((int)position.X + 8, (int)position.Y + 47, 12, 3);
            }
        }
        public Rectangle headBox
        {
            get
            {
                if (isFacingLeft)
                    return new Rectangle((int)position.X + 9, (int)position.Y + 4, 10, 2);
                else
                    return new Rectangle((int)position.X + 12, (int)position.Y + 4, 10, 2);
            }
        }
        public Rectangle shouldersBox
        {
            get
            {
                if (isFacingLeft)
                    return new Rectangle((int)position.X + 19, (int)position.Y + 13, 4, 2);
                else
                    return new Rectangle((int)position.X + 8, (int)position.Y + 13, 4, 2);
            }
        }

        public int currentAnimation
        {
            get { return animationState; }
            set
            {
                if (value != animationState)
                {
                    if(isBlaster||isLauncher)
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

                        if (value == 2 || value == 3)
                        {
                            if (animationState == 4 || animationState == 5)
                            {
                                currentFrame = new Point(1, 1);
                            }
                        }

                    if (value == 4 || value == 5)
                    {
                        sheetSize = new Point(6, 3);
                        millisecondsPerFrame = jumpAnimationSpeed;
                        if (animationState == 2 || animationState == 3)
                        {
                            currentFrame = new Point(1, 1);
                        }
                    }
                    else
                    {
                        sheetSize = new Point(6, 4);
                        millisecondsPerFrame = 50;
                    }
                    animationState = value;
                    textureImage = playerTextures[value];
                }
                    if (isBoomerang)
                    {
                        if (value == 6 || value == 7)
                        {
                            animatedTexture = false;
                            currentFrame = new Point(1, 1);
                        }
                        else
                        {
                            animatedTexture = true;
                        }

                        if (value == 8 || value == 9)
                        {
                            if (animationState == 4 || animationState == 5)
                            {
                                currentFrame = new Point(1, 1);
                            }
                        }

                        if (value == 10 || value == 11)
                        {
                            sheetSize = new Point(6, 3);
                            millisecondsPerFrame = jumpAnimationSpeed;
                            if (animationState == 2 || animationState == 3)
                            {
                                currentFrame = new Point(1, 1);
                            }
                        }
                        else
                        {
                            sheetSize = new Point(6, 4);
                            millisecondsPerFrame = 50;
                        }
                        animationState = value;
                        textureImage = playerTextures[value];
                    }
                }
            }
        }

        /// <summary>
        /// Метод, отвечающий за смену текстур в зависимости от состояния игрока
        /// </summary>
        void ChangeTexture()
        {
            if (xVel < 0)
            {
                isFacingLeft = true;
            }
            if (xVel > 0)
            {
                isFacingLeft = false;
            }
            if (!isOnTheGround)
            {
                if (isBlaster||isLauncher)
                {
                    if (isFacingLeft)
                    {
                        currentAnimation = 4;
                    }
                    if (!isFacingLeft)
                    {
                        currentAnimation = 5;
                    }

                    if (Math.Abs(yVel) < hoverVelocity)
                    {
                        currentFrame = hoverFrame;
                        animatedTexture = false;
                    }
                    else if (yVel == 1 + gravity)
                    {
                        currentFrame = new Point(1, 3);
                    }
                    else if (currentFrame == new Point(6, 3))
                    {
                        animatedTexture = false;
                    }
                    else
                    {
                        animatedTexture = true;
                    }
                }
                if (isBoomerang)
                {
                    if (isFacingLeft)
                    {
                        currentAnimation = 10;
                    }
                    if (!isFacingLeft)
                    {
                        currentAnimation = 11;
                    }
                    if (Math.Abs(yVel) < hoverVelocity)
                    {
                        currentFrame = hoverFrame;
                        animatedTexture = false;
                    }
                    else if (yVel == 1 + gravity)
                    {
                        currentFrame = new Point(1, 3);
                    }
                    else if (currentFrame == new Point(6, 3))
                    {
                        animatedTexture = false;
                    }
                    else
                    {
                        animatedTexture = true;
                    }
                }
            }
            else if (xVel == 0)
            {
                if (isBlaster||isLauncher)
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
                if (isBoomerang)
                {
                    if (isFacingLeft)
                    {
                        currentAnimation = 6;
                    }
                    else
                    {
                        currentAnimation = 7;
                    }
                }
            }
            else if (xVel < 0)
            {
                if (isBlaster || isLauncher)
                {
                    currentAnimation = 2;
                }
                if (isBoomerang)
                {
                    currentAnimation = 8;
                }
            }
            else if (xVel > 0)
            {
                if (isBlaster || isLauncher)
                {
                    currentAnimation = 3;
                }
                if (isBoomerang)
                {
                    currentAnimation = 9;
                }
            }
        }
        /// <summary>
        /// Метод для обработки входных данных с клавиатуры
        /// </summary>
        void HandleInputAndUpdateVelocity()
        {
            GamePadState newGamepadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState newKeyboardState = Keyboard.GetState();

            float inputVel = 0;
            if (newKeyboardState.IsKeyDown(Keys.Left) || newKeyboardState.IsKeyDown(Keys.A))
                inputVel = -xAcceleration;
            if (newKeyboardState.IsKeyDown(Keys.Right) || newKeyboardState.IsKeyDown(Keys.D))
                inputVel = xAcceleration;
            if (newGamepadState.ThumbSticks.Left.X < -0.05)
                inputVel = -xAcceleration;
            if (newGamepadState.ThumbSticks.Left.X > 0.05)
                inputVel = xAcceleration;

            if (inputVel == 0)
            {
                if (xVel > 0)
                {
                    xVel -= xDeceleration;
                    if (xVel < 0)
                    {
                        xVel = 0;
                    }
                }
                if (xVel < 0)
                {
                    xVel += xDeceleration;
                    if (xVel > 0)
                    {
                        xVel = 0;
                    }
                }
                if (Math.Abs(xVel) < 0.08f)
                {
                    xVel = 0;
                }
            }
            else
            {
                xVel += inputVel;
            }

            if ((newKeyboardState.IsKeyDown(Keys.LeftShift) || newGamepadState.Triggers.Right > 0.05f)
                && isOnTheGround && inputVel != 0)
            {
                xVel = MathHelper.Clamp(xVel, 1.75f * -xMaxVelocity, 1.75f * xMaxVelocity);
                millisecondsPerFrame = 30;
            }
            else
            {
                xVel = MathHelper.Clamp(xVel, -xMaxVelocity, xMaxVelocity);
                millisecondsPerFrame = 50;
            }
            if ((newKeyboardState.IsKeyUp(Keys.Space) && newGamepadState.Buttons.A == ButtonState.Released && jumpCounter > minJumpVelFrames)
                || jumpCounter > maxJumpVelFrames)
            {
                isJumping = false;
                jumpCounter = 1;
            }
            else if (isJumping)
            {
                yVel += jumpInitialVelocity / (jumpCurveFactor * jumpCounter);
                ++jumpCounter;
            }
            if ((newKeyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space)) ||
                (newGamepadState.Buttons.A == ButtonState.Pressed && oldGamepadState.Buttons.A == ButtonState.Released))
                if (isOnTheGround)
                {
                    yVel = jumpInitialVelocity;
                    isJumping = true;
                }
            if (yVel != 0)
            {
                isOnTheGround = false;
            }
            if (isOnTheGround)
            {
                yVel = 1;
            }
            else
            {
                yVel += gravity;
            }

            if (((newKeyboardState.IsKeyDown(Keys.Q) && oldKeyboardState.IsKeyUp(Keys.Q)) ||
                (newGamepadState.Buttons.X == ButtonState.Pressed && oldGamepadState.Buttons.X == ButtonState.Released)))
            {
                if (isBlaster)
                {
                    isLauncher = true;
                    isBlaster = false;
                  //  isBoomerang = true;
                }
                //if (isBoomerang)
                else
                {
                    isLauncher = false;
                    isBlaster = true;
                   // isBoomerang = false;
                }
               /* if (isLauncher)
                {
                    isLauncher = false;
                    isBlaster = true;
                    isBoomerang = false;
                }*/
            }
            if (pulledTheTrigger == true)
                pulledTheTrigger = false;
            if (((newKeyboardState.IsKeyDown(Keys.RightControl) && oldKeyboardState.IsKeyUp(Keys.RightControl)) ||
                (newGamepadState.Buttons.X == ButtonState.Pressed && oldGamepadState.Buttons.X == ButtonState.Released)))
            {
                pulledTheTrigger = true;
            }
            oldGamepadState = newGamepadState;
            oldKeyboardState = newKeyboardState;
        }

    }
}
