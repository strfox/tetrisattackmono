using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TetrisAttack.Model;

namespace TetrisAttack.Sprites
{
    class AnimatedSprite : Sprite
    {
        protected float _timer;

        public Animation Animation { get; set; }

        public void ResetAnimation()
        {
            Animation.CurrentFrame = 0;
            _timer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > Animation.FrameSpeed)
            {
                _timer = 0f;
                Animation.CurrentFrame++;

                if (Animation.CurrentFrame >= Animation.FrameCount)
                {
                    Animation.CurrentFrame = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Animation.Texture,
                            Position,
                            new Rectangle(Animation.CurrentFrame * Animation.FrameWidth,
                                        0,
                                        Animation.FrameWidth,
                                        Animation.FrameHeight), Color.White);
        }
    }
}
