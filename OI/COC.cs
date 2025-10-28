// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.OI.COC
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Services;

namespace RuntimeXNA.OI
{

    public class COC
    {
      public const short OBSTACLE_NONE = 0;
      public const short OBSTACLE_SOLID = 1;
      public const short OBSTACLE_PLATFORM = 2;
      public const short OBSTACLE_LADDER = 3;
      public const short OBSTACLE_TRANSPARENT = 4;
      public short ocObstacleType;
      public short ocColMode;
      public int ocCx;
      public int ocCy;
      public COI oi;

      public virtual void load(CFile file, short type)
      {
      }

      public virtual void enumElements(IEnum enumImages, IEnum enumFonts)
      {
      }
    }
}
