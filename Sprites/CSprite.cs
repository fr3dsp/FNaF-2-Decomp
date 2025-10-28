// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Sprites.CSprite
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using RuntimeXNA.Banks;
using RuntimeXNA.Objects;
using RuntimeXNA.Services;
using System;

namespace RuntimeXNA.Sprites
{

    public class CSprite
    {
      public const uint SF_RAMBO = 1;
      public const uint SF_RECALCSURF = 2;
      public const uint SF_PRIVATE = 4;
      public const uint SF_INACTIF = 8;
      public const uint SF_TOHIDE = 16 /*0x10*/;
      public const uint SF_TOKILL = 32 /*0x20*/;
      public const uint SF_REAF = 64 /*0x40*/;
      public const uint SF_HIDDEN = 128 /*0x80*/;
      public const uint SF_COLBOX = 256 /*0x0100*/;
      public const uint SF_NOSAVE = 512 /*0x0200*/;
      public const uint SF_FILLBACK = 1024 /*0x0400*/;
      public const uint SF_DISABLED = 2048 /*0x0800*/;
      public const uint SF_REAFINT = 4096 /*0x1000*/;
      public const uint SF_OWNERDRAW = 8192 /*0x2000*/;
      public const uint SF_OWNERSAVE = 16384 /*0x4000*/;
      public const uint SF_FADE = 32768 /*0x8000*/;
      public const uint SF_OBSTACLE = 65536 /*0x010000*/;
      public const uint SF_PLATFORM = 131072 /*0x020000*/;
      public const uint SF_BACKGROUND = 524288 /*0x080000*/;
      public const uint SF_SCALE_RESAMPLE = 1048576 /*0x100000*/;
      public const uint SF_ROTATE_ANTIA = 2097152 /*0x200000*/;
      public const uint SF_NOHOTSPOT = 4194304 /*0x400000*/;
      public const uint SF_OWNERCOLMASK = 8388608 /*0x800000*/;
      public const uint SF_UPDATECOLLIST = 268435456 /*0x10000000*/;
      public const uint SF_TRUEOBJECT = 536870912 /*0x20000000*/;
      public const int EFFECTFLAG_TRANSPARENT = 268435456 /*0x10000000*/;
      public const int EFFECTFLAG_ANTIALIAS = 536870912 /*0x20000000*/;
      public const int EFFECT_SEMITRANSP = 1;
      public CSprite objPrev;
      public CSprite objNext;
      public CImageBank bank;
      public uint sprFlags;
      public short sprLayer;
      public short sprAngle;
      public short sprAnglenew;
      public int sprZOrder;
      public int sprX;
      public int sprY;
      public int sprX1;
      public int sprY1;
      public int sprX2;
      public int sprY2;
      public int sprXnew;
      public int sprYnew;
      public int sprX1new;
      public int sprY1new;
      public int sprX2new;
      public int sprY2new;
      public int sprX1z;
      public int sprY1z;
      public int sprX2z;
      public int sprY2z;
      public float sprScaleX;
      public float sprScaleY;
      public float sprScaleXnew;
      public float sprScaleYnew;
      public short sprImg;
      public short sprImgNew;
      public IDrawing sprRout;
      public int sprEffect;
      public int sprEffectParam;
      public Color rgb = Color.White;
      public int sprBackColor;
      public CObject sprExtraInfo;

      public CSprite()
      {
      }

      public CSprite(CImageBank b) => this.bank = b;

      public int getSpriteLayer() => (int) this.sprLayer / 2;

      public uint getSpriteFlags() => this.sprFlags;

      public uint setSpriteFlags(uint dwNewFlags)
      {
        uint sprFlags = this.sprFlags;
        this.sprFlags = dwNewFlags;
        return sprFlags;
      }

      public uint setSpriteColFlag(uint colMode)
      {
        uint num = this.sprFlags & 1U;
        this.sprFlags = this.sprFlags & 4294967294U | colMode;
        return num;
      }

      public float getSpriteScaleX() => this.sprScaleX;

      public float getSpriteScaleY() => this.sprScaleY;

      public bool getSpriteScaleResample() => ((int) this.sprFlags & 1048576 /*0x100000*/) != 0;

      public int getSpriteAngle() => (int) this.sprAngle;

      public bool getSpriteAngleAntiA() => ((int) this.sprFlags & 2097152 /*0x200000*/) != 0;

      public CRect getSpriteRect()
      {
        return new CRect()
        {
          left = this.sprX1new,
          right = this.sprX2new,
          top = this.sprY1new,
          bottom = this.sprY2new
        };
      }

      public void updateBoundingBox()
      {
        CImage imageFromHandle = this.bank.getImageFromHandle(this.sprImgNew);
        if (imageFromHandle == null)
        {
          this.sprX1new = this.sprXnew;
          this.sprX2new = this.sprXnew + 1;
          this.sprY1new = this.sprYnew;
          this.sprY2new = this.sprYnew + 1;
        }
        else
        {
          int num1 = (int) imageFromHandle.width;
          int num2 = (int) imageFromHandle.height;
          int num3 = 0;
          int num4 = 0;
          if (((int) this.sprFlags & 4194304 /*0x400000*/) == 0)
          {
            num4 = (int) imageFromHandle.ySpot;
            num3 = (int) imageFromHandle.xSpot;
          }
          if (this.sprAngle == (short) 0)
          {
            if ((double) this.sprScaleXnew == 1.0)
            {
              this.sprX1new = this.sprXnew - num3;
              this.sprX2new = this.sprX1new + num1;
            }
            else
            {
              this.sprX1new = this.sprXnew - (int) ((double) num3 * (double) this.sprScaleXnew);
              this.sprX2new = this.sprX1new + (int) ((double) num1 * (double) this.sprScaleXnew);
            }
            if ((double) this.sprScaleYnew == 1.0)
            {
              this.sprY1new = this.sprYnew - num4;
              this.sprY2new = this.sprY1new + num2;
            }
            else
            {
              this.sprY1new = this.sprYnew - (int) ((double) num4 * (double) this.sprScaleYnew);
              this.sprY2new = this.sprY1new + (int) ((double) num2 * (double) this.sprScaleYnew);
            }
          }
          else
          {
            if ((double) this.sprScaleXnew != 1.0)
            {
              num3 = (int) ((double) num3 * (double) this.sprScaleXnew);
              num1 = (int) ((double) num1 * (double) this.sprScaleXnew);
            }
            if ((double) this.sprScaleYnew != 1.0)
            {
              num4 = (int) ((double) num4 * (double) this.sprScaleYnew);
              num2 = (int) ((double) num2 * (double) this.sprScaleYnew);
            }
            int num5 = num1 - 1;
            int num6 = num2 - 1;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            if (this.sprAnglenew == (short) 90)
            {
              num7 = num6;
              num8 = -num5;
              num9 = 0;
              num10 = 0;
              num11 = num4;
              num12 = -num3;
            }
            else if (this.sprAnglenew == (short) 180)
            {
              num7 = 0;
              num8 = 0;
              num9 = -num6;
              num10 = -num5;
              num11 = -num3;
              num12 = -num4;
            }
            else if (this.sprAnglenew == (short) 270)
            {
              num7 = -num6;
              num8 = num5;
              num9 = 0;
              num10 = 0;
              num11 = -num4;
              num12 = num3;
            }
            else
            {
              double num13 = (double) this.sprAnglenew * Math.PI / 180.0;
              float num14 = (float) Math.Cos(num13);
              float num15 = (float) Math.Sin(num13);
              num11 = (int) ((double) num3 * (double) num14 + (double) num4 * (double) num15);
              num12 = (int) ((double) num4 * (double) num14 - (double) num3 * (double) num15);
              num7 = (int) ((double) num6 * (double) num15);
              num8 = -(int) ((double) num5 * (double) num15);
              num9 = (int) ((double) num6 * (double) num14);
              num10 = (int) ((double) num5 * (double) num14);
            }
            int num16 = num7 + num10;
            int num17 = num9 + num8;
            int val1_1 = this.sprXnew - num11;
            int val1_2 = this.sprYnew - num12;
            int val2_1 = num7 + (this.sprXnew - num11);
            int val2_2 = num9 + (this.sprYnew - num12);
            int val2_3 = num16 + (this.sprXnew - num11);
            int val2_4 = num17 + (this.sprYnew - num12);
            int val2_5 = num10 + (this.sprXnew - num11);
            int val2_6 = num8 + (this.sprYnew - num12);
            this.sprX1new = Math.Min(val1_1, val2_1);
            this.sprX1new = Math.Min(this.sprX1new, val2_3);
            this.sprX1new = Math.Min(this.sprX1new, val2_5);
            this.sprX2new = Math.Max(val1_1, val2_1);
            this.sprX2new = Math.Max(this.sprX2new, val2_3);
            this.sprX2new = Math.Max(this.sprX2new, val2_5);
            ++this.sprX2new;
            this.sprY1new = Math.Min(val1_2, val2_2);
            this.sprY1new = Math.Min(this.sprY1new, val2_4);
            this.sprY1new = Math.Min(this.sprY1new, val2_6);
            this.sprY2new = Math.Max(val1_2, val2_2);
            this.sprY2new = Math.Max(this.sprY2new, val2_4);
            this.sprY2new = Math.Max(this.sprY2new, val2_6);
            ++this.sprY2new;
          }
        }
      }

      public void calcBoundingBox(
        short newImg,
        int newX,
        int newY,
        int newAngle,
        float newScaleX,
        float newScaleY,
        CRect prc)
      {
        prc.left = prc.top = prc.right = prc.bottom = 0;
        CImage imageFromHandle = this.bank.getImageFromHandle(newImg);
        if (imageFromHandle == null)
          return;
        int num1 = (int) imageFromHandle.width;
        int num2 = (int) imageFromHandle.height;
        int num3 = 0;
        int num4 = 0;
        if (((int) this.sprFlags & 4194304 /*0x400000*/) == 0)
        {
          num4 = (int) imageFromHandle.ySpot;
          num3 = (int) imageFromHandle.xSpot;
        }
        if (newAngle == 0)
        {
          if ((double) newScaleX == 1.0)
          {
            prc.left = newX - num3;
            prc.right = prc.left + num1;
          }
          else
          {
            prc.left = newX - (int) ((double) num3 * (double) newScaleX);
            prc.right = prc.left + (int) ((double) num1 * (double) newScaleX);
          }
          if ((double) newScaleY == 1.0)
          {
            prc.top = newY - num4;
            prc.bottom = prc.top + num2;
          }
          else
          {
            prc.top = newY - (int) ((double) num4 * (double) newScaleY);
            prc.bottom = prc.top + (int) ((double) num2 * (double) newScaleY);
          }
        }
        else
        {
          if ((double) newScaleX != 1.0)
          {
            num3 = (int) ((double) num3 * (double) newScaleX);
            num1 = (int) ((double) num1 * (double) newScaleX);
          }
          if ((double) newScaleY != 1.0)
          {
            num4 = (int) ((double) num4 * (double) newScaleY);
            num2 = (int) ((double) num2 * (double) newScaleY);
          }
          int num5 = num1 - 1;
          int num6 = num2 - 1;
          int num7;
          int num8;
          int num9;
          int num10;
          int num11;
          int num12;
          switch (newAngle)
          {
            case 90:
              num7 = num6;
              num8 = -num5;
              num9 = 0;
              num10 = 0;
              num11 = num4;
              num12 = -num3;
              break;
            case 180:
              num7 = 0;
              num8 = 0;
              num9 = -num6;
              num10 = -num5;
              num11 = -num3;
              num12 = -num4;
              break;
            case 270:
              num7 = -num6;
              num8 = num5;
              num9 = 0;
              num10 = 0;
              num11 = -num4;
              num12 = num3;
              break;
            default:
              double num13 = (double) newAngle * Math.PI / 180.0;
              float num14 = (float) Math.Cos(num13);
              float num15 = (float) Math.Sin(num13);
              num11 = (int) ((double) num3 * (double) num14 + (double) num4 * (double) num15);
              num12 = (int) ((double) num4 * (double) num14 - (double) num3 * (double) num15);
              num7 = (int) ((double) num6 * (double) num15);
              num8 = -(int) ((double) num5 * (double) num15);
              num9 = (int) ((double) num6 * (double) num14);
              num10 = (int) ((double) num5 * (double) num14);
              break;
          }
          int num16 = num7 + num10;
          int num17 = num9 + num8;
          int val1_1 = newX - num11;
          int val1_2 = newY - num12;
          int val2_1 = num7 + (newX - num11);
          int val2_2 = num9 + (newY - num12);
          int val2_3 = num16 + (newX - num11);
          int val2_4 = num17 + (newY - num12);
          int val2_5 = num10 + (newX - num11);
          int val2_6 = num8 + (newY - num12);
          prc.left = Math.Min(val1_1, val2_1);
          prc.left = Math.Min(prc.left, val2_3);
          prc.left = Math.Min(prc.left, val2_5);
          prc.right = Math.Max(val1_1, val2_1);
          prc.right = Math.Max(prc.right, val2_3);
          prc.right = Math.Max(prc.right, val2_5);
          ++prc.right;
          prc.top = Math.Min(val1_2, val2_2);
          prc.top = Math.Min(prc.top, val2_4);
          prc.top = Math.Min(prc.top, val2_6);
          prc.bottom = Math.Max(val1_2, val2_2);
          prc.bottom = Math.Max(prc.bottom, val2_4);
          prc.bottom = Math.Max(prc.bottom, val2_6);
          ++prc.bottom;
        }
      }

      private void draw(SpriteBatchEffect batch)
      {
      }
    }
}
