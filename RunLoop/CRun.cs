// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.RunLoop.CRun
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using RuntimeXNA.Animations;
using RuntimeXNA.Application;
using RuntimeXNA.Banks;
using RuntimeXNA.Events;
using RuntimeXNA.Expressions;
using RuntimeXNA.Frame;
using RuntimeXNA.Movements;
using RuntimeXNA.Objects;
using RuntimeXNA.OI;
using RuntimeXNA.Params;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;
using RuntimeXNA.Values;
using System;

namespace RuntimeXNA.RunLoop
{

    public class CRun
    {
      public const short GAMEFLAGS_VBLINDEP = 2;
      public const short GAMEFLAGS_LIMITEDSCROLL = 4;
      public const short GAMEFLAGS_FIRSTLOOPFADEIN = 16 /*0x10*/;
      public const short GAMEFLAGS_LOADONCALL = 32 /*0x20*/;
      public const short GAMEFLAGS_REALGAME = 64 /*0x40*/;
      public const short GAMEFLAGS_PLAY = 128 /*0x80*/;
      public const short GAMEFLAGS_INITIALISING = 512 /*0x0200*/;
      public const short DLF_DONTUPDATE = 2;
      public const short DLF_DRAWOBJECTS = 4;
      public const short DLF_RESTARTLEVEL = 8;
      public const short DLF_DONTUPDATECOLMASK = 16 /*0x10*/;
      public const short DLF_COLMASKCLIPPED = 32 /*0x20*/;
      public const short DLF_SKIPLAYER0 = 64 /*0x40*/;
      public const short DLF_REDRAWLAYER = 128 /*0x80*/;
      public const short DLF_STARTLEVEL = 256 /*0x0100*/;
      public const short GAME_XBORDER = 480;
      public const short GAME_YBORDER = 300;
      public const short COLMASK_XMARGIN = 64 /*0x40*/;
      public const short COLMASK_YMARGIN = 16 /*0x10*/;
      public const uint WRAP_X = 1;
      public const uint WRAP_Y = 2;
      public const uint WRAP_XY = 4;
      public const int RH3SCROLLING_SCROLL = 1;
      public const int RH3SCROLLING_REDRAWLAYERS = 2;
      public const int RH3SCROLLING_REDRAWALL = 4;
      public const int RH3SCROLLING_REDRAWTOTALCOLMASK = 8;
      public const int OBSTACLE_NONE = 0;
      public const int OBSTACLE_SOLID = 1;
      public const int OBSTACLE_PLATFORM = 2;
      public const int OBSTACLE_LADDER = 3;
      public const int OBSTACLE_TRANSPARENT = 4;
      public const short COF_NOMOVEMENT = 1;
      public const short COF_HIDDEN = 2;
      public const short COF_FIRSTTEXT = 4;
      public const short MAX_FRAMERATE = 10;
      public const short LOOPEXIT_NEXTLEVEL = 1;
      public const short LOOPEXIT_PREVLEVEL = 2;
      public const short LOOPEXIT_GOTOLEVEL = 3;
      public const short LOOPEXIT_NEWGAME = 4;
      public const short LOOPEXIT_PAUSEGAME = 5;
      public const short LOOPEXIT_SAVEAPPLICATION = 6;
      public const short LOOPEXIT_LOADAPPLICATION = 7;
      public const short LOOPEXIT_SAVEFRAME = 8;
      public const short LOOPEXIT_LOADFRAME = 9;
      public const short LOOPEXIT_ENDGAME = -2;
      public const short LOOPEXIT_QUIT = 100;
      public const short LOOPEXIT_RESTART = 101;
      public const short LOOPEXIT_APPLETPAUSE = 102;
      public const short BORDER_LEFT = 1;
      public const short BORDER_RIGHT = 2;
      public const short BORDER_TOP = 4;
      public const short BORDER_BOTTOM = 8;
      public const short BORDER_ALL = 15;
      public const int MAX_INTERMEDIATERESULTS = 128 /*0x80*/;
      public byte[] plMasks = new byte[20]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        byte.MaxValue,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        byte.MaxValue,
        byte.MaxValue,
        (byte) 0,
        (byte) 0,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        (byte) 0,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue
      };
      private short[] Table_InOut = new short[16 /*0x10*/]
      {
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 4,
        (short) 5,
        (short) 6,
        (short) 0,
        (short) 8,
        (short) 9,
        (short) 10,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 0
      };
      public static bool bMoveChanged;
      public CRunApp rhApp;
      public CRunFrame rhFrame;
      public int rhMaxOI;
      public byte rhStopFlag;
      public byte rhEvFlag;
      public int rhNPlayers;
      public byte rhMouseUsed;
      public short rhGameFlags;
      public byte[] rhPlayer = new byte[4];
      public short rhQuit;
      public short rhQuitBis;
      public int rhReturn;
      public int rhQuitParam;
      public int rhNObjects;
      public int rhMaxObjects;
      public CObjInfo[] rhOiList;
      public CEventProgram rhEvtProg;
      public int rhLevelSx;
      public int rhLevelSy;
      public int rhWindowX;
      public int rhWindowY;
      public int rhVBLDeltaOld;
      public int rhVBLObjet;
      public int rhVBLOld;
      public short rhMT_VBLStep;
      public short rhMT_VBLCount;
      public int rhMT_MoveStep;
      public int rhLoopCount;
      public long rhTimer;
      public long rhTimerOld;
      public long rhTimerFPSOld;
      public int rhTimerDelta;
      public int rhOiListPtr;
      public short rhObListNext;
      public short rhDestroyPos;
      public byte[] rh2OldPlayer = new byte[4];
      public byte[] rh2NewPlayer = new byte[4];
      public byte[] rh2InputMask = new byte[4];
      public byte rh2MouseKeys;
      public short rh2CreationCount;
      public int rh2MouseX;
      public int rh2MouseY;
      public int oldMouseKey;
      public int mouseKey;
      public int toucheID;
      public long mouseKeyTime;
      public int rh2MouseSaveX;
      public int rh2MouseSaveY;
      public int rh2PauseState;
      public int rh2PauseCompteur;
      public int rh2PauseTimer;
      public int rh2PauseFPSTimer;
      public int rh2PauseVbl;
      public int rh3DisplayX;
      public int rh3DisplayY;
      public int rh3WindowSx;
      public int rh3WindowSy;
      public short rh3CollisionCount;
      public byte rh3Scrolling;
      public int rh3Panic;
      public int rh3XMinimum;
      public int rh3YMinimum;
      public int rh3XMaximum;
      public int rh3YMaximum;
      public int rh3XMinimumKill;
      public int rh3YMinimumKill;
      public int rh3XMaximumKill;
      public int rh3YMaximumKill;
      public short rh3Graine;
      public Keys rh4PauseKey;
      public bool bCheckResume;
      public string rh4CurrentFastLoop;
      public int rh4EndOfPause;
      public short rh4MouseWheelDelta;
      public int rh4OnMouseWheel;
      public CArrayList rh4FastLoops;
      public CValue rh4ExpValue1;
      public CValue rh4ExpValue2;
      public int rh4KpxReturn;
      public int rh4ObjectCurCreate;
      public int rh4ObjectAddCreate;
      public short rh4FakeKey;
      public byte rh4DoUpdate;
      public bool rh4MenuEaten;
      public int rh4OnCloseCount;
      public bool rh4CursorShown;
      public short rh4ScrMode;
      public int rh4VBLDelta;
      public int rh4LoopTheoric;
      public int rh4EventCount;
      public CArrayList rh4BackDrawRoutines;
      public short rh4LastQuickDisplay;
      public short rh4FirstQuickDisplay;
      public int rh4WindowDeltaX;
      public int rh4WindowDeltaY;
      public long rh4TimeOut;
      public int rh4MouseXCenter;
      public int rh4MouseYCenter;
      public int rh4PosPile;
      public CValue[] rh4Results;
      public CExp[] rh4Operators;
      public CExp rh4OpeNull;
      public int rh4CurToken;
      public CExp[] rh4Tokens;
      public int[] rh4FrameRateArray = new int[10];
      public int rh4FrameRatePos;
      public int rh4FrameRatePrevious;
      public int[] rhDestroyList;
      public int rh4SaveFrame;
      public int rh4SaveFrameCount;
      public double rh4MvtTimerCoef;
      public CObject[] rhObjectList;
      public bool bOperande;
      public KeyboardState keyboardState;
      public byte rhJoystickMask;
      public short[] isColArray = new short[2];
      public bool bAnyKeyDown;
      public CQuestion questionObjectOn;
      public int nSubApps;
      public int nControls;
      public CArrayList controls;
      public IControl currentControl;
      public bool bMouseControlled;
      public int mouseX;
      public int mouseY;
      public PlayerIndex deviceSelectorPlayer;
      public ITouches touches;
      public CJoystick joystick;
      public CJoystickAcc joystickAcc;
      public int joystickAccCount;
      public int[] cancelledTouches;

      public CRun()
      {
      }

      public CRun(CRunApp app) => this.rhApp = app;

      public void setFrame(CRunFrame f) => this.rhFrame = f;

      public int allocRunHeader()
      {
        this.rhObjectList = new CObject[(int) this.rhFrame.maxObjects];
        this.rhEvtProg = this.rhFrame.evtProg;
        this.rhMaxOI = 0;
        for (COI coi = this.rhApp.OIList.getFirstOI(); coi != null; coi = this.rhApp.OIList.getNextOI())
        {
          if (coi.oiType >= (short) 2)
            ++this.rhMaxOI;
        }
        this.rhOiList = new CObjInfo[this.rhMaxOI];
        for (int index = 0; index < this.rhMaxOI; ++index)
          this.rhOiList[index] = (CObjInfo) null;
        this.rh3Graine = this.rhFrame.m_wRandomSeed != (short) -1 ? this.rhFrame.m_wRandomSeed : (short) new Random().Next(32000);
        this.rhApp.spriteGen.setData(this.rhApp.imageBank, this.rhApp, this.rhFrame);
        this.rhDestroyList = new int[(int) this.rhFrame.maxObjects / 32 /*0x20*/ + 1];
        this.rh4FastLoops = new CArrayList();
        this.rh4CurrentFastLoop = "";
        this.rhMaxObjects = (int) this.rhFrame.maxObjects;
        this.rhNPlayers = (int) this.rhEvtProg.nPlayers;
        this.rhWindowX = this.rhFrame.leX;
        this.rhWindowY = this.rhFrame.leY;
        this.rhLevelSx = this.rhFrame.leVirtualRect.right;
        if (this.rhLevelSx == -1)
          this.rhLevelSx = 2147479552;
        this.rhLevelSy = this.rhFrame.leVirtualRect.bottom;
        if (this.rhLevelSy == -1)
          this.rhLevelSy = 2147479552;
        this.rhNObjects = 0;
        this.rhStopFlag = (byte) 0;
        this.rhQuit = (short) 0;
        this.rhQuitBis = (short) 0;
        this.rhGameFlags &= (short) 128 /*0x80*/;
        this.rhGameFlags |= (short) 4;
        this.rh3Panic = 0;
        this.rh4FirstQuickDisplay = (short) -1;
        this.rh4LastQuickDisplay = (short) -1;
        this.rh4MouseXCenter = this.rhFrame.leEditWinWidth / 2;
        this.rh4MouseYCenter = this.rhFrame.leEditWinHeight / 2;
        this.rh4FrameRatePos = 0;
        this.rh4FrameRatePrevious = 0;
        this.rh4BackDrawRoutines = (CArrayList) null;
        this.rh4SaveFrame = 0;
        this.rh4SaveFrameCount = -3;
        this.nSubApps = 0;
        this.rhGameFlags |= (short) 64 /*0x40*/;
        this.rh4Results = new CValue[128 /*0x80*/];
        this.rh4Operators = new CExp[128 /*0x80*/];
        for (int index = 0; index < 128 /*0x80*/; ++index)
          this.rh4Results[index] = new CValue();
        this.rh4OpeNull = (CExp) new EXP_END();
        this.rh4OpeNull.code = 0;
        this.rhEvtProg.rh2CurrentClick = (short) -1;
        this.nControls = 0;
        this.currentControl = (IControl) null;
        this.controls = (CArrayList) null;
        this.bMouseControlled = true;
        this.mouseKey = -1;
        this.cancelledTouches = new int[this.rhApp.numberOfTouches];
        this.rhFrame.rhOK = true;
        return 0;
      }

      public void freeRunHeader()
      {
        this.rhFrame.rhOK = false;
        this.rhObjectList = (CObject[]) null;
        this.rhOiList = (CObjInfo[]) null;
        this.rhDestroyList = (int[]) null;
        this.rh4CurrentFastLoop = (string) null;
        this.rh4FastLoops = (CArrayList) null;
        this.rh4BackDrawRoutines = (CArrayList) null;
        for (int index = 0; index < 128 /*0x80*/; ++index)
          this.rh4Results[index] = (CValue) null;
        this.rh4OpeNull = (CExp) null;
      }

      public int initRunLoop()
      {
        int num1 = this.allocRunHeader();
        if (num1 != 0)
          return num1;
        this.initAsmLoop();
        this.y_InitLevel();
        int num2 = this.prepareFrame();
        if (num2 != 0)
          return num2;
        int frameObjects = this.createFrameObjects();
        if (frameObjects != 0)
          return frameObjects;
        this.redrawLevel(258);
        this.loadGlobalObjectsData();
        this.rhEvtProg.prepareProgram();
        this.rhEvtProg.assemblePrograms(this);
        this.captureMouse();
        this.rhQuitParam = 0;
        this.f_InitLoop();
        return 0;
      }

      public int doRunLoop()
      {
        this.rhApp.appRunFlags |= (short) 4;
        int num = this.f_GameLoop();
        this.rhApp.appRunFlags &= (short) -5;
        this.getTouches();
        this.getMouseCoords();
        if (this.mouseKey != this.oldMouseKey)
        {
          int nClicks = 1;
          if (this.mouseKey >= 0)
          {
            if (this.rhApp.timer - this.mouseKeyTime < 500L)
            {
              nClicks = 2;
              this.mouseKeyTime = 0L;
            }
            else
              this.mouseKeyTime = this.rhApp.timer;
            this.rhEvtProg.onMouseButton(this.mouseKey, nClicks);
            this.clickControls(nClicks);
          }
          this.oldMouseKey = this.mouseKey;
        }
        if (this.rhEvtProg.bTestAllKeys || this.rh2PauseCompteur > 0)
        {
          int index = 0;
          while (!this.keyboardState.IsKeyDown(CKeyConvert.xnaKeys[index]))
          {
            ++index;
            if (CKeyConvert.pcKeys[index] < 0)
              goto label_13;
          }
          if (!this.bAnyKeyDown)
          {
            this.bAnyKeyDown = true;
            this.rhEvtProg.onKeyDown(CKeyConvert.xnaKeys[index]);
          }
    label_13:
          if (CKeyConvert.pcKeys[index] < 0)
            this.bAnyKeyDown = false;
        }
        switch (num)
        {
          case -2:
          case 100:
            this.rhEvtProg.handle_GlobalEvents(-196611);
            break;
          case 101:
            if (!this.rhFrame.fade)
            {
              this.f_StopSamples();
              this.killFrameObjects();
              this.y_KillLevel(false);
              this.rhEvtProg.unBranchPrograms();
              this.freeMouse();
              this.freeRunHeader();
              this.rhFrame.leX = this.rhFrame.leLastScrlX = 0;
              this.rhFrame.leY = this.rhFrame.leLastScrlY = 0;
              if (this.rhFrame.colMask != null)
                this.rhFrame.colMask.setOrigin(0, 0);
              this.allocRunHeader();
              this.initAsmLoop();
              this.y_InitLevel();
              this.redrawLevel(10);
              this.prepareFrame();
              this.createFrameObjects();
              this.loadGlobalObjectsData();
              this.rhEvtProg.prepareProgram();
              this.rhEvtProg.assemblePrograms(this);
              this.f_InitLoop();
              this.captureMouse();
              num = 0;
              this.rhQuitParam = 0;
              break;
            }
            break;
          case 102:
            num = (int) this.rhQuit;
            break;
        }
        return num;
      }

      public int killRunLoop(int quit, bool bLeaveSamples)
      {
        if (quit > 100)
          quit = -2;
        int rhQuitParam = this.rhQuitParam;
        this.saveGlobalObjectsData();
        this.killFrameObjects();
        this.y_KillLevel(bLeaveSamples);
        this.rhEvtProg.unBranchPrograms();
        this.freeRunHeader();
        if (this.joystickAcc != null)
          this.stopJoystickAcc();
        return CServices.MAKELONG(quit, rhQuitParam);
      }

      public void y_InitLevel() => this.resetFrameLayers(-1, false);

      public void initAsmLoop()
      {
        this.rhApp.spriteGen.winSetColMode((short) 1);
        this.f_ObjMem_Init();
      }

      public void f_ObjMem_Init()
      {
        for (int index = 0; index < this.rhMaxObjects; ++index)
          this.rhObjectList[index] = (CObject) null;
      }

      public int prepareFrame()
      {
        if (((int) this.rhApp.gaFlags & 8) != 0 && !this.rhFrame.fade)
          this.rhGameFlags |= (short) 2;
        else
          this.rhGameFlags &= (short) -3;
        this.rhGameFlags |= (short) 32 /*0x20*/;
        this.rhGameFlags |= (short) 512 /*0x0200*/;
        this.rh2CreationCount = (short) 0;
        int index1 = 0;
        this.rhOiList = new CObjInfo[this.rhMaxOI];
        for (COI oiPtr = this.rhApp.OIList.getFirstOI(); oiPtr != null; oiPtr = this.rhApp.OIList.getNextOI())
        {
          short oiType = oiPtr.oiType;
          if (oiType >= (short) 2)
          {
            this.rhOiList[index1] = new CObjInfo();
            this.rhOiList[index1].copyData(oiPtr);
            this.rhOiList[index1].oilHFII = (short) -1;
            if (oiType == (short) 3 || oiType == (short) 4)
            {
              for (CLO clo = this.rhFrame.LOList.first_LevObj(); clo != null; clo = this.rhFrame.LOList.next_LevObj())
              {
                if ((int) clo.loOiHandle == (int) this.rhOiList[index1].oilOi)
                {
                  this.rhOiList[index1].oilHFII = clo.loHandle;
                  break;
                }
              }
            }
            ++index1;
            CObjectCommon oiOc = (CObjectCommon) oiPtr.oiOC;
            if ((oiOc.ocOEFlags & 16 /*0x10*/) != 0 && oiOc.ocMovements != null)
            {
              for (short index2 = 0; (int) index2 < oiOc.ocMovements.nMovements; ++index2)
              {
                CMoveDef move = oiOc.ocMovements.moveList[(int) index2];
                if (move.mvType == (short) 1)
                  this.rhMouseUsed |= (byte) (1 << (int) move.mvControl - 1);
              }
            }
          }
        }
        for (int index3 = 0; index3 < this.rhFrame.nLayers; ++index3)
          this.rhFrame.layers[index3].nZOrderMax = 1;
        return 0;
      }

      public int createFrameObjects()
      {
        int frameObjects = 0;
        int num = 0;
        for (CLO clo = this.rhFrame.LOList.first_LevObj(); clo != null; clo = this.rhFrame.LOList.next_LevObj())
        {
          COI oiFromHandle = this.rhApp.OIList.getOIFromHandle(clo.loOiHandle);
          CObjectCommon oiOc = (CObjectCommon) oiFromHandle.oiOC;
          short oiType = oiFromHandle.oiType;
          short flags = 0;
          if (clo.loParentType == (short) 0)
          {
            if (oiType == (short) 3)
              flags |= (short) 4;
            if (((int) oiOc.ocFlags2 & 8) == 0)
            {
              if (oiType != (short) 4)
                flags |= (short) 2;
              else
                goto label_9;
            }
            if ((oiOc.ocOEFlags & 131072 /*0x020000*/) == 0)
              this.f_CreateObject(clo.loHandle, clo.loOiHandle, int.MaxValue, int.MaxValue, -1, flags, -1, -1);
          }
    label_9:
          ++num;
        }
        this.rhGameFlags &= (short) -513;
        return frameObjects;
      }

      public void killFrameObjects()
      {
        for (short nObject = 0; (int) nObject < this.rhMaxObjects && this.rhNObjects != 0; ++nObject)
          this.f_KillObject((int) nObject, true);
        this.rh4FirstQuickDisplay = (short) -1;
      }

      public void y_KillLevel(bool bLeaveSamples)
      {
        this.resetFrameLayers(-1, false);
        if (bLeaveSamples)
          return;
        if (((int) this.rhApp.gaNewFlags & 1) == 0)
          this.rhApp.soundPlayer.stopAllSounds();
        else
          this.rhApp.soundPlayer.keepCurrentSounds();
      }

      public void resetFrameLayers(int nLayer, bool bDeleteFrame)
      {
        int num1;
        int num2;
        if (nLayer == -1)
        {
          num1 = 0;
          num2 = this.rhFrame.nLayers;
        }
        else
        {
          num1 = nLayer;
          num2 = nLayer + 1;
        }
        for (int index1 = 0; index1 < num2; ++index1)
        {
          CLayer layer = this.rhFrame.layers[index1];
          int nBkdLos = layer.nBkdLOs;
          for (int index2 = 0; index2 < nBkdLos; ++index2)
          {
            CLO loFromIndex = this.rhFrame.LOList.getLOFromIndex((short) (layer.nFirstLOIndex + index2));
            for (int index3 = 0; index3 < 4; ++index3)
            {
              if (loFromIndex.loSpr[index3] != null)
              {
                this.rhApp.spriteGen.delSpriteFast(loFromIndex.loSpr[index3]);
                loFromIndex.loSpr[index3] = (CSprite) null;
              }
            }
          }
          if (layer.pBkd2 != null)
          {
            for (int index4 = 0; index4 < layer.pBkd2.size(); ++index4)
            {
              CBkd2 cbkd2 = (CBkd2) layer.pBkd2.get(index4);
              for (int index5 = 0; index5 < 4; ++index5)
              {
                if (cbkd2.pSpr[index5] != null)
                {
                  this.rhApp.spriteGen.delSpriteFast(cbkd2.pSpr[index5]);
                  cbkd2.pSpr[index5] = (CSprite) null;
                }
              }
            }
          }
          layer.dwOptions = layer.backUp_dwOptions;
          layer.xCoef = layer.backUp_xCoef;
          layer.yCoef = layer.backUp_yCoef;
          layer.nBkdLOs = layer.backUp_nBkdLOs;
          layer.nFirstLOIndex = layer.backUp_nFirstLOIndex;
          layer.x = layer.y = layer.dx = layer.dy = 0;
          layer.pBkd2 = (CArrayList) null;
          layer.pLadders = (CArrayList) null;
        }
      }

      private void f_RemoveObjects()
      {
        int index1 = 0;
        for (int index2 = 0; index2 < this.rhNObjects; ++index2)
        {
          while (this.rhObjectList[index1] == null)
            ++index1;
          CObject rhObject = this.rhObjectList[index1];
          ++index1;
          if (rhObject.ros != null && rhObject.roc.rcSprite != null)
          {
            rhObject.ros.rsZOrder = rhObject.roc.rcSprite.sprZOrder;
            this.rhApp.spriteGen.delSpriteFast(rhObject.roc.rcSprite);
          }
          if ((rhObject.hoOEFlags & 4096 /*0x1000*/) != 0)
            this.remove_QuickDisplay(rhObject);
        }
      }

      public void captureMouse()
      {
      }

      public void freeMouse()
      {
      }

      public void showMouse()
      {
      }

      public void hideMouse()
      {
      }

      public void saveGlobalObjectsData()
      {
        for (int index1 = 0; index1 < this.rhOiList.Length; ++index1)
        {
          CObjInfo rhOi = this.rhOiList[index1];
          short index2 = rhOi.oilObject;
          if (rhOi.oilOi != short.MaxValue && ((int) index2 & 32768 /*0x8000*/) == 0 && ((int) this.rhApp.OIList.getOIFromHandle(rhOi.oilOi).oiFlags & 4) != 0)
          {
            CObject rhObject1 = this.rhObjectList[(int) index2];
            if (rhOi.oilType == (short) 3 || rhOi.oilType == (short) 7 || rhObject1.rov != null)
            {
              string str = $"{rhOi.oilName:s}::{rhOi.oilType:d}";
              if (this.rhApp.adGO == null)
                this.rhApp.adGO = new CArrayList();
              bool flag = false;
              CSaveGlobal o1 = (CSaveGlobal) null;
              for (int index3 = 0; index3 < this.rhApp.adGO.size(); ++index3)
              {
                o1 = (CSaveGlobal) this.rhApp.adGO.get(index3);
                if (str == o1.name)
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                o1 = new CSaveGlobal();
                o1.name = str;
                o1.objects = new CArrayList();
                this.rhApp.adGO.add((object) o1);
              }
              else
                o1.objects.clear();
              do
              {
                CObject rhObject2 = this.rhObjectList[(int) index2];
                if (rhOi.oilType == (short) 3)
                {
                  CText ctext = (CText) rhObject2;
                  o1.objects.add((object) new CSaveGlobalText()
                  {
                    text = ctext.rsTextBuffer,
                    rsMini = ctext.rsMini
                  });
                }
                else if (rhOi.oilType == (short) 7)
                {
                  CCounter ccounter = (CCounter) rhObject2;
                  o1.objects.add((object) new CSaveGlobalCounter()
                  {
                    value = new CValue(ccounter.rsValue),
                    rsMini = ccounter.rsMini,
                    rsMaxi = ccounter.rsMaxi,
                    rsMiniDouble = ccounter.rsMiniDouble,
                    rsMaxiDouble = ccounter.rsMaxiDouble
                  });
                }
                else
                {
                  CSaveGlobalValues o2 = new CSaveGlobalValues();
                  o2.flags = rhObject2.rov.rvValueFlags;
                  o2.values = new CValue[26];
                  for (int index4 = 0; index4 < 26; ++index4)
                  {
                    o2.values[index4] = (CValue) null;
                    if (rhObject2.rov.rvValues[index4] != null)
                      o2.values[index4] = new CValue(rhObject2.rov.rvValues[index4]);
                  }
                  o2.strings = new string[10];
                  for (int index5 = 0; index5 < 10; ++index5)
                  {
                    o2.strings[index5] = (string) null;
                    if (rhObject2.rov.rvStrings[index5] != null)
                      o2.strings[index5] = rhObject2.rov.rvStrings[index5];
                  }
                  o1.objects.add((object) o2);
                }
                index2 = rhObject2.hoNumNext;
              }
              while (((int) index2 & 32768 /*0x8000*/) == 0);
            }
          }
        }
      }

      public void loadGlobalObjectsData()
      {
        if (this.rhApp.adGO == null)
          return;
        for (int index1 = 0; index1 < this.rhOiList.Length; ++index1)
        {
          CObjInfo rhOi = this.rhOiList[index1];
          short index2 = rhOi.oilObject;
          if (rhOi.oilOi != short.MaxValue && ((int) index2 & 32768 /*0x8000*/) == 0 && ((int) this.rhApp.OIList.getOIFromHandle(rhOi.oilOi).oiFlags & 4) != 0)
          {
            string str = $"{rhOi.oilName:s}::{rhOi.oilType:d}";
            for (int index3 = 0; index3 < this.rhApp.adGO.size(); ++index3)
            {
              CSaveGlobal csaveGlobal = (CSaveGlobal) this.rhApp.adGO.get(index3);
              if (str == csaveGlobal.name)
              {
                int index4 = 0;
                do
                {
                  CObject rhObject = this.rhObjectList[(int) index2];
                  if (rhOi.oilType == (short) 3)
                  {
                    CSaveGlobalText csaveGlobalText = (CSaveGlobalText) csaveGlobal.objects.get(index4);
                    CText ctext = (CText) rhObject;
                    ctext.rsTextBuffer = csaveGlobalText.text;
                    ctext.rsMini = csaveGlobalText.rsMini;
                  }
                  else if (rhOi.oilType == (short) 7)
                  {
                    CSaveGlobalCounter csaveGlobalCounter = (CSaveGlobalCounter) csaveGlobal.objects.get(index4);
                    CCounter ccounter = (CCounter) rhObject;
                    ccounter.rsValue = new CValue(csaveGlobalCounter.value);
                    ccounter.rsMini = csaveGlobalCounter.rsMini;
                    ccounter.rsMaxi = csaveGlobalCounter.rsMaxi;
                    ccounter.rsMiniDouble = csaveGlobalCounter.rsMiniDouble;
                    ccounter.rsMaxiDouble = csaveGlobalCounter.rsMaxiDouble;
                  }
                  else
                  {
                    CSaveGlobalValues csaveGlobalValues = (CSaveGlobalValues) csaveGlobal.objects.get(index4);
                    rhObject.rov.rvValueFlags = csaveGlobalValues.flags;
                    for (int index5 = 0; index5 < 26; ++index5)
                    {
                      if (csaveGlobalValues.values[index5] != null)
                        rhObject.rov.rvValues[index5] = new CValue(csaveGlobalValues.values[index5]);
                    }
                    for (int index6 = 0; index6 < 10; ++index6)
                    {
                      if (csaveGlobalValues.strings[index6] != null)
                        rhObject.rov.rvStrings[index6] = csaveGlobalValues.strings[index6];
                    }
                  }
                  index2 = rhObject.hoNumNext;
                  if (((int) index2 & 32768 /*0x8000*/) == 0)
                    ++index4;
                  else
                    break;
                }
                while (index4 < csaveGlobal.objects.size());
                break;
              }
            }
          }
        }
      }

      public int f_CreateObject(
        short hlo,
        short oi,
        int coordX,
        int coordY,
        int initDir,
        short flags,
        int nLayer,
        int numCreation)
      {
        CCreateObjectInfo ccreateObjectInfo = new CCreateObjectInfo();
        CLO clo = (CLO) null;
        if (hlo != (short) -1)
          clo = this.rhFrame.LOList.getLOFromHandle(hlo);
        COI oiFromHandle = this.rhApp.OIList.getOIFromHandle(oi);
        CObjectCommon oiOc = (CObjectCommon) oiFromHandle.oiOC;
        if (((int) oiOc.ocFlags2 & 8) == 0)
          flags |= (short) 2;
        if (this.rhNObjects < this.rhMaxObjects)
        {
          CObject cobject = (CObject) null;
          switch (oiFromHandle.oiType)
          {
            case 2:
              cobject = (CObject) new CActive();
              goto case 8;
            case 3:
              cobject = (CObject) new CText();
              goto case 8;
            case 4:
              cobject = (CObject) new CQuestion();
              goto case 8;
            case 5:
              cobject = (CObject) new CScore();
              goto case 8;
            case 6:
              cobject = (CObject) new CLives();
              goto case 8;
            case 7:
              cobject = (CObject) new CCounter();
              goto case 8;
            case 8:
              if (cobject != null)
              {
                if (numCreation < 0)
                {
                  numCreation = 0;
                  while (numCreation < this.rhMaxObjects && this.rhObjectList[numCreation] != null)
                    ++numCreation;
                }
                if (numCreation >= this.rhMaxObjects)
                  return -1;
                this.rhObjectList[numCreation] = cobject;
                ++this.rhNObjects;
                cobject.hoIdentifier = oiOc.ocIdentifier;
                cobject.hoOEFlags = oiOc.ocOEFlags;
                if (numCreation > this.rh4ObjectCurCreate)
                  ++this.rh4ObjectAddCreate;
                cobject.hoNumber = (short) numCreation;
                ++this.rh2CreationCount;
                if (this.rh2CreationCount == (short) 0)
                  this.rh2CreationCount = (short) 1;
                cobject.hoCreationId = this.rh2CreationCount;
                cobject.hoOi = oi;
                cobject.hoHFII = hlo;
                cobject.hoType = oiFromHandle.oiType;
                this.oi_Insert(cobject);
                cobject.hoAdRunHeader = this;
                cobject.hoCallRoutine = true;
                cobject.hoCommon = oiOc;
                int num1 = coordX;
                if (num1 == int.MaxValue)
                  num1 = clo.loX;
                ccreateObjectInfo.cobX = num1;
                cobject.hoX = num1;
                int num2 = coordY;
                if (num2 == int.MaxValue)
                  num2 = clo.loY;
                ccreateObjectInfo.cobY = num2;
                cobject.hoY = num2;
                if (clo != null)
                {
                  if (nLayer == -1)
                    nLayer = (int) clo.loLayer;
                }
                else
                  nLayer = 0;
                ccreateObjectInfo.cobLayer = nLayer;
                cobject.hoLayer = nLayer;
                CLayer layer = this.rhFrame.layers[nLayer];
                ++layer.nZOrderMax;
                ccreateObjectInfo.cobZOrder = layer.nZOrderMax;
                ccreateObjectInfo.cobFlags = flags;
                ccreateObjectInfo.cobDir = initDir;
                ccreateObjectInfo.cobLevObj = clo;
                cobject.roc = (CRCom) null;
                if ((cobject.hoOEFlags & 560) != 0)
                {
                  cobject.roc = new CRCom();
                  cobject.roc.init();
                }
                cobject.rom = (CRMvt) null;
                if ((cobject.hoOEFlags & 16 /*0x10*/) != 0)
                {
                  cobject.rom = new CRMvt();
                  if (((int) ccreateObjectInfo.cobFlags & 1) == 0)
                    cobject.rom.init(0, cobject, oiOc, ccreateObjectInfo, -1);
                }
                cobject.roa = (CRAni) null;
                if ((cobject.hoOEFlags & 32 /*0x20*/) != 0)
                {
                  cobject.roa = new CRAni();
                  cobject.roa.init(cobject);
                }
                cobject.ros = (CRSpr) null;
                if ((cobject.hoOEFlags & 512 /*0x0200*/) != 0)
                {
                  cobject.ros = new CRSpr();
                  cobject.ros.init1(cobject, oiOc, ccreateObjectInfo);
                }
                cobject.rov = (CRVal) null;
                if ((cobject.hoOEFlags & 256 /*0x0100*/) != 0)
                {
                  cobject.rov = new CRVal();
                  cobject.rov.init(cobject, oiOc, ccreateObjectInfo);
                }
                cobject.init(oiOc, ccreateObjectInfo);
                if ((cobject.hoOEFlags & 512 /*0x0200*/) != 0)
                  cobject.ros.init2(true);
                return numCreation;
              }
              break;
            case 9:
              cobject = (CObject) new CCCA();
              goto case 8;
            default:
              cobject = (CObject) new CExtension((int) oiFromHandle.oiType, this);
              if (((CExtension) cobject).ext == null)
              {
                cobject = (CObject) null;
                goto case 8;
              }
              goto case 8;
          }
        }
        return -1;
      }

      public void f_KillObject(int nObject, bool bFast)
      {
        CObject rhObject = this.rhObjectList[nObject];
        if (rhObject == null)
          return;
        this.killShootPtr(rhObject);
        if (rhObject.rom != null)
          rhObject.rom.kill(bFast);
        if (rhObject.rov != null)
          rhObject.rov.kill(bFast);
        if (rhObject.ros != null)
          rhObject.ros.kill(bFast);
        if (rhObject.roc != null)
          rhObject.roc.kill(bFast);
        rhObject.kill(bFast);
        this.oi_Delete(rhObject);
        rhObject.hoCreationId = (short) 0;
        if ((rhObject.hoOEFlags & 4096 /*0x1000*/) != 0 && rhObject.ros.rsLayer == (short) 0)
          this.remove_QuickDisplay(rhObject);
        this.rhObjectList[nObject] = (CObject) null;
        --this.rhNObjects;
        rhObject.hoCallRoutine = false;
      }

      public void destroy_Add(int hoNumber)
      {
        this.rhDestroyList[hoNumber / 32 /*0x20*/] |= 1 << hoNumber;
        ++this.rhDestroyPos;
      }

      public void destroy_List()
      {
        if (this.rhDestroyPos == (short) 0)
          return;
        for (int index1 = 0; index1 < this.rhMaxObjects; index1 += 32 /*0x20*/)
        {
          int rhDestroy = this.rhDestroyList[index1 / 32 /*0x20*/];
          if (rhDestroy != 0)
          {
            for (int index2 = 0; rhDestroy != 0 && index2 < 32 /*0x20*/; ++index2)
            {
              if ((rhDestroy & 1) != 0)
              {
                CObject rhObject = this.rhObjectList[index1 + index2];
                if (rhObject != null && rhObject.hoOiList.oilNObjects == 1)
                {
                  int code = -2162688 | (int) rhObject.hoType & (int) ushort.MaxValue;
                  this.rhEvtProg.handle_Event(rhObject, code);
                }
                this.f_KillObject(index1 + index2, false);
                --this.rhDestroyPos;
              }
              rhDestroy >>= 1;
            }
            this.rhDestroyList[index1 / 32 /*0x20*/] = 0;
            if (this.rhDestroyPos == (short) 0)
              break;
          }
        }
      }

      private void killShootPtr(CObject hoSource)
      {
        int index1 = 0;
        for (int index2 = 0; index2 < this.rhNObjects; ++index2)
        {
          while (this.rhObjectList[index1] == null)
            ++index1;
          CObject rhObject = this.rhObjectList[index1];
          ++index1;
          if (rhObject.rom != null && rhObject.roc.rcMovementType == 13)
          {
            CMoveBullet rmMovement = (CMoveBullet) rhObject.rom.rmMovement;
            if (rmMovement.MBul_ShootObject == hoSource && rmMovement.MBul_Wait)
              rmMovement.startBullet();
          }
        }
      }

      public void oi_Insert(CObject pHo)
      {
        short hoOi = pHo.hoOi;
        int index = 0;
        while (index < this.rhMaxOI && (int) this.rhOiList[index].oilOi != (int) hoOi)
          ++index;
        CObjInfo rhOi = this.rhOiList[index];
        if (((int) rhOi.oilObject & 32768 /*0x8000*/) != 0)
        {
          rhOi.oilObject = pHo.hoNumber;
          pHo.hoNumPrev = (short) -1;
          pHo.hoNumNext = (short) -1;
        }
        else
        {
          CObject rhObject = this.rhObjectList[(int) rhOi.oilObject];
          pHo.hoNumPrev = rhObject.hoNumPrev;
          rhObject.hoNumPrev = pHo.hoNumber;
          pHo.hoNumNext = rhObject.hoNumber;
          rhOi.oilObject = pHo.hoNumber;
        }
        pHo.hoEvents = rhOi.oilEvents;
        pHo.hoOiList = rhOi;
        pHo.hoLimitFlags = rhOi.oilLimitFlags;
        if (pHo.hoHFII == (short) -1)
          pHo.hoHFII = rhOi.oilHFII;
        else if (rhOi.oilHFII == (short) -1)
          rhOi.oilHFII = pHo.hoHFII;
        ++rhOi.oilNObjects;
      }

      private void oi_Delete(CObject pHo)
      {
        CObjInfo hoOiList = pHo.hoOiList;
        --hoOiList.oilNObjects;
        if (pHo.hoNumPrev >= (short) 0)
        {
          CObject rhObject1 = this.rhObjectList[(int) pHo.hoNumPrev];
          if (pHo.hoNumNext >= (short) 0)
          {
            CObject rhObject2 = this.rhObjectList[(int) pHo.hoNumNext];
            if (rhObject1 != null)
              rhObject1.hoNumNext = pHo.hoNumNext;
            if (rhObject2 == null)
              return;
            rhObject2.hoNumPrev = pHo.hoNumPrev;
          }
          else
          {
            if (rhObject1 == null)
              return;
            rhObject1.hoNumNext = (short) -1;
          }
        }
        else if (pHo.hoNumNext >= (short) 0)
        {
          CObject rhObject = this.rhObjectList[(int) pHo.hoNumNext];
          if (rhObject == null)
            return;
          rhObject.hoNumPrev = pHo.hoNumPrev;
          hoOiList.oilObject = rhObject.hoNumber;
        }
        else
          hoOiList.oilObject = (short) -1;
      }

      public void pause()
      {
        ++this.rh2PauseCompteur;
        if (this.rh2PauseCompteur != 1)
          return;
        this.rh2PauseTimer = (int) this.rhApp.timer;
        this.rh2PauseFPSTimer = (int) this.rhApp.timer;
        this.rh2PauseState = 0;
        this.rh2PauseVbl = this.rhApp.newGetCptVbl() - this.rhVBLOld;
        int index1 = 0;
        for (int index2 = 0; index2 < this.rhNObjects; ++index2)
        {
          while (this.rhObjectList[index1] == null)
            ++index1;
          CObject rhObject = this.rhObjectList[index1];
          ++index1;
          if (rhObject.hoType == (short) 9)
            ((CCCA) rhObject).pause();
          else if (rhObject.hoType >= (short) 32 /*0x20*/)
            ((CExtension) rhObject).ext.pauseRunObject();
        }
        this.rhApp.soundPlayer.pause();
        this.showMouse();
      }

      public void resume()
      {
        if (this.rh2PauseCompteur == 0)
          return;
        this.rh2PauseCompteur = Math.Max(this.rh2PauseCompteur - 1, 0);
        if (this.rh2PauseCompteur != 0)
          return;
        int index1 = 0;
        for (int index2 = 0; index2 < this.rhNObjects; ++index2)
        {
          while (this.rhObjectList[index1] == null)
            ++index1;
          CObject rhObject = this.rhObjectList[index1];
          ++index1;
          if (rhObject.hoType == (short) 9)
            ((CCCA) rhObject).resume();
          else if (rhObject.hoType >= (short) 32 /*0x20*/)
            ((CExtension) rhObject).ext.continueRunObject();
        }
        this.rhApp.soundPlayer.resume();
        this.rhTimerOld += (long) (int) (this.rhApp.timer - (long) this.rh2PauseTimer);
        this.rhTimerFPSOld += (long) (int) (this.rhApp.timer - (long) this.rh2PauseFPSTimer);
        this.rhVBLOld = this.rhApp.newGetCptVbl() - this.rh2PauseVbl;
        this.rh4PauseKey = Keys.None;
        this.bCheckResume = false;
      }

      public void f_StopSamples() => this.rhApp.soundPlayer.stopAllSounds();

      public void redrawLevel(int flags)
      {
        bool flgColMaskEmpty = false;
        CObject sprProc1 = (CObject) null;
        bool flag1 = (this.rhFrame.leFlags & 32 /*0x20*/) != 0;
        bool flag2 = (flags & 16 /*0x10*/) == 0;
        bool flag3 = (flags & 64 /*0x40*/) != 0;
        CRect crect = new CRect();
        bool flag4 = false;
        crect.left = crect.top = 0;
        crect.right = this.rhApp.gaCxWin;
        crect.bottom = this.rhApp.gaCyWin;
        int x2edit = crect.right - 1;
        int y2edit = crect.bottom - 1;
        if ((flags & 268) != 0)
        {
          for (int nLayer = 0; nLayer < this.rhFrame.nLayers; ++nLayer)
          {
            CLayer layer = this.rhFrame.layers[nLayer];
            if ((layer.dwOptions & 262144 /*0x040000*/) != 0)
              this.f_ShowAllObjects(nLayer, true);
            if ((layer.dwOptions & 131072 /*0x020000*/) != 0)
              this.f_ShowAllObjects(nLayer, false);
          }
        }
        if (!flag3 && (flags & 128 /*0x80*/) != 0 && (this.rhFrame.layers[0].dwOptions & 65536 /*0x010000*/) == 0)
          flag3 = true;
        for (int index1 = 0; index1 < this.rhFrame.nLayers; ++index1)
        {
          CLayer layer = this.rhFrame.layers[index1];
          if ((layer.dwOptions & 131072 /*0x020000*/) != 0)
          {
            int nBkdLos = layer.nBkdLOs;
            for (int index2 = 0; index2 < nBkdLos; ++index2)
            {
              CLO loFromIndex = this.rhFrame.LOList.getLOFromIndex((short) (layer.nFirstLOIndex + index2));
              for (int index3 = 0; index3 < 4; ++index3)
              {
                if (loFromIndex.loSpr[index3] != null)
                {
                  this.rhApp.spriteGen.delSprite(loFromIndex.loSpr[index3]);
                  loFromIndex.loSpr[index3] = (CSprite) null;
                }
              }
            }
          }
        }
        if ((flags & 4) != 0)
        {
          int num = flags & 128 /*0x80*/;
          this.f_UpdateWindowPos(this.rhFrame.leX, this.rhFrame.leY);
        }
        if (this.rhFrame.colMask != null && flag2)
        {
          this.rhFrame.colMask.fillRectangle(-32767, -32767, (int) short.MaxValue, (int) short.MaxValue, 0);
          flgColMaskEmpty = true;
        }
        int leWidth = this.rhFrame.leWidth;
        int leHeight = this.rhFrame.leHeight;
        int nLayer1 = 0;
        if (flag3)
          ++nLayer1;
        for (; nLayer1 < this.rhFrame.nLayers; ++nLayer1)
        {
          CLayer layer = this.rhFrame.layers[nLayer1];
          layer.x += layer.dx;
          layer.y += layer.dy;
          layer.dx = 0;
          layer.dy = 0;
          if ((layer.dwOptions & 262144 /*0x040000*/) != 0)
            layer.dwOptions |= 16 /*0x10*/;
          if ((layer.dwOptions & 16 /*0x10*/) == 0)
          {
            if (flag2)
              flag4 = true;
            else
              continue;
          }
          if ((flags & 128 /*0x80*/) == 0 || (layer.dwOptions & 65536 /*0x010000*/) != 0)
          {
            layer.dwOptions &= -65537;
            bool flag5 = (layer.dwOptions & 32 /*0x20*/) != 0;
            bool flag6 = (layer.dwOptions & 64 /*0x40*/) != 0;
            bool flag7 = flag5 | flag6;
            int num1 = this.rhFrame.leX;
            int num2 = this.rhFrame.leY;
            if ((layer.dwOptions & 3) != 0)
            {
              if ((layer.dwOptions & 1) != 0)
                num1 = (int) ((double) num1 * (double) layer.xCoef);
              if ((layer.dwOptions & 2) != 0)
                num2 = (int) ((double) num2 * (double) layer.yCoef);
            }
            int num3 = num1 + layer.x;
            int num4 = num2 + layer.y;
            if (flag5)
              num3 %= leWidth;
            if (flag6)
              num4 %= leHeight;
            this.y_Ladder_Reset(nLayer1);
            int nBkdLos = layer.nBkdLOs;
            if ((layer.dwOptions & 131072 /*0x020000*/) != 0)
            {
              this.f_ShowAllObjects(nLayer1, false);
              if (nLayer1 == 0)
                flag4 = true;
            }
            if ((layer.dwOptions & 16 /*0x10*/) != 0 && (layer.dwOptions & 131072 /*0x020000*/) == 0 || nLayer1 == 0)
            {
              bool flag8 = (layer.dwOptions & 4) == 0;
              if ((layer.dwOptions & 262144 /*0x040000*/) != 0)
              {
                layer.dwOptions &= -262145;
                this.f_ShowAllObjects(nLayer1, true);
              }
              uint num5 = 0;
              int index4 = 0;
              for (int index5 = 0; index5 < nBkdLos; ++index5)
              {
                CLO loFromIndex = this.rhFrame.LOList.getLOFromIndex((short) (index5 + layer.nFirstLOIndex));
                bool flag9 = true;
                int num6 = index4;
                int index6 = index4;
                COI coi = (COI) null;
                COC sprProc2 = (COC) null;
                CObjectCommon cobjectCommon = (CObjectCommon) null;
                int loType = (int) loFromIndex.loType;
                if (loType < 2)
                {
                  crect.left = loFromIndex.loX - num3;
                  crect.top = loFromIndex.loY - num4;
                }
                else
                {
                  coi = this.rhApp.OIList.getOIFromHandle(loFromIndex.loOiHandle);
                  if (coi == null || coi.oiOC == null)
                  {
                    num5 = 0U;
                    index4 = 0;
                    goto label_146;
                  }
                  sprProc2 = coi.oiOC;
                  cobjectCommon = (CObjectCommon) sprProc2;
                  if ((cobjectCommon.ocOEFlags & 2) == 0 || (sprProc1 = this.find_HeaderObject(loFromIndex.loHandle)) == null)
                  {
                    num5 = 0U;
                    index4 = 0;
                    goto label_146;
                  }
                  crect.left = sprProc1.hoX - this.rhFrame.leX - sprProc1.hoImgXSpot;
                  crect.top = sprProc1.hoY - this.rhFrame.leY - sprProc1.hoImgYSpot;
                  sprProc1.getZoneInfos();
                }
                if (!flag1 && !flag7 && (crect.left >= x2edit + 64 /*0x40*/ + 32 /*0x20*/ || crect.top >= y2edit + 16 /*0x10*/))
                {
                  num5 = 0U;
                  index4 = 0;
                }
                else
                {
                  int num7;
                  bool flag10;
                  if (loType < 2)
                  {
                    coi = this.rhApp.OIList.getOIFromHandle(loFromIndex.loOiHandle);
                    if (coi == null || coi.oiOC == null)
                    {
                      num5 = 0U;
                      index4 = 0;
                      goto label_146;
                    }
                    sprProc2 = coi.oiOC;
                    crect.right = crect.left + sprProc2.ocCx;
                    crect.bottom = crect.top + sprProc2.ocCy;
                    num7 = (int) sprProc2.ocObstacleType;
                    flag10 = sprProc2.ocColMode != (short) 0;
                  }
                  else
                  {
                    crect.right = crect.left + sprProc1.hoImgWidth;
                    crect.bottom = crect.top + sprProc1.hoImgHeight;
                    num7 = ((int) cobjectCommon.ocFlags2 & 48 /*0x30*/) >> 4;
                    flag10 = ((int) cobjectCommon.ocFlags2 & 4) != 0;
                  }
                  if (flag7)
                  {
                    switch (index4)
                    {
                      case 0:
                        if (flag5 && (crect.left < 0 || crect.right > leWidth))
                        {
                          if (flag6 && (crect.top < 0 || crect.bottom > leHeight))
                          {
                            index4 = 3;
                            num5 |= 7U;
                          }
                          else
                          {
                            index4 = 1;
                            num5 |= 1U;
                          }
                        }
                        else if (flag6 && (crect.top < 0 || crect.bottom > leHeight))
                        {
                          index4 = 2;
                          num5 |= 2U;
                        }
                        if (((int) num5 & 1) == 0 && loFromIndex.loSpr[1] != null)
                        {
                          this.rhApp.spriteGen.delSprite(loFromIndex.loSpr[1]);
                          loFromIndex.loSpr[1] = (CSprite) null;
                        }
                        if (((int) num5 & 2) == 0 && loFromIndex.loSpr[2] != null)
                        {
                          this.rhApp.spriteGen.delSprite(loFromIndex.loSpr[2]);
                          loFromIndex.loSpr[2] = (CSprite) null;
                        }
                        if (((int) num5 & 4) == 0 && loFromIndex.loSpr[3] != null)
                        {
                          this.rhApp.spriteGen.delSprite(loFromIndex.loSpr[3]);
                          loFromIndex.loSpr[3] = (CSprite) null;
                          break;
                        }
                        break;
                      case 1:
                        if (crect.left < 0)
                        {
                          int num8 = leWidth;
                          crect.left += num8;
                          crect.right += num8;
                        }
                        else if (crect.right > leWidth)
                        {
                          int num9 = leWidth;
                          crect.left -= num9;
                          crect.right -= num9;
                        }
                        num5 &= 4294967294U;
                        index4 = 0;
                        if (((int) num5 & 2) != 0)
                        {
                          index4 = 2;
                          break;
                        }
                        break;
                      case 2:
                        if (crect.top < 0)
                        {
                          int num10 = leHeight;
                          crect.top += num10;
                          crect.bottom += num10;
                        }
                        else if (crect.bottom > leHeight)
                        {
                          int num11 = leHeight;
                          crect.top -= num11;
                          crect.bottom -= num11;
                        }
                        num5 &= 4294967293U;
                        index4 = 0;
                        if (((int) num5 & 1) != 0)
                        {
                          index4 = 1;
                          break;
                        }
                        break;
                      case 3:
                        if (crect.left < 0)
                        {
                          int num12 = leWidth;
                          crect.left += num12;
                          crect.right += num12;
                        }
                        else if (crect.right > leWidth)
                        {
                          int num13 = leWidth;
                          crect.left -= num13;
                          crect.right -= num13;
                        }
                        if (crect.top < 0)
                        {
                          int num14 = leHeight;
                          crect.top += num14;
                          crect.bottom += num14;
                        }
                        else if (crect.bottom > leHeight)
                        {
                          int num15 = leHeight;
                          crect.top -= num15;
                          crect.bottom -= num15;
                        }
                        num5 &= 4294967291U;
                        index4 = 2;
                        break;
                    }
                  }
                  if (num7 == 3)
                  {
                    this.y_Ladder_Add(nLayer1, crect.left, crect.top, crect.right, crect.bottom);
                    flag10 = true;
                  }
                  if (this.rhFrame.colMask != null && nLayer1 == 0 && flag2 && num7 != 4 && (flag1 || crect.right >= -96 && crect.bottom >= -16))
                  {
                    CMask cmask = (CMask) null;
                    if (flag1)
                    {
                      crect.left += num3;
                      crect.top += num4;
                      crect.right += num3;
                      crect.bottom += num4;
                    }
                    int val = 0;
                    if (num7 == 1)
                    {
                      val = 3;
                      flgColMaskEmpty = false;
                    }
                    if (!flgColMaskEmpty)
                    {
                      if (flag10)
                      {
                        this.rhFrame.colMask.fillRectangle(crect.left, crect.top, crect.right - 1, crect.bottom - 1, val);
                      }
                      else
                      {
                        if (cmask == null)
                          cmask = loType >= 2 ? sprProc1.getCollisionMask(0) : this.rhApp.imageBank.getImageFromHandle(((COCBackground) sprProc2).ocImage).getMask(0, 0, 1f, 1f);
                        if (cmask == null)
                          this.rhFrame.colMask.fillRectangle(crect.left, crect.top, crect.right - 1, crect.bottom - 1, val);
                        else
                          this.rhFrame.colMask.orMask(cmask, crect.left, crect.top, 3, val);
                      }
                    }
                    if (num7 == 2)
                    {
                      flgColMaskEmpty = false;
                      if (flag10)
                      {
                        this.rhFrame.colMask.fillRectangle(crect.left, crect.top, crect.right - 1, Math.Min(crect.top + 6, crect.bottom) - 1, 2);
                      }
                      else
                      {
                        if (cmask == null)
                          cmask = loType >= 2 ? sprProc1.getCollisionMask(0) : this.rhApp.imageBank.getImageFromHandle(((COCBackground) sprProc2).ocImage).getMask(0, 0, 1f, 1f);
                        if (cmask == null)
                          this.rhFrame.colMask.fillRectangle(crect.left, crect.top, crect.right - 1, Math.Min(crect.top + 6, crect.bottom) - 1, 2);
                        else
                          this.rhFrame.colMask.orPlatformMask(cmask, crect.left, crect.top);
                      }
                    }
                    if (flag1)
                    {
                      crect.left -= num3;
                      crect.top -= num4;
                      crect.right -= num3;
                      crect.bottom -= num4;
                    }
                  }
                  if (crect.left <= x2edit && crect.top <= y2edit && crect.right >= 0 && crect.bottom >= 0)
                  {
                    flag9 = false;
                    if (nLayer1 > 0 || !flag4)
                    {
                      uint sFlags = 4718600;
                      if (!flag8)
                        sFlags |= 512U /*0x0200*/;
                      if (nLayer1 > 0)
                      {
                        switch (num7)
                        {
                          case 1:
                            sFlags |= 65537U /*0x010001*/;
                            break;
                          case 2:
                            sFlags |= 131073U /*0x020001*/;
                            break;
                        }
                      }
                      if (loFromIndex.loSpr[index6] == null)
                      {
                        switch (loType)
                        {
                          case 0:
                            loFromIndex.loSpr[index6] = this.rhApp.spriteGen.addOwnerDrawSprite(crect.left, crect.top, crect.right, crect.bottom, loFromIndex.loLayer, index5 * 4 + num6, 0, sFlags | 256U /*0x0100*/, (CObject) null, (IDrawing) sprProc2);
                            break;
                          case 1:
                            loFromIndex.loSpr[index6] = this.rhApp.spriteGen.addSprite(crect.left, crect.top, ((COCBackground) sprProc2).ocImage, loFromIndex.loLayer, index5 * 4 + num6, 0, sFlags, (CObject) null);
                            this.rhApp.spriteGen.modifSpriteEffect(loFromIndex.loSpr[index4], coi.oiInkEffect, coi.oiInkEffectParam);
                            break;
                          default:
                            if (sprProc1 != null)
                            {
                              loFromIndex.loSpr[index6] = this.rhApp.spriteGen.addOwnerDrawSprite(crect.left, crect.top, crect.right, crect.bottom, loFromIndex.loLayer, index5 * 4 + num6, 0, sFlags | 256U /*0x0100*/, (CObject) null, (IDrawing) sprProc1);
                              break;
                            }
                            break;
                        }
                      }
                      else
                      {
                        switch (loType)
                        {
                          case 0:
                            CRect spriteRect = loFromIndex.loSpr[index6].getSpriteRect();
                            if (crect.left != spriteRect.left || crect.top != spriteRect.top || crect.right != spriteRect.right || crect.bottom != spriteRect.bottom)
                            {
                              this.rhApp.spriteGen.modifOwnerDrawSprite(loFromIndex.loSpr[index6], crect.left, crect.top, crect.right, crect.bottom);
                              break;
                            }
                            break;
                          case 1:
                            this.rhApp.spriteGen.modifSprite(loFromIndex.loSpr[index6], crect.left, crect.top, ((COCBackground) sprProc2).ocImage);
                            break;
                          default:
                            if (sprProc1 != null)
                            {
                              this.rhApp.spriteGen.modifOwnerDrawSprite(loFromIndex.loSpr[index6], crect.left, crect.top, crect.right, crect.bottom);
                              break;
                            }
                            break;
                        }
                      }
                    }
                  }
                }
    label_146:
                if (flag9 && loFromIndex.loSpr[index6] != null)
                {
                  this.rhApp.spriteGen.delSprite(loFromIndex.loSpr[index6]);
                  loFromIndex.loSpr[index6] = (CSprite) null;
                }
                if (num5 != 0U)
                  --index5;
              }
            }
            if (layer.pBkd2 != null)
              this.displayBkd2Layer(layer, nLayer1, flags, x2edit, y2edit, flgColMaskEmpty);
            if ((layer.dwOptions & 131072 /*0x020000*/) != 0)
              layer.dwOptions &= -131089;
          }
        }
        if (!flag1)
          return;
        CLayer layer1 = this.rhFrame.layers[0];
        int num16 = this.rhFrame.leX;
        int num17 = this.rhFrame.leY;
        if ((layer1.dwOptions & 3) != 0)
        {
          if ((layer1.dwOptions & 1) != 0)
            num16 = (int) ((double) num16 * (double) layer1.xCoef);
          if ((layer1.dwOptions & 2) != 0)
            num17 = (int) ((double) num17 * (double) layer1.yCoef);
        }
        int dx = num16 + layer1.x;
        int dy = num17 + layer1.y;
        if (this.rhFrame.colMask == null)
          return;
        this.rhFrame.colMask.setOrigin(dx, dy);
      }

      public void ohRedrawLevel(bool bRedrawTotalColMask)
      {
        this.rh3Scrolling |= (byte) 4;
        if (!bRedrawTotalColMask)
          return;
        this.rh3Scrolling |= (byte) 8;
      }

      private void scrollLevel()
      {
        int leEditWinWidth = this.rhFrame.leEditWinWidth;
        int leEditWinHeight = this.rhFrame.leEditWinHeight;
        float num1 = 1f;
        float num2 = 1f;
        if (this.rhFrame.nLayers > 0)
        {
          CLayer layer = this.rhFrame.layers[0];
          num1 = layer.xCoef;
          num2 = layer.yCoef;
        }
        int num3 = this.rhFrame.leLastScrlX;
        int num4 = this.rh3DisplayX;
        if ((double) num1 != 1.0)
        {
          num3 = (int) ((double) num3 * (double) num1);
          num4 = (int) ((double) num4 * (double) num1);
        }
        int num5;
        int num6;
        if (num4 < num3)
        {
          num5 = 0;
          num6 = num3 - num4;
          this.rhFrame.leLastScrlX = this.rh3DisplayX;
        }
        else
        {
          num5 = num4 - num3;
          num6 = 0;
          if (num5 != 0)
            this.rhFrame.leLastScrlX = this.rh3DisplayX;
        }
        int num7 = this.rhFrame.leLastScrlY;
        int num8 = this.rh3DisplayY;
        if ((double) num2 != 1.0)
        {
          num7 = (int) ((double) num7 * (double) num2);
          num8 = (int) ((double) num8 * (double) num2);
        }
        int num9;
        int num10;
        if (num8 < num7)
        {
          num9 = 0;
          num10 = num7 - num8;
          this.rhFrame.leLastScrlY = this.rh3DisplayY;
        }
        else
        {
          num9 = num8 - num7;
          num10 = 0;
          if (num9 != 0)
            this.rhFrame.leLastScrlY = this.rh3DisplayY;
        }
        int num11 = leEditWinWidth - num5 - num6;
        int num12 = leEditWinHeight - num9 - num10;
        this.rhFrame.leX = this.rh3DisplayX;
        this.rhFrame.leY = this.rh3DisplayY;
        this.rhApp.spriteGen.activeSprite((CSprite) null, 1, (CRect) null);
        for (int nLayer = 0; nLayer < this.rhFrame.nLayers; ++nLayer)
        {
          CLayer layer = this.rhFrame.layers[nLayer];
          if ((layer.dwOptions & 262144 /*0x040000*/) != 0)
            this.f_ShowAllObjects(nLayer, true);
          if ((layer.dwOptions & 131072 /*0x020000*/) != 0)
            this.f_ShowAllObjects(nLayer, false);
        }
        this.f_UpdateWindowPos(this.rhFrame.leX, this.rhFrame.leY);
        bool flag1;
        bool flag2 = flag1 = false;
        if (num11 > leEditWinWidth / 4 && num12 > leEditWinHeight / 4)
        {
          if (num11 == leEditWinWidth && num12 == leEditWinHeight)
          {
            flag2 = true;
            flag1 = true;
          }
          else if (num11 > 0 && num12 > 0)
            flag2 = true;
        }
        if (!flag2)
        {
          this.redrawLevel(18);
        }
        else
        {
          bool flag3 = false;
          if (num5 != 0 || num6 != 0)
          {
            if (flag1)
              this.redrawLevel(34);
            else
              this.redrawLevel(18);
            flag3 = true;
          }
          if (num9 != 0 || num10 != 0)
          {
            if (flag1)
              this.redrawLevel(34);
            else
              this.redrawLevel(18);
            flag3 = true;
          }
          if (flag3 || this.rhFrame.nLayers <= 0)
            return;
          if ((this.rhFrame.layers[0].dwOptions & 65536 /*0x010000*/) != 0)
            this.redrawLevel(18);
          else
            this.redrawLevel(82);
        }
      }

      private void updateScrollLevelPos()
      {
        float num1 = 1f;
        float num2 = 1f;
        if (this.rhFrame.nLayers > 0)
        {
          CLayer layer = this.rhFrame.layers[0];
          num1 = layer.xCoef;
          num2 = layer.yCoef;
        }
        int num3 = this.rhFrame.leLastScrlX;
        int num4 = this.rh3DisplayX;
        if ((double) num1 != 1.0)
        {
          num3 = (int) ((double) num3 * (double) num1);
          num4 = (int) ((double) num4 * (double) num1);
        }
        if (num4 < num3)
          this.rhFrame.leLastScrlX = this.rh3DisplayX;
        else if (num4 - num3 != 0)
          this.rhFrame.leLastScrlX = this.rh3DisplayX;
        int num5 = this.rhFrame.leLastScrlY;
        int num6 = this.rh3DisplayY;
        if ((double) num2 != 1.0)
        {
          num5 = (int) ((double) num5 * (double) num2);
          num6 = (int) ((double) num6 * (double) num2);
        }
        if (num6 < num5)
          this.rhFrame.leLastScrlY = this.rh3DisplayY;
        else if (num6 - num5 != 0)
          this.rhFrame.leLastScrlY = this.rh3DisplayY;
        this.rhFrame.leX = this.rh3DisplayX;
        this.rhFrame.leY = this.rh3DisplayY;
      }

      public void screen_Update()
      {
        Color color = CServices.getColor(this.rhApp.frame == null ? this.rhApp.gaBorderColour : this.rhApp.frame.leBackground);
        if (this.rhApp.parentApp == null)
        {
          this.rhApp.graphicsDevice.Clear(color);
        }
        else
        {
          if (!this.rhApp.bSubAppShown)
            return;
          this.rhApp.services.drawFilledRectangleSub(this.rhApp.spriteBatch, this.rhApp.xOffset, this.rhApp.yOffset, this.rhApp.parentWidth, this.rhApp.parentHeight, color, 0, 0);
        }
        if (this.rh3Scrolling != (byte) 0)
        {
          if (((int) this.rh3Scrolling & 4) != 0)
          {
            if (this.rhFrame.leX != this.rh3DisplayX || this.rhFrame.leY != this.rh3DisplayY)
              this.updateScrollLevelPos();
            int flags = 4;
            if (((int) this.rh3Scrolling & 8) == 0 && (this.rhFrame.leFlags & 32 /*0x20*/) != 0)
              flags |= 16 /*0x10*/;
            this.redrawLevel(flags);
            this.rh3DisplayX = this.rhWindowX;
            this.rh3DisplayY = this.rhWindowY;
          }
          else if (((int) this.rh3Scrolling & 1) != 0)
          {
            if (this.rhFrame.leX != this.rh3DisplayX || this.rhFrame.leY != this.rh3DisplayY)
              this.scrollLevel();
          }
          else
            this.redrawLevel(148);
        }
        this.rhApp.spriteGen.spriteUpdate();
        this.rhApp.spriteGen.spriteDraw(this.rhApp.spriteBatch);
        this.rh3Scrolling = (byte) 0;
        if (this.questionObjectOn != null)
          this.questionObjectOn.draw(this.rhApp.spriteBatch);
        if (this.nSubApps != 0)
        {
          int index1 = 0;
          for (int index2 = 0; index2 < this.rhNObjects; ++index2)
          {
            while (this.rhObjectList[index1] == null)
              ++index1;
            CObject rhObject = this.rhObjectList[index1];
            ++index1;
            if (rhObject.hoType == (short) 9)
              rhObject.draw(this.rhApp.spriteBatch);
          }
        }
        if (this.nControls != 0)
        {
          for (int index = 0; index < this.nControls; ++index)
            ((IControl) this.controls.get(index)).drawControl(this.rhApp.spriteBatch);
        }
        if (this.joystick == null)
          return;
        this.joystick.draw(this.rhApp.spriteBatch);
      }

      public CObject find_HeaderObject(short hlo)
      {
        int index1 = 0;
        for (int index2 = 0; index2 < this.rhNObjects; ++index2)
        {
          while (this.rhObjectList[index1] == null)
            ++index1;
          if ((int) hlo == (int) this.rhObjectList[index1].hoHFII)
            return this.rhObjectList[index1];
          ++index1;
        }
        return (CObject) null;
      }

      public void f_UpdateWindowPos(int newX, int newY)
      {
        short num1 = 0;
        this.rh4WindowDeltaX = newX - this.rhWindowX;
        if (this.rh4WindowDeltaX != 0)
          ++num1;
        this.rh4WindowDeltaY = newY - this.rhWindowY;
        if (this.rh4WindowDeltaY != 0)
          ++num1;
        if (num1 == (short) 0)
        {
          for (int index = 0; index < this.rhFrame.nLayers; ++index)
          {
            CLayer layer = this.rhFrame.layers[index];
            if (layer.dx != 0 || layer.dy != 0)
            {
              ++num1;
              break;
            }
          }
        }
        int rhWindowX = this.rhWindowX;
        int rhWindowY = this.rhWindowY;
        int num2 = newX;
        int num3 = newY;
        int rh4WindowDeltaX = this.rh4WindowDeltaX;
        int rh4WindowDeltaY = this.rh4WindowDeltaY;
        this.rhWindowX = newX;
        this.rh3XMinimum = newX - 64 /*0x40*/;
        if (this.rh3XMinimum < 0)
          this.rh3XMinimum = this.rh3XMinimumKill;
        this.rhWindowY = newY;
        this.rh3YMinimum = newY - 16 /*0x10*/;
        if (this.rh3YMinimum < 0)
          this.rh3YMinimum = this.rh3YMinimumKill;
        this.rh3XMaximum = newX + this.rh3WindowSx + 64 /*0x40*/;
        if (this.rh3XMaximum > this.rhLevelSx)
          this.rh3XMaximum = this.rh3XMaximumKill;
        this.rh3YMaximum = newY + this.rh3WindowSy + 16 /*0x10*/;
        if (this.rh3YMaximum > this.rhLevelSy)
          this.rh3YMaximum = this.rh3YMaximumKill;
        this.rh4FirstQuickDisplay = (short) -1;
        this.rh4LastQuickDisplay = (short) -1;
        int index1 = 0;
        for (int index2 = 0; index2 < this.rhNObjects; ++index2)
        {
          while (this.rhObjectList[index1] == null)
            ++index1;
          CObject rhObject = this.rhObjectList[index1];
          ++index1;
          if (num1 != (short) 0)
          {
            if ((rhObject.hoOEFlags & 2048 /*0x0800*/) != 0)
            {
              int num4 = rh4WindowDeltaX;
              int num5 = rh4WindowDeltaY;
              if (rhObject.rom == null)
              {
                rhObject.hoX += num4;
                rhObject.hoY += num5;
              }
              else
              {
                int x = num4 + rhObject.hoX;
                int u = num5 + rhObject.hoY;
                rhObject.rom.rmMovement.setXPosition(x);
                rhObject.rom.rmMovement.setYPosition(u);
              }
            }
            else
            {
              int hoLayer = rhObject.hoLayer;
              if (hoLayer < this.rhFrame.nLayers)
              {
                int num6 = rhWindowX;
                int num7 = rhWindowY;
                int num8 = num2;
                int num9 = num3;
                CLayer layer = this.rhFrame.layers[hoLayer];
                if ((layer.dwOptions & 1) != 0)
                {
                  num6 = (int) ((double) layer.xCoef * (double) num6);
                  num8 = (int) ((double) layer.xCoef * (double) num8);
                }
                if ((layer.dwOptions & 2) != 0)
                {
                  num7 = (int) ((double) layer.yCoef * (double) num7);
                  num9 = (int) ((double) layer.yCoef * (double) num9);
                }
                int x = rhObject.hoX + num6 - num8 + rh4WindowDeltaX - layer.dx;
                int u = rhObject.hoY + num7 - num9 + rh4WindowDeltaY - layer.dy;
                if ((rhObject.hoOEFlags & 16 /*0x10*/) == 0)
                {
                  rhObject.hoX = x;
                  rhObject.hoY = u;
                }
                else
                {
                  rhObject.rom.rmMovement.setXPosition(x);
                  rhObject.rom.rmMovement.setYPosition(u);
                }
              }
            }
            if ((rhObject.hoOEFlags & 2) == 0)
              rhObject.modif();
          }
          else if ((rhObject.hoOEFlags & 2) == 0)
            rhObject.display();
        }
      }

      public void f_ShowAllObjects(int nLayer, bool bShow)
      {
        int index1 = 0;
        for (int index2 = 0; index2 < this.rhNObjects; ++index2)
        {
          while (this.rhObjectList[index1] == null)
            ++index1;
          CObject rhObject = this.rhObjectList[index1];
          ++index1;
          if ((nLayer == rhObject.hoLayer || nLayer == -1) && rhObject.ros != null)
          {
            if (rhObject.roc.rcSprite != null)
              this.rhApp.spriteGen.activeSprite(rhObject.roc.rcSprite, 1, (CRect) null);
            if (bShow)
            {
              if (((int) rhObject.ros.rsFlags & 32 /*0x20*/) != 0)
              {
                CLayer layer = this.rhFrame.layers[rhObject.hoLayer];
                int dwOptions = layer.dwOptions;
                layer.dwOptions = layer.dwOptions & -393217 | 16 /*0x10*/;
                rhObject.ros.obShow();
                layer.dwOptions = dwOptions;
              }
            }
            else
              rhObject.ros.obHide();
            rhObject.ros.rsFlash = 0;
          }
        }
      }

      public void setDisplay(int x, int y, int nLayer, int flags)
      {
        x -= this.rh3WindowSx / 2;
        y -= this.rh3WindowSy / 2;
        float num1 = (float) x;
        float num2 = (float) y;
        if (nLayer != -1 && nLayer < this.rhFrame.nLayers)
        {
          CLayer layer = this.rhFrame.layers[nLayer];
          if ((double) layer.xCoef > 1.0)
            num1 = (float) this.rhWindowX + (num1 - (float) this.rhWindowX) / layer.xCoef;
          if ((double) layer.yCoef > 1.0)
            num2 = (float) this.rhWindowY + (num2 - (float) this.rhWindowY) / layer.yCoef;
        }
        x = (int) num1;
        y = (int) num2;
        if (x < 0)
          x = 0;
        if (y < 0)
          y = 0;
        int num3 = x + this.rh3WindowSx;
        int num4 = y + this.rh3WindowSy;
        if (num3 > this.rhLevelSx)
        {
          int num5 = this.rhLevelSx - this.rh3WindowSx;
          if (num5 < 0)
            num5 = 0;
          x = num5;
        }
        if (num4 > this.rhLevelSy)
        {
          int num6 = this.rhLevelSy - this.rh3WindowSy;
          if (num6 < 0)
            num6 = 0;
          y = num6;
        }
        if ((flags & 1) != 0 && x != this.rhWindowX)
        {
          this.rh3DisplayX = x;
          this.rh3Scrolling |= (byte) 1;
        }
        if ((flags & 2) == 0 || y == this.rhWindowY)
          return;
        this.rh3DisplayY = y;
        this.rh3Scrolling |= (byte) 1;
      }

      public void y_Ladder_Reset(int nLayer)
      {
        if (nLayer < 0 || nLayer >= this.rhFrame.nLayers)
          return;
        this.rhFrame.layers[nLayer].pLadders = (CArrayList) null;
      }

      public void y_Ladder_Add(int nLayer, int x1, int y1, int x2, int y2)
      {
        if (nLayer < 0 || nLayer >= this.rhFrame.nLayers)
          return;
        CLayer layer = this.rhFrame.layers[nLayer];
        CRect o = new CRect();
        o.left = Math.Min(x1, x2);
        o.top = Math.Min(y1, y2);
        o.right = Math.Max(x1, x2);
        o.bottom = Math.Max(y1, y2);
        if (layer.pLadders == null)
          layer.pLadders = new CArrayList();
        layer.pLadders.add((object) o);
      }

      public void y_Ladder_Sub(int nLayer, int x1, int y1, int x2, int y2)
      {
        if (nLayer < 0 || nLayer >= this.rhFrame.nLayers)
          return;
        CLayer layer = this.rhFrame.layers[nLayer];
        if (layer.pLadders == null)
          return;
        CRect rc = new CRect();
        rc.left = Math.Min(x1, x2);
        rc.top = Math.Min(y1, y2);
        rc.right = Math.Max(x1, x2);
        rc.bottom = Math.Max(y1, y2);
        for (int index = 0; index < layer.pLadders.size(); ++index)
        {
          if (((CRect) layer.pLadders.get(index)).intersectRect(rc))
          {
            layer.pLadders.remove(index);
            --index;
          }
        }
      }

      public CRect y_GetLadderAt(int nLayer, int x, int y)
      {
        int index1;
        int num;
        if (nLayer == -1)
        {
          index1 = 0;
          num = this.rhFrame.nLayers;
        }
        else
        {
          index1 = nLayer;
          num = nLayer + 1;
        }
        for (; index1 < num; ++index1)
        {
          CLayer layer = this.rhFrame.layers[index1];
          if (layer.pLadders != null)
          {
            for (int index2 = 0; index2 < layer.pLadders.size(); ++index2)
            {
              CRect ladderAt = (CRect) layer.pLadders.get(index2);
              if (x >= ladderAt.left && y >= ladderAt.top && x < ladderAt.right && y < ladderAt.bottom)
                return ladderAt;
            }
          }
        }
        return (CRect) null;
      }

      public CRect y_GetLadderAt_Absolute(int nLayer, int x, int y)
      {
        x -= this.rhFrame.leX;
        y -= this.rhFrame.leY;
        return this.y_GetLadderAt(nLayer, x, y);
      }

      public void activeToBackdrop(CObject hoPtr, int nTypeObst, bool bTrueObject)
      {
        CBkd2 toadd = new CBkd2();
        toadd.img = hoPtr.roc.rcImage;
        CImage imageFromHandle = this.rhApp.imageBank.getImageFromHandle(toadd.img);
        toadd.loHnd = (short) 0;
        toadd.oiHnd = (short) 0;
        toadd.x = hoPtr.hoX - (int) imageFromHandle.xSpot;
        toadd.y = hoPtr.hoY - (int) imageFromHandle.ySpot;
        toadd.nLayer = (short) hoPtr.hoLayer;
        toadd.obstacleType = (short) nTypeObst;
        toadd.colMode = (short) 1;
        if (((int) hoPtr.ros.rsCreaFlags & 256 /*0x0100*/) != 0)
          toadd.colMode = (short) 0;
        for (int index = 0; index < 4; ++index)
          toadd.pSpr[index] = (CSprite) null;
        toadd.inkEffect = hoPtr.ros.rsEffect;
        toadd.inkEffectParam = hoPtr.ros.rsEffectParam;
        this.addBackdrop2(toadd);
      }

      public void addBackdrop2(CBkd2 toadd)
      {
        if (toadd.nLayer < (short) 0 || (int) toadd.nLayer >= this.rhFrame.nLayers)
          return;
        CLayer layer = this.rhFrame.layers[(int) toadd.nLayer];
        if (layer.pBkd2 != null)
        {
          for (int index1 = 0; index1 < layer.pBkd2.size(); ++index1)
          {
            CBkd2 o = (CBkd2) layer.pBkd2.get(index1);
            if (o.x == toadd.x && o.y == toadd.y && (int) o.nLayer == (int) toadd.nLayer && (int) o.img == (int) toadd.img && (o.inkEffect & 4095 /*0x0FFF*/) == 0)
            {
              if (index1 != layer.pBkd2.size() - 1)
              {
                for (int index2 = 0; index2 < 4; ++index2)
                {
                  if (o.pSpr[index2] != null)
                    this.rhApp.spriteGen.moveSpriteToFront(o.pSpr[index2]);
                }
                layer.pBkd2.remove(index1);
                layer.pBkd2.add((object) o);
              }
              o.colMode = toadd.colMode;
              o.obstacleType = toadd.obstacleType;
              if (o.inkEffect == toadd.inkEffect && o.inkEffectParam == toadd.inkEffectParam)
                return;
              o.inkEffect = toadd.inkEffect;
              o.inkEffectParam = toadd.inkEffectParam;
              for (int index3 = 0; index3 < 4; ++index3)
              {
                if (o.pSpr[index3] != null)
                  this.rhApp.spriteGen.modifSpriteEffect(o.pSpr[index3], o.inkEffect, o.inkEffectParam);
              }
              return;
            }
          }
        }
        else
          layer.pBkd2 = new CArrayList();
        int num1 = layer.pBkd2.size();
        layer.pBkd2.add((object) toadd);
        CBkd2 cbkd2 = toadd;
        CRect crect = new CRect();
        int num2 = this.rhFrame.leX;
        int num3 = this.rhFrame.leY;
        bool flag1 = (layer.dwOptions & 32 /*0x20*/) != 0;
        bool flag2 = (layer.dwOptions & 64 /*0x40*/) != 0;
        bool flag3 = false;
        if (flag1 || flag2)
          flag3 = true;
        int leWidth = this.rhFrame.leWidth;
        int leHeight = this.rhFrame.leHeight;
        if ((layer.dwOptions & 3) != 0)
        {
          if ((layer.dwOptions & 1) != 0)
            num2 = (int) ((double) num2 * (double) layer.xCoef);
          if ((layer.dwOptions & 2) != 0)
            num3 = (int) ((double) num3 * (double) layer.yCoef);
        }
        int num4 = num2 + layer.x;
        int num5 = num3 + layer.y;
        if (flag1)
          num4 %= leWidth;
        if (flag2)
          num5 %= leHeight;
        if ((layer.dwOptions & 131088 /*0x020010*/) != 16 /*0x10*/)
          return;
        bool flag4 = (layer.dwOptions & 4) == 0;
        uint num6 = 0;
        int num7 = 0;
        do
        {
          int index = num7;
          crect.left = cbkd2.x - num4;
          crect.top = cbkd2.y - num5;
          int num8 = this.rhFrame.leEditWinWidth - 1;
          int num9 = this.rhFrame.leEditWinHeight - 1;
          short img = cbkd2.img;
          CImage imageFromHandle = this.rhApp.imageBank.getImageFromHandle(img);
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
            switch (num7)
            {
              case 0:
                if (flag1 && (crect.left < 0 || crect.right > leWidth))
                {
                  if (flag2 && (crect.top < 0 || crect.bottom > leHeight))
                  {
                    num7 = 3;
                    num6 |= 7U;
                  }
                  else
                  {
                    num7 = 1;
                    num6 |= 1U;
                  }
                }
                else if (flag2 && (crect.top < 0 || crect.bottom > leHeight))
                {
                  num7 = 2;
                  num6 |= 2U;
                }
                if (((int) num6 & 1) == 0 && cbkd2.pSpr[1] != null)
                {
                  this.rhApp.spriteGen.delSprite(cbkd2.pSpr[1]);
                  cbkd2.pSpr[1] = (CSprite) null;
                }
                if (((int) num6 & 2) == 0 && cbkd2.pSpr[2] != null)
                {
                  this.rhApp.spriteGen.delSprite(cbkd2.pSpr[2]);
                  cbkd2.pSpr[2] = (CSprite) null;
                }
                if (((int) num6 & 4) == 0 && cbkd2.pSpr[3] != null)
                {
                  this.rhApp.spriteGen.delSprite(cbkd2.pSpr[3]);
                  cbkd2.pSpr[3] = (CSprite) null;
                  break;
                }
                break;
              case 1:
                if (crect.left < 0)
                {
                  int num10 = leWidth;
                  crect.left += num10;
                  crect.right += num10;
                }
                else if (crect.right > leWidth)
                {
                  int num11 = leWidth;
                  crect.left -= num11;
                  crect.right -= num11;
                }
                num6 &= 4294967294U;
                num7 = 0;
                if (((int) num6 & 2) != 0)
                {
                  num7 = 2;
                  break;
                }
                break;
              case 2:
                if (crect.top < 0)
                {
                  int num12 = leHeight;
                  crect.top += num12;
                  crect.bottom += num12;
                }
                else if (crect.bottom > leHeight)
                {
                  int num13 = leHeight;
                  crect.top -= num13;
                  crect.bottom -= num13;
                }
                num6 &= 4294967293U;
                num7 = 0;
                if (((int) num6 & 1) != 0)
                {
                  num7 = 1;
                  break;
                }
                break;
              case 3:
                if (crect.left < 0)
                {
                  int num14 = leWidth;
                  crect.left += num14;
                  crect.right += num14;
                }
                else if (crect.right > leWidth)
                {
                  int num15 = leWidth;
                  crect.left -= num15;
                  crect.right -= num15;
                }
                if (crect.top < 0)
                {
                  int num16 = leHeight;
                  crect.top += num16;
                  crect.bottom += num16;
                }
                else if (crect.bottom > leHeight)
                {
                  int num17 = leHeight;
                  crect.top -= num17;
                  crect.bottom -= num17;
                }
                num6 &= 4294967291U;
                num7 = 2;
                break;
            }
          }
          if (this.rhFrame.colMask != null && cbkd2.nLayer == (short) 0 && cbkd2.colMode != (short) 4 && crect.right >= -96 && crect.bottom >= -16)
          {
            crect.left += num4;
            crect.top += num5;
            crect.right += num4;
            crect.bottom += num5;
            int val = 0;
            if (cbkd2.colMode == (short) 1)
              val = 3;
            CMask mask = this.rhApp.imageBank.getImageFromHandle(toadd.img).getMask(0, 0, 1f, 1f);
            if (cbkd2.obstacleType == (short) 0)
              this.rhFrame.colMask.fillRectangle(crect.left, crect.top, crect.right - 1, crect.bottom - 1, val);
            else if (mask == null)
              this.rhFrame.colMask.fillRectangle(crect.left, crect.top, crect.right - 1, crect.bottom - 1, val);
            else
              this.rhFrame.colMask.orMask(mask, crect.left, crect.top, 3, val);
            if (cbkd2.colMode == (short) 2)
            {
              if (cbkd2.obstacleType == (short) 0)
                this.rhFrame.colMask.fillRectangle(crect.left, crect.top, crect.right - 1, Math.Min(crect.top + 6, crect.bottom) - 1, 2);
              else if (mask == null)
                this.rhFrame.colMask.fillRectangle(crect.left, crect.top, crect.right - 1, Math.Min(crect.top + 6, crect.bottom) - 1, 2);
              else
                this.rhFrame.colMask.orPlatformMask(mask, crect.left, crect.top);
            }
            crect.left -= num4;
            crect.top -= num5;
            crect.right -= num4;
            crect.bottom -= num5;
          }
          if (crect.left < num8 + 64 /*0x40*/ + 32 /*0x20*/ && crect.top < num9 + 16 /*0x10*/)
          {
            int obstacleType = (int) cbkd2.obstacleType;
            if (obstacleType == 3)
              this.y_Ladder_Add((int) cbkd2.nLayer, crect.left, crect.top, crect.right, crect.bottom);
            if (crect.left <= num8 && crect.top <= num9 && crect.right >= 0 && crect.bottom >= 0)
            {
              uint num18 = 524296 /*0x080008*/;
              if (!flag4)
                num18 |= 512U /*0x0200*/;
              if (cbkd2.nLayer > (short) 0)
              {
                if (obstacleType == 1)
                  num18 |= 65537U /*0x010001*/;
                if (obstacleType == 2)
                  num18 |= 131073U /*0x020001*/;
              }
              this.rhApp.imageBank.getImageFromHandle(toadd.img);
              int left = crect.left;
              int top = crect.top;
              uint sFlags = num18 | 4194304U /*0x400000*/;
              cbkd2.pSpr[index] = this.rhApp.spriteGen.addSprite(left, top, img, cbkd2.nLayer, 268435456 /*0x10000000*/ + num1 * 4 + index, 0, sFlags, (CObject) null);
              this.rhApp.spriteGen.modifSpriteEffect(cbkd2.pSpr[index], cbkd2.inkEffect, cbkd2.inkEffectParam);
            }
          }
        }
        while (num6 != 0U);
      }

      public void deleteAllBackdrop2(int nLayer)
      {
        if (nLayer < 0 || nLayer >= this.rhFrame.nLayers)
          return;
        CLayer layer = this.rhFrame.layers[nLayer];
        if (layer.pBkd2 == null)
          return;
        for (int index1 = 0; index1 < layer.pBkd2.size(); ++index1)
        {
          CBkd2 cbkd2 = (CBkd2) layer.pBkd2.get(index1);
          for (int index2 = 0; index2 < 4; ++index2)
          {
            if (cbkd2.pSpr[index2] != null)
            {
              this.rhApp.spriteGen.delSprite(cbkd2.pSpr[index2]);
              cbkd2.pSpr[index2] = (CSprite) null;
            }
          }
        }
        layer.pBkd2 = (CArrayList) null;
        layer.dwOptions |= 65536 /*0x010000*/;
        this.rh3Scrolling |= (byte) 2;
      }

      public void deleteBackdrop2At(int nLayer, int x, int y, bool bFineDetection)
      {
        if (nLayer < 0 || nLayer >= this.rhFrame.nLayers)
          return;
        CLayer layer = this.rhFrame.layers[nLayer];
        if (layer.pBkd2 == null)
          return;
        bool flag1 = false;
        bool flag2 = (layer.dwOptions & 32 /*0x20*/) != 0;
        bool flag3 = (layer.dwOptions & 64 /*0x40*/) != 0;
        bool flag4 = flag2 | flag3;
        int leWidth = this.rhFrame.leWidth;
        int leHeight = this.rhFrame.leHeight;
        int num1 = this.rhFrame.leX;
        int num2 = this.rhFrame.leY;
        if ((layer.dwOptions & 3) != 0)
        {
          if ((layer.dwOptions & 1) != 0)
            num1 = (int) ((double) num1 * (double) layer.xCoef);
          if ((layer.dwOptions & 2) != 0)
            num2 = (int) ((double) num2 * (double) layer.yCoef);
        }
        int num3 = num1 + layer.x;
        int num4 = num2 + layer.y;
        if (flag2)
          num3 %= leWidth;
        if (flag3)
          num4 %= leHeight;
        uint num5 = 0;
        int num6 = 0;
        for (int index1 = 0; index1 < layer.pBkd2.size(); ++index1)
        {
          CBkd2 cbkd2 = (CBkd2) layer.pBkd2.get(index1);
          if ((int) cbkd2.nLayer == nLayer)
          {
            bool flag5 = false;
            CRect crect = new CRect();
            bool flag6 = cbkd2.colMode == (short) 0;
            crect.left = cbkd2.x - num3;
            crect.top = cbkd2.y - num4;
            CImage imageFromHandle = this.rhApp.imageBank.getImageFromHandle(cbkd2.img);
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
            if (flag4)
            {
              switch (num6)
              {
                case 0:
                  if (flag2 && (crect.left < 0 || crect.right > leWidth))
                  {
                    if (flag3 && (crect.top < 0 || crect.bottom > leHeight))
                    {
                      num6 = 3;
                      num5 |= 7U;
                      break;
                    }
                    num6 = 1;
                    num5 |= 1U;
                    break;
                  }
                  if (flag3 && (crect.top < 0 || crect.bottom > leHeight))
                  {
                    num6 = 2;
                    num5 |= 2U;
                    break;
                  }
                  break;
                case 1:
                  if (crect.left < 0)
                  {
                    int num7 = leWidth;
                    crect.left += num7;
                    crect.right += num7;
                  }
                  else if (crect.right > leWidth)
                  {
                    int num8 = leWidth;
                    crect.left -= num8;
                    crect.right -= num8;
                  }
                  num5 &= 4294967294U;
                  num6 = 0;
                  if (((int) num5 & 2) != 0)
                  {
                    num6 = 2;
                    break;
                  }
                  break;
                case 2:
                  if (crect.top < 0)
                  {
                    int num9 = leHeight;
                    crect.top += num9;
                    crect.bottom += num9;
                  }
                  else if (crect.bottom > leHeight)
                  {
                    int num10 = leHeight;
                    crect.top -= num10;
                    crect.bottom -= num10;
                  }
                  num5 &= 4294967293U;
                  num6 = 0;
                  if (((int) num5 & 1) != 0)
                  {
                    num6 = 1;
                    break;
                  }
                  break;
                case 3:
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
                  num5 &= 4294967291U;
                  num6 = 2;
                  break;
              }
            }
            if (x >= crect.left && y >= crect.top && x < crect.right && y < crect.bottom)
            {
              if (!bFineDetection || flag6)
              {
                flag5 = true;
              }
              else
              {
                CMask mask = this.rhApp.imageBank.getImageFromHandle(cbkd2.img).getMask(0, 0, 1f, 1f);
                if (mask != null && mask.testPoint(x - crect.left, y - crect.top))
                  flag5 = true;
              }
            }
            if (flag5)
            {
              flag1 = true;
              for (int index2 = 0; index2 < 4; ++index2)
              {
                if (cbkd2.pSpr[index2] != null)
                {
                  this.rhApp.spriteGen.delSprite(cbkd2.pSpr[index2]);
                  cbkd2.pSpr[index2] = (CSprite) null;
                }
              }
              layer.pBkd2.remove(index1);
              num5 = 0U;
              --index1;
            }
            if (num5 != 0U)
              --index1;
          }
        }
        if (!flag1)
          return;
        layer.dwOptions |= 65536 /*0x010000*/;
        this.rh3Scrolling |= (byte) 2;
      }

      public void displayBkd2Layer(
        CLayer pLayer,
        int nLayer,
        int flags,
        int x2edit,
        int y2edit,
        bool flgColMaskEmpty)
      {
        CRect crect = new CRect();
        bool flag1 = (this.rhFrame.leFlags & 32 /*0x20*/) != 0;
        bool flag2 = (flags & 16 /*0x10*/) == 0;
        int num1 = this.rhFrame.leX;
        int num2 = this.rhFrame.leY;
        bool flag3 = (pLayer.dwOptions & 32 /*0x20*/) != 0;
        bool flag4 = (pLayer.dwOptions & 64 /*0x40*/) != 0;
        bool flag5 = flag3 | flag4;
        int leWidth = this.rhFrame.leWidth;
        int leHeight = this.rhFrame.leHeight;
        if ((pLayer.dwOptions & 3) != 0)
        {
          if ((pLayer.dwOptions & 1) != 0)
            num1 = (int) ((double) num1 * (double) pLayer.xCoef);
          if ((pLayer.dwOptions & 2) != 0)
            num2 = (int) ((double) num2 * (double) pLayer.yCoef);
        }
        int num3 = num1 + pLayer.x;
        int num4 = num2 + pLayer.y;
        if (flag3)
          num3 %= leWidth;
        if (flag4)
          num4 %= leHeight;
        if ((pLayer.dwOptions & 131072 /*0x020000*/) != 0)
        {
          for (int index1 = 0; index1 < pLayer.pBkd2.size(); ++index1)
          {
            CBkd2 cbkd2 = (CBkd2) pLayer.pBkd2.get(index1);
            for (int index2 = 0; index2 < 4; ++index2)
            {
              if (cbkd2.pSpr[index2] != null)
              {
                this.rhApp.spriteGen.delSprite(cbkd2.pSpr[index2]);
                cbkd2.pSpr[index2] = (CSprite) null;
              }
            }
          }
        }
        if ((pLayer.dwOptions & 131072 /*0x020000*/) != 0)
          return;
        bool flag6 = (pLayer.dwOptions & 4) == 0;
        uint num5 = 0;
        int num6 = 0;
        for (int index3 = 0; index3 < pLayer.pBkd2.size(); ++index3)
        {
          CBkd2 cbkd2 = (CBkd2) pLayer.pBkd2.get(index3);
          int index4 = num6;
          crect.left = cbkd2.x - num3;
          crect.top = cbkd2.y - num4;
          if (!flag1 && !flag5 && (crect.left >= x2edit + 64 /*0x40*/ + 32 /*0x20*/ || crect.top >= y2edit + 16 /*0x10*/))
          {
            if (cbkd2.pSpr[index4] != null)
            {
              this.rhApp.spriteGen.delSprite(cbkd2.pSpr[index4]);
              cbkd2.pSpr[index4] = (CSprite) null;
            }
          }
          else
          {
            short img = cbkd2.img;
            CImage imageFromHandle = this.rhApp.imageBank.getImageFromHandle(img);
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
            if (flag5)
            {
              switch (num6)
              {
                case 0:
                  if (flag3 && (crect.left < 0 || crect.right > leWidth))
                  {
                    if (flag4 && (crect.top < 0 || crect.bottom > leHeight))
                    {
                      num6 = 3;
                      num5 |= 7U;
                    }
                    else
                    {
                      num6 = 1;
                      num5 |= 1U;
                    }
                  }
                  else if (flag4 && (crect.top < 0 || crect.bottom > leHeight))
                  {
                    num6 = 2;
                    num5 |= 2U;
                  }
                  if (((int) num5 & 1) == 0 && cbkd2.pSpr[1] != null)
                  {
                    this.rhApp.spriteGen.delSprite(cbkd2.pSpr[1]);
                    cbkd2.pSpr[1] = (CSprite) null;
                  }
                  if (((int) num5 & 2) == 0 && cbkd2.pSpr[2] != null)
                  {
                    this.rhApp.spriteGen.delSprite(cbkd2.pSpr[2]);
                    cbkd2.pSpr[2] = (CSprite) null;
                  }
                  if (((int) num5 & 4) == 0 && cbkd2.pSpr[3] != null)
                  {
                    this.rhApp.spriteGen.delSprite(cbkd2.pSpr[3]);
                    cbkd2.pSpr[3] = (CSprite) null;
                    break;
                  }
                  break;
                case 1:
                  if (crect.left < 0)
                  {
                    int num7 = leWidth;
                    crect.left += num7;
                    crect.right += num7;
                  }
                  else if (crect.right > leWidth)
                  {
                    int num8 = leWidth;
                    crect.left -= num8;
                    crect.right -= num8;
                  }
                  num5 &= 4294967294U;
                  num6 = 0;
                  if (((int) num5 & 2) != 0)
                  {
                    num6 = 2;
                    break;
                  }
                  break;
                case 2:
                  if (crect.top < 0)
                  {
                    int num9 = leHeight;
                    crect.top += num9;
                    crect.bottom += num9;
                  }
                  else if (crect.bottom > leHeight)
                  {
                    int num10 = leHeight;
                    crect.top -= num10;
                    crect.bottom -= num10;
                  }
                  num5 &= 4294967293U;
                  num6 = 0;
                  if (((int) num5 & 1) != 0)
                  {
                    num6 = 1;
                    break;
                  }
                  break;
                case 3:
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
                  num5 &= 4294967291U;
                  num6 = 2;
                  break;
              }
            }
            int obstacleType = (int) cbkd2.obstacleType;
            bool flag7 = cbkd2.colMode == (short) 0;
            if (obstacleType == 3)
            {
              this.y_Ladder_Add(nLayer, crect.left, crect.top, crect.right, crect.bottom);
              flag7 = true;
            }
            if (nLayer == 0 && flag2 && obstacleType != 4 && (flag1 || crect.right >= -96 && crect.bottom >= -16))
            {
              if (flag1)
              {
                crect.left += num3;
                crect.top += num4;
                crect.right += num3;
                crect.bottom += num4;
              }
              int val = 0;
              if (obstacleType == 1)
              {
                val = 3;
                flgColMaskEmpty = false;
              }
              if (!flgColMaskEmpty)
              {
                if (flag7)
                  this.rhFrame.colMask.fillRectangle(crect.left, crect.top, crect.right, crect.bottom, val);
                else
                  this.rhFrame.colMask.orMask(this.rhApp.imageBank.getImageFromHandle(img).getMask(0, 0, 1f, 1f), crect.left, crect.top, 3, val);
              }
              if (obstacleType == 2)
              {
                flgColMaskEmpty = false;
                if (flag7)
                  this.rhFrame.colMask.fillRectangle(crect.left, crect.top, crect.right, Math.Min(crect.top + 6, crect.bottom), 2);
                else
                  this.rhFrame.colMask.orPlatformMask(this.rhApp.imageBank.getImageFromHandle(img).getMask(0, 0, 1f, 1f), crect.left, crect.top);
              }
              if (flag1)
              {
                crect.left -= num3;
                crect.top -= num4;
                crect.right -= num3;
                crect.bottom -= num4;
              }
            }
            if (crect.left <= x2edit && crect.top <= y2edit && crect.right >= 0 && crect.bottom >= 0)
            {
              uint sFlags = 4718600;
              if (!flag6)
                sFlags |= 512U /*0x0200*/;
              if (obstacleType == 1)
                sFlags |= 65537U /*0x010001*/;
              if (obstacleType == 2)
                sFlags |= 131073U /*0x020001*/;
              if (cbkd2.pSpr[index4] == null)
              {
                cbkd2.pSpr[index4] = this.rhApp.spriteGen.addSprite(crect.left, crect.top, img, cbkd2.nLayer, 268435456 /*0x10000000*/ + index3 * 4 + index4, 0, sFlags, (CObject) null);
                this.rhApp.spriteGen.modifSpriteEffect(cbkd2.pSpr[index4], cbkd2.inkEffect, cbkd2.inkEffectParam);
              }
              else
                this.rhApp.spriteGen.modifSprite(cbkd2.pSpr[index4], crect.left, crect.top, img);
            }
            else if (cbkd2.pSpr[index4] != null)
            {
              this.rhApp.spriteGen.delSprite(cbkd2.pSpr[index4]);
              cbkd2.pSpr[index4] = (CSprite) null;
            }
            if (num5 != 0U)
              --index3;
          }
        }
      }

      public void f_InitLoop()
      {
        long timer = this.rhApp.timer;
        this.rhTimerOld = timer;
        this.rhTimerFPSOld = timer;
        this.rhTimer = 0L;
        this.rhLoopCount = 0;
        this.rh4LoopTheoric = 0;
        this.rhVBLOld = this.rhApp.newGetCptVbl() - 1;
        this.rh4VBLDelta = 0;
        this.rhQuit = (short) 0;
        this.rhQuitBis = (short) 0;
        this.rhDestroyPos = (short) 0;
        for (int index = 0; index < (this.rhMaxObjects + 31 /*0x1F*/) / 32 /*0x20*/; ++index)
          this.rhDestroyList[index] = 0;
        this.rh3WindowSx = this.rhFrame.leEditWinWidth;
        this.rh3WindowSy = this.rhFrame.leEditWinHeight;
        this.rh3XMinimumKill = -480;
        this.rh3YMinimumKill = -300;
        this.rh3XMaximumKill = this.rhLevelSx + 480;
        this.rh3YMaximumKill = this.rhLevelSy + 300;
        int rhWindowX = this.rhWindowX;
        this.rh3DisplayX = rhWindowX;
        int num1 = rhWindowX - 64 /*0x40*/;
        if (num1 < 0)
          num1 = this.rh3XMinimumKill;
        this.rh3XMinimum = num1;
        int rhWindowY = this.rhWindowY;
        this.rh3DisplayY = rhWindowY;
        int num2 = rhWindowY - 16 /*0x10*/;
        if (num2 < 0)
          num2 = this.rh3YMinimumKill;
        this.rh3YMinimum = num2;
        int num3 = this.rhWindowX + (this.rh3WindowSx + 64 /*0x40*/);
        if (num3 > this.rhLevelSx)
          num3 = this.rh3XMaximumKill;
        this.rh3XMaximum = num3;
        int num4 = this.rhWindowY + (this.rh3WindowSy + 16 /*0x10*/);
        if (num4 > this.rhLevelSy)
          num4 = this.rh3YMaximumKill;
        this.rh3YMaximum = num4;
        this.rh3Scrolling = (byte) 0;
        this.rh4DoUpdate = (byte) 0;
        this.rh4EventCount = 0;
        this.rh4TimeOut = 0L;
        this.rh2PauseCompteur = 0;
        this.rh4FakeKey = (short) 0;
        for (int index = 0; index < 4; ++index)
        {
          this.rhPlayer[index] = (byte) 0;
          this.rh2OldPlayer[index] = (byte) 0;
          this.rh2InputMask[index] = byte.MaxValue;
        }
        this.rh2MouseKeys = (byte) 0;
        this.oldMouseKey = -1;
        this.toucheID = -1;
        this.mouseKeyTime = 0L;
        if (this.rhMouseUsed != (byte) 0)
        {
          this.rh4MouseXCenter = this.rhApp.gaCxWin / 2;
          this.rh4MouseYCenter = this.rhApp.gaCyWin / 2;
          Mouse.SetPosition(this.rh4MouseXCenter, this.rh4MouseYCenter);
        }
        this.rhEvtProg.rh2ActionEndRoutine = false;
        this.rh4OnCloseCount = -1;
        this.rh4EndOfPause = -1;
        this.rhEvtProg.rh4CheckDoneInstart = false;
        this.rh4PauseKey = Keys.None;
        this.bCheckResume = false;
        this.rhApp.soundPlayer.reset();
        for (int index = 0; index < 10; ++index)
          this.rh4FrameRateArray[index] = 20;
        this.rh4FrameRatePos = 0;
        for (int index = 0; index < this.rhApp.numberOfTouches; ++index)
          this.cancelledTouches[index] = -1;
        if (this.rhEvtProg.bTestAllKeys)
        {
          int index = 0;
          while (!this.keyboardState.IsKeyDown(CKeyConvert.xnaKeys[index]))
          {
            ++index;
            if (CKeyConvert.pcKeys[index] < 0)
              goto label_27;
          }
          this.bAnyKeyDown = true;
        }
    label_27:
        if (this.rhApp.parentApp == null)
        {
          if (this.rhFrame.joystick == (short) 3)
          {
            if (this.joystick == null)
              this.joystick = new CJoystick(this.rhApp);
            else
              this.joystick.reset(0);
          }
          else
          {
            int f = 0;
            if (((int) this.rhFrame.iPhoneOptions & 1) != 0)
              f = 2;
            if (((int) this.rhFrame.iPhoneOptions & 2) != 0)
              f |= 4;
            if (((int) this.rhFrame.iPhoneOptions & 4) != 0)
              f |= 8;
            if (this.rhFrame.joystick == (short) 1)
              f |= 1;
            if ((f & 7) != 0)
            {
              if (this.joystick == null)
                this.joystick = new CJoystick(this.rhApp);
              this.joystick.reset(f);
            }
            else
              this.joystick = (CJoystick) null;
            if (this.rhFrame.joystick == (short) 2)
              this.startJoystickAcc();
            else
              this.stopJoystickAcc();
          }
        }
        this.rhJoystickMask = byte.MaxValue;
      }

      public void handleFrameRaate()
      {
        long timer = this.rhApp.timer;
        long num = timer - this.rhTimerFPSOld;
        this.rhTimerFPSOld = timer;
        this.rh4FrameRateArray[this.rh4FrameRatePos] = (int) num;
        ++this.rh4FrameRatePos;
        if (this.rh4FrameRatePos < 10)
          return;
        this.rh4FrameRatePos = 0;
      }

      public int f_GameLoop()
      {
        this.keyboardState = Keyboard.GetState();
        if (this.rh2PauseCompteur != 0)
        {
          if (this.questionObjectOn != null)
            this.questionObjectOn.handleQuestion();
          return 0;
        }
        this.rhApp.soundPlayer.checkSounds();
        long num1 = this.rhApp.timer - this.rhTimerOld;
        long rhTimer = this.rhTimer;
        this.rhTimer = num1;
        long num2 = num1 - rhTimer;
        this.rhTimerDelta = (int) num2;
        this.rh4TimeOut += num2;
        ++this.rhLoopCount;
        this.rh4MvtTimerCoef = (double) this.rhTimerDelta * (double) this.rhFrame.m_dwMvtTimerBase / 1000.0;
        for (int index = 0; index < 4; ++index)
          this.rh2OldPlayer[index] = this.rhPlayer[index];
        if (this.joystick != null)
          this.rhPlayer[0] = (byte) ((uint) this.joystick.getJoystick() & (uint) this.rhJoystickMask);
        if (this.joystickAcc != null)
          this.rhPlayer[0] |= this.joystickAcc.getJoystick();
        if (this.rhMouseUsed != (byte) 0)
        {
          byte num3 = 0;
          if (((int) this.rh2MouseKeys & 1) != 0)
            num3 |= (byte) 16 /*0x10*/;
          if (((int) this.rh2MouseKeys & 2) != 0)
            num3 |= (byte) 32 /*0x20*/;
          byte rhMouseUsed = this.rhMouseUsed;
          for (int index = 0; index < this.rhNPlayers; ++index)
          {
            if (((int) rhMouseUsed & 1) != 0)
            {
              byte num4 = (byte) ((uint) (byte) ((uint) this.rhPlayer[index] & 207U) | (uint) num3);
              this.rhPlayer[index] = num4;
            }
            rhMouseUsed >>= 1;
          }
        }
        for (int index = 0; index < 4; ++index)
        {
          byte num5 = (byte) ((uint) (byte) ((uint) this.rhPlayer[index] & (uint) this.plMasks[this.rhNPlayers * 4 + index]) & (uint) this.rh2InputMask[index]);
          this.rhPlayer[index] = num5;
          byte num6 = (byte) ((uint) num5 ^ (uint) this.rh2OldPlayer[index]);
          this.rh2NewPlayer[index] = num6;
          if (num6 != (byte) 0)
          {
            if (!this.bMouseControlled && index == 0)
              this.newKey();
            byte num7 = (byte) ((uint) num6 & (uint) this.rhPlayer[index]);
            if (((int) num7 & 240 /*0xF0*/) != 0)
            {
              this.rhEvtProg.rhCurOi = (short) index;
              if (((int) num7 & 240 /*0xF0*/) != 0)
              {
                this.rhEvtProg.rhCurParam0 = (int) num7;
                this.rhEvtProg.handle_GlobalEvents(-196615);
              }
              if (((int) num7 & 15) != 0)
              {
                this.rhEvtProg.rhCurParam0 = (int) num7;
                this.rhEvtProg.handle_GlobalEvents(-196615);
              }
            }
            else
            {
              int listPointer = this.rhEvtProg.listPointers[this.rhEvtProg.rhEvents[7] + 4];
              if (listPointer != 0)
              {
                this.rhEvtProg.rhCurParam0 = (int) num7;
                this.rhEvtProg.computeEventList(listPointer, (CObject) null);
              }
            }
          }
        }
        if (this.rhNObjects != 0)
        {
          int num8 = this.rhNObjects;
          int index = 0;
          do
          {
            this.rh4ObjectAddCreate = 0;
            while (this.rhObjectList[index] == null)
              ++index;
            CObject rhObject = this.rhObjectList[index];
            rhObject.hoPrevNoRepeat = rhObject.hoBaseNoRepeat;
            rhObject.hoBaseNoRepeat = (CArrayList) null;
            if (rhObject.hoCallRoutine)
            {
              this.rh4ObjectCurCreate = index;
              rhObject.handle();
            }
            int num9 = num8 + this.rh4ObjectAddCreate;
            ++index;
            num8 = num9 - 1;
          }
          while (num8 != 0);
        }
        ++this.rh3CollisionCount;
        this.rhEvtProg.compute_TimerEvents();
        if (this.rhEvtProg.rhEventAlways && ((int) this.rhGameFlags & 16 /*0x10*/) == 0)
          this.rhEvtProg.computeEventList(0, (CObject) null);
        this.rhEvtProg.handle_PushedEvents();
        this.modif_ChangedObjects();
        this.destroy_List();
        this.rhEvtProg.rh2CurrentClick = (short) -1;
        this.rhEvtProg.rh3CurrentMenu = 0;
        ++this.rh4EventCount;
        this.rh4FakeKey = (short) 0;
        if (this.rhQuit == (short) 0)
          return (int) this.rhQuitBis;
        if (this.rhQuit == (short) 1 || this.rhQuit == (short) 2 || this.rhQuit == (short) -2 || this.rhQuit == (short) 3 || this.rhQuit == (short) 100 || this.rhQuit == (short) 4)
          this.rhEvtProg.handle_GlobalEvents(-65539);
        return (int) this.rhQuit;
      }

      public void modif_ChangedObjects()
      {
        int index1 = 0;
        for (int index2 = 0; index2 < this.rhNObjects; ++index2)
        {
          while (this.rhObjectList[index1] == null)
            ++index1;
          CObject rhObject = this.rhObjectList[index1];
          ++index1;
          if ((rhObject.hoOEFlags & 560) != 0 && rhObject.roc.rcChanged)
          {
            rhObject.modif();
            rhObject.roc.rcChanged = false;
          }
        }
      }

      public void draw()
      {
        if (((int) this.rhGameFlags & 16 /*0x10*/) != 0 || this.rhApp.parentApp != null)
          return;
        this.screen_Update();
      }

      private void joyTest()
      {
        GamePadState[] gamePadStateArray = new GamePadState[4];
        for (int index = 0; index < 4; ++index)
        {
          this.rhPlayer[index] = (byte) 0;
          switch (index)
          {
            case 0:
              gamePadStateArray[index] = GamePad.GetState(PlayerIndex.One);
              break;
            case 1:
              gamePadStateArray[index] = GamePad.GetState(PlayerIndex.Two);
              break;
            case 2:
              gamePadStateArray[index] = GamePad.GetState(PlayerIndex.Three);
              break;
            case 3:
              gamePadStateArray[index] = GamePad.GetState(PlayerIndex.Four);
              break;
          }
        }
        short[] ctrlType = this.rhApp.getCtrlType();
        Keys[] ctrlKeys = this.rhApp.getCtrlKeys();
        for (int index1 = 0; index1 < 4; ++index1)
        {
          short num = ctrlType[index1];
          if (num != (short) 5)
          {
            for (int index2 = 0; index2 < 4; ++index2)
            {
              if (((int) num & 1 << index2) != 0)
              {
                if (gamePadStateArray[index2].DPad.Left == ButtonState.Pressed)
                  this.rhPlayer[index1] |= (byte) 4;
                if (gamePadStateArray[index2].DPad.Right == ButtonState.Pressed)
                  this.rhPlayer[index1] |= (byte) 8;
                if (gamePadStateArray[index2].DPad.Up == ButtonState.Pressed)
                  this.rhPlayer[index1] |= (byte) 1;
                if (gamePadStateArray[index2].DPad.Down == ButtonState.Pressed)
                  this.rhPlayer[index1] |= (byte) 2;
                if ((double) gamePadStateArray[index2].ThumbSticks.Left.X < -0.5)
                  this.rhPlayer[index1] |= (byte) 4;
                if ((double) gamePadStateArray[index2].ThumbSticks.Left.X > 0.5)
                  this.rhPlayer[index1] |= (byte) 8;
                if ((double) gamePadStateArray[index2].ThumbSticks.Left.Y > 0.5)
                  this.rhPlayer[index1] |= (byte) 1;
                if ((double) gamePadStateArray[index2].ThumbSticks.Left.Y < -0.5)
                  this.rhPlayer[index1] |= (byte) 2;
                if (gamePadStateArray[index2].Buttons.A == ButtonState.Pressed)
                  this.rhPlayer[index1] |= (byte) 16 /*0x10*/;
                if (gamePadStateArray[index2].Buttons.B == ButtonState.Pressed)
                  this.rhPlayer[index1] |= (byte) 32 /*0x20*/;
                if (gamePadStateArray[index2].Buttons.X == ButtonState.Pressed)
                  this.rhPlayer[index1] |= (byte) 64 /*0x40*/;
                if (gamePadStateArray[index2].Buttons.Y == ButtonState.Pressed)
                  this.rhPlayer[index1] |= (byte) 128 /*0x80*/;
              }
            }
          }
          else
          {
            for (int index3 = 0; index3 < 8; ++index3)
            {
              if (this.isKeyDown(ctrlKeys[index1 * 4 + index3]))
                this.rhPlayer[index1] |= (byte) (1 << index3);
            }
          }
        }
      }

      public bool isKeyDown(Keys key)
      {
        return this.keyboardState.IsKeyDown(key) || key == Keys.LeftShift && this.keyboardState.IsKeyDown(Keys.RightShift) || key == Keys.LeftControl && this.keyboardState.IsKeyDown(Keys.RightControl);
      }

      private void getMouseCoords()
      {
        this.rh2MouseX = this.mouseX + this.rhWindowX;
        this.rh2MouseY = this.mouseY + this.rhWindowY;
        if (this.rhApp.parentApp != null)
        {
          this.rh2MouseX -= this.rhApp.xOffset;
          this.rh2MouseY -= this.rhApp.yOffset;
        }
        this.rh2MouseKeys = (byte) 0;
        if (this.mouseKey != 0)
          return;
        this.rh2MouseKeys |= (byte) 1;
      }

      public bool newHandle_Collisions(CObject pHo)
      {
        pHo.rom.rmMoveFlag = false;
        pHo.rom.rmEventFlags = (short) 0;
        CRun.bMoveChanged = false;
        if (((int) pHo.hoLimitFlags & 1024 /*0x0400*/) != 0)
        {
          int num1 = this.quadran_In(pHo.roc.rcOldX1, pHo.roc.rcOldY1, pHo.roc.rcOldX2, pHo.roc.rcOldY2);
          if (num1 != 0)
          {
            int num2 = this.quadran_In(pHo.hoX - pHo.hoImgXSpot, pHo.hoY - pHo.hoImgYSpot, pHo.hoX - pHo.hoImgXSpot + pHo.hoImgWidth, pHo.hoY - pHo.hoImgYSpot + pHo.hoImgHeight);
            if (num2 == 0)
            {
              int num3 = num1 ^ num2;
              if (num3 != 0)
              {
                pHo.rom.rmEventFlags |= (short) 1;
                this.rhEvtProg.rhCurParam0 = num3;
                this.rhEvtProg.handle_Event(pHo, -720896 | (int) pHo.hoType & (int) ushort.MaxValue);
              }
            }
          }
          int num4 = this.quadran_In(pHo.hoX - pHo.hoImgXSpot, pHo.hoY - pHo.hoImgYSpot, pHo.hoX - pHo.hoImgXSpot + pHo.hoImgWidth, pHo.hoY - pHo.hoImgYSpot + pHo.hoImgHeight);
          if ((num4 & (int) pHo.rom.rmWrapping) != 0)
          {
            if ((num4 & 1) != 0)
              pHo.rom.rmMovement.setXPosition(pHo.hoX + this.rhLevelSx);
            else if ((num4 & 2) != 0)
              pHo.rom.rmMovement.setXPosition(pHo.hoX - this.rhLevelSx);
            if ((num4 & 4) != 0)
              pHo.rom.rmMovement.setYPosition(pHo.hoY + this.rhLevelSy);
            else if ((num4 & 8) != 0)
              pHo.rom.rmMovement.setYPosition(pHo.hoY - this.rhLevelSy);
          }
          int num5 = this.quadran_Out(pHo.roc.rcOldX1, pHo.roc.rcOldY1, pHo.roc.rcOldX2, pHo.roc.rcOldY2);
          if (num5 != 15)
          {
            int num6 = this.quadran_Out(pHo.hoX - pHo.hoImgXSpot, pHo.hoY - pHo.hoImgYSpot, pHo.hoX - pHo.hoImgXSpot + pHo.hoImgWidth, pHo.hoY - pHo.hoImgYSpot + pHo.hoImgHeight);
            int num7 = ~num5 & num6;
            if (num7 != 0)
            {
              pHo.rom.rmEventFlags |= (short) 2;
              this.rhEvtProg.rhCurParam0 = num7;
              this.rhEvtProg.handle_Event(pHo, -786432 | (int) pHo.hoType & (int) ushort.MaxValue);
            }
          }
        }
        if (((int) pHo.hoLimitFlags & 512 /*0x0200*/) != 0)
        {
          if (pHo.roc.rcMovementType == 9)
          {
            ((CMovePlatform) pHo.rom.rmMovement).mpHandle_Background();
          }
          else
          {
            int code = this.colMask_TestObject_IXY(pHo, pHo.roc.rcImage, pHo.roc.rcAngle, pHo.roc.rcScaleX, pHo.roc.rcScaleY, pHo.hoX, pHo.hoY, 0, 1);
            if (code != 0)
              this.rhEvtProg.handle_Event(pHo, code);
          }
        }
        if (((int) pHo.hoLimitFlags & 128 /*0x80*/) != 0)
        {
          CArrayList carrayList = this.objectAllCol_IXY(pHo, pHo.roc.rcImage, pHo.roc.rcAngle, pHo.roc.rcScaleX, pHo.roc.rcScaleY, pHo.hoX, pHo.hoY, pHo.hoOiList.oilColList);
          if (carrayList != null)
          {
            for (int index = 0; index < carrayList.size(); ++index)
            {
              CObject cobject1 = (CObject) carrayList.get(index);
              if (((int) cobject1.hoFlags & 1) == 0)
              {
                short hoType = pHo.hoType;
                CObject pHo1 = pHo;
                CObject cobject2 = cobject1;
                if ((int) pHo1.hoType > (int) cobject2.hoType)
                {
                  pHo1 = cobject1;
                  cobject2 = pHo;
                  hoType = pHo1.hoType;
                }
                this.rhEvtProg.rhCurParam0 = (int) cobject2.hoOi;
                this.rhEvtProg.rh1stObjectNumber = cobject2.hoNumber;
                this.rhEvtProg.handle_Event(pHo1, -917504 | (int) hoType & (int) ushort.MaxValue);
              }
            }
          }
        }
        return CRun.bMoveChanged;
      }

      public CArrayList objectAllCol_IXY(
        CObject pHo,
        short newImg,
        int newAngle,
        float newScaleX,
        float newScaleY,
        int newX,
        int newY,
        short[] pOiColList)
      {
        CArrayList carrayList = (CArrayList) null;
        int x1 = newX - pHo.hoImgXSpot;
        int num1 = x1 + pHo.hoImgWidth;
        int y1 = newY - pHo.hoImgYSpot;
        int num2 = y1 + pHo.hoImgHeight;
        if (((int) pHo.hoFlags & 8192 /*0x2000*/) != 0)
          return carrayList;
        bool flag = false;
        CMask cmask = (CMask) null;
        int num3 = -1;
        if (pHo.hoType == (short) 2)
        {
          CSprite rcSprite = pHo.roc.rcSprite;
          if (rcSprite != null && ((int) rcSprite.sprFlags & 256 /*0x0100*/) == 0)
            flag = true;
          num3 = (int) pHo.ros.rsLayer;
        }
        short hoFlags = pHo.hoFlags;
        pHo.hoFlags |= (short) 8192 /*0x2000*/;
        int index1 = 0;
        if (pOiColList != null)
        {
          for (int index2 = 0; index2 < pOiColList.Length; index2 += 2)
          {
            int index3 = (int) this.rhOiList[(int) pOiColList[index2 + 1]].oilObject;
            while (index3 >= 0)
            {
              CObject rhObject = this.rhObjectList[index3];
              index3 = (int) rhObject.hoNumNext;
              if (((int) rhObject.hoFlags & 8192 /*0x2000*/) == 0)
              {
                int x2 = rhObject.hoX - rhObject.hoImgXSpot;
                int y2 = rhObject.hoY - rhObject.hoImgYSpot;
                if (x2 < num1 && x2 + rhObject.hoImgWidth > x1 && y2 < num2 && y2 + rhObject.hoImgHeight > y1)
                {
                  switch (rhObject.hoType)
                  {
                    case 2:
                      if (num3 < 0 || num3 >= 0 && num3 == (int) rhObject.ros.rsLayer)
                      {
                        CSprite rcSprite = rhObject.roc.rcSprite;
                        if (rcSprite != null && ((int) rcSprite.sprFlags & 1) != 0)
                        {
                          if (!flag || ((int) rcSprite.sprFlags & 256 /*0x0100*/) != 0)
                          {
                            if (carrayList == null)
                              carrayList = new CArrayList();
                            carrayList.add((object) rhObject);
                            continue;
                          }
                          if (cmask == null)
                          {
                            CImage imageFromHandle = this.rhApp.imageBank.getImageFromHandle(newImg);
                            if (imageFromHandle != null)
                              cmask = imageFromHandle.getMask(0, newAngle, newScaleX, newScaleY);
                          }
                          CMask pMask2 = (CMask) null;
                          CImage imageFromHandle1 = this.rhApp.imageBank.getImageFromHandle(rhObject.roc.rcImage);
                          if (imageFromHandle1 != null)
                            pMask2 = imageFromHandle1.getMask(0, rhObject.roc.rcAngle, rhObject.roc.rcScaleX, rhObject.roc.rcScaleY);
                          if (cmask != null && pMask2 != null && cmask.testMask(0, x1, y1, pMask2, 0, x2, y2))
                          {
                            if (carrayList == null)
                              carrayList = new CArrayList();
                            carrayList.add((object) rhObject);
                            continue;
                          }
                          continue;
                        }
                        continue;
                      }
                      continue;
                    case 3:
                    case 5:
                    case 6:
                    case 7:
                    case 9:
                      if (carrayList == null)
                        carrayList = new CArrayList();
                      carrayList.add((object) rhObject);
                      continue;
                    default:
                      if (carrayList == null)
                        carrayList = new CArrayList();
                      carrayList.add((object) rhObject);
                      continue;
                  }
                }
              }
            }
          }
        }
        else
        {
          for (int index4 = 0; index4 < this.rhNObjects; ++index4)
          {
            while (this.rhObjectList[index1] == null)
              ++index1;
            CObject rhObject = this.rhObjectList[index1];
            ++index1;
            if (((int) rhObject.hoFlags & 8192 /*0x2000*/) == 0)
            {
              int x2 = rhObject.hoX - rhObject.hoImgXSpot;
              int y2 = rhObject.hoY - rhObject.hoImgYSpot;
              if (x2 < num1 && x2 + rhObject.hoImgWidth > x1 && y2 < num2 && y2 + rhObject.hoImgHeight > y1)
              {
                switch (rhObject.hoType)
                {
                  case 2:
                    if (num3 < 0 || num3 >= 0 && num3 == (int) rhObject.ros.rsLayer)
                    {
                      CSprite rcSprite = rhObject.roc.rcSprite;
                      if (rcSprite != null && ((int) rcSprite.sprFlags & 1) != 0)
                      {
                        if (!flag || ((int) rcSprite.sprFlags & 256 /*0x0100*/) != 0)
                        {
                          if (carrayList == null)
                            carrayList = new CArrayList();
                          carrayList.add((object) rhObject);
                          continue;
                        }
                        if (cmask == null)
                        {
                          CImage imageFromHandle = this.rhApp.imageBank.getImageFromHandle(newImg);
                          if (imageFromHandle != null)
                            cmask = imageFromHandle.getMask(0, newAngle, newScaleX, newScaleY);
                        }
                        CImage imageFromHandle2 = this.rhApp.imageBank.getImageFromHandle(rhObject.roc.rcImage);
                        CMask pMask2 = (CMask) null;
                        if (imageFromHandle2 != null)
                          pMask2 = imageFromHandle2.getMask(0, rhObject.roc.rcAngle, rhObject.roc.rcScaleX, rhObject.roc.rcScaleY);
                        if (cmask != null && pMask2 != null && cmask.testMask(0, x1, y1, pMask2, 0, x2, y2))
                        {
                          if (carrayList == null)
                            carrayList = new CArrayList();
                          carrayList.add((object) rhObject);
                          continue;
                        }
                        continue;
                      }
                      continue;
                    }
                    continue;
                  case 3:
                  case 5:
                  case 6:
                  case 7:
                  case 9:
                    if (carrayList == null)
                      carrayList = new CArrayList();
                    carrayList.add((object) rhObject);
                    continue;
                  default:
                    if (carrayList == null)
                      carrayList = new CArrayList();
                    carrayList.add((object) rhObject);
                    continue;
                }
              }
            }
          }
        }
        pHo.hoFlags = hoFlags;
        return carrayList;
      }

      public int colMask_TestObject_IXY(
        CObject pHo,
        short newImg,
        int newAngle,
        float newScaleX,
        float newScaleY,
        int newX,
        int newY,
        int htfoot,
        int plan)
      {
        int num = 0;
        int newX1 = newX - this.rhWindowX;
        int newY1 = newY - this.rhWindowY;
        bool flag = false;
        if (((int) pHo.hoFlags & 36) != 0 && ((int) pHo.ros.rsCreaFlags & 256 /*0x0100*/) == 0)
          flag = true;
        if (flag)
        {
          CSprite rcSprite = pHo.roc.rcSprite;
          if (rcSprite != null && this.rhFrame.bkdCol_TestSprite(rcSprite, (int) newImg, newX1, newY1, newAngle, newScaleX, newScaleY, htfoot, plan))
            num = -851968 | (int) pHo.hoType & (int) ushort.MaxValue;
        }
        else
        {
          int x = newX1 - pHo.hoImgXSpot;
          int y1 = newY1 - pHo.hoImgYSpot;
          if (htfoot != 0)
          {
            int y2 = y1 + pHo.hoImgHeight - htfoot;
            if (this.rhFrame.bkdCol_TestRect(x, y2, pHo.hoImgWidth, htfoot, pHo.hoLayer, plan))
              num = -851968 | (int) pHo.hoType & (int) ushort.MaxValue;
          }
          else if (this.rhFrame.bkdCol_TestRect(x, y1, pHo.hoImgWidth, pHo.hoImgHeight, pHo.hoLayer, plan))
            num = -851968 | (int) pHo.hoType & (int) ushort.MaxValue;
        }
        return num;
      }

      public int quadran_Out(int x1, int y1, int x2, int y2)
      {
        int index = 0;
        if (x1 < 0)
          index |= 1;
        if (y1 < 0)
          index |= 4;
        if (x2 > this.rhLevelSx)
          index |= 2;
        if (y2 > this.rhLevelSy)
          index |= 8;
        return (int) this.Table_InOut[index];
      }

      public int quadran_In(int x1, int y1, int x2, int y2)
      {
        int index = 15;
        if (x1 < this.rhLevelSx)
          index &= -3;
        if (y1 < this.rhLevelSy)
          index &= -9;
        if (x2 > 0)
          index &= -2;
        if (y2 > 0)
          index &= -5;
        return (int) this.Table_InOut[index];
      }

      public short random(short wMax)
      {
        int num = (int) this.rh3Graine * 31415 + 1;
        this.rh3Graine = (short) num;
        return (short) ((num & (int) ushort.MaxValue) * (int) wMax >> 16 /*0x10*/);
      }

      public int get_Direction(int dir)
      {
        if (dir == 0 || dir == -1)
          return (int) this.random((short) 32 /*0x20*/);
        int direction1 = 0;
        int wMax = 0;
        int num1 = dir;
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if ((num1 & 1) != 0)
          {
            ++wMax;
            direction1 = index;
          }
          num1 = num1 >> 1 & int.MaxValue;
        }
        if (wMax == 1)
          return direction1;
        int num2 = (int) this.random((short) wMax);
        int num3 = dir;
        for (int direction2 = 0; direction2 < 32 /*0x20*/; ++direction2)
        {
          if ((num3 & 1) != 0)
          {
            --num2;
            if (num2 < 0)
              return direction2;
          }
          num3 = num3 >> 1 & int.MaxValue;
        }
        return 0;
      }

      public CValue get_EventExpressionAny(CParamExpression pExp)
      {
        this.rh4Tokens = pExp.tokens;
        this.rh4CurToken = 0;
        return new CValue(this.getExpression());
      }

      public int get_EventExpressionInt(CParamExpression pExp)
      {
        this.rh4Tokens = pExp.tokens;
        this.rh4CurToken = 0;
        return this.getExpression().getInt();
      }

      public double get_EventExpressionDouble(CParamExpression pExp)
      {
        this.rh4Tokens = pExp.tokens;
        this.rh4CurToken = 0;
        return this.getExpression().getDouble();
      }

      public string get_EventExpressionString(CParamExpression pExp)
      {
        this.rh4Tokens = pExp.tokens;
        this.rh4CurToken = 0;
        return this.getExpression().getString();
      }

      public int get_ExpressionInt() => this.getExpression().getInt();

      public double get_ExpressionDouble() => this.getExpression().getDouble();

      public string get_ExpressionString() => this.getExpression().getString();

      public CValue get_ExpressionAny() => new CValue(this.getExpression());

      public CValue getExpression()
      {
        int rh4PosPile = this.rh4PosPile;
        this.rh4Operators[this.rh4PosPile] = this.rh4OpeNull;
        do
        {
          ++this.rh4PosPile;
          this.bOperande = true;
          this.rh4Tokens[this.rh4CurToken].evaluate(this);
          this.bOperande = false;
          ++this.rh4CurToken;
          while (true)
          {
            CExp rh4Token = this.rh4Tokens[this.rh4CurToken];
            if (rh4Token.code > 0 && rh4Token.code < 1310720 /*0x140000*/)
            {
              if (rh4Token.code > this.rh4Operators[this.rh4PosPile - 1].code)
              {
                this.rh4Operators[this.rh4PosPile] = rh4Token;
                ++this.rh4CurToken;
                ++this.rh4PosPile;
                this.bOperande = true;
                this.rh4Tokens[this.rh4CurToken].evaluate(this);
                this.bOperande = false;
                ++this.rh4CurToken;
              }
              else
              {
                --this.rh4PosPile;
                this.rh4Operators[this.rh4PosPile].evaluate(this);
              }
            }
            else
            {
              --this.rh4PosPile;
              if (this.rh4PosPile != rh4PosPile)
                this.rh4Operators[this.rh4PosPile].evaluate(this);
              else
                break;
            }
          }
        }
        while (this.rh4PosPile > rh4PosPile + 1);
        return this.rh4Results[rh4PosPile + 1];
      }

      public CValue getCurrentResult() => this.rh4Results[this.rh4PosPile];

      public CValue getPreviousResult() => this.rh4Results[this.rh4PosPile - 1];

      public CValue getNextResult() => this.rh4Results[this.rh4PosPile + 1];

      public static bool compareTo(CValue pValue1, CValue pValue2, short comp)
      {
        switch (comp)
        {
          case 0:
            return pValue1.equal(pValue2);
          case 1:
            return pValue1.notEqual(pValue2);
          case 2:
            return pValue1.lower(pValue2);
          case 3:
            return pValue1.lowerThan(pValue2);
          case 4:
            return pValue1.greater(pValue2);
          case 5:
            return pValue1.greaterThan(pValue2);
          default:
            return false;
        }
      }

      public static bool compareTer(int value1, int value2, short comparaison)
      {
        switch (comparaison)
        {
          case 0:
            return value1 == value2;
          case 1:
            return value1 != value2;
          case 2:
            return value1 <= value2;
          case 3:
            return value1 < value2;
          case 4:
            return value1 >= value2;
          case 5:
            return value1 > value2;
          default:
            return false;
        }
      }

      public void update_PlayerObjects(int joueur, short type, int value)
      {
        ++joueur;
        int index1 = 0;
        for (int index2 = 0; index2 < this.rhNObjects; ++index2)
        {
          while (this.rhObjectList[index1] == null)
            ++index1;
          CObject rhObject = this.rhObjectList[index1];
          if ((int) rhObject.hoType == (int) type)
          {
            switch (type)
            {
              case 5:
                CScore cscore = (CScore) rhObject;
                if ((int) cscore.rsPlayer == joueur)
                {
                  cscore.rsValue.forceInt(value);
                  break;
                }
                break;
              case 6:
                CLives clives = (CLives) rhObject;
                if ((int) clives.rsPlayer == joueur)
                {
                  clives.rsValue.forceInt(value);
                  break;
                }
                break;
            }
            rhObject.roc.rcChanged = true;
            rhObject.modif();
          }
          ++index1;
        }
      }

      public void actPla_FinishLives(int joueur, int live)
      {
        int[] lives = this.rhApp.getLives();
        if (live == lives[joueur])
          return;
        if (live == 0 && lives[joueur] != 0)
          this.rhEvtProg.push_Event(0, -262151, 0, (CObject) null, (short) joueur);
        lives[joueur] = live;
        this.update_PlayerObjects(joueur, (short) 6, live);
      }

      public bool getMouseOnObjectsEDX(short oiList, bool nega)
      {
        CObject cobject = this.rhEvtProg.evt_FirstObject(oiList);
        if (cobject == null)
          return nega;
        int nselectedObjects = this.rhEvtProg.evtNSelectedObjects;
        int xp = this.rh2MouseX - this.rhWindowX;
        int yp = this.rh2MouseY - this.rhWindowY;
        CArrayList carrayList = new CArrayList();
        for (CSprite firstSpr = this.rhApp.spriteGen.spriteCol_TestPoint((CSprite) null, (short) -1, xp, yp, 0); firstSpr != null; firstSpr = this.rhApp.spriteGen.spriteCol_TestPoint(firstSpr, (short) -1, xp, yp, 0))
        {
          CObject sprExtraInfo = firstSpr.sprExtraInfo;
          if (((int) sprExtraInfo.hoFlags & 1) == 0)
            carrayList.add((object) sprExtraInfo);
        }
        int index1 = 0;
        for (int index2 = 0; index2 < this.rhNObjects; ++index2)
        {
          while (this.rhObjectList[index1] == null)
            ++index1;
          CObject rhObject = this.rhObjectList[index1];
          ++index1;
          if (((int) rhObject.hoFlags & 8196) == 0)
          {
            int num1 = rhObject.hoX - this.rhWindowX - rhObject.hoImgXSpot;
            int num2 = num1 + rhObject.hoImgWidth;
            int num3 = rhObject.hoY - this.rhWindowY - rhObject.hoImgYSpot;
            int num4 = num3 + rhObject.hoImgHeight;
            if (xp >= num1 && xp < num2 && yp >= num3 && yp < num4 && ((int) rhObject.hoFlags & 1) == 0)
              carrayList.add((object) rhObject);
          }
        }
        if (carrayList.size() == 0)
          return nega;
        if (!nega)
        {
          do
          {
            int index3 = 0;
            while (index3 < carrayList.size() && (CObject) carrayList.get(index3) != cobject)
              ++index3;
            if (index3 == carrayList.size())
            {
              --nselectedObjects;
              this.rhEvtProg.evt_DeleteCurrentObject();
            }
            cobject = this.rhEvtProg.evt_NextObject();
          }
          while (cobject != null);
          return nselectedObjects != 0;
        }
        do
        {
          for (int index4 = 0; index4 < carrayList.size(); ++index4)
          {
            if ((CObject) carrayList.get(index4) == cobject)
              return false;
          }
          cobject = this.rhEvtProg.evt_NextObject();
        }
        while (cobject != null);
        return true;
      }

      public int txtDisplay(CEvent pe, short oi, int txtNumber)
      {
        PARAM_CREATE evtParam = (PARAM_CREATE) pe.evtParams[0];
        CPositionInfo pInfo = new CPositionInfo();
        if (evtParam.read_Position(this, 16 /*0x10*/, pInfo))
        {
          int index1 = 0;
          for (int index2 = 0; index2 < this.rhNObjects; ++index2)
          {
            while (this.rhObjectList[index1] == null)
              ++index1;
            CObject rhObject = this.rhObjectList[index1];
            ++index1;
            if (rhObject.hoType == (short) 3 && (int) rhObject.hoOi == (int) oi && rhObject.hoX == pInfo.x && rhObject.hoY == pInfo.y)
            {
              rhObject.ros.obShow();
              rhObject.hoFlags &= (short) -8193 /*0xDFFF*/;
              CText ctext = (CText) rhObject;
              ctext.rsMini = -2;
              ctext.txtChange(txtNumber);
              rhObject.roc.rcChanged = true;
              rhObject.display();
              rhObject.ros.rsFlash = 0;
              rhObject.ros.rsFlags |= (short) 32 /*0x20*/;
              return (int) rhObject.hoNumber;
            }
          }
          int index3 = this.f_CreateObject((short) -1, oi, pInfo.x, pInfo.y, 0, (short) 0, this.rhFrame.nLayers - 1, -1);
          if (index3 >= 0)
          {
            ((CText) this.rhObjectList[index3]).txtChange(txtNumber);
            return index3;
          }
        }
        return -1;
      }

      public int txtDoDisplay(CEvent pe, int txtNumber)
      {
        if (pe.evtOiList >= (short) 0)
          return this.txtDisplay(pe, pe.evtOi, txtNumber);
        if (pe.evtOiList == (short) -1)
          return -1;
        CQualToOiList qualToOi = this.rhEvtProg.qualToOiList[(int) pe.evtOiList & (int) short.MaxValue];
        for (int index = 0; index < qualToOi.qoiList.Length; index += 2)
          this.txtDisplay(pe, qualToOi.qoiList[index], txtNumber);
        return -1;
      }

      public static CFontInfo getObjectFont(CObject hoPtr)
      {
        CFontInfo objectFont = (CFontInfo) null;
        if (hoPtr.hoType >= (short) 32 /*0x20*/)
        {
          objectFont = ((CExtension) hoPtr).ext.getRunObjectFont();
        }
        else
        {
          switch (hoPtr.hoType)
          {
            case 3:
              objectFont = ((CText) hoPtr).getFont();
              break;
            case 5:
              objectFont = ((CScore) hoPtr).getFont();
              break;
            case 6:
              objectFont = ((CLives) hoPtr).getFont();
              break;
            case 7:
              objectFont = ((CCounter) hoPtr).getFont();
              break;
          }
        }
        if (objectFont == null)
          objectFont = new CFontInfo();
        return objectFont;
      }

      public static void setObjectFont(CObject hoPtr, CFontInfo pLf, CRect pNewSize)
      {
        if (hoPtr.hoType >= (short) 32 /*0x20*/)
        {
          ((CExtension) hoPtr).ext.setRunObjectFont(pLf, pNewSize);
        }
        else
        {
          switch (hoPtr.hoType)
          {
            case 3:
              ((CText) hoPtr).setFont(pLf, pNewSize);
              break;
            case 5:
              ((CScore) hoPtr).setFont(pLf, pNewSize);
              break;
            case 6:
              ((CLives) hoPtr).setFont(pLf, pNewSize);
              break;
            case 7:
              ((CCounter) hoPtr).setFont(pLf, pNewSize);
              break;
          }
        }
      }

      public static int getObjectTextColor(CObject hoPtr)
      {
        if (hoPtr.hoType >= (short) 32 /*0x20*/)
          return ((CExtension) hoPtr).ext.getRunObjectTextColor();
        switch (hoPtr.hoType)
        {
          case 3:
            return ((CText) hoPtr).getFontColor();
          case 5:
            return ((CScore) hoPtr).getFontColor();
          case 6:
            return ((CLives) hoPtr).getFontColor();
          case 7:
            return ((CCounter) hoPtr).getFontColor();
          default:
            return 0;
        }
      }

      public static void setObjectTextColor(CObject hoPtr, int rgb)
      {
        if (hoPtr.hoType >= (short) 32 /*0x20*/)
        {
          ((CExtension) hoPtr).ext.setRunObjectTextColor(rgb);
        }
        else
        {
          switch (hoPtr.hoType)
          {
            case 3:
              ((CText) hoPtr).setFontColor(rgb);
              break;
            case 5:
              ((CScore) hoPtr).setFontColor(rgb);
              break;
            case 6:
              ((CLives) hoPtr).setFontColor(rgb);
              break;
            case 7:
              ((CCounter) hoPtr).setFontColor(rgb);
              break;
          }
        }
      }

      public static void setXPosition(CObject hoPtr, int x)
      {
        if (hoPtr.rom != null)
        {
          hoPtr.rom.rmMovement.setXPosition(x);
        }
        else
        {
          if (hoPtr.hoX == x)
            return;
          hoPtr.hoX = x;
          if (hoPtr.roc == null)
            return;
          hoPtr.roc.rcChanged = true;
          hoPtr.roc.rcCheckCollides = true;
        }
      }

      public static void setYPosition(CObject hoPtr, int y)
      {
        if (hoPtr.rom != null)
        {
          hoPtr.rom.rmMovement.setYPosition(y);
        }
        else
        {
          if (hoPtr.hoY == y)
            return;
          hoPtr.hoY = y;
          if (hoPtr.roc == null)
            return;
          hoPtr.roc.rcChanged = true;
          hoPtr.roc.rcCheckCollides = true;
        }
      }

      public static int get_DirFromPente(int x, int y)
      {
        if (x == 0)
          return y >= 0 ? 24 : 8;
        if (y == 0)
          return x >= 0 ? 0 : 16 /*0x10*/;
        bool flag1 = false;
        bool flag2 = false;
        if (x < 0)
        {
          flag1 = true;
          x = -x;
        }
        if (y < 0)
        {
          flag2 = true;
          y = -y;
        }
        int num = x * 256 /*0x0100*/ / y;
        int index = 0;
        while (num < CMove.CosSurSin32[index])
          index += 2;
        int dirFromPente = CMove.CosSurSin32[index + 1];
        if (flag2)
          dirFromPente = -dirFromPente + 32 /*0x20*/ & 31 /*0x1F*/;
        if (flag1)
          dirFromPente = (-(dirFromPente - 8 & 31 /*0x1F*/) & 31 /*0x1F*/) + 8 & 31 /*0x1F*/;
        return dirFromPente;
      }

      public void init_Disappear(CObject hoPtr)
      {
        bool flag = false;
        int num1 = 0;
        if (((int) hoPtr.hoFlags & 8) == 0)
        {
          if (hoPtr.ros.initFadeOut())
            return;
          if (hoPtr.roa != null && hoPtr.roa.anim_Exist(4))
            num1 = 1;
        }
        if (num1 == 0)
          flag = true;
        if (flag)
        {
          hoPtr.hoCallRoutine = false;
          this.destroy_Add((int) hoPtr.hoNumber);
        }
        else
        {
          if (hoPtr.roc.rcSprite != null)
          {
            int num2 = (int) hoPtr.roc.rcSprite.setSpriteColFlag(0U);
          }
          if (hoPtr.rom != null)
          {
            hoPtr.rom.initSimple(hoPtr, 11, false);
            hoPtr.roc.rcSpeed = 0;
          }
          if ((num1 & 1) == 0)
            return;
          hoPtr.roa.animation_Force(4);
          hoPtr.roa.animation_OneLoop();
        }
      }

      public void add_QuickDisplay(CObject hoPtr)
      {
        if (this.rh4FirstQuickDisplay < (short) 0)
        {
          this.rh4FirstQuickDisplay = hoPtr.hoNumber;
          hoPtr.hoPreviousQuickDisplay = (short) -1;
        }
        else if (this.rh4LastQuickDisplay >= (short) 0)
        {
          CObject rhObject = this.rhObjectList[(int) this.rh4LastQuickDisplay];
          rhObject.hoNextQuickDisplay = hoPtr.hoNumber;
          hoPtr.hoPreviousQuickDisplay = rhObject.hoNumber;
        }
        this.rh4LastQuickDisplay = hoPtr.hoNumber;
        hoPtr.hoNextQuickDisplay = (short) -1;
      }

      public void draw_QuickDisplay(SpriteBatchEffect batch)
      {
        CObject rhObject;
        for (int index = (int) this.rh4FirstQuickDisplay; index >= 0; index = (int) rhObject.hoNextQuickDisplay)
        {
          rhObject = this.rhObjectList[index];
          if (((int) rhObject.ros.rsFlags & 5) == 0)
            rhObject.draw(batch);
        }
      }

      public void remove_QuickDisplay(CObject hoPtr)
      {
        short nextQuickDisplay = hoPtr.hoNextQuickDisplay;
        short previousQuickDisplay = hoPtr.hoPreviousQuickDisplay;
        if (previousQuickDisplay >= (short) 0)
          this.rhObjectList[(int) previousQuickDisplay].hoNextQuickDisplay = nextQuickDisplay;
        else
          this.rh4FirstQuickDisplay = nextQuickDisplay;
        if (nextQuickDisplay >= (short) 0)
          this.rhObjectList[(int) nextQuickDisplay].hoPreviousQuickDisplay = previousQuickDisplay;
        else
          this.rh4LastQuickDisplay = previousQuickDisplay;
      }

      public bool isMouseOn() => this.rh4CursorShown;

      public static void objectHide(CObject pHo)
      {
        if (pHo.ros == null)
          return;
        pHo.ros.obHide();
        pHo.ros.rsFlags &= (short) -33;
        pHo.ros.rsFlash = 0;
      }

      public static void objectShow(CObject pHo)
      {
        if (pHo.ros == null)
          return;
        pHo.ros.obShow();
        pHo.ros.rsFlags |= (short) 32 /*0x20*/;
        pHo.ros.rsFlash = 0;
      }

      public void setFrameRate(int value)
      {
        if (value < 1 || value > 1000)
          return;
        CRunApp crunApp = this.rhApp;
        while (crunApp.parentApp != null)
          crunApp = crunApp.parentApp;
        crunApp.gaFrameRate = value;
      }

      public int getXMouse() => this.rhMouseUsed != (byte) 0 ? 0 : this.rh2MouseX;

      public int getYMouse() => this.rhMouseUsed != (byte) 0 ? 0 : this.rh2MouseY;

      public int getRGBAt(CObject hoPtr, int x, int y) => 0;

      public CExtStorage getStorage(int id)
      {
        if (this.rhApp.extensionStorage != null)
        {
          for (int index = 0; index < this.rhApp.extensionStorage.size(); ++index)
          {
            CExtStorage storage = (CExtStorage) this.rhApp.extensionStorage.get(index);
            if (storage.id == id)
              return storage;
          }
        }
        return (CExtStorage) null;
      }

      public void delStorage(int id)
      {
        if (this.rhApp.extensionStorage == null)
          return;
        for (int index = 0; index < this.rhApp.extensionStorage.size(); ++index)
        {
          if (((CExtStorage) this.rhApp.extensionStorage.get(index)).id == id)
            this.rhApp.extensionStorage.remove(index);
        }
      }

      public void addStorage(CExtStorage data, int id)
      {
        if (this.getStorage(id) != null)
          return;
        if (this.rhApp.extensionStorage == null)
          this.rhApp.extensionStorage = new CArrayList();
        data.id = id;
        this.rhApp.extensionStorage.add((object) data);
      }

      public void getTouches()
      {
        this.mouseKey = -1;
        this.rh2MouseKeys = (byte) 0;
        if (this.rhApp.numberOfTouches <= 0)
          return;
        foreach (TouchLocation touch in TouchPanel.GetState())
        {
          if (this.toucheID < 0 && touch.State == TouchLocationState.Pressed)
            this.toucheID = touch.Id;
          bool flag1 = false;
          bool flag2 = false;
          switch (touch.State)
          {
            case TouchLocationState.Invalid:
              if (this.joystick != null)
                this.joystick.touchCancelled(touch);
              for (int index = 0; index < this.rhApp.numberOfTouches; ++index)
              {
                if (this.cancelledTouches[index] == touch.Id)
                {
                  this.cancelledTouches[index] = -1;
                  break;
                }
              }
              if (this.touches != null)
              {
                this.touches.touchCancelled(touch);
                break;
              }
              break;
            case TouchLocationState.Released:
              if (this.joystick != null)
                this.joystick.touchEnded(touch);
              for (int index = 0; index < this.rhApp.numberOfTouches; ++index)
              {
                if (this.cancelledTouches[index] == touch.Id)
                {
                  this.cancelledTouches[index] = -1;
                  flag2 = true;
                  flag1 = true;
                  break;
                }
              }
              if (this.touches != null && !flag2)
              {
                this.touches.touchEnded(touch);
                break;
              }
              break;
            case TouchLocationState.Pressed:
              if (this.joystick != null)
              {
                flag2 = this.joystick.touchBegan(touch);
                if (flag2)
                  flag1 = true;
              }
              if (this.touches != null && !flag2)
                this.touches.touchBegan(touch);
              if (flag2)
              {
                for (int index = 0; index < this.rhApp.numberOfTouches; ++index)
                {
                  if (this.cancelledTouches[index] < 0)
                  {
                    this.cancelledTouches[index] = touch.Id;
                    break;
                  }
                }
                break;
              }
              break;
            case TouchLocationState.Moved:
              if (this.joystick != null)
                this.joystick.touchMoved(touch);
              for (int index = 0; index < this.rhApp.numberOfTouches; ++index)
              {
                if (this.cancelledTouches[index] == touch.Id)
                {
                  flag2 = true;
                  flag1 = true;
                  break;
                }
              }
              if (this.touches != null && !flag2)
              {
                this.touches.touchMoved(touch);
                break;
              }
              break;
          }
          if (touch.Id == this.toucheID)
          {
            switch (touch.State)
            {
              case TouchLocationState.Invalid:
                this.toucheID = -1;
                continue;
              case TouchLocationState.Released:
                this.mouseX = (int) touch.Position.X;
                this.mouseY = (int) touch.Position.Y;
                this.toucheID = -1;
                continue;
              case TouchLocationState.Pressed:
              case TouchLocationState.Moved:
                this.mouseX = (int) touch.Position.X;
                this.mouseY = (int) touch.Position.Y;
                if (!flag1)
                {
                  this.mouseKey = 0;
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
      }

      public void startJoystickAcc()
      {
        ++this.joystickAccCount;
        if (this.joystickAccCount != 1)
          return;
        //this.rhApp.startAccelerometer();
        this.joystickAcc = new CJoystickAcc();
      }

      public void stopJoystickAcc()
      {
        --this.joystickAccCount;
        if (this.joystickAccCount != 0)
          return;
        //this.rhApp.stopAccelerometer();
        this.joystickAcc = (CJoystickAcc) null;
      }

      public void callEventExtension(CExtension hoPtr, int code, int param)
      {
        if (this.rh2PauseCompteur != 0)
          return;
        int rhCurParam0 = this.rhEvtProg.rhCurParam0;
        this.rhEvtProg.rhCurParam0 = param;
        code = -(code + 80 /*0x50*/ + 1) << 16 /*0x10*/;
        code |= (int) hoPtr.hoType & (int) ushort.MaxValue;
        this.rhEvtProg.handle_Event((CObject) hoPtr, code);
        this.rhEvtProg.rhCurParam0 = rhCurParam0;
      }

      public void addControl(IControl c)
      {
        ++this.nControls;
        if (this.controls == null)
          this.controls = new CArrayList();
        this.controls.add((object) c);
        c.setMouseControlled(this.bMouseControlled);
      }

      public void delControl(IControl c)
      {
        --this.nControls;
        this.controls.remove((object) c);
      }

      public void clickControls(int nClicks)
      {
        for (int index = 0; index < this.nControls; ++index)
          ((IControl) this.controls.get(index)).click(nClicks);
      }

      public void newKey()
      {
        if (this.nControls <= 0)
          return;
        if (((int) this.rh2NewPlayer[0] & 4) != 0 && ((int) this.rhPlayer[0] & 4) != 0)
        {
          IControl control1;
          int num1;
          int num2;
          if (this.currentControl == null)
          {
            control1 = (IControl) null;
            num1 = 1000;
            num2 = 1000;
          }
          else
          {
            control1 = this.currentControl;
            num1 = this.currentControl.getX();
            num2 = this.currentControl.getY();
            control1.setFocus(false);
          }
          int num3 = -1000;
          int num4 = -1000;
          IControl control2 = (IControl) null;
          for (int index = 0; index < this.nControls; ++index)
          {
            IControl control3 = (IControl) this.controls.get(index);
            if (control3 != control1)
            {
              int x = control3.getX();
              int y = control3.getY();
              if ((y < num2 || y == num2 && x < num1) && (y > num4 || y == num4 && x > num3))
              {
                num3 = x;
                num4 = y;
                control2 = control3;
              }
            }
          }
          this.currentControl = control2;
        }
        if (((int) this.rh2NewPlayer[0] & 8) != 0 && ((int) this.rhPlayer[0] & 8) != 0)
        {
          IControl control4;
          int num5;
          int num6;
          if (this.currentControl == null)
          {
            control4 = (IControl) null;
            num5 = -1000;
            num6 = -1000;
          }
          else
          {
            control4 = this.currentControl;
            num5 = this.currentControl.getX();
            num6 = this.currentControl.getY();
            control4.setFocus(false);
          }
          int num7 = 1000;
          int num8 = 1000;
          IControl control5 = (IControl) null;
          for (int index = 0; index < this.nControls; ++index)
          {
            IControl control6 = (IControl) this.controls.get(index);
            if (control6 != control4)
            {
              int x = control6.getX();
              int y = control6.getY();
              if ((y > num6 || y == num6 && x > num5) && (y < num8 || y == num8 && x < num7))
              {
                num7 = x;
                num8 = y;
                control5 = control6;
              }
            }
          }
          this.currentControl = control5;
        }
        if (this.currentControl == null)
          return;
        this.currentControl.setFocus(true);
      }
    }
}
