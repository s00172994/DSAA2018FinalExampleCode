﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CameraNS
{
    /*                     Camera Shake Reference
     * http://xnaessentials.com/post/2011/04/27/shake-that-camera.aspx */

    public class Camera : GameComponent
    {
        #region Properties

        public static Vector2 Target;
        public static Vector2 _camPos = Vector2.Zero;
        public static Vector2 WorldBound;
        public float CameraSpeed = 0.03f;
        public float CameraSpread = 120f;
        public static float Rotation = 0f;
        public static float Zoom = 1f;
        private const float ZOOM_SPEED = 0.01f;

        // Only one Random object needed
        public static readonly Random random = new Random();
        private bool shaking;
        // The maximum magnitude of our shake offset
        private float shakeMagnitude;
        // The total duration of the current shake
        private float shakeDuration;
        // A timer that determines how far into our shake we are
        private float shakeTimer;
        // The shake offset vector
        static Vector2 shakeOffset;

        public static Matrix CurrentCameraTranslation
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-CamPos, 0)) *
                       Matrix.CreateRotationZ(Rotation) *
                       Matrix.CreateScale(Zoom, Zoom, 1);
            }
        }

        public static Vector2 CamPos
        {
            get
            {
                return _camPos;
            }

            set
            {
                _camPos = value;
            }
        }

        static Vector2 _followPos { get; set; }

        #endregion

        #region Constructor

        public Camera(Game game, Vector2 startPos, Vector2 bound): base(game)
        {
            game.Components.Add(this);
            CamPos = startPos;
            WorldBound = bound;
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            //Player p;
            //p = (Player)Game.Components
            //    .FirstOrDefault(pl => pl.GetType() == typeof(Player));
            //if (p != null)
            //    follow(p.Position,Game.GraphicsDevice.Viewport);

            //#region Clamp player within bounds
            //player.PixelPosition = Vector2.Clamp(player.PixelPosition, Vector2.Zero,
            //                                new Vector2(WorldBound.X - player.BoundingRectangle.Width,
            //                                            WorldBound.Y - player.BoundingRectangle.Height));
            //#endregion

            Shake(gameTime);

            base.Update(gameTime);
        }

        private void Shake(GameTime gameTime)
        {
            if (shaking)
            {
                // Move our timer ahead based on the elapsed time
                shakeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // If we're at the max duration, we're not going to be shaking anymore
                if (shakeTimer >= shakeDuration)
                {
                    shaking = false;
                    shakeTimer = shakeDuration;
                }

                // Compute our progress in a [0, 1] range
                float progress = shakeTimer / shakeDuration;

                // Compute our magnitude based on our maximum value and our progress. This causes
                // the shake to reduce in magnitude as time moves on, giving us a smooth transition
                // back to being stationary. We use progress * progress to have a non-linear fall 
                // off of our magnitude. We could switch that with just progress if we want a linear 
                // fall off.

                float magnitude = shakeMagnitude * (1f - (progress * progress));

                // Generate a new offset vector with three random values and our magnitude

                shakeOffset = new Vector2(NextFloat(), NextFloat()) * magnitude;

                // If we're shaking, add our offset to our position and target

                _camPos += shakeOffset;
                _followPos += shakeOffset;
            }
        }

        public static void Follow(Vector2 followPos, Viewport v, float cameraSpeed)
        {
            _followPos = followPos;
            // Center of Viewport
            Vector2 centerScreen = new Vector2(v.Width / 2, v.Height / 2);
            // Add smoothness
            Vector2 delta = ((followPos - centerScreen) - _camPos); // Distance from following position to camera
            _camPos += Vector2.Multiply(delta, cameraSpeed); // Now move the camera by 3%
            _camPos = Vector2.Clamp(_camPos, Vector2.Zero, WorldBound - new Vector2(v.Width, v.Height));
        }

        /// <summary>
        /// Helper to generate a random float in the range of [-1, 1].
        /// </summary>
        private float NextFloat()
        {
            return (float)random.NextDouble() * 2f - 1f;
        }

        /// <summary>
        /// Shakes the camera with a specific magnitude and duration.
        /// </summary>
        /// <param name="magnitude">The largest magnitude to apply to the shake.</param>
        /// <param name="duration">The length of time (in seconds) for which the shake should occur.</param>
        public void Shake(float magnitude, float duration)
        {
            // We're now shaking
            shaking = true;

            // Store our magnitude and duration
            shakeMagnitude = magnitude;
            shakeDuration = duration;

            // Reset our timer
            shakeTimer = 0f;
        }

        #endregion
    }
}
