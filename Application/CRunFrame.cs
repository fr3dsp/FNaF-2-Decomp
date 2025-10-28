// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Application.CRunFrame
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Events;
using RuntimeXNA.Frame;
using RuntimeXNA.Objects;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;
using System;

namespace RuntimeXNA.Application
{

    public class CRunFrame
    {
      public const int LEF_DISPLAYNAME = 1;
      public const int LEF_GRABDESKTOP = 2;
      public const int LEF_KEEPDISPLAY = 4;
      public const int LEF_TOTALCOLMASK = 32 /*0x20*/;
      public const int LEF_RESIZEATSTART = 256 /*0x0100*/;
      public const int LEF_NOSURFACE = 2048 /*0x0800*/;
      public const int LEF_TIMEDMVTS = 32768 /*0x8000*/;
      public const int IPHONEOPT_JOYSTICK_FIRE1 = 1;
      public const int IPHONEOPT_JOYSTICK_FIRE2 = 2;
      public const int IPHONEOPT_JOYSTICK_LEFTHAND = 4;
      public const int IPHONEOPT_MULTITOUCH = 8;
      public const int IPHONEOPT_SCREENLOCKING = 16 /*0x10*/;
      public const int IPHONEOPT_IPHONEFRAMEIAD = 32 /*0x20*/;
      public const int JOYSTICK_NONE = 0;
      public const int JOYSTICK_TOUCH = 1;
      public const int JOYSTICK_ACCELEROMETER = 2;
      public const int JOYSTICK_EXT = 3;
      public int leWidth;
      public int leHeight;
      public int leBackground;
      public int leFlags;
      public CRect leVirtualRect;
      public int leEditWinWidth;
      public int leEditWinHeight;
      public string frameName;
      public int nLayers;
      public CLayer[] layers;
      public CLOList LOList;
      public CEventProgram evtProg;
      public short maxObjects = 512 /*0x0200*/;
      public int leX;
      public int leY;
      public int leLastScrlX;
      public int leLastScrlY;
      public CRect fadeIn;
      public CRect fadeOut;
      public int levelQuit;
      public bool rhOK;
      public int startLeX;
      public int startLeY;
      public bool fade;
      public int fadeTimerDelta;
      public int fadeVblDelta;
      public int dwColMaskBits;
      public CColMask colMask;
      public short m_wRandomSeed;
      public int m_dwMvtTimerBase;
      public CRunApp app;
      public CRun rhPtr;
      public short joystick;
      public short iPhoneOptions;
      public short[] mosaicHandles;
      public int[] mosaicX;
      public int[] mosaicY;
      public int mosaicMaxHandle;

      public CRunFrame()
      {
      }

      public CRunFrame(CRunApp pApp) => this.app = pApp;

      public bool loadFullFrame(int index)
      {
        this.app.file.seek(this.app.frameOffsets[index]);
        this.evtProg = new CEventProgram();
        this.LOList = new CLOList();
        this.leVirtualRect = new CRect();
        CChunk cchunk = new CChunk();
        int num1 = 0;
        int num2 = 0;
        this.m_wRandomSeed = (short) -1;
        while (cchunk.chID != (short) 32639)
        {
          int num3 = (int) cchunk.readHeader(this.app.file);
          if (cchunk.chSize != 0)
          {
            int pos = this.app.file.getFilePointer() + cchunk.chSize;
            switch (cchunk.chID)
            {
              case 13108:
                this.loadHeader();
                this.leEditWinWidth = Math.Min(this.app.gaCxWin, this.leWidth);
                this.leEditWinHeight = Math.Min(this.app.gaCyWin, this.leHeight);
                break;
              case 13109:
                this.frameName = this.app.file.readAString();
                break;
              case 13112:
                this.LOList.load(this.app);
                break;
              case 13117:
                this.evtProg.load(this.app);
                this.maxObjects = this.evtProg.maxObjects;
                break;
              case 13121:
                this.loadLayers();
                break;
              case 13122:
                this.leVirtualRect.load(this.app.file);
                if ((this.leFlags & 256 /*0x0100*/) != 0)
                {
                  if (this.leVirtualRect.right - this.leVirtualRect.left == num1 || this.leVirtualRect.right - this.leVirtualRect.left < this.leWidth)
                    this.leVirtualRect.right = this.leVirtualRect.left + this.leWidth;
                  if (this.leVirtualRect.bottom - this.leVirtualRect.top == num2 || this.leVirtualRect.bottom - this.leVirtualRect.top < this.leHeight)
                  {
                    this.leVirtualRect.bottom = this.leVirtualRect.top + this.leHeight;
                    break;
                  }
                  break;
                }
                break;
              case 13124:
                this.m_wRandomSeed = this.app.file.readAShort();
                break;
              case 13127:
                this.m_dwMvtTimerBase = this.app.file.readAInt();
                break;
              case 13128:
                int length = cchunk.chSize / 6;
                this.mosaicHandles = new short[length];
                this.mosaicX = new int[length];
                this.mosaicY = new int[length];
                this.mosaicMaxHandle = 0;
                for (int index1 = 0; index1 < length; ++index1)
                {
                  this.mosaicHandles[index1] = this.app.file.readAShort();
                  this.mosaicMaxHandle = Math.Max(this.mosaicMaxHandle, (int) this.mosaicHandles[index1]);
                  this.mosaicX[index1] = (int) this.app.file.readAShort();
                  this.mosaicY[index1] = (int) this.app.file.readAShort();
                }
                ++this.mosaicMaxHandle;
                break;
              case 13130:
                this.joystick = this.app.file.readAShort();
                this.iPhoneOptions = this.app.file.readAShort();
                break;
            }
            this.app.file.seek(pos);
          }
        }
        this.app.OIList.resetToLoad();
        for (int index2 = 0; index2 < this.LOList.nIndex; ++index2)
          this.app.OIList.setToLoad((int) this.LOList.getLOFromIndex((short) index2).loOiHandle);
        this.app.imageBank.resetToLoad();
        this.app.fontBank.resetToLoad();
        this.app.OIList.load(this.app.file, this.app);
        this.app.OIList.enumElements((IEnum) this.app.imageBank, (IEnum) this.app.fontBank);
        this.app.imageBank.load();
        this.app.fontBank.load();
        this.evtProg.enumSounds((IEnum) this.app.soundBank);
        this.app.soundBank.load();
        this.app.OIList.resetOICurrent();
        for (int index3 = 0; index3 < this.LOList.nIndex; ++index3)
        {
          CLO clo = this.LOList.list[index3];
          if (clo.loType >= (short) 2)
            this.app.OIList.setOICurrent((int) clo.loOiHandle);
        }
        return true;
      }

      public void loadLayers()
      {
        this.nLayers = this.app.file.readAInt();
        this.layers = new CLayer[this.nLayers];
        for (int index = 0; index < this.nLayers; ++index)
        {
          this.layers[index] = new CLayer();
          this.layers[index].load(this.app.file);
        }
      }

      public void loadHeader()
      {
        this.leWidth = this.app.file.readAInt();
        this.leHeight = this.app.file.readAInt();
        this.leBackground = this.app.file.readAColor();
        this.leFlags = this.app.file.readAInt();
      }

      public int getMaskBits()
      {
        int maskBits = 0;
        for (int index = 0; index < this.LOList.nIndex; ++index)
        {
          CLO loFromIndex = this.LOList.getLOFromIndex((short) index);
          if (loFromIndex.loLayer <= (short) 0)
          {
            COI oiFromHandle = this.app.OIList.getOIFromHandle(loFromIndex.loOiHandle);
            if (oiFromHandle.oiType < (short) 2)
            {
              switch (oiFromHandle.oiOC.ocObstacleType)
              {
                case 1:
                  maskBits |= 1;
                  continue;
                case 2:
                  maskBits |= 2;
                  continue;
                default:
                  continue;
              }
            }
            else
            {
              CObjectCommon oiOc = (CObjectCommon) oiFromHandle.oiOC;
              if ((oiOc.ocOEFlags & 2) != 0)
              {
                switch (((int) oiOc.ocFlags2 & 48 /*0x30*/) >> 4)
                {
                  case 1:
                    maskBits |= 1;
                    continue;
                  case 2:
                    maskBits |= 2;
                    continue;
                  default:
                    continue;
                }
              }
            }
          }
          else
            break;
        }
        return maskBits;
      }

      public bool bkdLevObjCol_TestPoint(int x, int y, int nTestLayer, int nPlane)
      {
        CRect crect = new CRect();
        int num1;
        int num2;
        if (nTestLayer == -1)
        {
          num1 = 1;
          num2 = this.nLayers - 1;
        }
        else
        {
          if (nTestLayer >= this.nLayers)
            return false;
          num1 = num2 = nTestLayer;
        }
        int leWidth = this.leWidth;
        int leHeight = this.leHeight;
        for (int index1 = num1; index1 <= num2; ++index1)
        {
          CLayer layer = this.layers[index1];
          bool flag1 = (layer.dwOptions & 32 /*0x20*/) != 0;
          bool flag2 = (layer.dwOptions & 64 /*0x40*/) != 0;
          bool flag3 = flag1 | flag2;
          int num3 = this.leX;
          int num4 = this.leY;
          if ((layer.dwOptions & 3) != 0)
          {
            if ((layer.dwOptions & 1) != 0)
              num3 = (int) ((double) num3 * (double) layer.xCoef);
            if ((layer.dwOptions & 2) != 0)
              num4 = (int) ((double) num4 * (double) layer.yCoef);
          }
          int num5 = num3 + layer.x;
          int num6 = num4 + layer.y;
          if (flag1)
            num5 %= leWidth;
          if (flag2)
            num6 %= leHeight;
          uint num7 = 0;
          int num8 = 0;
          int nBkdLos = layer.nBkdLOs;
          for (int index2 = 0; index2 < nBkdLos; ++index2)
          {
            CLO loFromIndex = this.LOList.getLOFromIndex((short) (layer.nFirstLOIndex + index2));
            CObject cobject = (CObject) null;
            COI oiFromHandle = this.app.OIList.getOIFromHandle(loFromIndex.loOiHandle);
            if (oiFromHandle != null && oiFromHandle.oiOC != null)
            {
              COC oiOc = oiFromHandle.oiOC;
              int oiType = (int) oiFromHandle.oiType;
              crect.left = loFromIndex.loX - num5;
              crect.top = loFromIndex.loY - num6;
              int num9;
              int num10;
              if (oiType < 2)
              {
                num9 = (int) oiOc.ocObstacleType;
                switch (num9)
                {
                  case 0:
                  case 3:
                  case 4:
                    continue;
                  default:
                    num10 = (int) oiOc.ocColMode;
                    crect.right = crect.left + oiOc.ocCx;
                    crect.bottom = crect.top + oiOc.ocCy;
                    break;
                }
              }
              else
              {
                CObjectCommon cobjectCommon = (CObjectCommon) oiOc;
                if ((cobjectCommon.ocOEFlags & 2) != 0 && (cobject = this.rhPtr.find_HeaderObject(loFromIndex.loHandle)) != null)
                {
                  num9 = ((int) cobjectCommon.ocFlags2 & 48 /*0x30*/) >> 4;
                  switch (num9)
                  {
                    case 0:
                    case 3:
                    case 4:
                      continue;
                    default:
                      num10 = ((int) cobjectCommon.ocFlags2 & 4) != 0 ? 1 : 0;
                      crect.left = cobject.hoX - this.leX - cobject.hoImgXSpot;
                      crect.top = cobject.hoY - this.leY - cobject.hoImgYSpot;
                      crect.right = crect.left + cobject.hoImgWidth;
                      crect.bottom = crect.top + cobject.hoImgHeight;
                      break;
                  }
                }
                else
                  continue;
              }
              if (flag3)
              {
                switch (num8)
                {
                  case 0:
                    if (flag1 && (crect.left < 0 || crect.right > leWidth))
                    {
                      if (flag2 && (crect.top < 0 || crect.bottom > leHeight))
                      {
                        num8 = 3;
                        num7 |= 7U;
                        break;
                      }
                      num8 = 1;
                      num7 |= 1U;
                      break;
                    }
                    if (flag2 && (crect.top < 0 || crect.bottom > leHeight))
                    {
                      num8 = 2;
                      num7 |= 2U;
                      break;
                    }
                    break;
                  case 1:
                    if (crect.left < 0)
                    {
                      int num11 = leWidth;
                      crect.left += num11;
                      crect.right += num11;
                    }
                    else if (crect.right > leWidth)
                    {
                      int num12 = leWidth;
                      crect.left -= num12;
                      crect.right -= num12;
                    }
                    num7 &= 4294967294U;
                    num8 = 0;
                    if (((int) num7 & 2) != 0)
                    {
                      num8 = 2;
                      break;
                    }
                    break;
                  case 2:
                    if (crect.top < 0)
                    {
                      int num13 = leHeight;
                      crect.top += num13;
                      crect.bottom += num13;
                    }
                    else if (crect.bottom > leHeight)
                    {
                      int num14 = leHeight;
                      crect.top -= num14;
                      crect.bottom -= num14;
                    }
                    num7 &= 4294967293U;
                    num8 = 0;
                    if (((int) num7 & 1) != 0)
                    {
                      num8 = 1;
                      break;
                    }
                    break;
                  case 3:
                    if (crect.left < 0)
                    {
                      int num15 = leWidth;
                      crect.left += num15;
                      crect.right += num15;
                    }
                    else if (crect.right > leWidth)
                    {
                      int num16 = leWidth;
                      crect.left -= num16;
                      crect.right -= num16;
                    }
                    if (crect.top < 0)
                    {
                      int num17 = leHeight;
                      crect.top += num17;
                      crect.bottom += num17;
                    }
                    else if (crect.bottom > leHeight)
                    {
                      int num18 = leHeight;
                      crect.top -= num18;
                      crect.bottom -= num18;
                    }
                    num7 &= 4294967291U;
                    num8 = 2;
                    break;
                }
              }
              if (x >= crect.left && y >= crect.top && x < crect.right && y < crect.bottom && (num9 != 2 || nPlane != 0))
              {
                if (num10 != 0)
                  return true;
                int flags = 0;
                if (num9 == 2)
                  flags = 1;
                CMask cmask = oiType >= 2 ? cobject.getCollisionMask(flags) : this.app.imageBank.getImageFromHandle(((COCBackground) oiOc).ocImage).getMask(flags, 0, 1f, 1f);
                if (cmask == null || cmask.testPoint(x - crect.left, y - crect.top))
                  return true;
              }
              if (num7 != 0U)
                --index2;
            }
          }
          if (layer.pBkd2 != null)
          {
            uint num19 = 0;
            int num20 = 0;
            for (int index3 = 0; index3 < layer.pBkd2.size(); ++index3)
            {
              CBkd2 cbkd2 = (CBkd2) layer.pBkd2.get(index3);
              crect.left = cbkd2.x - num5;
              crect.top = cbkd2.y - num6;
              int obstacleType = (int) cbkd2.obstacleType;
              switch (obstacleType)
              {
                case 0:
                case 3:
                case 4:
                  continue;
                default:
                  int num21 = cbkd2.colMode == (short) 0 ? 1 : 0;
                  CImage imageFromHandle = this.app.imageBank.getImageFromHandle(cbkd2.img);
                  if (imageFromHandle != null)
                  {
                    crect.right = crect.left + (int) imageFromHandle.width;
                    crect.bottom = crect.top + (int) imageFromHandle.height;
                  }
                  else
                  {
                    crect.right = crect.left + 1;
                    crect.bottom = crect.top + 1;
                  }
                  if (flag3)
                  {
                    switch (num20)
                    {
                      case 0:
                        if (flag1 && (crect.left < 0 || crect.right > leWidth))
                        {
                          if (flag2 && (crect.top < 0 || crect.bottom > leHeight))
                          {
                            num20 = 3;
                            num19 |= 7U;
                            break;
                          }
                          num20 = 1;
                          num19 |= 1U;
                          break;
                        }
                        if (flag2 && (crect.top < 0 || crect.bottom > leHeight))
                        {
                          num20 = 2;
                          num19 |= 2U;
                          break;
                        }
                        break;
                      case 1:
                        if (crect.left < 0)
                        {
                          int num22 = leWidth;
                          crect.left += num22;
                          crect.right += num22;
                        }
                        else if (crect.right > leWidth)
                        {
                          int num23 = leWidth;
                          crect.left -= num23;
                          crect.right -= num23;
                        }
                        num19 &= 4294967294U;
                        num20 = 0;
                        if (((int) num19 & 2) != 0)
                        {
                          num20 = 2;
                          break;
                        }
                        break;
                      case 2:
                        if (crect.top < 0)
                        {
                          int num24 = leHeight;
                          crect.top += num24;
                          crect.bottom += num24;
                        }
                        else if (crect.bottom > leHeight)
                        {
                          int num25 = leHeight;
                          crect.top -= num25;
                          crect.bottom -= num25;
                        }
                        num19 &= 4294967293U;
                        num20 = 0;
                        if (((int) num19 & 1) != 0)
                        {
                          num20 = 1;
                          break;
                        }
                        break;
                      case 3:
                        if (crect.left < 0)
                        {
                          int num26 = leWidth;
                          crect.left += num26;
                          crect.right += num26;
                        }
                        else if (crect.right > leWidth)
                        {
                          int num27 = leWidth;
                          crect.left -= num27;
                          crect.right -= num27;
                        }
                        if (crect.top < 0)
                        {
                          int num28 = leHeight;
                          crect.top += num28;
                          crect.bottom += num28;
                        }
                        else if (crect.bottom > leHeight)
                        {
                          int num29 = leHeight;
                          crect.top -= num29;
                          crect.bottom -= num29;
                        }
                        num19 &= 4294967291U;
                        num20 = 2;
                        break;
                    }
                  }
                  if (x >= crect.left && y >= crect.top && x < crect.right && y < crect.bottom && (obstacleType != 2 || nPlane != 0))
                  {
                    if (num21 != 0)
                      return true;
                    int flags = 0;
                    if (obstacleType == 2)
                      flags = 1;
                    CMask mask = this.app.imageBank.getImageFromHandle(cbkd2.img).getMask(flags, 0, 1f, 1f);
                    if (mask != null && mask.testPoint(x - crect.left, y - crect.top))
                      return true;
                  }
                  if (num19 != 0U)
                  {
                    --index3;
                    continue;
                  }
                  continue;
              }
            }
          }
        }
        return false;
      }

      public bool bkdLevObjCol_TestRect(
        int x,
        int y,
        int nWidth,
        int nHeight,
        int nTestLayer,
        int nPlane)
      {
        CRect crect = new CRect();
        int num1;
        int num2;
        if (nTestLayer == -1)
        {
          num1 = 1;
          num2 = this.nLayers - 1;
        }
        else
        {
          if (nTestLayer >= this.nLayers)
            return false;
          num1 = num2 = nTestLayer;
        }
        int leWidth = this.leWidth;
        int leHeight = this.leHeight;
        for (int index1 = num1; index1 <= num2; ++index1)
        {
          CLayer layer = this.layers[index1];
          bool flag1 = (layer.dwOptions & 32 /*0x20*/) != 0;
          bool flag2 = (layer.dwOptions & 64 /*0x40*/) != 0;
          bool flag3 = flag1 | flag2;
          int num3 = this.leX;
          int num4 = this.leY;
          if ((layer.dwOptions & 3) != 0)
          {
            if ((layer.dwOptions & 1) != 0)
              num3 = (int) ((double) num3 * (double) layer.xCoef);
            if ((layer.dwOptions & 2) != 0)
              num4 = (int) ((double) num4 * (double) layer.yCoef);
          }
          int num5 = num3 + layer.x;
          int num6 = num4 + layer.y;
          if (flag1)
            num5 %= leWidth;
          if (flag2)
            num6 %= leHeight;
          uint num7 = 0;
          int num8 = 0;
          int nBkdLos = layer.nBkdLOs;
          for (int index2 = 0; index2 < nBkdLos; ++index2)
          {
            CLO loFromIndex = this.LOList.getLOFromIndex((short) (layer.nFirstLOIndex + index2));
            CObject cobject = (CObject) null;
            COI oiFromHandle = this.app.OIList.getOIFromHandle(loFromIndex.loOiHandle);
            if (oiFromHandle != null && oiFromHandle.oiOC != null)
            {
              COC oiOc = oiFromHandle.oiOC;
              int oiType = (int) oiFromHandle.oiType;
              crect.left = loFromIndex.loX - num5;
              crect.top = loFromIndex.loY - num6;
              int num9;
              int num10;
              if (oiType < 2)
              {
                num9 = (int) oiOc.ocObstacleType;
                switch (num9)
                {
                  case 0:
                  case 3:
                  case 4:
                    continue;
                  default:
                    num10 = (int) oiOc.ocColMode;
                    crect.right = crect.left + oiOc.ocCx;
                    crect.bottom = crect.top + oiOc.ocCy;
                    break;
                }
              }
              else
              {
                CObjectCommon cobjectCommon = (CObjectCommon) oiOc;
                if ((cobjectCommon.ocOEFlags & 2) != 0 && (cobject = this.rhPtr.find_HeaderObject(loFromIndex.loHandle)) != null)
                {
                  num9 = ((int) cobjectCommon.ocFlags2 & 48 /*0x30*/) >> 4;
                  switch (num9)
                  {
                    case 0:
                    case 3:
                    case 4:
                      continue;
                    default:
                      num10 = ((int) cobjectCommon.ocFlags2 & 4) != 0 ? 1 : 0;
                      crect.left = cobject.hoX - this.leX - cobject.hoImgXSpot;
                      crect.top = cobject.hoY - this.leY - cobject.hoImgYSpot;
                      crect.right = crect.left + cobject.hoImgWidth;
                      crect.bottom = crect.top + cobject.hoImgHeight;
                      break;
                  }
                }
                else
                  continue;
              }
              if (flag3)
              {
                switch (num8)
                {
                  case 0:
                    if (flag1 && (crect.left < 0 || crect.right > leWidth))
                    {
                      if (flag2 && (crect.top < 0 || crect.bottom > leHeight))
                      {
                        num8 = 3;
                        num7 |= 7U;
                        break;
                      }
                      num8 = 1;
                      num7 |= 1U;
                      break;
                    }
                    if (flag2 && (crect.top < 0 || crect.bottom > leHeight))
                    {
                      num8 = 2;
                      num7 |= 2U;
                      break;
                    }
                    break;
                  case 1:
                    if (crect.left < 0)
                    {
                      int num11 = leWidth;
                      crect.left += num11;
                      crect.right += num11;
                    }
                    else if (crect.right > leWidth)
                    {
                      int num12 = leWidth;
                      crect.left -= num12;
                      crect.right -= num12;
                    }
                    num7 &= 4294967294U;
                    num8 = 0;
                    if (((int) num7 & 2) != 0)
                    {
                      num8 = 2;
                      break;
                    }
                    break;
                  case 2:
                    if (crect.top < 0)
                    {
                      int num13 = leHeight;
                      crect.top += num13;
                      crect.bottom += num13;
                    }
                    else if (crect.bottom > leHeight)
                    {
                      int num14 = leHeight;
                      crect.top -= num14;
                      crect.bottom -= num14;
                    }
                    num7 &= 4294967293U;
                    num8 = 0;
                    if (((int) num7 & 1) != 0)
                    {
                      num8 = 1;
                      break;
                    }
                    break;
                  case 3:
                    if (crect.left < 0)
                    {
                      int num15 = leWidth;
                      crect.left += num15;
                      crect.right += num15;
                    }
                    else if (crect.right > leWidth)
                    {
                      int num16 = leWidth;
                      crect.left -= num16;
                      crect.right -= num16;
                    }
                    if (crect.top < 0)
                    {
                      int num17 = leHeight;
                      crect.top += num17;
                      crect.bottom += num17;
                    }
                    else if (crect.bottom > leHeight)
                    {
                      int num18 = leHeight;
                      crect.top -= num18;
                      crect.bottom -= num18;
                    }
                    num7 &= 4294967291U;
                    num8 = 2;
                    break;
                }
              }
              if (x + nWidth > crect.left && y + nHeight > crect.top && x < crect.right && y < crect.bottom && (num9 != 2 || nPlane != 0))
              {
                if (num10 != 0)
                  return true;
                int flags = 0;
                if (num9 == 2)
                  flags = 1;
                CMask cmask = oiType >= 2 ? cobject.getCollisionMask(flags) : this.app.imageBank.getImageFromHandle(((COCBackground) oiOc).ocImage).getMask(flags, 0, 1f, 1f);
                if (cmask == null || cmask.testRect(0, x - crect.left, y - crect.top, nWidth, nHeight))
                  return true;
              }
              if (num7 != 0U)
                --index2;
            }
          }
          if (layer.pBkd2 != null)
          {
            uint num19 = 0;
            int num20 = 0;
            for (int index3 = 0; index3 < layer.pBkd2.size(); ++index3)
            {
              CBkd2 cbkd2 = (CBkd2) layer.pBkd2.get(index3);
              crect.left = cbkd2.x - num5;
              crect.top = cbkd2.y - num6;
              int obstacleType = (int) cbkd2.obstacleType;
              switch (obstacleType)
              {
                case 0:
                case 3:
                case 4:
                  continue;
                default:
                  int num21 = cbkd2.colMode == (short) 0 ? 1 : 0;
                  CImage imageFromHandle = this.app.imageBank.getImageFromHandle(cbkd2.img);
                  if (imageFromHandle != null)
                  {
                    crect.right = crect.left + (int) imageFromHandle.width;
                    crect.bottom = crect.top + (int) imageFromHandle.height;
                  }
                  else
                  {
                    crect.right = crect.left + 1;
                    crect.bottom = crect.top + 1;
                  }
                  if (flag3)
                  {
                    switch (num20)
                    {
                      case 0:
                        if (flag1 && (crect.left < 0 || crect.right > leWidth))
                        {
                          if (flag2 && (crect.top < 0 || crect.bottom > leHeight))
                          {
                            num20 = 3;
                            num19 |= 7U;
                            break;
                          }
                          num20 = 1;
                          num19 |= 1U;
                          break;
                        }
                        if (flag2 && (crect.top < 0 || crect.bottom > leHeight))
                        {
                          num20 = 2;
                          num19 |= 2U;
                          break;
                        }
                        break;
                      case 1:
                        if (crect.left < 0)
                        {
                          int num22 = leWidth;
                          crect.left += num22;
                          crect.right += num22;
                        }
                        else if (crect.right > leWidth)
                        {
                          int num23 = leWidth;
                          crect.left -= num23;
                          crect.right -= num23;
                        }
                        num19 &= 4294967294U;
                        num20 = 0;
                        if (((int) num19 & 2) != 0)
                        {
                          num20 = 2;
                          break;
                        }
                        break;
                      case 2:
                        if (crect.top < 0)
                        {
                          int num24 = leHeight;
                          crect.top += num24;
                          crect.bottom += num24;
                        }
                        else if (crect.bottom > leHeight)
                        {
                          int num25 = leHeight;
                          crect.top -= num25;
                          crect.bottom -= num25;
                        }
                        num19 &= 4294967293U;
                        num20 = 0;
                        if (((int) num19 & 1) != 0)
                        {
                          num20 = 1;
                          break;
                        }
                        break;
                      case 3:
                        if (crect.left < 0)
                        {
                          int num26 = leWidth;
                          crect.left += num26;
                          crect.right += num26;
                        }
                        else if (crect.right > leWidth)
                        {
                          int num27 = leWidth;
                          crect.left -= num27;
                          crect.right -= num27;
                        }
                        if (crect.top < 0)
                        {
                          int num28 = leHeight;
                          crect.top += num28;
                          crect.bottom += num28;
                        }
                        else if (crect.bottom > leHeight)
                        {
                          int num29 = leHeight;
                          crect.top -= num29;
                          crect.bottom -= num29;
                        }
                        num19 &= 4294967291U;
                        num20 = 2;
                        break;
                    }
                  }
                  if (x + nWidth > crect.left && y + nHeight > crect.top && x < crect.right && y < crect.bottom && (obstacleType != 2 || nPlane != 0))
                  {
                    if (num21 != 0)
                      return true;
                    int flags = 0;
                    if (obstacleType == 2)
                      flags = 1;
                    CMask mask = this.app.imageBank.getImageFromHandle(cbkd2.img).getMask(flags, 0, 1f, 1f);
                    if (mask != null && mask.testRect(0, x - crect.left, y - crect.top, nWidth, nHeight))
                      return true;
                  }
                  if (num19 != 0U)
                  {
                    --index3;
                    continue;
                  }
                  continue;
              }
            }
          }
        }
        return false;
      }

      public bool bkdLevObjCol_TestSprite(
        CSprite pSpr,
        short newImg,
        int newX,
        int newY,
        int newAngle,
        float newScaleX,
        float newScaleY,
        int subHt,
        int nPlane)
      {
        CRect crect1 = new CRect();
        CLayer layer = this.layers[(int) pSpr.sprLayer / 2];
        bool flag1 = (layer.dwOptions & 32 /*0x20*/) != 0;
        bool flag2 = (layer.dwOptions & 64 /*0x40*/) != 0;
        bool flag3 = flag1 | flag2;
        int leWidth = this.leWidth;
        int leHeight = this.leHeight;
        int num1 = this.leX;
        int num2 = this.leY;
        if ((layer.dwOptions & 3) != 0)
        {
          if ((layer.dwOptions & 1) != 0)
            num1 = (int) ((double) num1 * (double) layer.xCoef);
          if ((layer.dwOptions & 2) != 0)
            num2 = (int) ((double) num2 * (double) layer.yCoef);
        }
        int num3 = num1 + layer.x;
        int num4 = num2 + layer.y;
        if (flag1)
          num3 %= leWidth;
        if (flag2)
          num4 %= leHeight;
        uint sprFlags = pSpr.sprFlags;
        bool flag4 = ((int) sprFlags & 256 /*0x0100*/) != 0;
        CRect crect2 = new CRect();
        int num5 = (int) newImg;
        crect2.left = newX;
        crect2.top = newY;
        if (newImg == (short) 0)
          num5 = (int) pSpr.sprImg;
        CMask pMask2 = (CMask) null;
        CMask cmask1 = (CMask) null;
        int num6 = 0;
        int w;
        int h;
        if (!flag4)
        {
          pMask2 = this.app.spriteGen.getSpriteMask(pSpr, (short) num5, 0, 0, 1f, 1f);
          if (pMask2 == null)
          {
            crect2.left = pSpr.sprX1new;
            crect2.right = pSpr.sprX2new;
            crect2.top = pSpr.sprY1new;
            crect2.bottom = pSpr.sprY2new;
            w = crect2.right - crect2.left;
            h = crect2.bottom - crect2.top;
            flag4 = true;
          }
          else
          {
            if (((int) pSpr.sprFlags & 4194304 /*0x400000*/) == 0)
            {
              crect2.left -= pMask2.xSpot;
              crect2.top -= pMask2.ySpot;
            }
            w = pMask2.width;
            h = pMask2.height;
            crect2.right = crect2.left + w;
            crect2.bottom = crect2.top + h;
          }
        }
        else if (num5 == 0 || num5 == (int) pSpr.sprImg || ((int) sprFlags & 8192 /*0x2000*/) != 0)
        {
          crect2.left = pSpr.sprX1new;
          crect2.right = pSpr.sprX2new;
          crect2.top = pSpr.sprY1new;
          crect2.bottom = pSpr.sprY2new;
          w = crect2.right - crect2.left;
          h = crect2.bottom - crect2.top;
        }
        else
        {
          CImage imageFromHandle = this.app.imageBank.getImageFromHandle((short) num5);
          if (imageFromHandle != null)
          {
            crect2.left -= (int) imageFromHandle.xSpot;
            crect2.top -= (int) imageFromHandle.ySpot;
            w = (int) imageFromHandle.width;
            h = (int) imageFromHandle.height;
            crect2.right = crect2.left + w;
            crect2.bottom = crect2.top + h;
          }
          else
          {
            crect2.left = pSpr.sprX1new;
            crect2.right = pSpr.sprX2new;
            crect2.top = pSpr.sprY1new;
            crect2.bottom = pSpr.sprY2new;
            w = crect2.right - crect2.left;
            h = crect2.bottom - crect2.top;
          }
        }
        if (subHt != 0)
        {
          if (subHt > h)
            subHt = h;
          crect2.top += h - subHt;
          if (pMask2 != null)
            num6 = h - subHt;
          h = subHt;
        }
        uint num7 = 0;
        int num8 = 0;
        int nBkdLos = layer.nBkdLOs;
        for (int index = 0; index < nBkdLos; ++index)
        {
          CLO loFromIndex = this.LOList.getLOFromIndex((short) (layer.nFirstLOIndex + index));
          COI oiFromHandle = this.app.OIList.getOIFromHandle(loFromIndex.loOiHandle);
          if (oiFromHandle != null && oiFromHandle.oiOC != null)
          {
            COC oiOc = oiFromHandle.oiOC;
            int oiType = (int) oiFromHandle.oiType;
            crect1.left = loFromIndex.loX - num3;
            crect1.top = loFromIndex.loY - num4;
            CObject cobject = (CObject) null;
            int num9;
            int num10;
            if (oiType < 2)
            {
              num9 = (int) oiOc.ocObstacleType;
              switch (num9)
              {
                case 0:
                case 3:
                case 4:
                  continue;
                default:
                  num10 = (int) oiOc.ocColMode;
                  crect1.right = crect1.left + oiOc.ocCx;
                  crect1.bottom = crect1.top + oiOc.ocCy;
                  break;
              }
            }
            else
            {
              CObjectCommon cobjectCommon = (CObjectCommon) oiOc;
              if ((cobjectCommon.ocOEFlags & 2) != 0 && (cobject = this.rhPtr.find_HeaderObject(loFromIndex.loHandle)) != null)
              {
                num9 = ((int) cobjectCommon.ocFlags2 & 48 /*0x30*/) >> 4;
                switch (num9)
                {
                  case 0:
                  case 3:
                  case 4:
                    continue;
                  default:
                    num10 = ((int) cobjectCommon.ocFlags2 & 4) != 0 ? 1 : 0;
                    crect1.left = cobject.hoX - this.leX - cobject.hoImgXSpot;
                    crect1.top = cobject.hoY - this.leY - cobject.hoImgYSpot;
                    crect1.right = crect1.left + cobject.hoImgWidth;
                    crect1.bottom = crect1.top + cobject.hoImgHeight;
                    break;
                }
              }
              else
                continue;
            }
            if (flag3)
            {
              switch (num8)
              {
                case 0:
                  if (flag1 && (crect1.left < 0 || crect1.right > leWidth))
                  {
                    if (flag2 && (crect1.top < 0 || crect1.bottom > leHeight))
                    {
                      num8 = 3;
                      num7 |= 7U;
                      break;
                    }
                    num8 = 1;
                    num7 |= 1U;
                    break;
                  }
                  if (flag2 && (crect1.top < 0 || crect1.bottom > leHeight))
                  {
                    num8 = 2;
                    num7 |= 2U;
                    break;
                  }
                  break;
                case 1:
                  if (crect1.left < 0)
                  {
                    int num11 = leWidth;
                    crect1.left += num11;
                    crect1.right += num11;
                  }
                  else if (crect1.right > leWidth)
                  {
                    int num12 = leWidth;
                    crect1.left -= num12;
                    crect1.right -= num12;
                  }
                  num7 &= 4294967294U;
                  num8 = 0;
                  if (((int) num7 & 2) != 0)
                  {
                    num8 = 2;
                    break;
                  }
                  break;
                case 2:
                  if (crect1.top < 0)
                  {
                    int num13 = leHeight;
                    crect1.top += num13;
                    crect1.bottom += num13;
                  }
                  else if (crect1.bottom > leHeight)
                  {
                    int num14 = leHeight;
                    crect1.top -= num14;
                    crect1.bottom -= num14;
                  }
                  num7 &= 4294967293U;
                  num8 = 0;
                  if (((int) num7 & 1) != 0)
                  {
                    num8 = 1;
                    break;
                  }
                  break;
                case 3:
                  if (crect1.left < 0)
                  {
                    int num15 = leWidth;
                    crect1.left += num15;
                    crect1.right += num15;
                  }
                  else if (crect1.right > leWidth)
                  {
                    int num16 = leWidth;
                    crect1.left -= num16;
                    crect1.right -= num16;
                  }
                  if (crect1.top < 0)
                  {
                    int num17 = leHeight;
                    crect1.top += num17;
                    crect1.bottom += num17;
                  }
                  else if (crect1.bottom > leHeight)
                  {
                    int num18 = leHeight;
                    crect1.top -= num18;
                    crect1.bottom -= num18;
                  }
                  num7 &= 4294967291U;
                  num8 = 2;
                  break;
              }
            }
            if (crect2.right > crect1.left && crect2.bottom > crect1.top && crect2.left < crect1.right && crect2.top < crect1.bottom && (num9 != 2 || nPlane != 0))
            {
              if (num10 != 0)
              {
                if (flag4)
                  return true;
                if (pMask2 == null)
                {
                  if (pMask2 == null)
                    return true;
                  num6 = 0;
                  if (subHt != 0)
                  {
                    if (subHt > h)
                      subHt = h;
                    num6 = h - subHt;
                  }
                }
                if (pMask2.testRect(num6, crect1.left - crect2.left, crect1.top - crect2.top, crect1.right - crect1.left, crect1.bottom - crect1.top))
                  return true;
              }
              else
              {
                int flags = 0;
                if (num9 == 2)
                  flags = 1;
                cmask1 = (CMask) null;
                CMask cmask2 = oiType >= 2 ? cobject.getCollisionMask(flags) : this.app.imageBank.getImageFromHandle(((COCBackground) oiOc).ocImage).getMask(flags, 0, 1f, 1f);
                if (flag4)
                {
                  if (cmask2 == null || cmask2.testRect(0, crect2.left - crect1.left, crect2.top - crect1.top, w, h))
                    return true;
                }
                else
                {
                  num6 = 0;
                  if (subHt != 0)
                  {
                    if (subHt > h)
                      subHt = h;
                    num6 = h - subHt;
                  }
                  if (cmask2 == null)
                  {
                    if (pMask2.testRect(num6, crect1.left - crect2.left, crect1.top - crect2.top, crect1.right - crect1.left, crect1.bottom - crect1.top))
                      return true;
                  }
                  else if (pMask2 == null)
                  {
                    if (cmask2.testRect(0, crect2.left - crect1.left, crect2.top - crect1.top, w, h))
                      return true;
                  }
                  else if (cmask2.testMask(0, crect1.left, crect1.top, pMask2, num6, crect2.left, crect2.top))
                    return true;
                }
              }
            }
            if (num7 != 0U)
              --index;
          }
        }
        if (layer.pBkd2 != null)
        {
          uint num19 = 0;
          int num20 = 0;
          for (int index = 0; index < layer.pBkd2.size(); ++index)
          {
            CBkd2 cbkd2 = (CBkd2) layer.pBkd2.get(index);
            crect1.left = cbkd2.x - num3;
            crect1.top = cbkd2.y - num4;
            int obstacleType = (int) cbkd2.obstacleType;
            switch (obstacleType)
            {
              case 0:
              case 3:
              case 4:
                continue;
              default:
                int num21 = cbkd2.colMode == (short) 0 ? 1 : 0;
                CImage imageFromHandle = this.app.imageBank.getImageFromHandle(cbkd2.img);
                if (imageFromHandle != null)
                {
                  crect1.right = crect1.left + (int) imageFromHandle.width;
                  crect1.bottom = crect1.top + (int) imageFromHandle.height;
                }
                else
                {
                  crect1.right = crect1.left + 1;
                  crect1.bottom = crect1.top + 1;
                }
                if (flag3)
                {
                  switch (num20)
                  {
                    case 0:
                      if (flag1 && (crect1.left < 0 || crect1.right > leWidth))
                      {
                        if (flag2 && (crect1.top < 0 || crect1.bottom > leHeight))
                        {
                          num20 = 3;
                          num19 |= 7U;
                          break;
                        }
                        num20 = 1;
                        num19 |= 1U;
                        break;
                      }
                      if (flag2 && (crect1.top < 0 || crect1.bottom > leHeight))
                      {
                        num20 = 2;
                        num19 |= 2U;
                        break;
                      }
                      break;
                    case 1:
                      if (crect1.left < 0)
                      {
                        int num22 = leWidth;
                        crect1.left += num22;
                        crect1.right += num22;
                      }
                      else if (crect1.right > leWidth)
                      {
                        int num23 = leWidth;
                        crect1.left -= num23;
                        crect1.right -= num23;
                      }
                      num19 &= 4294967294U;
                      num20 = 0;
                      if (((int) num19 & 2) != 0)
                      {
                        num20 = 2;
                        break;
                      }
                      break;
                    case 2:
                      if (crect1.top < 0)
                      {
                        int num24 = leHeight;
                        crect1.top += num24;
                        crect1.bottom += num24;
                      }
                      else if (crect1.bottom > leHeight)
                      {
                        int num25 = leHeight;
                        crect1.top -= num25;
                        crect1.bottom -= num25;
                      }
                      num19 &= 4294967293U;
                      num20 = 0;
                      if (((int) num19 & 1) != 0)
                      {
                        num20 = 1;
                        break;
                      }
                      break;
                    case 3:
                      if (crect1.left < 0)
                      {
                        int num26 = leWidth;
                        crect1.left += num26;
                        crect1.right += num26;
                      }
                      else if (crect1.right > leWidth)
                      {
                        int num27 = leWidth;
                        crect1.left -= num27;
                        crect1.right -= num27;
                      }
                      if (crect1.top < 0)
                      {
                        int num28 = leHeight;
                        crect1.top += num28;
                        crect1.bottom += num28;
                      }
                      else if (crect1.bottom > leHeight)
                      {
                        int num29 = leHeight;
                        crect1.top -= num29;
                        crect1.bottom -= num29;
                      }
                      num19 &= 4294967291U;
                      num20 = 2;
                      break;
                  }
                }
                if (crect2.right > crect1.left && crect2.bottom > crect1.top && crect2.left < crect1.right && crect2.top < crect1.bottom && (obstacleType != 2 || nPlane != 0))
                {
                  if (num21 != 0)
                  {
                    if (flag4)
                      return true;
                    cmask1 = this.app.imageBank.getImageFromHandle(cbkd2.img).getMask(0, 0, 1f, 1f);
                    if (pMask2 == null)
                      return true;
                    int yBase1 = 0;
                    if (subHt != 0)
                    {
                      if (subHt > h)
                        subHt = h;
                      yBase1 = h - subHt;
                    }
                    if (pMask2.testRect(yBase1, crect1.left - crect2.left, crect1.top - crect2.top, crect1.right - crect1.left, crect1.bottom - crect1.top))
                      return true;
                  }
                  else
                  {
                    int flags = 0;
                    if (obstacleType == 2)
                      flags = 1;
                    CMask mask = this.app.imageBank.getImageFromHandle(cbkd2.img).getMask(flags, 0, 1f, 1f);
                    if (mask != null)
                    {
                      if (flag4)
                      {
                        if (mask.testRect(0, crect2.left - crect1.left, crect2.top - crect1.top, w, h))
                          return true;
                      }
                      else
                      {
                        if (pMask2 == null)
                          return true;
                        int yBase2 = 0;
                        if (subHt != 0)
                        {
                          if (subHt > h)
                            subHt = h;
                          yBase2 = h - subHt;
                        }
                        if (mask.testMask(0, crect1.left, crect1.top, pMask2, yBase2, crect2.left, crect2.top))
                          return true;
                      }
                    }
                  }
                }
                if (num19 != 0U)
                {
                  --index;
                  continue;
                }
                continue;
            }
          }
        }
        return false;
      }

      public bool bkdCol_TestPoint(int x, int y, int nLayer, int nPlane)
      {
        switch (nLayer)
        {
          case -1:
            CLayer layer1 = this.layers[0];
            if ((this.leFlags & 32 /*0x20*/) != 0 && (layer1.dwOptions & 96 /*0x60*/) != 0)
              return this.bkdLevObjCol_TestPoint(x, y, 0, nPlane);
            if (this.colMask != null && this.colMask.testPoint(x, y, nPlane))
              return true;
            if (this.nLayers == 1)
              return false;
            if ((this.leFlags & 32 /*0x20*/) != 0)
              return this.bkdLevObjCol_TestPoint(x, y, nLayer, nPlane);
            int num1 = 8;
            int dwFlags1 = nPlane != 1 ? num1 | 1 : num1 | 2;
            return this.app.spriteGen.spriteCol_TestPoint((CSprite) null, (short) nLayer, x, y, dwFlags1) != null;
          case 0:
            CLayer layer2 = this.layers[0];
            if ((this.leFlags & 32 /*0x20*/) == 0 || (layer2.dwOptions & 96 /*0x60*/) == 0)
              return this.colMask.testPoint(x, y, nPlane);
            return this.bkdLevObjCol_TestPoint(x, y, 0, nPlane);
          default:
            if (this.nLayers == 1)
              return false;
            if ((this.leFlags & 32 /*0x20*/) != 0)
              return this.bkdLevObjCol_TestPoint(x, y, nLayer, nPlane);
            int num2 = 8;
            int dwFlags2 = nPlane != 1 ? num2 | 1 : num2 | 2;
            return this.app.spriteGen.spriteCol_TestPoint((CSprite) null, (short) -1, x, y, dwFlags2) != null;
        }
      }

      public bool bkdCol_TestRect(int x, int y, int nWidth, int nHeight, int nLayer, int nPlane)
      {
        switch (nLayer)
        {
          case -1:
            CLayer layer1 = this.layers[0];
            if ((this.leFlags & 32 /*0x20*/) != 0 && (layer1.dwOptions & 96 /*0x60*/) != 0)
              return this.bkdLevObjCol_TestRect(x, y, nWidth, nHeight, 0, nPlane);
            if (this.colMask.testRect(x, y, nWidth, nHeight, nPlane))
              return true;
            if (this.nLayers == 1)
              return false;
            if ((this.leFlags & 32 /*0x20*/) != 0)
              return this.bkdLevObjCol_TestRect(x, y, nWidth, nHeight, nLayer, nPlane);
            int num1 = 8;
            int dwFlags1 = nPlane != 1 ? num1 | 1 : num1 | 2;
            return this.app.spriteGen.spriteCol_TestRect((CSprite) null, nLayer, x, y, nWidth, nHeight, dwFlags1) != null;
          case 0:
            CLayer layer2 = this.layers[0];
            return (this.leFlags & 32 /*0x20*/) != 0 && (layer2.dwOptions & 96 /*0x60*/) != 0 ? this.bkdLevObjCol_TestRect(x, y, nWidth, nHeight, 0, nPlane) : this.colMask.testRect(x, y, nWidth, nHeight, nPlane);
          default:
            if (this.nLayers == 1)
              return false;
            if ((this.leFlags & 32 /*0x20*/) != 0)
              return this.bkdLevObjCol_TestRect(x, y, nWidth, nHeight, nLayer, nPlane);
            int num2 = 8;
            int dwFlags2 = nPlane != 1 ? num2 | 1 : num2 | 2;
            return this.app.spriteGen.spriteCol_TestRect((CSprite) null, -1, x, y, nWidth, nHeight, dwFlags2) != null;
        }
      }

      public bool bkdCol_TestSprite(
        CSprite pSpr,
        int newImg,
        int newX,
        int newY,
        int newAngle,
        float newScaleX,
        float newScaleY,
        int subHt,
        int nPlane)
      {
        if ((int) pSpr.sprLayer / 2 == 0)
        {
          CLayer layer = this.layers[0];
          return (this.leFlags & 32 /*0x20*/) != 0 && (layer.dwOptions & 96 /*0x60*/) != 0 ? this.bkdLevObjCol_TestSprite(pSpr, (short) newImg, newX, newY, newAngle, newScaleX, newScaleY, subHt, nPlane) : this.colMask_TestSprite(pSpr, newImg, newX, newY, newAngle, newScaleX, newScaleY, subHt, nPlane);
        }
        if (this.nLayers == 1)
          return false;
        if ((this.leFlags & 32 /*0x20*/) != 0)
          return this.bkdLevObjCol_TestSprite(pSpr, (short) newImg, newX, newY, newAngle, newScaleX, newScaleY, subHt, nPlane);
        uint num = 8;
        uint dwFlags = nPlane != 1 ? num | 1U : num | 2U;
        return this.app.spriteGen.spriteCol_TestSprite(pSpr, (short) newImg, newX, newY, newAngle, newScaleX, newScaleY, subHt, dwFlags) != null;
      }

      public bool colMask_TestSprite(
        CSprite pSpr,
        int newImg,
        int newX,
        int newY,
        int newAngle,
        float newScaleX,
        float newScaleY,
        int subHt,
        int nPlane)
      {
        if (pSpr == null || this.colMask == null)
          return false;
        int num1 = newImg;
        int num2 = newX;
        int num3 = newY;
        int colMode = (int) this.app.spriteGen.colMode;
        CRect crect = new CRect();
        if (newImg == 0)
          num1 = (int) pSpr.sprImg;
        int w;
        int h;
        if (colMode != 0 && ((int) pSpr.sprFlags & 256 /*0x0100*/) == 0)
        {
          CMask spriteMask = this.app.spriteGen.getSpriteMask(pSpr, (short) num1, 0, newAngle, newScaleX, newScaleY);
          if (spriteMask == null)
          {
            num2 -= pSpr.sprX - pSpr.sprX1;
            num3 -= pSpr.sprY - pSpr.sprY1;
            w = pSpr.sprX2 - pSpr.sprX1;
            h = pSpr.sprY2 - pSpr.sprY1;
          }
          else
          {
            if (((int) pSpr.sprFlags & 4194304 /*0x400000*/) == 0)
            {
              num2 -= spriteMask.xSpot;
              num3 -= spriteMask.ySpot;
            }
            w = spriteMask.width;
            h = spriteMask.height;
          }
          if (spriteMask != null)
          {
            int yBase = 0;
            if (subHt != 0)
            {
              if (subHt > h)
                subHt = h;
              num3 += h - subHt;
              yBase = h - subHt;
            }
            return this.colMask.testMask(spriteMask, yBase, num2, num3, nPlane);
          }
        }
        else if (num1 == 0 || num1 == (int) pSpr.sprImg || ((int) pSpr.sprFlags & 8192 /*0x2000*/) != 0)
        {
          num2 -= pSpr.sprX - pSpr.sprX1;
          num3 -= pSpr.sprY - pSpr.sprY1;
          w = pSpr.sprX2 - pSpr.sprX1;
          h = pSpr.sprY2 - pSpr.sprY1;
        }
        else
        {
          CImage imageFromHandle = this.app.imageBank.getImageFromHandle((short) num1);
          if (imageFromHandle != null)
          {
            num2 -= (int) imageFromHandle.xSpot;
            num3 -= (int) imageFromHandle.ySpot;
            w = (int) imageFromHandle.width;
            h = (int) imageFromHandle.height;
          }
          else
          {
            num2 -= pSpr.sprX - pSpr.sprX1;
            num3 -= pSpr.sprY - pSpr.sprY1;
            w = pSpr.sprX2 - pSpr.sprX1;
            h = pSpr.sprY2 - pSpr.sprY1;
          }
        }
        if (subHt != 0)
        {
          if (subHt > h)
            subHt = h;
          num3 += h - subHt;
          h = subHt;
        }
        return this.colMask.testRect(num2, num3, w, h, nPlane);
      }
    }
}
