// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveDefBall
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    internal class CMoveDefBall : CMoveDef
    {
      public short mbSpeed;
      public short mbBounce;
      public short mbAngles;
      public short mbSecurity;
      public short mbDecelerate;

      public override void load(CFile file, int length)
      {
        this.mbSpeed = file.readAShort();
        this.mbBounce = file.readAShort();
        this.mbAngles = file.readAShort();
        this.mbSecurity = file.readAShort();
        this.mbDecelerate = file.readAShort();
      }
    }
}
