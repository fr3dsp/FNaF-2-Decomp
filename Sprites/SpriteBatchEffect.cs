// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Sprites.SpriteBatchEffect
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RuntimeXNA.Sprites
{

    public class SpriteBatchEffect
    {
      private int effect;
      private BlendState stateSub;
      private SpriteBatch batch;
      public GraphicsDevice GraphicsDevice;

      public SpriteBatchEffect(ContentManager content, GraphicsDevice device)
      {
        this.stateSub = new BlendState();
        this.stateSub.ColorSourceBlend = Blend.SourceAlpha;
        this.stateSub.AlphaSourceBlend = Blend.SourceAlpha;
        this.stateSub.ColorDestinationBlend = Blend.One;
        this.stateSub.AlphaDestinationBlend = Blend.One;
        this.stateSub.ColorBlendFunction = BlendFunction.ReverseSubtract;
        this.GraphicsDevice = device;
        this.batch = new SpriteBatch(device);
      }

      public void Begin()
      {
        this.effect = 0;
        this.batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
      }

      public void End() => this.batch.End();

      public void SetEffect(int e)
      {
        if (e == this.effect)
          return;
        this.effect = e;
        if (this.effect == 9)
        {
          this.batch.End();
          this.batch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
        }
        else
        {
          this.batch.End();
          this.batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
          this.effect = 0;
        }
      }

      public void Draw(
        Texture2D texture,
        Vector2 position,
        Rectangle? sourceRectangle,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth,
        int e,
        int effectParam)
      {
        if (e != this.effect)
          this.SetEffect(e);
        if (this.effect == 1)
        {
          float num = (float) (128 /*0x80*/ - effectParam) / 128f;
          color *= num;
        }
        this.batch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
      }

      public void Draw(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle? sourceRectangle,
        Color color,
        float rotation,
        Vector2 origin,
        int e)
      {
        if (e != this.effect)
          this.SetEffect(e);
        this.batch.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, SpriteEffects.None, 0.0f);
      }

      public void Draw(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle? sourceRectangle,
        Color color)
      {
        this.SetEffect(0);
        this.batch.Draw(texture, destinationRectangle, sourceRectangle, color);
      }

      public void Draw(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle? sourceRectangle,
        Color color,
        int effect,
        int effectParam)
      {
        this.SetEffect(effect & 4095 /*0x0FFF*/);
        this.batch.Draw(texture, destinationRectangle, sourceRectangle, color);
      }

      public void DrawString(SpriteFont font, string s, Vector2 v, Color c)
      {
        this.SetEffect(0);
        this.batch.DrawString(font, s, v, c);
      }

      public void DrawString(SpriteFont font, string s, Vector2 v, Color c, int e, int effectParam)
      {
        if (e != this.effect)
          this.SetEffect(e);
        if (this.effect == 1)
        {
          float num = (float) (128 /*0x80*/ - effectParam) / 128f;
          c *= num;
        }
        this.batch.DrawString(font, s, v, c);
      }
    }
}
