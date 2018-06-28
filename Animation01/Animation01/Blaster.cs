using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNA2DGame
{
    class Blaster : Weapoon
    {
        static public Texture2D blastBullTexture;
        /// <summary>
        /// При создании обекта этого класса необходимо передать значение количества пуль
        /// </summary>
        /// <param name="bull"></param>
        public Blaster(int bull)
        {
            this.countBull = bull;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bullet"></param>Лист объектов, куда будет помещаться пуля
        public void MakeBullet(List<Sprite> bullet, Player p)
        {
            if (p.isFacingLeft)
                bullet.Add(new Sprite(blastBullTexture, new Vector2(p.xPos + 4, p.yPos + 18),
                    0.5f, new Vector2(-8, 0), Color.White));
            else
                bullet.Add(new Sprite(blastBullTexture, new Vector2(p.xPos + 22, p.yPos + 18),
                    0.5f, new Vector2(8, 0), Color.White));
            this.countBull--;
        }
    }
}
