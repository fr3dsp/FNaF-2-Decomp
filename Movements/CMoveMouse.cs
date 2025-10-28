// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveMouse
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Movements
{

    internal class CMoveMouse : CMove
    {
      public int MM_DXMouse;
      public int MM_DYMouse;
      public int MM_FXMouse;
      public int MM_FYMouse;
      public int MM_Stopped;
      public int MM_OldSpeed;

      public override void init(CObject ho, CMoveDef mvPtr)
      {
        this.hoPtr = ho;
        CMoveDefMouse cmoveDefMouse = (CMoveDefMouse) mvPtr;
        this.hoPtr.roc.rcPlayer = (int) cmoveDefMouse.mvControl;
        this.MM_DXMouse = (int) cmoveDefMouse.mmDx + this.hoPtr.hoX;
        this.MM_DYMouse = (int) cmoveDefMouse.mmDy + this.hoPtr.hoY;
        this.MM_FXMouse = (int) cmoveDefMouse.mmFx + this.hoPtr.hoX;
        this.MM_FYMouse = (int) cmoveDefMouse.mmFy + this.hoPtr.hoY;
        this.hoPtr.roc.rcSpeed = 0;
        this.MM_OldSpeed = 0;
        this.MM_Stopped = 0;
        this.hoPtr.roc.rcMinSpeed = 0;
        this.hoPtr.roc.rcMaxSpeed = 100;
        this.rmOpt = cmoveDefMouse.mvOpt;
        this.moveAtStart(mvPtr);
        this.hoPtr.roc.rcChanged = true;
      }

      public override void move()
      {
        int num1 = this.hoPtr.hoX;
        int num2 = this.hoPtr.hoY;
        if (this.rmStopSpeed == 0 && this.hoPtr.hoAdRunHeader.rh2InputMask[this.hoPtr.roc.rcPlayer - 1] != (byte) 0)
        {
          num1 = this.hoPtr.hoAdRunHeader.rh2MouseX;
          num2 = this.hoPtr.hoAdRunHeader.rh2MouseY;
          if (num1 < this.MM_DXMouse)
            num1 = this.MM_DXMouse;
          if (num1 > this.MM_FXMouse)
            num1 = this.MM_FXMouse;
          if (num2 < this.MM_DYMouse)
            num2 = this.MM_DYMouse;
          if (num2 > this.MM_FYMouse)
            num2 = this.MM_FYMouse;
          int num3 = num1 - this.hoPtr.hoX;
          int num4 = num2 - this.hoPtr.hoY;
          int num5 = 0;
          if (num3 < 0)
          {
            num3 = -num3;
            num5 |= 1;
          }
          if (num4 < 0)
          {
            num4 = -num4;
            num5 |= 2;
          }
          int num6 = num3 + num4 << 2;
          if (num6 > 250)
            num6 = 250;
          this.hoPtr.roc.rcSpeed = num6;
          if (num6 != 0)
          {
            int num7 = num3 << 8;
            if (num4 == 0)
              num4 = 1;
            int num8 = num7 / num4;
            int index = 0;
            while (num8 < CMove.CosSurSin32[index])
              index += 2;
            int num9 = CMove.CosSurSin32[index + 1];
            if ((num5 & 2) != 0)
              num9 = -num9 + 32 /*0x20*/ & 31 /*0x1F*/;
            if ((num5 & 1) != 0)
              num9 = (-(num9 - 8 & 31 /*0x1F*/) & 31 /*0x1F*/) + 8 & 31 /*0x1F*/;
            this.hoPtr.roc.rcDir = num9;
          }
        }
        if (this.hoPtr.roc.rcSpeed != 0)
        {
          this.MM_Stopped = 0;
          this.MM_OldSpeed = this.hoPtr.roc.rcSpeed;
        }
        ++this.MM_Stopped;
        if (this.MM_Stopped > 10)
          this.MM_OldSpeed = 0;
        this.hoPtr.roc.rcSpeed = this.MM_OldSpeed;
        if (this.hoPtr.roa != null)
          this.hoPtr.roa.animate();
        if (CRun.bMoveChanged)
          return;
        this.hoPtr.hoX = num1;
        this.hoPtr.hoY = num2;
        this.hoPtr.roc.rcChanged = true;
        ++this.hoPtr.hoAdRunHeader.rh3CollisionCount;
        this.rmCollisionCount = this.hoPtr.hoAdRunHeader.rh3CollisionCount;
        this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr);
      }

      public override void stop()
      {
        if ((int) this.rmCollisionCount == (int) this.hoPtr.hoAdRunHeader.rh3CollisionCount)
          this.mv_Approach(((int) this.rmOpt & 1) != 0);
        this.hoPtr.roc.rcSpeed = 0;
      }

      public override void start()
      {
        this.rmStopSpeed = 0;
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
    }
}
