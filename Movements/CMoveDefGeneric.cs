// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveDefGeneric
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    internal class CMoveDefGeneric : CMoveDef
    {
      public short mgSpeed;
      public short mgAcc;
      public short mgDec;
      public short mgBounceMult;
      public int mgDir;

      public override void load(CFile file, int length)
      {
        this.mgSpeed = file.readAShort();
        this.mgAcc = file.readAShort();
        this.mgDec = file.readAShort();
        this.mgBounceMult = file.readAShort();
        this.mgDir = file.readAInt();
      }
    }
}
