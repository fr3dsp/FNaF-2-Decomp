// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Values.CDefStrings
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Values
{

    public class CDefStrings
    {
      public short nStrings;
      public string[] strings;

      public void load(CFile file)
      {
        this.nStrings = file.readAShort();
        this.strings = new string[(int) this.nStrings];
        for (int index = 0; index < (int) this.nStrings; ++index)
          this.strings[index] = file.readAString();
      }
    }
}
