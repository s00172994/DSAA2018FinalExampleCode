using CameraNS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Untilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprites
{
    public class MyComponent : DrawableGameComponent
    {
        #region Properties

        public string ImageName;
        public Vector2 Position;
        public Rectangle BoundingRect;
        public bool Active;

        #endregion

        #region Constructor

        public MyComponent(Game g, string imageName, Vector2 position) : base(g)
        {
            g.Components.Add(this);
            //game.Components.Add(this);
            // Take a copy of the texture passed down
            ImageName = imageName;
            // Take a copy of the start position
            Position = position;
            // Calculate the bounding rectangle
            Texture2D Image;
            if (GameUtilities.sGameTextures.TryGetValue(ImageName, out Image))
                BoundingRect = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);

        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            //BoundingRect = new Rectangle((int)Position.X, (int)Position.Y, BoundingRect.Width, BoundingRect.Height);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Explain the following code
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();
            Texture2D Image;
            if(GameUtilities.sGameTextures.TryGetValue(ImageName, out Image))
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Camera.CurrentCameraTranslation);
                spriteBatch.Draw(Image, BoundingRect, Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        #endregion
    }
}
