// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Values.CDefValues
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Values
{

    public class CDefValues
    {
      public short nValues;
      public int[] values;

      public void load(CFile file)
      {
        this.nValues = file.readAShort();
        this.values = new int[(int) this.nValues];
        for (int index = 0; index < (int) this.nValues; ++index)
          this.values[index] = file.readAInt();
      }
    }
}
