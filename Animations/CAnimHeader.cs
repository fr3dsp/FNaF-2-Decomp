// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Animations.CAnimHeader
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Services;

namespace RuntimeXNA.Animations
{

    public class CAnimHeader
    {
      private static short[] tableApprox = new short[64 /*0x40*/]
      {
        (short) 3,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 2,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 1,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0
      };
      public short ahAnimMax;
      public CAnim[] ahAnims;
      public byte[] ahAnimExists;

      public void load(CFile file)
      {
        int filePointer = file.getFilePointer();
        file.skipBytes(2);
        this.ahAnimMax = file.readAShort();
        short[] numArray = new short[(int) this.ahAnimMax];
        for (int index = 0; index < (int) this.ahAnimMax; ++index)
          numArray[index] = file.readAShort();
        this.ahAnims = new CAnim[(int) this.ahAnimMax];
        this.ahAnimExists = new byte[(int) this.ahAnimMax];
        for (int index = 0; index < (int) this.ahAnimMax; ++index)
        {
          this.ahAnims[index] = (CAnim) null;
          this.ahAnimExists[index] = (byte) 0;
          if (numArray[index] != (short) 0)
          {
            this.ahAnims[index] = new CAnim();
            file.seek(filePointer + (int) numArray[index]);
            this.ahAnims[index].load(file);
            this.ahAnimExists[index] = (byte) 1;
          }
        }
        for (int nAnim = 0; nAnim < (int) this.ahAnimMax; ++nAnim)
        {
          if (this.ahAnimExists[nAnim] == (byte) 0)
          {
            bool flag = false;
            if (nAnim < 12)
            {
              for (int index = 0; index < 4; ++index)
              {
                if (this.ahAnimExists[(int) CAnimHeader.tableApprox[nAnim * 4 + index]] != (byte) 0)
                {
                  this.ahAnims[nAnim] = this.ahAnims[(int) CAnimHeader.tableApprox[nAnim * 4 + index]];
                  flag = true;
                  break;
                }
              }
            }
            if (!flag)
            {
              for (int index = 0; index < (int) this.ahAnimMax; ++index)
              {
                if (this.ahAnimExists[index] != (byte) 0)
                {
                  this.ahAnims[nAnim] = this.ahAnims[index];
                  break;
                }
              }
            }
          }
          else
            this.ahAnims[nAnim].approximate(nAnim);
        }
      }

      public void enumElements(IEnum enumImages)
      {
        for (int index = 0; index < (int) this.ahAnimMax; ++index)
        {
          if (this.ahAnimExists[index] != (byte) 0)
            this.ahAnims[index].enumElements(enumImages);
        }
      }
    }
}
