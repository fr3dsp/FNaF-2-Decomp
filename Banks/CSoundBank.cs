// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Banks.CSoundBank
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.Banks
{

    public class CSoundBank : IEnum
    {
      private CRunApp app;
      private CSound[] sounds;
      private int nHandlesReel;
      private int nHandlesTotal;
      private int nSounds;
      private int[] offsetsToSounds;
      private int[] handleToIndex;
      private int[] useCount;

      public void preLoad(CRunApp a)
      {
        this.app = a;
        this.nHandlesReel = (int) this.app.file.readAShort();
        this.offsetsToSounds = new int[this.nHandlesReel];
        int num = (int) this.app.file.readAShort();
        CSound csound = new CSound(a);
        for (int index = 0; index < num; ++index)
        {
          int filePointer = this.app.file.getFilePointer();
          csound.loadHandle();
          this.offsetsToSounds[(int) csound.handle] = filePointer;
        }
        this.useCount = new int[this.nHandlesReel];
        this.resetToLoad();
        this.handleToIndex = (int[]) null;
        this.nHandlesTotal = this.nHandlesReel;
        this.nSounds = 0;
        this.sounds = (CSound[]) null;
      }

      public CSound getSoundFromHandle(short handle)
      {
        return handle >= (short) 0 && (int) handle < this.nHandlesTotal && this.handleToIndex[(int) handle] != -1 ? this.sounds[this.handleToIndex[(int) handle]] : (CSound) null;
      }

      public CSound getSoundFromIndex(int index)
      {
        return index >= 0 && index < this.nSounds ? this.sounds[index] : (CSound) null;
      }

      public void resetToLoad()
      {
        for (int index = 0; index < this.nHandlesReel; ++index)
          this.useCount[index] = 0;
      }

      public void setToLoad(short handle) => ++this.useCount[(int) handle];

      public short enumerate(short num)
      {
        this.setToLoad(num);
        return -1;
      }

      public void load()
      {
        this.nSounds = 0;
        for (int index = 0; index < this.nHandlesReel; ++index)
        {
          if (this.useCount[index] != 0)
            ++this.nSounds;
        }
        CSound[] csoundArray = new CSound[this.nSounds];
        int index1 = 0;
        for (int index2 = 0; index2 < this.nHandlesReel; ++index2)
        {
          if (this.useCount[index2] != 0)
          {
            if (this.sounds != null && this.handleToIndex[index2] != -1 && this.sounds[this.handleToIndex[index2]] != null)
            {
              csoundArray[index1] = this.sounds[this.handleToIndex[index2]];
              csoundArray[index1].useCount = this.useCount[index2];
            }
            else
            {
              csoundArray[index1] = new CSound(this.app);
              this.app.file.seek(this.offsetsToSounds[index2]);
              csoundArray[index1].load();
              csoundArray[index1].useCount = this.useCount[index2];
            }
            ++index1;
          }
        }
        this.sounds = csoundArray;
        this.handleToIndex = new int[this.nHandlesReel];
        for (int index3 = 0; index3 < this.nHandlesReel; ++index3)
          this.handleToIndex[index3] = -1;
        for (int index4 = 0; index4 < this.nSounds; ++index4)
          this.handleToIndex[(int) this.sounds[index4].handle] = index4;
        this.nHandlesTotal = this.nHandlesReel;
        this.resetToLoad();
      }
    }
}
