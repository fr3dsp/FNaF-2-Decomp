// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Game1
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using BinaryRead;
//using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using RuntimeXNA.Application;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;
using System;

namespace RuntimeXNA
{

    public class Game1 : Game
    {
      public GraphicsDeviceManager graphics;
      public SpriteBatchEffect spriteBatch;
      public bool bPreviousActive;
      public bool bInitialActivation;
      private CRunApp application;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.bPreviousActive = false;
            this.bInitialActivation = true;
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);

            if (this.application == null || this.application.run == null)
                return;

            if (this.application.XNAObject != null)
                this.application.run.callEventExtension(this.application.XNAObject, 3, 0);

            this.application.run.resume();
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);

            if (this.application == null || this.application.run == null)
                return;

            if (this.application.XNAObject != null)
                this.application.run.callEventExtension(this.application.XNAObject, 2, 0);

            this.application.run.pause();
        }

        protected override void Initialize() => base.Initialize();

      protected override void LoadContent()
      {
        this.spriteBatch = new SpriteBatchEffect(this.Content, this.GraphicsDevice);
        this.IsMouseVisible = true;
        this.application = new CRunApp(this, new CFile(this.Content.Load<Data>("Application").data));
        if (!this.application.load())
          return;
        this.application.startApplication();
      }

      protected override void UnloadContent()
      {
      }

      protected override void Update(GameTime gameTime)
      {
        if (this.bPreviousActive != this.IsActive)
        {
          this.bPreviousActive = this.IsActive;
          if (!this.bInitialActivation)
          {
            if (this.application.run != null)
            {
              if (this.IsActive)
                this.application.run.resume();
              else
                this.application.run.pause();
            }
          }
          else
            this.bInitialActivation = false;
        }
        if (!this.application.playApplication(false, gameTime.TotalGameTime.TotalMilliseconds))
          this.Exit();
        base.Update(gameTime);
      }

      protected override void Draw(GameTime gameTime)
      {
        double totalMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
        this.application.draw();
        base.Draw(gameTime);
      }
    }
}
