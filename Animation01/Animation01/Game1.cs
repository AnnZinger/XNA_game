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

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteManager spriteManager;

        public static Random rnd { get; private set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            rnd = new Random();
            //Задаем размер кадра
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //Делаем на весь экран
           graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "2D Game Name";
        }

        /// <summary>
        /// Позволяет игре выполнить любую инициализацию, необходимую для запуска.
        /// Здесь он может запрашивать любые необходимые службы и загружать любое неграфическое содержимое
        /// Calling base.Initialize загрузит необходимые элементы
        /// </summary>
        protected override void Initialize()
        {
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent будет вызываться один раз за игру и является местом для загрузки всего контента
        /// </summary>
        protected override void LoadContent()
        {
            // Создаем SpriteBatch, который будет использован для прорисовки текстур.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent будет вызываться один раз за игру и является местом для выгрузки всего контента
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Позволяет игре запускать логику, такую как обновление мира,проверка на наличие коллизий, сбор входных данных и воспроизведение аудио.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            // Позволяет выйти из игры
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {

                this.Exit();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// Вызывается для отрисовки игры
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
