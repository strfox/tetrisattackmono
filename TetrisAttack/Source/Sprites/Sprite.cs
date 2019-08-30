using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TetrisAttack.Model;

namespace TetrisAttack.Sprites
{
    class Sprite
    {
        public Vector2 Position;

        public Texture2D Texture;

        public Input Input;

        public float Speed = 1f;

        public Vector2 Velocity;

        public virtual void Update(GameTime gameTime)
        {
            Position += Velocity;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
