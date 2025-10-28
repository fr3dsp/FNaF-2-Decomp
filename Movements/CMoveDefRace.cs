// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveDefRace
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    internal class CMoveDefRace : CMoveDef
    {
      public short mrSpeed;
      public short mrAcc;
      public short mrDec;
      public short mrRot;
      public short mrBounceMult;
      public short mrAngles;
      public short mrOkReverse;

      public override void load(CFile file, int length)
      {
        this.mrSpeed = file.readAShort();
        this.mrAcc = file.readAShort();
        this.mrDec = file.readAShort();
        this.mrRot = file.readAShort();
        this.mrBounceMult = file.readAShort();
        this.mrAngles = file.readAShort();
        this.mrOkReverse = file.readAShort();
      }
    }
}
