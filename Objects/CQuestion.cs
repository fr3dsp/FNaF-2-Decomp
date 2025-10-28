// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Objects.CQuestion
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;
using System;

namespace RuntimeXNA.Objects
{

    public class CQuestion : CObject
    {
      private int rsBoxCx;
      private int rsBoxCy;
      private CRect[] rcA;
      private int currentDown;
      private int xMouse;
      private int yMouse;

      public override void init(CObjectCommon ocPtr, CCreateObjectInfo cob)
      {
      }

      public override void handle()
      {
        this.hoAdRunHeader.pause();
        this.hoAdRunHeader.questionObjectOn = this;
      }

      public void handleQuestion()
      {
        this.xMouse = this.hoAdRunHeader.rh2MouseX;
        this.yMouse = this.hoAdRunHeader.rh2MouseX;
        if (this.currentDown == 0)
        {
          if (((int) this.hoAdRunHeader.rh2MouseKeys & 1) == 0)
            return;
          int question = this.getQuestion();
          if (question == 0)
            return;
          this.currentDown = question;
        }
        else
        {
          if (((int) this.hoAdRunHeader.rh2MouseKeys & 1) != 0)
            return;
          if (this.getQuestion() == this.currentDown)
          {
            this.hoAdRunHeader.rhEvtProg.rhCurParam0 = this.currentDown;
            this.hoAdRunHeader.rhEvtProg.handle_Event((CObject) this, -5439484);
            if (((int) ((CDefTexts) this.hoCommon.ocObject).otTexts[this.currentDown].tsFlags & 256 /*0x0100*/) != 0)
              this.hoAdRunHeader.rhEvtProg.handle_Event((CObject) this, -5308412);
            else
              this.hoAdRunHeader.rhEvtProg.handle_Event((CObject) this, -5373948);
            this.hoAdRunHeader.questionObjectOn = (CQuestion) null;
            this.hoAdRunHeader.resume();
            this.hoAdRunHeader.f_KillObject((int) this.hoNumber, true);
          }
          else
            this.currentDown = 0;
        }
      }

      public int getQuestion()
      {
        if (this.rcA != null)
        {
          for (int question = 1; question < this.rcA.Length; ++question)
          {
            if (this.xMouse >= this.rcA[question].left && this.xMouse < this.rcA[question].right && this.yMouse > this.rcA[question].top && this.yMouse < this.rcA[question].bottom)
              return question;
          }
        }
        return 0;
      }

      public virtual void border3D(SpriteBatchEffect batch, CRect rc, bool state)
      {
        int rgb1;
        int rgb2;
        if (state)
        {
          rgb1 = CServices.RGBJava(128 /*0x80*/, 128 /*0x80*/, 128 /*0x80*/);
          rgb2 = CServices.RGBJava((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        }
        else
        {
          rgb2 = CServices.RGBJava(128 /*0x80*/, 128 /*0x80*/, 128 /*0x80*/);
          rgb1 = CServices.RGBJava((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        }
        this.hoAdRunHeader.rhApp.services.drawRect(batch, rc, 0, 0, 0);
        CPoint[] cpointArray = new CPoint[3];
        for (int index = 0; index < 3; ++index)
          cpointArray[index] = new CPoint();
        cpointArray[0].x = rc.right - 1;
        if (!state)
          --cpointArray[0].x;
        cpointArray[0].y = rc.top + 1;
        cpointArray[1].y = rc.top + 1;
        cpointArray[1].x = rc.left + 1;
        cpointArray[2].x = rc.left + 1;
        cpointArray[2].y = rc.bottom;
        if (!state)
          --cpointArray[2].y;
        this.hoAdRunHeader.rhApp.services.drawLine(this.hoAdRunHeader.rhApp.spriteBatch, cpointArray[0].x, cpointArray[0].y, cpointArray[1].x, cpointArray[1].y, rgb1, 1, 0, 0);
        this.hoAdRunHeader.rhApp.services.drawLine(this.hoAdRunHeader.rhApp.spriteBatch, cpointArray[1].x, cpointArray[1].y, cpointArray[2].x, cpointArray[2].y, rgb1, 1, 0, 0);
        if (!state)
          --cpointArray[0].x;
        ++cpointArray[0].y;
        ++cpointArray[1].x;
        ++cpointArray[1].y;
        ++cpointArray[2].x;
        if (!state)
          --cpointArray[2].y;
        this.hoAdRunHeader.rhApp.services.drawLine(this.hoAdRunHeader.rhApp.spriteBatch, cpointArray[0].x, cpointArray[0].y, cpointArray[1].x, cpointArray[1].y, rgb1, 1, 0, 0);
        this.hoAdRunHeader.rhApp.services.drawLine(this.hoAdRunHeader.rhApp.spriteBatch, cpointArray[1].x, cpointArray[1].y, cpointArray[2].x, cpointArray[2].y, rgb1, 1, 0, 0);
        if (state)
          return;
        cpointArray[0].x += 2;
        cpointArray[1].x = rc.right - 1;
        cpointArray[1].y = rc.bottom - 1;
        cpointArray[2].y = rc.bottom - 1;
        --cpointArray[2].x;
        this.hoAdRunHeader.rhApp.services.drawLine(this.hoAdRunHeader.rhApp.spriteBatch, cpointArray[0].x, cpointArray[0].y, cpointArray[1].x, cpointArray[1].y, rgb2, 1, 0, 0);
        this.hoAdRunHeader.rhApp.services.drawLine(this.hoAdRunHeader.rhApp.spriteBatch, cpointArray[1].x, cpointArray[1].y, cpointArray[2].x, cpointArray[2].y, rgb2, 1, 0, 0);
        --cpointArray[0].x;
        ++cpointArray[0].y;
        --cpointArray[1].x;
        --cpointArray[1].y;
        ++cpointArray[2].x;
        --cpointArray[2].y;
        this.hoAdRunHeader.rhApp.services.drawLine(this.hoAdRunHeader.rhApp.spriteBatch, cpointArray[0].x, cpointArray[0].y, cpointArray[1].x, cpointArray[1].y, rgb2, 1, 0, 0);
        this.hoAdRunHeader.rhApp.services.drawLine(this.hoAdRunHeader.rhApp.spriteBatch, cpointArray[1].x, cpointArray[1].y, cpointArray[2].x, cpointArray[2].y, rgb2, 1, 0, 0);
      }

      public void redraw_Answer(
        SpriteBatchEffect batch,
        CDefText ptts,
        CRect lpRc,
        int color,
        bool flgRelief,
        CFont font,
        bool state)
      {
        CRect rc = new CRect();
        rc.copyRect(lpRc);
        this.border3D(batch, lpRc, state);
        rc.left += 2;
        rc.top += 2;
        rc.right -= 4;
        rc.bottom -= 4;
        if (state)
        {
          rc.left += 2;
          rc.top += 2;
        }
        if (flgRelief)
        {
          rc.left += 2;
          rc.top += 2;
          CServices.drawText(batch, ptts.tsText, (short) 37, rc, 16777215 /*0xFFFFFF*/, font, 0, 0);
          rc.left -= 2;
          rc.top -= 2;
        }
        CServices.drawText(batch, ptts.tsText, (short) 37, rc, color, font, 0, 0);
      }

      public override void draw(SpriteBatchEffect batch)
      {
        CDefTexts ocObject = (CDefTexts) this.hoCommon.ocObject;
        CRun hoAdRunHeader = this.hoAdRunHeader;
        int num1 = this.hoX - hoAdRunHeader.rhWindowX;
        int num2 = this.hoY - hoAdRunHeader.rhWindowY;
        CDefText otText1 = ocObject.otTexts[1];
        int tsColor = otText1.tsColor;
        bool flgRelief = ((int) otText1.tsFlags & 512 /*0x0200*/) != 0;
        CFont fontFromHandle1 = hoAdRunHeader.rhApp.fontBank.getFontFromHandle(otText1.tsFont);
        CRect rc1 = new CRect();
        rc1.right = 2000;
        CServices.drawText((SpriteBatchEffect) null, "X", (short) 1024 /*0x0400*/, rc1, tsColor, fontFromHandle1, 0, 0);
        int num3 = rc1.right * 3 / 2;
        int val1_1 = 4;
        int val1_2 = 64 /*0x40*/;
        for (int index = 1; index < ocObject.otTexts.Length; ++index)
        {
          CDefText otText2 = ocObject.otTexts[index];
          if (otText2.tsText.Length > 0)
          {
            rc1.right = 2000;
            rc1.bottom = 0;
            CServices.drawText((SpriteBatchEffect) null, otText2.tsText, (short) 1024 /*0x0400*/, rc1, tsColor, fontFromHandle1, 0, 0);
            val1_2 = Math.Max(val1_2, rc1.right + num3 * 2 + 4);
            val1_1 = Math.Max(val1_1, rc1.bottom * 3 / 2);
          }
        }
        int num4 = Math.Max(val1_1 / 4, 2);
        int val1_3 = val1_2 + (num3 * 2 + 4);
        CDefText otText3 = ocObject.otTexts[0];
        CFont fontFromHandle2 = hoAdRunHeader.rhApp.fontBank.getFontFromHandle(otText3.tsFont);
        rc1.right = 2000;
        rc1.bottom = 0;
        CServices.drawText((SpriteBatchEffect) null, "X", (short) 1024 /*0x0400*/, rc1, tsColor, fontFromHandle2, 0, 0);
        int num5 = rc1.right * 3 / 2;
        rc1.right = 2000;
        rc1.bottom = 0;
        CServices.drawText((SpriteBatchEffect) null, otText3.tsText, (short) 1024 /*0x0400*/, rc1, tsColor, fontFromHandle2, 0, 0);
        int num6 = rc1.bottom * 3 / 2;
        int num7 = Math.Max(val1_3, rc1.right + num5 * 2 + 4);
        if (num7 > hoAdRunHeader.rhApp.gaCxWin)
        {
          num1 += (num7 - hoAdRunHeader.rhApp.gaCxWin) / 2;
          num7 = hoAdRunHeader.rhApp.gaCxWin;
        }
        else if (num7 > hoAdRunHeader.rhFrame.leWidth)
        {
          num1 += (num7 - hoAdRunHeader.rhFrame.leWidth) / 2;
          num7 = hoAdRunHeader.rhFrame.leWidth;
        }
        short num8 = 1;
        if (rc1.right + num5 * 2 + 4 > Math.Min(hoAdRunHeader.rhApp.gaCxWin, hoAdRunHeader.rhFrame.leWidth))
          num8 = (short) 0;
        CRect rc2 = new CRect();
        rc2.left = num1;
        rc2.top = num2;
        this.rsBoxCx = num7;
        this.rsBoxCy = num6 + 1 + (val1_1 + num4) * (ocObject.otTexts.Length - 1) + num4 + 4;
        rc2.right = num1 + this.rsBoxCx;
        rc2.bottom = num2 + this.rsBoxCy;
        hoAdRunHeader.rhApp.services.fillRect(batch, rc2, 12632256 /*0xC0C0C0*/, 0, 0);
        this.border3D(batch, rc2, false);
        rc2.left += 2;
        rc2.top += 2;
        rc2.right -= 2;
        rc2.bottom = rc2.top + num6;
        if (((int) otText3.tsFlags & 512 /*0x0200*/) != 0)
        {
          rc2.left += 2;
          rc2.top += 2;
          CServices.drawText(batch, otText3.tsText, (short) (32 /*0x20*/ | (int) num8 | 4), rc2, 16777215 /*0xFFFFFF*/, fontFromHandle2, 0, 0);
          rc2.left -= 2;
          rc2.top -= 2;
        }
        CServices.drawText(batch, otText3.tsText, (short) (32 /*0x20*/ | (int) num8 | 4), rc2, otText3.tsColor, fontFromHandle2, 0, 0);
        rc2.top = rc2.bottom;
        hoAdRunHeader.rhApp.services.drawLine(this.hoAdRunHeader.rhApp.spriteBatch, rc2.left, rc2.top, rc2.right, rc2.bottom, 8421504 /*0x808080*/, 1, 0, 0);
        ++rc2.top;
        ++rc2.bottom;
        hoAdRunHeader.rhApp.services.drawLine(this.hoAdRunHeader.rhApp.spriteBatch, rc2.left, rc2.top, rc2.right, rc2.bottom, 16777215 /*0xFFFFFF*/, 1, 0, 0);
        if (this.rcA == null)
        {
          this.rcA = new CRect[ocObject.otTexts.Length];
          for (int index = 1; index < ocObject.otTexts.Length; ++index)
          {
            this.rcA[index] = new CRect();
            this.rcA[index].left = num1 + 2 + num3;
            this.rcA[index].right = num1 + num7 - 2 - num3;
            this.rcA[index].top = num2 + 2 + num6 + 1 + num4 + (val1_1 + num4) * (index - 1);
            this.rcA[index].bottom = this.rcA[index].top + val1_1;
          }
        }
        for (int index = 1; index < ocObject.otTexts.Length; ++index)
        {
          CDefText otText4 = ocObject.otTexts[index];
          bool state = this.currentDown == index;
          this.redraw_Answer(batch, otText4, this.rcA[index], tsColor, flgRelief, fontFromHandle1, state);
        }
      }
    }
}
