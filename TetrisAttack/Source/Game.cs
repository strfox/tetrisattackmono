using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TetrisAttack.Model;
using TetrisAttack.Sprites;

namespace TetrisAttack
{
    public class Game : Microsoft.Xna.Framework.Game
    {

        #region Constants

        public readonly int VirtualWidth = 256;
        public readonly int VirtualHeight = 244;

        #endregion

        #region Helpers

        public int WindowWidth { get { return GraphicsDevice.PresentationParameters.Bounds.Width; } }

        public int WindowHeight { get { return GraphicsDevice.PresentationParameters.Bounds.Height; } }

        public int ViewportWidth { get { return GraphicsDevice.Viewport.Width; } }

        public int ViewportHeight { get { return GraphicsDevice.Viewport.Height; } }

        #endregion

        #region ScaleMatrix

        private Matrix _scaleMatrix;
        private bool _requestScaleMatrixRecalculation = true;

        public void RecalculateScaleMatrix()
        {
            var scaleX = (float)ViewportWidth / VirtualWidth;
            var scaleY = (float)ViewportHeight / VirtualHeight;

            _scaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);
            _requestScaleMatrixRecalculation = false;
        }

        #endregion

        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;

        private List<Sprite> _sprites = new List<Sprite>();

        public Game()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.AllowUserResizing = true;
            Window.AllowAltF4 = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            // Call it just once to make the viewport fit
            Window_ClientSizeChanged(null, null);
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            var aspectRatio = (float)VirtualWidth / VirtualHeight;
            var width = ViewportWidth;
            var height = (int)(width / aspectRatio + 0.5f);

            if (height > ViewportHeight)
            {
                height = ViewportHeight;
                width = (int)(height * aspectRatio + 0.5f);
            }

            var x = (ViewportWidth / 2) - (width / 2);
            var y = (ViewportHeight / 2) - (height / 2);

            GraphicsDevice.Viewport = new Viewport(x, y, width, height);

            _requestScaleMatrixRecalculation = true;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            var hIdle = Content.Load<Texture2D>("Panels/HeartIdle");
            var hPanic = Content.Load<Texture2D>("Panels/HeartPanic");
            var hPop = Content.Load<Texture2D>("Panels/HeartPop");
            var hFlash = Content.Load<Texture2D>("Panels/HeartFlash");
            var hDark = Content.Load<Texture2D>("Panels/HeartDark");
            var heartPanelFactory = new PanelFactory(hIdle, hPanic, hPop, hFlash, hDark);

            var tmp = heartPanelFactory.Make();
            tmp.Panick();
            tmp.Position = new Vector2(200, 200);
            _sprites.Add(tmp);

            #region BorderMarkers
            tmp = heartPanelFactory.Make();
            tmp.Position = Vector2.Zero;
            _sprites.Add(tmp);

            tmp = heartPanelFactory.Make();
            tmp.Position = new Vector2(0, 244 - tmp.Animation.FrameHeight);
            _sprites.Add(tmp);

            tmp = heartPanelFactory.Make();
            tmp.Position = new Vector2(256 - tmp.Animation.FrameWidth, 0);
            _sprites.Add(tmp);

            tmp = heartPanelFactory.Make();
            tmp.Position = new Vector2(256 - tmp.Animation.FrameWidth, 244 - tmp.Animation.FrameHeight);
            _sprites.Add(tmp);
            #endregion

            var p1Board = new Board();
            p1Board.Texture = Content.Load<Texture2D>("Boards/BlueBoard");
            _sprites.Add(p1Board);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (_requestScaleMatrixRecalculation)
            {
                RecalculateScaleMatrix();
            }

            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(transformMatrix: _scaleMatrix);
            foreach (var sprite in _sprites)
            {
                sprite.Draw(SpriteBatch);
            }
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
