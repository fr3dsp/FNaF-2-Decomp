// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Objects.CCounter
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RuntimeXNA.Banks;
using RuntimeXNA.Expressions;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;
using System;

namespace RuntimeXNA.Objects
{

    internal class CCounter : CObject, IDrawing
    {
      public short rsFlags;
      public int rsMini;
      public int rsMaxi;
      public CValue rsValue;
      public int rsBoxCx;
      public int rsBoxCy;
      public double rsMiniDouble;
      public double rsMaxiDouble;
      public short rsOldFrame;
      public byte rsHidden;
      public short rsFont;
      public int rsColor1;
      public int rsColor2;
      public int displayFlags;
      public Texture2D texture;
      public CRect tempRc;

      public override void init(CObjectCommon ocPtr, CCreateObjectInfo cob)
      {
        this.rsFlags = (short) 0;
        this.rsFont = (short) -1;
        this.rsColor1 = 0;
        this.rsColor2 = 0;
        this.hoImgWidth = this.hoImgHeight = 1;
        if (this.hoCommon.ocCounters == null)
        {
          this.hoImgWidth = this.rsBoxCx = 1;
          this.hoImgHeight = this.rsBoxCy = 1;
        }
        else
        {
          CDefCounters ocCounters = this.hoCommon.ocCounters;
          this.hoImgWidth = this.rsBoxCx = ocCounters.odCx;
          this.hoImgHeight = this.rsBoxCy = ocCounters.odCy;
          this.displayFlags = (int) ocCounters.odDisplayFlags;
          switch (ocCounters.odDisplayType)
          {
            case 2:
            case 3:
              this.rsColor1 = ocCounters.ocColor1;
              this.rsColor2 = ocCounters.ocColor2;
              break;
            case 5:
              this.rsColor1 = ocCounters.ocColor1;
              break;
          }
        }
        CDefCounter ocObject = (CDefCounter) this.hoCommon.ocObject;
        this.rsMini = ocObject.ctMini;
        this.rsMaxi = ocObject.ctMaxi;
        this.rsMiniDouble = (double) this.rsMini;
        this.rsMaxiDouble = (double) this.rsMaxi;
        this.rsValue = new CValue(ocObject.ctInit);
        this.rsOldFrame = (short) -1;
      }

      public override void handle()
      {
        this.ros.handle();
        if (!this.roc.rcChanged)
          return;
        this.roc.rcChanged = false;
        this.modif();
      }

      public override void modif() => this.ros.modifRoutine();

      public override void display() => this.ros.displayRoutine();

      public override void getZoneInfos()
      {
        this.hoImgWidth = this.hoImgHeight = 1;
        if (this.hoCommon.ocCounters == null)
          return;
        CDefCounters ocCounters = this.hoCommon.ocCounters;
        double num1 = 0.0;
        int num2;
        if (this.rsValue.getType() == (byte) 0)
        {
          num2 = this.rsValue.getInt();
        }
        else
        {
          num1 = this.rsValue.getDouble();
          num2 = (int) num1;
        }
        switch (ocCounters.odDisplayType)
        {
          case 1:
            string str = this.rsValue.getType() != (byte) 0 ? CServices.doubleToString(num1, this.displayFlags) : CServices.intToString(num2, this.displayFlags);
            int num3 = 0;
            int val1 = 0;
            for (int index = 0; index < str.Length; ++index)
            {
              char ch = str[index];
              short handle = 0;
              switch (ch)
              {
                case '+':
                  handle = ocCounters.frames[11];
                  break;
                case ',':
                case '.':
                  handle = ocCounters.frames[12];
                  break;
                case '-':
                  handle = ocCounters.frames[10];
                  break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                  handle = ocCounters.frames[(int) ch - 48 /*0x30*/];
                  break;
                case 'E':
                case 'e':
                  handle = ocCounters.frames[13];
                  break;
              }
              CImage imageFromHandle = this.hoAdRunHeader.rhApp.imageBank.getImageFromHandle(handle);
              num3 += (int) imageFromHandle.width;
              val1 = Math.Max(val1, (int) imageFromHandle.height);
            }
            this.hoImgWidth = num3;
            this.hoImgHeight = val1;
            this.hoImgXSpot = num3;
            this.hoImgYSpot = val1;
            break;
          case 2:
          case 3:
            int num4 = this.rsBoxCx;
            if (ocCounters.odDisplayType == (short) 2)
              num4 = this.rsBoxCy;
            this.rsOldFrame = this.rsMaxi > this.rsMini ? (short) ((num2 - this.rsMini) * num4 / (this.rsMaxi - this.rsMini)) : (short) 0;
            if (ocCounters.odDisplayType == (short) 3)
            {
              this.hoImgYSpot = 0;
              this.hoImgHeight = this.rsBoxCy;
              this.hoImgWidth = (int) this.rsOldFrame;
              if (((int) ocCounters.odDisplayFlags & 256 /*0x0100*/) != 0)
              {
                this.hoImgXSpot = (int) this.rsOldFrame - this.rsBoxCx;
                break;
              }
              this.hoImgXSpot = 0;
              break;
            }
            this.hoImgXSpot = 0;
            this.hoImgWidth = this.rsBoxCx;
            this.hoImgHeight = (int) this.rsOldFrame;
            if (((int) ocCounters.odDisplayFlags & 256 /*0x0100*/) != 0)
            {
              this.hoImgYSpot = (int) this.rsOldFrame - this.rsBoxCy;
              break;
            }
            this.hoImgYSpot = 0;
            break;
          case 4:
            int num5 = (int) ocCounters.nFrames - 1;
            this.rsOldFrame = this.rsMaxi > this.rsMini ? (short) Math.Min((num2 - this.rsMini) * num5 / (this.rsMaxi - this.rsMini), (int) ocCounters.nFrames - 1) : (short) 0;
            CImage imageFromHandle1 = this.hoAdRunHeader.rhApp.imageBank.getImageFromHandle(ocCounters.frames[Math.Max((int) this.rsOldFrame - 1, 0)]);
            this.rsBoxCx = this.hoImgWidth = (int) imageFromHandle1.width;
            this.rsBoxCy = this.hoImgHeight = (int) imageFromHandle1.height;
            this.hoImgXSpot = (int) imageFromHandle1.xSpot;
            this.hoImgYSpot = (int) imageFromHandle1.ySpot;
            break;
          case 5:
            string s = this.rsValue.getType() != (byte) 0 ? CServices.doubleToString(num1, this.displayFlags) : CServices.intToString(num2, this.displayFlags);
            CRect rc = new CRect()
            {
              left = this.hoX - this.hoAdRunHeader.rhWindowX,
              top = this.hoY - this.hoAdRunHeader.rhWindowY
            };
            rc.right = rc.left + this.rsBoxCx;
            rc.bottom = rc.top + this.rsBoxCy;
            this.hoImgWidth = (int) (short) (rc.right - rc.left);
            this.hoImgHeight = (int) (short) (rc.bottom - rc.top);
            this.hoImgXSpot = this.hoImgYSpot = 0;
            short handle1 = this.rsFont;
            if (handle1 == (short) -1)
              handle1 = ocCounters.odFont;
            CFont fontFromHandle = this.hoAdRunHeader.rhApp.fontBank.getFontFromHandle(handle1);
            short num6 = 38;
            int right = rc.right;
            int num7 = CServices.drawText((SpriteBatchEffect) null, s, (short) ((int) num6 | 1024 /*0x0400*/), rc, 0, fontFromHandle, 0, 0);
            rc.right = right;
            if (num7 == 0)
              break;
            this.hoImgXSpot = this.hoImgWidth = (int) (short) (rc.right - rc.left);
            if (this.hoImgHeight < rc.bottom - rc.top)
              this.hoImgHeight = (int) (short) (rc.bottom - rc.top);
            this.hoImgYSpot = this.hoImgHeight;
            break;
          default:
            this.hoImgWidth = this.hoImgHeight = 1;
            break;
        }
      }

      public override void draw(SpriteBatchEffect batch)
      {
        if (this.hoCommon.ocCounters == null)
          return;
        CDefCounters ocCounters = this.hoCommon.ocCounters;
        int rsEffect = this.ros.rsEffect;
        int rsEffectParam = this.ros.rsEffectParam;
        double num1 = 0.0;
        int num2;
        if (this.rsValue.getType() == (byte) 0)
        {
          num2 = this.rsValue.getInt();
        }
        else
        {
          num1 = this.rsValue.getDouble();
          num2 = (int) num1;
        }
        int rsColor1 = this.rsColor1;
        switch (ocCounters.odDisplayType)
        {
          case 1:
            string str = this.rsValue.getType() != (byte) 0 ? CServices.doubleToString(num1, this.displayFlags) : CServices.intToString(num2, this.displayFlags);
            int left1 = this.hoRect.left;
            int top1 = this.hoRect.top;
            for (int index = 0; index < str.Length; ++index)
            {
              char ch = str[index];
              short num3 = 0;
              switch (ch)
              {
                case '+':
                  num3 = ocCounters.frames[11];
                  break;
                case ',':
                case '.':
                  num3 = ocCounters.frames[12];
                  break;
                case '-':
                  num3 = ocCounters.frames[10];
                  break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                  num3 = ocCounters.frames[(int) ch - 48 /*0x30*/];
                  break;
                case 'E':
                case 'e':
                  num3 = ocCounters.frames[13];
                  break;
              }
              this.hoAdRunHeader.rhApp.spriteGen.pasteSpriteEffect(batch, num3, left1, top1, 0, rsEffect, rsEffectParam);
              CImage imageFromHandle = this.hoAdRunHeader.rhApp.imageBank.getImageFromHandle(num3);
              left1 += (int) imageFromHandle.width;
            }
            break;
          case 2:
          case 3:
            int num4 = this.rsBoxCx;
            if (ocCounters.odDisplayType == (short) 2)
              num4 = this.rsBoxCy;
            int width = this.hoRect.right - this.hoRect.left;
            int height = this.hoRect.bottom - this.hoRect.top;
            int left2 = this.hoRect.left;
            int top2 = this.hoRect.top;
            switch (ocCounters.ocFillType)
            {
              case 1:
                Color color = CServices.getColor(rsColor1);
                this.hoAdRunHeader.rhApp.services.drawFilledRectangleSub(batch, left2 + this.hoAdRunHeader.rhApp.xOffset, top2 + this.hoAdRunHeader.rhApp.yOffset, width, height, color, 0, 0);
                return;
              case 2:
                if (this.texture == null)
                {
                  int num5 = this.rsColor1;
                  int rsColor2 = this.rsColor2;
                  int color2 = CServices.RGBJava((CServices.getRValueJava(rsColor2) - CServices.getRValueJava(num5)) * (int) this.rsOldFrame / num4 + CServices.getRValueJava(num5) & (int) byte.MaxValue, (CServices.getGValueJava(rsColor2) - CServices.getGValueJava(num5)) * (int) this.rsOldFrame / num4 + CServices.getGValueJava(num5) & (int) byte.MaxValue, (CServices.getBValueJava(rsColor2) - CServices.getBValueJava(num5)) * (int) this.rsOldFrame / num4 + CServices.getBValueJava(num5) & (int) byte.MaxValue);
                  if (((int) ocCounters.odDisplayFlags & 256 /*0x0100*/) != 0)
                  {
                    int num6 = num5;
                    num5 = color2;
                    color2 = num6;
                  }
                  bool bVertical = ocCounters.ocGradientFlags != 0;
                  this.texture = CServices.createGradientRectangle(this.hoAdRunHeader.rhApp, width, height, num5, color2, bVertical, 0, 0);
                }
                if (this.texture == null)
                  return;
                this.hoAdRunHeader.rhApp.tempRect.X = left2 + this.hoAdRunHeader.rhApp.xOffset;
                this.hoAdRunHeader.rhApp.tempRect.Y = top2 + this.hoAdRunHeader.rhApp.yOffset;
                this.hoAdRunHeader.rhApp.tempRect.Width = this.texture.Width;
                this.hoAdRunHeader.rhApp.tempRect.Height = this.texture.Height;
                batch.Draw(this.texture, this.hoAdRunHeader.rhApp.tempRect, new Rectangle?(), Color.White);
                return;
              default:
                return;
            }
          case 4:
            this.hoAdRunHeader.rhApp.spriteGen.pasteSpriteEffect(batch, ocCounters.frames[Math.Max((int) this.rsOldFrame - 1, 0)], this.hoRect.left, this.hoRect.top, 0, rsEffect, rsEffectParam);
            break;
          case 5:
            string s = this.rsValue.getType() != (byte) 0 ? CServices.doubleToString(num1, this.displayFlags) : CServices.intToString(num2, this.displayFlags);
            short handle = this.rsFont;
            if (handle == (short) -1)
              handle = ocCounters.odFont;
            CFont fontFromHandle = this.hoAdRunHeader.rhApp.fontBank.getFontFromHandle(handle);
            short flags = 38;
            if (this.hoRect.bottom - this.hoRect.top == 0)
              break;
            if (this.tempRc == null)
              this.tempRc = new CRect();
            this.tempRc.copyRect(this.hoRect);
            this.tempRc.offsetRect(this.hoAdRunHeader.rhApp.xOffset, this.hoAdRunHeader.rhApp.yOffset);
            CServices.drawText(batch, s, flags, this.tempRc, this.rsColor1, fontFromHandle, rsEffect, rsEffectParam);
            break;
        }
      }

      public void cpt_ToFloat(CValue pValue)
      {
        if (this.rsValue.getType() == (byte) 0)
        {
          if (pValue.getType() == (byte) 0)
            return;
          this.rsValue.forceDouble((double) this.rsValue.getInt());
          this.display();
          this.roc.rcChanged = true;
        }
        else
          pValue.convertToDouble();
      }

      public void cpt_Change(CValue pValue)
      {
        if (this.rsValue.getType() == (byte) 0)
        {
          int num = pValue.getInt();
          if (num < this.rsMini)
            num = this.rsMini;
          if (num > this.rsMaxi)
            num = this.rsMaxi;
          if (num == this.rsValue.getInt())
            return;
          this.rsValue.forceInt(num);
          this.texture = (Texture2D) null;
          this.modif();
        }
        else
        {
          double num = pValue.getDouble();
          if (num < this.rsMiniDouble)
            num = this.rsMiniDouble;
          if (num > this.rsMaxiDouble)
            num = this.rsMaxiDouble;
          if (num == this.rsValue.getDouble())
            return;
          this.rsValue.forceDouble(num);
          this.texture = (Texture2D) null;
          this.modif();
        }
      }

      public void cpt_Add(CValue pValue)
      {
        this.cpt_ToFloat(pValue);
        CValue pValue1 = new CValue(this.rsValue);
        pValue1.add(pValue);
        this.cpt_Change(pValue1);
      }

      public void cpt_Sub(CValue pValue)
      {
        this.cpt_ToFloat(pValue);
        CValue pValue1 = new CValue(this.rsValue);
        pValue1.sub(pValue);
        this.cpt_Change(pValue1);
      }

      public void cpt_SetMin(CValue value)
      {
        this.rsMini = value.getInt();
        this.rsMiniDouble = value.getDouble();
        this.cpt_Change(new CValue(this.rsValue));
      }

      public void cpt_SetMax(CValue value)
      {
        this.rsMaxi = value.getInt();
        this.rsMaxiDouble = value.getDouble();
        this.cpt_Change(new CValue(this.rsValue));
      }

      public void cpt_SetColor1(int rgb)
      {
        this.rsColor1 = rgb;
        this.display();
        this.roc.rcChanged = true;
      }

      public void cpt_SetColor2(int rgb)
      {
        this.rsColor2 = rgb;
        this.display();
        this.roc.rcChanged = true;
      }

      public CValue cpt_GetValue() => this.rsValue;

      public CValue cpt_GetMin()
      {
        CValue min = new CValue();
        if (this.rsValue.type == (byte) 0)
          min.forceInt(this.rsMini);
        else
          min.forceDouble(this.rsMiniDouble);
        return min;
      }

      public CValue cpt_GetMax()
      {
        CValue max = new CValue();
        if (this.rsValue.type == (byte) 0)
          max.forceInt(this.rsMaxi);
        else
          max.forceDouble(this.rsMaxiDouble);
        return max;
      }

      public int cpt_GetColor1() => this.rsColor1;

      public int cpt_GetColor2() => this.rsColor2;

      public CFontInfo getFont()
      {
        CDefCounters ocCounters = this.hoCommon.ocCounters;
        if (ocCounters.odDisplayType != (short) 5)
          return (CFontInfo) null;
        short handle = this.rsFont;
        if (handle == (short) -1)
          handle = ocCounters.odFont;
        return this.hoAdRunHeader.rhApp.fontBank.getFontInfoFromHandle(handle);
      }

      public void setFont(CFontInfo info, CRect pRc)
      {
        if (this.hoCommon.ocCounters.odDisplayType != (short) 5)
          return;
        this.rsFont = this.hoAdRunHeader.rhApp.fontBank.addFont(info);
        if (pRc != null)
        {
          this.hoImgWidth = this.rsBoxCx = pRc.right - pRc.left;
          this.hoImgHeight = this.rsBoxCy = pRc.bottom - pRc.top;
        }
        this.modif();
        this.roc.rcChanged = true;
      }

      public int getFontColor() => this.rsColor1;

      public void setFontColor(int rgb)
      {
        this.rsColor1 = rgb;
        this.modif();
        this.roc.rcChanged = true;
      }

      public override void drawableDraw(
        SpriteBatchEffect batch,
        CSprite sprite,
        CImageBank bank,
        int x,
        int y)
      {
        this.draw(batch);
      }

      public override void drawableKill()
      {
      }

      public override CMask drawableGetMask(int flags) => (CMask) null;
    }
}
