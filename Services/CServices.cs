// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Services.CServices
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RuntimeXNA.Application;
using RuntimeXNA.Banks;
using RuntimeXNA.Sprites;
using System;

namespace RuntimeXNA.Services
{

    public class CServices
    {
      public const short DT_LEFT = 0;
      public const short DT_TOP = 0;
      public const short DT_CENTER = 1;
      public const short DT_RIGHT = 2;
      public const short DT_BOTTOM = 8;
      public const short DT_VCENTER = 4;
      public const short DT_SINGLELINE = 32 /*0x20*/;
      public const short DT_CALCRECT = 1024 /*0x0400*/;
      public const short DT_VALIGN = 2048 /*0x0800*/;
      public const int CPTDISPFLAG_INTNDIGITS = 15;
      public const int CPTDISPFLAG_FLOATNDIGITS = 240 /*0xF0*/;
      public const int CPTDISPFLAG_FLOATNDIGITS_SHIFT = 4;
      public const int CPTDISPFLAG_FLOATNDECIMALS = 61440 /*0xF000*/;
      public const int CPTDISPFLAG_FLOATNDECIMALS_SHIFT = 12;
      public const int CPTDISPFLAG_FLOAT_FORMAT = 512 /*0x0200*/;
      public const int CPTDISPFLAG_FLOAT_USENDECIMALS = 1024 /*0x0400*/;
      public const int CPTDISPFLAG_FLOAT_PADD = 2048 /*0x0800*/;
      private Texture2D pixel;
      private Rectangle tempRect;
      private static int[] xPos = (int[]) null;
      private static Vector2 vector;

      public static int HIWORD(int ul) => ul >> 16 /*0x10*/;

      public static int LOWORD(int ul) => ul & (int) ushort.MaxValue;

      public static int MAKELONG(int lo, int hi) => hi << 16 /*0x10*/ | lo & (int) ushort.MaxValue;

      public static int getRValueJava(int rgb) => rgb >> 16 /*0x10*/ & (int) byte.MaxValue;

      public static int getGValueJava(int rgb) => rgb >> 8 & (int) byte.MaxValue;

      public static int getBValueJava(int rgb) => rgb & (int) byte.MaxValue;

      public static int RGBJava(int r, int g, int b)
      {
        return (r & (int) byte.MaxValue) << 16 /*0x10*/ | (g & (int) byte.MaxValue) << 8 | b & (int) byte.MaxValue;
      }

      public static int swapRGB(int rgb)
      {
        int num1 = rgb >> 16 /*0x10*/ & (int) byte.MaxValue;
        int num2 = rgb >> 8 & (int) byte.MaxValue;
        return (rgb & (int) byte.MaxValue & (int) byte.MaxValue) << 16 /*0x10*/ | (num2 & (int) byte.MaxValue) << 8 | num1 & (int) byte.MaxValue;
      }

      public static Color getColor(int rgb)
      {
        return new Color((int) (byte) (rgb >> 16 /*0x10*/ & (int) byte.MaxValue), (int) (byte) (rgb >> 8 & (int) byte.MaxValue), (int) (byte) (rgb & (int) byte.MaxValue));
      }

      public static Color getColorAlpha(int rgb)
      {
        return new Color(rgb >> 16 /*0x10*/ & (int) byte.MaxValue, rgb >> 8 & (int) byte.MaxValue, rgb & (int) byte.MaxValue, (int) byte.MaxValue);
      }

      public static int clamp(int val, int a, int b) => Math.Min(Math.Max(val, a), b);

      public static int drawText(
        SpriteBatchEffect batch,
        string s,
        short flags,
        CRect rc,
        int rgb,
        CFont font,
        int effect,
        int effectParam)
      {
        if (s.Length == 0)
        {
          if (((int) flags & 1024 /*0x0400*/) != 0)
          {
            rc.right = rc.left;
            rc.bottom = rc.top;
          }
          return 0;
        }
        SpriteFont font1 = font.getFont();
        int num1 = 0;
        int val1_1 = s.IndexOf('\n');
        if (val1_1 < 0)
          return CServices.drawIt(batch, font1, s, (short) ((int) flags | 2048 /*0x0800*/), rc, rgb, effect, effectParam);
        CRect rc1 = new CRect();
        rc1.copyRect(rc);
        int startIndex1 = 0;
        int val1_2 = 0;
        do
        {
          int val2 = -1;
          if (startIndex1 < s.Length)
            val2 = s.IndexOf('\r', startIndex1);
          int num2 = Math.Max(val1_1, val2);
          if (val2 == val1_1 - 1)
            --val1_1;
          string s1 = s.Substring(startIndex1, val1_1 - startIndex1);
          int num3 = CServices.drawIt(batch, font1, s1, (short) ((int) flags | 1024 /*0x0400*/), rc1, rgb, effect, effectParam);
          val1_2 = Math.Max(val1_2, rc1.right - rc1.left);
          num1 += num3;
          rc1.top += num3;
          rc1.bottom = rc.bottom;
          rc1.right = rc.right;
          startIndex1 = num2 + 1;
          val1_1 = -1;
          if (startIndex1 < s.Length)
            val1_1 = s.IndexOf('\n', startIndex1);
        }
        while (val1_1 >= 0);
        if (startIndex1 < s.Length)
        {
          string s2 = s.Substring(startIndex1);
          int num4 = CServices.drawIt(batch, font1, s2, (short) ((int) flags | 1024 /*0x0400*/), rc1, rgb, effect, effectParam);
          val1_2 = Math.Max(val1_2, rc1.right - rc1.left);
          num1 += num4;
        }
        if (((int) flags & 1024 /*0x0400*/) != 0)
        {
          rc.right = rc.left + val1_2;
          rc.bottom = rc1.bottom;
          return num1;
        }
        rc1.copyRect(rc);
        if (((int) flags & 4) != 0)
          rc1.top = rc1.top + (rc1.bottom - rc1.top) / 2 - num1 / 2;
        else if (((int) flags & 8) != 0)
          rc1.top = rc1.bottom - num1;
        int num5 = 0;
        int startIndex2 = 0;
        int val1_3 = s.IndexOf('\n');
        do
        {
          int val2 = -1;
          if (startIndex2 < s.Length)
            val2 = s.IndexOf('\r', startIndex2);
          int num6 = Math.Max(val1_3, val2);
          if (val2 == val1_3 - 1)
            --val1_3;
          string s3 = s.Substring(startIndex2, val1_3 - startIndex2);
          int num7 = CServices.drawIt(batch, font1, s3, flags, rc1, rgb, effect, effectParam);
          num5 += num7;
          rc1.top += num7;
          rc1.bottom = rc.bottom;
          rc1.right = rc.right;
          startIndex2 = num6 + 1;
          val1_3 = -1;
          if (startIndex2 < s.Length)
            val1_3 = s.IndexOf('\n', startIndex2);
        }
        while (val1_3 >= 0);
        if (startIndex2 < s.Length)
        {
          string s4 = s.Substring(startIndex2);
          int num8 = CServices.drawIt(batch, font1, s4, flags, rc1, rgb, effect, effectParam);
          num5 += num8;
        }
        return num5;
      }

      public static int drawIt(
        SpriteBatchEffect batch,
        SpriteFont f,
        string s,
        short flags,
        CRect rc,
        int rgb,
        int effect,
        int effectParam)
      {
        if (s.Length == 0)
          s = " ";
        int lineSpacing = f.LineSpacing;
        int x1 = (int) f.MeasureString(" ").X;
        int num1 = rc.right - rc.left;
        int num2 = 0;
        int num3 = 0;
        int val2 = 0;
        int num4 = 0;
        if (CServices.xPos == null)
          CServices.xPos = new int[100];
        bool flag1 = false;
        bool flag2 = false;
        Color c = Color.Black;
        if (((int) flags & 1024 /*0x0400*/) == 0)
          c = CServices.getColor(rgb);
        int num5 = rc.top;
        int num6 = lineSpacing;
        if ((num6 & 1) != 0)
          ++num6;
        if (((int) flags & 2048 /*0x0800*/) != 0)
        {
          if (((int) flags & 4) != 0)
            num5 = rc.top + (rc.bottom - rc.top) / 2 - num6 / 2;
          else if (((int) flags & 8) != 0)
            num5 = rc.bottom - lineSpacing;
        }
        int num7 = num5;
        do
        {
          int startIndex1 = num2;
          int index1 = 0;
          int val1 = 0;
          num4 += lineSpacing;
          int num8;
          int x2;
          while (true)
          {
            CServices.xPos[index1] = val1;
            ++index1;
            num8 = num3;
            num3 = -1;
            if (startIndex1 < s.Length)
              num3 = s.IndexOf(' ', startIndex1);
            if (num3 == -1)
              num3 = s.Length;
            if (num3 >= startIndex1)
            {
              string text = s.Substring(startIndex1, num3 - startIndex1);
              x2 = (int) f.MeasureString(text).X;
              if (val1 + x2 > num1)
              {
                --index1;
                if (index1 <= 0)
                {
                  for (int startIndex2 = startIndex1; startIndex2 < num3; ++startIndex2)
                  {
                    x2 = (int) f.MeasureString(s.Substring(startIndex2, 1)).X;
                    if (val1 + x2 >= num1)
                    {
                      int startIndex3 = startIndex2 - 1;
                      if (startIndex3 > 0)
                      {
                        val2 = Math.Max(val1, val2);
                        if (((int) flags & 1024 /*0x0400*/) == 0)
                        {
                          val1 = ((int) flags & 1) == 0 ? (((int) flags & 2) == 0 ? rc.left : rc.right - val1) : rc.left + (rc.right - rc.left) / 2 - val1 / 2;
                          string s1 = s.Substring(startIndex1, startIndex3 - startIndex1);
                          CServices.vector.X = (float) val1;
                          CServices.vector.Y = (float) num5;
                          batch.DrawString(f, s1, CServices.vector, c, effect, effectParam);
                        }
                      }
                      num3 = -1;
                      if (startIndex3 < s.Length)
                        num3 = s.IndexOf(' ', startIndex3);
                      flag1 = true;
                      if (num3 >= 0)
                      {
                        flag2 = true;
                        break;
                      }
                      break;
                    }
                    val1 += x2;
                  }
                }
                else
                  goto label_23;
              }
              if (!flag1)
              {
                val1 += x2;
                if (val1 + x1 <= num1)
                {
                  val1 += x1;
                  startIndex1 = num3 + 1;
                }
                else
                  goto label_38;
              }
              else
                goto label_38;
            }
            else
              break;
          }
          val1 -= x1;
          goto label_38;
    label_23:
          int num9 = x2 - x1;
          val1 -= x1;
          num3 = num8;
    label_38:
          if (!flag2)
          {
            if (!flag1)
            {
              val2 = Math.Max(val1, val2);
              if (((int) flags & 1024 /*0x0400*/) == 0)
              {
                int num10 = ((int) flags & 1) == 0 ? (((int) flags & 2) == 0 ? rc.left : rc.right - val1) : rc.left + (rc.right - rc.left) / 2 - val1 / 2;
                int startIndex4 = num2;
                for (int index2 = 0; index2 < index1; ++index2)
                {
                  num3 = -1;
                  if (startIndex4 < s.Length)
                    num3 = s.IndexOf(' ', startIndex4);
                  if (num3 == -1)
                    num3 = s.Length;
                  if (num3 >= startIndex4)
                  {
                    string s2 = s.Substring(startIndex4, num3 - startIndex4);
                    CServices.vector.X = (float) (num10 + CServices.xPos[index2]);
                    CServices.vector.Y = (float) num5;
                    batch.DrawString(f, s2, CServices.vector, c, effect, effectParam);
                    startIndex4 = num3 + 1;
                  }
                  else
                    break;
                }
              }
            }
            else
              break;
          }
          flag1 = false;
          flag2 = false;
          num5 += lineSpacing;
          num2 = num3 + 1;
        }
        while (num2 < s.Length);
        if (((int) flags & 1024 /*0x0400*/) != 0)
        {
          rc.right = rc.left + val2;
          rc.bottom = num7 + num4;
        }
        return num4;
      }

      public static string intToString(int value, int displayFlags)
      {
        string str = $"{value:D}";
        if ((displayFlags & 15) != 0)
        {
          int length = displayFlags & 15;
          if (str.Length > length)
          {
            str = str.Substring(0, length);
          }
          else
          {
            while (str.Length < length)
              str = "0" + str;
          }
        }
        return str;
      }

      public static string doubleToString(double value, int displayFlags)
      {
        string str;
        if ((displayFlags & 512 /*0x0200*/) == 0)
        {
          str = $"{value:G}";
        }
        else
        {
          int num1 = ((displayFlags & 240 /*0xF0*/) >> 4) + 1;
          int num2 = -1;
          if ((displayFlags & 1024 /*0x0400*/) != 0)
            num2 = (displayFlags & 61440 /*0xF000*/) >> 12;
          else if (value != 0.0 && value > -1.0 && value < 1.0)
            num2 = num1;
          str = num2 >= 0 ? string.Format($"{{0:F{num2.ToString()}}}", (object) value) : string.Format($"{{0:G{num1.ToString()}}}", (object) value);
          if ((displayFlags & 2048 /*0x0800*/) != 0)
          {
            int num3 = 0;
            for (int index = 0; index < str.Length; ++index)
            {
              switch (str[index])
              {
                case '+':
                case '-':
                case '.':
                case 'E':
                case 'e':
                  continue;
                default:
                  ++num3;
                  continue;
              }
            }
            bool flag = false;
            if (str[0] == '-')
            {
              flag = true;
              str = str.Substring(1);
            }
            for (; num3 < num1; ++num3)
              str = "0" + str;
            if (flag)
              str = "-" + str;
          }
        }
        return str;
      }

      public static int getNextPowerOfTwo(int value)
      {
        uint num1 = (uint) (value - 1);
        uint num2 = num1 | num1 >> 1;
        uint num3 = num2 | num2 >> 2;
        uint num4 = num3 | num3 >> 4;
        uint num5 = num4 | num4 >> 8;
        return (int) ((num5 | num5 >> 16 /*0x10*/) + 1U);
      }

      public void createThePixel(SpriteBatchEffect batch)
      {
        this.pixel = new Texture2D(batch.GraphicsDevice, 1, 1);
        this.pixel.SetData<Color>(new Color[1]{ Color.White });
      }

      public void drawFilledRectangle(
        CRunApp app,
        int x,
        int y,
        int width,
        int height,
        int rgb,
        int thickness,
        int borderColor,
        int effect,
        int effectParam)
      {
        Color color1 = CServices.getColor(rgb);
        this.drawFilledRectangleSub(app.spriteBatch, x, y, width, height, color1, effect, effectParam);
        if (thickness <= 0)
          return;
        Color color2 = CServices.getColor(borderColor);
        this.drawFilledRectangleSub(app.spriteBatch, x, y, width, thickness, color2, effect, effectParam);
        this.drawFilledRectangleSub(app.spriteBatch, x, y + height - thickness, width, thickness, color2, effect, effectParam);
        this.drawFilledRectangleSub(app.spriteBatch, x, y, thickness, height, color2, effect, effectParam);
        this.drawFilledRectangleSub(app.spriteBatch, x + width - thickness, y, thickness, height, color2, effect, effectParam);
      }

      public void drawRect(SpriteBatchEffect batch, CRect rc, int rgb, int effect, int effectParam)
      {
        int width = rc.right - rc.left;
        int height = rc.bottom - rc.top;
        Color color = CServices.getColor(rgb);
        this.drawFilledRectangleSub(batch, rc.left, rc.top, width, 1, color, effect, effectParam);
        this.drawFilledRectangleSub(batch, rc.left, rc.bottom - 1, width, 1, color, effect, effectParam);
        this.drawFilledRectangleSub(batch, rc.left, rc.top, 1, height, color, effect, effectParam);
        this.drawFilledRectangleSub(batch, rc.right - 1, rc.top, 1, height, color, effect, effectParam);
      }

      public void drawRect(
        SpriteBatchEffect batch,
        int x1,
        int y1,
        int width,
        int height,
        int rgb,
        int effect,
        int effectParam)
      {
        Color color = CServices.getColor(rgb);
        this.drawFilledRectangleSub(batch, x1, y1, width, 1, color, effect, effectParam);
        this.drawFilledRectangleSub(batch, x1, y1 + height - 1, width, 1, color, effect, effectParam);
        this.drawFilledRectangleSub(batch, x1, y1, 1, height, color, effect, effectParam);
        this.drawFilledRectangleSub(batch, x1 + width - 1, y1, 1, height, color, effect, effectParam);
      }

      public void fillRect(SpriteBatchEffect batch, CRect rc, int rgb, int effect, int effectParam)
      {
        Color color = CServices.getColor(rgb);
        this.drawFilledRectangleSub(batch, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, color, effect, effectParam);
      }

      public void fillRect(
        SpriteBatchEffect batch,
        int x1,
        int y1,
        int width,
        int height,
        int rgb,
        int effect,
        int effectParam)
      {
        Color color = CServices.getColor(rgb);
        this.drawFilledRectangleSub(batch, x1, y1, width, height, color, effect, effectParam);
      }

      public void drawFilledRectangleSub(
        SpriteBatchEffect batch,
        int x,
        int y,
        int width,
        int height,
        Color color,
        int effect,
        int effectParam)
      {
        if (this.pixel == null)
          this.createThePixel(batch);
        this.tempRect.X = x;
        this.tempRect.Y = y;
        this.tempRect.Width = width;
        this.tempRect.Height = height;
        batch.Draw(this.pixel, this.tempRect, new Rectangle?(), color, effect, effectParam);
      }

      private static void drawColorLine(
        Color[] colors,
        int x1,
        int y1,
        int x2,
        int width,
        Color color)
      {
        int num = y1 * width;
        for (int index = x1; index < x2; ++index)
          colors[index + num] = color;
      }

      public static Texture2D createUpArrow(CRunApp app, int width, int height, int rgb)
      {
        if (width == 0 || height == 0)
          return (Texture2D) null;
        int nextPowerOfTwo = CServices.getNextPowerOfTwo(width);
        Texture2D upArrow = new Texture2D(app.spriteBatch.GraphicsDevice, nextPowerOfTwo, height);
        Color[] colorArray = new Color[nextPowerOfTwo * height];
        Color color = CServices.getColor(rgb);
        for (double y1 = 0.0; y1 < (double) height; ++y1)
        {
          double x1 = (double) (width / 2) - y1 / (double) height * (double) (width / 2);
          double x2 = (double) (width / 2) + y1 / (double) height * (double) (width / 2);
          CServices.drawColorLine(colorArray, (int) x1, (int) y1, (int) x2, nextPowerOfTwo, color);
        }
        upArrow.SetData<Color>(colorArray);
        return upArrow;
      }

      public static Texture2D createDownArrow(CRunApp app, int width, int height, int rgb)
      {
        if (width == 0 || height == 0)
          return (Texture2D) null;
        int nextPowerOfTwo = CServices.getNextPowerOfTwo(width);
        Texture2D downArrow = new Texture2D(app.spriteBatch.GraphicsDevice, nextPowerOfTwo, height);
        Color[] colorArray = new Color[nextPowerOfTwo * height];
        Color color = CServices.getColor(rgb);
        for (double y1 = 0.0; y1 < (double) height; ++y1)
        {
          double x1 = y1 / (double) height * (double) (width / 2);
          double x2 = (double) width - y1 / (double) height * (double) (width / 2);
          CServices.drawColorLine(colorArray, (int) x1, (int) y1, (int) x2, nextPowerOfTwo, color);
        }
        downArrow.SetData<Color>(colorArray);
        return downArrow;
      }

      public static Texture2D createRoundedRect(
        CRunApp app,
        int width,
        int height,
        int colorRect1,
        int colorRect2,
        int colorFill1,
        int colorFill2)
      {
        if (width == 0 || height == 0)
          return (Texture2D) null;
        int nextPowerOfTwo = CServices.getNextPowerOfTwo(width);
        Texture2D roundedRect = new Texture2D(app.spriteBatch.GraphicsDevice, nextPowerOfTwo, height);
        Color[] colorArray = new Color[nextPowerOfTwo * height];
        Color color = new Color(0, 0, 0, 0);
        for (int index1 = 0; index1 < height; ++index1)
        {
          int num = nextPowerOfTwo * index1;
          for (int index2 = width; index2 < nextPowerOfTwo; ++index2)
            colorArray[num + index2] = color;
        }
        float num1 = (float) (colorRect1 >> 16 /*0x10*/ & (int) byte.MaxValue);
        float num2 = (float) (colorRect1 >> 8 & (int) byte.MaxValue);
        float num3 = (float) (colorRect1 & (int) byte.MaxValue);
        float num4 = (float) (colorRect2 >> 16 /*0x10*/ & (int) byte.MaxValue);
        float num5 = (float) (colorRect2 >> 8 & (int) byte.MaxValue);
        float num6 = (float) (colorRect2 & (int) byte.MaxValue);
        float num7 = (float) (colorFill1 >> 16 /*0x10*/ & (int) byte.MaxValue);
        float num8 = (float) (colorFill1 >> 8 & (int) byte.MaxValue);
        float num9 = (float) (colorFill1 & (int) byte.MaxValue);
        float num10 = (float) (colorFill2 >> 16 /*0x10*/ & (int) byte.MaxValue);
        float num11 = (float) (colorFill2 >> 8 & (int) byte.MaxValue);
        float num12 = (float) (colorFill2 & (int) byte.MaxValue);
        int num13 = height / 6;
        double num14 = Math.PI / 100.0;
        float num15 = (num10 - num7) / (float) height;
        float num16 = (num11 - num8) / (float) height;
        float num17 = (num12 - num9) / (float) height;
        float num18 = (num4 - num1) / (float) height;
        float num19 = (num5 - num2) / (float) height;
        float num20 = (num6 - num3) / (float) height;
        int num21 = -1;
        for (double num22 = 0.0; num22 < Math.PI / 2.0; num22 += num14)
        {
          int x1 = (int) ((double) num13 - (double) num13 * Math.Cos(num22));
          int x2 = (int) ((double) (width - num13) + (double) num13 * Math.Cos(num22));
          int y1 = (int) ((double) num13 - (double) num13 * Math.Sin(num22));
          if (y1 != num21)
          {
            num21 = y1;
            color = new Color((int) (byte) (num7 + num15 * (float) y1), (int) (byte) (num8 + num16 * (float) y1), (int) (byte) (num9 + num17 * (float) y1));
            CServices.drawColorLine(colorArray, x1, y1, x2, nextPowerOfTwo, color);
            color = new Color((int) (byte) (num1 + num18 * (float) y1), (int) (byte) (num2 + num19 * (float) y1), (int) (byte) (num3 + num20 * (float) y1));
            if (y1 == 0)
            {
              CServices.drawColorLine(colorArray, x1, y1, x2, nextPowerOfTwo, color);
            }
            else
            {
              colorArray[x1 + y1 * nextPowerOfTwo] = color;
              colorArray[x2 + y1 * nextPowerOfTwo] = color;
            }
          }
        }
        for (int y1 = num13; y1 < height - num13; ++y1)
        {
          color = new Color((int) (byte) (num7 + num15 * (float) y1), (int) (byte) (num8 + num16 * (float) y1), (int) (byte) (num9 + num17 * (float) y1));
          CServices.drawColorLine(colorArray, 0, y1, width, nextPowerOfTwo, color);
          color = new Color((int) (byte) (num1 + num18 * (float) y1), (int) (byte) (num2 + num19 * (float) y1), (int) (byte) (num3 + num20 * (float) y1));
          colorArray[y1 * nextPowerOfTwo] = color;
          colorArray[width - 1 + y1 * nextPowerOfTwo] = color;
        }
        for (double num23 = num14; num23 < Math.PI / 2.0; num23 += num14)
        {
          int x1 = (int) ((double) num13 - (double) num13 * Math.Cos(num23));
          int x2 = (int) ((double) (width - num13) + (double) num13 * Math.Cos(num23));
          int y1 = (int) ((double) (height - num13) + (double) num13 * Math.Sin(num23));
          if (y1 != num21)
          {
            num21 = y1;
            color = new Color((int) (byte) (num7 + num15 * (float) y1), (int) (byte) (num8 + num16 * (float) y1), (int) (byte) (num9 + num17 * (float) y1));
            CServices.drawColorLine(colorArray, x1, y1, x2, nextPowerOfTwo, color);
            color = new Color((int) (byte) (num1 + num18 * (float) y1), (int) (byte) (num2 + num19 * (float) y1), (int) (byte) (num3 + num20 * (float) y1));
            colorArray[x1 + y1 * nextPowerOfTwo] = color;
            colorArray[x2 + y1 * nextPowerOfTwo] = color;
            if (y1 == height - 1)
              CServices.drawColorLine(colorArray, x1, y1, x2, nextPowerOfTwo, color);
          }
        }
        roundedRect.SetData<Color>(colorArray);
        return roundedRect;
      }

      public static Texture2D createGradientRectangle(
        CRunApp app,
        int width,
        int height,
        int color1,
        int color2,
        bool bVertical,
        int thickness,
        int borderColor)
      {
        if (width == 0 || height == 0)
          return (Texture2D) null;
        int nextPowerOfTwo = CServices.getNextPowerOfTwo(width);
        Texture2D gradientRectangle = new Texture2D(app.spriteBatch.GraphicsDevice, nextPowerOfTwo, height);
        Color[] colorArray = new Color[nextPowerOfTwo * height];
        float r = (float) (color1 >> 16 /*0x10*/ & (int) byte.MaxValue);
        float g = (float) (color1 >> 8 & (int) byte.MaxValue);
        float b = (float) (color1 & (int) byte.MaxValue);
        float num1 = (float) (color2 >> 16 /*0x10*/ & (int) byte.MaxValue);
        float num2 = (float) (color2 >> 8 & (int) byte.MaxValue);
        float num3 = (float) (color2 & (int) byte.MaxValue);
        Color color = new Color(r, g, b);
        float num4 = r;
        float num5 = g;
        float num6 = b;
        if (bVertical)
        {
          float num7 = (num1 - r) / (float) height;
          float num8 = (num2 - g) / (float) height;
          float num9 = (num3 - b) / (float) height;
          for (int index1 = 0; index1 < height; ++index1)
          {
            int num10 = index1 * nextPowerOfTwo;
            if ((double) r != (double) num4 || (double) g != (double) num5 || (double) b != (double) num6)
              color = new Color((int) (byte) r, (int) (byte) g, (int) (byte) b);
            for (int index2 = 0; index2 < width; ++index2)
              colorArray[num10 + index2] = color;
            r += num7;
            g += num8;
            b += num9;
          }
        }
        else
        {
          float num11 = (num1 - r) / (float) width;
          float num12 = (num2 - g) / (float) width;
          float num13 = (num3 - b) / (float) width;
          for (int index3 = 0; index3 < width; ++index3)
          {
            if ((double) r != (double) num4 || (double) g != (double) num5 || (double) b != (double) num6)
              color = new Color((int) (byte) r, (int) (byte) g, (int) (byte) b);
            for (int index4 = 0; index4 < height; ++index4)
              colorArray[index4 * nextPowerOfTwo + index3] = color;
            r += num11;
            g += num12;
            b += num13;
          }
        }
        color = new Color(0, 0, 0, 0);
        for (int index5 = 0; index5 < height; ++index5)
        {
          int num14 = nextPowerOfTwo * index5;
          for (int index6 = width; index6 < nextPowerOfTwo; ++index6)
            colorArray[num14 + index6] = color;
        }
        if (thickness > 0)
        {
          color = CServices.getColor(borderColor);
          CServices.fillRectangle(colorArray, nextPowerOfTwo, 0, 0, width, thickness, color);
          CServices.fillRectangle(colorArray, nextPowerOfTwo, 0, height - thickness, width, height, color);
          CServices.fillRectangle(colorArray, nextPowerOfTwo, 0, 0, thickness, height, color);
          CServices.fillRectangle(colorArray, nextPowerOfTwo, width - thickness, 0, width, height, color);
        }
        gradientRectangle.SetData<Color>(colorArray);
        return gradientRectangle;
      }

      private static void fillRectangle(
        Color[] colors,
        int textureWidth,
        int x1,
        int y1,
        int x2,
        int y2,
        Color color)
      {
        for (int index1 = y1; index1 < y2; ++index1)
        {
          int num = index1 * textureWidth;
          for (int index2 = x1; index2 < x2; ++index2)
            colors[num + index2] = color;
        }
      }

      public void drawPatternRectangle(
        SpriteBatchEffect batch,
        CImage image,
        int xx,
        int yy,
        int width,
        int height,
        int thickness,
        int borderColor,
        int effect,
        int effectParam)
      {
        int num1 = (width + (int) image.width - 1) / (int) image.width;
        int num2 = (height + (int) image.height - 1) / (int) image.height;
        this.tempRect.Width = (int) image.width;
        this.tempRect.Height = (int) image.height;
        Texture2D texture = image.image;
        Rectangle? sourceRectangle = new Rectangle?();
        if (image.mosaic != (short) 0)
        {
          texture = image.app.imageBank.mosaics[(int) image.mosaic];
          sourceRectangle = new Rectangle?(image.mosaicRectangle);
        }
        for (int index1 = 0; index1 < num1; ++index1)
        {
          for (int index2 = 0; index2 < num2; ++index2)
          {
            this.tempRect.X = xx + index1 * (int) image.width;
            this.tempRect.Y = yy + index2 * (int) image.height;
            batch.Draw(texture, this.tempRect, sourceRectangle, Color.White, effect, effectParam);
          }
        }
        if (thickness <= 0)
          return;
        int width1 = num1 * (int) image.width;
        int height1 = num2 * (int) image.height;
        Color color = CServices.getColor(borderColor);
        this.drawFilledRectangleSub(batch, xx, yy, width1, thickness, color, effect, effectParam);
        this.drawFilledRectangleSub(batch, xx, yy + height1 - thickness, width1, thickness, color, effect, effectParam);
        this.drawFilledRectangleSub(batch, xx, yy, thickness, height1, color, effect, effectParam);
        this.drawFilledRectangleSub(batch, xx + width1 - thickness, yy, thickness, height1, color, effect, effectParam);
      }

      public static Texture2D createEllipse(
        CRunApp app,
        int width,
        int height,
        int borderWidth,
        int borderColor)
      {
        int nextPowerOfTwo = CServices.getNextPowerOfTwo(width);
        Texture2D ellipse = new Texture2D(app.spriteBatch.GraphicsDevice, nextPowerOfTwo, height);
        Color[] colorArray = new Color[nextPowerOfTwo * height];
        int num1 = width / 2;
        int num2 = height / 2;
        Color color1 = new Color(0, 0, 0, 0);
        int num3 = nextPowerOfTwo * height;
        for (int index = 0; index < num3; ++index)
          colorArray[index] = color1;
        Color color2 = CServices.getColor(borderColor);
        CServices.createEllipse(colorArray, nextPowerOfTwo, width, height, borderWidth, color2);
        ellipse.SetData<Color>(colorArray);
        return ellipse;
      }

      public static Texture2D createFilledEllipse(
        CRunApp app,
        int width,
        int height,
        int rgb,
        int borderWidth,
        int borderColor)
      {
        int nextPowerOfTwo = CServices.getNextPowerOfTwo(width);
        Texture2D filledEllipse = new Texture2D(app.spriteBatch.GraphicsDevice, nextPowerOfTwo, height);
        Color[] colorArray = new Color[nextPowerOfTwo * height];
        int num1 = width / 2 - 1;
        int num2 = height / 2 - 1;
        Color color1 = new Color(0, 0, 0, 0);
        int num3 = nextPowerOfTwo * height;
        for (int index = 0; index < num3; ++index)
          colorArray[index] = color1;
        double num4 = Math.PI / 1000.0;
        int num5 = -1;
        Color color2 = CServices.getColor(rgb);
        for (double num6 = 0.0; num6 < Math.PI; num6 += num4)
        {
          int num7 = (int) ((double) num2 - (double) num2 * Math.Sin(Math.PI / 2.0 + num6));
          if (num7 != num5)
          {
            int num8 = (int) ((double) num1 + (double) num1 * Math.Cos(Math.PI / 2.0 + num6));
            int num9 = (int) ((double) num1 + (double) num1 * Math.Cos(Math.PI / 2.0 - num6));
            int num10 = num7 * nextPowerOfTwo;
            for (int index = num8; index < num9; ++index)
              colorArray[num10 + index] = color2;
            num5 = num7;
          }
        }
        if (borderWidth > 0)
        {
          Color color3 = CServices.getColor(borderColor);
          CServices.createEllipse(colorArray, nextPowerOfTwo, width, height, borderWidth, color3);
        }
        filledEllipse.SetData<Color>(colorArray);
        return filledEllipse;
      }

      public static Texture2D createGradientEllipse(
        CRunApp app,
        int width,
        int height,
        int color1,
        int color2,
        bool bVertical,
        int borderWidth,
        int borderColor)
      {
        int nextPowerOfTwo = CServices.getNextPowerOfTwo(width);
        Texture2D gradientEllipse = new Texture2D(app.spriteBatch.GraphicsDevice, nextPowerOfTwo, height);
        Color[] colorArray = new Color[nextPowerOfTwo * height];
        int num1 = width / 2 - 1;
        int num2 = height / 2 - 1;
        Color color = new Color(0, 0, 0, 0);
        int num3 = nextPowerOfTwo * height;
        for (int index = 0; index < num3; ++index)
          colorArray[index] = color;
        float r = (float) (color1 >> 16 /*0x10*/ & (int) byte.MaxValue);
        float g = (float) (color1 >> 8 & (int) byte.MaxValue);
        float b = (float) (color1 & (int) byte.MaxValue);
        float num4 = (float) (color2 >> 16 /*0x10*/ & (int) byte.MaxValue);
        float num5 = (float) (color2 >> 8 & (int) byte.MaxValue);
        float num6 = (float) (color2 & (int) byte.MaxValue);
        color = new Color(r, g, b);
        float num7 = r;
        float num8 = g;
        float num9 = b;
        double num10 = Math.PI / 1000.0;
        int num11 = -1;
        int num12 = -1;
        if (bVertical)
        {
          float num13 = (num4 - r) / (float) height;
          float num14 = (num5 - g) / (float) height;
          float num15 = (num6 - b) / (float) height;
          for (double num16 = 0.0; num16 < Math.PI; num16 += num10)
          {
            int num17 = (int) ((double) num2 - (double) num2 * Math.Sin(Math.PI / 2.0 + num16));
            if (num17 != num12)
            {
              if ((double) r != (double) num7 || (double) g != (double) num8 || (double) b != (double) num9)
                color = new Color((int) (byte) r, (int) (byte) g, (int) (byte) b);
              int num18 = (int) ((double) num1 + (double) num1 * Math.Cos(Math.PI / 2.0 + num16));
              int num19 = (int) ((double) num1 + (double) num1 * Math.Cos(Math.PI / 2.0 - num16));
              int num20 = num17 * nextPowerOfTwo;
              for (int index = num18; index < num19; ++index)
                colorArray[num20 + index] = color;
              num12 = num17;
              r += num13;
              g += num14;
              b += num15;
            }
          }
        }
        else
        {
          float num21 = (num4 - r) / (float) width;
          float num22 = (num5 - g) / (float) width;
          float num23 = (num6 - b) / (float) width;
          for (double num24 = 0.0; num24 < Math.PI; num24 += num10)
          {
            int num25 = (int) ((double) num1 + (double) num1 * Math.Cos(Math.PI - num24));
            if (num25 != num11)
            {
              if ((double) r != (double) num7 || (double) g != (double) num8 || (double) b != (double) num9)
                color = new Color((int) (byte) r, (int) (byte) g, (int) (byte) b);
              int num26 = (int) ((double) num2 - (double) num2 * Math.Sin(Math.PI - num24));
              int num27 = (int) ((double) num2 - (double) num2 * Math.Sin(Math.PI + num24));
              for (int index = num26; index < num27; ++index)
                colorArray[index * nextPowerOfTwo + num25] = color;
              num11 = num25;
              r += num21;
              g += num22;
              b += num23;
            }
          }
        }
        if (borderWidth > 0)
        {
          color = CServices.getColor(borderColor);
          CServices.createEllipse(colorArray, nextPowerOfTwo, width, height, borderWidth, color);
        }
        gradientEllipse.SetData<Color>(colorArray);
        return gradientEllipse;
      }

      private static void createEllipse(
        Color[] colors,
        int textureWidth,
        int width,
        int height,
        int thickness,
        Color color)
      {
        int num1 = width / 2 - 1;
        int num2 = height / 2 - 1;
        int num3 = num1;
        int num4 = num2;
        double num5 = Math.PI / 1000.0;
        for (int index = 0; index < thickness; ++index)
        {
          for (double num6 = 0.0; num6 < 2.0 * Math.PI; num6 += num5)
          {
            int num7 = (int) ((double) num3 + (double) num1 * Math.Cos(Math.PI / 2.0 + num6));
            int num8 = (int) ((double) num4 - (double) num2 * Math.Sin(Math.PI / 2.0 + num6));
            colors[num8 * textureWidth + num7] = color;
          }
          --num1;
          --num2;
        }
      }

      public void drawLine(
        SpriteBatchEffect batch,
        int x1,
        int y1,
        int x2,
        int y2,
        int rgb,
        int thickness,
        int effect,
        int effectParam)
      {
        if (this.pixel == null)
          this.createThePixel(batch);
        Vector2 position = new Vector2((float) x1, (float) y1);
        Vector2 vector2 = new Vector2((float) x2, (float) y2);
        float x = Vector2.Distance(position, vector2);
        float rotation = (float) Math.Atan2((double) vector2.Y - (double) position.Y, (double) vector2.X - (double) position.X);
        Color color = CServices.getColor(rgb);
        batch.Draw(this.pixel, position, new Rectangle?(), color, rotation, Vector2.Zero, new Vector2(x, (float) thickness), SpriteEffects.None, 0.0f, effect, effectParam);
      }

      public static void replaceColor(
        CRunApp app,
        Color[] pixels,
        int width,
        int height,
        int oldColor,
        int newColor)
      {
        Color color = CServices.getColor(newColor);
        byte rvalueJava = (byte) CServices.getRValueJava(oldColor);
        byte gvalueJava = (byte) CServices.getGValueJava(oldColor);
        byte bvalueJava = (byte) CServices.getBValueJava(oldColor);
        for (int index1 = 0; index1 < height; ++index1)
        {
          for (int index2 = 0; index2 < width; ++index2)
          {
            Color pixel = pixels[index1 * width + index2];
            if ((int) pixel.R == (int) rvalueJava && (int) pixel.G == (int) gvalueJava && (int) pixel.B == (int) bvalueJava)
            {
              color.A = newColor == 0 ? (byte) 0 : pixel.A;
              pixels[index1 * width + index2] = color;
            }
          }
        }
      }
    }
}
