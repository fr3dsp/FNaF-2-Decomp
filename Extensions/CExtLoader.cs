// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Extensions.CExtLoader
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;
using RuntimeXNA.Services;

namespace RuntimeXNA.Extensions
{

    public class CExtLoader
    {
      public const int KPX_BASE = 32 /*0x20*/;
      private CRunApp app;
      private CExtLoad[] extensions;
      private short[] numOfConditions;

      public CExtLoader(CRunApp a) => this.app = a;

      public void loadList(CFile file)
      {
        int num = (int) file.readAShort();
        int length = (int) file.readAShort();
        this.extensions = new CExtLoad[length];
        this.numOfConditions = new short[length];
        for (int index = 0; index < length; ++index)
          this.extensions[index] = (CExtLoad) null;
        for (int index = 0; index < num; ++index)
        {
          CExtLoad cextLoad = new CExtLoad();
          cextLoad.loadInfo(file);
          CRunExtension crunExtension = cextLoad.loadRunObject();
          if (crunExtension != null)
          {
            this.extensions[(int) cextLoad.handle] = cextLoad;
            this.numOfConditions[(int) cextLoad.handle] = (short) crunExtension.getNumberOfConditions();
          }
        }
      }

      public CRunExtension loadRunObject(int type)
      {
        type -= 32 /*0x20*/;
        CRunExtension crunExtension = (CRunExtension) null;
        if (type < this.extensions.Length && this.extensions[type] != null)
          crunExtension = this.extensions[type].loadRunObject();
        return crunExtension;
      }

      public int getNumberOfConditions(int type)
      {
        type -= 32 /*0x20*/;
        return type < this.extensions.Length ? (int) this.numOfConditions[type] : 0;
      }
    }
}
