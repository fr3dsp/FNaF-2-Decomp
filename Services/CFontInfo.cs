// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Services.CFontInfo
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

namespace RuntimeXNA.Services
{

    public class CFontInfo
    {
      public int lfHeight;
      public int lfWeight;
      public byte lfItalic;
      public byte lfUnderline;
      public byte lfStrikeOut;
      public string lfFaceName;

      public void copy(CFontInfo f)
      {
        this.lfHeight = f.lfHeight;
        this.lfWeight = f.lfWeight;
        this.lfItalic = f.lfItalic;
        this.lfUnderline = f.lfUnderline;
        this.lfStrikeOut = f.lfStrikeOut;
        this.lfFaceName = f.lfFaceName;
      }
    }
}
