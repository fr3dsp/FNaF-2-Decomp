// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveBall
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Movements
{

    internal class CMoveBall : CMove
    {
      public int MB_StartDir;
      public int MB_Angles;
      public int MB_Securite;
      public int MB_SecuCpt;
      public int MB_Bounce;
      public int MB_Speed;
      public int MB_MaskBounce;
      public int MB_LastBounce;
      public bool MB_Blocked;
      private static short[] rebond_List = new short[512 /*0x0200*/]
      {
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 3,
        (short) 4,
        (short) 5,
        (short) 6,
        (short) 7,
        (short) 8,
        (short) 9,
        (short) 10,
        (short) 11,
        (short) 12,
        (short) 13,
        (short) 14,
        (short) 15,
        (short) 16 /*0x10*/,
        (short) 17,
        (short) 18,
        (short) 19,
        (short) 20,
        (short) 21,
        (short) 22,
        (short) 23,
        (short) 24,
        (short) 25,
        (short) 26,
        (short) 27,
        (short) 28,
        (short) 29,
        (short) 30,
        (short) 31 /*0x1F*/,
        (short) 30,
        (short) 31 /*0x1F*/,
        (short) 0,
        (short) 1,
        (short) 4,
        (short) 3,
        (short) 2,
        (short) 1,
        (short) 0,
        (short) 31 /*0x1F*/,
        (short) 30,
        (short) 29,
        (short) 28,
        (short) 27,
        (short) 26,
        (short) 25,
        (short) 24,
        (short) 23,
        (short) 22,
        (short) 21,
        (short) 20,
        (short) 24,
        (short) 25,
        (short) 26,
        (short) 27,
        (short) 27,
        (short) 28,
        (short) 28,
        (short) 28,
        (short) 28,
        (short) 29,
        (short) 29,
        (short) 24,
        (short) 23,
        (short) 22,
        (short) 21,
        (short) 20,
        (short) 19,
        (short) 18,
        (short) 17,
        (short) 16 /*0x10*/,
        (short) 15,
        (short) 14,
        (short) 13,
        (short) 12,
        (short) 16 /*0x10*/,
        (short) 17,
        (short) 18,
        (short) 19,
        (short) 19,
        (short) 20,
        (short) 20,
        (short) 20,
        (short) 20,
        (short) 21,
        (short) 21,
        (short) 22,
        (short) 23,
        (short) 24,
        (short) 25,
        (short) 28,
        (short) 27,
        (short) 26,
        (short) 25,
        (short) 0,
        (short) 31 /*0x1F*/,
        (short) 30,
        (short) 29,
        (short) 28,
        (short) 27,
        (short) 26,
        (short) 25,
        (short) 24,
        (short) 23,
        (short) 22,
        (short) 21,
        (short) 20,
        (short) 19,
        (short) 18,
        (short) 17,
        (short) 16 /*0x10*/,
        (short) 20,
        (short) 21,
        (short) 22,
        (short) 22,
        (short) 23,
        (short) 24,
        (short) 24,
        (short) 24,
        (short) 24,
        (short) 25,
        (short) 26,
        (short) 27,
        (short) 28,
        (short) 29,
        (short) 30,
        (short) 8,
        (short) 7,
        (short) 6,
        (short) 5,
        (short) 4,
        (short) 8,
        (short) 9,
        (short) 10,
        (short) 11,
        (short) 11,
        (short) 12,
        (short) 12,
        (short) 12,
        (short) 12,
        (short) 13,
        (short) 13,
        (short) 14,
        (short) 15,
        (short) 16 /*0x10*/,
        (short) 17,
        (short) 20,
        (short) 19,
        (short) 18,
        (short) 17,
        (short) 16 /*0x10*/,
        (short) 15,
        (short) 14,
        (short) 13,
        (short) 12,
        (short) 11,
        (short) 10,
        (short) 9,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 3,
        (short) 4,
        (short) 5,
        (short) 6,
        (short) 7,
        (short) 8,
        (short) 9,
        (short) 10,
        (short) 11,
        (short) 12,
        (short) 13,
        (short) 14,
        (short) 15,
        (short) 16 /*0x10*/,
        (short) 17,
        (short) 18,
        (short) 19,
        (short) 20,
        (short) 21,
        (short) 22,
        (short) 23,
        (short) 24,
        (short) 25,
        (short) 26,
        (short) 27,
        (short) 28,
        (short) 29,
        (short) 30,
        (short) 31 /*0x1F*/,
        (short) 16 /*0x10*/,
        (short) 15,
        (short) 14,
        (short) 13,
        (short) 12,
        (short) 11,
        (short) 10,
        (short) 9,
        (short) 8,
        (short) 12,
        (short) 13,
        (short) 14,
        (short) 15,
        (short) 15,
        (short) 16 /*0x10*/,
        (short) 16 /*0x10*/,
        (short) 16 /*0x10*/,
        (short) 16 /*0x10*/,
        (short) 17,
        (short) 17,
        (short) 18,
        (short) 19,
        (short) 20,
        (short) 21,
        (short) 24,
        (short) 23,
        (short) 22,
        (short) 21,
        (short) 20,
        (short) 19,
        (short) 18,
        (short) 17,
        (short) 16 /*0x10*/,
        (short) 17,
        (short) 18,
        (short) 19,
        (short) 20,
        (short) 21,
        (short) 22,
        (short) 23,
        (short) 24,
        (short) 23,
        (short) 22,
        (short) 21,
        (short) 20,
        (short) 19,
        (short) 18,
        (short) 17,
        (short) 16 /*0x10*/,
        (short) 17,
        (short) 18,
        (short) 19,
        (short) 20,
        (short) 21,
        (short) 22,
        (short) 23,
        (short) 24,
        (short) 23,
        (short) 22,
        (short) 21,
        (short) 20,
        (short) 19,
        (short) 18,
        (short) 17,
        (short) 3,
        (short) 3,
        (short) 4,
        (short) 4,
        (short) 4,
        (short) 4,
        (short) 5,
        (short) 5,
        (short) 6,
        (short) 7,
        (short) 8,
        (short) 9,
        (short) 12,
        (short) 11,
        (short) 10,
        (short) 9,
        (short) 8,
        (short) 7,
        (short) 6,
        (short) 5,
        (short) 4,
        (short) 3,
        (short) 2,
        (short) 1,
        (short) 0,
        (short) 31 /*0x1F*/,
        (short) 30,
        (short) 29,
        (short) 28,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 0,
        (short) 0,
        (short) 1,
        (short) 1,
        (short) 2,
        (short) 3,
        (short) 4,
        (short) 5,
        (short) 8,
        (short) 7,
        (short) 6,
        (short) 5,
        (short) 4,
        (short) 3,
        (short) 2,
        (short) 1,
        (short) 0,
        (short) 31 /*0x1F*/,
        (short) 30,
        (short) 29,
        (short) 28,
        (short) 27,
        (short) 26,
        (short) 25,
        (short) 24,
        (short) 28,
        (short) 29,
        (short) 30,
        (short) 31 /*0x1F*/,
        (short) 31 /*0x1F*/,
        (short) 0,
        (short) 0,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 3,
        (short) 4,
        (short) 5,
        (short) 6,
        (short) 7,
        (short) 8,
        (short) 9,
        (short) 10,
        (short) 11,
        (short) 12,
        (short) 13,
        (short) 14,
        (short) 15,
        (short) 16 /*0x10*/,
        (short) 17,
        (short) 18,
        (short) 19,
        (short) 20,
        (short) 21,
        (short) 22,
        (short) 23,
        (short) 24,
        (short) 25,
        (short) 26,
        (short) 27,
        (short) 28,
        (short) 29,
        (short) 30,
        (short) 31 /*0x1F*/,
        (short) 0,
        (short) 31 /*0x1F*/,
        (short) 30,
        (short) 29,
        (short) 28,
        (short) 27,
        (short) 26,
        (short) 25,
        (short) 24,
        (short) 25,
        (short) 26,
        (short) 27,
        (short) 28,
        (short) 29,
        (short) 30,
        (short) 31 /*0x1F*/,
        (short) 0,
        (short) 31 /*0x1F*/,
        (short) 30,
        (short) 29,
        (short) 28,
        (short) 27,
        (short) 25,
        (short) 25,
        (short) 24,
        (short) 25,
        (short) 26,
        (short) 27,
        (short) 28,
        (short) 29,
        (short) 30,
        (short) 31 /*0x1F*/,
        (short) 0,
        (short) 4,
        (short) 5,
        (short) 6,
        (short) 7,
        (short) 7,
        (short) 8,
        (short) 8,
        (short) 8,
        (short) 8,
        (short) 9,
        (short) 9,
        (short) 10,
        (short) 11,
        (short) 12,
        (short) 13,
        (short) 16 /*0x10*/,
        (short) 15,
        (short) 14,
        (short) 13,
        (short) 12,
        (short) 11,
        (short) 10,
        (short) 9,
        (short) 8,
        (short) 7,
        (short) 6,
        (short) 5,
        (short) 4,
        (short) 3,
        (short) 2,
        (short) 1,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 3,
        (short) 4,
        (short) 5,
        (short) 6,
        (short) 7,
        (short) 8,
        (short) 7,
        (short) 6,
        (short) 5,
        (short) 4,
        (short) 3,
        (short) 2,
        (short) 1,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 3,
        (short) 4,
        (short) 5,
        (short) 6,
        (short) 7,
        (short) 8,
        (short) 7,
        (short) 6,
        (short) 5,
        (short) 4,
        (short) 3,
        (short) 2,
        (short) 1,
        (short) 16 /*0x10*/,
        (short) 15,
        (short) 14,
        (short) 13,
        (short) 12,
        (short) 11,
        (short) 10,
        (short) 9,
        (short) 8,
        (short) 9,
        (short) 10,
        (short) 11,
        (short) 12,
        (short) 13,
        (short) 14,
        (short) 15,
        (short) 16 /*0x10*/,
        (short) 15,
        (short) 14,
        (short) 13,
        (short) 12,
        (short) 11,
        (short) 10,
        (short) 9,
        (short) 8,
        (short) 9,
        (short) 10,
        (short) 11,
        (short) 12,
        (short) 13,
        (short) 14,
        (short) 15,
        (short) 0,
        (short) 1,
        (short) 2,
        (short) 3,
        (short) 4,
        (short) 5,
        (short) 6,
        (short) 7,
        (short) 8,
        (short) 9,
        (short) 10,
        (short) 11,
        (short) 12,
        (short) 13,
        (short) 14,
        (short) 15,
        (short) 16 /*0x10*/,
        (short) 17,
        (short) 18,
        (short) 19,
        (short) 20,
        (short) 21,
        (short) 22,
        (short) 23,
        (short) 24,
        (short) 25,
        (short) 26,
        (short) 27,
        (short) 28,
        (short) 29,
        (short) 30,
        (short) 31 /*0x1F*/
      };
      private static uint[] MaskBounce = new uint[3]
      {
        4294967292U,
        4294967294U,
        uint.MaxValue
      };
      private static int[] PlusAngles = new int[6]
      {
        -4,
        4,
        -2,
        2,
        -1,
        1
      };
      private static int[] PlusAnglesTry = new int[6]
      {
        -4,
        4,
        -4,
        4,
        -4,
        4
      };

      public override void init(CObject ho, CMoveDef mvPtr)
      {
        this.hoPtr = ho;
        CMoveDefBall cmoveDefBall = (CMoveDefBall) mvPtr;
        this.hoPtr.hoCalculX = 0;
        this.hoPtr.hoCalculY = 0;
        this.hoPtr.roc.rcSpeed = (int) cmoveDefBall.mbSpeed;
        this.hoPtr.roc.rcMaxSpeed = (int) cmoveDefBall.mbSpeed;
        this.hoPtr.roc.rcMinSpeed = (int) cmoveDefBall.mbSpeed;
        this.MB_Speed = (int) cmoveDefBall.mbSpeed << 8;
        int acceleration = (int) cmoveDefBall.mbDecelerate;
        if (acceleration != 0)
        {
          acceleration = this.getAccelerator(acceleration);
          this.hoPtr.roc.rcMinSpeed = 0;
        }
        this.rmDecValue = acceleration;
        this.MB_Bounce = (int) cmoveDefBall.mbBounce;
        this.MB_Angles = (int) cmoveDefBall.mbAngles;
        this.MB_MaskBounce = (int) CMoveBall.MaskBounce[this.MB_Angles];
        this.MB_Blocked = false;
        this.MB_LastBounce = -1;
        this.MB_Securite = (100 - (int) cmoveDefBall.mbSecurity) / 8;
        this.MB_SecuCpt = this.MB_Securite;
        this.moveAtStart(mvPtr);
        this.hoPtr.roc.rcChanged = true;
      }

      public override void move()
      {
        this.hoPtr.rom.rmBouncing = false;
        this.hoPtr.hoAdRunHeader.rhVBLObjet = 1;
        this.hoPtr.roc.rcAnim = 1;
        if (this.hoPtr.roa != null)
          this.hoPtr.roa.animate();
        if (CRun.bMoveChanged)
          return;
        if (this.rmDecValue != 0)
        {
          int mbSpeed = this.MB_Speed;
          if (mbSpeed > 0)
          {
            int num1 = this.rmDecValue;
            if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
              num1 = (int) ((double) num1 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
            int num2 = mbSpeed - num1;
            if (num2 < 0)
              num2 = 0;
            this.MB_Speed = num2;
            this.hoPtr.roc.rcSpeed = num2 >> 8;
          }
        }
        this.newMake_Move(this.hoPtr.roc.rcSpeed, this.hoPtr.roc.rcDir);
      }

      public override void stop()
      {
        if (this.rmStopSpeed != 0)
          return;
        this.rmStopSpeed = this.hoPtr.roc.rcSpeed | 32768 /*0x8000*/;
        this.hoPtr.roc.rcSpeed = 0;
        this.MB_Speed = 0;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public override void start()
      {
        int rmStopSpeed = this.rmStopSpeed;
        if (rmStopSpeed == 0)
          return;
        int num = rmStopSpeed & (int) short.MaxValue;
        this.hoPtr.roc.rcSpeed = num;
        this.MB_Speed = num << 8;
        this.rmStopSpeed = 0;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public override void bounce()
      {
        if (this.rmStopSpeed != 0 || this.hoPtr.hoAdRunHeader.rhLoopCount == this.MB_LastBounce)
          return;
        this.MB_LastBounce = this.hoPtr.hoAdRunHeader.rhLoopCount;
        if ((int) this.rmCollisionCount == (int) this.hoPtr.hoAdRunHeader.rh3CollisionCount)
          this.mb_Approach(this.MB_Blocked);
        int hoX = this.hoPtr.hoX;
        int hoY = this.hoPtr.hoY;
        int num1 = 0;
        int x1 = hoX - 8;
        int y1 = hoY - 8;
        if (!this.tst_Position(x1, y1, this.MB_Blocked))
          num1 |= 1;
        int x2 = x1 + 16 /*0x10*/;
        if (!this.tst_Position(x2, y1, this.MB_Blocked))
          num1 |= 2;
        int y2 = y1 + 16 /*0x10*/;
        if (!this.tst_Position(x2, y2, this.MB_Blocked))
          num1 |= 4;
        if (!this.tst_Position(x2 - 16 /*0x10*/, y2, this.MB_Blocked))
          num1 |= 8;
        int dir1 = (int) CMoveBall.rebond_List[num1 * 32 /*0x20*/ + this.hoPtr.roc.rcDir] & this.MB_MaskBounce;
        if (!this.mvb_Test(dir1))
        {
          int num2 = CMoveBall.PlusAnglesTry[this.MB_Angles * 2 + 1];
          int num3 = num2;
          bool flag = false;
          do
          {
            dir1 = dir1 - num2 & 31 /*0x1F*/;
            if (this.mvb_Test(dir1))
            {
              flag = true;
              break;
            }
            dir1 = dir1 + 2 * num2 & 31 /*0x1F*/;
            if (this.mvb_Test(dir1))
            {
              flag = true;
              break;
            }
            dir1 = dir1 - num2 & 31 /*0x1F*/;
            num2 += num3;
          }
          while (num2 <= 16 /*0x10*/);
          if (!flag)
          {
            this.MB_Blocked = true;
            this.hoPtr.roc.rcDir = (int) this.hoPtr.hoAdRunHeader.random((short) 32 /*0x20*/) & this.MB_MaskBounce;
            this.hoPtr.rom.rmBouncing = true;
            this.hoPtr.rom.rmMoveFlag = true;
            return;
          }
        }
        this.MB_Blocked = false;
        this.hoPtr.roc.rcDir = dir1;
        int num4 = (int) this.hoPtr.hoAdRunHeader.random((short) 100);
        if (num4 < this.MB_Bounce)
        {
          int num5 = num4 >> 2;
          if (num5 < 25)
          {
            int dir2 = num5 - 12 & 31 /*0x1F*/ & this.MB_MaskBounce;
            if (this.mvb_Test(dir2))
            {
              this.hoPtr.roc.rcDir = dir2;
              this.hoPtr.rom.rmBouncing = true;
              this.hoPtr.rom.rmMoveFlag = true;
              return;
            }
          }
        }
        int num6 = this.hoPtr.roc.rcDir & 7;
        if (this.MB_SecuCpt != 12)
        {
          if (num6 == 0)
          {
            --this.MB_SecuCpt;
            if (this.MB_SecuCpt < 0)
            {
              int dir3 = this.hoPtr.roc.rcDir + CMoveBall.PlusAngles[(int) this.hoPtr.hoAdRunHeader.random((short) 2) + this.MB_Angles * 2] & 31 /*0x1F*/;
              if (this.mvb_Test(dir3))
              {
                this.hoPtr.roc.rcDir = dir3;
                this.MB_SecuCpt = this.MB_Securite;
              }
            }
          }
          else
            this.MB_SecuCpt = this.MB_Securite;
        }
        this.hoPtr.rom.rmBouncing = true;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      private bool mvb_Test(int dir)
      {
        int num1 = this.hoPtr.hoX << 16 /*0x10*/ | this.hoPtr.hoCalculX & (int) ushort.MaxValue;
        int num2 = this.hoPtr.hoY << 16 /*0x10*/ | this.hoPtr.hoCalculY & (int) ushort.MaxValue;
        return this.tst_Position((CMove.Cosinus32[dir] << 11) + num1 >> 16 /*0x10*/ & (int) ushort.MaxValue, (CMove.Sinus32[dir] << 11) + num2 >> 16 /*0x10*/ & (int) ushort.MaxValue, false);
      }

      public override void setSpeed(int speed)
      {
        if (speed < 0)
          speed = 0;
        if (speed > 250)
          speed = 250;
        this.hoPtr.roc.rcSpeed = speed;
        this.MB_Speed = speed << 8;
        this.rmStopSpeed = 0;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public override void setMaxSpeed(int speed) => this.setSpeed(speed);

      public override void reverse()
      {
        if (this.rmStopSpeed != 0)
          return;
        this.hoPtr.rom.rmMoveFlag = true;
        this.hoPtr.roc.rcDir += 16 /*0x10*/;
        this.hoPtr.roc.rcDir &= 31 /*0x1F*/;
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
