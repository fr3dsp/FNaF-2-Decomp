// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveBullet
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Movements
{

    internal class CMoveBullet : CMove
    {
      public bool MBul_Wait;
      public CObject MBul_ShootObject;

      public override void init(CObject ho, CMoveDef mvPtr)
      {
        this.hoPtr = ho;
        if (this.hoPtr.roc.rcSprite != null)
        {
          int num = (int) this.hoPtr.roc.rcSprite.setSpriteColFlag(0U);
        }
        if (this.hoPtr.ros != null)
        {
          this.hoPtr.ros.rsFlags &= (short) -33;
          this.hoPtr.ros.obHide();
        }
        this.MBul_Wait = true;
        this.hoPtr.hoCalculX = 0;
        this.hoPtr.hoCalculY = 0;
        if (this.hoPtr.roa != null)
          this.hoPtr.roa.init_Animation(1);
        this.hoPtr.roc.rcSpeed = 0;
        this.hoPtr.roc.rcCheckCollides = true;
        this.hoPtr.roc.rcChanged = true;
      }

      public void init2(CObject parent)
      {
        this.hoPtr.roc.rcMaxSpeed = this.hoPtr.roc.rcSpeed;
        this.hoPtr.roc.rcMinSpeed = this.hoPtr.roc.rcSpeed;
        this.MBul_ShootObject = parent;
      }

      public override void move()
      {
        if (this.MBul_Wait)
        {
          if (this.MBul_ShootObject.roa != null && this.MBul_ShootObject.roa.raAnimOn == 6)
            return;
          this.startBullet();
        }
        if (this.hoPtr.roa != null)
        {
          this.hoPtr.roa.animate();
          if (CRun.bMoveChanged)
            return;
        }
        this.newMake_Move(this.hoPtr.roc.rcSpeed, this.hoPtr.roc.rcDir);
        if (CRun.bMoveChanged)
          return;
        if (this.hoPtr.hoX < -64 || this.hoPtr.hoX > this.hoPtr.hoAdRunHeader.rhLevelSx + 64 /*0x40*/ || this.hoPtr.hoY < -64 || this.hoPtr.hoY > this.hoPtr.hoAdRunHeader.rhLevelSy + 64 /*0x40*/)
        {
          this.hoPtr.hoCallRoutine = false;
          this.hoPtr.hoAdRunHeader.destroy_Add((int) this.hoPtr.hoNumber);
        }
        if (!this.hoPtr.roc.rcCheckCollides)
          return;
        this.hoPtr.roc.rcCheckCollides = false;
        this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr);
      }

      public void startBullet()
      {
        if (this.hoPtr.roc.rcSprite != null)
        {
          int num = (int) this.hoPtr.roc.rcSprite.setSpriteColFlag(1U);
        }
        if (this.hoPtr.ros != null)
        {
          this.hoPtr.ros.rsFlags |= (short) 32 /*0x20*/;
          this.hoPtr.ros.obShow();
        }
        this.MBul_Wait = false;
        this.MBul_ShootObject = (CObject) null;
      }

      public override void setXPosition(int x)
      {
        if (this.hoPtr.hoX == x)
          return;
        this.hoPtr.hoX = x;
        this.hoPtr.rom.rmMoveFlag = true;
        this.hoPtr.roc.rcChanged = true;
        this.hoPtr.roc.rcCheckCollides = true;
      }

      public override void setYPosition(int y)
      {
        if (this.hoPtr.hoY == y)
          return;
        this.hoPtr.hoY = y;
        this.hoPtr.rom.rmMoveFlag = true;
        this.hoPtr.roc.rcChanged = true;
        this.hoPtr.roc.rcCheckCollides = true;
      }
    }
}
