﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolKit;
using UmbrellaToolKit.UI;
using tainicom.Aether.Physics2D.Dynamics;

namespace ball.Gameplay
{
    public class Hud : Scene
    {
        public MouseManager Mouse;

        public Reload Reload;
        public GameObject Home;
        public GameObject Resize;

        public Stage CurrentLevel;

        public void Start(ContentManager Content, World World, MouseManager Mouse, ScreemController ScreemController)
        {
            this.Content = Content;
            this.Mouse = Mouse;
            this.Screem = ScreemController;

            this.Reload = new Reload();
            this.Reload.Start(Content, World, Mouse, ScreemController);

            this.UI.Add(this.Reload);
            this.SetBackgroundColor = Color.Transparent;
            this.LevelReady = true;
        }

        public void SetLevel(Stage stage)
        {
            this.CurrentLevel = stage;
            this.Reload.CurrentLevel = stage;
        }
    }

    #region Reload
    public class Reload : GameObject
    {
        public Stage CurrentLevel;
        public void Start(ContentManager Content, World World, MouseManager Mouse, ScreemController ScreemController)
        {
            this._Screem = ScreemController;
            this.Sprite = Content.Load<Texture2D>("Sprites/UI/return");
            this._bodySize = new Vector2(this.Sprite.Height, this.Sprite.Width);
            this.TextureSize = this._bodySize;
            this._Mouse = Mouse;
            this.SetBoxCollision(World);
        }

        bool _pressedLeftButton;
        public override void Update(GameTime gameTime)
        {
            if (_MouseOver && Mouse.GetState().LeftButton == ButtonState.Pressed && !_pressedLeftButton)
            {
                CurrentLevel.Destroy();
                CurrentLevel.ResetLevel();
                _pressedLeftButton = true;
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released) _pressedLeftButton = false;

            Vector2 _position = new Vector2(this._Screem.getCenterScreem.X, this.Sprite.Height);
            this.CBody.SetTransform(ref _position, this.CBody.Rotation);

            _MouseOver = false;
        }
        
        bool _MouseOver;
        public override void OnMouseOver()
        {
            _MouseOver = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.DrawSprite(spriteBatch);
        }
    }
    #endregion
}
