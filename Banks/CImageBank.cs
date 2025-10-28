// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Banks.CImageBank
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework.Graphics;
using RuntimeXNA.Application;
using RuntimeXNA.Services;
using System;

namespace RuntimeXNA.Banks
{

    public class CImageBank : IEnum
    {
      public CRunApp app;
      public CFile file;
      public CImage[] images;
      public int nHandlesReel;
      public int nHandlesTotal;
      public int nImages;
      private int[] offsetsToImage;
      private short[] handleToIndex;
      private byte[] useCount;
      private CRect rcInfo;
      private CPoint hsInfo;
      private CPoint apInfo;
      public Texture2D[] mosaics;
      public Texture2D[] oldMosaics;

      public CImageBank()
      {
      }

      public CImageBank(CRunApp a)
      {
        this.app = a;
        this.file = this.app.file;
      }

      public void preLoad()
      {
        this.nHandlesReel = (int) this.file.readAShort();
        this.offsetsToImage = new int[this.nHandlesReel];
        int num = (int) this.file.readAShort();
        CImage cimage = new CImage();
        for (int index = 0; index < num; ++index)
        {
          int filePointer = this.file.getFilePointer();
          cimage.loadHandle(this.app.file);
          this.offsetsToImage[(int) cimage.handle] = filePointer;
        }
        this.useCount = new byte[this.nHandlesReel];
        this.resetToLoad();
        this.handleToIndex = (short[]) null;
        this.nHandlesTotal = this.nHandlesReel;
        this.nImages = 0;
        this.images = (CImage[]) null;
      }

      public CImage getImageFromHandle(short handle)
      {
        return handle >= (short) 0 && (int) handle < this.nHandlesTotal && this.handleToIndex[(int) handle] != (short) -1 ? this.images[(int) this.handleToIndex[(int) handle]] : (CImage) null;
      }

      public CImage getImageFromIndex(short index)
      {
        return index >= (short) 0 && (int) index < this.nImages ? this.images[(int) index] : (CImage) null;
      }

      public void resetToLoad()
      {
        for (int index = 0; index < this.nHandlesReel; ++index)
          this.useCount[index] = (byte) 0;
      }

      public void setToLoad(short handle) => ++this.useCount[(int) handle];

      public short enumerate(short num)
      {
        this.setToLoad(num);
        return -1;
      }

      public void loadMosaic(short handle)
      {
        if (this.mosaics[(int) handle] != null)
          return;
        if (this.oldMosaics != null && (int) handle < this.oldMosaics.Length && this.oldMosaics[(int) handle] != null)
        {
          this.mosaics[(int) handle] = this.oldMosaics[(int) handle];
        }
        else
        {
          string assetName = "ImgM" + handle.ToString("D4");
          this.mosaics[(int) handle] = this.app.content.Load<Texture2D>(assetName);
        }
      }

      public void load()
      {
        if (this.app.frame.mosaicMaxHandle > 0)
        {
          int val1 = this.app.frame.mosaicMaxHandle;
          if (this.mosaics != null)
          {
            this.oldMosaics = new Texture2D[this.mosaics.Length];
            for (int index = 0; index < this.mosaics.Length; ++index)
              this.oldMosaics[index] = this.mosaics[index];
            val1 = Math.Max(val1, this.mosaics.Length);
          }
          this.mosaics = new Texture2D[val1];
          for (int index = 0; index < val1; ++index)
            this.mosaics[index] = (Texture2D) null;
        }
        this.nImages = 0;
        for (int index = 0; index < this.nHandlesReel; ++index)
        {
          if (this.useCount[index] != (byte) 0)
            ++this.nImages;
        }
        CImage[] cimageArray = new CImage[this.nImages];
        int index1 = 0;
        for (int index2 = 0; index2 < this.nHandlesReel; ++index2)
        {
          if (this.useCount[index2] != (byte) 0)
          {
            if (this.images != null && this.handleToIndex[index2] != (short) -1 && this.images[(int) this.handleToIndex[index2]] != null)
            {
              cimageArray[index1] = this.images[(int) this.handleToIndex[index2]];
              cimageArray[index1].useCount = (short) this.useCount[index2];
              if (this.mosaics != null && this.oldMosaics != null)
              {
                short mosaic = cimageArray[index1].mosaic;
                if (mosaic > (short) 0)
                  this.mosaics[(int) mosaic] = this.oldMosaics[(int) mosaic];
              }
            }
            else
            {
              cimageArray[index1] = new CImage();
              this.file.seek(this.offsetsToImage[index2]);
              cimageArray[index1].load(this.app);
              cimageArray[index1].useCount = (short) this.useCount[index2];
            }
            ++index1;
          }
        }
        this.images = cimageArray;
        this.handleToIndex = new short[this.nHandlesReel];
        for (int index3 = 0; index3 < this.nHandlesReel; ++index3)
          this.handleToIndex[index3] = (short) -1;
        for (int index4 = 0; index4 < this.nImages; ++index4)
          this.handleToIndex[(int) this.images[index4].handle] = (short) index4;
        this.nHandlesTotal = this.nHandlesReel;
        this.resetToLoad();
        this.oldMosaics = (Texture2D[]) null;
      }

      public CImage getImageInfoEx(short nImage, int nAngle, float fScaleX, float fScaleY)
      {
        CImage imageInfoEx = new CImage();
        CImage imageFromHandle = this.getImageFromHandle(nImage);
        if (imageFromHandle == null)
          return (CImage) null;
        int num1 = (int) imageFromHandle.width;
        int num2 = (int) imageFromHandle.height;
        int num3 = (int) imageFromHandle.xSpot;
        int num4 = (int) imageFromHandle.ySpot;
        int num5 = (int) imageFromHandle.xAP;
        int num6 = (int) imageFromHandle.yAP;
        if (nAngle == 0)
        {
          if ((double) fScaleX != 1.0)
          {
            num3 = (int) ((double) num3 * (double) fScaleX);
            num5 = (int) ((double) num5 * (double) fScaleX);
            num1 = (int) ((double) num1 * (double) fScaleX);
          }
          if ((double) fScaleY != 1.0)
          {
            num4 = (int) ((double) num4 * (double) fScaleY);
            num6 = (int) ((double) num6 * (double) fScaleY);
            num2 = (int) ((double) num2 * (double) fScaleY);
          }
        }
        else
        {
          if ((double) fScaleX != 1.0)
          {
            num3 = (int) ((double) num3 * (double) fScaleX);
            num5 = (int) ((double) num5 * (double) fScaleX);
            num1 = (int) ((double) num1 * (double) fScaleX);
          }
          if ((double) fScaleY != 1.0)
          {
            num4 = (int) ((double) num4 * (double) fScaleY);
            num6 = (int) ((double) num6 * (double) fScaleY);
            num2 = (int) ((double) num2 * (double) fScaleY);
          }
          if (this.rcInfo == null)
            this.rcInfo = new CRect();
          if (this.hsInfo == null)
            this.hsInfo = new CPoint();
          if (this.apInfo == null)
            this.apInfo = new CPoint();
          this.hsInfo.x = num3;
          this.hsInfo.y = num4;
          this.apInfo.x = num5;
          this.apInfo.y = num6;
          this.rcInfo.left = this.rcInfo.top = 0;
          this.rcInfo.right = num1;
          this.rcInfo.bottom = num2;
          this.doRotateRect(this.rcInfo, this.hsInfo, this.apInfo, (double) nAngle);
          num1 = this.rcInfo.right;
          num2 = this.rcInfo.bottom;
          num3 = this.hsInfo.x;
          num4 = this.hsInfo.y;
          num5 = this.apInfo.x;
          num6 = this.apInfo.y;
        }
        imageInfoEx.width = (short) num1;
        imageInfoEx.height = (short) num2;
        imageInfoEx.xSpot = (short) num3;
        imageInfoEx.ySpot = (short) num4;
        imageInfoEx.xAP = (short) num5;
        imageInfoEx.yAP = (short) num6;
        return imageInfoEx;
      }

      private void doRotateRect(CRect prc, CPoint pHotSpot, CPoint pActionPoint, double fAngle)
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
          double num3 = fAngle * Math.PI / 180.0;
          num1 = Math.Cos(num3);
          num2 = Math.Sin(num3);
        }
        double num4;
        double num5;
        double val1_1;
        double val1_2;
        if (pHotSpot == null)
        {
          num5 = num4 = 0.0;
          val1_2 = val1_1 = 0.0;
        }
        else
        {
          double num6 = (double) -pHotSpot.x * num1;
          double num7 = (double) -pHotSpot.x * num2;
          num5 = (double) -pHotSpot.y * num1;
          num4 = (double) -pHotSpot.y * num2;
          val1_2 = num6 + num4;
          val1_1 = num5 - num7;
        }
        double num8 = pHotSpot != null ? (double) (prc.right - pHotSpot.x) : (double) prc.right;
        double num9 = num8 * num1;
        double num10 = num8 * num2;
        double val1_3 = num9 + num4;
        double val1_4 = num5 - num10;
        double num11 = pHotSpot != null ? (double) (prc.bottom - pHotSpot.y) : (double) prc.bottom;
        double num12 = num11 * num1;
        double num13 = num11 * num2;
        double val1_5 = num9 + num13;
        double val1_6 = num12 - num10;
        double val2_1 = val1_2 + val1_5 - val1_3;
        double val2_2 = val1_1 + val1_6 - val1_4;
        double num14 = Math.Min(val1_2, Math.Min(val1_3, Math.Min(val1_5, val2_1)));
        double num15 = Math.Min(val1_1, Math.Min(val1_4, Math.Min(val1_6, val2_2)));
        double num16 = Math.Max(val1_2, Math.Max(val1_3, Math.Max(val1_5, val2_1)));
        double num17 = Math.Max(val1_1, Math.Max(val1_4, Math.Max(val1_6, val2_2)));
        if (pActionPoint != null)
        {
          double num18;
          double num19;
          if (pHotSpot == null)
          {
            num18 = (double) pActionPoint.x;
            num19 = (double) pActionPoint.y;
          }
          else
          {
            num18 = (double) (pActionPoint.x - pHotSpot.x);
            num19 = (double) (pActionPoint.y - pHotSpot.y);
          }
          pActionPoint.x = (int) (num18 * num1 + num19 * num2 - num14);
          pActionPoint.y = (int) (num19 * num1 - num18 * num2 - num15);
        }
        if (pHotSpot != null)
        {
          pHotSpot.x = (int) -num14;
          pHotSpot.y = (int) -num15;
        }
        prc.right = (int) (num16 - num14);
        prc.bottom = (int) (num17 - num15);
      }

      public short addImage(
        Texture2D img,
        short xSpot,
        short ySpot,
        short xAP,
        short yAP,
        short count)
      {
        short index1 = -1;
        for (int nHandlesReel = this.nHandlesReel; nHandlesReel < this.nHandlesTotal; ++nHandlesReel)
        {
          if (this.handleToIndex[nHandlesReel] == (short) -1)
          {
            index1 = (short) nHandlesReel;
            break;
          }
        }
        if (index1 == (short) -1)
        {
          short[] numArray = new short[this.nHandlesTotal + 10];
          int index2;
          for (index2 = 0; index2 < this.nHandlesTotal; ++index2)
            numArray[index2] = this.handleToIndex[index2];
          for (; index2 < this.nHandlesTotal + 10; ++index2)
            numArray[index2] = (short) -1;
          index1 = (short) this.nHandlesTotal;
          this.nHandlesTotal += 10;
          this.handleToIndex = numArray;
        }
        int index3 = -1;
        for (int index4 = 0; index4 < this.nImages; ++index4)
        {
          if (this.images[index4] == null)
          {
            index3 = index4;
            break;
          }
        }
        if (index3 == -1)
        {
          CImage[] cimageArray = new CImage[this.nImages + 10];
          int index5;
          for (index5 = 0; index5 < this.nImages; ++index5)
            cimageArray[index5] = this.images[index5];
          for (; index5 < this.nImages + 10; ++index5)
            cimageArray[index5] = (CImage) null;
          index3 = this.nImages;
          this.nImages += 10;
          this.images = cimageArray;
        }
        this.handleToIndex[(int) index1] = (short) index3;
        this.images[index3] = new CImage();
        this.images[index3].handle = index1;
        this.images[index3].image = img;
        this.images[index3].xSpot = xSpot;
        this.images[index3].ySpot = ySpot;
        this.images[index3].xAP = xAP;
        this.images[index3].yAP = yAP;
        this.images[index3].useCount = count;
        this.images[index3].width = (short) img.Width;
        this.images[index3].height = (short) img.Height;
        return index1;
      }

      public void delImage(short handle)
      {
        CImage imageFromHandle = this.getImageFromHandle(handle);
        if (imageFromHandle == null)
          return;
        --imageFromHandle.useCount;
        if (imageFromHandle.useCount > (short) 0)
          return;
        for (int index = 0; index < this.nImages; ++index)
        {
          if (this.images[index] == imageFromHandle)
          {
            this.images[index] = (CImage) null;
            this.handleToIndex[(int) handle] = (short) -1;
            break;
          }
        }
      }

      public void loadImageList(short[] handles)
      {
        for (int index1 = 0; index1 < handles.Length; ++index1)
        {
          if (handles[index1] >= (short) 0 && (int) handles[index1] < this.nHandlesTotal && this.offsetsToImage[(int) handles[index1]] != 0 && this.getImageFromHandle(handles[index1]) == null)
          {
            int index2 = -1;
            for (int index3 = 0; index3 < this.nImages; ++index3)
            {
              if (this.images[index3] == null)
              {
                index2 = index3;
                break;
              }
            }
            if (index2 == -1)
            {
              CImage[] cimageArray = new CImage[this.nImages + 10];
              int index4;
              for (index4 = 0; index4 < this.nImages; ++index4)
                cimageArray[index4] = this.images[index4];
              for (; index4 < this.nImages + 10; ++index4)
                cimageArray[index4] = (CImage) null;
              index2 = this.nImages;
              this.nImages += 10;
              this.images = cimageArray;
            }
            this.handleToIndex[(int) handles[index1]] = (short) index2;
            this.images[index2] = new CImage();
            this.images[index2].useCount = (short) 1;
            this.file.seek(this.offsetsToImage[(int) handles[index1]]);
            this.images[index2].load(this.app);
          }
        }
      }
    }
}
