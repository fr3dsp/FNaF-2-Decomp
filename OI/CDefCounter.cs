// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.OI.CDefCounter
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.OI
{

    internal class CDefCounter : CDefObject
    {
      public int ctInit;
      public int ctMini;
      public int ctMaxi;

      public override void load(CFile file)
      {
        file.skipBytes(2);
        this.ctInit = file.readAInt();
        this.ctMini = file.readAInt();
        this.ctMaxi = file.readAInt();
      }
    }
}
