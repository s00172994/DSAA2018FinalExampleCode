using AudioPlayer;
using CameraNS;
using InputEngineNS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NSLoader;
using Sprites;
using Untilities;

namespace ComponentExample2018
{
    public class GameRoot : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            new InputEngine(this);
            new Camera(this, Vector2.Zero, GraphicsDevice.Viewport.Bounds.Size.ToVector2());
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(spriteBatch);
            // This will load a set of Enemy Images into the static dictionary 
            GameUtilities.sGameTextures = Loader.ContentLoad<Texture2D>(Content, "Images");
            // To add additional images from a different location to the same Dictionary
            foreach (var tx in Loader.ContentLoad<Texture2D>(Content, "Images\\Textures"))
            {
                GameUtilities.sGameTextures.Add(tx.Key, tx.Value);
            }
            Enemy e1 = new Enemy(this, "Enemy1", new Vector2(100, 100));

            // load the audio in the Audio Manager
            AudioManager.SoundEffects = Loader.ContentLoad<SoundEffect>(Content, "Audio");
            // TODO: use this.Content to load your game content here
        }
    
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(GameUtilities.sGameTextures["islandicon_2"], GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
