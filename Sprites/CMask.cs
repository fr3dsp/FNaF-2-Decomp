// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Sprites.CMask
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RuntimeXNA.Banks;
using System;

namespace RuntimeXNA.Sprites
{

    public class CMask
    {
      public const int SCMF_FULL = 0;
      public const int SCMF_PLATFORM = 1;
      public const int GCMF_OBSTACLE = 0;
      public const int GCMF_PLATFORM = 1;
      public ushort[] mask;
      public int lineWidth;
      public int height;
      public int width;
      public int xSpot;
      public int ySpot;
      private static ushort[] lMask = new ushort[16 /*0x10*/]
      {
        ushort.MaxValue,
        (ushort) short.MaxValue,
        (ushort) 16383 /*0x3FFF*/,
        (ushort) 8191 /*0x1FFF*/,
        (ushort) 4095 /*0x0FFF*/,
        (ushort) 2047 /*0x07FF*/,
        (ushort) 1023 /*0x03FF*/,
        (ushort) 511 /*0x01FF*/,
        (ushort) byte.MaxValue,
        (ushort) sbyte.MaxValue,
        (ushort) 63 /*0x3F*/,
        (ushort) 31 /*0x1F*/,
        (ushort) 15,
        (ushort) 7,
        (ushort) 3,
        (ushort) 1
      };
      private static ushort[] rMask = new ushort[17]
      {
        (ushort) 0,
        (ushort) 32768 /*0x8000*/,
        (ushort) 49152 /*0xC000*/,
        (ushort) 57344 /*0xE000*/,
        (ushort) 61440 /*0xF000*/,
        (ushort) 63488,
        (ushort) 64512,
        (ushort) 65024,
        (ushort) 65280,
        (ushort) 65408,
        (ushort) 65472,
        (ushort) 65504,
        (ushort) 65520,
        (ushort) 65528,
        (ushort) 65532,
        (ushort) 65534,
        ushort.MaxValue
      };

      public void createMask(CImage image, int nFlags)
      {
        this.width = (int) image.width;
        this.height = (int) image.height;
        this.xSpot = (int) image.xSpot;
        this.ySpot = (int) image.ySpot;
        int[] data = new int[this.width * this.height];
        Texture2D texture2D = image.image;
        Rectangle? rect = new Rectangle?();
        if (image.mosaic != (short) 0)
        {
          texture2D = image.app.imageBank.mosaics[(int) image.mosaic];
          rect = new Rectangle?(image.mosaicRectangle);
        }
        texture2D.GetData<int>(0, rect, data, 0, this.width * this.height);
        int num1 = (int) (((long) (this.width + 15) & 4294967280L) / 16L /*0x10*/);
        this.mask = new ushort[num1 * this.height + 1];
        this.lineWidth = num1;
        for (int index = 0; index < num1 * this.height + 1; ++index)
          this.mask[index] = (ushort) 0;
        if ((nFlags & 1) == 0)
        {
          for (int index1 = 0; index1 < this.height; ++index1)
          {
            for (int index2 = 0; index2 < this.width; ++index2)
            {
              int index3 = (int) ((long) (index1 * num1) + ((long) index2 & 4294967280L) / 16L /*0x10*/);
              if (((long) data[index1 * this.width + index2] & 4278190080L /*0xFF000000*/) != 0L)
              {
                ushort num2 = (ushort) (32768 /*0x8000*/ >> index2 % 16 /*0x10*/);
                this.mask[index3] |= num2;
              }
            }
          }
        }
        else
        {
          for (int index = 0; index < this.width; ++index)
          {
            int num3 = 0;
            while (num3 < this.height && ((long) data[num3 * this.width + index] & 4278190080L /*0xFF000000*/) == 0L)
              ++num3;
            if (num3 < this.height)
            {
              int num4 = Math.Min(this.height, num3 + 6);
              ushort num5 = (ushort) (32768 /*0x8000*/ >> (index & 15));
              for (; num3 < num4; ++num3)
              {
                if (((long) data[num3 * this.width + index] & 4278190080L /*0xFF000000*/) != 0L)
                  this.mask[num3 * num1 + index / 16 /*0x10*/] |= num5;
              }
            }
          }
        }
      }

      private void rotateRect(
        ref int pWidth,
        ref int pHeight,
        ref int pHX,
        ref int pHY,
        double fAngle)
      {
        double num1;
        double num2;
        if (fAngle == 90.0)
        {
          num1 = 0.0;
          num2 = 1.0;
        }
        else if (fAngle == 180.0)
        {
          num1 = -1.0;
          num2 = 0.0;
        }
        else if (fAngle == 270.0)
        {
          num1 = 0.0;
          num2 = -1.0;
        }
        else
        {
          double num3 = fAngle * 0.017453292;
          num1 = Math.Cos(num3);
          num2 = Math.Sin(num3);
        }
        double num4 = (double) -pHX * num1;
        double num5 = (double) -pHX * num2;
        double num6 = (double) -pHY * num1;
        double num7 = (double) -pHY * num2;
        int val1_1 = (int) (num4 + num7);
        int val1_2 = (int) (num6 - num5);
        double num8 = (double) (pWidth - pHX);
        double num9 = num8 * num1;
        double num10 = num8 * num2;
        int val1_3 = (int) (num9 + num7);
        int val1_4 = (int) (num6 - num10);
        double num11 = (double) (pHeight - pHY);
        double num12 = num11 * num1;
        double num13 = num11 * num2;
        int val1_5 = (int) (num9 + num13);
        int val1_6 = (int) (num12 - num10);
        int val2_1 = val1_1 + val1_5 - val1_3;
        int val2_2 = val1_2 + val1_6 - val1_4;
        int num14 = Math.Min(val1_1, Math.Min(val1_3, Math.Min(val1_5, val2_1)));
        int num15 = Math.Min(val1_2, Math.Min(val1_4, Math.Min(val1_6, val2_2)));
        int num16 = Math.Max(val1_1, Math.Max(val1_3, Math.Max(val1_5, val2_1)));
        int num17 = Math.Max(val1_2, Math.Max(val1_4, Math.Max(val1_6, val2_2)));
        pHX = -num14;
        pHY = -num15;
        pWidth = num16 - num14;
        pHeight = num17 - num15;
      }

      public bool createRotatedMask(CMask pMask, double fAngle, double fScaleX, double fScaleY)
      {
        int width = pMask.width;
        int height = pMask.height;
        int pWidth = (int) ((double) pMask.width * fScaleX);
        int pHeight = (int) ((double) pMask.height * fScaleY);
        int pHX = (int) ((double) pMask.xSpot * fScaleX);
        int pHY = (int) ((double) pMask.ySpot * fScaleY);
        this.rotateRect(ref pWidth, ref pHeight, ref pHX, ref pHY, fAngle);
        int num1 = pWidth;
        int num2 = pHeight;
        if (num1 <= 0 || num2 <= 0)
          return false;
        int lineWidth = pMask.lineWidth;
        int num3 = (num1 + 15 & 2147483632) / 16 /*0x10*/;
        this.mask = new ushort[num3 * num2 + 1];
        this.lineWidth = num3;
        this.width = num1;
        this.height = num2;
        this.xSpot = pHX;
        this.ySpot = pHY;
        double num4 = fAngle * 0.017453292;
        double num5 = Math.Cos(num4);
        double num6 = Math.Sin(num4);
        double num7 = (double) width / 2.0 - ((double) num1 / 2.0 * num5 - (double) num2 / 2.0 * num6) / fScaleX;
        double num8 = (double) height / 2.0 - ((double) num1 / 2.0 * num6 + (double) num2 / 2.0 * num5) / fScaleY;
        int num9 = 0;
        int num10 = (int) (num7 * 65536.0);
        int num11 = (int) (num8 * 65536.0);
        int num12 = (int) (num5 * 65536.0 / fScaleX);
        int num13 = (int) (num6 * 65536.0 / fScaleY);
        int num14 = num1 / 16 /*0x10*/;
        int num15 = num1 % 16 /*0x10*/;
        int num16 = (int) (num5 * 65536.0 / fScaleY);
        int num17 = (int) (num6 * 65536.0 / fScaleX);
        int num18 = width * 65536 /*0x010000*/;
        int num19 = height * 65536 /*0x010000*/;
        for (int index1 = 0; index1 < num2; ++index1)
        {
          int num20 = num10;
          int num21 = num11;
          int index2 = num9;
          for (int index3 = 0; index3 < num14; ++index3)
          {
            ushort num22 = 0;
            if (num20 >= 0 && num20 < num18 && num21 >= 0 && num21 < num19)
            {
              int num23 = num20 / 65536 /*0x010000*/;
              int num24 = num21 / 65536 /*0x010000*/;
              ushort num25 = (ushort) (32768 /*0x8000*/ >> num23 % 16 /*0x10*/);
              if (((int) pMask.mask[num24 * lineWidth + num23 / 16 /*0x10*/] & (int) num25) != 0)
                num22 |= (ushort) 32768 /*0x8000*/;
            }
            int num26 = num20 + num12;
            int num27 = num21 + num13;
            if (num26 >= 0 && num26 < num18 && num27 >= 0 && num27 < num19)
            {
              int num28 = num26 / 65536 /*0x010000*/;
              int num29 = num27 / 65536 /*0x010000*/;
              ushort num30 = (ushort) (32768 /*0x8000*/ >> num28 % 16 /*0x10*/);
              if (((int) pMask.mask[num29 * lineWidth + num28 / 16 /*0x10*/] & (int) num30) != 0)
                num22 |= (ushort) 16384 /*0x4000*/;
            }
            int num31 = num26 + num12;
            int num32 = num27 + num13;
            if (num31 >= 0 && num31 < num18 && num32 >= 0 && num32 < num19)
            {
              int num33 = num31 / 65536 /*0x010000*/;
              int num34 = num32 / 65536 /*0x010000*/;
              ushort num35 = (ushort) (32768 /*0x8000*/ >> num33 % 16 /*0x10*/);
              if (((int) pMask.mask[num34 * lineWidth + num33 / 16 /*0x10*/] & (int) num35) != 0)
                num22 |= (ushort) 8192 /*0x2000*/;
            }
            int num36 = num31 + num12;
            int num37 = num32 + num13;
            if (num36 >= 0 && num36 < num18 && num37 >= 0 && num37 < num19)
            {
              int num38 = num36 / 65536 /*0x010000*/;
              int num39 = num37 / 65536 /*0x010000*/;
              ushort num40 = (ushort) (32768 /*0x8000*/ >> num38 % 16 /*0x10*/);
              if (((int) pMask.mask[num39 * lineWidth + num38 / 16 /*0x10*/] & (int) num40) != 0)
                num22 |= (ushort) 4096 /*0x1000*/;
            }
            int num41 = num36 + num12;
            int num42 = num37 + num13;
            if (num41 >= 0 && num41 < num18 && num42 >= 0 && num42 < num19)
            {
              int num43 = num41 / 65536 /*0x010000*/;
              int num44 = num42 / 65536 /*0x010000*/;
              ushort num45 = (ushort) (32768 /*0x8000*/ >> num43 % 16 /*0x10*/);
              if (((int) pMask.mask[num44 * lineWidth + num43 / 16 /*0x10*/] & (int) num45) != 0)
                num22 |= (ushort) 2048 /*0x0800*/;
            }
            int num46 = num41 + num12;
            int num47 = num42 + num13;
            if (num46 >= 0 && num46 < num18 && num47 >= 0 && num47 < num19)
            {
              int num48 = num46 / 65536 /*0x010000*/;
              int num49 = num47 / 65536 /*0x010000*/;
              ushort num50 = (ushort) (32768 /*0x8000*/ >> num48 % 16 /*0x10*/);
              if (((int) pMask.mask[num49 * lineWidth + num48 / 16 /*0x10*/] & (int) num50) != 0)
                num22 |= (ushort) 1024 /*0x0400*/;
            }
            int num51 = num46 + num12;
            int num52 = num47 + num13;
            if (num51 >= 0 && num51 < num18 && num52 >= 0 && num52 < num19)
            {
              int num53 = num51 / 65536 /*0x010000*/;
              int num54 = num52 / 65536 /*0x010000*/;
              ushort num55 = (ushort) (32768 /*0x8000*/ >> num53 % 16 /*0x10*/);
              if (((int) pMask.mask[num54 * lineWidth + num53 / 16 /*0x10*/] & (int) num55) != 0)
                num22 |= (ushort) 512 /*0x0200*/;
            }
            int num56 = num51 + num12;
            int num57 = num52 + num13;
            if (num56 >= 0 && num56 < num18 && num57 >= 0 && num57 < num19)
            {
              int num58 = num56 / 65536 /*0x010000*/;
              int num59 = num57 / 65536 /*0x010000*/;
              ushort num60 = (ushort) (32768 /*0x8000*/ >> num58 % 16 /*0x10*/);
              if (((int) pMask.mask[num59 * lineWidth + num58 / 16 /*0x10*/] & (int) num60) != 0)
                num22 |= (ushort) 256 /*0x0100*/;
            }
            int num61 = num56 + num12;
            int num62 = num57 + num13;
            if (num61 >= 0 && num61 < num18 && num62 >= 0 && num62 < num19)
            {
              int num63 = num61 / 65536 /*0x010000*/;
              int num64 = num62 / 65536 /*0x010000*/;
              ushort num65 = (ushort) (32768 /*0x8000*/ >> num63 % 16 /*0x10*/);
              if (((int) pMask.mask[num64 * lineWidth + num63 / 16 /*0x10*/] & (int) num65) != 0)
                num22 |= (ushort) 128 /*0x80*/;
            }
            int num66 = num61 + num12;
            int num67 = num62 + num13;
            if (num66 >= 0 && num66 < num18 && num67 >= 0 && num67 < num19)
            {
              int num68 = num66 / 65536 /*0x010000*/;
              int num69 = num67 / 65536 /*0x010000*/;
              ushort num70 = (ushort) (32768 /*0x8000*/ >> num68 % 16 /*0x10*/);
              if (((int) pMask.mask[num69 * lineWidth + num68 / 16 /*0x10*/] & (int) num70) != 0)
                num22 |= (ushort) 64 /*0x40*/;
            }
            int num71 = num66 + num12;
            int num72 = num67 + num13;
            if (num71 >= 0 && num71 < num18 && num72 >= 0 && num72 < num19)
            {
              int num73 = num71 / 65536 /*0x010000*/;
              int num74 = num72 / 65536 /*0x010000*/;
              ushort num75 = (ushort) (32768 /*0x8000*/ >> num73 % 16 /*0x10*/);
              if (((int) pMask.mask[num74 * lineWidth + num73 / 16 /*0x10*/] & (int) num75) != 0)
                num22 |= (ushort) 32 /*0x20*/;
            }
            int num76 = num71 + num12;
            int num77 = num72 + num13;
            if (num76 >= 0 && num76 < num18 && num77 >= 0 && num77 < num19)
            {
              int num78 = num76 / 65536 /*0x010000*/;
              int num79 = num77 / 65536 /*0x010000*/;
              ushort num80 = (ushort) (32768 /*0x8000*/ >> num78 % 16 /*0x10*/);
              if (((int) pMask.mask[num79 * lineWidth + num78 / 16 /*0x10*/] & (int) num80) != 0)
                num22 |= (ushort) 16 /*0x10*/;
            }
            int num81 = num76 + num12;
            int num82 = num77 + num13;
            if (num81 >= 0 && num81 < num18 && num82 >= 0 && num82 < num19)
            {
              int num83 = num81 / 65536 /*0x010000*/;
              int num84 = num82 / 65536 /*0x010000*/;
              ushort num85 = (ushort) (32768 /*0x8000*/ >> num83 % 16 /*0x10*/);
              if (((int) pMask.mask[num84 * lineWidth + num83 / 16 /*0x10*/] & (int) num85) != 0)
                num22 |= (ushort) 8;
            }
            int num86 = num81 + num12;
            int num87 = num82 + num13;
            if (num86 >= 0 && num86 < num18 && num87 >= 0 && num87 < num19)
            {
              int num88 = num86 / 65536 /*0x010000*/;
              int num89 = num87 / 65536 /*0x010000*/;
              ushort num90 = (ushort) (32768 /*0x8000*/ >> num88 % 16 /*0x10*/);
              if (((int) pMask.mask[num89 * lineWidth + num88 / 16 /*0x10*/] & (int) num90) != 0)
                num22 |= (ushort) 4;
            }
            int num91 = num86 + num12;
            int num92 = num87 + num13;
            if (num91 >= 0 && num91 < num18 && num92 >= 0 && num92 < num19)
            {
              int num93 = num91 / 65536 /*0x010000*/;
              int num94 = num92 / 65536 /*0x010000*/;
              ushort num95 = (ushort) (32768 /*0x8000*/ >> num93 % 16 /*0x10*/);
              if (((int) pMask.mask[num94 * lineWidth + num93 / 16 /*0x10*/] & (int) num95) != 0)
                num22 |= (ushort) 2;
            }
            int num96 = num91 + num12;
            int num97 = num92 + num13;
            if (num96 >= 0 && num96 < num18 && num97 >= 0 && num97 < num19)
            {
              int num98 = num96 / 65536 /*0x010000*/;
              int num99 = num97 / 65536 /*0x010000*/;
              ushort num100 = (ushort) (32768 /*0x8000*/ >> num98 % 16 /*0x10*/);
              if (((int) pMask.mask[num99 * lineWidth + num98 / 16 /*0x10*/] & (int) num100) != 0)
                num22 |= (ushort) 1;
            }
            num20 = num96 + num12;
            num21 = num97 + num13;
            this.mask[index2++] = num22;
          }
          if (num15 != 0)
          {
            ushort num101 = 32768 /*0x8000*/;
            ushort num102 = 0;
            int num103 = 0;
            while (num103 < num15)
            {
              if (num20 >= 0 && num20 < num18 && num21 >= 0 && num21 < num19)
              {
                int num104 = num20 / 65536 /*0x010000*/;
                int num105 = num21 / 65536 /*0x010000*/;
                ushort num106 = (ushort) (32768 /*0x8000*/ >> num104 % 16 /*0x10*/);
                if (((int) pMask.mask[num105 * lineWidth + num104 / 16 /*0x10*/] & (int) num106) != 0)
                  num102 |= num101;
              }
              num20 += num12;
              num21 += num13;
              ++num103;
              num101 = (ushort) ((int) num101 >> 1 & (int) short.MaxValue);
            }
            this.mask[index2] = num102;
          }
          num9 += num3;
          num10 -= num17;
          num11 += num16;
        }
        return true;
      }

      public bool testMask(int yBase1, int x1, int y1, CMask pMask2, int yBase2, int x2, int y2)
      {
        CMask cmask1;
        CMask cmask2;
        int num1;
        int num2;
        int num3;
        int num4;
        int num5;
        int num6;
        if (x1 <= x2)
        {
          cmask1 = this;
          cmask2 = pMask2;
          num1 = yBase1;
          num2 = yBase2;
          num3 = x1;
          num4 = y1;
          num5 = x2;
          num6 = y2;
        }
        else
        {
          cmask1 = pMask2;
          cmask2 = this;
          num1 = yBase2;
          num2 = yBase1;
          num3 = x2;
          num4 = y2;
          num5 = x1;
          num6 = y1;
        }
        int num7 = cmask1.height - num1;
        int num8 = cmask2.height - num2;
        if (num3 >= num5 + cmask2.width || num3 + cmask1.width <= num5 || num4 >= num6 + num8 || num4 + num7 < num6)
          return false;
        int num9 = num5 - num3;
        int num10 = num9 / 16 /*0x10*/;
        int num11 = num9 % 16 /*0x10*/;
        int num12 = (Math.Min(num3 + cmask1.width - num5, cmask2.width) + 15) / 16 /*0x10*/;
        int num13;
        int num14;
        int num15;
        if (num4 <= num6)
        {
          num13 = num6 - num4 + num1;
          num14 = num2;
          num15 = Math.Min(num4 + num7, num6 + num8) - num6;
        }
        else
        {
          num13 = num1;
          num14 = num4 - num6 + num2;
          num15 = Math.Min(num4 + num7, num6 + num8) - num4;
        }
        if (num11 != 0)
        {
          switch (num12)
          {
            case 1:
              for (int index1 = 0; index1 < num15; ++index1)
              {
                int num16 = (num13 + index1) * cmask1.lineWidth;
                int index2 = (num14 + index1) * cmask2.lineWidth;
                if (((int) (ushort) ((uint) cmask1.mask[num16 + num10] << num11) & (int) cmask2.mask[index2]) != 0 || num10 * 16 /*0x10*/ + 16 /*0x10*/ < cmask1.width && ((int) (ushort) (((int) cmask1.mask[num16 + num10 + 1] & (int) ushort.MaxValue) << num11 >> 16 /*0x10*/) & (int) cmask2.mask[index2]) != 0)
                  return true;
              }
              break;
            case 2:
              for (int index3 = 0; index3 < num15; ++index3)
              {
                int num17 = (num13 + index3) * cmask1.lineWidth;
                int index4 = (num14 + index3) * cmask2.lineWidth;
                if (((int) (ushort) ((uint) cmask1.mask[num17 + num10] << num11) & (int) cmask2.mask[index4]) != 0)
                  return true;
                int num18 = ((int) cmask1.mask[num17 + num10 + 1] & (int) ushort.MaxValue) << num11;
                if (((int) (ushort) (num18 >> 16 /*0x10*/) & (int) cmask2.mask[index4]) != 0 || ((int) (ushort) num18 & (int) cmask2.mask[index4 + 1]) != 0 || num10 + 2 < cmask1.lineWidth && ((int) (ushort) ((int) cmask1.mask[num17 + num10 + 2] << num11 >> 16 /*0x10*/) & (int) cmask2.mask[index4 + 1]) != 0)
                  return true;
              }
              break;
            default:
              for (int index5 = 0; index5 < num15; ++index5)
              {
                int num19 = (num13 + index5) * cmask1.lineWidth;
                int index6 = (num14 + index5) * cmask2.lineWidth;
                if (((int) (ushort) ((uint) cmask1.mask[num19 + num10] << num11) & (int) cmask2.mask[index6]) != 0)
                  return true;
                int num20;
                for (num20 = 0; num20 < num12 - 1; ++num20)
                {
                  int num21 = ((int) cmask1.mask[num19 + num10 + num20 + 1] & (int) ushort.MaxValue) << num11;
                  if (((int) (ushort) (num21 >> 16 /*0x10*/) & (int) cmask2.mask[index6 + num20]) != 0 || ((int) (ushort) num21 & (int) cmask2.mask[index6 + num20 + 1]) != 0)
                    return true;
                }
                if (num10 + num20 + 1 < cmask1.lineWidth && ((int) (ushort) ((int) cmask1.mask[num19 + num10 + num20 + 1] << num11 >> 16 /*0x10*/) & (int) cmask2.mask[index6 + num20]) != 0)
                  return true;
              }
              break;
          }
        }
        else
        {
          for (int index7 = 0; index7 < num15; ++index7)
          {
            int num22 = (num13 + index7) * cmask1.lineWidth;
            int num23 = (num14 + index7) * cmask2.lineWidth;
            for (int index8 = 0; index8 < num12; ++index8)
            {
              int num24 = (int) cmask1.mask[num22 + num10 + index8];
              if (((int) cmask2.mask[num23 + index8] & num24) != 0)
                return true;
            }
          }
        }
        return false;
      }

      public bool testRect(int yBase1, int xx, int yy, int w, int h)
      {
        int num1 = xx;
        if (num1 < 0)
        {
          w += num1;
          num1 = 0;
        }
        int num2 = yy;
        if (yBase1 != 0 && num2 >= 0)
        {
          num2 = yBase1 + num2;
          h = this.height - num2;
        }
        if (num2 < 0)
        {
          h += num2;
          num2 = 0;
        }
        int num3 = num1 + w;
        if (num3 > this.width)
          num3 = this.width;
        int num4 = num2 + h;
        if (num4 > this.height)
          num4 = this.height;
        int num5 = num2 * this.lineWidth;
        int num6 = num4 - num2;
        int num7 = (num3 - num1) / 16 /*0x10*/ + 1;
        int num8 = num1 / 16 /*0x10*/;
        for (int index = 0; index < num6; ++index)
        {
          int num9 = index * this.lineWidth + num5;
          switch (num7)
          {
            case 1:
              ushort num10 = (ushort) ((uint) CMask.lMask[num1 & 15] & (uint) CMask.rMask[num3 - 1 & 15]);
              if (((int) this.mask[num9 + num8] & (int) num10) != 0)
                return true;
              break;
            case 2:
              ushort num11 = CMask.lMask[num1 & 15];
              if (((int) this.mask[num9 + num8] & (int) num11) != 0)
                return true;
              ushort num12 = CMask.rMask[num3 - 1 & 15];
              if (((int) this.mask[num9 + num8 + 1] & (int) num12) != 0)
                return true;
              break;
            default:
              ushort num13 = CMask.lMask[num1 & 15];
              if (((int) this.mask[num9 + num8] & (int) num13) != 0)
                return true;
              int num14;
              for (num14 = 1; num14 < num7 - 1; ++num14)
              {
                if (this.mask[num9 + num8 + 1] != (ushort) 0)
                  return true;
              }
              ushort num15 = CMask.rMask[num3 - 1 & 15];
              if (((int) this.mask[num9 + num8 + num14] & (int) num15) != 0)
                return true;
              break;
          }
        }
        return false;
      }

      public bool testPoint(int x1, int y1)
      {
        return x1 >= 0 && x1 < this.width && y1 >= 0 && y1 < this.height && ((int) this.mask[y1 * this.lineWidth + x1 / 16 /*0x10*/] & (int) (ushort) (32768 /*0x8000*/ >> (x1 & 15))) != 0;
      }
    }
}
