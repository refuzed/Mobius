using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Möbius
{
    public class Tessallation : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D background;
        private const int MaxX = 600;
        private const int MaxY = 600;
        private const int MoveSpeed = 6;

        private int posX;
        private int posY;
        private int imgMaxX;
        private int imgMaxY;

        public Tessallation()
        {
            graphics = new GraphicsDeviceManager(this)
                {
                    IsFullScreen = false,
                    PreferredBackBufferWidth = MaxX,
                    PreferredBackBufferHeight = MaxY
                };

            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("starfield");
            imgMaxX = background.Width;
            imgMaxY = background.Height;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var currentKeyState = Keyboard.GetState();

            if (currentKeyState.IsKeyDown(Keys.Left))
                posX = posX - MoveSpeed < 0         ? imgMaxX + posX - MoveSpeed : posX - MoveSpeed;
            if (currentKeyState.IsKeyDown(Keys.Right))
                posX = posX + MoveSpeed > imgMaxX   ? imgMaxX - posX + MoveSpeed : posX + MoveSpeed;
            if (currentKeyState.IsKeyDown(Keys.Up))
                posY = posY - MoveSpeed < 0         ? imgMaxY + posY - MoveSpeed : posY - MoveSpeed;
            if (currentKeyState.IsKeyDown(Keys.Down))
                posY = posY + MoveSpeed > imgMaxY   ? imgMaxY - posY + MoveSpeed : posY + MoveSpeed;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Case 1: windowed 1/2 left & 1/2 right of sprite map
            if(posX + MaxX > imgMaxX && posY + MaxY <= imgMaxY)
            {
                var offsetX = imgMaxX - posX;
                // left
                spriteBatch.Draw(background, new Rectangle(0, 0, offsetX, MaxY), new Rectangle(posX, posY, offsetX, MaxY), Color.AliceBlue);

                //right
                spriteBatch.Draw(background, new Rectangle(offsetX, 0, MaxX - offsetX, MaxY), new Rectangle(0, posY, MaxX - offsetX, MaxY), Color.AliceBlue);
            }
            // Case 2: windowed 1/2 top & 1/2 bottom of sprite map
            else if(posX + MaxX <= imgMaxX && posY + MaxY > imgMaxY)
            {
                var offsetY = imgMaxY - posY;
                //top
                spriteBatch.Draw(background, new Rectangle(0, 0, MaxX, offsetY), new Rectangle(posX, posY, MaxX, offsetY), Color.AliceBlue);

                // bot
                spriteBatch.Draw(background, new Rectangle(0, offsetY, MaxX, MaxY - offsetY), new Rectangle(posX, 0, MaxX, MaxY - offsetY), Color.AliceBlue);
            }
            // Case 3: windowed on all edges of sprite map
            else if (posX + MaxX > imgMaxX && posY + MaxY > imgMaxY)
            {
                var offsetX = imgMaxX - posX;
                var offsetY = imgMaxY - posY;
                //top left
                spriteBatch.Draw(background, new Rectangle(0, 0, offsetX, offsetY), new Rectangle(posX, posY, offsetX, offsetY), Color.AliceBlue);

                // top right
                spriteBatch.Draw(background, new Rectangle(offsetX, 0, MaxX - offsetX, offsetY), new Rectangle(0, posY, MaxX - offsetX, offsetY), Color.AliceBlue);

                // bot left
                spriteBatch.Draw(background, new Rectangle(0, offsetY, offsetX, MaxY - offsetY), new Rectangle(posX, 0, offsetX, MaxY - offsetY), Color.AliceBlue);

                // bot right
                spriteBatch.Draw(background, new Rectangle(offsetX, offsetY, MaxX - offsetX, MaxY - offsetY), new Rectangle(0, 0, MaxX - offsetX, MaxY - offsetY), Color.AliceBlue);
            }
            else spriteBatch.Draw(background, new Rectangle(0, 0, MaxX, MaxY), new Rectangle(posX, posY, MaxX, MaxY), Color.AliceBlue);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
