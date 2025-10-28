// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveDefExtension
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    public class CMoveDefExtension : CMoveDef
    {
      public string moduleName;
      public int mvtID;
      public byte[] data;

      public override void load(CFile file, int length)
      {
        file.skipBytes(14);
        this.data = new byte[length - 14];
        file.read(this.data);
      }

      public void setModuleName(string name, int id)
      {
        this.moduleName = name;
        this.mvtID = id;
      }
    }
}
