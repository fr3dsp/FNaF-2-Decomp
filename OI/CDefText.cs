// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.OI.CDefText
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Services;

namespace RuntimeXNA.OI
{

    public class CDefText
    {
      public const short TSF_LEFT = 0;
      public const short TSF_HCENTER = 1;
      public const short TSF_RIGHT = 2;
      public const short TSF_VCENTER = 4;
      public const short TSF_HALIGN = 15;
      public const short TSF_CORRECT = 256 /*0x0100*/;
      public const short TSF_RELIEF = 512 /*0x0200*/;
      public short tsFont;
      public short tsFlags;
      public int tsColor;
      public string tsText;

      public void load(CFile file)
      {
        this.tsFont = file.readAShort();
        this.tsFlags = file.readAShort();
        this.tsColor = file.readAColor();
        this.tsText = file.readAString();
      }

      public void enumElements(IEnum enumImages, IEnum enumFonts)
      {
        if (enumFonts == null)
          return;
        short num = enumFonts.enumerate(this.tsFont);
        if (num == (short) -1)
          return;
        this.tsFont = num;
      }
    }
}
