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

        void LoadPlayer()
        {

            Player.playerTextures[0] = Game.Content.Load<Texture2D>("player/player_idle_left");
            Player.playerTextures[1] = Game.Content.Load<Texture2D>("player/player_idle_right");
            Player.playerTextures[2] = Game.Content.Load<Texture2D>("player/player_walk_left");
            Player.playerTextures[3] = Game.Content.Load<Texture2D>("player/player_walk_right");
            Player.playerTextures[4] = Game.Content.Load<Texture2D>("player/player_jump_left");
            Player.playerTextures[5] = Game.Content.Load<Texture2D>("player/player_jump_right");

            Player.playerTextures[6] = Game.Content.Load<Texture2D>("player/player_boom/player_idle_left");
            Player.playerTextures[7] = Game.Content.Load<Texture2D>("player/player_boom/player_idle_right");
            Player.playerTextures[8] = Game.Content.Load<Texture2D>("player/player_boom/player_walk_left");
            Player.playerTextures[9] = Game.Content.Load<Texture2D>("player/player_boom/player_walk_right");
            Player.playerTextures[10] = Game.Content.Load<Texture2D>("player/player_boom/player_jump_left");
            Player.playerTextures[11] = Game.Content.Load<Texture2D>("player/player_boom/player_jump_right");

            player = new Player(new Vector2(20, 119), new Point(30, 50), new Point(6, 4));
        }
       
        void LoadForegrounds()
        {
            for (int i = 0; i < 9600; i += 320)
            {
                foregroundList.Add(new Foreground(Game.Content.Load<Texture2D>("backgrounds/street"),
                new Vector2(i, 0)));
            }

            for (int i = 0; i < 9600; i += 320)
            {
                if(i!=640&&i!=1280)
                Foreground.floorBoxList.Add(new Rectangle(i, 175, 320, 5));
            }
            ///Создаем границы игрового поля
            Foreground.wallBoxList.Add(new Rectangle(3, -180, 5, 360)); 
            Foreground.wallBoxList.Add(new Rectangle(9600, -180, 5, 360));
            Foreground.ceilingBoxList.Add(new Rectangle(0, 3, 9600, 1));
        }

        void LoadForegroundObjects()
        {
            TestStepBox.stepBoxImage = Game.Content.Load<Texture2D>("backgrounds/reference_level_step");

            foregroundObjectList.Add(new TestStepBox(new Vector2(410, 95)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(460, 55)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(640, 105)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(730, 65)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(800, 95)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(900, 95)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(1060, 95)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(1250, 70)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(1390, 85)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(1500, 85)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(2500, 105)));

            TestStepBox.stepBoxImage = Game.Content.Load<Texture2D>("backgrounds/barrier");
            foregroundObjectList.Add(new TestStepBox(new Vector2(630, 160)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(930, 160)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(1270, 160)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(1590, 160)));
            TestStepBox.stepBoxImage = Game.Content.Load<Texture2D>("backgrounds/barrier2");
            foregroundObjectList.Add(new TestStepBox(new Vector2(70, 60)));
        }

        void LoadMotionStep()
        {
            MotionStep.stepBoxImage = Game.Content.Load<Texture2D>("backgrounds/reference_level_step");
            motionStepList.Add(new MotionStep(new Vector2(1100, 80)));
            MotionStep step = motionStepList[0];
            step.xRight = 1190;
            step.xLeft = 1100;

            motionStepList.Add(new MotionStep(new Vector2(1280, 150)));
            step = motionStepList[1];
            step.xRight = 1380;
            step.xLeft = 1280;

            motionStepList.Add(new MotionStep(new Vector2(1450, 150)));
            step = motionStepList[2];
            step.xRight = 1535;
            step.xLeft = 1450;

            motionStepList.Add(new MotionStep(new Vector2(2700, 150)));
            step = motionStepList[3];
            step.xRight = 3400;
            step.xLeft = 2700;
        }

        void LoadWeapon()
        {
            Blaster.blastBullTexture = Game.Content.Load<Texture2D>("player/projectiles/basic_projectile_left");
            blaster = new Blaster(30);

            Boomerang.boomerangTexture = Game.Content.Load<Texture2D>("player/projectiles/boomerang");
            boomerang = new Boomerang(10);

            Grenade.launcherBullTexture = Game.Content.Load<Texture2D>("player/projectiles/gran");
            grenade = new Grenade(50,60,20);

        }

        void LoadRiverObjects()
        {
            TestStepBox.stepBoxImage = Game.Content.Load<Texture2D>("backgrounds/river");
            foregroundObjectList.Add(new TestStepBox(new Vector2(640, 175)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(1280, 175)));
            foregroundObjectList.Add(new TestStepBox(new Vector2(3000, 175)));
        }

        void LoadBoxObjects()
        {
            BoxWithObject.boxImage = Game.Content.Load<Texture2D>("backgrounds/box");
            foregroundObjectList.Add(new BoxWithObject(new Vector2(490, 70)));
            foregroundObjectList.Add(new BoxWithObject(new Vector2(1000, 135)));
        }

        void LoadBulletObjects()
        {
            DissapearsObjects.objTexture = Game.Content.Load<Texture2D>("player/projectiles/basic_projectile_left");

            bulletObjectList.Add(new DissapearsObjects(new Vector2(275, 150))); 
            bulletObjectList.Add(new DissapearsObjects(new Vector2(265, 150)));
            bulletObjectList.Add(new DissapearsObjects(new Vector2(255, 150)));

            bulletObjectList.Add(new DissapearsObjects(new Vector2(525, 85))); 
            bulletObjectList.Add(new DissapearsObjects(new Vector2(515, 85)));
            bulletObjectList.Add(new DissapearsObjects(new Vector2(505, 85)));

            bulletObjectList.Add(new DissapearsObjects(new Vector2(1015, 150))); 
            bulletObjectList.Add(new DissapearsObjects(new Vector2(1025, 150)));
            bulletObjectList.Add(new DissapearsObjects(new Vector2(1035, 150)));

            bulletObjectList.Add(new DissapearsObjects(new Vector2(1290, 115)));
            bulletObjectList.Add(new DissapearsObjects(new Vector2(1335, 115)));
            bulletObjectList.Add(new DissapearsObjects(new Vector2(1380, 115)));
            bulletObjectList.Add(new DissapearsObjects(new Vector2(1490, 115)));
            bulletObjectList.Add(new DissapearsObjects(new Vector2(1535, 115)));

            bulletObjectList.Add(new DissapearsObjects(new Vector2(3400, 90)));
            bulletObjectList.Add(new DissapearsObjects(new Vector2(3500, 80)));
            bulletObjectList.Add(new DissapearsObjects(new Vector2(3600, 70)));
        }
 
        void LoadWaterObject()
        {
            DissapearsObjects.objTexture = Game.Content.Load<Texture2D>("player/projectiles/water");
            waterObjectList.Add(new DissapearsObjects(new Vector2(650, 85)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(740, 45)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(810, 75)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(1000, 75)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(1510, 65)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(2800, 85)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(3000, 45)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(3800, 75)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(4500, 75)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(4800, 65)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(5000, 85)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(7012, 45)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(8390, 75)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(8450, 75)));
            waterObjectList.Add(new DissapearsObjects(new Vector2(8900, 65)));

        }
 
        void LoadFoodObject()
        {
            DissapearsObjects.objTexture = Game.Content.Load<Texture2D>("player/projectiles/eat");
            foodObjectList.Add(new DissapearsObjects(new Vector2(1260, 45)));
            foodObjectList.Add(new DissapearsObjects(new Vector2(2000, 100)));
            foodObjectList.Add(new DissapearsObjects(new Vector2(2400, 100)));
            foodObjectList.Add(new DissapearsObjects(new Vector2(3500, 45)));
            foodObjectList.Add(new DissapearsObjects(new Vector2(5000, 150)));
            foodObjectList.Add(new DissapearsObjects(new Vector2(7000, 150)));
            foodObjectList.Add(new DissapearsObjects(new Vector2(9000, 45)));

        }

        void LoadOilObject()
        {
            DissapearsObjects.objTexture = Game.Content.Load<Texture2D>("player/projectiles/oil");
            oilObjectList.Add(new DissapearsObjects(new Vector2(1400, 45)));
            oilObjectList.Add(new DissapearsObjects(new Vector2(1800, 45)));
            oilObjectList.Add(new DissapearsObjects(new Vector2(3200, 45)));
            oilObjectList.Add(new DissapearsObjects(new Vector2(3900, 45)));
            oilObjectList.Add(new DissapearsObjects(new Vector2(7000, 45)));
        }

        void LoadHeartObjects()
        {

            DissapearsObjects.objTexture = Game.Content.Load<Texture2D>("player/projectiles/heart");
            heartObjectList.Add(new DissapearsObjects(new Vector2(470, 35)));
            heartObjectList.Add(new DissapearsObjects(new Vector2(1030, 150)));
            heartObjectList.Add(new DissapearsObjects(new Vector2(2789, 100)));
        }

        void LoadBird()
        {
            Bird.birdTextures[0] = Game.Content.Load<Texture2D>("monsters/bird/bird");
            Bird.birdTextures[1] = Game.Content.Load<Texture2D>("monsters/bird/bird");
            Bird.birdTextures[2] = Game.Content.Load<Texture2D>("monsters/bird/bird");
            Bird.birdTextures[3] = Game.Content.Load<Texture2D>("monsters/bird/bird");

            monsterBoomList.Add(new Bird(new Vector2(50, 60)));

            monsterBoomList.Add(new Bird(new Vector2(1000, 60)));
        }

        void LoadMonsters()
        {
            ReverseMob.reverseMobTextures[0] = Game.Content.Load<Texture2D>("monsters/reverse/idle_left");
            ReverseMob.reverseMobTextures[1] = Game.Content.Load<Texture2D>("monsters/reverse/idle_right");
            ReverseMob.reverseMobTextures[2] = Game.Content.Load<Texture2D>("monsters/reverse/walk_left");
            ReverseMob.reverseMobTextures[3] = Game.Content.Load<Texture2D>("monsters/reverse/walk_right");

            BlobMob.blobMobTextures[0] = Game.Content.Load<Texture2D>("monsters/blob/idle_left");
            BlobMob.blobMobTextures[1] = Game.Content.Load<Texture2D>("monsters/blob/idle_right");
            BlobMob.blobMobTextures[2] = Game.Content.Load<Texture2D>("monsters/blob/jump_left");
            BlobMob.blobMobTextures[3] = Game.Content.Load<Texture2D>("monsters/blob/jump_right");

            for (int i = 0; i < 30; i++)
            {
                if ((i > 0) && (i < 3))
                {
                    monsterBullList.Add(new ReverseMob(new Vector2(500 + (40 + Game1.rnd.Next(50)) * i, 100)));
                }
                if ((i > 3) && (i < 7))
                {
                    monsterBullList.Add(new ReverseMob(new Vector2(4000 + (40 + Game1.rnd.Next(50)) * i, 100)));
                }
                if ((i < 11) && (i > 7))
                {
                    monsterBullList.Add(new ReverseMob(new Vector2(50 + (40 + Game1.rnd.Next(100)) * i, 100)));
                }
                if ((i < 20) && (i > 11))
                {
                    monsterBullList.Add(new ReverseMob(new Vector2(2000 + (40 + Game1.rnd.Next(100)) * 2 * i, 100)));
                }
                else
                {
                    monsterBullList.Add(new ReverseMob(new Vector2(1000 + (40 + Game1.rnd.Next(60)) * i, 100)));
                }

            }

            monsterBullList.Add(new BlobMob(new Vector2(150, 130)));
            monsterBullList.Add(new BlobMob(new Vector2(408, 75)));
            monsterBullList.Add(new BlobMob(new Vector2(1900, 75)));
            monsterBullList.Add(new BlobMob(new Vector2(3600, 75)));
        }
     
        void LoadFonts()
        {
            UIfont = Game.Content.Load<SpriteFont>("UI_font");
        }
    }
}
