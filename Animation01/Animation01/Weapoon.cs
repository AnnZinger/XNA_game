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
    /// Базовый класс для оружия, которым пользуется игрок
    /// </summary>
    class Weapoon
    {
        public int countBull { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bullet"></param>Лист выпущенных пуль
        public void Fire(List<Sprite> bullet, GameTime gameT, float cameraX)
        {

            for (int i = 0; i < bullet.Count; ++i)
            {
                Sprite p = bullet[i];

                p.Update(gameT);

                // Проверка на выход за границы кадра
                if (p.xPos - p.texture.Width + 5 < cameraX || p.xPos - 5 > cameraX + 320)
                {
                    bullet.RemoveAt(i);
                }
            }
        }
    }
}
