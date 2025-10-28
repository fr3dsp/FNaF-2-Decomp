// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMovePath
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;
using System;

namespace RuntimeXNA.Movements
{

    internal class CMovePath : CMove
    {
      public int MT_Speed;
      public int MT_Sinus;
      public int MT_Cosinus;
      public int MT_Longueur;
      public int MT_XOrigin;
      public int MT_YOrigin;
      public int MT_XDest;
      public int MT_YDest;
      public int MT_MoveNumber;
      public bool MT_Direction;
      public CMoveDefPath MT_Movement;
      public int MT_Calculs;
      public int MT_XStart;
      public int MT_YStart;
      public int MT_Pause;
      public string MT_GotoNode;
      private bool MT_FlagBranch;

      public override void init(CObject ho, CMoveDef mvPtr)
      {
        this.hoPtr = ho;
        CMoveDefPath cmoveDefPath = (CMoveDefPath) mvPtr;
        this.MT_XStart = this.hoPtr.hoX;
        this.MT_YStart = this.hoPtr.hoY;
        this.MT_Direction = false;
        this.MT_Pause = 0;
        this.hoPtr.hoMark1 = 0;
        this.MT_Movement = cmoveDefPath;
        this.hoPtr.roc.rcMinSpeed = (int) cmoveDefPath.mtMinSpeed;
        this.hoPtr.roc.rcMaxSpeed = (int) cmoveDefPath.mtMaxSpeed;
        this.MT_Calculs = 0;
        this.MT_GotoNode = (string) null;
        this.mtGoAvant(0);
        this.moveAtStart(mvPtr);
        this.hoPtr.roc.rcSpeed = this.MT_Speed;
        this.hoPtr.roc.rcChanged = true;
        if (this.MT_Movement.steps.Length != 0)
          return;
        this.stop();
      }

      public override void move()
      {
        this.hoPtr.hoMark1 = 0;
        this.hoPtr.roc.rcAnim = 1;
        if (this.hoPtr.roa != null)
          this.hoPtr.roa.animate();
        if (CRun.bMoveChanged)
          return;
        if (this.MT_Speed == 0)
        {
          int mtPause = this.MT_Pause;
          if (mtPause == 0)
          {
            this.hoPtr.roc.rcSpeed = 0;
            this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr);
            return;
          }
          int num = mtPause - this.hoPtr.hoAdRunHeader.rhTimerDelta;
          if (num > 0)
          {
            this.MT_Pause = num;
            this.hoPtr.roc.rcSpeed = 0;
            this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr);
            return;
          }
          this.MT_Pause = 0;
          this.MT_Speed = this.rmStopSpeed & (int) short.MaxValue;
          this.rmStopSpeed = 0;
          this.hoPtr.roc.rcSpeed = this.MT_Speed;
        }
        int step = (this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) == 0 ? 256 /*0x0100*/ : (int) (256.0 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
        this.hoPtr.hoAdRunHeader.rhMT_VBLCount = (short) step;
        bool flag;
        do
        {
          flag = false;
          this.hoPtr.hoAdRunHeader.rhMT_VBLStep = (short) step;
          step = step * this.MT_Speed << 5;
          if (step <= 524288 /*0x080000*/)
          {
            this.hoPtr.hoAdRunHeader.rhMT_MoveStep = step;
          }
          else
          {
            step = 16384 /*0x4000*/ / this.MT_Speed;
            this.hoPtr.hoAdRunHeader.rhMT_VBLStep = (short) step;
            this.hoPtr.hoAdRunHeader.rhMT_MoveStep = 524288 /*0x080000*/;
          }
          this.MT_FlagBranch = false;
          if (this.mtMove(this.hoPtr.hoAdRunHeader.rhMT_MoveStep) && !this.MT_FlagBranch)
            flag = true;
          else if ((int) this.hoPtr.hoAdRunHeader.rhMT_VBLCount == (int) this.hoPtr.hoAdRunHeader.rhMT_VBLStep)
            flag = true;
          else if ((int) this.hoPtr.hoAdRunHeader.rhMT_VBLCount > (int) this.hoPtr.hoAdRunHeader.rhMT_VBLStep)
          {
            this.hoPtr.hoAdRunHeader.rhMT_VBLCount -= this.hoPtr.hoAdRunHeader.rhMT_VBLStep;
            step = (int) this.hoPtr.hoAdRunHeader.rhMT_VBLCount;
          }
          else
          {
            step = (int) this.hoPtr.hoAdRunHeader.rhMT_VBLCount * this.MT_Speed << 5;
            this.mtMove(step);
            flag = true;
          }
        }
        while (!flag);
      }

      private bool mtMove(int step)
      {
        step += this.MT_Calculs;
        int num1 = step >> 16 /*0x10*/ & (int) ushort.MaxValue;
        if (num1 < this.MT_Longueur)
        {
          this.MT_Calculs = step;
          int num2 = num1 * this.MT_Cosinus / 16384 /*0x4000*/ + this.MT_XOrigin;
          int num3 = num1 * this.MT_Sinus / 16384 /*0x4000*/ + this.MT_YOrigin;
          this.hoPtr.hoX = num2;
          this.hoPtr.hoY = num3;
          this.hoPtr.roc.rcChanged = true;
          this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr);
          return this.hoPtr.rom.rmMoveFlag;
        }
        step = num1 - this.MT_Longueur << 16 /*0x10*/ | step & (int) ushort.MaxValue;
        if (this.MT_Speed != 0)
          step /= this.MT_Speed;
        step >>= 5;
        this.hoPtr.hoAdRunHeader.rhMT_VBLCount += (short) (step & (int) ushort.MaxValue);
        this.hoPtr.hoX = this.MT_XDest;
        this.hoPtr.hoY = this.MT_YDest;
        this.hoPtr.roc.rcChanged = true;
        this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr);
        if (this.hoPtr.rom.rmMoveFlag)
          return true;
        this.hoPtr.hoMark1 = this.hoPtr.hoAdRunHeader.rhLoopCount;
        this.hoPtr.hoMT_NodeName = (string) null;
        int mtMoveNumber = this.MT_MoveNumber;
        this.MT_Calculs = 0;
        if (!this.MT_Direction)
        {
          int number1 = mtMoveNumber + 1;
          if (number1 < (int) this.MT_Movement.mtNumber)
          {
            this.hoPtr.hoMT_NodeName = this.MT_Movement.steps[number1].mdName;
            if (this.MT_GotoNode != null && this.MT_Movement.steps[number1].mdName != null && string.Compare(this.MT_GotoNode, this.MT_Movement.steps[number1].mdName, StringComparison.OrdinalIgnoreCase) == 0)
            {
              this.MT_MoveNumber = number1;
              this.mtMessages();
              return this.mtTheEnd();
            }
            this.mtGoAvant(number1);
            this.mtMessages();
            return this.hoPtr.rom.rmMoveFlag;
          }
          this.hoPtr.hoMark2 = this.hoPtr.hoAdRunHeader.rhLoopCount;
          this.MT_MoveNumber = number1;
          if (this.MT_Direction)
          {
            this.mtMessages();
            return this.hoPtr.rom.rmMoveFlag;
          }
          if (this.MT_Movement.mtReverse != (byte) 0)
          {
            this.MT_Direction = true;
            int number2 = number1 - 1;
            this.hoPtr.hoMT_NodeName = this.MT_Movement.steps[number2].mdName;
            this.mtGoArriere(number2);
            this.mtMessages();
            return this.hoPtr.rom.rmMoveFlag;
          }
          this.mtReposAtEnd();
          if (this.MT_Movement.mtLoop == (byte) 0)
          {
            this.mtTheEnd();
            this.mtMessages();
            return this.hoPtr.rom.rmMoveFlag;
          }
          this.mtGoAvant(0);
          this.mtMessages();
          return this.hoPtr.rom.rmMoveFlag;
        }
        if (this.MT_GotoNode != null && this.MT_Movement.steps[mtMoveNumber].mdName != null && string.Compare(this.MT_GotoNode, this.MT_Movement.steps[mtMoveNumber].mdName, StringComparison.OrdinalIgnoreCase) == 0)
        {
          this.mtMessages();
          return this.mtTheEnd();
        }
        this.hoPtr.hoMT_NodeName = this.MT_Movement.steps[mtMoveNumber].mdName;
        this.MT_Pause = (int) this.MT_Movement.steps[mtMoveNumber].mdPause;
        int number3 = mtMoveNumber - 1;
        if (number3 >= 0)
        {
          this.mtGoArriere(number3);
          this.mtMessages();
          return this.hoPtr.rom.rmMoveFlag;
        }
        this.mtReposAtEnd();
        if (!this.MT_Direction)
        {
          this.mtMessages();
          return this.hoPtr.rom.rmMoveFlag;
        }
        if (this.MT_Movement.mtLoop == (byte) 0)
        {
          this.mtTheEnd();
          this.mtMessages();
          return this.hoPtr.rom.rmMoveFlag;
        }
        int number4 = 0;
        this.MT_Direction = false;
        this.mtGoAvant(number4);
        this.mtMessages();
        return this.hoPtr.rom.rmMoveFlag;
      }

      private void mtGoAvant(int number)
      {
        if (number >= this.MT_Movement.steps.Length)
        {
          this.stop();
        }
        else
        {
          this.MT_Direction = false;
          this.MT_MoveNumber = number;
          this.MT_Pause = (int) this.MT_Movement.steps[number].mdPause;
          this.MT_Cosinus = (int) this.MT_Movement.steps[number].mdCosinus;
          this.MT_Sinus = (int) this.MT_Movement.steps[number].mdSinus;
          this.MT_XOrigin = this.hoPtr.hoX;
          this.MT_YOrigin = this.hoPtr.hoY;
          this.MT_XDest = this.hoPtr.hoX + (int) this.MT_Movement.steps[number].mdDx;
          this.MT_YDest = this.hoPtr.hoY + (int) this.MT_Movement.steps[number].mdDy;
          this.hoPtr.roc.rcDir = (int) this.MT_Movement.steps[number].mdDir;
          this.mtBranche();
        }
      }

      private void mtGoArriere(int number)
      {
        if (number >= this.MT_Movement.steps.Length)
        {
          this.stop();
        }
        else
        {
          this.MT_Direction = true;
          this.MT_MoveNumber = number;
          this.MT_Cosinus = (int) -this.MT_Movement.steps[number].mdCosinus;
          this.MT_Sinus = (int) -this.MT_Movement.steps[number].mdSinus;
          this.MT_XOrigin = this.hoPtr.hoX;
          this.MT_YOrigin = this.hoPtr.hoY;
          this.MT_XDest = this.hoPtr.hoX - (int) this.MT_Movement.steps[number].mdDx;
          this.MT_YDest = this.hoPtr.hoY - (int) this.MT_Movement.steps[number].mdDy;
          this.hoPtr.roc.rcDir = (int) this.MT_Movement.steps[number].mdDir + 16 /*0x10*/ & 31 /*0x1F*/;
          this.mtBranche();
        }
      }

      private void mtBranche()
      {
        this.MT_Longueur = (int) this.MT_Movement.steps[this.MT_MoveNumber].mdLength;
        int num = (int) this.MT_Movement.steps[this.MT_MoveNumber].mdSpeed;
        int mtPause = this.MT_Pause;
        if (mtPause != 0)
        {
          this.MT_Pause = mtPause * 20;
          num |= 32768 /*0x8000*/;
          this.rmStopSpeed = num;
        }
        if (this.rmStopSpeed != 0)
          num = 0;
        if (num != this.MT_Speed || num != 0)
        {
          this.MT_Speed = num;
          this.hoPtr.rom.rmMoveFlag = true;
          this.MT_FlagBranch = true;
        }
        this.hoPtr.roc.rcSpeed = this.MT_Speed;
      }

      private void mtMessages()
      {
        if (this.hoPtr.hoMark1 == this.hoPtr.hoAdRunHeader.rhLoopCount)
        {
          this.hoPtr.hoAdRunHeader.rhEvtProg.rhCurParam0 = 0;
          this.hoPtr.hoAdRunHeader.rhEvtProg.handle_Event(this.hoPtr, -1310720 | (int) this.hoPtr.hoType & (int) ushort.MaxValue);
          this.hoPtr.hoAdRunHeader.rhEvtProg.handle_Event(this.hoPtr, -2293760 | (int) this.hoPtr.hoType & (int) ushort.MaxValue);
        }
        if (this.hoPtr.hoMark2 != this.hoPtr.hoAdRunHeader.rhLoopCount)
          return;
        this.hoPtr.hoAdRunHeader.rhEvtProg.rhCurParam0 = 0;
        this.hoPtr.hoAdRunHeader.rhEvtProg.handle_Event(this.hoPtr, -1376256 | (int) this.hoPtr.hoType & (int) ushort.MaxValue);
      }

      private bool mtTheEnd()
      {
        this.MT_Speed = 0;
        this.rmStopSpeed = 0;
        this.hoPtr.rom.rmMoveFlag = true;
        this.MT_FlagBranch = false;
        return true;
      }

      private void mtReposAtEnd()
      {
        if (this.MT_Movement.mtRepos == (byte) 0)
          return;
        this.hoPtr.hoX = this.MT_XStart;
        this.hoPtr.hoY = this.MT_YStart;
        this.hoPtr.roc.rcChanged = true;
      }

      public void mtBranchNode(string pName)
      {
        for (int number1 = 0; number1 < (int) this.MT_Movement.mtNumber; ++number1)
        {
          if (this.MT_Movement.steps[number1].mdName != null && string.Compare(pName, this.MT_Movement.steps[number1].mdName, StringComparison.OrdinalIgnoreCase) == 0)
          {
            if (!this.MT_Direction)
            {
              this.mtGoAvant(number1);
              this.hoPtr.hoMark1 = this.hoPtr.hoAdRunHeader.rhLoopCount;
              this.hoPtr.hoMT_NodeName = this.MT_Movement.steps[number1].mdName;
              this.hoPtr.hoMark2 = 0;
              this.mtMessages();
            }
            else if (number1 > 0)
            {
              int number2 = number1 - 1;
              this.mtGoArriere(number2);
              this.hoPtr.hoMark1 = this.hoPtr.hoAdRunHeader.rhLoopCount;
              this.hoPtr.hoMT_NodeName = this.MT_Movement.steps[number2].mdName;
              this.hoPtr.hoMark2 = 0;
              this.mtMessages();
            }
            this.hoPtr.rom.rmMoveFlag = true;
            break;
          }
        }
      }

      private void freeMTNode() => this.MT_GotoNode = (string) null;

      public void mtGotoNode(string pName)
      {
        for (int index = 0; index < (int) this.MT_Movement.mtNumber; ++index)
        {
          if (this.MT_Movement.steps[index].mdName != null && string.Compare(pName, this.MT_Movement.steps[index].mdName, StringComparison.OrdinalIgnoreCase) == 0)
          {
            if (index == this.MT_MoveNumber && this.MT_Calculs == 0)
              break;
            this.freeMTNode();
            this.MT_GotoNode = pName;
            if (!this.MT_Direction)
            {
              if (index > this.MT_MoveNumber)
              {
                if (this.MT_Speed != 0)
                  break;
                if ((this.rmStopSpeed & 32768 /*0x8000*/) != 0)
                {
                  this.start();
                  break;
                }
                this.mtGoAvant(this.MT_MoveNumber);
                break;
              }
              if (this.MT_Speed != 0)
              {
                this.reverse();
                break;
              }
              if ((this.rmStopSpeed & 32768 /*0x8000*/) != 0)
              {
                this.start();
                this.reverse();
                break;
              }
              this.mtGoArriere(this.MT_MoveNumber - 1);
              break;
            }
            if (index <= this.MT_MoveNumber)
            {
              if (this.MT_Speed != 0)
                break;
              if ((this.rmStopSpeed & 32768 /*0x8000*/) != 0)
              {
                this.start();
                break;
              }
              this.mtGoArriere(this.MT_MoveNumber - 1);
              break;
            }
            if (this.MT_Speed != 0)
            {
              this.reverse();
              break;
            }
            if ((this.rmStopSpeed & 32768 /*0x8000*/) != 0)
            {
              this.start();
              this.reverse();
              break;
            }
            this.mtGoAvant(this.MT_MoveNumber);
            break;
          }
        }
      }

      public override void stop()
      {
        if (this.rmStopSpeed == 0)
          this.rmStopSpeed = this.MT_Speed | 32768 /*0x8000*/;
        this.MT_Speed = 0;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public override void start()
      {
        if ((this.rmStopSpeed & 32768 /*0x8000*/) == 0)
          return;
        this.MT_Speed = this.rmStopSpeed & (int) short.MaxValue;
        this.MT_Pause = 0;
        this.rmStopSpeed = 0;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public override void reverse()
      {
        if (this.rmStopSpeed != 0)
          return;
        this.hoPtr.rom.rmMoveFlag = true;
        int mtMoveNumber = this.MT_MoveNumber;
        if (this.MT_Calculs == 0)
        {
          this.MT_Direction = !this.MT_Direction;
          if (this.MT_Direction)
          {
            if (mtMoveNumber == 0)
              this.MT_Direction = !this.MT_Direction;
            else
              this.mtGoArriere(mtMoveNumber - 1);
          }
          else
            this.mtGoAvant(mtMoveNumber);
        }
        else
        {
          this.MT_Direction = !this.MT_Direction;
          this.MT_Cosinus = -this.MT_Cosinus;
          this.MT_Sinus = -this.MT_Sinus;
          int mtXorigin = this.MT_XOrigin;
          this.MT_XOrigin = this.MT_XDest;
          this.MT_XDest = mtXorigin;
          int mtYorigin = this.MT_YOrigin;
          this.MT_YOrigin = this.MT_YDest;
          this.MT_YDest = mtYorigin;
          this.hoPtr.roc.rcDir += 16 /*0x10*/;
          this.hoPtr.roc.rcDir &= 31 /*0x1F*/;
          this.MT_Calculs = this.MT_Longueur - (this.MT_Calculs >> 16 /*0x10*/ & (int) ushort.MaxValue) << 16 /*0x10*/ | this.MT_Calculs & (int) ushort.MaxValue;
        }
      }

      public override void setXPosition(int x)
      {
        int hoX = this.hoPtr.hoX;
        this.hoPtr.hoX = x;
        int num = hoX - this.MT_XOrigin;
        x -= num;
        this.MT_XDest = this.MT_XDest - this.MT_XOrigin + x;
        int mtXorigin = this.MT_XOrigin;
        this.MT_XOrigin = x;
        this.MT_XStart -= mtXorigin - x;
        this.hoPtr.rom.rmMoveFlag = true;
        this.hoPtr.roc.rcChanged = true;
        this.hoPtr.roc.rcCheckCollides = true;
      }

      public override void setYPosition(int y)
      {
        int hoY = this.hoPtr.hoY;
        this.hoPtr.hoY = y;
        int num = hoY - this.MT_YOrigin;
        y -= num;
        this.MT_YDest = this.MT_YDest - this.MT_YOrigin + y;
        int mtYorigin = this.MT_YOrigin;
        this.MT_YOrigin = y;
        this.MT_YStart -= mtYorigin - y;
        this.hoPtr.rom.rmMoveFlag = true;
        this.hoPtr.roc.rcChanged = true;
        this.hoPtr.roc.rcCheckCollides = true;
      }

      public override void setSpeed(int speed)
      {
        if (speed < 0)
          speed = 0;
        if (speed > 250)
          speed = 250;
        this.MT_Speed = speed;
        this.hoPtr.roc.rcSpeed = speed;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public override void setMaxSpeed(int speed) => this.setSpeed(speed);
    }
}
