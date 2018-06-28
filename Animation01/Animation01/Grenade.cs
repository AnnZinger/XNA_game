using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNA2DGame
{
    class Grenade : Weapoon
    {
        static public Texture2D launcherBullTexture;
        public int radiusFire { get; set; }
        public int timerMax { get; set; }
        public int timerThis { get; set; }
        /// <summary>
        /// При создании обекта этого класса необходимо передать значение количества пуль, радиус действия и таймер
        /// </summary>
        /// <param name="countb"></param>
        public Grenade(int countb,int radius,int time)
        {
            this.countBull = countb;
            radiusFire = radius;
            timerMax = time;
            timerThis = timerMax;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bullet"></param>Лист объектов, куда будет помещаться пуля
        public void MakeBoom(List<Sprite> bullet, Player p)
        {
            timerThis = timerMax;
                if (p.isFacingLeft)
                    bullet.Add(new Sprite(launcherBullTexture, new Vector2(p.xPos + 4, p.yPos + 18),
                        0.5f, new Vector2(-8, -3), Color.White));
                else
                    bullet.Add(new Sprite(launcherBullTexture, new Vector2(p.xPos + 22, p.yPos + 18),
                        0.5f, new Vector2(8, -3), Color.White));
                this.countBull--;
        }

       
    }
}
