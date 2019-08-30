using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TetrisAttack.Model;

namespace TetrisAttack.Sprites
{
    class Panel : AnimatedSprite
    {
        protected Animation Idle, Panic, Pop, Flash, Dark;

        public Panel(Texture2D idle, Texture2D panic, Texture2D pop, Texture2D flash, Texture2D dark)
        {
            Idle = new Animation(idle, 1);
            Panic = new Animation(panic, 4);
            Pop = new Animation(pop, 1);
            Flash = new Animation(flash, 2);
            Dark = new Animation(dark, 1);

            Panic.FrameSpeed = 0.15f;

            Animation = Idle;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Panick()
        {
            Animation = Panic;
        }
    }

    class PanelFactory : IFactory<Panel>
    {
        private Texture2D Idle, Panic, Pop, Flash, Dark;

        public PanelFactory(Texture2D idle, Texture2D panic, Texture2D pop, Texture2D flash, Texture2D dark)
        {
            Idle = idle;
            Panic = panic;
            Pop = pop;
            Flash = flash;
            Dark = dark;
        }

        public Panel Make()
        {
            return new Panel(Idle, Panic, Pop, Flash, Dark);
        }
    }
}
