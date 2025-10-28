// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Frame.CLayer
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Frame
{

    public class CLayer
    {
      public const int FLOPT_XCOEF = 1;
      public const int FLOPT_YCOEF = 2;
      public const int FLOPT_NOSAVEBKD = 4;
      public const int FLOPT_VISIBLE = 16 /*0x10*/;
      public const int FLOPT_WRAP_HORZ = 32 /*0x20*/;
      public const int FLOPT_WRAP_VERT = 64 /*0x40*/;
      public const int FLOPT_REDRAW = 65536 /*0x010000*/;
      public const int FLOPT_TOHIDE = 131072 /*0x020000*/;
      public const int FLOPT_TOSHOW = 262144 /*0x040000*/;
      public string pName;
      public int x;
      public int y;
      public int dx;
      public int dy;
      public CArrayList pBkd2;
      public CArrayList pLadders;
      public int nZOrderMax;
      public int dwOptions;
      public float xCoef;
      public float yCoef;
      public int nBkdLOs;
      public int nFirstLOIndex;
      public int backUp_dwOptions;
      public float backUp_xCoef;
      public float backUp_yCoef;
      public int backUp_nBkdLOs;
      public int backUp_nFirstLOIndex;

      public void load(CFile file)
      {
        this.dwOptions = file.readAInt();
        this.xCoef = file.readAFloat();
        this.yCoef = file.readAFloat();
        this.nBkdLOs = file.readAInt();
        this.nFirstLOIndex = file.readAInt();
        this.pName = file.readAString();
        this.backUp_dwOptions = this.dwOptions;
        this.backUp_xCoef = this.xCoef;
        this.backUp_yCoef = this.yCoef;
        this.backUp_nBkdLOs = this.nBkdLOs;
        this.backUp_nFirstLOIndex = this.nFirstLOIndex;
      }
    }
}
