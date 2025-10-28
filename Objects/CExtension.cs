// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Objects.CExtension
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RuntimeXNA.Actions;
using RuntimeXNA.Banks;
using RuntimeXNA.Conditions;
using RuntimeXNA.Expressions;
using RuntimeXNA.Extensions;
using RuntimeXNA.Movements;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;

namespace RuntimeXNA.Objects
{

    public class CExtension : CObject, IDrawing
    {
      public CRunExtension ext;
      private bool noHandle;
      public int privateData;
      public int objectCount;
      public int objectNumber;

      public CExtension(int type, CRun rhPtr) => this.ext = rhPtr.rhApp.extLoader.loadRunObject(type);

      public override void init(CObjectCommon ocPtr, CCreateObjectInfo cob)
      {
        this.ext.init(this);
        CFile file = (CFile) null;
        if (ocPtr.ocExtension != null)
        {
          file = new CFile(ocPtr.ocExtension);
          file.setUnicode(this.hoAdRunHeader.rhApp.bUnicode);
        }
        this.privateData = ocPtr.ocPrivate;
        this.ext.createRunObject(file, cob, ocPtr.ocVersion);
      }

      public override void handle()
      {
        if ((this.hoOEFlags & 512 /*0x0200*/) != 0)
          this.ros.handle();
        else if ((this.hoOEFlags & 48 /*0x30*/) == 16 /*0x10*/ || (this.hoOEFlags & 48 /*0x30*/) == 48 /*0x30*/)
          this.rom.move();
        else if ((this.hoOEFlags & 48 /*0x30*/) == 32 /*0x20*/)
          this.roa.animate();
        int num = 0;
        if (!this.noHandle)
          num = this.ext.handleRunObject();
        if ((num & 2) != 0)
          this.noHandle = true;
        if (this.roc != null && this.roc.rcChanged)
        {
          num |= 1;
          this.roc.rcChanged = false;
        }
        if ((num & 1) == 0)
          return;
        this.modif();
      }

      public override void modif()
      {
        if (this.ros != null)
          this.ros.modifRoutine();
        else if ((this.hoOEFlags & 2) != 0)
          this.hoAdRunHeader.redrawLevel(2);
        else
          this.ext.displayRunObject((SpriteBatchEffect) null);
      }

      public override void display()
      {
      }

      public override void kill(bool bFast) => this.ext.destroyRunObject(bFast);

      public override void getZoneInfos()
      {
        this.ext.getZoneInfos();
        this.hoRect.left = this.hoX - this.hoAdRunHeader.rhWindowX - this.hoImgXSpot;
        this.hoRect.right = this.hoRect.left + this.hoImgWidth;
        this.hoRect.top = this.hoY - this.hoAdRunHeader.rhWindowY - this.hoImgYSpot;
        this.hoRect.bottom = this.hoRect.top + this.hoImgHeight;
      }

      public override CMask getCollisionMask(int flags) => this.ext.getRunObjectCollisionMask(flags);

      public override void draw(SpriteBatchEffect batch)
      {
        Texture2D runObjectSurface = this.ext.getRunObjectSurface();
        if (runObjectSurface != null)
          batch.Draw(runObjectSurface, new Rectangle()
          {
            X = this.hoRect.left + this.hoAdRunHeader.rhApp.xOffset,
            Y = this.hoRect.top + this.hoAdRunHeader.rhApp.yOffset,
            Width = this.hoRect.right - this.hoRect.left,
            Height = this.hoRect.bottom - this.hoRect.top
          }, new Rectangle?(), Color.White);
        else
          this.ext.displayRunObject(batch);
      }

      public override void drawableDraw(
        SpriteBatchEffect batch,
        CSprite sprite,
        CImageBank bank,
        int x,
        int y)
      {
        this.draw(batch);
      }

      public override void drawableKill()
      {
      }

      public override CMask drawableGetMask(int flags) => this.ext.getRunObjectCollisionMask(flags);

      public virtual bool condition(int num, CCndExtension cnd) => this.ext.condition(num, cnd);

      public virtual void action(int num, CActExtension act) => this.ext.action(num, act);

      public virtual CValue expression(int num) => this.ext.expression(num);

      public int getX() => this.hoX;

      public int getY() => this.hoY;

      public int getWidth() => this.hoImgWidth;

      public int getHeight() => this.hoImgHeight;

      public void setX(int x)
      {
        if (this.rom != null)
        {
          this.rom.rmMovement.setXPosition(x);
        }
        else
        {
          this.hoX = x;
          if (this.roc == null)
            return;
          this.roc.rcChanged = true;
          this.roc.rcCheckCollides = true;
        }
      }

      public void setY(int y)
      {
        if (this.rom != null)
        {
          this.rom.rmMovement.setYPosition(y);
        }
        else
        {
          this.hoY = y;
          if (this.roc == null)
            return;
          this.roc.rcChanged = true;
          this.roc.rcCheckCollides = true;
        }
      }

      public void setWidth(int width)
      {
        this.hoImgWidth = width;
        this.hoRect.right = this.hoRect.left + width;
      }

      public void setHeight(int height)
      {
        this.hoImgHeight = height;
        this.hoRect.bottom = this.hoRect.top + height;
      }

      public virtual void loadImageList(short[] list)
      {
        this.hoAdRunHeader.rhApp.imageBank.loadImageList(list);
      }

      public virtual CImage getImage(short handle)
      {
        return this.hoAdRunHeader.rhApp.imageBank.getImageFromHandle(handle);
      }

      public virtual void reHandle() => this.noHandle = false;

      public virtual void generateEvent(int code, int param)
      {
        if (this.hoAdRunHeader.rh2PauseCompteur != 0)
          return;
        int rhCurParam0 = this.hoAdRunHeader.rhEvtProg.rhCurParam0;
        this.hoAdRunHeader.rhEvtProg.rhCurParam0 = param;
        code = -(code + 80 /*0x50*/ + 1) << 16 /*0x10*/;
        code |= (int) this.hoType & (int) ushort.MaxValue;
        this.hoAdRunHeader.rhEvtProg.handle_Event((CObject) this, code);
        this.hoAdRunHeader.rhEvtProg.rhCurParam0 = rhCurParam0;
      }

      public virtual void pushEvent(int code, int param)
      {
        if (this.hoAdRunHeader.rh2PauseCompteur != 0)
          return;
        code = -(code + 80 /*0x50*/ + 1) << 16 /*0x10*/;
        code |= (int) this.hoType & (int) ushort.MaxValue;
        this.hoAdRunHeader.rhEvtProg.push_Event(1, code, param, (CObject) this, this.hoOi);
      }

      public virtual void pause() => this.hoAdRunHeader.pause();

      public virtual void resume() => this.hoAdRunHeader.resume();

      public virtual void redisplay() => this.hoAdRunHeader.ohRedrawLevel(true);

      public virtual void redraw()
      {
        this.modif();
        if ((this.hoOEFlags & 560) == 0)
          return;
        this.roc.rcChanged = true;
      }

      public virtual void destroy() => this.hoAdRunHeader.destroy_Add((int) this.hoNumber);

      public virtual void setPosition(int x, int y)
      {
        if (this.rom != null)
        {
          this.rom.rmMovement.setXPosition(x);
          this.rom.rmMovement.setYPosition(y);
        }
        else
        {
          this.hoX = x;
          this.hoY = y;
          if (this.roc == null)
            return;
          this.roc.rcChanged = true;
          this.roc.rcCheckCollides = true;
        }
      }

      public virtual void addBackdrop(
        CImage img,
        int x,
        int y,
        int dwEffect,
        int dwEffectParam,
        int typeObst,
        int nLayer)
      {
      }

      public int getEventCount() => this.hoAdRunHeader.rh4EventCount;

      public CValue getExpParam()
      {
        ++this.hoAdRunHeader.rh4CurToken;
        return this.hoAdRunHeader.getExpression();
      }

      public int getEventParam() => this.hoAdRunHeader.rhEvtProg.rhCurParam0;

      public virtual double callMovement(CObject hoPtr, int action, double param)
      {
        return (hoPtr.hoOEFlags & 16 /*0x10*/) != 0 && hoPtr.roc.rcMovementType == 14 ? ((CMoveExtension) hoPtr.rom.rmMovement).callMovement(action, param) : 0.0;
      }

      public virtual CValue callExpression(CObject hoPtr, int action, int param)
      {
        CExtension cextension = (CExtension) hoPtr;
        cextension.privateData = param;
        return cextension.expression(action);
      }

      public virtual CObject getObjectFromFixed(int fixed_Renamed)
      {
        int index1 = 0;
        for (int index2 = 0; index2 < this.hoAdRunHeader.rhNObjects; ++index2)
        {
          while (this.hoAdRunHeader.rhObjectList[index1] == null)
            ++index1;
          CObject rhObject = this.hoAdRunHeader.rhObjectList[index1];
          ++index1;
          if (((int) rhObject.hoCreationId << 16 /*0x10*/ | (int) rhObject.hoNumber & (int) ushort.MaxValue) == fixed_Renamed)
            return rhObject;
        }
        return (CObject) null;
      }

      public CObject getFirstObject()
      {
        this.objectCount = 0;
        this.objectNumber = 0;
        return this.getNextObject();
      }

      public CObject getNextObject()
      {
        if (this.objectNumber >= this.hoAdRunHeader.rhNObjects)
          return (CObject) null;
        while (this.hoAdRunHeader.rhObjectList[this.objectCount] == null)
          ++this.objectCount;
        CObject rhObject = this.hoAdRunHeader.rhObjectList[this.objectCount];
        ++this.objectNumber;
        ++this.objectCount;
        return rhObject;
      }
    }
}
