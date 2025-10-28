// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CPathStep
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    internal class CPathStep
    {
      public byte mdSpeed;
      public byte mdDir;
      public short mdDx;
      public short mdDy;
      public short mdCosinus;
      public short mdSinus;
      public short mdLength;
      public short mdPause;
      public string mdName;

      public void load(CFile file)
      {
        this.mdSpeed = file.readByte();
        this.mdDir = file.readByte();
        this.mdDx = file.readAShort();
        this.mdDy = file.readAShort();
        this.mdCosinus = file.readAShort();
        this.mdSinus = file.readAShort();
        this.mdLength = file.readAShort();
        this.mdPause = file.readAShort();
        string str = file.readAString();
        if (str.Length <= 0)
          return;
        this.mdName = str;
      }
    }
}
