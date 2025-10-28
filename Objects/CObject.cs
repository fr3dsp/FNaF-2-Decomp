// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Objects.CObject
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Animations;
using RuntimeXNA.Banks;
using RuntimeXNA.Movements;
using RuntimeXNA.OI;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;
using RuntimeXNA.Values;

namespace RuntimeXNA.Objects
{

    public class CObject : IDrawing
    {
      public const short HOF_DESTROYED = 1;
      public const short HOF_TRUEEVENT = 2;
      public const short HOF_REALSPRITE = 4;
      public const short HOF_FADEIN = 8;
      public const short HOF_FADEOUT = 16 /*0x10*/;
      public const short HOF_OWNERDRAW = 32 /*0x20*/;
      public const short HOF_NOCOLLISION = 8192 /*0x2000*/;
      public const short HOF_FLOAT = 16384 /*0x4000*/;
      public const short HOF_STRING = -32768 /*0x8000*/;
      public short hoNumber;
      public short hoNextSelected;
      public CRun hoAdRunHeader;
      public short hoHFII;
      public short hoOi;
      public short hoNumPrev;
      public short hoNumNext;
      public short hoType;
      public short hoCreationId;
      public CObjInfo hoOiList;
      public int hoEvents;
      public CArrayList hoPrevNoRepeat;
      public CArrayList hoBaseNoRepeat;
      public int hoMark1;
      public int hoMark2;
      public string hoMT_NodeName;
      public int hoEventNumber;
      public CObjectCommon hoCommon;
      public int hoCalculX;
      public int hoX;
      public int hoCalculY;
      public int hoY;
      public int hoImgXSpot;
      public int hoImgYSpot;
      public int hoImgWidth;
      public int hoImgHeight;
      public CRect hoRect = new CRect();
      public int hoOEFlags;
      public short hoFlags;
      public byte hoSelectedInOR;
      public int hoOffsetValue;
      public int hoLayer;
      public short hoLimitFlags;
      public short hoPreviousQuickDisplay;
      public short hoNextQuickDisplay;
      public int hoCurrentParam;
      public int hoIdentifier;
      public bool hoCallRoutine;
      public CRCom roc;
      public CRMvt rom;
      public CRAni roa;
      public CRVal rov;
      public CRSpr ros;

      public void setScale(float fScaleX, float fScaleY, bool bResample)
      {
        if ((double) this.roc.rcScaleX == (double) fScaleX && (double) this.roc.rcScaleY == (double) fScaleY)
          return;
        this.roc.rcScaleX = fScaleX;
        this.roc.rcScaleY = fScaleY;
        this.roc.rcChanged = true;
        CImage imageInfoEx = this.hoAdRunHeader.rhApp.imageBank.getImageInfoEx(this.roc.rcImage, this.roc.rcAngle, this.roc.rcScaleX, this.roc.rcScaleY);
        this.hoImgWidth = (int) imageInfoEx.width;
        this.hoImgHeight = (int) imageInfoEx.height;
        this.hoImgXSpot = (int) imageInfoEx.xSpot;
        this.hoImgYSpot = (int) imageInfoEx.ySpot;
      }

      public void shtCreate(PARAM_SHOOT p, int x, int y, int dir)
      {
        int hoLayer = this.hoLayer;
        int index = this.hoAdRunHeader.f_CreateObject(p.cdpHFII, p.cdpOi, x, y, dir, (short) 3, hoLayer, -1);
        if (index < 0)
          return;
        CObject rhObject = this.hoAdRunHeader.rhObjectList[index];
        if (rhObject.rom != null)
        {
          rhObject.rom.initSimple(rhObject, 13, false);
          rhObject.roc.rcDir = dir;
          rhObject.roc.rcSpeed = (int) p.shtSpeed;
          ((CMoveBullet) rhObject.rom.rmMovement).init2(this);
          if (hoLayer != -1 && (rhObject.hoOEFlags & 512 /*0x0200*/) != 0 && (this.hoAdRunHeader.rhFrame.layers[hoLayer].dwOptions & 131088 /*0x020010*/) != 16 /*0x10*/)
            rhObject.ros.obHide();
          this.hoAdRunHeader.rhEvtProg.evt_AddCurrentObject(rhObject);
          if ((this.hoOEFlags & 32 /*0x20*/) == 0 || !this.roa.anim_Exist(6))
            return;
          this.roa.animation_Force(6);
          this.roa.animation_OneLoop();
        }
        else
          this.hoAdRunHeader.destroy_Add((int) rhObject.hoNumber);
      }

      public virtual void init(CObjectCommon ocPtr, CCreateObjectInfo cob)
      {
      }

      public virtual void handle()
      {
      }

      public virtual void modif()
      {
      }

      public virtual void display()
      {
      }

      public virtual void kill(bool bFast)
      {
      }

      public virtual void killBack()
      {
      }

      public virtual void getZoneInfos()
      {
      }

      public virtual void draw(SpriteBatchEffect batch)
      {
      }

      public virtual CMask getCollisionMask(int flags) => (CMask) null;

      public virtual void drawableDraw(
        SpriteBatchEffect batch,
        CSprite sprite,
        CImageBank bank,
        int x,
        int y)
      {
      }

      public virtual void drawableKill()
      {
      }

      public virtual CMask drawableGetMask(int flags) => (CMask) null;
    }
}
