// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveDef
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    public class CMoveDef
    {
      public const short MVTYPE_STATIC = 0;
      public const short MVTYPE_MOUSE = 1;
      public const short MVTYPE_RACE = 2;
      public const short MVTYPE_GENERIC = 3;
      public const short MVTYPE_BALL = 4;
      public const short MVTYPE_TAPED = 5;
      public const short MVTYPE_PLATFORM = 9;
      public const short MVTYPE_DISAPPEAR = 11;
      public const short MVTYPE_APPEAR = 12;
      public const short MVTYPE_BULLET = 13;
      public const short MVTYPE_EXT = 14;
      public short mvType;
      public short mvControl;
      public byte mvMoveAtStart;
      public int mvDirAtStart;
      public byte mvOpt;

      public virtual void load(CFile file, int length)
      {
      }

      public void setData(short t, short c, byte m, int d, byte mo)
      {
        this.mvType = t;
        this.mvControl = c;
        this.mvMoveAtStart = m;
        this.mvDirAtStart = d;
        this.mvOpt = mo;
      }
    }
}
