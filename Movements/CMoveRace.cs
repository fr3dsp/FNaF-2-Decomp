// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveRace
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Movements
{

    internal class CMoveRace : CMove
    {
      public int MR_Bounce;
      public int MR_BounceMu;
      public int MR_Speed;
      public int MR_RotSpeed;
      public int MR_RotCpt;
      public int MR_RotPos;
      public int MR_RotMask;
      public int MR_OkReverse;
      public int MR_OldJoy;
      public int MR_LastBounce;
      public static uint[] RaceMask = new uint[4]
      {
        4294967288U,
        4294967292U,
        4294967294U,
        uint.MaxValue
      };

      public override void init(CObject ho, CMoveDef mvPtr)
      {
        this.hoPtr = ho;
        CMoveDefRace cmoveDefRace = (CMoveDefRace) mvPtr;
        this.MR_Speed = 0;
        this.hoPtr.roc.rcSpeed = 0;
        this.MR_Bounce = 0;
        this.MR_LastBounce = -1;
        this.hoPtr.roc.rcPlayer = (int) cmoveDefRace.mvControl;
        this.rmAcc = (int) cmoveDefRace.mrAcc;
        this.rmAccValue = this.getAccelerator((int) cmoveDefRace.mrAcc);
        this.rmDec = (int) cmoveDefRace.mrDec;
        this.rmDecValue = this.getAccelerator((int) cmoveDefRace.mrDec);
        this.hoPtr.roc.rcMaxSpeed = (int) cmoveDefRace.mrSpeed;
        this.hoPtr.roc.rcMinSpeed = 0;
        this.MR_BounceMu = (int) cmoveDefRace.mrBounceMult;
        this.MR_OkReverse = (int) cmoveDefRace.mrOkReverse;
        this.hoPtr.rom.rmReverse = 0;
        this.MR_OldJoy = 0;
        this.rmOpt = cmoveDefRace.mvOpt;
        this.MR_RotMask = (int) CMoveRace.RaceMask[(int) cmoveDefRace.mrAngles];
        this.MR_RotSpeed = (int) cmoveDefRace.mrRot;
        this.MR_RotCpt = 0;
        this.MR_RotPos = this.hoPtr.roc.rcDir;
        this.hoPtr.hoCalculX = 0;
        this.hoPtr.hoCalculY = 0;
        this.moveAtStart(mvPtr);
        this.hoPtr.roc.rcChanged = true;
      }

      public override void move()
      {
        this.hoPtr.hoAdRunHeader.rhVBLObjet = 1;
        if (this.MR_Bounce == 0)
        {
          this.hoPtr.rom.rmBouncing = false;
          int num1 = (int) this.hoPtr.hoAdRunHeader.rhPlayer[this.hoPtr.roc.rcPlayer - 1] & 15;
          int num2 = 0;
          if ((num1 & 8) != 0)
            num2 = -1;
          if ((num1 & 4) != 0)
            num2 = 1;
          if (num2 != 0)
          {
            int num3 = this.MR_RotSpeed;
            if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
              num3 = (int) ((double) num3 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
            this.MR_RotCpt += num3;
            while (this.MR_RotCpt > 100)
            {
              this.MR_RotCpt -= 100;
              this.MR_RotPos += num2;
              this.MR_RotPos &= 31 /*0x1F*/;
              this.hoPtr.roc.rcDir = this.MR_RotPos & this.MR_RotMask;
            }
            this.hoPtr.roc.rcChanged = true;
          }
          int num4 = 0;
          if (this.hoPtr.rom.rmReverse != 0)
          {
            if ((num1 & 1) != 0)
              num4 = 1;
            if ((num1 & 2) != 0)
              num4 = 2;
          }
          else
          {
            if ((num1 & 1) != 0)
              num4 = 2;
            if ((num1 & 2) != 0)
              num4 = 1;
          }
          int mrSpeed = this.MR_Speed;
          if ((num4 & 1) != 0)
          {
            if (this.MR_Speed == 0)
            {
              if (this.MR_OkReverse != 0 && (this.MR_OldJoy & 3) == 0)
              {
                this.hoPtr.rom.rmReverse ^= 1;
                int num5 = this.rmAccValue;
                if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
                  num5 = (int) ((double) num5 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
                int num6 = mrSpeed + num5;
                if (num6 >> 8 > this.hoPtr.roc.rcMaxSpeed)
                {
                  num6 = this.hoPtr.roc.rcMaxSpeed << 8;
                  this.MR_Speed = num6;
                }
                this.MR_Speed = num6;
              }
            }
            else
            {
              int num7 = this.rmDecValue;
              if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
                num7 = (int) ((double) num7 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
              int num8 = mrSpeed - num7;
              if (num8 < 0)
                num8 = 0;
              this.MR_Speed = num8;
            }
          }
          else if ((num4 & 2) != 0)
          {
            int num9 = this.rmAccValue;
            if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
              num9 = (int) ((double) num9 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
            int num10 = mrSpeed + num9;
            if (num10 >> 8 > this.hoPtr.roc.rcMaxSpeed)
            {
              num10 = this.hoPtr.roc.rcMaxSpeed << 8;
              this.MR_Speed = num10;
            }
            this.MR_Speed = num10;
          }
          this.MR_OldJoy = num1;
          this.hoPtr.roc.rcSpeed = this.MR_Speed >> 8;
          this.hoPtr.roc.rcAnim = 1;
          if (this.hoPtr.roa != null)
          {
            this.hoPtr.roa.animate();
            if (CRun.bMoveChanged)
              return;
          }
          int angle = this.hoPtr.roc.rcDir;
          if (this.hoPtr.rom.rmReverse != 0)
            angle = angle + 16 /*0x10*/ & 31 /*0x1F*/;
          if (!this.newMake_Move(this.hoPtr.roc.rcSpeed, angle) || CRun.bMoveChanged)
            return;
        }
        while (!CRun.bMoveChanged && this.MR_Bounce != 0 && this.hoPtr.hoAdRunHeader.rhVBLObjet != 0)
        {
          int num = this.MR_Speed - this.rmDecValue;
          if (num <= 0)
          {
            this.MR_Speed = 0;
            this.MR_Bounce = 0;
            break;
          }
          this.MR_Speed = num;
          int speed = num >> 8;
          int angle = this.hoPtr.roc.rcDir;
          if (this.MR_Bounce != 0)
            angle = angle + 16 /*0x10*/ & 31 /*0x1F*/;
          if (!this.newMake_Move(speed, angle))
            break;
        }
      }

      public override void stop()
      {
        this.MR_Bounce = 0;
        this.MR_Speed = 0;
        this.hoPtr.rom.rmReverse = 0;
        if ((int) this.rmCollisionCount != (int) this.hoPtr.hoAdRunHeader.rh3CollisionCount)
          return;
        this.mv_Approach(((int) this.rmOpt & 1) != 0);
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public override void start()
      {
        this.rmStopSpeed = 0;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public override void bounce()
      {
        if ((int) this.rmCollisionCount == (int) this.hoPtr.hoAdRunHeader.rh3CollisionCount)
          this.mv_Approach(((int) this.rmOpt & 1) != 0);
        if (this.hoPtr.hoAdRunHeader.rhLoopCount == this.MR_LastBounce)
          return;
        this.MR_Bounce = this.hoPtr.rom.rmReverse;
        this.hoPtr.rom.rmReverse = 0;
        ++this.MR_Bounce;
        if (this.MR_Bounce >= 16 /*0x10*/)
        {
          this.stop();
        }
        else
        {
          this.hoPtr.rom.rmMoveFlag = true;
          this.hoPtr.rom.rmBouncing = true;
        }
      }

      public override void setSpeed(int speed)
      {
        if (speed < 0)
          speed = 0;
        if (speed > 250)
          speed = 250;
        if (speed > this.hoPtr.roc.rcMaxSpeed)
          speed = this.hoPtr.roc.rcMaxSpeed;
        speed <<= 8;
        this.MR_Speed = speed;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public override void setMaxSpeed(int speed)
      {
        if (speed < 0)
          speed = 0;
        if (speed > 250)
          speed = 250;
        this.hoPtr.roc.rcMaxSpeed = speed;
        speed <<= 8;
        if (this.MR_Speed > speed)
          this.MR_Speed = speed;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public void MRSetRotSpeed(int speed) => this.MR_RotSpeed = speed;

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

      public override void setDir(int dir)
      {
        this.MR_RotPos = dir;
        this.hoPtr.roc.rcDir = dir & this.MR_RotMask;
      }
    }
}
