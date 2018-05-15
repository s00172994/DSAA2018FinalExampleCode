
using AudioPlayer;
using CameraNS;
using InputEngineNS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprites
{
    public class Enemy : MyComponent
    {
        public float speed = 1f;
        public Vector2 Target = Vector2.Zero;
        public Enemy(Game game, string imageName,
                            Vector2 startPosition) : base(game,imageName,startPosition)
        {
            Active = true;
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (!Active) return;
            // If we click on the enemy object play the assosiated sound effect
            // and hide it
            if (InputEngine.IsMouseLeftClick())
            {
                if (BoundingRect.Contains(InputEngine.MousePosition))
                {
                    SoundEffect sndFx;
                    if(AudioManager.SoundEffects.TryGetValue(ImageName, out sndFx))
                    {
                        sndFx.CreateInstance().Play();
                    }
                    Active = false;
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (!Active) return;
            base.Draw(gameTime);
        }
    }
}
