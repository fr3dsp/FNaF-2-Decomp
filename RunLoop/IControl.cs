// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.RunLoop.IControl
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Sprites;

namespace RuntimeXNA.RunLoop
{

    public interface IControl
    {
      void drawControl(SpriteBatchEffect batch);

      int getX();

      int getY();

      void setFocus(bool bFlag);

      void click(int nClicks);

      void setMouseControlled(bool bFlag);
    }
}
