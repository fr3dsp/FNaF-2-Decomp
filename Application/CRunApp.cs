// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Application.CRunApp
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

//using Microsoft.Devices.Sensors;
//using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using RuntimeXNA.Banks;
using RuntimeXNA.Expressions;
using RuntimeXNA.Extensions;
using RuntimeXNA.Objects;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;
using System;

namespace RuntimeXNA.Application
{

    public class CRunApp
    {
      public const short RUNTIME_VERSION = 770;
      public const short MAX_PLAYER = 4;
      public const short MAX_KEY = 8;
      public const short GA_NOHEADING = 2;
      public const short GA_SPEEDINDEPENDANT = 8;
      public const short GA_STRETCH = 16 /*0x10*/;
      public const short GA_MENUHIDDEN = 128 /*0x80*/;
      public const short GA_MENUBAR = 256 /*0x0100*/;
      public const short GA_MAXIMISE = 512 /*0x0200*/;
      public const short GA_MIX = 1024 /*0x0400*/;
      public const short GA_FULLSCREENATSTART = 2048 /*0x0800*/;
      public const short GANF_SAMPLESOVERFRAMES = 1;
      public const short GANF_RUNFRAME = 4;
      public const short GANF_NOTHICKFRAME = 64 /*0x40*/;
      public const short GANF_DONOTCENTERFRAME = 128 /*0x80*/;
      public const short GANF_DISABLE_CLOSE = 512 /*0x0200*/;
      public const short GANF_HIDDENATSTART = 1024 /*0x0400*/;
      public const short GANF_MDI = 16384 /*0x4000*/;
      public const short GAOF_JAVASWING = 4096 /*0x1000*/;
      public const short GAOF_JAVAAPPLET = 8192 /*0x2000*/;
      public const short SL_RESTART = 0;
      public const short SL_STARTFRAME = 1;
      public const short SL_FRAMEFADEINLOOP = 2;
      public const short SL_FRAMELOOP = 3;
      public const short SL_FRAMEFADEOUTLOOP = 4;
      public const short SL_ENDFRAME = 5;
      public const short SL_QUIT = 6;
      public const int MAX_VK = 523;
      public const short CTRLTYPE_MOUSE = 0;
      public const short CTRLTYPE_JOY1 = 1;
      public const short CTRLTYPE_JOY2 = 2;
      public const short CTRLTYPE_JOY3 = 3;
      public const short CTRLTYPE_JOY4 = 4;
      public const short CTRLTYPE_KEYBOARD = 5;
      public const short ARF_INGAMELOOP = 4;
      public const int AH2OPT_STATUSLINE = 64 /*0x40*/;
      public const int AH2OPT_EDITPRESENT = 1024 /*0x0400*/;
      private const float kFilteringFactor = 0.1f;
      public GraphicsDeviceManager graphicsDeviceManager;
      public SpriteBatchEffect spriteBatch;
      public GraphicsDevice graphicsDevice;
      public ContentManager content;
      public int displayType;
      public int[] frameOffsets;
      public int frameMaxIndex;
      public string[] framePasswords;
      public string appName;
      public string appCopyright;
      public string appAboutText;
      public string appDoc;
      public short nGlobalValuesInit;
      public byte[] globalValuesInitTypes;
      public int[] globalValuesInit;
      public short nGlobalStringsInit;
      public string[] globalStringsInit;
      public COIList OIList;
      public CImageBank imageBank;
      public CFontBank fontBank;
      public CSoundBank soundBank;
      public CSoundPlayer soundPlayer;
      public int appRunningState;
      public int[] lives;
      public int[] scores;
      public string[] playerNames;
      public CArrayList gValues;
      public CArrayList gStrings;
      public CValue tempGValue;
      public int startFrame;
      public int nextFrame;
      public int currentFrame;
      public CRunFrame frame;
      public CFile file;
      public CRunApp parentApp;
      public int parentOptions;
      public int parentX;
      public int parentY;
      public int parentWidth;
      public int parentHeight;
      public bool redrawBack = true;
      public short gaFlags;
      public short gaNewFlags;
      public short gaMode;
      public short gaOtherFlags;
      public int gaCxWin;
      public int gaCyWin;
      public int gaScoreInit;
      public int gaLivesInit;
      public int gaBorderColour;
      public int gaNbFrames;
      public int gaFrameRate;
      public short[] pcCtrlType = new short[4];
      public Keys[] pcCtrlKeys = new Keys[32 /*0x20*/];
      public short[] frameHandleToIndex;
      public short frameMaxHandle;
      public short appRunFlags;
      public CArrayList adGO;
      public CArrayList sysEvents;
      private bool quit;
      public CExtLoader extLoader;
      public bool m_bLoading;
      public bool bVisible;
      public bool bPositionWindow;
      public bool bResizeWindow;
      public int debug;
      public CArrayList extensionStorage;
      public CEmbeddedFile[] embeddedFiles;
      public bool internalPaintFlag;
      public bool bUnicode;
      public int VBL;
      public long timer;
      public double timeDouble;
      public CSpriteGen spriteGen;
      public CRun run;
      public CServices services;
      public Rectangle tempRect;
      public Game1 game;
      public int xOffset;
      public int yOffset;
      public bool bSubAppShown;
      public int numberOfTouches;
      public int hdr2Options;
      public int hdr2Orientation;
      public bool bSignedIn;
      public CArrayList advertisements;
      //private Accelerometer accel;
      private int nAccel;
      public static float accX;
      public static float accY;
      public static float accZ;
      public static float filteredAccX;
      public static float filteredAccY;
      public static float filteredAccZ;
      public static float instantAccX;
      public static float instantAccY;
      public static float instantAccZ;
      public static DisplayOrientation orientation;
      public CExtension XNAObject;

      public CRunApp()
      {
      }

      public CRunApp(Game1 gam, CFile f)
      {
        this.game = gam;
        this.file = f;
        this.content = gam.Content;
        this.graphicsDeviceManager = gam.graphics;
        this.graphicsDevice = gam.GraphicsDevice;
        this.spriteBatch = gam.spriteBatch;
      }

      public void setParentApp(
        CRunApp pApp,
        int sFrame,
        int options,
        int x,
        int y,
        int width,
        int height)
      {
        this.parentApp = pApp;
        this.parentOptions = options;
        this.startFrame = sFrame;
        this.xOffset = x;
        this.yOffset = y;
        this.parentWidth = width;
        this.parentHeight = height;
      }

      public void setOffsets(int x, int y)
      {
        this.xOffset = x;
        this.yOffset = y;
        this.spriteGen.setOffsets(x, y);
      }

      public void showSubApp(bool bShown) => this.bSubAppShown = bShown;

      public bool load()
      {
        byte[] dest = new byte[4];
        this.file.read(dest);
        bool flag = false;
        if (dest[0] == (byte) 80 /*0x50*/ && dest[1] == (byte) 65 && dest[2] == (byte) 77 && dest[3] == (byte) 69)
        {
          flag = true;
          this.bUnicode = false;
        }
        if (dest[0] == (byte) 80 /*0x50*/ && dest[1] == (byte) 65 && dest[2] == (byte) 77 && dest[3] == (byte) 85)
        {
          flag = true;
          this.bUnicode = true;
        }
        if (!flag)
          return false;
        this.file.setUnicode(this.bUnicode);
        if (this.file.readAShort() != (short) 770)
          return false;
        this.file.readAShort();
        this.file.readAInt();
        if (this.file.readAInt() < 249)
          return false;
        this.OIList = new COIList();
        this.imageBank = new CImageBank(this);
        this.fontBank = new CFontBank(this);
        this.soundBank = new CSoundBank();
        this.soundPlayer = new CSoundPlayer(this);
        CChunk cchunk1 = new CChunk();
        int num1 = 0;
        while (cchunk1.chID != (short) 32639)
        {
          int num2 = (int) cchunk1.readHeader(this.file);
          if (cchunk1.chSize != 0)
          {
            int pos1 = this.file.getFilePointer() + cchunk1.chSize;
            switch (cchunk1.chID)
            {
              case 8739:
                this.loadAppHeader(this.file);
                this.frameOffsets = new int[this.gaNbFrames];
                this.framePasswords = new string[this.gaNbFrames];
                for (int index = 0; index < this.gaNbFrames; ++index)
                  this.framePasswords[index] = (string) null;
                break;
              case 8740:
                this.appName = this.file.readAString();
                break;
              case 8745:
              case 8767:
                this.OIList.preLoad(this.file);
                break;
              case 8747:
                this.loadFrameHandles(this.file, cchunk1.chSize);
                break;
              case 8752:
                this.appDoc = this.file.readAString();
                break;
              case 8754:
                this.loadGlobalValues(this.file);
                break;
              case 8755:
                this.loadGlobalStrings(this.file);
                break;
              case 8756:
                this.extLoader = new CExtLoader(this);
                this.extLoader.loadList(this.file);
                break;
              case 8760:
                int length = this.file.readAInt();
                this.embeddedFiles = new CEmbeddedFile[length];
                for (int index = 0; index < length; ++index)
                {
                  this.embeddedFiles[index] = new CEmbeddedFile(this);
                  this.embeddedFiles[index].preLoad();
                }
                break;
              case 8762:
                this.appAboutText = this.file.readAString();
                break;
              case 8763:
                this.appCopyright = this.file.readAString();
                break;
              case 8773:
                this.hdr2Options = this.file.readAInt();
                this.file.skipBytes(10);
                this.hdr2Orientation = (int) this.file.readAShort();
                break;
              case 13107 /*0x3333*/:
                this.frameOffsets[this.frameMaxIndex] = this.file.getFilePointer();
                CChunk cchunk2 = new CChunk();
                while (cchunk2.chID != (short) 32639)
                {
                  int num3 = (int) cchunk2.readHeader(this.file);
                  if (cchunk2.chSize != 0)
                  {
                    int pos2 = this.file.getFilePointer() + cchunk2.chSize;
                    switch (cchunk2.chID)
                    {
                      case 13110:
                        this.framePasswords[this.frameMaxIndex] = this.file.readAString();
                        ++num1;
                        break;
                    }
                    this.file.seek(pos2);
                  }
                }
                ++this.frameMaxIndex;
                break;
              case 26214 /*0x6666*/:
                this.imageBank.preLoad();
                break;
              case 26215:
                this.fontBank.preLoad();
                break;
              case 26216:
                this.soundBank.preLoad(this);
                break;
            }
            this.file.seek(pos1);
          }
        }
        this.soundPlayer.setMultipleSounds(((int) this.gaFlags & 1024 /*0x0400*/) != 0);
        return true;
      }

      public bool startApplication()
      {
        this.sysEvents = new CArrayList();
        this.graphicsDeviceManager.PreferredBackBufferWidth = this.gaCxWin;
        this.graphicsDeviceManager.PreferredBackBufferHeight = this.gaCyWin;
        this.graphicsDeviceManager.IsFullScreen = (this.hdr2Options & 64 /*0x40*/) == 0;
        switch (this.hdr2Orientation)
        {
          case 0:
            this.graphicsDeviceManager.SupportedOrientations = DisplayOrientation.Portrait;
            break;
          case 1:
            this.graphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft;
            break;
          case 2:
            this.graphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeRight;
            break;
          case 3:
            this.graphicsDeviceManager.SupportedOrientations = DisplayOrientation.Default;
            break;
        }
        this.graphicsDeviceManager.ApplyChanges();
        this.spriteGen = new CSpriteGen();
        this.spriteGen.setOffsets(this.xOffset, this.yOffset);
        this.run = new CRun(this);
        this.services = new CServices();
        this.tempRect = new Rectangle();
        this.setFrameRate(this.gaFrameRate);
        this.numberOfTouches = 0;
        TouchPanelCapabilities capabilities = TouchPanel.GetCapabilities();
        if (capabilities.IsConnected)
          this.numberOfTouches = capabilities.MaximumTouchCount;
        this.startOrientationHandler();
        this.displayType = -1;
        this.appRunningState = 0;
        this.currentFrame = -2;
        return true;
      }

      public bool playApplication(bool bOnlyRestartApp, double time)
      {
        int num = 0;
        bool flag1 = true;
        bool flag2 = true;
        ++this.VBL;
        this.timeDouble = time;
        this.timer = (long) time;
        do
        {
          switch (this.appRunningState)
          {
            case 0:
              this.initGlobal();
              this.nextFrame = this.startFrame;
              this.appRunningState = 1;
              this.killGlobalData();
              if (bOnlyRestartApp)
              {
                flag1 = false;
                break;
              }
              goto case 1;
            case 1:
              num = this.startTheFrame();
              break;
            case 3:
              if (!this.loopFrame())
              {
                this.endFrame();
                break;
              }
              flag1 = false;
              break;
            case 5:
              this.endFrame();
              break;
            default:
              flag1 = false;
              break;
          }
        }
        while (flag1 && num == 0 && !this.quit);
        if (num != 0)
          this.appRunningState = 6;
        if (this.appRunningState == 6)
          flag2 = false;
        return flag2;
      }

      public void endApplication()
      {
      }

      public int startTheFrame()
      {
        int num1 = 0;
        if (this.nextFrame != this.currentFrame)
        {
          this.frame = new CRunFrame(this);
          if (!this.frame.loadFullFrame(this.nextFrame))
          {
            num1 = -1;
            goto label_7;
          }
          this.currentFrame = this.nextFrame;
        }
        this.frame.leX = this.frame.leY = 0;
        this.frame.leLastScrlX = this.frame.leLastScrlY = 0;
        this.frame.rhOK = false;
        this.frame.levelQuit = 0;
        int num2 = Math.Min(this.gaCxWin, this.frame.leWidth);
        int num3 = Math.Min(this.gaCyWin, this.frame.leHeight);
        this.frame.leEditWinWidth = num2;
        this.frame.leEditWinHeight = num3;
        int flags = this.frame.evtProg.getCollisionFlags() | this.frame.getMaskBits();
        this.frame.leFlags |= 32 /*0x20*/;
        this.frame.colMask = (CColMask) null;
        if ((flags & 3) != 0)
          this.frame.colMask = CColMask.create(-64, -16, this.frame.leWidth + 64 /*0x40*/, this.frame.leHeight + 16 /*0x10*/, flags);
        this.setLevelTitle();
        this.newResetCptVbl();
    label_7:
        //PhoneApplicationService.Current.UserIdleDetectionMode = ((int) this.frame.iPhoneOptions & 16 /*0x10*/) == 0 ? (IdleDetectionMode) 0 : (IdleDetectionMode) 1;
        this.bResizeWindow = true;
        this.run.setFrame(this.frame);
        this.run.initRunLoop();
        this.frame.rhPtr = this.run;
        if (this.frame.fadeIn != null)
        {
          if (!this.loopFrame())
            this.appRunningState = 5;
          else if (!this.startFrameFadeIn())
            this.appRunningState = 3;
        }
        else
          this.appRunningState = 3;
        if (num1 != 0)
          this.appRunningState = 6;
        return num1;
      }

      public bool loopFrame()
      {
        if (this.frame.levelQuit == 0)
          this.frame.levelQuit = this.run.doRunLoop();
        return this.frame.levelQuit == 0;
      }

      public void endFrame()
      {
        int ul = this.run.killRunLoop(this.frame.levelQuit, false);
        if (((int) this.gaNewFlags & 4) != 0)
        {
          this.appRunningState = 6;
        }
        else
        {
          switch (CServices.LOWORD(ul))
          {
            case 1:
              this.nextFrame = this.currentFrame + 1;
              this.appRunningState = 1;
              break;
            case 2:
              this.nextFrame = Math.Max(0, this.currentFrame - 1);
              this.appRunningState = 1;
              break;
            case 3:
              this.appRunningState = 1;
              if ((CServices.HIWORD(ul) & 32768 /*0x8000*/) != 0)
              {
                this.nextFrame = CServices.HIWORD(ul) & (int) short.MaxValue;
                if (this.nextFrame >= this.gaNbFrames)
                  this.nextFrame = this.gaNbFrames - 1;
                if (this.nextFrame < 0)
                {
                  this.nextFrame = 0;
                  break;
                }
                break;
              }
              if (CServices.HIWORD(ul) < (int) this.frameMaxHandle)
              {
                this.nextFrame = (int) this.frameHandleToIndex[CServices.HIWORD(ul)];
                if (this.nextFrame == -1)
                {
                  this.nextFrame = this.currentFrame + 1;
                  break;
                }
                break;
              }
              this.nextFrame = this.currentFrame + 1;
              break;
            case 4:
              this.appRunningState = 0;
              this.nextFrame = this.startFrame;
              break;
            default:
              this.appRunningState = 6;
              break;
          }
        }
        if (this.appRunningState == 1 && (this.nextFrame < 0 || this.nextFrame >= this.gaNbFrames))
          this.appRunningState = 6;
        if (this.appRunningState == 1 && this.nextFrame == this.currentFrame)
          return;
        this.currentFrame = -1;
      }

      public void draw()
      {
        this.spriteBatch.Begin();
        this.run.draw();
        this.spriteBatch.End();
      }

      public void killGlobalData() => this.adGO = (CArrayList) null;

      public bool startFrameFadeIn() => false;

      public bool loopFrameFadeIn() => false;

      public bool endFrameFadeIn() => true;

      public bool startFrameFadeOut() => false;

      public bool loopFrameFadeOut() => false;

      public bool endFrameFadeOut() => true;

      public void initGlobal()
      {
        if (this.parentApp == null || this.parentApp != null)
        {
          this.lives = new int[4];
          for (int index = 0; index < 4; ++index)
            this.lives[index] = this.gaLivesInit ^ -1;
        }
        else
          this.lives = (int[]) null;
        if (this.parentApp == null || this.parentApp != null)
        {
          this.scores = new int[4];
          for (int index = 0; index < 4; ++index)
            this.scores[index] = this.gaScoreInit ^ -1;
        }
        else
          this.scores = (int[]) null;
        this.playerNames = new string[4];
        for (int index = 0; index < 4; ++index)
          this.playerNames[index] = "";
        if (this.parentApp == null || this.parentApp != null)
        {
          this.gValues = new CArrayList();
          for (int index = 0; index < (int) this.nGlobalValuesInit; ++index)
            this.gValues.add((object) new CValue(this.globalValuesInit[index]));
        }
        else
          this.gValues = (CArrayList) null;
        this.tempGValue = new CValue();
        if (this.parentApp == null || this.parentApp != null)
        {
          this.gStrings = new CArrayList();
          for (int index = 0; index < (int) this.nGlobalStringsInit; ++index)
            this.gStrings.add((object) this.globalStringsInit[index]);
        }
        else
          this.gStrings = (CArrayList) null;
      }

      public int[] getLives()
      {
        CRunApp crunApp = this;
        while (crunApp.lives == null)
          crunApp = crunApp.parentApp;
        return crunApp.lives;
      }

      public int[] getScores()
      {
        CRunApp crunApp = this;
        while (crunApp.scores == null)
          crunApp = crunApp.parentApp;
        return crunApp.scores;
      }

      public short[] getCtrlType()
      {
        CRunApp crunApp = this;
        while (crunApp.parentApp != null)
          crunApp = crunApp.parentApp;
        return crunApp.pcCtrlType;
      }

      public Keys[] getCtrlKeys()
      {
        CRunApp crunApp = this;
        while (crunApp.parentApp != null)
          crunApp = crunApp.parentApp;
        return crunApp.pcCtrlKeys;
      }

      public CArrayList getGlobalValues()
      {
        CRunApp crunApp = this;
        while (crunApp.gValues == null)
          crunApp = crunApp.parentApp;
        return crunApp.gValues;
      }

      public int getNGlobalValues() => this.gValues != null ? this.gValues.size() : 0;

      public CArrayList getGlobalStrings()
      {
        CRunApp crunApp = this;
        while (crunApp.gStrings == null)
          crunApp = crunApp.parentApp;
        return crunApp.gStrings;
      }

      public int getNGlobalStrings() => this.gStrings != null ? this.gStrings.size() : 0;

      public CArrayList checkGlobalValue(int num)
      {
        CArrayList globalValues = this.getGlobalValues();
        if (num < 0 || num > 1000)
          return (CArrayList) null;
        int num1 = globalValues.size();
        if (num >= num1)
        {
          globalValues.ensureCapacity(num);
          for (int index = num1; index <= num; ++index)
            globalValues.add((object) new CValue());
        }
        return globalValues;
      }

      public CValue getGlobalValueAt(int num)
      {
        CArrayList carrayList = this.checkGlobalValue(num);
        return carrayList != null ? (CValue) carrayList.get(num) : this.tempGValue;
      }

      public void setGlobalValueAt(int num, CValue value)
      {
        ((CValue) this.checkGlobalValue(num)?.get(num)).forceValue(value);
      }

      public CArrayList checkGlobalString(int num)
      {
        CArrayList globalStrings = this.getGlobalStrings();
        if (num < 0 || num > 1000)
          return (CArrayList) null;
        int num1 = globalStrings.size();
        if (num >= num1)
        {
          globalStrings.ensureCapacity(num);
          for (int index = num1; index <= num; ++index)
            globalStrings.add((object) "");
        }
        return globalStrings;
      }

      public string getGlobalStringAt(int num)
      {
        CArrayList carrayList = this.checkGlobalString(num);
        return carrayList != null ? (string) carrayList.get(num) : "";
      }

      public void setGlobalStringAt(int num, string value)
      {
        this.checkGlobalString(num)?.set(num, (object) value);
      }

      public void loadAppHeader(CFile file)
      {
        file.skipBytes(4);
        this.gaFlags = file.readAShort();
        this.gaNewFlags = file.readAShort();
        this.gaMode = file.readAShort();
        this.gaOtherFlags = file.readAShort();
        this.gaCxWin = (int) file.readAShort();
        this.gaCyWin = (int) file.readAShort();
        this.gaScoreInit = file.readAInt();
        this.gaLivesInit = file.readAInt();
        for (int index = 0; index < 4; ++index)
        {
          short num = file.readAShort();
          if (num == (short) 0)
            num = (short) 5;
          if (num < (short) 5)
            num = (short) (1 << (int) num - 1 | 128 /*0x80*/);
          this.pcCtrlType[index] = num;
        }
        for (int index1 = 0; index1 < 4; ++index1)
        {
          for (int index2 = 0; index2 < 8; ++index2)
            this.pcCtrlKeys[index1 * 8 + index2] = CKeyConvert.getXnaKey((int) file.readAShort());
        }
        this.gaBorderColour = file.readAColor();
        this.gaNbFrames = file.readAInt();
        this.gaFrameRate = file.readAInt();
        file.skipBytes(1);
        file.skipBytes(3);
      }

      public void loadGlobalValues(CFile file)
      {
        this.nGlobalValuesInit = file.readAShort();
        this.globalValuesInit = new int[(int) this.nGlobalValuesInit];
        this.globalValuesInitTypes = new byte[(int) this.nGlobalValuesInit];
        for (int index = 0; index < (int) this.nGlobalValuesInit; ++index)
          this.globalValuesInit[index] = file.readAInt();
        file.read(this.globalValuesInitTypes);
      }

      public void loadGlobalStrings(CFile file)
      {
        this.nGlobalStringsInit = (short) file.readAInt();
        this.globalStringsInit = new string[(int) this.nGlobalStringsInit];
        for (int index = 0; index < (int) this.nGlobalStringsInit; ++index)
          this.globalStringsInit[index] = file.readAString();
      }

      public void loadFrameHandles(CFile file, int size)
      {
        this.frameMaxHandle = (short) (size / 2);
        this.frameHandleToIndex = new short[(int) this.frameMaxHandle];
        for (int index = 0; index < (int) this.frameMaxHandle; ++index)
          this.frameHandleToIndex[index] = file.readAShort();
      }

      public short HCellToNCell(short hCell)
      {
        return this.frameHandleToIndex == null || hCell == (short) -1 || (int) hCell >= (int) this.frameMaxHandle ? (short) -1 : this.frameHandleToIndex[(int) hCell];
      }

      public void showCursor(bool bShown) => this.game.IsMouseVisible = bShown;

      public int newGetCptVbl() => this.VBL;

      public void newResetCptVbl() => this.VBL = 0;

      public void setFrameRate(int fps)
      {
        this.gaFrameRate = fps;
        this.game.TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0 / (double) fps);
      }

      public void setFullScreen(bool flag)
      {
        try
        {
          if (flag)
          {
            if (this.graphicsDeviceManager.IsFullScreen)
              return;
            this.graphicsDeviceManager.ToggleFullScreen();
            this.graphicsDeviceManager.ApplyChanges();
          }
          else
          {
            if (!this.graphicsDeviceManager.IsFullScreen)
              return;
            this.graphicsDeviceManager.ToggleFullScreen();
            this.graphicsDeviceManager.ApplyChanges();
          }
        }
        catch (NoSuitableGraphicsDeviceException ex)
        {
          ex.GetType();
        }
        catch (InvalidOperationException ex)
        {
          ex.GetType();
        }
        catch (ArgumentException ex)
        {
          ex.GetType();
        }
      }

      private void setLevelTitle()
      {
      }

      private void startOrientationHandler()
      {
        CRunApp.orientation = this.game.Window.CurrentOrientation;
        this.game.Window.OrientationChanged += new EventHandler<EventArgs>(this.orientationChanged);
      }

      private void orientationChanged(object sender, EventArgs e)
      {
        CRunApp.orientation = this.game.Window.CurrentOrientation;
      }

      /* public void startAccelerometer()
      {
        ++this.nAccel;
        if (this.nAccel != 1)
          return;
        if (this.accel == null)
        {
          this.accel = new Accelerometer();
          this.accel.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(CRunApp.accelerometerChanged);
        }
        this.accel.Start();
      }

      public void stopAccelerometer()
      {
        --this.nAccel;
        if (this.nAccel != 0)
          return;
        this.accel.Stop();
      }

      private static void accelerometerChanged(object sender, AccelerometerReadingEventArgs e)
      {
        switch (CRunApp.orientation)
        {
          case DisplayOrientation.LandscapeLeft:
            CRunApp.accX = -(float) e.Y;
            CRunApp.accY = -(float) e.X;
            CRunApp.accZ = (float) e.Z;
            break;
          case DisplayOrientation.LandscapeRight:
            CRunApp.accX = (float) e.Y;
            CRunApp.accY = (float) e.X;
            CRunApp.accZ = (float) e.Z;
            break;
          default:
            CRunApp.accX = (float) e.X;
            CRunApp.accY = -(float) e.Y;
            CRunApp.accZ = (float) e.Z;
            break;
        }
        CRunApp.filteredAccX = (float) ((double) CRunApp.accX * 0.10000000149011612 + (double) CRunApp.filteredAccX * 0.89999999850988388);
        CRunApp.filteredAccY = (float) ((double) CRunApp.accY * 0.10000000149011612 + (double) CRunApp.filteredAccY * 0.89999999850988388);
        CRunApp.filteredAccZ = (float) ((double) CRunApp.accZ * 0.10000000149011612 + (double) CRunApp.filteredAccZ * 0.89999999850988388);
        CRunApp.instantAccX = CRunApp.accX - (float) ((double) CRunApp.accX * 0.10000000149011612 + (double) CRunApp.instantAccX * 0.89999999850988388);
        CRunApp.instantAccY = CRunApp.accX - (float) ((double) CRunApp.accY * 0.10000000149011612 + (double) CRunApp.instantAccY * 0.89999999850988388);
        CRunApp.instantAccZ = CRunApp.accZ - (float) ((double) CRunApp.accZ * 0.10000000149011612 + (double) CRunApp.instantAccZ * 0.89999999850988388);
      } */

      public CEmbeddedFile getEmbeddedFile(string path)
      {
        if (this.embeddedFiles != null)
        {
          for (int index = 0; index < this.embeddedFiles.Length; ++index)
          {
            if (string.Compare(this.embeddedFiles[index].path, path) == 0)
              return this.embeddedFiles[index];
          }
        }
        return (CEmbeddedFile) null;
      }
    }
}
