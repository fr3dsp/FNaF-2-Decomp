// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Banks.CFontBank
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;
using RuntimeXNA.Services;
using System;

namespace RuntimeXNA.Banks
{

    public class CFontBank : IEnum
    {
      private CRunApp app;
      private CFile file;
      public CFont[] fonts;
      private int[] offsetsToFonts;
      private int nFonts;
      private short[] handleToIndex;
      public int nHandlesReel;
      public int nHandlesTotal;
      private short[] useCount;

      public CFontBank()
      {
      }

      public CFontBank(CRunApp a)
      {
        this.app = a;
        this.file = this.app.file;
      }

      public void preLoad()
      {
        int num = (int) this.file.readAShort();
        this.nHandlesReel = (int) this.file.readAShort();
        this.offsetsToFonts = new int[this.nHandlesReel];
        this.file.getFilePointer();
        CFont cfont = new CFont();
        for (int index = 0; index < num; ++index)
        {
          int filePointer = this.file.getFilePointer();
          cfont.loadHandle(this.file);
          this.offsetsToFonts[(int) cfont.handle] = filePointer;
        }
        this.useCount = new short[this.nHandlesReel];
        this.resetToLoad();
        this.handleToIndex = (short[]) null;
        this.nHandlesTotal = this.nHandlesReel;
        this.fonts = (CFont[]) null;
      }

      public void load()
      {
        this.nFonts = 0;
        for (int index = 0; index < this.nHandlesTotal; ++index)
        {
          if (this.useCount[index] != (short) 0)
            ++this.nFonts;
        }
        CFont[] cfontArray = new CFont[this.nFonts];
        int index1 = 0;
        for (int index2 = 0; index2 < this.nHandlesReel; ++index2)
        {
          if (this.useCount[index2] != (short) 0)
          {
            if (this.fonts != null && this.handleToIndex[index2] != (short) -1 && this.fonts[(int) this.handleToIndex[index2]] != null)
            {
              cfontArray[index1] = this.fonts[(int) this.handleToIndex[index2]];
              cfontArray[index1].useCount = this.useCount[index2];
            }
            else
            {
              cfontArray[index1] = new CFont();
              this.file.seek(this.offsetsToFonts[index2]);
              cfontArray[index1].load(this.file, this.app.content);
              cfontArray[index1].useCount = this.useCount[index2];
            }
            ++index1;
          }
        }
        this.fonts = cfontArray;
        this.handleToIndex = new short[this.nHandlesReel];
        for (int index3 = 0; index3 < this.nHandlesReel; ++index3)
          this.handleToIndex[index3] = (short) -1;
        for (int index4 = 0; index4 < this.nFonts; ++index4)
          this.handleToIndex[(int) this.fonts[index4].handle] = (short) index4;
        this.nHandlesTotal = this.nHandlesReel;
        this.resetToLoad();
      }

      public CFont getFontFromHandle(short handle)
      {
        if (handle == (short) -1)
          return this.fonts[0];
        return handle >= (short) 0 && (int) handle < this.nHandlesTotal && this.handleToIndex[(int) handle] != (short) -1 ? this.fonts[(int) this.handleToIndex[(int) handle]] : (CFont) null;
      }

      public CFont getFontFromIndex(short index)
      {
        return index >= (short) 0 && (int) index < this.nFonts ? this.fonts[(int) index] : (CFont) null;
      }

      public CFontInfo getFontInfoFromHandle(short handle)
      {
        return handle < (short) 0 ? this.fonts[0].getFontInfo() : this.getFontFromHandle(handle).getFontInfo();
      }

      public void resetToLoad()
      {
        for (int index = 0; index < this.nHandlesReel; ++index)
          this.useCount[index] = (short) 0;
      }

      public void setToLoad(short handle)
      {
        if (handle == (short) -1)
        {
          CFont[] fonts = this.fonts;
        }
        else
          ++this.useCount[(int) handle];
      }

      public short enumerate(short num)
      {
        this.setToLoad(num);
        return -1;
      }

      public short addFont(CFontInfo info)
      {
        int index1 = 0;
        while (index1 < this.nFonts && (this.fonts[index1] == null || this.fonts[index1].lfHeight != info.lfHeight || this.fonts[index1].lfWeight != info.lfWeight || (int) this.fonts[index1].lfItalic != (int) info.lfItalic || string.Compare(this.fonts[index1].lfFaceName, info.lfFaceName, StringComparison.OrdinalIgnoreCase) != 0))
          ++index1;
        if (index1 < this.nFonts)
          return this.fonts[index1].handle;
        short index2 = -1;
        for (int nHandlesReel = this.nHandlesReel; nHandlesReel < this.nHandlesTotal; ++nHandlesReel)
        {
          if (this.handleToIndex[nHandlesReel] == (short) -1)
          {
            index2 = (short) nHandlesReel;
            break;
          }
        }
        if (index2 == (short) -1)
        {
          short[] numArray = new short[this.nHandlesTotal + 10];
          int index3;
          for (index3 = 0; index3 < this.nHandlesTotal; ++index3)
            numArray[index3] = this.handleToIndex[index3];
          for (; index3 < this.nHandlesTotal + 10; ++index3)
            numArray[index3] = (short) -1;
          index2 = (short) this.nHandlesTotal;
          this.nHandlesTotal += 10;
          this.handleToIndex = numArray;
        }
        int index4 = -1;
        for (int index5 = 0; index5 < this.nFonts; ++index5)
        {
          if (this.fonts[index5] == null)
          {
            index4 = index5;
            break;
          }
        }
        if (index4 == -1)
        {
          CFont[] cfontArray = new CFont[this.nFonts + 10];
          int index6;
          for (index6 = 0; index6 < this.nFonts; ++index6)
            cfontArray[index6] = this.fonts[index6];
          for (; index6 < this.nFonts + 10; ++index6)
            cfontArray[index6] = (CFont) null;
          index4 = this.nFonts;
          this.nFonts += 10;
          this.fonts = cfontArray;
        }
        this.handleToIndex[(int) index2] = (short) index4;
        this.fonts[index4] = new CFont();
        this.fonts[index4].handle = index2;
        this.fonts[index4].lfHeight = info.lfHeight;
        this.fonts[index4].lfWeight = info.lfWeight;
        this.fonts[index4].lfItalic = info.lfItalic;
        this.fonts[index4].lfFaceName = info.lfFaceName;
        return index2;
      }
    }
}
