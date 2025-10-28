// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Objects.CCCA
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;
using RuntimeXNA.Expressions;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;

namespace RuntimeXNA.Objects
{

    internal class CCCA : CObject
    {
      public const int CCAF_SHARE_GLOBALVALUES = 1;
      public const int CCAF_SHARE_LIVES = 2;
      public const int CCAF_SHARE_SCORES = 4;
      public const int CCAF_SHARE_WINATTRIB = 8;
      public const int CCAF_STRETCH = 16 /*0x10*/;
      public const int CCAF_POPUP = 32 /*0x20*/;
      public const int CCAF_CAPTION = 64 /*0x40*/;
      public const int CCAF_TOOLCAPTION = 128 /*0x80*/;
      public const int CCAF_BORDER = 256 /*0x0100*/;
      public const int CCAF_WINRESIZE = 512 /*0x0200*/;
      public const int CCAF_SYSMENU = 1024 /*0x0400*/;
      public const int CCAF_DISABLECLOSE = 2048 /*0x0800*/;
      public const int CCAF_MODAL = 4096 /*0x1000*/;
      public const int CCAF_DIALOGFRAME = 8192 /*0x2000*/;
      public const int CCAF_INTERNAL = 16384 /*0x4000*/;
      public const int CCAF_HIDEONCLOSE = 32768 /*0x8000*/;
      public const int CCAF_CUSTOMSIZE = 65536 /*0x010000*/;
      public const int CCAF_INTERNALABOUTBOX = 131072 /*0x020000*/;
      public const int CCAF_CLIPSIBLINGS = 262144 /*0x040000*/;
      public const int CCAF_SHARE_PLAYERCTRLS = 524288 /*0x080000*/;
      public const int CCAF_MDICHILD = 1048576 /*0x100000*/;
      public const int CCAF_DOCKED = 2097152 /*0x200000*/;
      public const int CCAF_DOCKING_AREA = 12582912 /*0xC00000*/;
      public const int CCAF_DOCKED_LEFT = 0;
      public const int CCAF_DOCKED_TOP = 4194304 /*0x400000*/;
      public const int CCAF_DOCKED_RIGHT = 8388608 /*0x800000*/;
      public const int CCAF_DOCKED_BOTTOM = 12582912 /*0xC00000*/;
      public const int CCAF_REOPEN = 16777216 /*0x01000000*/;
      public const int CCAF_MDIRUNEVENIFNOTACTIVE = 33554432 /*0x02000000*/;
      public const int CCAF_HIDDENATSTART = 67108864 /*0x04000000*/;
      internal int flags;
      internal int odOptions;
      internal CRunApp subApp;
      internal int level;
      internal int oldLevel;
      private bool bPaused;

      public void startCCA(CObjectCommon ocPtr, bool bInit, int nStartFrame)
      {
        CDefCCA ocObject = (CDefCCA) ocPtr.ocObject;
        this.hoImgWidth = ocObject.odCx;
        this.hoImgHeight = ocObject.odCy;
        this.odOptions = ocObject.odOptions;
        if ((this.odOptions & 16 /*0x10*/) != 0)
          this.odOptions |= 65536 /*0x010000*/;
        if (nStartFrame == -1)
        {
          nStartFrame = 0;
          if ((this.odOptions & 16384 /*0x4000*/) != 0)
            nStartFrame = (int) ocObject.odNStartFrame;
        }
        if (ocObject.odName == null || ocObject.odName.Length != 0 || (this.odOptions & 16384 /*0x4000*/) == 0 || nStartFrame >= this.hoAdRunHeader.rhApp.gaNbFrames || nStartFrame == this.hoAdRunHeader.rhApp.currentFrame)
          return;
        this.subApp = new CRunApp(this.hoAdRunHeader.rhApp.game, new CFile(this.hoAdRunHeader.rhApp.file));
        this.subApp.setParentApp(this.hoAdRunHeader.rhApp, nStartFrame, this.odOptions, this.hoX - this.hoAdRunHeader.rhWindowX, this.hoY - this.hoAdRunHeader.rhWindowY, this.hoImgWidth, this.hoImgHeight);
        this.subApp.showSubApp(((int) ocPtr.ocFlags2 & 8) != 0);
        this.subApp.load();
        this.subApp.startApplication();
        this.subApp.playApplication(true, this.hoAdRunHeader.rhApp.timeDouble);
        ++this.hoAdRunHeader.nSubApps;
      }

      public override void init(CObjectCommon ocPtr, CCreateObjectInfo cob)
      {
        this.startCCA(ocPtr, true, -1);
      }

      public override void handle()
      {
        this.rom.move();
        if (this.subApp == null)
          return;
        this.subApp.setOffsets(this.hoX - this.hoAdRunHeader.rhWindowX, this.hoY - this.hoAdRunHeader.rhWindowY);
        if (!this.subApp.playApplication(false, this.hoAdRunHeader.rhApp.timeDouble))
        {
          this.subApp.endApplication();
          this.subApp = (CRunApp) null;
        }
        else
        {
          this.oldLevel = this.level;
          this.level = this.subApp.currentFrame;
        }
      }

      public override void kill(bool bFast)
      {
        if (this.subApp == null)
          return;
        if (this.subApp.appRunningState == 3)
          this.subApp.endFrame();
        this.subApp.endApplication();
        this.subApp = (CRunApp) null;
        --this.hoAdRunHeader.nSubApps;
      }

      public virtual void restartApp()
      {
        if (this.subApp != null)
        {
          if (this.subApp.run != null)
          {
            this.subApp.run.rhQuit = (short) 4;
            return;
          }
          this.kill(true);
          --this.hoAdRunHeader.nSubApps;
        }
        this.startCCA(this.hoCommon, false, -1);
      }

      public virtual void endApp()
      {
        if (this.subApp == null || this.subApp.run == null)
          return;
        this.subApp.run.rhQuit = (short) -2;
      }

      public virtual void hide()
      {
        if (this.subApp == null)
          return;
        this.subApp.showSubApp(false);
      }

      public virtual void show()
      {
        if (this.subApp == null)
          return;
        this.subApp.showSubApp(true);
      }

      public virtual void jumpFrame(int frame)
      {
        if (this.subApp == null || this.subApp.run == null)
          return;
        this.subApp.run.rhQuit = (short) 3;
        this.subApp.run.rhQuitParam = 32768 /*0x8000*/ | frame;
      }

      public virtual void nextFrame()
      {
        if (this.subApp == null || this.subApp.run == null)
          return;
        this.subApp.run.rhQuit = (short) 1;
      }

      public virtual void previousFrame()
      {
        if (this.subApp == null || this.subApp.run == null)
          return;
        this.subApp.run.rhQuit = (short) 2;
      }

      public virtual void restartFrame()
      {
        if (this.subApp == null || this.subApp.run == null)
          return;
        this.subApp.run.rhQuit = (short) 101;
      }

      public virtual void pause()
      {
        if (this.subApp == null)
          return;
        this.bPaused = true;
        if (this.subApp.run == null)
          return;
        this.subApp.run.pause();
      }

      public virtual void resume()
      {
        if (this.subApp == null)
          return;
        this.bPaused = false;
        if (this.subApp.run == null)
          return;
        this.subApp.run.resume();
      }

      public virtual void setGlobalValue(int number, CValue value_Renamed)
      {
        if (this.subApp == null)
          return;
        this.subApp.setGlobalValueAt(number, value_Renamed);
      }

      public virtual void setGlobalString(int number, string value_Renamed)
      {
        if (this.subApp == null)
          return;
        this.subApp.setGlobalStringAt(number, value_Renamed);
      }

      public virtual bool appFinished() => this.subApp == null;

      public virtual bool frameChanged() => this.level != this.oldLevel;

      public virtual string getGlobalString(int num)
      {
        return this.subApp != null ? this.subApp.getGlobalStringAt(num) : "";
      }

      public virtual CValue getGlobalValue(int num)
      {
        return this.subApp != null ? this.subApp.getGlobalValueAt(num) : new CValue(0);
      }

      public bool isVisible() => this.subApp != null && this.subApp.bSubAppShown;

      public bool isPaused() => this.bPaused;

      public override void draw(SpriteBatchEffect batch)
      {
        if (this.subApp == null || this.subApp.run == null)
          return;
        this.subApp.run.screen_Update();
      }
    }
}
