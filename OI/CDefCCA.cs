// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.OI.CDefCCA
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.OI
{

    internal class CDefCCA : CDefObject
    {
      public int odCx;
      public int odCy;
      public short odVersion;
      public short odNStartFrame;
      public int odOptions;
      public string odName;

      public override void load(CFile file)
      {
        file.skipBytes(4);
        this.odCx = file.readAInt();
        this.odCy = file.readAInt();
        this.odVersion = file.readAShort();
        this.odNStartFrame = file.readAShort();
        this.odOptions = file.readAInt();
        file.skipBytes(8);
        this.odName = file.readAString();
      }
    }
}
