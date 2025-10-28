// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Objects.CText
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;

namespace RuntimeXNA.Objects
{

    internal class CText : CObject, IDrawing
    {
      public short rsFlag;
      public int rsBoxCx;
      public int rsBoxCy;
      public int rsMaxi;
      public int rsMini;
      public byte rsHidden;
      public string rsTextBuffer;
      public short rsFont;
      public int rsTextColor;
      public int deltaY;

      public override void init(CObjectCommon ocPtr, CCreateObjectInfo cob)
      {
        this.rsFlag = (short) 0;
        CDefTexts ocObject = (CDefTexts) ocPtr.ocObject;
        this.hoImgWidth = ocObject.otCx;
        this.hoImgHeight = ocObject.otCy;
        this.rsBoxCx = ocObject.otCx;
        this.rsBoxCy = ocObject.otCy;
        this.rsMaxi = ocObject.otNumberOfText;
        this.rsTextColor = 0;
        if (ocObject.otTexts.Length > 0)
          this.rsTextColor = ocObject.otTexts[0].tsColor;
        this.rsHidden = (byte) cob.cobFlags;
        this.rsTextBuffer = (string) null;
        this.rsFont = (short) -1;
        this.rsMini = 0;
        if (((int) this.rsHidden & 4) == 0)
          return;
        if (ocObject.otTexts.Length > 0)
          this.rsTextBuffer = ocObject.otTexts[0].tsText;
        else
          this.rsTextBuffer = "";
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
        CDefTexts ocObject = (CDefTexts) this.hoCommon.ocObject;
        short tsFlags = ocObject.otTexts[0].tsFlags;
        CRect rc = new CRect()
        {
          left = this.hoX - this.hoAdRunHeader.rhWindowX + 1,
          top = this.hoY - this.hoAdRunHeader.rhWindowY
        };
        rc.right = rc.left + this.rsBoxCx;
        rc.bottom = rc.top + this.rsBoxCy;
        this.hoImgWidth = (int) (short) (rc.right - rc.left);
        this.hoImgHeight = (int) (short) (rc.bottom - rc.top);
        this.hoImgXSpot = 0;
        this.hoImgYSpot = 0;
        short handle = this.rsFont;
        if (handle == (short) -1 && ocObject.otTexts.Length > 0)
          handle = ocObject.otTexts[0].tsFont;
        CFont fontFromHandle = this.hoAdRunHeader.rhApp.fontBank.getFontFromHandle(handle);
        string s = this.rsMini < 0 ? this.rsTextBuffer ?? "" : ocObject.otTexts[this.rsMini].tsText;
        short num1 = (short) ((int) tsFlags & 47);
        int right = rc.right;
        int num2 = CServices.drawText((SpriteBatchEffect) null, s, (short) ((int) num1 | 1024 /*0x0400*/), rc, this.rsTextColor, fontFromHandle, 0, 0);
        rc.right = right;
        if (num2 == 0)
          return;
        this.deltaY = 0;
        if (((int) num1 & 8) != 0)
          this.deltaY = this.hoImgHeight - num2;
        else if (((int) num1 & 4) != 0)
        {
          this.deltaY = this.hoImgHeight / 2 - num2 / 2;
        }
        else
        {
          this.hoImgWidth = (int) (short) (rc.right - rc.left);
          this.hoImgHeight = (int) (short) (rc.bottom - rc.top);
        }
      }

      public override void draw(SpriteBatchEffect batch)
      {
        int effect = this.ros.rsEffect & 4095 /*0x0FFF*/;
        int rsEffectParam = this.ros.rsEffectParam;
        CDefTexts ocObject = (CDefTexts) this.hoCommon.ocObject;
        short tsFlags = ocObject.otTexts[0].tsFlags;
        short handle = this.rsFont;
        if (handle == (short) -1 && ocObject.otTexts.Length > 0)
          handle = ocObject.otTexts[0].tsFont;
        CFont fontFromHandle = this.hoAdRunHeader.rhApp.fontBank.getFontFromHandle(handle);
        string s = this.rsMini < 0 ? this.rsTextBuffer ?? "" : ocObject.otTexts[this.rsMini].tsText;
        CRect rc = new CRect();
        rc.copyRect(this.hoRect);
        rc.offsetRect(this.hoAdRunHeader.rhApp.xOffset, this.hoAdRunHeader.rhApp.yOffset);
        rc.top += this.deltaY;
        ++rc.left;
        short num = (short) ((int) tsFlags & 47);
        CServices.drawText(batch, s, (short) ((int) num & -13), rc, this.rsTextColor, fontFromHandle, effect, rsEffectParam);
      }

      public CFontInfo getFont()
      {
        short handle = this.rsFont;
        if (handle == (short) -1)
          handle = ((CDefTexts) this.hoCommon.ocObject).otTexts[0].tsFont;
        return this.hoAdRunHeader.rhApp.fontBank.getFontInfoFromHandle(handle);
      }

      public void setFont(CFontInfo info, CRect pRc)
      {
        this.rsFont = this.hoAdRunHeader.rhApp.fontBank.addFont(info);
        if (pRc != null)
        {
          this.hoImgWidth = this.rsBoxCx = pRc.right - pRc.left;
          this.hoImgHeight = this.rsBoxCy = pRc.bottom - pRc.top;
        }
        this.modif();
        this.roc.rcChanged = true;
      }

      public int getFontColor() => this.rsTextColor;

      public void setFontColor(int rgb)
      {
        this.rsTextColor = rgb;
        this.modif();
        this.roc.rcChanged = true;
      }

      public bool txtChange(int num)
      {
        if (num < -1)
          num = -1;
        if (num >= this.rsMaxi)
          num = this.rsMaxi - 1;
        if (num == this.rsMini)
          return false;
        this.rsMini = num;
        if (num >= 0)
          this.txtSetString(((CDefTexts) this.hoCommon.ocObject).otTexts[this.rsMini].tsText);
        return ((int) this.ros.rsFlags & 1) == 0;
      }

      public void txtSetString(string s) => this.rsTextBuffer = s;

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
