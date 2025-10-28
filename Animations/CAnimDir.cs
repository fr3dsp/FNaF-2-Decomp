// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Animations.CAnimDir
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Services;

namespace RuntimeXNA.Animations
{

    public class CAnimDir
    {
      public byte adMinSpeed;
      public byte adMaxSpeed;
      public short adRepeat;
      public short adRepeatFrame;
      public short adNumberOfFrame;
      public short[] adFrames;

      public void load(CFile file)
      {
        this.adMinSpeed = file.readAByte();
        this.adMaxSpeed = file.readAByte();
        this.adRepeat = file.readAShort();
        this.adRepeatFrame = file.readAShort();
        this.adNumberOfFrame = file.readAShort();
        this.adFrames = new short[(int) this.adNumberOfFrame];
        for (int index = 0; index < (int) this.adNumberOfFrame; ++index)
          this.adFrames[index] = file.readAShort();
      }

      public void enumElements(IEnum enumImages)
      {
        for (int index = 0; index < (int) this.adNumberOfFrame; ++index)
        {
          if (enumImages != null)
          {
            short num = enumImages.enumerate(this.adFrames[index]);
            if (num != (short) -1)
              this.adFrames[index] = num;
          }
        }
      }
    }
}
