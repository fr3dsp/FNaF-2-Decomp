// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Sprites.CRSpr
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Sprites
{

    public class CRSpr
    {
      public const short RSFLAG_HIDDEN = 1;
      public const short RSFLAG_INACTIVE = 2;
      public const short RSFLAG_SLEEPING = 4;
      public const short RSFLAG_SCALE_RESAMPLE = 8;
      public const short RSFLAG_ROTATE_ANTIA = 16 /*0x10*/;
      public const short RSFLAG_VISIBLE = 32 /*0x20*/;
      public const short SPRTYPE_TRUESPRITE = 0;
      public const short SPRTYPE_OWNERDRAW = 1;
      public const short SPRTYPE_QUICKDISPLAY = 2;
      public CObject hoPtr;
      public CSpriteGen spriteGen;
      public int rsFlash;
      public int rsFlashCpt;
      public short rsLayer;
      public int rsZOrder;
      public uint rsCreaFlags;
      public int rsBackColor;
      public int rsEffect;
      public int rsEffectParam;
      public short rsFlags;
      public uint rsFadeCreaFlags;
      public short rsSpriteType;
      public long startFade;

      public void init1(CObject ho, CObjectCommon ocPtr, CCreateObjectInfo cobPtr)
      {
        this.hoPtr = ho;
        this.spriteGen = ho.hoAdRunHeader.rhApp.spriteGen;
        this.rsLayer = (short) cobPtr.cobLayer;
        this.rsZOrder = cobPtr.cobZOrder;
        this.rsCreaFlags = 1U;
        if (((int) this.hoPtr.hoLimitFlags & 256 /*0x0100*/) == 0)
          this.rsCreaFlags &= 4294967294U;
        this.rsBackColor = 0;
        if ((this.hoPtr.hoOEFlags & 4) == 0 || ((int) this.hoPtr.hoOiList.oilOCFlags2 & 1) != 0)
        {
          this.hoPtr.hoOEFlags &= -5;
          this.rsCreaFlags |= 512U /*0x0200*/;
          if (((int) this.hoPtr.hoOiList.oilOCFlags2 & 2) != 0)
          {
            this.rsBackColor = this.hoPtr.hoOiList.oilBackColor;
            this.rsCreaFlags |= 1024U /*0x0400*/;
          }
        }
        if ((this.hoPtr.hoOEFlags & 1024 /*0x0400*/) != 0)
          this.rsCreaFlags |= 16384U /*0x4000*/;
        if (((int) this.hoPtr.hoOiList.oilOCFlags2 & 4) != 0)
          this.rsCreaFlags |= 256U /*0x0100*/;
        if (((int) cobPtr.cobFlags & 2) != 0)
        {
          this.rsCreaFlags |= 128U /*0x80*/;
          this.rsFlags = (short) 1;
          if (this.hoPtr.hoType == (short) 3)
            this.hoPtr.hoFlags |= (short) 8192 /*0x2000*/;
        }
        else
          this.rsFlags |= (short) 32 /*0x20*/;
        this.rsEffect = this.hoPtr.hoOiList.oilInkEffect;
        this.rsEffectParam = this.hoPtr.hoOiList.oilEffectParam;
        if (this.hoPtr.roc.rcMovementType == 0)
        {
          this.rsFlags |= (short) 2;
          this.rsCreaFlags |= 8U;
        }
        this.rsFadeCreaFlags = (uint) (ushort) this.rsCreaFlags;
      }

      public void init2(bool bTransition) => this.createSprite((CSprite) null, bTransition);

      public void displayRoutine()
      {
        switch (this.rsSpriteType)
        {
          case 0:
            if (this.hoPtr.roc.rcSprite == null)
              break;
            this.spriteGen.modifSpriteEx(this.hoPtr.roc.rcSprite, this.hoPtr.hoX - this.hoPtr.hoAdRunHeader.rhWindowX, this.hoPtr.hoY - this.hoPtr.hoAdRunHeader.rhWindowY, this.hoPtr.roc.rcImage, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY, ((int) this.hoPtr.ros.rsFlags & 8) != 0, this.hoPtr.roc.rcAngle, ((int) this.hoPtr.ros.rsFlags & 16 /*0x10*/) != 0);
            break;
          case 1:
            if (this.hoPtr.roc.rcSprite == null)
              break;
            this.spriteGen.activeSprite(this.hoPtr.roc.rcSprite, 1, (CRect) null);
            break;
        }
      }

      public void handle()
      {
        CRun hoAdRunHeader = this.hoPtr.hoAdRunHeader;
        if (((int) this.rsFlags & 4) == 0)
        {
          if (((int) this.hoPtr.hoFlags & 8) != 0)
            this.performFadeIn();
          else if (((int) this.hoPtr.hoFlags & 16 /*0x10*/) != 0)
          {
            this.performFadeOut();
          }
          else
          {
            if (this.rsFlash != 0)
            {
              this.rsFlashCpt -= hoAdRunHeader.rhTimerDelta;
              if (this.rsFlashCpt < 0)
              {
                this.rsFlashCpt = this.rsFlash;
                if (((int) this.rsFlags & 32 /*0x20*/) == 0)
                {
                  this.rsFlags |= (short) 32 /*0x20*/;
                  this.obShow();
                }
                else
                {
                  this.rsFlags &= (short) -33;
                  this.obHide();
                }
              }
            }
            if (this.hoPtr.rom != null)
              this.hoPtr.rom.move();
            if (this.hoPtr.roc.rcPlayer != 0 || (this.hoPtr.hoOEFlags & 16384 /*0x4000*/) != 0)
              return;
            int num1 = this.hoPtr.hoX - this.hoPtr.hoImgXSpot;
            int num2 = this.hoPtr.hoY - this.hoPtr.hoImgYSpot;
            int num3 = num1 + this.hoPtr.hoImgWidth;
            int num4 = num2 + this.hoPtr.hoImgHeight;
            if (num3 >= hoAdRunHeader.rh3XMinimum && num1 <= hoAdRunHeader.rh3XMaximum && num4 >= hoAdRunHeader.rh3YMinimum && num2 <= hoAdRunHeader.rh3YMaximum)
              return;
            if (num3 >= hoAdRunHeader.rh3XMinimumKill && num1 <= hoAdRunHeader.rh3XMaximumKill && num4 >= hoAdRunHeader.rh3YMinimumKill && num2 <= hoAdRunHeader.rh3YMaximumKill)
            {
              this.rsFlags |= (short) 4;
              if (this.hoPtr.roc.rcSprite != null)
              {
                this.rsZOrder = this.hoPtr.roc.rcSprite.sprZOrder;
                this.hoPtr.hoAdRunHeader.rhApp.spriteGen.delSpriteFast(this.hoPtr.roc.rcSprite);
                this.hoPtr.roc.rcSprite = (CSprite) null;
              }
              else
                this.hoPtr.killBack();
            }
            else
            {
              if ((this.hoPtr.hoOEFlags & 8192 /*0x2000*/) != 0)
                return;
              hoAdRunHeader.destroy_Add((int) this.hoPtr.hoNumber);
            }
          }
        }
        else
        {
          int num5 = this.hoPtr.hoX - this.hoPtr.hoImgXSpot;
          int num6 = this.hoPtr.hoY - this.hoPtr.hoImgYSpot;
          int num7 = num5 + this.hoPtr.hoImgWidth;
          int num8 = num6 + this.hoPtr.hoImgHeight;
          if (num7 < hoAdRunHeader.rh3XMinimum || num5 > hoAdRunHeader.rh3XMaximum || num8 < hoAdRunHeader.rh3YMinimum || num6 > hoAdRunHeader.rh3YMaximum)
            return;
          this.rsFlags &= (short) -5;
          this.init2(false);
        }
      }

      public void modifRoutine()
      {
        switch (this.rsSpriteType)
        {
          case 0:
            if (this.hoPtr.roc.rcSprite == null)
              break;
            this.spriteGen.modifSpriteEx(this.hoPtr.roc.rcSprite, this.hoPtr.hoX - this.hoPtr.hoAdRunHeader.rhWindowX, this.hoPtr.hoY - this.hoPtr.hoAdRunHeader.rhWindowY, this.hoPtr.roc.rcImage, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY, ((int) this.hoPtr.ros.rsFlags & 8) != 0, this.hoPtr.roc.rcAngle, ((int) this.hoPtr.ros.rsFlags & 16 /*0x10*/) != 0);
            break;
          case 1:
            this.objGetZoneInfos();
            if (this.hoPtr.roc.rcSprite == null)
              break;
            this.spriteGen.modifOwnerDrawSprite(this.hoPtr.roc.rcSprite, this.hoPtr.hoRect.left, this.hoPtr.hoRect.top, this.hoPtr.hoRect.right, this.hoPtr.hoRect.bottom);
            break;
          case 2:
            this.objGetZoneInfos();
            break;
        }
      }

      public bool createSprite(CSprite pSprBefore, bool bTransition)
      {
        if ((this.hoPtr.hoOEFlags & 32 /*0x20*/) != 0)
        {
          CSprite csprite = this.spriteGen.addSprite(this.hoPtr.hoX - this.hoPtr.hoAdRunHeader.rhWindowX, this.hoPtr.hoY - this.hoPtr.hoAdRunHeader.rhWindowY, this.hoPtr.roc.rcImage, this.rsLayer, this.rsZOrder, this.rsBackColor, this.rsCreaFlags | 536870912U /*0x20000000*/, this.hoPtr);
          if (csprite != null)
          {
            this.hoPtr.roc.rcSprite = csprite;
            this.hoPtr.hoFlags |= (short) 4;
            this.spriteGen.modifSpriteEffect(csprite, this.rsEffect, this.rsEffectParam);
            if (pSprBefore != null)
              this.spriteGen.moveSpriteBefore(csprite, pSprBefore);
            this.rsSpriteType = (short) 0;
            if (bTransition && this.hoPtr.hoCommon.ocFadeInLength != 0)
            {
              this.hoPtr.hoFlags |= (short) 8;
              this.spriteGen.modifSpriteEffect(csprite, 1, 128 /*0x80*/);
              this.hoPtr.hoFlags |= (short) 8192 /*0x2000*/;
              int num = (int) csprite.setSpriteColFlag(0U);
              this.startFade = this.hoPtr.hoAdRunHeader.rhTimer;
            }
          }
          return true;
        }
        if ((this.hoPtr.hoOEFlags & 4096 /*0x1000*/) == 0 || (this.hoPtr.hoOEFlags & 4096 /*0x1000*/) != 0 && this.rsLayer != (short) 0)
        {
          this.rsCreaFlags |= 8200U;
          if (((int) this.rsCreaFlags & 256 /*0x0100*/) == 0)
            this.rsCreaFlags |= 8388608U /*0x800000*/;
          this.rsFlags |= (short) 2;
          this.hoPtr.hoFlags |= (short) 32 /*0x20*/;
          this.hoPtr.hoRect.left = this.hoPtr.hoX - this.hoPtr.hoAdRunHeader.rhWindowX - this.hoPtr.hoImgXSpot;
          this.hoPtr.hoRect.top = this.hoPtr.hoY - this.hoPtr.hoAdRunHeader.rhWindowY - this.hoPtr.hoImgYSpot;
          this.hoPtr.hoRect.right = this.hoPtr.hoRect.left + this.hoPtr.hoImgWidth;
          this.hoPtr.hoRect.bottom = this.hoPtr.hoRect.top + this.hoPtr.hoImgHeight;
          CSprite pSprToMove = this.spriteGen.addOwnerDrawSprite(this.hoPtr.hoRect.left, this.hoPtr.hoRect.top, this.hoPtr.hoRect.right, this.hoPtr.hoRect.bottom, this.rsLayer, this.rsZOrder, this.rsBackColor, this.rsCreaFlags, this.hoPtr, (IDrawing) this.hoPtr);
          if (pSprToMove == null)
            return false;
          this.hoPtr.roc.rcSprite = pSprToMove;
          if (pSprBefore != null)
            this.spriteGen.moveSpriteBefore(pSprToMove, pSprBefore);
          this.rsSpriteType = (short) 1;
          return true;
        }
        this.hoPtr.hoAdRunHeader.add_QuickDisplay(this.hoPtr);
        this.rsSpriteType = (short) 2;
        return true;
      }

      public void performFadeIn()
      {
        long num1 = this.hoPtr.hoAdRunHeader.rhTimer - this.startFade;
        if (num1 >= (long) this.hoPtr.hoCommon.ocFadeInLength)
        {
          this.spriteGen.modifSpriteEffect(this.hoPtr.roc.rcSprite, 1, this.rsEffectParam);
          this.hoPtr.hoFlags &= (short) -9;
          this.hoPtr.hoFlags &= (short) -8193 /*0xDFFF*/;
          int num2 = (int) this.hoPtr.roc.rcSprite.setSpriteColFlag(this.rsCreaFlags & 1U);
        }
        else
          this.spriteGen.modifSpriteEffect(this.hoPtr.roc.rcSprite, 1, (int) (128.0 - (double) (128 /*0x80*/ - this.rsEffectParam) * (double) num1 / (double) this.hoPtr.hoCommon.ocFadeInLength));
      }

      public bool initFadeOut()
      {
        if (this.hoPtr.hoCommon.ocFadeOutLength == 0 || this.hoPtr.roc.rcSprite == null)
          return false;
        this.hoPtr.hoFlags |= (short) 16 /*0x10*/;
        this.hoPtr.hoFlags |= (short) 8192 /*0x2000*/;
        int num = (int) this.hoPtr.roc.rcSprite.setSpriteColFlag(0U);
        this.startFade = this.hoPtr.hoAdRunHeader.rhTimer;
        return true;
      }

      public void performFadeOut()
      {
        long num = this.hoPtr.hoAdRunHeader.rhTimer - this.startFade;
        if (num >= (long) this.hoPtr.hoCommon.ocFadeOutLength)
        {
          this.spriteGen.modifSpriteEffect(this.hoPtr.roc.rcSprite, 1, 128 /*0x80*/);
          this.hoPtr.hoCallRoutine = false;
          this.hoPtr.hoAdRunHeader.destroy_Add((int) this.hoPtr.hoNumber);
        }
        else
          this.spriteGen.modifSpriteEffect(this.hoPtr.roc.rcSprite, 1, (int) ((double) this.rsEffectParam + (double) num / (double) this.hoPtr.hoCommon.ocFadeOutLength * (double) (128 /*0x80*/ - this.rsEffectParam)));
      }

      public bool kill(bool fast)
      {
        bool flag = false;
        if (this.hoPtr.roc.rcSprite != null)
        {
          this.rsZOrder = this.hoPtr.roc.rcSprite.sprZOrder;
          if (!fast)
          {
            flag = ((int) this.hoPtr.roc.rcSprite.sprFlags & 8192 /*0x2000*/) != 0;
            this.spriteGen.delSprite(this.hoPtr.roc.rcSprite);
          }
          else
            this.spriteGen.delSpriteFast(this.hoPtr.roc.rcSprite);
          this.hoPtr.roc.rcSprite = (CSprite) null;
        }
        else if ((this.hoPtr.hoOEFlags & 4096 /*0x1000*/) != 0)
          this.hoPtr.hoAdRunHeader.remove_QuickDisplay(this.hoPtr);
        return flag;
      }

      public void objGetZoneInfos()
      {
        this.hoPtr.getZoneInfos();
        this.hoPtr.hoRect.left = this.hoPtr.hoX - this.hoPtr.hoAdRunHeader.rhWindowX - this.hoPtr.hoImgXSpot;
        this.hoPtr.hoRect.right = this.hoPtr.hoRect.left + this.hoPtr.hoImgWidth;
        this.hoPtr.hoRect.top = this.hoPtr.hoY - this.hoPtr.hoAdRunHeader.rhWindowY - this.hoPtr.hoImgYSpot;
        this.hoPtr.hoRect.bottom = this.hoPtr.hoRect.top + this.hoPtr.hoImgHeight;
      }

      public void obHide()
      {
        if (((int) this.rsFlags & 1) != 0)
          return;
        this.rsFlags |= (short) 1;
        this.rsCreaFlags |= 128U /*0x80*/;
        this.rsFadeCreaFlags |= 128U /*0x80*/;
        this.hoPtr.roc.rcChanged = true;
        if (this.hoPtr.roc.rcSprite == null)
          return;
        this.spriteGen.showSprite(this.hoPtr.roc.rcSprite, false);
      }

      public void obShow()
      {
        if (((int) this.rsFlags & 1) == 0 || (this.hoPtr.hoAdRunHeader.rhFrame.layers[this.hoPtr.hoLayer].dwOptions & 131088 /*0x020010*/) != 16 /*0x10*/)
          return;
        this.rsCreaFlags &= 4294967167U;
        this.rsFadeCreaFlags &= 4294967167U;
        this.rsFlags &= (short) -2;
        this.hoPtr.hoFlags &= (short) -8193 /*0xDFFF*/;
        this.hoPtr.roc.rcChanged = true;
        if (this.hoPtr.roc.rcSprite == null)
          return;
        this.hoPtr.hoAdRunHeader.rhApp.spriteGen.showSprite(this.hoPtr.roc.rcSprite, true);
      }

      public void modifSpriteEffect(int effect, int effectParam)
      {
        this.rsEffect &= -4096;
        this.rsEffect |= effect;
        this.rsEffectParam = effectParam;
        this.hoPtr.roc.rcChanged = true;
        if (this.hoPtr.roc.rcSprite == null)
          return;
        this.spriteGen.modifSpriteEffect(this.hoPtr.roc.rcSprite, this.rsEffect, this.rsEffectParam);
      }
    }
}
