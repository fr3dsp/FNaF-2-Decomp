// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CRunMvtExtension
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    public abstract class CRunMvtExtension
    {
      public CObject ho;
      public CRun rh;

      public virtual void init(CObject hoPtr)
      {
        this.ho = hoPtr;
        this.rh = this.ho.hoAdRunHeader;
      }

      public virtual void initialize(CFile file)
      {
      }

      public virtual void kill()
      {
      }

      public virtual bool move() => false;

      public virtual void stop(bool bCurrent)
      {
      }

      public virtual void bounce(bool bCurrent)
      {
      }

      public virtual void reverse()
      {
      }

      public virtual void start()
      {
      }

      public virtual int extension(int function, int param) => 0;

      public virtual double actionEntry(int action) => 0.0;

      public virtual void setPosition(int x, int y)
      {
      }

      public virtual void setXPosition(int value)
      {
      }

      public virtual void setYPosition(int Values)
      {
      }

      public virtual void setSpeed(int value)
      {
      }

      public virtual int getSpeed() => 0;

      public virtual void setMaxSpeed(int speed)
      {
      }

      public virtual void setDir(int dir)
      {
      }

      public virtual void set8Dirs(int dirs)
      {
      }

      public virtual void setAcc(int acc)
      {
      }

      public virtual void setDec(int dec)
      {
      }

      public virtual void setRotSpeed(int speed)
      {
      }

      public virtual void setGravity(int gravity)
      {
      }

      public virtual int getGravity() => 0;

      public virtual void setAcceleration(int acc)
      {
      }

      public virtual int getAcceleration() => 0;

      public virtual void setDeceleration(int dec)
      {
      }

      public virtual int getDeceleration() => 0;

      public double getParamDouble() => ((CMoveExtension) this.ho.rom.rmMovement).callParam;

      public virtual int dirAtStart(int dir) => this.ho.rom.dirAtStart(this.ho, dir, 32 /*0x20*/);

      public virtual void animations(int anm)
      {
        this.ho.roc.rcAnim = anm;
        if (this.ho.roa == null)
          return;
        this.ho.roa.animate();
      }

      public virtual void collisions()
      {
        ++this.ho.hoAdRunHeader.rh3CollisionCount;
        this.ho.rom.rmMovement.rmCollisionCount = this.ho.hoAdRunHeader.rh3CollisionCount;
        this.ho.hoAdRunHeader.newHandle_Collisions(this.ho);
      }

      public virtual bool approachObject(
        int destX,
        int destY,
        int originX,
        int originY,
        int htFoot,
        int planCol,
        CPoint ptDest)
      {
        destX -= this.ho.hoAdRunHeader.rhWindowX;
        destY -= this.ho.hoAdRunHeader.rhWindowY;
        originX -= this.ho.hoAdRunHeader.rhWindowX;
        originY -= this.ho.hoAdRunHeader.rhWindowY;
        bool flag = this.ho.rom.rmMovement.mpApproachSprite(destX, destY, originX, originY, (short) htFoot, (short) planCol, ptDest);
        ptDest.x += this.ho.hoAdRunHeader.rhWindowX;
        ptDest.y += this.ho.hoAdRunHeader.rhWindowY;
        return flag;
      }

      public virtual bool moveIt()
      {
        return this.ho.rom.rmMovement.newMake_Move(this.ho.roc.rcSpeed, this.ho.roc.rcDir);
      }

      public virtual bool testPosition(int x, int y, int htFoot, int planCol, bool flag)
      {
        return this.ho.rom.rmMovement.tst_SpritePosition(x, y, (short) htFoot, (short) planCol, flag);
      }

      public virtual byte getJoystick(int player) => this.ho.hoAdRunHeader.rhPlayer[player];

      public virtual bool colMaskTestRect(int x, int y, int sx, int sy, int layer, int plan)
      {
        return !this.ho.hoAdRunHeader.rhFrame.bkdCol_TestRect(x - this.ho.hoAdRunHeader.rhWindowX, y - this.ho.hoAdRunHeader.rhWindowY, sx, sy, layer, plan);
      }

      public virtual bool colMaskTestPoint(int x, int y, int layer, int plan)
      {
        return !this.ho.hoAdRunHeader.rhFrame.bkdCol_TestPoint(x - this.ho.hoAdRunHeader.rhWindowX, y - this.ho.hoAdRunHeader.rhWindowY, layer, plan);
      }
    }
}
