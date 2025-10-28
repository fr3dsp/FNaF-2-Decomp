// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Banks.CImage
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RuntimeXNA.Application;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;

namespace RuntimeXNA.Banks
{

    public class CImage
    {
      public const int maxRotatedMasks = 10;
      public CRunApp app;
      public short handle;
      public short width;
      public short height;
      public short xSpot;
      public short ySpot;
      public short xAP;
      public short yAP;
      public short useCount;
      public Texture2D image;
      public CMask maskNormal;
      public CMask maskPlatform;
      public CArrayList maskRotation;
      public short mosaic;
      public Rectangle mosaicRectangle;

      public void loadHandle(CFile file)
      {
        this.handle = file.readAShort();
        file.skipBytes(12);
      }

      public void load(CRunApp a)
      {
        this.app = a;
        this.handle = this.app.file.readAShort();
        this.width = this.app.file.readAShort();
        this.height = this.app.file.readAShort();
        this.xSpot = this.app.file.readAShort();
        this.ySpot = this.app.file.readAShort();
        this.xAP = this.app.file.readAShort();
        this.yAP = this.app.file.readAShort();
        this.mosaic = (short) 0;
        if (this.app.frame.mosaicHandles != null && this.app.frame.mosaicHandles[(int) this.handle] != (short) 0)
        {
          this.mosaic = this.app.frame.mosaicHandles[(int) this.handle];
          this.app.imageBank.loadMosaic(this.mosaic);
          this.mosaicRectangle.X = this.app.frame.mosaicX[(int) this.handle];
          this.mosaicRectangle.Y = this.app.frame.mosaicY[(int) this.handle];
          this.mosaicRectangle.Width = (int) this.width;
          this.mosaicRectangle.Height = (int) this.height;
        }
        else
          this.image = this.app.content.Load<Texture2D>("Img" + this.handle.ToString("D4"));
      }

      public CMask getMask(int flags, int angle, float scaleX, float scaleY)
      {
        if ((flags & 1) == 0)
        {
          if (this.maskNormal == null)
          {
            this.maskNormal = new CMask();
            this.maskNormal.createMask(this, flags);
          }
          if (angle == 0 && (double) scaleX == 1.0 && (double) scaleY == 1.0)
            return this.maskNormal;
          if (this.maskRotation == null)
            this.maskRotation = new CArrayList();
          int num = int.MaxValue;
          int index1 = -1;
          for (int index2 = 0; index2 < this.maskRotation.size(); ++index2)
          {
            CRotatedMask crotatedMask = (CRotatedMask) this.maskRotation.get(index2);
            if (angle == crotatedMask.angle && (double) scaleX == (double) crotatedMask.scaleX && (double) scaleY == (double) crotatedMask.scaleY)
              return crotatedMask.mask;
            if (crotatedMask.tick < num)
            {
              num = crotatedMask.tick;
              index1 = index2;
            }
          }
          if (this.maskRotation.size() < 10)
            index1 = -1;
          CRotatedMask o = new CRotatedMask();
          o.mask = new CMask();
          o.mask.createRotatedMask(this.maskNormal, (double) angle, (double) scaleX, (double) scaleY);
          o.angle = angle;
          o.scaleX = scaleX;
          o.scaleY = scaleY;
          o.tick = (int) this.app.timer;
          if (index1 < 0)
            this.maskRotation.add((object) o);
          else
            this.maskRotation.set(index1, (object) o);
          return o.mask;
        }
        if (this.maskPlatform == null)
        {
          this.maskPlatform = new CMask();
          this.maskPlatform.createMask(this, flags);
        }
        return this.maskPlatform;
      }
    }
}
