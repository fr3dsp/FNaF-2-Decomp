// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMove
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    public class CMove
    {
      public const int MVTOPT_8DIR_STICK = 1;
      public static int[] Cosinus32 = new int[32 /*0x20*/]
      {
        256 /*0x0100*/,
        251,
        236,
        212,
        181,
        142,
        97,
        49,
        0,
        -49,
        -97,
        -142,
        -181,
        -212,
        -236,
        -251,
        -256,
        -251,
        -236,
        -212,
        -181,
        -142,
        -97,
        -49,
        0,
        49,
        97,
        142,
        181,
        212,
        236,
        251
      };
      public static int[] Sinus32 = new int[32 /*0x20*/]
      {
        0,
        -49,
        -97,
        -142,
        -181,
        -212,
        -236,
        -251,
        -256,
        -251,
        -236,
        -212,
        -181,
        -142,
        -97,
        -49,
        0,
        49,
        97,
        142,
        181,
        212,
        236,
        251,
        256 /*0x0100*/,
        251,
        236,
        212,
        181,
        142,
        97,
        49
      };
      public static short[] accelerators = new short[101]
      {
        (short) 2,
        (short) 3,
        (short) 4,
        (short) 6,
        (short) 8,
        (short) 10,
        (short) 12,
        (short) 16 /*0x10*/,
        (short) 20,
        (short) 24,
        (short) 48 /*0x30*/,
        (short) 56,
        (short) 64 /*0x40*/,
        (short) 72,
        (short) 80 /*0x50*/,
        (short) 88,
        (short) 96 /*0x60*/,
        (short) 104,
        (short) 112 /*0x70*/,
        (short) 120,
        (short) 144 /*0x90*/,
        (short) 160 /*0xA0*/,
        (short) 176 /*0xB0*/,
        (short) 192 /*0xC0*/,
        (short) 208 /*0xD0*/,
        (short) 224 /*0xE0*/,
        (short) 240 /*0xF0*/,
        (short) 256 /*0x0100*/,
        (short) 272,
        (short) 288,
        (short) 320,
        (short) 336,
        (short) 352,
        (short) 368,
        (short) 384,
        (short) 400,
        (short) 416,
        (short) 432,
        (short) 448,
        (short) 480,
        (short) 512 /*0x0200*/,
        (short) 544,
        (short) 560,
        (short) 592,
        (short) 624,
        (short) 640,
        (short) 672,
        (short) 688,
        (short) 720,
        (short) 736,
        (short) 768 /*0x0300*/,
        (short) 784,
        (short) 816,
        (short) 848,
        (short) 864,
        (short) 896,
        (short) 928,
        (short) 944,
        (short) 976,
        (short) 992,
        (short) 1024 /*0x0400*/,
        (short) 1120,
        (short) 1216,
        (short) 1312,
        (short) 1440,
        (short) 1536 /*0x0600*/,
        (short) 1632,
        (short) 1728,
        (short) 1824,
        (short) 1952,
        (short) 2048 /*0x0800*/,
        (short) 2240,
        (short) 2432,
        (short) 2688,
        (short) 2880,
        (short) 3072 /*0x0C00*/,
        (short) 3264,
        (short) 3456,
        (short) 3712,
        (short) 3904,
        (short) 4096 /*0x1000*/,
        (short) 6544,
        (short) 4914,
        (short) 5216,
        (short) 5732,
        (short) 6144,
        (short) 6553,
        (short) 6962,
        (short) 7366,
        (short) 7780,
        (short) 8192 /*0x2000*/,
        (short) 9836,
        (short) 11672,
        (short) 13316,
        (short) 14960,
        (short) 16604,
        (short) 18248,
        (short) 19892,
        (short) 21504,
        (short) 25600,
        (short) 25600
      };
      public static sbyte[] Joy2Dir = new sbyte[16 /*0x10*/]
      {
        (sbyte) -1,
        (sbyte) 8,
        (sbyte) 24,
        (sbyte) -1,
        (sbyte) 16 /*0x10*/,
        (sbyte) 12,
        (sbyte) 20,
        (sbyte) 16 /*0x10*/,
        (sbyte) 0,
        (sbyte) 4,
        (sbyte) 28,
        (sbyte) 0,
        (sbyte) -1,
        (sbyte) 8,
        (sbyte) 24,
        (sbyte) -1
      };
      public static int[] CosSurSin32 = new int[18]
      {
        2599,
        0,
        844,
        31 /*0x1F*/,
        479,
        30,
        312,
        29,
        210,
        28,
        137,
        27,
        78,
        26,
        25,
        25,
        0,
        24
      };
      public static int[] mvap_TableDirs = new int[144 /*0x90*/]
      {
        0,
        -2,
        0,
        2,
        0,
        -4,
        0,
        4,
        0,
        -8,
        0,
        8,
        -4,
        0,
        -8,
        0,
        0,
        0,
        -2,
        -2,
        2,
        2,
        -4,
        -4,
        4,
        4,
        -8,
        -8,
        8,
        8,
        -4,
        4,
        -8,
        8,
        0,
        0,
        -2,
        0,
        2,
        0,
        -4,
        0,
        4,
        0,
        -8,
        0,
        8,
        0,
        0,
        4,
        0,
        8,
        0,
        0,
        -2,
        2,
        2,
        -2,
        -4,
        4,
        4,
        -4,
        -8,
        8,
        8,
        -8,
        4,
        4,
        8,
        8,
        0,
        0,
        0,
        2,
        0,
        -2,
        0,
        4,
        0,
        -4,
        0,
        8,
        0,
        -8,
        4,
        0,
        8,
        0,
        0,
        0,
        2,
        2,
        -2,
        -2,
        4,
        4,
        -4,
        -4,
        8,
        8,
        -8,
        -8,
        4,
        -4,
        8,
        -8,
        0,
        0,
        2,
        0,
        -2,
        0,
        4,
        0,
        -4,
        0,
        8,
        0,
        -8,
        0,
        0,
        -4,
        0,
        -8,
        0,
        0,
        2,
        -2,
        -2,
        2,
        4,
        -4,
        -4,
        4,
        8,
        -8,
        -8,
        8,
        -4,
        -4,
        -8,
        -8,
        0,
        0
      };
      public CObject hoPtr;
      public int rmAcc;
      public int rmDec;
      public short rmCollisionCount;
      public int rmStopSpeed;
      public int rmAccValue;
      public int rmDecValue;
      public byte rmOpt;

      public bool newMake_Move(int speed, int angle)
      {
        ++this.hoPtr.hoAdRunHeader.rh3CollisionCount;
        this.rmCollisionCount = this.hoPtr.hoAdRunHeader.rh3CollisionCount;
        this.hoPtr.rom.rmMoveFlag = false;
        if (speed == 0)
        {
          this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr);
          return false;
        }
        int num1;
        for (num1 = (this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) == 0 ? speed << 5 : (int) ((double) speed * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef * 32.0); num1 > 2048 /*0x0800*/; num1 -= 2048 /*0x0800*/)
        {
          int num2 = this.hoPtr.hoX << 16 /*0x10*/ | this.hoPtr.hoCalculX & (int) ushort.MaxValue;
          int num3 = this.hoPtr.hoY << 16 /*0x10*/ | this.hoPtr.hoCalculY & (int) ushort.MaxValue;
          int num4 = num2 + CMove.Cosinus32[angle] * 2048 /*0x0800*/;
          int num5 = num3 + CMove.Sinus32[angle] * 2048 /*0x0800*/;
          this.hoPtr.hoCalculX = num4 & (int) ushort.MaxValue;
          this.hoPtr.hoX = (int) (short) (num4 >> 16 /*0x10*/);
          this.hoPtr.hoCalculY = num5 & (int) ushort.MaxValue;
          this.hoPtr.hoY = (int) (short) (num5 >> 16 /*0x10*/);
          if (this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr))
            return true;
          if (this.hoPtr.rom.rmMoveFlag)
            break;
        }
        if (!this.hoPtr.rom.rmMoveFlag)
        {
          int num6 = this.hoPtr.hoX << 16 /*0x10*/ | this.hoPtr.hoCalculX & (int) ushort.MaxValue;
          int num7 = this.hoPtr.hoY << 16 /*0x10*/ | this.hoPtr.hoCalculY & (int) ushort.MaxValue;
          int num8 = num6 + CMove.Cosinus32[angle] * num1;
          int num9 = num7 + CMove.Sinus32[angle] * num1;
          this.hoPtr.hoCalculX = num8 & (int) ushort.MaxValue;
          this.hoPtr.hoX = (int) (short) (num8 >> 16 /*0x10*/);
          this.hoPtr.hoCalculY = num9 & (int) ushort.MaxValue;
          this.hoPtr.hoY = (int) (short) (num9 >> 16 /*0x10*/);
          if (this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr))
            return true;
        }
        this.hoPtr.roc.rcChanged = true;
        if (!this.hoPtr.rom.rmMoveFlag)
          this.hoPtr.hoAdRunHeader.rhVBLObjet = 0;
        return this.hoPtr.rom.rmMoveFlag;
      }

      public void moveAtStart(CMoveDef mvPtr)
      {
        if (mvPtr.mvMoveAtStart != (byte) 0)
          return;
        this.stop();
      }

      public int getAccelerator(int acceleration)
      {
        return acceleration <= 100 ? (int) CMove.accelerators[acceleration] : acceleration << 8;
      }

      public void mv_Approach(bool bStickToObject)
      {
        if (bStickToObject)
        {
          this.mb_Approach(false);
        }
        else
        {
          bool flag = false;
          switch (this.hoPtr.hoAdRunHeader.rhEvtProg.rhCurCode >> 16 /*0x10*/)
          {
            case -14:
            case -13:
              int index = (this.hoPtr.roc.rcDir >> 2) * 18;
              while (!this.tst_Position(this.hoPtr.hoX + CMove.mvap_TableDirs[index], this.hoPtr.hoY + CMove.mvap_TableDirs[index + 1], flag))
              {
                index += 2;
                if (CMove.mvap_TableDirs[index] == 0 && CMove.mvap_TableDirs[index + 1] == 0)
                {
                  if (flag)
                    return;
                  this.hoPtr.hoX = this.hoPtr.roc.rcOldX;
                  this.hoPtr.hoY = this.hoPtr.roc.rcOldY;
                  this.hoPtr.roc.rcImage = this.hoPtr.roc.rcOldImage;
                  this.hoPtr.roc.rcAngle = this.hoPtr.roc.rcOldAngle;
                  return;
                }
              }
              this.hoPtr.hoX += CMove.mvap_TableDirs[index];
              this.hoPtr.hoY += CMove.mvap_TableDirs[index + 1];
              break;
            case -12:
              int x1 = this.hoPtr.hoX - this.hoPtr.hoImgXSpot;
              int y1 = this.hoPtr.hoY - this.hoPtr.hoImgYSpot;
              int num1 = this.hoPtr.hoAdRunHeader.quadran_Out(x1, y1, x1 + this.hoPtr.hoImgWidth, y1 + this.hoPtr.hoImgHeight);
              int num2 = this.hoPtr.hoX;
              int num3 = this.hoPtr.hoY;
              if ((num1 & 1) != 0)
                num2 = this.hoPtr.hoImgXSpot;
              if ((num1 & 2) != 0)
                num2 = this.hoPtr.hoAdRunHeader.rhLevelSx - this.hoPtr.hoImgWidth + this.hoPtr.hoImgXSpot;
              if ((num1 & 4) != 0)
                num3 = this.hoPtr.hoImgYSpot;
              if ((num1 & 8) != 0)
                num3 = this.hoPtr.hoAdRunHeader.rhLevelSy - this.hoPtr.hoImgHeight + this.hoPtr.hoImgYSpot;
              this.hoPtr.hoX = num2;
              this.hoPtr.hoY = num3;
              break;
          }
        }
      }

      public void mb_Approach(bool flag)
      {
        switch (this.hoPtr.hoAdRunHeader.rhEvtProg.rhCurCode >> 16 /*0x10*/)
        {
          case -14:
          case -13:
            CPoint ptFinal = new CPoint();
            if (this.mbApproachSprite(this.hoPtr.hoX, this.hoPtr.hoY, this.hoPtr.roc.rcOldX, this.hoPtr.roc.rcOldY, flag, ptFinal))
            {
              this.hoPtr.hoX = ptFinal.x;
              this.hoPtr.hoY = ptFinal.y;
              break;
            }
            int index = (this.hoPtr.roc.rcDir >> 2) * 18;
            while (!this.tst_Position(this.hoPtr.hoX + CMove.mvap_TableDirs[index], this.hoPtr.hoY + CMove.mvap_TableDirs[index + 1], flag))
            {
              index += 2;
              if (CMove.mvap_TableDirs[index] == 0 && CMove.mvap_TableDirs[index + 1] == 0)
              {
                if (flag)
                  return;
                this.hoPtr.hoX = this.hoPtr.roc.rcOldX;
                this.hoPtr.hoY = this.hoPtr.roc.rcOldY;
                this.hoPtr.roc.rcImage = this.hoPtr.roc.rcOldImage;
                this.hoPtr.roc.rcAngle = this.hoPtr.roc.rcOldAngle;
                return;
              }
            }
            this.hoPtr.hoX += CMove.mvap_TableDirs[index];
            this.hoPtr.hoY += CMove.mvap_TableDirs[index + 1];
            break;
          case -12:
            int x1 = this.hoPtr.hoX - this.hoPtr.hoImgXSpot;
            int y1 = this.hoPtr.hoY - this.hoPtr.hoImgYSpot;
            int num1 = this.hoPtr.hoAdRunHeader.quadran_Out(x1, y1, x1 + this.hoPtr.hoImgWidth, y1 + this.hoPtr.hoImgHeight);
            int num2 = this.hoPtr.hoX;
            int num3 = this.hoPtr.hoY;
            if ((num1 & 1) != 0)
              num2 = this.hoPtr.hoImgXSpot;
            if ((num1 & 2) != 0)
              num2 = this.hoPtr.hoAdRunHeader.rhLevelSx - this.hoPtr.hoImgWidth + this.hoPtr.hoImgXSpot;
            if ((num1 & 4) != 0)
              num3 = this.hoPtr.hoImgYSpot;
            if ((num1 & 8) != 0)
              num3 = this.hoPtr.hoAdRunHeader.rhLevelSy - this.hoPtr.hoImgHeight + this.hoPtr.hoImgYSpot;
            this.hoPtr.hoX = num2;
            this.hoPtr.hoY = num3;
            break;
        }
      }

      public bool tst_SpritePosition(int x, int y, short htFoot, short planCol, bool flag)
      {
        short num = -1;
        if (flag)
          num = this.hoPtr.hoOi;
        CObjInfo hoOiList = this.hoPtr.hoOiList;
        if (((int) hoOiList.oilLimitFlags & 15) != 0)
        {
          int x1 = x - this.hoPtr.hoImgXSpot;
          int y1 = y - this.hoPtr.hoImgYSpot;
          if ((this.hoPtr.hoAdRunHeader.quadran_Out(x1, y1, x1 + this.hoPtr.hoImgWidth, y1 + this.hoPtr.hoImgHeight) & (int) hoOiList.oilLimitFlags) != 0)
            return false;
        }
        if (((int) hoOiList.oilLimitFlags & 16 /*0x10*/) != 0 && this.hoPtr.hoAdRunHeader.colMask_TestObject_IXY(this.hoPtr, this.hoPtr.roc.rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY, x, y, (int) htFoot, (int) planCol) != 0)
          return false;
        if (hoOiList.oilLimitList == -1)
          return true;
        CArrayList carrayList = this.hoPtr.hoAdRunHeader.objectAllCol_IXY(this.hoPtr, this.hoPtr.roc.rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY, x, y, hoOiList.oilColList);
        if (carrayList == null)
          return true;
        short[] limitBuffer = this.hoPtr.hoAdRunHeader.rhEvtProg.limitBuffer;
        for (int index = 0; index < carrayList.size(); ++index)
        {
          short hoOi = ((CObject) carrayList.get(index)).hoOi;
          if ((int) hoOi != (int) num)
          {
            for (int oilLimitList = hoOiList.oilLimitList; limitBuffer[oilLimitList] >= (short) 0; ++oilLimitList)
            {
              if ((int) limitBuffer[oilLimitList] == (int) hoOi)
                return false;
            }
          }
        }
        return true;
      }

      public bool tst_Position(int x, int y, bool flag)
      {
        short num = -1;
        if (flag)
          num = this.hoPtr.hoOi;
        CObjInfo hoOiList = this.hoPtr.hoOiList;
        if (((int) hoOiList.oilLimitFlags & 15) != 0)
        {
          int x1 = x - this.hoPtr.hoImgXSpot;
          int y1 = y - this.hoPtr.hoImgYSpot;
          if ((this.hoPtr.hoAdRunHeader.quadran_Out(x1, y1, x1 + this.hoPtr.hoImgWidth, y1 + this.hoPtr.hoImgHeight) & (int) hoOiList.oilLimitFlags) != 0)
            return false;
        }
        if (((int) hoOiList.oilLimitFlags & 16 /*0x10*/) != 0 && this.hoPtr.hoAdRunHeader.colMask_TestObject_IXY(this.hoPtr, this.hoPtr.roc.rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY, x, y, 0, 1) != 0)
          return false;
        if (hoOiList.oilLimitList == -1)
          return true;
        CArrayList carrayList = this.hoPtr.hoAdRunHeader.objectAllCol_IXY(this.hoPtr, this.hoPtr.roc.rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY, x, y, hoOiList.oilColList);
        if (carrayList == null)
          return true;
        short[] limitBuffer = this.hoPtr.hoAdRunHeader.rhEvtProg.limitBuffer;
        for (int index = 0; index < carrayList.size(); ++index)
        {
          short hoOi = ((CObject) carrayList.get(index)).hoOi;
          if ((int) hoOi != (int) num)
          {
            for (int oilLimitList = hoOiList.oilLimitList; limitBuffer[oilLimitList] >= (short) 0; ++oilLimitList)
            {
              if ((int) limitBuffer[oilLimitList] == (int) hoOi)
                return false;
            }
          }
        }
        return true;
      }

      public bool mpApproachSprite(
        int destX,
        int destY,
        int maxX,
        int maxY,
        short htFoot,
        short planCol,
        CPoint ptFinal)
      {
        int num1 = destX;
        int num2 = destY;
        int num3 = maxX;
        int num4 = maxY;
        int num5 = (num1 + num3) / 2;
        int num6 = (num2 + num4) / 2;
        int num7;
        int num8;
        do
        {
          while (!this.tst_SpritePosition(num5 + this.hoPtr.hoAdRunHeader.rhWindowX, num6 + this.hoPtr.hoAdRunHeader.rhWindowY, htFoot, planCol, false))
          {
            num1 = num5;
            num2 = num6;
            int num9 = num5;
            int num10 = num6;
            num5 = (num3 + num1) / 2;
            num6 = (num4 + num2) / 2;
            if (num5 == num9 && num6 == num10)
            {
              if ((num3 != num1 || num4 != num2) && this.tst_SpritePosition(num3 + this.hoPtr.hoAdRunHeader.rhWindowX, num4 + this.hoPtr.hoAdRunHeader.rhWindowY, htFoot, planCol, false))
              {
                ptFinal.x = num3;
                ptFinal.y = num4;
                return true;
              }
              ptFinal.x = num5;
              ptFinal.y = num6;
              return false;
            }
          }
          num3 = num5;
          num4 = num6;
          num7 = num5;
          num8 = num6;
          num5 = (num3 + num1) / 2;
          num6 = (num4 + num2) / 2;
        }
        while (num5 != num7 || num6 != num8);
        if ((num3 != num1 || num4 != num2) && this.tst_SpritePosition(num1 + this.hoPtr.hoAdRunHeader.rhWindowX, num2 + this.hoPtr.hoAdRunHeader.rhWindowY, htFoot, planCol, false))
        {
          num5 = num1;
          num6 = num2;
        }
        ptFinal.x = num5;
        ptFinal.y = num6;
        return true;
      }

      private bool mbApproachSprite(
        int destX,
        int destY,
        int maxX,
        int maxY,
        bool flag,
        CPoint ptFinal)
      {
        int x1 = destX;
        int y1 = destY;
        int x2 = maxX;
        int y2 = maxY;
        int x3 = (x1 + x2) / 2;
        int y3 = (y1 + y2) / 2;
        int num1;
        int num2;
        do
        {
          while (!this.tst_Position(x3, y3, flag))
          {
            x1 = x3;
            y1 = y3;
            int num3 = x3;
            int num4 = y3;
            x3 = (x2 + x1) / 2;
            y3 = (y2 + y1) / 2;
            if (x3 == num3 && y3 == num4)
            {
              if ((x2 != x1 || y2 != y1) && this.tst_Position(x2, y2, flag))
              {
                ptFinal.x = x2;
                ptFinal.y = y2;
                return true;
              }
              ptFinal.x = x3;
              ptFinal.y = y3;
              return false;
            }
          }
          x2 = x3;
          y2 = y3;
          num1 = x3;
          num2 = y3;
          x3 = (x2 + x1) / 2;
          y3 = (y2 + y1) / 2;
        }
        while (x3 != num1 || y3 != num2);
        if ((x2 != x1 || y2 != y1) && this.tst_Position(x1, y1, flag))
        {
          x3 = x1;
          y3 = y1;
        }
        ptFinal.x = x3;
        ptFinal.y = y3;
        return true;
      }

      public static int getDeltaX(int pente, int angle)
      {
        return pente * CMove.Cosinus32[angle] / 256 /*0x0100*/;
      }

      public static int getDeltaY(int pente, int angle)
      {
        return pente * CMove.Sinus32[angle] / 256 /*0x0100*/;
      }

      public void setAcc(int acc)
      {
        if (acc > 250)
          acc = 250;
        if (acc < 0)
          acc = 0;
        this.rmAcc = acc;
        this.rmAccValue = this.getAccelerator(acc);
        if (this.hoPtr.roc.rcMovementType != 14)
          return;
        ((CMoveExtension) this).movement.setAcc(acc);
      }

      public void setDec(int dec)
      {
        if (dec > 250)
          dec = 250;
        if (dec < 0)
          dec = 0;
        this.rmDec = dec;
        this.rmDecValue = this.getAccelerator(dec);
        if (this.hoPtr.roc.rcMovementType != 14)
          return;
        ((CMoveExtension) this).movement.setDec(dec);
      }

      public void setRotSpeed(int speed)
      {
        if (speed > 250)
          speed = 250;
        if (speed < 0)
          speed = 0;
        if (this.hoPtr.roc.rcMovementType == 2)
          ((CMoveRace) this).MRSetRotSpeed(speed);
        if (this.hoPtr.roc.rcMovementType != 14)
          return;
        ((CMoveExtension) this).movement.setRotSpeed(speed);
      }

      public void set8Dirs(int dirs)
      {
        if (this.hoPtr.roc.rcMovementType == 3)
          ((CMoveGeneric) this).set8Dir(dirs);
        if (this.hoPtr.roc.rcMovementType != 14)
          return;
        ((CMoveExtension) this).movement.set8Dirs(dirs);
      }

      public void setGravity(int gravity)
      {
        if (gravity > 250)
          gravity = 250;
        if (gravity < 0)
          gravity = 0;
        if (this.hoPtr.roc.rcMovementType == 9)
          ((CMovePlatform) this).MPSetGravity(gravity);
        if (this.hoPtr.roc.rcMovementType != 14)
          return;
        ((CMoveExtension) this).movement.setGravity(gravity);
      }

      public int getSpeed()
      {
        return this.hoPtr.roc.rcMovementType == 14 ? ((CMoveExtension) this).movement.getSpeed() : this.hoPtr.roc.rcSpeed;
      }

      public int getAcc()
      {
        return this.hoPtr.roc.rcMovementType == 14 ? ((CMoveExtension) this).movement.getAcceleration() : this.rmAcc;
      }

      public int getDec()
      {
        return this.hoPtr.roc.rcMovementType == 14 ? ((CMoveExtension) this).movement.getDeceleration() : this.rmDec;
      }

      public int getGravity()
      {
        if (this.hoPtr.roc.rcMovementType == 9)
          return ((CMovePlatform) this).MP_Gravity;
        return this.hoPtr.roc.rcMovementType == 14 ? ((CMoveExtension) this).movement.getGravity() : 0;
      }

      public virtual void init(CObject hoPtr, CMoveDef mvPtr)
      {
      }

      public virtual void kill()
      {
      }

      public virtual void move()
      {
      }

      public virtual void stop()
      {
      }

      public virtual void start()
      {
      }

      public virtual void bounce()
      {
      }

      public virtual void reverse()
      {
      }

      public virtual void setXPosition(int x)
      {
      }

      public virtual void setYPosition(int u)
      {
      }

      public virtual void setSpeed(int speed)
      {
      }

      public virtual void setMaxSpeed(int speed)
      {
      }

      public virtual void setDir(int dir)
      {
      }
    }
}
