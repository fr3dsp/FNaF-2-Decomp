// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMovePlatform
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using System;

namespace RuntimeXNA.Movements
{

    internal class CMovePlatform : CMove
    {
      public const short MPJC_NOJUMP = 0;
      public const short MPJC_DIAGO = 1;
      public const short MPJC_BUTTON1 = 2;
      public const short MPJC_BUTTON2 = 3;
      public const short MPTYPE_WALK = 0;
      public const short MPTYPE_CLIMB = 1;
      public const short MPTYPE_JUMP = 2;
      public const short MPTYPE_FALL = 3;
      public const short MPTYPE_CROUCH = 4;
      public const short MPTYPE_UNCROUCH = 5;
      public int MP_Type;
      public int MP_Bounce;
      public int MP_BounceMu;
      public int MP_XSpeed;
      public int MP_Gravity;
      public int MP_Jump;
      public int MP_YSpeed;
      public int MP_XMB;
      public int MP_YMB;
      public int MP_HTFOOT;
      public int MP_JumpControl;
      public int MP_JumpStopped;
      public int MP_PreviousDir;
      public CObject MP_ObjectUnder;
      public int MP_XObjectUnder;
      public int MP_YObjectUnder;
      public bool MP_NoJump;

      public override void init(CObject ho, CMoveDef mvPtr)
      {
        this.hoPtr = ho;
        CMoveDefPlatform cmoveDefPlatform = (CMoveDefPlatform) mvPtr;
        this.hoPtr.hoCalculX = 0;
        this.hoPtr.hoCalculY = 0;
        this.MP_XSpeed = 0;
        this.hoPtr.roc.rcSpeed = 0;
        this.MP_Bounce = 0;
        this.hoPtr.roc.rcPlayer = (int) mvPtr.mvControl;
        this.rmAcc = (int) cmoveDefPlatform.mpAcc;
        this.rmAccValue = this.getAccelerator(this.rmAcc);
        this.rmDec = (int) cmoveDefPlatform.mpDec;
        this.rmDecValue = this.getAccelerator(this.rmDec);
        this.hoPtr.roc.rcMaxSpeed = (int) cmoveDefPlatform.mpSpeed;
        this.hoPtr.roc.rcMinSpeed = 0;
        this.MP_Gravity = (int) cmoveDefPlatform.mpGravity;
        this.MP_Jump = (int) cmoveDefPlatform.mpJump;
        int num = (int) cmoveDefPlatform.mpJumpControl;
        if (num > 3)
          num = 1;
        this.MP_JumpControl = num;
        this.MP_YSpeed = 0;
        this.MP_JumpStopped = 0;
        this.MP_ObjectUnder = (CObject) null;
        this.moveAtStart(mvPtr);
        this.MP_PreviousDir = this.hoPtr.roc.rcDir;
        this.hoPtr.roc.rcChanged = true;
        this.MP_Type = 0;
      }

      public override void move()
      {
        this.hoPtr.hoAdRunHeader.rhVBLObjet = 1;
        int num1 = (int) this.hoPtr.hoAdRunHeader.rhPlayer[this.hoPtr.roc.rcPlayer - 1];
        this.calcMBFoot();
        int num2 = this.MP_XSpeed;
        if (this.MP_JumpStopped == 0)
        {
          if (num2 <= 0)
          {
            if ((num1 & 4) != 0)
            {
              int num3 = this.rmAccValue;
              if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
                num3 = (int) ((double) num3 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
              num2 -= num3;
              if (num2 / 256 /*0x0100*/ < -this.hoPtr.roc.rcMaxSpeed)
                num2 = -this.hoPtr.roc.rcMaxSpeed * 256 /*0x0100*/;
            }
            else if (num2 < 0)
            {
              int num4 = this.rmDecValue;
              if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
                num4 = (int) ((double) num4 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
              num2 += num4;
              if (num2 > 0)
                num2 = 0;
            }
            if ((num1 & 8) != 0)
              num2 = -num2;
          }
          if (num2 >= 0)
          {
            if ((num1 & 8) != 0)
            {
              int num5 = this.rmAccValue;
              if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
                num5 = (int) ((double) num5 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
              num2 += num5;
              if (num2 / 256 /*0x0100*/ > this.hoPtr.roc.rcMaxSpeed)
                num2 = this.hoPtr.roc.rcMaxSpeed * 256 /*0x0100*/;
            }
            else if (num2 > 0)
            {
              int num6 = this.rmDecValue;
              if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
                num6 = (int) ((double) num6 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
              num2 -= num6;
              if (num2 < 0)
                num2 = 0;
            }
            if ((num1 & 4) != 0)
              num2 = -num2;
          }
          this.MP_XSpeed = num2;
        }
        int num7 = this.MP_YSpeed;
        bool flag1 = false;
        while (true)
        {
          switch (this.MP_Type)
          {
            case 0:
              if ((num1 & 1) != 0)
              {
                if (this.check_Ladder(this.hoPtr.hoLayer, this.hoPtr.hoX + this.MP_XMB, this.hoPtr.hoY + this.MP_YMB - 4) != int.MaxValue)
                {
                  this.MP_Type = 1;
                  flag1 = true;
                  continue;
                }
                goto label_65;
              }
              if ((num1 & 2) != 0 && this.check_Ladder(this.hoPtr.hoLayer, this.hoPtr.hoX + this.MP_XMB, this.hoPtr.hoY + this.MP_YMB + 4) != int.MaxValue)
              {
                this.MP_Type = 1;
                flag1 = true;
                continue;
              }
              goto label_65;
            case 1:
              goto label_39;
            case 2:
            case 3:
              goto label_30;
            default:
              goto label_65;
          }
        }
    label_30:
        int num8 = this.MP_Gravity << 5;
        if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
          num8 = (int) ((double) num8 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
        num7 += num8;
        if (num7 > 64000)
        {
          num7 = 64000;
          goto label_65;
        }
        goto label_65;
    label_39:
        if (!flag1)
        {
          this.MP_JumpStopped = 0;
          if (this.check_Ladder(this.hoPtr.hoLayer, this.hoPtr.hoX + this.MP_XMB, this.hoPtr.hoY + this.MP_YMB) == int.MaxValue && this.check_Ladder(this.hoPtr.hoLayer, this.hoPtr.hoX + this.MP_XMB, this.hoPtr.hoY + this.MP_YMB - 4) == int.MaxValue)
            goto label_65;
        }
        if (num7 <= 0)
        {
          if ((num1 & 1) != 0)
          {
            int num9 = this.rmAccValue;
            if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
              num9 = (int) ((double) num9 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
            num7 -= num9;
            if (num7 / 256 /*0x0100*/ < -this.hoPtr.roc.rcMaxSpeed)
              num7 = -this.hoPtr.roc.rcMaxSpeed * 256 /*0x0100*/;
          }
          else
          {
            int num10 = this.rmDecValue;
            if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
              num10 = (int) ((double) num10 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
            num7 += num10;
            if (num7 > 0)
              num7 = 0;
          }
          if ((num1 & 2) != 0)
            num7 = -num7;
        }
        if (num7 >= 0)
        {
          if ((num1 & 2) != 0)
          {
            int num11 = this.rmAccValue;
            if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
              num11 = (int) ((double) num11 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
            num7 += num11;
            if (num7 / 256 /*0x0100*/ > this.hoPtr.roc.rcMaxSpeed)
              num7 = this.hoPtr.roc.rcMaxSpeed * 256 /*0x0100*/;
          }
          else
          {
            int num12 = this.rmDecValue;
            if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
              num12 = (int) ((double) num12 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
            num7 -= num12;
            if (num7 < 0)
              num7 = 0;
          }
          if ((num1 & 1) != 0)
            num7 = -num7;
        }
    label_65:
        this.MP_YSpeed = num7;
        int angle = 0;
        if (num2 < 0)
          angle = 16 /*0x10*/;
        int num13 = num2;
        int num14 = num7;
        if (num14 != 0)
        {
          int num15 = 0;
          if (num13 < 0)
          {
            num15 |= 1;
            num13 = -num13;
          }
          if (num14 < 0)
          {
            num15 |= 2;
            num14 = -num14;
          }
          int num16 = (num13 << 8) / num14;
          int index = 0;
          while (num16 < CMove.CosSurSin32[index])
            index += 2;
          angle = CMove.CosSurSin32[index + 1];
          if ((num15 & 2) != 0)
            angle = -angle + 32 /*0x20*/ & 31 /*0x1F*/;
          if ((num15 & 1) != 0)
            angle = (-(angle - 8 & 31 /*0x1F*/) & 31 /*0x1F*/) + 8 & 31 /*0x1F*/;
        }
        int num17 = num2;
        int num18 = CMove.Cosinus32[angle];
        int num19 = CMove.Sinus32[angle];
        if (num18 < 0)
          num18 = -num18;
        if (num19 < 0)
          num19 = -num19;
        if (num18 < num19)
        {
          num18 = num19;
          num17 = num7;
        }
        if (num17 < 0)
          num17 = -num17;
        int num20 = num17 / num18;
        if (num20 > 250)
          num20 = 250;
        this.hoPtr.roc.rcSpeed = num20;
        switch (this.MP_Type)
        {
          case 1:
            if (num7 < 0)
            {
              this.hoPtr.roc.rcDir = 8;
              break;
            }
            if (num7 > 0)
            {
              this.hoPtr.roc.rcDir = 24;
              break;
            }
            break;
          case 3:
            this.hoPtr.roc.rcDir = angle;
            break;
          default:
            if (num2 < 0)
            {
              this.hoPtr.roc.rcDir = 16 /*0x10*/;
              break;
            }
            if (num2 > 0)
            {
              this.hoPtr.roc.rcDir = 0;
              break;
            }
            break;
        }
        switch (this.MP_Type)
        {
          case 1:
            this.hoPtr.roc.rcAnim = 9;
            break;
          case 2:
            this.hoPtr.roc.rcAnim = 7;
            break;
          case 3:
            this.hoPtr.roc.rcAnim = 8;
            break;
          case 4:
            this.hoPtr.roc.rcAnim = 10;
            break;
          case 5:
            this.hoPtr.roc.rcAnim = 11;
            break;
          default:
            this.hoPtr.roc.rcAnim = 1;
            break;
        }
        if (this.hoPtr.roa != null)
        {
          this.hoPtr.roa.animate();
          if (CRun.bMoveChanged)
            return;
        }
        this.calcMBFoot();
        this.newMake_Move(this.hoPtr.roc.rcSpeed, angle);
        if (CRun.bMoveChanged)
          return;
        if ((this.MP_Type == 0 || this.MP_Type == 1) && !this.MP_NoJump)
        {
          bool flag2 = false;
          int mpJumpControl = this.MP_JumpControl;
          if (mpJumpControl != 0)
          {
            int num21 = mpJumpControl - 1;
            if (num21 == 0)
            {
              if ((num1 & 5) == 5)
                flag2 = true;
              if ((num1 & 9) == 9)
                flag2 = true;
            }
            else
            {
              int num22 = num21 << 4;
              if ((num1 & num22) != 0)
                flag2 = true;
            }
          }
          if (flag2)
          {
            this.MP_YSpeed = -this.MP_Jump << 8;
            this.MP_Type = 2;
          }
        }
        switch (this.MP_Type)
        {
          case 0:
            if ((num1 & 3) != 0 && (num1 & 12) == 0 && this.check_Ladder(this.hoPtr.hoLayer, this.hoPtr.hoX + this.MP_XMB, this.hoPtr.hoY + this.MP_YMB) != int.MaxValue)
            {
              this.MP_Type = 1;
              this.MP_XSpeed = 0;
              break;
            }
            if ((num1 & 2) != 0 && this.hoPtr.roa != null && this.hoPtr.roa.anim_Exist(10))
            {
              this.MP_XSpeed = 0;
              this.MP_Type = 4;
            }
            if (this.check_Ladder(this.hoPtr.hoLayer, this.hoPtr.hoX + this.MP_XMB, this.hoPtr.hoY + this.MP_YMB) == int.MaxValue)
            {
              if (!this.tst_SpritePosition(this.hoPtr.hoX, this.hoPtr.hoY + 10, (short) this.MP_HTFOOT, (short) 1, true))
              {
                int num23 = this.hoPtr.hoX - this.hoPtr.hoAdRunHeader.rhWindowX;
                int maxY = this.hoPtr.hoY - this.hoPtr.hoAdRunHeader.rhWindowY;
                int destY = maxY + this.MP_HTFOOT - 1;
                CPoint ptFinal = new CPoint();
                this.mpApproachSprite(num23, destY, num23, maxY, (short) this.MP_HTFOOT, (short) 1, ptFinal);
                this.hoPtr.hoX = ptFinal.x + this.hoPtr.hoAdRunHeader.rhWindowX;
                this.hoPtr.hoY = ptFinal.y + this.hoPtr.hoAdRunHeader.rhWindowY;
                this.MP_NoJump = false;
                break;
              }
              this.MP_Type = 3;
              break;
            }
            break;
          case 1:
            if (this.check_Ladder(this.hoPtr.hoLayer, this.hoPtr.hoX + this.MP_XMB, this.hoPtr.hoY + this.MP_YMB) == int.MaxValue)
            {
              if (this.MP_YSpeed < 0)
              {
                for (int index = 0; index < 32 /*0x20*/; ++index)
                {
                  if (this.check_Ladder(this.hoPtr.hoLayer, this.hoPtr.hoX + this.MP_XMB, this.hoPtr.hoY + this.MP_YMB + index) != int.MaxValue)
                  {
                    this.hoPtr.hoY += index;
                    break;
                  }
                }
              }
              this.MP_YSpeed = 0;
            }
            if ((num1 & 12) != 0)
            {
              this.MP_Type = 0;
              this.MP_YSpeed = 0;
              break;
            }
            break;
          case 2:
            if (this.MP_YSpeed >= 0)
            {
              this.MP_Type = 3;
              break;
            }
            break;
          case 3:
            if (this.check_Ladder(this.hoPtr.hoLayer, this.hoPtr.hoX + this.MP_XMB, this.hoPtr.hoY + this.MP_YMB) != int.MaxValue)
            {
              this.MP_YSpeed = 0;
              this.MP_Type = 1;
              this.hoPtr.roc.rcDir = 8;
              break;
            }
            break;
          case 4:
            if ((num1 & 2) == 0)
            {
              if (this.hoPtr.roa != null && this.hoPtr.roa.anim_Exist(11))
              {
                this.MP_Type = 5;
                this.hoPtr.roc.rcAnim = 11;
                this.hoPtr.roa.animate();
                this.hoPtr.roa.raAnimRepeat = 1;
                break;
              }
              this.MP_Type = 0;
              break;
            }
            break;
          case 5:
            if (this.hoPtr.roa != null && this.hoPtr.roa.raAnimNumberOfFrame == 0)
            {
              this.MP_Type = 0;
              break;
            }
            break;
        }
        if (this.MP_Type == 0 || this.MP_Type == 4 || this.MP_Type == 5)
        {
          if (this.hoPtr.hoAdRunHeader.objectAllCol_IXY(this.hoPtr, this.hoPtr.roc.rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY, this.hoPtr.hoX, this.hoPtr.hoY, this.hoPtr.hoOiList.oilColList) == null)
          {
            CArrayList carrayList = this.hoPtr.hoAdRunHeader.objectAllCol_IXY(this.hoPtr, this.hoPtr.roc.rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY, this.hoPtr.hoX, this.hoPtr.hoY + 1, this.hoPtr.hoOiList.oilColList);
            if (carrayList != null && carrayList.size() == 1)
            {
              CObject cobject = (CObject) carrayList.get(0);
              if ((this.MP_ObjectUnder == null || this.MP_ObjectUnder != cobject) && (int) this.hoPtr.hoOi != (int) cobject.hoOi)
              {
                this.MP_ObjectUnder = cobject;
                this.MP_XObjectUnder = cobject.hoX;
                this.MP_YObjectUnder = cobject.hoY;
                return;
              }
              int num24 = cobject.hoX - this.MP_XObjectUnder;
              int num25 = cobject.hoY - this.MP_YObjectUnder;
              this.MP_XObjectUnder = cobject.hoX;
              this.MP_YObjectUnder = cobject.hoY;
              this.hoPtr.hoX += num24;
              this.hoPtr.hoY += num25;
              this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr);
              this.hoPtr.roc.rcChanged = true;
              return;
            }
          }
          this.MP_ObjectUnder = (CObject) null;
        }
        else
          this.MP_ObjectUnder = (CObject) null;
      }

      private void mpStopIt()
      {
        this.hoPtr.roc.rcSpeed = 0;
        this.MP_XSpeed = 0;
        this.MP_YSpeed = 0;
      }

      public override void stop()
      {
        this.MP_Bounce = 0;
        if ((int) this.rmCollisionCount != (int) this.hoPtr.hoAdRunHeader.rh3CollisionCount)
        {
          this.mpStopIt();
        }
        else
        {
          this.hoPtr.rom.rmMoveFlag = true;
          int num1 = this.hoPtr.hoX - this.hoPtr.hoAdRunHeader.rhWindowX;
          int destY = this.hoPtr.hoY - this.hoPtr.hoAdRunHeader.rhWindowY;
          switch (this.hoPtr.hoAdRunHeader.rhEvtProg.rhCurCode >> 16 /*0x10*/)
          {
            case -14:
            case -13:
              this.MP_NoJump = false;
              CPoint ptFinal = new CPoint();
              if (this.MP_Type == 3)
              {
                this.mpApproachSprite(num1, destY, this.hoPtr.roc.rcOldX - this.hoPtr.hoAdRunHeader.rhWindowX, this.hoPtr.roc.rcOldY - this.hoPtr.hoAdRunHeader.rhWindowY, (short) this.MP_HTFOOT, (short) 1, ptFinal);
                this.hoPtr.hoX = ptFinal.x + this.hoPtr.hoAdRunHeader.rhWindowX;
                this.hoPtr.hoY = ptFinal.y + this.hoPtr.hoAdRunHeader.rhWindowY;
                this.MP_Type = 0;
                this.hoPtr.roc.rcChanged = true;
                if (this.tst_SpritePosition(this.hoPtr.hoX, this.hoPtr.hoY + 1, (short) 0, (short) 1, true))
                {
                  this.hoPtr.roc.rcSpeed = 0;
                  this.MP_XSpeed = 0;
                  break;
                }
                this.MP_JumpStopped = 0;
                this.hoPtr.roc.rcSpeed = Math.Abs(this.MP_XSpeed / 256 /*0x0100*/);
                this.MP_YSpeed = 0;
                break;
              }
              if (this.MP_Type == 0)
              {
                if (this.mpApproachSprite(num1, destY, num1, destY - this.MP_HTFOOT, (short) 0, (short) 1, ptFinal))
                {
                  this.hoPtr.hoX = ptFinal.x + this.hoPtr.hoAdRunHeader.rhWindowX;
                  this.hoPtr.hoY = ptFinal.y + this.hoPtr.hoAdRunHeader.rhWindowY;
                  this.hoPtr.roc.rcChanged = true;
                  break;
                }
                if (this.mpApproachSprite(num1, destY, this.hoPtr.roc.rcOldX - this.hoPtr.hoAdRunHeader.rhWindowX, this.hoPtr.roc.rcOldY - this.hoPtr.hoAdRunHeader.rhWindowY, (short) 0, (short) 1, ptFinal))
                {
                  this.hoPtr.hoX = ptFinal.x + this.hoPtr.hoAdRunHeader.rhWindowX;
                  this.hoPtr.hoY = ptFinal.y + this.hoPtr.hoAdRunHeader.rhWindowY;
                  this.hoPtr.roc.rcChanged = true;
                  this.mpStopIt();
                  break;
                }
              }
              if (this.MP_Type == 2)
              {
                if (this.mpApproachSprite(num1, destY, num1, destY - this.MP_HTFOOT, (short) 0, (short) 1, ptFinal))
                {
                  this.hoPtr.hoX = ptFinal.x + this.hoPtr.hoAdRunHeader.rhWindowX;
                  this.hoPtr.hoY = ptFinal.y + this.hoPtr.hoAdRunHeader.rhWindowY;
                  this.hoPtr.roc.rcChanged = true;
                  break;
                }
                this.MP_JumpStopped = 1;
                this.MP_XSpeed = 0;
              }
              if (this.MP_Type == 1 && this.mpApproachSprite(num1, destY, this.hoPtr.roc.rcOldX - this.hoPtr.hoAdRunHeader.rhWindowX, this.hoPtr.roc.rcOldY - this.hoPtr.hoAdRunHeader.rhWindowY, (short) 0, (short) 1, ptFinal))
              {
                this.hoPtr.hoX = ptFinal.x + this.hoPtr.hoAdRunHeader.rhWindowX;
                this.hoPtr.hoY = ptFinal.y + this.hoPtr.hoAdRunHeader.rhWindowY;
                this.hoPtr.roc.rcChanged = true;
                this.mpStopIt();
                break;
              }
              this.hoPtr.roc.rcImage = this.hoPtr.roc.rcOldImage;
              this.hoPtr.roc.rcAngle = this.hoPtr.roc.rcOldAngle;
              if (this.tst_SpritePosition(this.hoPtr.hoX, this.hoPtr.hoY, (short) 0, (short) 1, true))
                break;
              this.hoPtr.hoX = this.hoPtr.roc.rcOldX;
              this.hoPtr.hoY = this.hoPtr.roc.rcOldY;
              this.hoPtr.roc.rcChanged = true;
              break;
            case -12:
              int x1 = this.hoPtr.hoX - this.hoPtr.hoImgXSpot;
              int y1 = this.hoPtr.hoY - this.hoPtr.hoImgYSpot;
              int num2 = this.hoPtr.hoAdRunHeader.quadran_Out(x1, y1, x1 + this.hoPtr.hoImgWidth, y1 + this.hoPtr.hoImgHeight);
              int num3 = this.hoPtr.hoX;
              int num4 = this.hoPtr.hoY;
              if ((num2 & 1) != 0)
              {
                num3 = this.hoPtr.hoImgXSpot;
                this.MP_XSpeed = 0;
                this.MP_NoJump = true;
              }
              if ((num2 & 2) != 0)
              {
                num3 = this.hoPtr.hoAdRunHeader.rhLevelSx - this.hoPtr.hoImgWidth + this.hoPtr.hoImgXSpot;
                this.MP_XSpeed = 0;
                this.MP_NoJump = true;
              }
              if ((num2 & 4) != 0)
              {
                num4 = this.hoPtr.hoImgYSpot;
                this.MP_YSpeed = 0;
                this.MP_NoJump = false;
              }
              if ((num2 & 8) != 0)
              {
                num4 = this.hoPtr.hoAdRunHeader.rhLevelSy - this.hoPtr.hoImgHeight + this.hoPtr.hoImgYSpot;
                this.MP_YSpeed = 0;
                this.MP_NoJump = false;
              }
              this.hoPtr.hoX = num3;
              this.hoPtr.hoY = num4;
              this.MP_Type = this.MP_Type != 2 ? 0 : 3;
              this.MP_JumpStopped = 0;
              break;
          }
        }
      }

      public override void bounce() => this.stop();

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

      public override void setSpeed(int speed)
      {
        if (speed < 0)
          speed = 0;
        if (speed > 250)
          speed = 250;
        if (speed > this.hoPtr.roc.rcMaxSpeed)
          speed = this.hoPtr.roc.rcMaxSpeed;
        this.hoPtr.roc.rcSpeed = speed;
        this.MP_XSpeed = this.hoPtr.roc.rcSpeed * CMove.Cosinus32[this.hoPtr.roc.rcDir];
        this.MP_YSpeed = this.hoPtr.roc.rcSpeed * CMove.Sinus32[this.hoPtr.roc.rcDir];
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
        if (this.MP_XSpeed > speed)
          this.MP_XSpeed = speed;
        this.hoPtr.rom.rmMoveFlag = true;
      }

      public void MPSetGravity(int gravity) => this.MP_Gravity = gravity;

      public override void setDir(int dir)
      {
        this.hoPtr.roc.rcDir = dir;
        this.MP_XSpeed = this.hoPtr.roc.rcSpeed * CMove.Cosinus32[dir];
        this.MP_YSpeed = this.hoPtr.roc.rcSpeed * CMove.Sinus32[dir];
      }

      private void calcMBFoot()
      {
        CImage cimage;
        if (this.hoPtr.roc.rcImage != (short) 0)
        {
          cimage = this.hoPtr.hoAdRunHeader.rhApp.imageBank.getImageInfoEx(this.hoPtr.roc.rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY);
        }
        else
        {
          cimage = new CImage();
          cimage.width = (short) this.hoPtr.hoImgWidth;
          cimage.height = (short) this.hoPtr.hoImgHeight;
          cimage.xSpot = (short) this.hoPtr.hoImgXSpot;
          cimage.ySpot = (short) this.hoPtr.hoImgYSpot;
        }
        this.MP_XMB = -this.hoPtr.hoAdRunHeader.rhWindowX;
        this.MP_YMB = (int) cimage.height - this.hoPtr.hoAdRunHeader.rhWindowY - (int) cimage.ySpot;
        this.MP_HTFOOT = (int) cimage.height * 2 + (int) cimage.height >> 3;
      }

      private int check_Ladder(int nLayer, int x, int y)
      {
        CRect ladderAt = this.hoPtr.hoAdRunHeader.y_GetLadderAt(nLayer, x, y);
        return ladderAt != null ? ladderAt.top : int.MaxValue;
      }

      public void mpHandle_Background()
      {
        this.calcMBFoot();
        if (this.check_Ladder(this.hoPtr.hoLayer, this.hoPtr.hoX + this.MP_XMB, this.hoPtr.hoY + this.MP_YMB) != int.MaxValue || this.hoPtr.hoAdRunHeader.colMask_TestObject_IXY(this.hoPtr, this.hoPtr.roc.rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY, this.hoPtr.hoX, this.hoPtr.hoY, 0, 0) == 0 && (this.MP_Type == 2 && this.MP_YSpeed < 0 || this.hoPtr.hoAdRunHeader.colMask_TestObject_IXY(this.hoPtr, this.hoPtr.roc.rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY, this.hoPtr.hoX, this.hoPtr.hoY, (int) (short) this.MP_HTFOOT, 1) == 0))
          return;
        this.hoPtr.hoAdRunHeader.rhEvtProg.handle_Event(this.hoPtr, -851968 | (int) this.hoPtr.hoType & (int) ushort.MaxValue);
      }
    }
}
