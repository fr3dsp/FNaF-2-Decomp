// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.OI.COIList
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;
using RuntimeXNA.Banks;
using RuntimeXNA.Services;

namespace RuntimeXNA.OI
{

    public class COIList
    {
      public short oiMaxIndex;
      public COI[] ois;
      public short oiMaxHandle;
      public short[] oiHandleToIndex;
      public byte[] oiToLoad;
      public byte[] oiLoaded;
      private int currentOI;

      public void preLoad(CFile file)
      {
        this.oiMaxIndex = (short) file.readAInt();
        this.ois = new COI[(int) this.oiMaxIndex];
        this.oiMaxHandle = (short) 0;
        for (int index = 0; index < (int) this.oiMaxIndex; ++index)
        {
          CChunk cchunk = new CChunk();
          while (cchunk.chID != (short) 32639)
          {
            int num = (int) cchunk.readHeader(file);
            if (cchunk.chSize != 0)
            {
              int pos = file.getFilePointer() + cchunk.chSize;
              switch (cchunk.chID)
              {
                case 17476 /*0x4444*/:
                  this.ois[index] = new COI();
                  this.ois[index].loadHeader(file);
                  if ((int) this.ois[index].oiHandle >= (int) this.oiMaxHandle)
                  {
                    this.oiMaxHandle = (short) ((int) this.ois[index].oiHandle + 1);
                    break;
                  }
                  break;
                case 17477:
                  this.ois[index].oiName = file.readAString();
                  break;
                case 17478:
                  this.ois[index].oiFileOffset = file.getFilePointer();
                  break;
              }
              file.seek(pos);
            }
          }
        }
        this.oiHandleToIndex = new short[(int) this.oiMaxHandle];
        for (int index = 0; index < (int) this.oiMaxIndex; ++index)
          this.oiHandleToIndex[(int) this.ois[index].oiHandle] = (short) index;
        this.oiToLoad = new byte[(int) this.oiMaxHandle];
        this.oiLoaded = new byte[(int) this.oiMaxHandle];
        for (int index = 0; index < (int) this.oiMaxHandle; ++index)
        {
          this.oiToLoad[index] = (byte) 0;
          this.oiLoaded[index] = (byte) 0;
        }
      }

      public COI getOIFromHandle(short handle) => this.ois[(int) this.oiHandleToIndex[(int) handle]];

      public COI getOIFromIndex(short index) => this.ois[(int) index];

      public void resetOICurrent()
      {
        for (int index = 0; index < (int) this.oiMaxIndex; ++index)
          this.ois[index].oiFlags &= (short) -17;
      }

      public void setOICurrent(int handle)
      {
        this.ois[(int) this.oiHandleToIndex[handle]].oiFlags |= (short) 16 /*0x10*/;
      }

      public COI getFirstOI()
      {
        for (int index = 0; index < (int) this.oiMaxIndex; ++index)
        {
          if (((int) this.ois[index].oiFlags & 16 /*0x10*/) != 0)
          {
            this.currentOI = index;
            return this.ois[index];
          }
        }
        return (COI) null;
      }

      public COI getNextOI()
      {
        if (this.currentOI < (int) this.oiMaxIndex)
        {
          for (int index = this.currentOI + 1; index < (int) this.oiMaxIndex; ++index)
          {
            if (((int) this.ois[index].oiFlags & 16 /*0x10*/) != 0)
            {
              this.currentOI = index;
              return this.ois[index];
            }
          }
        }
        return (COI) null;
      }

      public void resetToLoad()
      {
        for (int index = 0; index < (int) this.oiMaxHandle; ++index)
          this.oiToLoad[index] = (byte) 0;
      }

      public void setToLoad(int n) => this.oiToLoad[n] = (byte) 1;

      public void load(CFile file, CRunApp app)
      {
        for (int index = 0; index < (int) this.oiMaxHandle; ++index)
        {
          if (this.oiToLoad[index] != (byte) 0)
          {
            if (this.oiLoaded[index] == (byte) 0 || this.oiLoaded[index] != (byte) 0 && (this.ois[(int) this.oiHandleToIndex[index]].oiLoadFlags & 32 /*0x20*/) != 0)
            {
              this.ois[(int) this.oiHandleToIndex[index]].load(file, app);
              this.oiLoaded[index] = (byte) 1;
            }
          }
          else if (this.oiLoaded[index] != (byte) 0)
          {
            this.ois[(int) this.oiHandleToIndex[index]].unLoad();
            this.oiLoaded[index] = (byte) 0;
          }
        }
        this.resetToLoad();
      }

      public void enumElements(IEnum enumImages, IEnum enumFonts)
      {
        for (int index = 0; index < (int) this.oiMaxHandle; ++index)
        {
          if (this.oiLoaded[index] != (byte) 0)
            this.ois[(int) this.oiHandleToIndex[index]].enumElements(enumImages, enumFonts);
        }
      }
    }
}
