// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Objects.CScore
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Expressions;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;
using System;

namespace RuntimeXNA.Objects
{

    internal class CScore : CObject, IDrawing
    {
      public short rsPlayer;
      public CValue rsValue;
      public int rsBoxCx;
      public int rsBoxCy;
      public short rsFont;
      public int rsColor1;
      public int displayFlags;
      public CRect tempRc;

      public override void init(CObjectCommon ocPtr, CCreateObjectInfo cob)
      {
        this.rsFont = (short) -1;
        this.rsColor1 = 0;
        this.hoImgWidth = this.hoImgHeight = 1;
        CDefCounters ocCounters = this.hoCommon.ocCounters;
        this.hoImgWidth = this.rsBoxCx = ocCounters.odCx;
        this.hoImgHeight = this.rsBoxCy = ocCounters.odCy;
        this.rsColor1 = ocCounters.ocColor1;
        this.rsPlayer = ocCounters.odPlayer;
        this.displayFlags = (int) ocCounters.odDisplayFlags;
        this.rsValue = new CValue(this.hoAdRunHeader.rhApp.getScores()[(int) this.rsPlayer - 1]);
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
        string s = CServices.intToString(this.rsValue.getInt(), this.displayFlags);
        switch (ocCounters.odDisplayType)
        {
          case 1:
            int num1 = 0;
            int val1 = 0;
            for (int index = 0; index < s.Length; ++index)
            {
              char ch = s[index];
              short handle = 0;
              switch (ch)
              {
                case '+':
                  handle = ocCounters.frames[11];
                  break;
                case '-':
                  handle = ocCounters.frames[10];
                  break;
                case '.':
                  handle = ocCounters.frames[12];
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
              num1 += (int) imageFromHandle.width;
              val1 = Math.Max(val1, (int) imageFromHandle.height);
            }
            this.hoImgWidth = num1;
            this.hoImgHeight = val1;
            this.hoImgXSpot = num1;
            this.hoImgYSpot = val1;
            break;
          case 5:
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
            short num2 = 38;
            int right = rc.right;
            int num3 = CServices.drawText((SpriteBatchEffect) null, s, (short) ((int) num2 | 1024 /*0x0400*/), rc, 0, fontFromHandle, 0, 0);
            rc.right = right;
            if (num3 == 0)
              break;
            this.hoImgXSpot = this.hoImgWidth = (int) (short) (rc.right - rc.left);
            if (this.hoImgHeight < rc.bottom - rc.top)
              this.hoImgHeight = (int) (short) (rc.bottom - rc.top);
            this.hoImgYSpot = this.hoImgHeight;
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
        string s = CServices.intToString(this.rsValue.getInt(), this.displayFlags);
        switch (ocCounters.odDisplayType)
        {
          case 1:
            int left = this.hoRect.left;
            int top = this.hoRect.top;
            for (int index = 0; index < s.Length; ++index)
            {
              char ch = s[index];
              short num = 0;
              switch (ch)
              {
                case '+':
                  num = ocCounters.frames[11];
                  break;
                case '-':
                  num = ocCounters.frames[10];
                  break;
                case '.':
                  num = ocCounters.frames[12];
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
                  num = ocCounters.frames[(int) ch - 48 /*0x30*/];
                  break;
                case 'E':
                case 'e':
                  num = ocCounters.frames[13];
                  break;
              }
              this.hoAdRunHeader.rhApp.spriteGen.pasteSpriteEffect(batch, num, left, top, 0, rsEffect, rsEffectParam);
              CImage imageFromHandle = this.hoAdRunHeader.rhApp.imageBank.getImageFromHandle(num);
              left += (int) imageFromHandle.width;
            }
            break;
          case 5:
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

      public override CMask drawableGetMask(int mask) => (CMask) null;
    }
}
