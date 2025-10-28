// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Sprites.IDrawing
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;

namespace RuntimeXNA.Sprites
{

    public interface IDrawing
    {
      void drawableDraw(SpriteBatchEffect batch, CSprite sprite, CImageBank bank, int x, int y);

      void drawableKill();

      CMask drawableGetMask(int flags);
    }
}
