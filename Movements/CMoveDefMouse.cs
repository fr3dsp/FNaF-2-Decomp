// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveDefMouse
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    internal class CMoveDefMouse : CMoveDef
    {
      public short mmDx;
      public short mmFx;
      public short mmDy;
      public short mmFy;
      public short mmFlags;

      public override void load(CFile file, int length)
      {
        this.mmDx = file.readAShort();
        this.mmFx = file.readAShort();
        this.mmDy = file.readAShort();
        this.mmFy = file.readAShort();
        this.mmFlags = file.readAShort();
      }
    }
}
