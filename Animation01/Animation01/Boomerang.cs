using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNA2DGame
{
    class Boomerang : Weapoon
    {
        static public Texture2D boomerangTexture;
        /// <summary>
        /// При создании обекта этого класса необходимо передать значение количества пуль
        /// </summary>
        /// <param name="countb"></param>
        public Boomerang(int countb)
        {
            this.countBull = countb;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bullet"></param>Лист объектов, куда будет помещаться пуля
        public void MakeBoom(List<Sprite> bullet, Player p)
        {
            if (p.isFacingLeft)
                bullet.Add(new Sprite(boomerangTexture, new Vector2(p.xPos , p.yPos + 25),
                    0.5f, new Vector2(-8, -10), Color.White));
            else
                bullet.Add(new Sprite(boomerangTexture, new Vector2(p.xPos + 22, p.yPos + 25),
                    0.5f, new Vector2(8, -10), Color.White));
            this.countBull--;
        }
    }
}
