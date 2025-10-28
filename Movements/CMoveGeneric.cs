// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveGeneric
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Movements
{

    internal class CMoveGeneric : CMove
    {
      public int MG_Bounce;
      public int MG_OkDirs;
      public int MG_BounceMu;
      public int MG_Speed;
      public int MG_LastBounce;
      public int MG_DirMask;

      public override void init(CObject ho, CMoveDef mvPtr)
      {
        this.hoPtr = ho;
        CMoveDefGeneric cmoveDefGeneric = (CMoveDefGeneric) mvPtr;
        this.hoPtr.hoCalculX = 0;
        this.hoPtr.hoCalculY = 0;
        this.MG_Speed = 0;
        this.hoPtr.roc.rcSpeed = 0;
        this.MG_Bounce = 0;
        this.MG_LastBounce = -1;
        this.hoPtr.roc.rcPlayer = (int) mvPtr.mvControl;
        this.rmAcc = (int) cmoveDefGeneric.mgAcc;
        this.rmAccValue = this.getAccelerator(this.rmAcc);
        this.rmDec = (int) cmoveDefGeneric.mgDec;
        this.rmDecValue = this.getAccelerator(this.rmDec);
        this.hoPtr.roc.rcMaxSpeed = (int) cmoveDefGeneric.mgSpeed;
        this.hoPtr.roc.rcMinSpeed = 0;
        this.MG_BounceMu = (int) cmoveDefGeneric.mgBounceMult;
        this.MG_OkDirs = cmoveDefGeneric.mgDir;
        this.rmOpt = cmoveDefGeneric.mvOpt;
        this.hoPtr.roc.rcChanged = true;
      }

      public override void move()
      {
        this.hoPtr.hoAdRunHeader.rhVBLObjet = 1;
        int num1 = this.hoPtr.roc.rcDir;
        this.hoPtr.roc.rcOldDir = num1;
        if (this.MG_Bounce == 0)
        {
          this.hoPtr.rom.rmBouncing = false;
          int num2 = 0;
          int index = (int) this.hoPtr.hoAdRunHeader.rhPlayer[this.hoPtr.roc.rcPlayer - 1] & 15;
          if (index != 0)
          {
            int num3 = (int) CMove.Joy2Dir[index];
            if (num3 != -1 && (1 << num3 & this.MG_OkDirs) != 0)
            {
              num2 = 1;
              num1 = num3;
            }
          }
          int num4 = this.MG_Speed;
          if (num2 == 0)
          {
            if (num4 != 0)
            {
              int num5 = this.rmDecValue;
              if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
                num5 = (int) ((double) num5 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
              num4 -= num5;
              if (num4 <= 0)
                num4 = 0;
            }
          }
          else if (num4 >> 8 < this.hoPtr.roc.rcMaxSpeed)
          {
            int num6 = this.rmAccValue;
            if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
              num6 = (int) ((double) num6 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
            num4 += num6;
            if (num4 >> 8 > this.hoPtr.roc.rcMaxSpeed)
              num4 = this.hoPtr.roc.rcMaxSpeed << 8;
          }
          this.MG_Speed = num4;
          this.hoPtr.roc.rcSpeed = num4 >> 8;
          this.hoPtr.roc.rcDir = num1;
          this.hoPtr.roc.rcAnim = 1;
          if (this.hoPtr.roa != null)
          {
            this.hoPtr.roa.animate();
            if (CRun.bMoveChanged)
              return;
          }
          if (!this.newMake_Move(this.hoPtr.roc.rcSpeed, this.hoPtr.roc.rcDir) || CRun.bMoveChanged)
            return;
          if (this.hoPtr.roc.rcSpeed == 0)
          {
            int mgSpeed = this.MG_Speed;
            if (mgSpeed == 0 || this.hoPtr.roc.rcOldDir == this.hoPtr.roc.rcDir)
              return;
            this.hoPtr.roc.rcSpeed = mgSpeed >> 8;
            this.hoPtr.roc.rcDir = this.hoPtr.roc.rcOldDir;
            if (!this.newMake_Move(this.hoPtr.roc.rcSpeed, this.hoPtr.roc.rcDir) || CRun.bMoveChanged)
              return;
          }
        }
        while (this.MG_Bounce != 0 && this.hoPtr.hoAdRunHeader.rhVBLObjet != 0)
        {
          int num7 = this.MG_Speed - this.rmDecValue;
          if (num7 > 0)
          {
            this.MG_Speed = num7;
            int speed = num7 >> 8;
            this.hoPtr.roc.rcSpeed = speed;
            int angle = this.hoPtr.roc.rcDir;
            if (this.MG_Bounce != 0)
              angle = angle + 16 /*0x10*/ & 31 /*0x1F*/;
            if (!this.newMake_Move(speed, angle) || CRun.bMoveChanged)
              break;
          }
          else
          {
            this.MG_Speed = 0;
            this.hoPtr.roc.rcSpeed = 0;
            this.MG_Bounce = 0;
            break;
          }
        }
      }

      public override void bounce()
      {
        if ((int) this.rmCollisionCount == (int) this.hoPtr.hoAdRunHeader.rh3CollisionCount)
          this.mv_Approach(((int) this.rmOpt & 1) != 0);
        if (this.hoPtr.hoAdRunHeader.rhLoopCount == this.MG_LastBounce)
          return;
        this.MG_LastBounce = this.hoPtr.hoAdRunHeader.rhLoopCount;
        ++this.MG_Bounce;
        if (this.MG_Bounce >= 12)
        {
          this.stop();
        }
        else
        {
          this.hoPtr.rom.rmBouncing = true;
          this.hoPtr.rom.rmMoveFlag = true;
        }
      }

      public override void stop()
      {
        this.hoPtr.roc.rcSpeed = 0;
        this.MG_Bounce = 0;
        this.MG_Speed = 0;
        this.hoPtr.rom.rmMoveFlag = true;
        if ((int) this.rmCollisionCount != (int) this.hoPtr.hoAdRunHeader.rh3CollisionCount)
          return;
        this.mv_Approach(((int) this.rmOpt & 1) != 0);
        this.MG_Bounce = 0;
      }

      public override void start()
      {
        this.hoPtr.rom.rmMoveFlag = true;
        this.rmStopSpeed = 0;
      }

      public override void setMaxSpeed(int speed)
      {
        if (speed < 0)
          speed = 0;
        if (speed > 250)
          speed = 250;
        this.hoPtr.roc.rcMaxSpeed = speed;
        if (this.hoPtr.roc.rcSpeed > speed)
        {
          this.hoPtr.roc.rcSpeed = speed;
          this.MG_Speed = speed << 8;
        }
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public override void setSpeed(int speed)
      {
        if (speed < 0)
          speed = 0;
        if (speed > 250)
          speed = 250;
        if (speed > this.hoPtr.roc.rcMaxSpeed)
          speed = this.hoPtr.roc.rcMaxSpeed;
        this.hoPtr.roc.rcSpeed = speed;
        this.MG_Speed = speed << 8;
        this.hoPtr.rom.rmMoveFlag = true;
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

      public void set8Dir(int dirs) => this.MG_OkDirs = dirs;
    }
}
