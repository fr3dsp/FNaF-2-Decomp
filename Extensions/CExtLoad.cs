// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Extensions.CExtLoad
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;
using System;

namespace RuntimeXNA.Extensions
{

    internal class CExtLoad
    {
      public string name;
      public string subType;
      public short handle;

      public void loadInfo(CFile file)
      {
        int filePointer = file.getFilePointer();
        short num = Math.Abs(file.readAShort());
        this.handle = file.readAShort();
        file.skipBytes(12);
        this.name = file.readAString();
        this.name = this.name.Substring(0, this.name.LastIndexOf('.'));
        this.subType = file.readAString();
        file.seek(filePointer + (int) num);
      }

      public CRunExtension loadRunObject()
      {
        CRunExtension crunExtension = (CRunExtension) null;
        if (string.Compare(this.name, "XNA") == 0)
          crunExtension = (CRunExtension) new CRunXNA();
        if (string.Compare(this.name, "kcini") == 0)
          crunExtension = (CRunExtension) new CRunkcini();
        return crunExtension;
      }
    }
}
