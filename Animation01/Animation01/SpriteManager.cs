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
        SpriteBatch spriteBatch;
        float scalingFactor = 4;
        Player player;
        Blaster blaster;
        Boomerang boomerang;
        Grenade grenade;
        int score = 0;
        List<Foreground> foregroundList = new List<Foreground>();
        List<Foreground> riverObjList = new List<Foreground>();
        List<Sprite> foregroundObjectList = new List<Sprite>();
        List<MotionStep> motionStepList = new List<MotionStep>();
        List<Sprite> bulletObjectList = new List<Sprite>();
        List<Sprite> heartObjectList = new List<Sprite>();
        List<Monster> monsterBullList = new List<Monster>();
        List<Monster> monsterBoomList = new List<Monster>();

        List<Sprite> blastBulObj = new List<Sprite>();
        List<Sprite> boomBulObj = new List<Sprite>();
        List<Sprite> granBulObj = new List<Sprite>();

        List<Sprite> boxObjectList = new List<Sprite>();
        List<Sprite> waterObjectList = new List<Sprite>();
        List<Sprite> foodObjectList = new List<Sprite>();
        List<Sprite> oilObjectList = new List<Sprite>();

        SpriteFont UIfont;

        float cameraXPos = 0;
        int movementStateX = 0;
        bool mustCatchUp = false;       

        public SpriteManager(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Sprite.boxTexture = new Texture2D(Game.GraphicsDevice, 1, 1);
            Sprite.boxTexture.SetData(new Color[] { Color.White });

            LoadPlayer();
            LoadForegrounds();
            LoadForegroundObjects();
            LoadMonsters();
            LoadBird();
            LoadBulletObjects();
            LoadHeartObjects();
            LoadFonts();
            LoadBoxObjects();
            LoadRiverObjects();
            LoadWaterObject();
            LoadFoodObject();
            LoadOilObject();
            LoadWeapon();
            LoadMotionStep();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if ((player.currentHP <= 0) || (player.yPos > 190))
            {
                System.Threading.Thread.Sleep(1000);
                Game.Exit();
            }

            player.Update(gameTime);

            if (player.pulledTheTrigger && (!player.isInvulnerable || player.invulnCounter > 15))
            {
                if (player.isBlaster && blaster.countBull > 0)
                {
                    blaster.MakeBullet(blastBulObj, player);
                }

                /* if (player.isBoomerang && boomerang.countBull > 0)//Если в руках бумеранг
                     boomerang.MakeBoom(boomBulObj, player);*/

                if (player.isLauncher && grenade.countBull > 0)//Стреляем гранатометом
                {
                    grenade.MakeBoom(granBulObj, player);
                }

            }
            if (player.isBlaster)//Стреляем из бластера
            {
                blaster.Fire(blastBulObj, gameTime, cameraXPos);
            }
            /*if (player.isBoomerang)
                boomerang.Fire(boomBulObj, gameTime, cameraXPos);*/
            if (player.isLauncher)//Стреляем гранатометом
            {
                grenade.Fire(granBulObj, gameTime, cameraXPos);
            }

            for (int i = 0; i < bulletObjectList.Count; ++i)//Игрок собирает пули для бластера
            {
                Sprite bullet = bulletObjectList[i];

                if (player.boundingBox.Intersects(bullet.boundingBox))
                {
                    blaster.countBull++;
                    bulletObjectList.RemoveAt(0);
                    --i;
                }
            }

            for (int i = 0; i < heartObjectList.Count; ++i)//Игрок собирает жизни
            {
                Sprite heart = heartObjectList[i];

                if ((player.boundingBox.Intersects(heart.boundingBox)) && (player.currentHP < 100))
                {
                    player.currentHP += 5;
                    heartObjectList.RemoveAt(0);
                    --i;
                }
            }

            for (int i = 0; i < waterObjectList.Count; ++i)//Игрок собирает воду
            {
                Sprite water = waterObjectList[i];

                if ((player.boundingBox.Intersects(water.boundingBox)))
                {
                    player.waterCount++;
                    waterObjectList.RemoveAt(0);
                    --i;
                }
            }

            for (int i = 0; i < foodObjectList.Count; ++i)//Игрок собирает еду
            {
                Sprite food = foodObjectList[i];

                if ((player.boundingBox.Intersects(food.boundingBox)))
                {
                    player.foodCount++;
                    foodObjectList.RemoveAt(0);
                    --i;
                }
            }

            for (int i = 0; i < oilObjectList.Count; ++i)//Игрок собирает топливо
            {
                Sprite oil = oilObjectList[i];

                if ((player.boundingBox.Intersects(oil.boundingBox)))
                {
                    player.oilCount++;
                    oilObjectList.RemoveAt(0);
                    --i;
                }
            }
            for (int i = 0; i < monsterBoomList.Count; ++i)//Проверка на различные взаимодействия птицы
            {
                Monster bird = monsterBoomList[i];
                bird.Update(gameTime);
                int hitCount = 0; 
                for (int j = 0; j < boomBulObj.Count; ++j)
                {
                    Sprite boom = boomBulObj[j];

                    if (boom.boundingBox.Intersects(bird.boundingBox))
                    {
                        boomBulObj.RemoveAt(j);
                        --j;
                        ++hitCount;
                    }
                }
                int count = 0;
                while ((grenade.timerThis >= 0) && (count != granBulObj.Count))
                {
                    Sprite pp = granBulObj[count];
                    if (grenade.timerThis == 0)
                    {
                        var distanceToMonster = Math.Sqrt(Math.Pow((pp.xPos - bird.xPos), 2) + Math.Pow((pp.yPos - bird.yPos), 2));
                        if (distanceToMonster <= grenade.radiusFire)
                        {
                            granBulObj.RemoveAt(count);
                            --count;
                            ++hitCount;
                        }
                    }
                    else
                    {
                        grenade.timerThis--;
                    }
                    count++;
                }
                if (hitCount > 0)
                {
                    bird.isHit = true;
                    bird.currentHP -= hitCount;
                    if (bird.currentHP <= 0)
                    {
                        score += monsterBoomList[i].maxHP;
                        monsterBoomList.RemoveAt(i);
                        --i;
                    }
                }
                else if (bird.boundingBox.Intersects(player.boundingBox))
                {
                    player.PlayerIsHit();
                }
            }
            for (int i = 0; i < monsterBullList.Count; ++i)//Проверка на различные взаимодействия монстров
            {
                Monster monster = monsterBullList[i];
                if (monster.maxHP == 1)
                    if (Math.Abs(monster.yPos - player.yPos) < 35)
                        if ((monster.xPos - player.xPos) < 32)
                            if ((monster.xPos - player.xPos) > 0)
                                monster.isLeftOfPlayer = true;
                            else if ((player.xPos - monster.xPos) < 42)
                                if ((player.xPos - monster.xPos) > 10)
                                    monster.isRightOfPlayer = true;
                monster.Update(gameTime);
                int hitCount = 0;
                for (int j = 0; j < blastBulObj.Count; ++j)
                {
                    Sprite p = blastBulObj[j];
                    if (p.boundingBox.Intersects(monster.boundingBox))
                    {
                        blastBulObj.RemoveAt(j);
                        --j;
                        ++hitCount;
                    }
                }
                int kol = 0;
                while ((grenade.timerThis >= 0) && (kol != granBulObj.Count))
                {
                    Sprite pp = granBulObj[kol];
                    if (grenade.timerThis == 0)
                    {
                        var distanceToMonster = Math.Sqrt(Math.Pow((pp.xPos - monster.xPos), 2) + Math.Pow((pp.yPos - monster.yPos), 2));
                        if (distanceToMonster <= grenade.radiusFire)
                        {
                            granBulObj.RemoveAt(kol);
                            --kol;
                            ++hitCount;
                        }
                    }
                    else
                    {
                        grenade.timerThis--;
                    }
                    kol++;
                }
                for (int t = 0; t < motionStepList.Count; ++t)//Обновляем движущиеся платформы
                {
                    MotionStep motionStep = motionStepList[t];
                    motionStep.Update(gameTime);
                }
                if (hitCount > 0)
                {
                    monster.isHit = true;
                    monster.currentHP -= hitCount;
                    if (monster.currentHP <= 0)
                    {
                        score += monsterBullList[i].maxHP;
                        monsterBullList.RemoveAt(i);
                        --i;
                    }
                }

                else if (monster.boundingBox.Intersects(player.boundingBox))
                {
                    player.PlayerIsHit();
                }
            }

            player.FlashPlayerAndUpdateVulnerability();

            CheckForTerrainCollisions();

            MoveCamera_X();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix globalTransformation = Matrix.CreateTranslation(-cameraXPos, 0, 0) *
                Matrix.CreateScale(new Vector3(scalingFactor, scalingFactor, 1));

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp,
                null, null, null, globalTransformation);

            player.Draw(gameTime, spriteBatch);
            foreach (Sprite s in blastBulObj)
                s.Draw(gameTime, spriteBatch);
            foreach (Sprite boom in boomBulObj)
                boom.Draw(gameTime, spriteBatch);
            foreach (Sprite g in granBulObj)
                g.Draw(gameTime, spriteBatch);
            foreach (Foreground b in foregroundList)
                b.Draw(gameTime, spriteBatch);
            foreach (Monster m in monsterBullList)
                m.Draw(gameTime, spriteBatch);
            foreach (Monster b in monsterBoomList)
                b.Draw(gameTime, spriteBatch);
            foreach (Sprite s in foregroundObjectList)
                s.Draw(gameTime, spriteBatch);
            foreach (Sprite p in bulletObjectList)
                p.Draw(gameTime, spriteBatch);
            foreach (Sprite l in heartObjectList)
                l.Draw(gameTime, spriteBatch);
            foreach (Sprite food in foodObjectList)
                food.Draw(gameTime, spriteBatch);
            foreach (Sprite oil in oilObjectList)
                oil.Draw(gameTime, spriteBatch);
            foreach (Sprite wat in waterObjectList)
                wat.Draw(gameTime, spriteBatch);
            foreach (Sprite n in motionStepList)
                n.Draw(gameTime, spriteBatch);
            foreach (Foreground h in riverObjList)
                h.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null);

            spriteBatch.DrawString(UIfont, "Health: " + player.currentHP + "%", new Vector2(1000, 30), Color.White,
                0, Vector2.Zero, 1, SpriteEffects.None, 1);
            if (player.isBlaster)
                spriteBatch.DrawString(UIfont, "Bullets count: " + blaster.countBull, new Vector2(1000, 60), Color.White,
                    0, Vector2.Zero, 1, SpriteEffects.None, 1);
            if (player.isLauncher)
                spriteBatch.DrawString(UIfont, "Grenades count: " + grenade.countBull, new Vector2(1000, 60), Color.White,
                    0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(UIfont, "Your score: " + score, new Vector2(1000, 90), Color.White,
               0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(UIfont, "Water: " + player.waterCount + "/15", new Vector2(20, 30), Color.White,
                0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(UIfont, "Food: " + player.foodCount + "/7", new Vector2(20, 60), Color.White,
                0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(UIfont, "Oil: " + player.oilCount + "/5", new Vector2(20, 90), Color.White,
                0, Vector2.Zero, 1, SpriteEffects.None, 1);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
