using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNA2DGame
{
    public partial class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        // Метод, описывающий движение камеры по X
        void MoveCamera_X()
        {
            const int endOfMap = 9600;
            // Для следующих значений: L = при повороте влево, R = при повороте вправо
            // Все значения относительно текущего положения камеры 

            // Положение игрока фиксируется в том случае, если игрок движется в лицевом направлении
            const float movingPlayerPosL = 160;
            const float movingPlayerPosR = 120;

            // Положение, при котором камера начнет смотреть в противоположном направлении
            const float reverseFacingPosL = 230;
            const float reverseFacingPosR = 50;

            const float resumeMovementPosL = 50;
            const float resumeMovementPosR = 230;

            const float catchUpSpeed = 15;

            // Положение игрока на экране, или "относительное положение"
            float playerScreenXPos = player.xPos - cameraXPos;

            float offset = 0;

            switch (movementStateX)
            {
                case 0:     
                    if (mustCatchUp)       
                    {
                        offset = catchUpSpeed;
                        if (playerScreenXPos - offset < movingPlayerPosR)
                        {
                            offset = playerScreenXPos - movingPlayerPosR;
                            mustCatchUp = false;                                        
                            movementStateX = 2;                                          
                        }
                    }
                    else if (playerScreenXPos > resumeMovementPosR)
                    {
                        mustCatchUp = true;
                    }
                    else
                    {
                        offset = 0;
                    }
                    break;

                case 1:     
                    if (mustCatchUp)                                                    
                    {
                        offset = -catchUpSpeed;
                        if (playerScreenXPos - offset > movingPlayerPosL)
                        {
                            offset = playerScreenXPos - movingPlayerPosL;
                            mustCatchUp = false;                                       
                            movementStateX = 3;                                         
                        }
                    }
                    else if (playerScreenXPos < resumeMovementPosL)
                    {
                        mustCatchUp = true;
                    }
                    else
                    {
                        offset = 0;
                    }
                    break;

                case 2:    
                    if (mustCatchUp)                                                    
                    {
                        offset = catchUpSpeed;
                        if (playerScreenXPos - offset < movingPlayerPosR)
                        {
                            offset = playerScreenXPos - movingPlayerPosR;
                            mustCatchUp = false;                                     
                        }
                    }
                    else if (playerScreenXPos > movingPlayerPosR)                      
                    {
                        offset = playerScreenXPos - movingPlayerPosR;
                    }
                    else if (playerScreenXPos < reverseFacingPosR)
                    {
                        offset = 0;
                        mustCatchUp = true;
                        movementStateX = 3;
                    }
                    else
                    {
                        offset = 0;
                    }
                    break;

                case 3:     
                    if (mustCatchUp)                                                    
                    {
                        offset = -catchUpSpeed;
                        if (playerScreenXPos - offset > movingPlayerPosL)
                        {
                            offset = playerScreenXPos - movingPlayerPosL;
                            mustCatchUp = false;                                       
                        }
                    }
                    else if (playerScreenXPos < movingPlayerPosL)                     
                    {
                        offset = playerScreenXPos - movingPlayerPosL;
                    }
                    else if (playerScreenXPos > reverseFacingPosL)
                    {
                        offset = 0;
                        mustCatchUp = true;
                        movementStateX = 2;
                    }
                    else
                    {
                        offset = 0;
                    }
                    break;
            }

            cameraXPos += offset;

            if (cameraXPos < 0)                                            
            {
                cameraXPos = 0;                                            
                movementStateX = 0;                                         
                mustCatchUp = false;
            }
            if (cameraXPos > endOfMap)                                     
            {
                cameraXPos = endOfMap;                                     
                movementStateX = 1;                                          
                mustCatchUp = false;
            }
        }

        // Методы, описывающие столкновения
        void CheckForTerrainCollisions()
        {
            // Для игрока
            // Столкновение со стеной
            if (player.xVel != 0)
                foreach (Rectangle r in Foreground.wallBoxList)
                {
                    if (r.Intersects(player.frontBox))
                    {
                        if (player.isFacingLeft)
                        {
                            player.xPos = r.X + r.Width - 10;
                        }
                        else
                        {
                            player.xPos = r.X - 21;
                        }
                        player.xVel = 0;
                    }
                    if (!player.isFacingLeft && player.xVel < 0)
                    {
                        if (r.Intersects(player.backBox))
                        {
                            player.xPos = r.X + r.Width - 9;
                            player.xVel = 0;
                        }
                        if (r.Intersects(player.backOfHeadBox))
                        {
                            player.xPos = r.X + r.Width - 13;
                            player.xVel = 0;
                        }
                    }
                    if (player.isFacingLeft && player.xVel > 0)
                    {
                        if (r.Intersects(player.backBox))
                        {
                            player.xPos = r.X - 22;
                            player.xVel = 0;
                        }
                        if (r.Intersects(player.backOfHeadBox))
                        {
                            player.xPos = r.X - 17;
                            player.xVel = 0;
                        }
                    }
                }

            // Столкновение с полом
            if (player.yVel >= 0)
                foreach (Rectangle r in Foreground.floorBoxList)
                    if (r.Intersects(player.feetBox))
                    {
                        player.yPos = r.Y - 49;
                        player.yVel = 0;
                        player.isOnTheGround = true;
                    }
            //Столкновение с движущейся платформой
            //С верхом
            for (int j = 0; j < motionStepList.Count; j++)
            {
                if (player.yVel >= 0)

                    if (motionStepList[j].topBox.Intersects(player.feetBox))
                    {
                        player.yPos = motionStepList[j].topBox.Y - 49;
                        player.yVel = 0;
                        player.xPos = motionStepList[j].xPos;
                        player.isOnTheGround = true;

                    }

                // С низом
                if (player.yVel < 0)
                    if (motionStepList[j].bottomBox.Intersects(player.headBox))
                    {
                        player.yPos = motionStepList[j].bottomBox.Y - 1;
                        player.yVel = 0;
                    }
            }

            // Столкновение с потолком
            if (player.yVel < 0)
                foreach (Rectangle r in Foreground.ceilingBoxList)
                {
                    if (r.Intersects(player.headBox))
                    {
                        player.yPos = r.Y - 1;
                        player.yVel = 0;
                    }
                    if (r.Intersects(player.shouldersBox))
                    {
                        player.yPos = r.Y - 9;
                        player.yVel = 0;
                    }
                }

            //Описание столкновений
            // Для всех монстров
            foreach (Monster m in monsterBullList)
            {
                //Столкновение со стеной
                if (m.xVel != 0)
                    foreach (Rectangle r in Foreground.wallBoxList)
                    {
                        if (r.Intersects(m.frontBox))
                        {
                            if (m.isFacingLeft)
                            {
                                m.xPos = r.X + r.Width - 10;
                            }
                            else
                            {
                                m.xPos = r.X - 21;
                            }
                            m.xVel *= -1;
                        }
                        if (!m.isFacingLeft && m.xVel < 0)
                        {
                            if (r.Intersects(m.backBox))
                            {
                                m.xPos = r.X + r.Width - 9;
                                m.xVel = 0;
                            }
                        }
                        if (m.isFacingLeft && m.xVel > 0)
                        {
                            if (r.Intersects(m.backBox))
                            {
                                m.xPos = r.X - 22;
                                m.xVel = 0;
                            }
                        }
                    }

                // Столкновение с полом
                if (m.yVel >= 0)
                    foreach (Rectangle r in Foreground.floorBoxList)
                        if (r.Intersects(m.bottomBox))
                        {
                            m.yPos = r.Y - m.bottomResetPoint;
                            m.yVel = 0;
                            m.isOnTheGround = true;
                        }
            }
        }
    }
}
