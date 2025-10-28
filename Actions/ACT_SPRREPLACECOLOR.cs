// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_SPRREPLACECOLOR
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RuntimeXNA.Banks;
using RuntimeXNA.Objects;
using RuntimeXNA.OI;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Actions
{

    public class ACT_SPRREPLACECOLOR : CAct, IEnum
    {
      internal int mode;
      internal int dwMax;
      internal short[] pImages;
      internal CRun pRh;

      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        actionObjects.roa.animIn(0);
        int oldColor = this.evtParams[0].code != (short) 24 ? CServices.swapRGB(rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0])) : ((PARAM_COLOUR) this.evtParams[0]).color;
        int newColor = this.evtParams[1].code != (short) 24 ? CServices.swapRGB(rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[1])) : ((PARAM_COLOUR) this.evtParams[1]).color;
        this.pRh = rhPtr;
        short hoOi = actionObjects.hoOi;
        COI oiFromHandle = rhPtr.rhApp.OIList.getOIFromHandle(hoOi);
        if (oiFromHandle == null)
          return;
        this.dwMax = -1;
        this.mode = 0;
        oiFromHandle.enumElements((IEnum) this, (IEnum) null);
        CObject cobject1 = actionObjects;
        while (((int) cobject1.hoNumPrev & 32768 /*0x8000*/) == 0)
          cobject1 = rhPtr.rhObjectList[(int) cobject1.hoNumPrev & (int) short.MaxValue];
        while (true)
        {
          if (cobject1.roc.rcImage != (short) -1 && (int) cobject1.roc.rcImage > this.dwMax)
            this.dwMax = (int) cobject1.roc.rcImage;
          if (cobject1.roc.rcOldImage != (short) -1 && (int) cobject1.roc.rcOldImage > this.dwMax)
            this.dwMax = (int) cobject1.roc.rcOldImage;
          if (((int) cobject1.hoNumNext & 32768 /*0x8000*/) == 0)
            cobject1 = rhPtr.rhObjectList[(int) cobject1.hoNumNext];
          else
            break;
        }
        this.pImages = new short[this.dwMax + 1];
        for (int index = 0; index < this.dwMax + 1; ++index)
          this.pImages[index] = (short) -1;
        this.mode = 1;
        oiFromHandle.enumElements((IEnum) this, (IEnum) null);
        for (int handle = 0; handle <= this.dwMax; ++handle)
        {
          if (this.pImages[handle] != (short) -1)
          {
            CImage imageFromHandle = rhPtr.rhApp.imageBank.getImageFromHandle((short) handle);
            int width = (int) imageFromHandle.width;
            int height = (int) imageFromHandle.height;
            Color[] colorArray = new Color[width * height];
            if (imageFromHandle.mosaic == (short) 0)
            {
              imageFromHandle.image.GetData<Color>(colorArray);
              CServices.replaceColor(rhPtr.rhApp, colorArray, width, height, oldColor, newColor);
              Texture2D img = new Texture2D(rhPtr.rhApp.spriteBatch.GraphicsDevice, width, height);
              img.SetData<Color>(colorArray);
              short num = rhPtr.rhApp.imageBank.addImage(img, imageFromHandle.xSpot, imageFromHandle.ySpot, imageFromHandle.xAP, imageFromHandle.yAP, (short) 0);
              this.pImages[handle] = num;
            }
            else
            {
              rhPtr.rhApp.imageBank.mosaics[(int) imageFromHandle.mosaic].GetData<Color>(0, new Rectangle?(imageFromHandle.mosaicRectangle), colorArray, 0, width * height);
              CServices.replaceColor(rhPtr.rhApp, colorArray, width, height, oldColor, newColor);
              Texture2D img = new Texture2D(rhPtr.rhApp.spriteBatch.GraphicsDevice, width, height);
              img.SetData<Color>(colorArray);
              short num = rhPtr.rhApp.imageBank.addImage(img, imageFromHandle.xSpot, imageFromHandle.ySpot, imageFromHandle.xAP, imageFromHandle.yAP, (short) 0);
              this.pImages[handle] = num;
            }
          }
        }
        CObject cobject2 = actionObjects;
        while (((int) cobject2.hoNumPrev & 32768 /*0x8000*/) == 0)
          cobject2 = rhPtr.rhObjectList[(int) cobject2.hoNumPrev & (int) short.MaxValue];
        while (true)
        {
          if (cobject2.roc.rcImage != (short) -1 && this.pImages[(int) cobject2.roc.rcImage] != (short) -1)
            cobject2.roc.rcImage = this.pImages[(int) cobject2.roc.rcImage];
          if (cobject2.roc.rcOldImage != (short) -1 && this.pImages[(int) cobject2.roc.rcOldImage] != (short) -1)
            cobject2.roc.rcOldImage = this.pImages[(int) cobject2.roc.rcOldImage];
          if (cobject2.roc.rcSprite != null)
            rhPtr.rhApp.spriteGen.modifSprite(cobject2.roc.rcSprite, cobject2.hoX - rhPtr.rhWindowX, cobject2.hoY - rhPtr.rhWindowY, cobject2.roc.rcImage);
          if (((int) cobject2.hoNumNext & 32768 /*0x8000*/) == 0)
            cobject2 = rhPtr.rhObjectList[(int) cobject2.hoNumNext];
          else
            break;
        }
        this.mode = 2;
        oiFromHandle.enumElements((IEnum) this, (IEnum) null);
        this.mode = 3;
        oiFromHandle.enumElements((IEnum) this, (IEnum) null);
        oiFromHandle.oiLoadFlags |= 32 /*0x20*/;
        actionObjects.roc.rcChanged = true;
      }

      public virtual short enumerate(short num)
      {
        switch (this.mode)
        {
          case 0:
            if ((int) num > this.dwMax)
              this.dwMax = (int) num;
            return -1;
          case 1:
            this.pImages[(int) num] = (short) 1;
            return -1;
          case 2:
            if (this.pImages[(int) num] >= (short) 0)
              this.pRh.rhApp.imageBank.delImage(num);
            return -1;
          case 3:
            if (this.pImages[(int) num] >= (short) 0)
            {
              ++this.pRh.rhApp.imageBank.getImageFromHandle(this.pImages[(int) num]).useCount;
              return this.pImages[(int) num];
            }
            break;
        }
        return -1;
      }
    }
}
