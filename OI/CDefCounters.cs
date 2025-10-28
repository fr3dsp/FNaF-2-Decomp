// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.OI.CDefCounters
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Services;

namespace RuntimeXNA.OI
{

    public class CDefCounters
    {
      public const short CTA_HIDDEN = 0;
      public const short CTA_DIGITS = 1;
      public const short CTA_VBAR = 2;
      public const short CTA_HBAR = 3;
      public const short CTA_ANIM = 4;
      public const short CTA_TEXT = 5;
      public const short BARFLAG_INVERSE = 256 /*0x0100*/;
      public int odCx;
      public int odCy;
      public short odPlayer;
      public short odDisplayType;
      public short odDisplayFlags;
      public short odFont;
      public short ocBorderSize;
      public int ocBorderColor;
      public short ocShape;
      public short ocFillType;
      public short ocLineFlags;
      public int ocColor1;
      public int ocColor2;
      public int ocGradientFlags;
      public short nFrames;
      public short[] frames;

      public void load(CFile file)
      {
        file.skipBytes(4);
        this.odCx = file.readAInt();
        this.odCy = file.readAInt();
        this.odPlayer = file.readAShort();
        this.odDisplayType = file.readAShort();
        this.odDisplayFlags = file.readAShort();
        this.odFont = file.readAShort();
        switch (this.odDisplayType)
        {
          case 1:
          case 4:
            this.nFrames = file.readAShort();
            this.frames = new short[(int) this.nFrames];
            for (int index = 0; index < (int) this.nFrames; ++index)
              this.frames[index] = file.readAShort();
            break;
          case 2:
          case 3:
          case 5:
            this.ocBorderSize = file.readAShort();
            this.ocBorderColor = file.readAColor();
            this.ocShape = file.readAShort();
            this.ocFillType = file.readAShort();
            if (this.ocShape == (short) 1)
            {
              this.ocLineFlags = file.readAShort();
              break;
            }
            switch (this.ocFillType)
            {
              case 1:
                this.ocColor1 = file.readAColor();
                return;
              case 2:
                this.ocColor1 = file.readAColor();
                this.ocColor2 = file.readAColor();
                this.ocGradientFlags = file.readAInt();
                return;
              case 3:
                return;
              default:
                return;
            }
        }
      }

      public void enumElements(IEnum enumImages, IEnum enumFonts)
      {
        switch (this.odDisplayType)
        {
          case 1:
          case 4:
            for (int index = 0; index < (int) this.nFrames; ++index)
            {
              if (enumImages != null)
              {
                short num = enumImages.enumerate(this.frames[index]);
                if (num != (short) -1)
                  this.frames[index] = num;
              }
            }
            break;
          case 5:
            if (enumFonts == null)
              break;
            short num1 = enumFonts.enumerate(this.odFont);
            if (num1 == (short) -1)
              break;
            this.odFont = num1;
            break;
        }
      }
    }
}
