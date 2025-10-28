// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Sprites.CSpriteGen
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RuntimeXNA.Application;
using RuntimeXNA.Banks;
using RuntimeXNA.Objects;
using RuntimeXNA.Services;
using System;

namespace RuntimeXNA.Sprites
{

    public class CSpriteGen
    {
      public const int AS_DEACTIVATE = 0;
      public const int AS_REDRAW = 1;
      public const int AS_ACTIVATE = 2;
      public const int AS_ENABLE = 4;
      public const int AS_DISABLE = 8;
      public const int AS_REDRAW_RECT = 32 /*0x20*/;
      public const int GS_BACKGROUND = 1;
      public const int GS_SAMELAYER = 2;
      public const short CM_BOX = 0;
      public const short CM_BITMAP = 1;
      public const short PSCF_CURRENTSURFACE = 1;
      public const short PSCF_TEMPSURFACE = 2;
      public const short LAYER_ALL = -1;
      public const int BOP_COPY = 0;
      public const int BOP_BLEND = 1;
      public const int BOP_INVERT = 2;
      public const int BOP_XOR = 3;
      public const int BOP_AND = 4;
      public const int BOP_OR = 5;
      public const int BOP_BLEND_REPLACETRANSP = 6;
      public const int BOP_DWROP = 7;
      public const int BOP_ANDNOT = 8;
      public const int BOP_ADD = 9;
      public const int BOP_MONO = 10;
      public const int BOP_SUB = 11;
      public const int BOP_BLEND_DONTREPLACECOLOR = 12;
      public const int BOP_EFFECTEX = 13;
      public const int BOP_MAX = 14;
      public const int EFFECTFLAG_TRANSPARENT = 268435456 /*0x10000000*/;
      public const int EFFECTFLAG_ANTIALIAS = 536870912 /*0x20000000*/;
      public const int BOP_MASK = 4095 /*0x0FFF*/;
      public const int BOP_RGBAFILTER = 4096 /*0x1000*/;
      public const int SCF_OBSTACLE = 1;
      public const int SCF_PLATFORM = 2;
      public const int SCF_EVENNOCOL = 4;
      public const int SCF_BACKGROUND = 8;
      public const int PSF_HOTSPOT = 1;
      public const int PSF_NOTRANSP = 2;
      public CSprite firstSprite;
      public CSprite lastSprite;
      private CRunFrame frame;
      private CRunApp app;
      private CImageBank bank;
      public short colMode;
      public Rectangle tempRect = new Rectangle();
      public int xOffset;
      public int yOffset;
      private Vector2 vector = new Vector2();
      private Color bColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);

      public CSpriteGen()
      {
        this.firstSprite = (CSprite) null;
        this.lastSprite = (CSprite) null;
      }

      public void setData(CImageBank b, CRunApp a, CRunFrame f)
      {
        this.bank = b;
        this.frame = f;
        this.app = a;
      }

      public void setOffsets(int x, int y)
      {
        this.xOffset = x;
        this.yOffset = y;
      }

      public CSprite addSprite(
        int xSpr,
        int ySpr,
        short iSpr,
        short wLayer,
        int nZOrder,
        int backSpr,
        uint sFlags,
        CObject extraInfo)
      {
        CSprite ptSprOrg = this.winAllocSprite();
        ptSprOrg.bank = this.bank;
        ptSprOrg.sprFlags = sFlags | 64U /*0x40*/;
        ptSprOrg.sprFlags &= 4294938589U;
        ptSprOrg.sprLayer = (short) ((int) wLayer * 2);
        if (((int) sFlags & 524288 /*0x080000*/) == 0)
          ++ptSprOrg.sprLayer;
        ptSprOrg.sprZOrder = nZOrder;
        ptSprOrg.sprX = ptSprOrg.sprXnew = xSpr;
        ptSprOrg.sprY = ptSprOrg.sprYnew = ySpr;
        ptSprOrg.sprImg = ptSprOrg.sprImgNew = iSpr;
        ptSprOrg.sprExtraInfo = extraInfo;
        ptSprOrg.sprEffect = 268435456 /*0x10000000*/;
        ptSprOrg.sprEffectParam = 0;
        ptSprOrg.sprScaleX = ptSprOrg.sprScaleY = ptSprOrg.sprScaleXnew = ptSprOrg.sprScaleYnew = 1f;
        ptSprOrg.sprAngle = ptSprOrg.sprAnglenew = (short) 0;
        ptSprOrg.sprX1z = ptSprOrg.sprY1z = -1;
        ptSprOrg.sprBackColor = 0;
        if (((int) sFlags & 1024 /*0x0400*/) != 0)
          ptSprOrg.sprBackColor = backSpr;
        ptSprOrg.updateBoundingBox();
        ptSprOrg.sprX1 = ptSprOrg.sprX1new;
        ptSprOrg.sprY1 = ptSprOrg.sprY1new;
        ptSprOrg.sprX2 = ptSprOrg.sprX2new;
        ptSprOrg.sprY2 = ptSprOrg.sprY2new;
        this.sortLastSprite(ptSprOrg);
        return ptSprOrg;
      }

      public CSprite addOwnerDrawSprite(
        int x1,
        int y1,
        int x2,
        int y2,
        short wLayer,
        int nZOrder,
        int backSpr,
        uint sFlags,
        CObject extraInfo,
        IDrawing sprProc)
      {
        CSprite ptSprOrg = this.winAllocSprite();
        ptSprOrg.sprX = ptSprOrg.sprXnew = x1;
        ptSprOrg.sprY = ptSprOrg.sprYnew = y1;
        ptSprOrg.sprX1new = ptSprOrg.sprX1 = x1;
        ptSprOrg.sprY1new = ptSprOrg.sprY1 = y1;
        ptSprOrg.sprX2new = ptSprOrg.sprX2 = x2;
        ptSprOrg.sprY2new = ptSprOrg.sprY2 = y2;
        ptSprOrg.sprX1z = ptSprOrg.sprY1z = -1;
        ptSprOrg.sprLayer = (short) ((int) wLayer * 2);
        if (((int) sFlags & 524288 /*0x080000*/) == 0)
          ++ptSprOrg.sprLayer;
        ptSprOrg.sprZOrder = nZOrder;
        ptSprOrg.sprExtraInfo = extraInfo;
        ptSprOrg.sprRout = sprProc;
        ptSprOrg.sprFlags = sFlags | 8192U /*0x2000*/;
        ptSprOrg.sprFlags &= 4294963165U;
        ptSprOrg.sprEffect = 268435456 /*0x10000000*/;
        ptSprOrg.sprEffectParam = 0;
        ptSprOrg.sprScaleX = ptSprOrg.sprScaleY = ptSprOrg.sprScaleXnew = ptSprOrg.sprScaleYnew = 1f;
        ptSprOrg.sprAngle = ptSprOrg.sprAnglenew = (short) 0;
        ptSprOrg.sprBackColor = 0;
        if (((int) sFlags & 1024 /*0x0400*/) != 0)
          ptSprOrg.sprBackColor = backSpr;
        this.sortLastSprite(ptSprOrg);
        return ptSprOrg;
      }

      public CSprite modifSprite(CSprite ptSpr, int xSpr, int ySpr, short iSpr)
      {
        if (ptSpr != null && (ptSpr.sprXnew != xSpr || ptSpr.sprYnew != ySpr || (int) ptSpr.sprImgNew != (int) iSpr))
        {
          ptSpr.sprXnew = xSpr;
          ptSpr.sprYnew = ySpr;
          ptSpr.sprImgNew = iSpr;
          ptSpr.updateBoundingBox();
          ptSpr.sprFlags |= 64U /*0x40*/;
        }
        return ptSpr;
      }

      public CSprite modifSpriteEx(
        CSprite ptSpr,
        int xSpr,
        int ySpr,
        short iSpr,
        float fScaleX,
        float fScaleY,
        bool bResample,
        int nAngle,
        bool bAntiA)
      {
        if (ptSpr != null)
        {
          if ((double) fScaleX < 0.0)
            fScaleX = 0.0f;
          if ((double) fScaleY < 0.0)
            fScaleY = 0.0f;
          nAngle %= 360;
          if (nAngle < 0)
            nAngle += 360;
          if (ptSpr.sprXnew != xSpr || ptSpr.sprYnew != ySpr || (int) ptSpr.sprImgNew != (int) iSpr || (double) fScaleX != (double) ptSpr.sprScaleX || (double) fScaleY != (double) ptSpr.sprScaleY || nAngle != (int) ptSpr.sprAngle)
          {
            ptSpr.sprXnew = xSpr;
            ptSpr.sprYnew = ySpr;
            ptSpr.sprImgNew = iSpr;
            ptSpr.sprScaleXnew = fScaleX;
            ptSpr.sprScaleYnew = fScaleY;
            ptSpr.sprAnglenew = (short) nAngle;
            ptSpr.updateBoundingBox();
            ptSpr.sprFlags |= 64U /*0x40*/;
          }
        }
        return ptSpr;
      }

      public CSprite modifSpriteEffect(CSprite ptSpr, int eff, int effectParam)
      {
        if (ptSpr != null)
        {
          ptSpr.sprEffect = eff & 4095 /*0x0FFF*/;
          ptSpr.sprEffectParam = effectParam;
          ptSpr.rgb = Color.White;
          float num = 1f;
          if ((eff & 4096 /*0x1000*/) != 0)
          {
            ptSpr.rgb = CServices.getColorAlpha(effectParam & 16777215 /*0xFFFFFF*/);
            num = (float) (effectParam >> 24 & (int) byte.MaxValue) / (float) byte.MaxValue;
          }
          else if (ptSpr.sprEffect == 1)
            num = (float) (128 /*0x80*/ - ptSpr.sprEffectParam) / 128f;
          ptSpr.rgb *= num;
          ptSpr.sprFlags |= 64U /*0x40*/;
        }
        return ptSpr;
      }

      public CSprite modifOwnerDrawSprite(CSprite ptSprModif, int x1, int y1, int x2, int y2)
      {
        if (ptSprModif != null)
        {
          ptSprModif.sprX1new = x1;
          ptSprModif.sprY1new = y1;
          ptSprModif.sprX2new = x2;
          ptSprModif.sprY2new = y2;
          ptSprModif.sprFlags |= 64U /*0x40*/;
        }
        return ptSprModif;
      }

      public void setSpriteLayer(CSprite ptSpr, int nLayer)
      {
        if (ptSpr == null)
          return;
        int num = nLayer * 2;
        if (((int) ptSpr.sprFlags & 524288 /*0x080000*/) == 0)
          ++num;
        if ((int) ptSpr.sprLayer == (int) (short) num)
          return;
        int sprLayer = (int) ptSpr.sprLayer;
        ptSpr.sprLayer = (short) num;
        if (sprLayer < num)
        {
          if (this.lastSprite != null)
          {
            while (ptSpr != this.lastSprite)
            {
              CSprite objNext = ptSpr.objNext;
              if (objNext != null && (int) objNext.sprLayer <= (int) (short) num)
              {
                int sprZorder1 = ptSpr.sprZOrder;
                int sprZorder2 = objNext.sprZOrder;
                this.swapSprites(ptSpr, objNext);
                ptSpr.sprZOrder = sprZorder1;
                objNext.sprZOrder = sprZorder2;
              }
              else
                break;
            }
          }
        }
        else if (this.firstSprite != null)
        {
          while (ptSpr != this.firstSprite)
          {
            CSprite objPrev = ptSpr.objPrev;
            if (objPrev != null && (int) objPrev.sprLayer > (int) (short) num)
            {
              int sprZorder3 = ptSpr.sprZOrder;
              int sprZorder4 = objPrev.sprZOrder;
              this.swapSprites(objPrev, ptSpr);
              ptSpr.sprZOrder = sprZorder3;
              objPrev.sprZOrder = sprZorder4;
            }
            else
              break;
          }
        }
        CSprite objPrev1 = ptSpr.objPrev;
        if (objPrev1 == null || (int) objPrev1.sprLayer != (int) ptSpr.sprLayer)
          ptSpr.sprZOrder = 1;
        else
          ptSpr.sprZOrder = objPrev1.sprZOrder + 1;
      }

      public void setSpriteScale(CSprite ptSpr, float fScaleX, float fScaleY, bool bResample)
      {
        if (ptSpr == null)
          return;
        if ((double) fScaleX < 0.0)
          fScaleX = 0.0f;
        if ((double) fScaleY < 0.0)
          fScaleY = 0.0f;
        bool flag = ((int) ptSpr.sprFlags & 1048576 /*0x100000*/) != 0;
        if ((double) ptSpr.sprScaleX == (double) fScaleX && (double) ptSpr.sprScaleY == (double) fScaleY && bResample == flag)
          return;
        ptSpr.sprScaleXnew = fScaleX;
        ptSpr.sprScaleYnew = fScaleY;
        ptSpr.sprFlags |= 64U /*0x40*/;
        ptSpr.sprFlags &= 4293918719U;
        ptSpr.updateBoundingBox();
      }

      public void setSpriteAngle(CSprite ptSpr, int nAngle, bool bAntiA)
      {
        if (ptSpr == null)
          return;
        nAngle %= 360;
        if (nAngle < 0)
          nAngle += 360;
        if ((int) ptSpr.sprAngle == nAngle)
          return;
        ptSpr.sprAnglenew = (short) nAngle;
        ptSpr.sprFlags &= 4292870143U;
        ptSpr.sprFlags |= 64U /*0x40*/;
        ptSpr.updateBoundingBox();
      }

      public void sortLastSprite(CSprite ptSprOrg)
      {
        CSprite csprite = ptSprOrg;
        short sprLayer = csprite.sprLayer;
        CSprite objPrev1;
        for (objPrev1 = csprite.objPrev; objPrev1 != null && (int) sprLayer < (int) objPrev1.sprLayer; objPrev1 = csprite.objPrev)
        {
          CSprite objPrev2 = objPrev1.objPrev;
          if (objPrev2 == null)
            this.firstSprite = csprite;
          else
            objPrev2.objNext = csprite;
          CSprite objNext = csprite.objNext;
          if (objNext == null)
            this.lastSprite = objPrev1;
          else
            objNext.objPrev = objPrev1;
          csprite.objPrev = objPrev1.objPrev;
          objPrev1.objPrev = csprite;
          objPrev1.objNext = csprite.objNext;
          csprite.objNext = objPrev1;
        }
        if (objPrev1 == null || (int) sprLayer != (int) objPrev1.sprLayer)
          return;
        for (int sprZorder = csprite.sprZOrder; objPrev1 != null && (int) sprLayer == (int) objPrev1.sprLayer && sprZorder < objPrev1.sprZOrder; objPrev1 = csprite.objPrev)
        {
          CSprite objPrev3 = objPrev1.objPrev;
          if (objPrev3 == null)
            this.firstSprite = csprite;
          else
            objPrev3.objNext = csprite;
          CSprite objNext = csprite.objNext;
          if (objNext == null)
            this.lastSprite = objPrev1;
          else
            objNext.objPrev = objPrev1;
          csprite.objPrev = objPrev1.objPrev;
          objPrev1.objPrev = csprite;
          objPrev1.objNext = csprite.objNext;
          csprite.objNext = objPrev1;
        }
      }

      public void swapSprites(CSprite sp1, CSprite sp2)
      {
        if (sp1 == sp2)
          return;
        CSprite objPrev1 = sp1.objPrev;
        CSprite objNext1 = sp1.objNext;
        CSprite objPrev2 = sp2.objPrev;
        CSprite objNext2 = sp2.objNext;
        int sprZorder = sp1.sprZOrder;
        sp1.sprZOrder = sp2.sprZOrder;
        sp2.sprZOrder = sprZorder;
        if (objNext1 == sp2)
        {
          if (objPrev1 != null)
            objPrev1.objNext = sp2;
          sp2.objPrev = objPrev1;
          sp2.objNext = sp1;
          sp1.objPrev = sp2;
          sp1.objNext = objNext2;
          if (objNext2 != null)
            objNext2.objPrev = sp1;
          if (objPrev1 == null)
            this.firstSprite = sp2;
          if (objNext2 != null)
            return;
          this.lastSprite = sp1;
        }
        else if (objNext2 == sp1)
        {
          if (objPrev2 != null)
            objPrev2.objNext = sp1;
          sp1.objPrev = objPrev2;
          sp1.objNext = sp2;
          sp2.objPrev = sp1;
          sp2.objNext = objNext1;
          if (objNext1 != null)
            objNext1.objPrev = sp2;
          if (objPrev2 == null)
            this.firstSprite = sp1;
          if (objNext1 != null)
            return;
          this.lastSprite = sp2;
        }
        else
        {
          if (objPrev1 != null)
            objPrev1.objNext = sp2;
          if (objNext1 != null)
            objNext1.objPrev = sp2;
          sp1.objPrev = objPrev2;
          sp1.objNext = objNext2;
          if (objPrev2 != null)
            objPrev2.objNext = sp1;
          if (objNext2 != null)
            objNext2.objPrev = sp1;
          sp2.objPrev = objPrev1;
          sp2.objNext = objNext1;
          if (objPrev1 == null)
            this.firstSprite = sp2;
          if (objPrev2 == null)
            this.firstSprite = sp1;
          if (objNext1 == null)
            this.lastSprite = sp2;
          if (objNext2 != null)
            return;
          this.lastSprite = sp1;
        }
      }

      public void moveSpriteToFront(CSprite pSpr)
      {
        if (this.lastSprite == null)
          return;
        int sprLayer = (int) pSpr.sprLayer;
        while (pSpr != this.lastSprite)
        {
          CSprite objNext = pSpr.objNext;
          if (objNext == null || (int) objNext.sprLayer > sprLayer)
            break;
          this.swapSprites(pSpr, objNext);
        }
      }

      public void moveSpriteToBack(CSprite pSpr)
      {
        if (this.lastSprite == null)
          return;
        int sprLayer = (int) pSpr.sprLayer;
        while (pSpr != this.firstSprite)
        {
          CSprite objPrev = pSpr.objPrev;
          if (objPrev == null || (int) objPrev.sprLayer < sprLayer)
            break;
          this.swapSprites(objPrev, pSpr);
        }
      }

      public void moveSpriteBefore(CSprite pSprToMove, CSprite pSprDest)
      {
        if ((int) pSprToMove.sprLayer != (int) pSprDest.sprLayer)
          return;
        CSprite objPrev1 = pSprToMove.objPrev;
        while (objPrev1 != null && objPrev1 != pSprDest)
          objPrev1 = objPrev1.objPrev;
        if (objPrev1 == null)
          return;
        CSprite objPrev2;
        do
        {
          objPrev2 = pSprToMove.objPrev;
          if (objPrev2 == null)
            break;
          this.swapSprites(pSprToMove, objPrev2);
        }
        while (objPrev2 != pSprDest);
      }

      public void moveSpriteAfter(CSprite pSprToMove, CSprite pSprDest)
      {
        if ((int) pSprToMove.sprLayer != (int) pSprDest.sprLayer)
          return;
        CSprite objNext1 = pSprToMove.objNext;
        while (objNext1 != null && objNext1 != pSprDest)
          objNext1 = objNext1.objNext;
        if (objNext1 == null)
          return;
        CSprite objNext2;
        do
        {
          objNext2 = pSprToMove.objNext;
          if (objNext2 == null)
            break;
          this.swapSprites(pSprToMove, objNext2);
        }
        while (objNext2 != pSprDest);
      }

      public bool isSpriteBefore(CSprite pSpr, CSprite pSprDest)
      {
        return (int) pSpr.sprLayer < (int) pSprDest.sprLayer || (int) pSpr.sprLayer <= (int) pSprDest.sprLayer && pSpr.sprZOrder < pSprDest.sprZOrder;
      }

      public bool isSpriteAfter(CSprite pSpr, CSprite pSprDest)
      {
        return (int) pSpr.sprLayer > (int) pSprDest.sprLayer || (int) pSpr.sprLayer >= (int) pSprDest.sprLayer && pSpr.sprZOrder > pSprDest.sprZOrder;
      }

      public CSprite getFirstSprite(int nLayer, int dwFlags)
      {
        CSprite firstSprite = this.firstSprite;
        int num = nLayer;
        if (nLayer != -1)
        {
          num *= 2;
          if ((dwFlags & 1) == 0)
            ++num;
        }
        for (; firstSprite != null && num != -1 && (int) firstSprite.sprLayer != num; firstSprite = firstSprite.objNext)
        {
          if ((int) firstSprite.sprLayer > num)
          {
            firstSprite = (CSprite) null;
            break;
          }
        }
        return firstSprite;
      }

      public CSprite getNextSprite(CSprite pSpr, int dwFlags)
      {
        if (pSpr != null)
        {
          int sprLayer = (int) pSpr.sprLayer;
          if ((dwFlags & 1) != 0)
          {
            while ((pSpr = pSpr.objNext) != null)
            {
              if (((int) pSpr.sprFlags & 524288 /*0x080000*/) == 0)
              {
                if ((dwFlags & 2) != 0)
                {
                  pSpr = (CSprite) null;
                  break;
                }
              }
              else
              {
                if ((dwFlags & 2) != 0 && (int) pSpr.sprLayer != sprLayer)
                {
                  pSpr = (CSprite) null;
                  break;
                }
                break;
              }
            }
          }
          else
          {
            while ((pSpr = pSpr.objNext) != null)
            {
              if (((int) pSpr.sprFlags & 524288 /*0x080000*/) != 0)
              {
                if ((dwFlags & 2) != 0)
                {
                  pSpr = (CSprite) null;
                  break;
                }
              }
              else
              {
                if ((dwFlags & 2) != 0 && (int) pSpr.sprLayer != sprLayer)
                {
                  pSpr = (CSprite) null;
                  break;
                }
                break;
              }
            }
          }
        }
        return pSpr;
      }

      public CSprite getPrevSprite(CSprite pSpr, int dwFlags)
      {
        if (pSpr != null)
        {
          int sprLayer = (int) pSpr.sprLayer;
          if ((dwFlags & 1) != 0)
          {
            while ((pSpr = pSpr.objPrev) != null)
            {
              if (((int) pSpr.sprFlags & 524288 /*0x080000*/) == 0)
              {
                if ((dwFlags & 2) != 0)
                {
                  pSpr = (CSprite) null;
                  break;
                }
              }
              else
              {
                if ((dwFlags & 2) != 0 && (int) pSpr.sprLayer != sprLayer)
                {
                  pSpr = (CSprite) null;
                  break;
                }
                break;
              }
            }
          }
          else
          {
            while ((pSpr = pSpr.objPrev) != null)
            {
              if (((int) pSpr.sprFlags & 524288 /*0x080000*/) != 0)
              {
                if ((dwFlags & 2) != 0)
                {
                  pSpr = (CSprite) null;
                  break;
                }
              }
              else
              {
                if ((dwFlags & 2) != 0 && (int) pSpr.sprLayer != sprLayer)
                {
                  pSpr = (CSprite) null;
                  break;
                }
                break;
              }
            }
          }
        }
        return pSpr;
      }

      public void showSprite(CSprite ptSpr, bool showFlag)
      {
        if (ptSpr == null)
          return;
        if (showFlag)
        {
          if (((int) ptSpr.sprFlags & 128 /*0x80*/) == 0)
            return;
          ptSpr.sprFlags &= 4294967167U;
          ptSpr.sprFlags |= 64U /*0x40*/;
        }
        else
        {
          if (((int) ptSpr.sprFlags & 128 /*0x80*/) != 0)
            return;
          ptSpr.sprFlags |= 128U /*0x80*/;
          ptSpr.sprFlags |= 64U /*0x40*/;
        }
      }

      public void killSprite(CSprite ptSprToKill)
      {
        CSprite spr = ptSprToKill;
        if (((int) spr.sprFlags & 8192 /*0x2000*/) != 0)
          spr.sprRout.drawableKill();
        this.winFreeSprite(spr);
      }

      public void activeSprite(CSprite ptSpr, int activeFlag, CRect reafRect)
      {
        if (ptSpr != null)
        {
          switch (activeFlag)
          {
            case 0:
              ptSpr.sprFlags |= 72U;
              break;
            case 1:
              ptSpr.sprFlags |= 64U /*0x40*/;
              break;
            case 2:
              ptSpr.sprFlags &= 4294967287U;
              break;
            case 4:
              ptSpr.sprFlags &= 4294965247U;
              break;
            case 8:
              ptSpr.sprFlags |= 2048U /*0x0800*/;
              break;
          }
        }
        else
        {
          for (ptSpr = this.firstSprite; ptSpr != null; ptSpr = ptSpr.objNext)
          {
            switch (activeFlag)
            {
              case 0:
                ptSpr.sprFlags |= 72U;
                break;
              case 1:
                ptSpr.sprFlags |= 64U /*0x40*/;
                break;
              case 2:
                ptSpr.sprFlags &= 4294967287U;
                break;
              case 4:
                ptSpr.sprFlags &= 4294965247U;
                break;
              case 8:
                ptSpr.sprFlags |= 2048U /*0x0800*/;
                break;
              case 17:
                if (((int) ptSpr.sprFlags & 524416 /*0x080080*/) == 0)
                {
                  ptSpr.sprFlags |= 64U /*0x40*/;
                  break;
                }
                break;
              default:
                ptSpr.sprFlags &= 4294967287U;
                break;
            }
          }
        }
      }

      public void delSprite(CSprite ptSprToDel) => this.killSprite(ptSprToDel);

      public void delSpriteFast(CSprite ptSpr) => this.killSprite(ptSpr);

      public CMask getSpriteMask(
        CSprite ptSpr,
        short newImg,
        int nFlags,
        int newAngle,
        float newScaleX,
        float newScaleY)
      {
        if (ptSpr != null)
        {
          if (((int) ptSpr.sprFlags & 8192 /*0x2000*/) != 0)
            return ptSpr.sprRout.drawableGetMask(nFlags);
          short handle = newImg;
          if (handle == (short) -1)
            handle = ptSpr.sprImg;
          if (handle != (short) -1)
            return this.bank.getImageFromHandle(handle).getMask(nFlags, newAngle, newScaleX, newScaleY);
        }
        return (CMask) null;
      }

      public void spriteUpdate()
      {
        for (CSprite csprite = this.firstSprite; csprite != null; csprite = csprite.objNext)
        {
          if (((int) csprite.sprFlags & 64 /*0x40*/) != 0)
          {
            csprite.sprX = csprite.sprXnew;
            csprite.sprY = csprite.sprYnew;
            csprite.sprX1 = csprite.sprX1new;
            csprite.sprY1 = csprite.sprY1new;
            csprite.sprX2 = csprite.sprX2new;
            csprite.sprY2 = csprite.sprY2new;
            csprite.sprScaleX = csprite.sprScaleXnew;
            csprite.sprScaleY = csprite.sprScaleYnew;
            csprite.sprAngle = csprite.sprAnglenew;
            if (((int) csprite.sprFlags & 8192 /*0x2000*/) == 0)
              csprite.sprImg = csprite.sprImgNew;
          }
        }
      }

      public void pasteSpriteEffect(
        SpriteBatchEffect batch,
        short iNum,
        int iX,
        int iY,
        int flags,
        int effect,
        int effectParam)
      {
        CImage imageFromHandle = this.bank.getImageFromHandle(iNum);
        if (imageFromHandle == null)
          return;
        int num1 = iX;
        if ((flags & 1) != 0)
          num1 -= (int) imageFromHandle.xSpot;
        int num2 = iY;
        if ((flags & 1) != 0)
          num2 -= (int) imageFromHandle.ySpot;
        this.tempRect.X = num1 + this.xOffset;
        this.tempRect.Y = num2 + this.yOffset;
        this.tempRect.Width = (int) imageFromHandle.width;
        this.tempRect.Height = (int) imageFromHandle.height;
        Color color1 = Color.White;
        float num3 = 1f;
        if ((effect & 4096 /*0x1000*/) != 0)
        {
          color1 = CServices.getColorAlpha(effectParam & 16777215 /*0xFFFFFF*/);
          num3 = (float) (effectParam >> 24 & (int) byte.MaxValue) / (float) byte.MaxValue;
        }
        else if ((effect & 4095 /*0x0FFF*/) == 1)
          num3 = (float) (128 /*0x80*/ - effectParam) / 128f;
        Color color2 = color1 * num3;
        Texture2D texture = imageFromHandle.image;
        Rectangle? sourceRectangle = new Rectangle?();
        if (imageFromHandle.mosaic != (short) 0)
        {
          texture = this.app.imageBank.mosaics[(int) imageFromHandle.mosaic];
          sourceRectangle = new Rectangle?(imageFromHandle.mosaicRectangle);
        }
        batch.Draw(texture, this.tempRect, sourceRectangle, color2, effect & 4095 /*0x0FFF*/, effectParam);
      }

      public void spriteDraw(SpriteBatchEffect batch)
      {
        CSprite firstSprite = this.firstSprite;
        if (firstSprite == null)
        {
          this.app.run.draw_QuickDisplay(batch);
        }
        else
        {
          bool flag = true;
          for (CSprite sprite = firstSprite; sprite != null; sprite = sprite.objNext)
          {
            if (flag && ((int) sprite.sprFlags & 536870912 /*0x20000000*/) != 0)
            {
              this.app.run.draw_QuickDisplay(batch);
              flag = false;
            }
            if (((int) sprite.sprFlags & 2176) == 0)
            {
              if (((int) sprite.sprFlags & 8192 /*0x2000*/) != 0 && sprite.sprRout != null)
              {
                sprite.sprRout.drawableDraw(batch, sprite, this.bank, sprite.sprX1 + this.xOffset, sprite.sprY1 + this.yOffset);
              }
              else
              {
                CImage imageFromHandle = this.bank.getImageFromHandle(sprite.sprImg);
                if (imageFromHandle != null)
                {
                  int num1 = 0;
                  int num2 = 0;
                  if (((int) sprite.sprFlags & 4194304 /*0x400000*/) == 0)
                  {
                    num1 = (int) imageFromHandle.xSpot;
                    num2 = (int) imageFromHandle.ySpot;
                  }
                  this.tempRect.X = sprite.sprX + this.xOffset;
                  this.tempRect.Y = sprite.sprY + this.yOffset;
                  this.tempRect.Width = (int) ((double) imageFromHandle.width * (double) sprite.sprScaleX);
                  this.tempRect.Height = (int) ((double) imageFromHandle.height * (double) sprite.sprScaleY);
                  this.vector.X = (float) num1;
                  this.vector.Y = (float) num2;
                  Texture2D texture = imageFromHandle.image;
                  Rectangle? sourceRectangle = new Rectangle?();
                  if (imageFromHandle.mosaic != (short) 0)
                  {
                    texture = this.app.imageBank.mosaics[(int) imageFromHandle.mosaic];
                    sourceRectangle = new Rectangle?(imageFromHandle.mosaicRectangle);
                  }
                  batch.Draw(texture, this.tempRect, sourceRectangle, sprite.rgb, (float) ((double) -sprite.sprAngle * Math.PI / 180.0), this.vector, sprite.sprEffect);
                }
              }
              sprite.sprFlags &= 4294963135U;
            }
          }
          if (!flag)
            return;
          this.app.run.draw_QuickDisplay(batch);
        }
      }

      public CSprite getLastSprite(int nLayer, int dwFlags)
      {
        CSprite lastSprite = this.lastSprite;
        int num = nLayer;
        if (nLayer != -1)
        {
          num *= 2;
          if ((dwFlags & 1) == 0)
            ++num;
        }
        for (; lastSprite != null && num != -1 && (int) lastSprite.sprLayer != num; lastSprite = lastSprite.objPrev)
        {
          if ((int) lastSprite.sprLayer < num)
          {
            lastSprite = (CSprite) null;
            break;
          }
        }
        return lastSprite;
      }

      public CSprite winAllocSprite()
      {
        CSprite csprite = new CSprite(this.bank);
        if (this.firstSprite == null)
        {
          this.firstSprite = csprite;
          this.lastSprite = csprite;
          csprite.objPrev = (CSprite) null;
          csprite.objNext = (CSprite) null;
          return csprite;
        }
        CSprite lastSprite = this.lastSprite;
        lastSprite.objNext = csprite;
        csprite.objPrev = lastSprite;
        csprite.objNext = (CSprite) null;
        this.lastSprite = csprite;
        return csprite;
      }

      public void winFreeSprite(CSprite spr)
      {
        if (spr.objPrev == null)
          this.firstSprite = spr.objNext;
        else
          spr.objPrev.objNext = spr.objNext;
        if (spr.objNext != null)
          spr.objNext.objPrev = spr.objPrev;
        else
          this.lastSprite = spr.objPrev;
      }

      public void winSetColMode(short c) => this.colMode = c;

      public CSprite spriteCol_TestPoint(CSprite firstSpr, short nLayer, int xp, int yp, int dwFlags)
      {
        CSprite csprite = firstSpr;
        CSprite ptSpr = csprite != null ? csprite.objNext : this.firstSprite;
        bool flag1 = nLayer == (short) -1;
        bool flag2 = (dwFlags & 4) != 0;
        short num1;
        if ((dwFlags & 8) != 0)
        {
          num1 = (short) 0;
          if (nLayer != (short) -1)
            nLayer *= (short) 2;
        }
        else
        {
          num1 = (short) 1;
          if (nLayer != (short) -1)
            nLayer = (short) ((int) nLayer * 2 + 1);
        }
        for (; ptSpr != null; ptSpr = ptSpr.objNext)
        {
          if (!flag1)
          {
            if ((int) ptSpr.sprLayer >= (int) nLayer)
            {
              if ((int) ptSpr.sprLayer > (int) nLayer)
                break;
            }
            else
              continue;
          }
          else if (((int) ptSpr.sprLayer & 1) != (int) num1)
            continue;
          if ((flag2 || ((int) ptSpr.sprFlags & 1) != 0) && xp >= ptSpr.sprX1 && xp < ptSpr.sprX2 && yp >= ptSpr.sprY1 && yp < ptSpr.sprY2)
          {
            int nFlags = 0;
            if ((dwFlags & 8) != 0 && ((int) ptSpr.sprFlags & 131072 /*0x020000*/) != 0)
            {
              if ((dwFlags & 1) == 0)
                nFlags = 1;
              else
                continue;
            }
            if (this.colMode == (short) 0 || ((int) ptSpr.sprFlags & 256 /*0x0100*/) != 0)
              return ptSpr;
            CMask spriteMask = this.getSpriteMask(ptSpr, (short) -1, nFlags, (int) ptSpr.sprAngle, ptSpr.sprScaleX, ptSpr.sprScaleY);
            if (spriteMask != null)
            {
              int num2 = yp - ptSpr.sprY1;
              if (num2 < spriteMask.height)
              {
                int num3 = num2 * spriteMask.lineWidth;
                int num4 = xp - ptSpr.sprX1;
                if (num4 < spriteMask.width)
                {
                  int index = num3 + num4 / 16 /*0x10*/;
                  short num5 = (short) (32768 /*0x8000*/ >> (num4 & 15));
                  if (((int) spriteMask.mask[index] & (int) num5) != 0)
                    return ptSpr;
                }
              }
            }
          }
        }
        return (CSprite) null;
      }

      public CSprite spriteCol_TestPointOne(
        CSprite firstSpr,
        short nLayer,
        int xp,
        int yp,
        int dwFlags)
      {
        CSprite ptSpr = firstSpr;
        bool flag1 = nLayer == (short) -1;
        bool flag2 = true;
        short num1;
        if ((dwFlags & 8) != 0)
        {
          num1 = (short) 0;
          if (nLayer != (short) -1)
            nLayer *= (short) 2;
        }
        else
        {
          num1 = (short) 1;
          if (nLayer != (short) -1)
            nLayer = (short) ((int) nLayer * 2 + 1);
        }
        for (; ptSpr != null; ptSpr = ptSpr.objNext)
        {
          if (!flag1)
          {
            if ((int) ptSpr.sprLayer >= (int) nLayer)
            {
              if ((int) ptSpr.sprLayer > (int) nLayer)
                break;
            }
            else
              continue;
          }
          else if (((int) ptSpr.sprLayer & 1) != (int) num1)
            continue;
          if ((flag2 || ((int) ptSpr.sprFlags & 1) != 0) && xp >= ptSpr.sprX1 && xp < ptSpr.sprX2 && yp >= ptSpr.sprY1 && yp < ptSpr.sprY2)
          {
            int nFlags = 0;
            if (this.colMode == (short) 0 || ((int) ptSpr.sprFlags & 256 /*0x0100*/) != 0)
              return ptSpr;
            CMask spriteMask = this.getSpriteMask(ptSpr, (short) -1, nFlags, (int) ptSpr.sprAngle, ptSpr.sprScaleX, ptSpr.sprScaleY);
            if (spriteMask != null)
            {
              int num2 = yp - ptSpr.sprY1;
              if (num2 < spriteMask.height)
              {
                int num3 = num2 * spriteMask.lineWidth;
                int num4 = xp - ptSpr.sprX1;
                if (num4 < spriteMask.width)
                {
                  int index = num3 + num4 / 16 /*0x10*/;
                  short num5 = (short) (32768 /*0x8000*/ >> (num4 & 15));
                  if (((int) spriteMask.mask[index] & (int) num5) != 0)
                    return ptSpr;
                }
              }
            }
          }
        }
        return (CSprite) null;
      }

      public CArrayList spriteCol_TestSprite_All(
        CSprite ptSpr,
        short newImg,
        int newX,
        int newY,
        int newAngle,
        float newScaleX,
        float newScaleY,
        int dwFlags)
      {
        int num1 = (int) this.colMode;
        CArrayList carrayList = (CArrayList) null;
        if (ptSpr == null || newImg < (short) 0)
          return (CArrayList) null;
        if (((int) ptSpr.sprFlags & 256 /*0x0100*/) != 0)
          num1 = 0;
        int sprLayer = (int) ptSpr.sprLayer;
        int num2;
        if ((dwFlags & 8) != 0)
        {
          num2 = sprLayer & -2;
        }
        else
        {
          if (((int) ptSpr.sprFlags & 1) == 0)
            return (CArrayList) null;
          num2 = sprLayer | 1;
        }
        int x1 = newX;
        int y1 = newY;
        int num3 = x1;
        int num4 = y1;
        CMask cmask = (CMask) null;
        int num5;
        int num6;
        if (((int) ptSpr.sprFlags & 8192 /*0x2000*/) != 0)
        {
          num5 = num3 + (ptSpr.sprX2 - ptSpr.sprX1);
          num6 = num4 + (ptSpr.sprY2 - ptSpr.sprY1);
        }
        else
        {
          CImage imageInfoEx = this.bank.getImageInfoEx(newImg, newAngle, newScaleX, newScaleY);
          if (((int) ptSpr.sprFlags & 4194304 /*0x400000*/) == 0)
          {
            x1 -= (int) imageInfoEx.xSpot;
            y1 -= (int) imageInfoEx.ySpot;
          }
          num5 = x1 + (int) imageInfoEx.width;
          num6 = y1 + (int) imageInfoEx.height;
        }
        for (CSprite ptSpr1 = this.firstSprite; ptSpr1 != null; ptSpr1 = ptSpr1.objNext)
        {
          if ((int) ptSpr1.sprLayer >= num2)
          {
            if ((int) ptSpr1.sprLayer <= num2)
            {
              if (((int) ptSpr1.sprFlags & 1) != 0 && x1 < ptSpr1.sprX2 && num5 > ptSpr1.sprX1 && y1 < ptSpr1.sprY2 && num6 > ptSpr1.sprY1 && ptSpr1 != ptSpr)
              {
                int nFlags = 0;
                if ((dwFlags & 8) != 0 && ((int) ptSpr1.sprFlags & 131072 /*0x020000*/) != 0)
                {
                  if ((dwFlags & 1) == 0)
                    nFlags = 1;
                  else
                    continue;
                }
                if (num1 == 0 || ((int) ptSpr1.sprFlags & 256 /*0x0100*/) != 0)
                {
                  if (carrayList == null)
                    carrayList = new CArrayList();
                  carrayList.add((object) ptSpr1.sprExtraInfo);
                }
                else
                {
                  if (cmask == null)
                  {
                    cmask = this.getSpriteMask(ptSpr, newImg, 0, newAngle, newScaleX, newScaleY);
                    if (cmask == null)
                    {
                      if (carrayList == null)
                        carrayList = new CArrayList();
                      carrayList.add((object) ptSpr1.sprExtraInfo);
                      continue;
                    }
                  }
                  CMask spriteMask = this.getSpriteMask(ptSpr1, (short) -1, nFlags, (int) ptSpr1.sprAngle, ptSpr1.sprScaleX, ptSpr1.sprScaleY);
                  if (spriteMask != null && cmask.testMask(0, x1, y1, spriteMask, 0, ptSpr1.sprX1, ptSpr1.sprY1))
                  {
                    if (carrayList == null)
                      carrayList = new CArrayList();
                    carrayList.add((object) ptSpr1.sprExtraInfo);
                  }
                }
              }
            }
            else
              break;
          }
        }
        return carrayList;
      }

      public CSprite spriteCol_TestSprite(
        CSprite ptSpr,
        short newImg,
        int newX,
        int newY,
        int newAngle,
        float newScaleX,
        float newScaleY,
        int subHt,
        uint dwFlags)
      {
        if (ptSpr == null)
          return (CSprite) null;
        if (((int) ptSpr.sprFlags & 256 /*0x0100*/) != 0)
          this.colMode = (short) 0;
        int sprLayer = (int) ptSpr.sprLayer;
        int num1;
        if (((int) dwFlags & 8) != 0)
        {
          num1 = sprLayer & -2;
        }
        else
        {
          if (((int) ptSpr.sprFlags & 1) == 0)
            return (CSprite) null;
          num1 = sprLayer | 1;
        }
        int x1 = newX;
        int y1 = newY;
        int num2 = x1;
        int num3 = y1;
        CMask cmask = (CMask) null;
        int num4;
        int num5;
        if (((int) ptSpr.sprFlags & 8192 /*0x2000*/) != 0)
        {
          num4 = num2 + (ptSpr.sprX2 - ptSpr.sprX1);
          num5 = num3 + (ptSpr.sprY2 - ptSpr.sprY1);
        }
        else
        {
          CImage imageInfoEx = this.bank.getImageInfoEx(newImg, newAngle, newScaleX, newScaleY);
          if (((int) ptSpr.sprFlags & 4194304 /*0x400000*/) == 0)
          {
            x1 -= (int) imageInfoEx.xSpot;
            y1 -= (int) imageInfoEx.ySpot;
          }
          num4 = x1 + (int) imageInfoEx.width;
          num5 = y1 + (int) imageInfoEx.height;
        }
        if (subHt != 0)
        {
          int num6 = num5 - y1;
          if (subHt > num6)
            subHt = num6;
          y1 += num6 - subHt;
        }
        for (CSprite ptSpr1 = this.firstSprite; ptSpr1 != null; ptSpr1 = ptSpr1.objNext)
        {
          if ((int) ptSpr1.sprLayer >= num1)
          {
            if ((int) ptSpr1.sprLayer <= num1)
            {
              if (((int) ptSpr1.sprFlags & 1) != 0 && x1 < ptSpr1.sprX2 && num4 > ptSpr1.sprX1 && y1 < ptSpr1.sprY2 && num5 > ptSpr1.sprY1 && ptSpr1 != ptSpr && ((int) ptSpr1.sprFlags & 32 /*0x20*/) == 0)
              {
                int nFlags = 0;
                if (((int) dwFlags & 8) != 0 && ((int) ptSpr1.sprFlags & 131072 /*0x020000*/) != 0)
                {
                  if (((int) dwFlags & 1) == 0)
                    nFlags = 1;
                  else
                    continue;
                }
                if (this.colMode == (short) 0 || ((int) ptSpr1.sprFlags & 256 /*0x0100*/) != 0)
                  return ptSpr1;
                if (cmask == null)
                {
                  cmask = this.getSpriteMask(ptSpr, newImg, 0, newAngle, newScaleX, newScaleY);
                  if (cmask == null)
                    return ptSpr1;
                }
                int yBase1 = 0;
                int height = cmask.height;
                if (subHt != 0)
                {
                  if (subHt > height)
                    subHt = height;
                  yBase1 = height - subHt;
                }
                CMask spriteMask = this.getSpriteMask(ptSpr1, (short) -1, nFlags, (int) ptSpr1.sprAngle, ptSpr1.sprScaleX, ptSpr1.sprScaleY);
                if (spriteMask != null && cmask.testMask(yBase1, x1, y1, spriteMask, 0, ptSpr1.sprX1, ptSpr1.sprY1))
                  return ptSpr1;
              }
            }
            else
              break;
          }
        }
        return (CSprite) null;
      }

      public CSprite spriteCol_TestRect(
        CSprite firstSpr,
        int nLayer,
        int xp,
        int yp,
        int wp,
        int hp,
        int dwFlags)
      {
        CSprite csprite = firstSpr;
        CSprite ptSpr = csprite != null ? csprite.objNext : this.firstSprite;
        bool flag1 = nLayer == -1;
        bool flag2 = (dwFlags & 4) != 0;
        short num;
        if ((dwFlags & 8) != 0)
        {
          num = (short) 0;
          if (nLayer != -1)
            nLayer *= 2;
        }
        else
        {
          num = (short) 1;
          if (nLayer != -1)
            nLayer = nLayer * 2 + 1;
        }
        for (; ptSpr != null; ptSpr = ptSpr.objNext)
        {
          if (!flag1)
          {
            if ((int) ptSpr.sprLayer >= nLayer)
            {
              if ((int) ptSpr.sprLayer > nLayer)
                break;
            }
            else
              continue;
          }
          else if (((int) ptSpr.sprLayer & 1) != (int) num)
            continue;
          if ((flag2 || ((int) ptSpr.sprFlags & 1) != 0) && xp <= ptSpr.sprX2 && xp + wp > ptSpr.sprX1 && yp <= ptSpr.sprY2 && yp + hp > ptSpr.sprY1 && ((int) ptSpr.sprFlags & 32 /*0x20*/) == 0)
          {
            int nFlags = 0;
            if ((dwFlags & 8) != 0 && ((int) ptSpr.sprFlags & 131072 /*0x020000*/) != 0)
            {
              if ((dwFlags & 1) == 0)
                nFlags = 1;
              else
                continue;
            }
            if (this.colMode == (short) 0 || ((int) ptSpr.sprFlags & 256 /*0x0100*/) != 0)
              return ptSpr;
            CMask spriteMask = this.getSpriteMask(ptSpr, (short) -1, nFlags, (int) ptSpr.sprAngle, ptSpr.sprScaleX, ptSpr.sprScaleY);
            if (spriteMask != null && spriteMask.testRect(0, xp - ptSpr.sprX1, yp - ptSpr.sprY1, wp, hp))
              return ptSpr;
          }
        }
        return (CSprite) null;
      }
    }
}
