// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Animations.CAnim
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Services;

namespace RuntimeXNA.Animations
{

    public class CAnim
    {
      public const short ANIMID_STOP = 0;
      public const short ANIMID_WALK = 1;
      public const short ANIMID_RUN = 2;
      public const short ANIMID_APPEAR = 3;
      public const short ANIMID_DISAPPEAR = 4;
      public const short ANIMID_BOUNCE = 5;
      public const short ANIMID_SHOOT = 6;
      public const short ANIMID_JUMP = 7;
      public const short ANIMID_FALL = 8;
      public const short ANIMID_CLIMB = 9;
      public const short ANIMID_CROUCH = 10;
      public const short ANIMID_UNCROUCH = 11;
      public const short ANIMID_USER1 = 12;
      private static byte[] tableAnimTwoSpeeds = new byte[16 /*0x10*/]
      {
        (byte) 0,
        (byte) 1,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 1,
        (byte) 1,
        (byte) 1,
        (byte) 1,
        (byte) 1,
        (byte) 1,
        (byte) 1,
        (byte) 1,
        (byte) 1
      };
      public CAnimDir[] anDirs;
      public byte[] anTrigo;
      public byte[] anAntiTrigo;

      public void load(CFile file)
      {
        int filePointer = file.getFilePointer();
        short[] numArray = new short[32 /*0x20*/];
        for (int index = 0; index < 32 /*0x20*/; ++index)
          numArray[index] = file.readAShort();
        this.anDirs = new CAnimDir[32 /*0x20*/];
        this.anTrigo = new byte[32 /*0x20*/];
        this.anAntiTrigo = new byte[32 /*0x20*/];
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          this.anDirs[index] = (CAnimDir) null;
          this.anTrigo[index] = (byte) 0;
          this.anAntiTrigo[index] = (byte) 0;
          if (numArray[index] != (short) 0)
          {
            this.anDirs[index] = new CAnimDir();
            file.seek(filePointer + (int) numArray[index]);
            this.anDirs[index].load(file);
          }
        }
      }

      public void enumElements(IEnum enumImages)
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.anDirs[index] != null)
            this.anDirs[index].enumElements(enumImages);
        }
      }

      public void approximate(int nAnim)
      {
        for (int index1 = 0; index1 < 32 /*0x20*/; ++index1)
        {
          if (this.anDirs[index1] == null)
          {
            int num1 = 0;
            int index2 = index1 + 1;
            while (num1 < 32 /*0x20*/)
            {
              index2 &= 31 /*0x1F*/;
              if (this.anDirs[index2] != null)
              {
                this.anTrigo[index1] = (byte) index2;
                break;
              }
              ++num1;
              ++index2;
            }
            int num2 = 0;
            int index3 = index1 - 1;
            while (num2 < 32 /*0x20*/)
            {
              index3 &= 31 /*0x1F*/;
              if (this.anDirs[index3] != null)
              {
                this.anAntiTrigo[index1] = (byte) index3;
                break;
              }
              ++num2;
              --index3;
            }
            if (index2 == index3 || num1 < num2)
              this.anTrigo[index1] |= (byte) 64 /*0x40*/;
            else if (num2 < num1)
              this.anAntiTrigo[index1] |= (byte) 64 /*0x40*/;
          }
          else if (nAnim < 16 /*0x10*/ && CAnim.tableAnimTwoSpeeds[nAnim] == (byte) 0)
            this.anDirs[index1].adMinSpeed = this.anDirs[index1].adMaxSpeed;
        }
      }
    }
}
