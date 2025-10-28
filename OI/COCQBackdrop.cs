// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.OI.COCQBackdrop
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RuntimeXNA.Application;
using RuntimeXNA.Banks;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;

namespace RuntimeXNA.OI
{

    internal class COCQBackdrop : COC, IDrawing
    {
      public const short FILLTYPE_NONE = 0;
      public const short FILLTYPE_SOLID = 1;
      public const short FILLTYPE_GRADIENT = 2;
      public const short FILLTYPE_MOTIF = 3;
      public const short SHAPE_NONE = 0;
      public const short SHAPE_LINE = 1;
      public const short SHAPE_RECTANGLE = 2;
      public const short SHAPE_ELLIPSE = 3;
      public const short LINEF_INVX = 1;
      public const short LINEF_INVY = 2;
      public short ocBorderSize;
      public int ocBorderColor;
      public short ocShape;
      public short ocFillType;
      public short ocLineFlags;
      public int ocColor1;
      public int ocColor2;
      public int ocGradientFlags;
      public short ocImage;
      public CRunApp app;
      public Texture2D texture;

      public COCQBackdrop()
      {
      }

      public COCQBackdrop(CRunApp a) => this.app = a;

      public override void load(CFile file, short type)
      {
        file.skipBytes(4);
        this.ocObstacleType = file.readAShort();
        this.ocColMode = file.readAShort();
        this.ocCx = file.readAInt();
        this.ocCy = file.readAInt();
        this.ocBorderSize = file.readAShort();
        this.ocBorderColor = file.readAColor();
        this.ocShape = file.readAShort();
        this.ocFillType = file.readAShort();
        if (this.ocShape == (short) 1)
        {
          this.ocLineFlags = file.readAShort();
        }
        else
        {
          switch (this.ocFillType)
          {
            case 1:
              this.ocColor1 = file.readAColor();
              break;
            case 2:
              this.ocColor1 = file.readAColor();
              this.ocColor2 = file.readAColor();
              this.ocGradientFlags = file.readAInt();
              break;
            case 3:
              this.ocImage = file.readAShort();
              break;
          }
        }
      }

      public override void enumElements(IEnum enumImages, IEnum enumFonts)
      {
        if (this.ocFillType != (short) 3 || enumImages == null)
          return;
        short num = enumImages.enumerate(this.ocImage);
        if (num == (short) -1)
          return;
        this.ocImage = num;
      }

      public void drawableDraw(
        SpriteBatchEffect batch,
        CSprite sprite,
        CImageBank bank,
        int x,
        int y)
      {
        int ocBorderSize = (int) this.ocBorderSize;
        int width = this.ocCx;
        int height = this.ocCy;
        bool bVertical = false;
        if (this.ocGradientFlags != 0)
          bVertical = true;
        switch (this.ocShape)
        {
          case 2:
            switch (this.ocFillType)
            {
              case 1:
                this.app.services.drawFilledRectangle(this.app, x, y, width, height, this.ocColor1, ocBorderSize, this.ocBorderColor, this.oi.oiInkEffect & 4095 /*0x0FFF*/, this.oi.oiInkEffectParam);
                break;
              case 2:
                if (this.texture == null)
                {
                  this.texture = CServices.createGradientRectangle(this.app, width, height, this.ocColor1, this.ocColor2, bVertical, ocBorderSize, this.ocBorderColor);
                  break;
                }
                break;
              case 3:
                CImage imageFromHandle1 = this.app.imageBank.getImageFromHandle(this.ocImage);
                this.app.services.drawPatternRectangle(this.app.spriteBatch, imageFromHandle1, x, y, width, height, ocBorderSize, this.ocBorderColor, this.oi.oiInkEffect & 4095 /*0x0FFF*/, this.oi.oiInkEffectParam);
                width = (width + (int) imageFromHandle1.width - 1) / (int) imageFromHandle1.width * (int) imageFromHandle1.width;
                height = (height + (int) imageFromHandle1.height - 1) / (int) imageFromHandle1.height * (int) imageFromHandle1.width;
                break;
            }
            break;
          case 3:
            switch (this.ocFillType)
            {
              case 1:
                if (this.texture == null)
                {
                  this.texture = CServices.createFilledEllipse(this.app, width, height, this.ocColor1, ocBorderSize, this.ocBorderColor);
                  break;
                }
                break;
              case 2:
                if (this.texture == null)
                {
                  this.texture = CServices.createGradientEllipse(this.app, width, height, this.ocColor1, this.ocColor2, bVertical, ocBorderSize, this.ocBorderColor);
                  break;
                }
                break;
              case 3:
                CImage imageFromHandle2 = this.app.imageBank.getImageFromHandle(this.ocImage);
                this.app.services.drawPatternRectangle(this.app.spriteBatch, imageFromHandle2, x, y, width, height, ocBorderSize, this.ocBorderColor, this.oi.oiInkEffect & 4095 /*0x0FFF*/, this.oi.oiInkEffectParam);
                width = (width + (int) imageFromHandle2.width - 1) / (int) imageFromHandle2.width * (int) imageFromHandle2.width;
                height = (height + (int) imageFromHandle2.height - 1) / (int) imageFromHandle2.height * (int) imageFromHandle2.width;
                break;
            }
            break;
        }
        if (this.ocShape == (short) 1 && ocBorderSize > 0)
        {
          if (((int) this.ocLineFlags & 1) != 0)
          {
            x += width;
            width = -width;
          }
          if (((int) this.ocLineFlags & 2) != 0)
          {
            y += height;
            height = -height;
          }
          this.app.services.drawLine(this.app.spriteBatch, x, y, x + width, y + height, this.ocBorderColor, ocBorderSize, this.oi.oiInkEffect & 4095 /*0x0FFF*/, this.oi.oiInkEffectParam);
        }
        if (this.texture == null)
          return;
        this.app.tempRect.X = x;
        this.app.tempRect.Y = y;
        this.app.tempRect.Width = this.texture.Width;
        this.app.tempRect.Height = this.texture.Height;
        batch.Draw(this.texture, this.app.tempRect, new Rectangle?(), Color.White, this.oi.oiInkEffect & 4095 /*0x0FFF*/, this.oi.oiInkEffectParam);
      }

      public void drawableKill()
      {
      }

      public CMask drawableGetMask(int flags) => (CMask) null;
    }
}
