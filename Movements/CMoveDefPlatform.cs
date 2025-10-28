// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveDefPlatform
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    internal class CMoveDefPlatform : CMoveDef
    {
      public short mpSpeed;
      public short mpAcc;
      public short mpDec;
      public short mpJumpControl;
      public short mpGravity;
      public short mpJump;

      public override void load(CFile file, int length)
      {
        this.mpSpeed = file.readAShort();
        this.mpAcc = file.readAShort();
        this.mpDec = file.readAShort();
        this.mpJumpControl = file.readAShort();
        this.mpGravity = file.readAShort();
        this.mpJump = file.readAShort();
      }
    }
}
