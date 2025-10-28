// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Animations.CRAni
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Objects;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Animations
{

    public class CRAni
    {
      private static short[] anim_Defined = new short[15]
      {
        (short) 0,
        (short) 1,
        (short) 2,
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
        (short) -1
      };
      public CObject hoPtr;
      public int raAnimForced;
      public int raAnimDirForced;
      public int raAnimSpeedForced;
      public bool raAnimStopped;
      public int raAnimOn;
      public CAnim raAnimOffset;
      public int raAnimDir;
      public int raAnimPreviousDir;
      public CAnimDir raAnimDirOffset;
      public int raAnimSpeed;
      public int raAnimMinSpeed;
      public int raAnimMaxSpeed;
      public int raAnimDeltaSpeed;
      public int raAnimCounter;
      public int raAnimDelta;
      public int raAnimRepeat;
      public int raAnimRepeatLoop;
      public int raAnimFrame;
      public int raAnimNumberOfFrame;
      public int raAnimFrameForced;
      public int raRoutineAnimation;
      public int raOldAngle = -1;

      public void init(CObject ho)
      {
        this.hoPtr = ho;
        this.raRoutineAnimation = 0;
        this.init_Animation(1);
        if (this.anim_Exist(3))
        {
          this.raRoutineAnimation = 1;
          this.animation_Force(3);
          this.animation_OneLoop();
          this.animations();
        }
        else
        {
          int index = 0;
          while (CRAni.anim_Defined[index] >= (short) 0 && !this.anim_Exist((int) CRAni.anim_Defined[index]))
            ++index;
          if (CRAni.anim_Defined[index] >= (short) 0 || !this.anim_Exist(4))
            return;
          this.raRoutineAnimation = 2;
          this.animation_Force(4);
          this.animation_OneLoop();
          this.animations();
        }
      }

      public void init_Animation(int anim)
      {
        this.hoPtr.roc.rcAnim = anim;
        this.raAnimStopped = false;
        this.raAnimForced = 0;
        this.raAnimDirForced = 0;
        this.raAnimSpeedForced = 0;
        this.raAnimFrameForced = 0;
        this.raAnimCounter = 0;
        this.raAnimFrame = 0;
        this.raAnimOffset = (CAnim) null;
        this.raAnimDirOffset = (CAnimDir) null;
        this.raAnimOn = -1;
        this.raAnimMinSpeed = -1;
        this.raAnimPreviousDir = -1;
        this.raAnimOffset = (CAnim) null;
        this.raAnimDirOffset = (CAnimDir) null;
        this.animations();
      }

      private void check_Animate() => this.animIn(0);

      public void extAnimations(int anim)
      {
        this.hoPtr.roc.rcAnim = anim;
        this.animate();
      }

      public bool animate()
      {
        switch (this.raRoutineAnimation)
        {
          case 0:
            return this.animations();
          case 1:
            this.anim_Appear();
            break;
          case 2:
            this.anim_Disappear();
            break;
        }
        return false;
      }

      public bool animations()
      {
        int hoX = this.hoPtr.hoX;
        this.hoPtr.roc.rcOldX = hoX;
        int num1 = hoX - this.hoPtr.hoImgXSpot;
        this.hoPtr.roc.rcOldX1 = num1;
        this.hoPtr.roc.rcOldX2 = num1 + this.hoPtr.hoImgWidth;
        int hoY = this.hoPtr.hoY;
        this.hoPtr.roc.rcOldY = hoY;
        int num2 = hoY - this.hoPtr.hoImgYSpot;
        this.hoPtr.roc.rcOldY1 = num2;
        this.hoPtr.roc.rcOldY2 = num2 + this.hoPtr.hoImgHeight;
        this.hoPtr.roc.rcOldImage = this.hoPtr.roc.rcImage;
        this.hoPtr.roc.rcOldAngle = this.hoPtr.roc.rcAngle;
        return this.animIn(1);
      }

      public bool animIn(int vbl)
      {
        CRun.bMoveChanged = false;
        CObjectCommon hoCommon = this.hoPtr.hoCommon;
        int num1 = this.hoPtr.roc.rcSpeed;
        int index1 = this.hoPtr.roc.rcAnim;
        if (this.raAnimSpeedForced != 0)
          num1 = this.raAnimSpeedForced - 1;
        if (index1 == 1)
        {
          if (num1 == 0)
            index1 = 0;
          if (num1 >= 75)
            index1 = 2;
        }
        if (this.raAnimForced != 0)
          index1 = this.raAnimForced - 1;
        if (index1 != this.raAnimOn)
        {
          this.raAnimOn = index1;
          if (index1 >= (int) hoCommon.ocAnimations.ahAnimMax)
            index1 = (int) hoCommon.ocAnimations.ahAnimMax - 1;
          CAnim ahAnim = hoCommon.ocAnimations.ahAnims[index1];
          if (ahAnim != this.raAnimOffset)
          {
            this.raAnimOffset = ahAnim;
            this.raAnimDir = -1;
            this.raAnimFrame = 0;
          }
        }
        int index2 = this.hoPtr.roc.rcDir;
        if (this.raAnimDirForced != 0)
          index2 = this.raAnimDirForced - 1;
        bool flag = false;
        if (this.raAnimDir != index2)
        {
          this.raAnimDir = index2;
          CAnimDir anDir;
          if (this.raAnimOffset.anDirs[index2] == null)
          {
            int index3;
            if (((int) this.raAnimOffset.anAntiTrigo[index2] & 64 /*0x40*/) != 0)
              index3 = (int) this.raAnimOffset.anAntiTrigo[index2] & 63 /*0x3F*/;
            else if (((int) this.raAnimOffset.anTrigo[index2] & 64 /*0x40*/) != 0)
            {
              index3 = (int) this.raAnimOffset.anTrigo[index2] & 63 /*0x3F*/;
            }
            else
            {
              int index4 = index2;
              index3 = this.raAnimPreviousDir >= 0 ? ((index2 - this.raAnimPreviousDir & 31 /*0x1F*/) <= 15 ? (int) this.raAnimOffset.anAntiTrigo[index4] & 63 /*0x3F*/ : (int) this.raAnimOffset.anTrigo[index4] & 63 /*0x3F*/) : (int) this.raAnimOffset.anTrigo[index2] & 63 /*0x3F*/;
            }
            anDir = this.raAnimOffset.anDirs[index3];
          }
          else
          {
            this.raAnimPreviousDir = index2;
            anDir = this.raAnimOffset.anDirs[index2];
          }
          if (this.raAnimOffset.anDirs[0] != null && ((int) this.hoPtr.hoCommon.ocFlags2 & 64 /*0x40*/) != 0)
          {
            this.hoPtr.roc.rcAngle = this.raAnimDir * 360 / 32 /*0x20*/;
            anDir = this.raAnimOffset.anDirs[0];
            this.raAnimDirOffset = (CAnimDir) null;
            flag = true;
          }
          if (this.raAnimDirOffset != anDir)
          {
            this.raAnimDirOffset = anDir;
            this.raAnimRepeat = (int) anDir.adRepeat;
            this.raAnimRepeatLoop = (int) anDir.adRepeatFrame;
            int adMinSpeed = (int) anDir.adMinSpeed;
            int adMaxSpeed = (int) anDir.adMaxSpeed;
            if (adMinSpeed != this.raAnimMinSpeed || adMaxSpeed != this.raAnimMaxSpeed)
            {
              this.raAnimMinSpeed = adMinSpeed;
              this.raAnimMaxSpeed = adMaxSpeed;
              this.raAnimDeltaSpeed = adMaxSpeed - adMinSpeed;
              this.raAnimDelta = adMinSpeed;
              this.raAnimSpeed = -1;
            }
            this.raAnimNumberOfFrame = (int) anDir.adNumberOfFrame;
            if (this.raAnimFrameForced != 0 && this.raAnimFrameForced - 1 >= this.raAnimNumberOfFrame)
              this.raAnimFrameForced = 0;
            if (this.raAnimFrame >= this.raAnimNumberOfFrame)
              this.raAnimFrame = 0;
            short adFrame = anDir.adFrames[this.raAnimFrame];
            if (!this.raAnimStopped)
            {
              this.hoPtr.roc.rcImage = adFrame;
              CImage imageInfoEx = this.hoPtr.hoAdRunHeader.rhApp.imageBank.getImageInfoEx(adFrame, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY);
              this.hoPtr.hoImgWidth = (int) imageInfoEx.width;
              this.hoPtr.hoImgHeight = (int) imageInfoEx.height;
              this.hoPtr.hoImgXSpot = (int) imageInfoEx.xSpot;
              this.hoPtr.hoImgYSpot = (int) imageInfoEx.ySpot;
              this.hoPtr.roc.rcChanged = true;
              this.hoPtr.roc.rcCheckCollides = true;
            }
            if (this.raAnimNumberOfFrame == 1)
            {
              if (this.raAnimMinSpeed == 0)
                this.raAnimNumberOfFrame = 0;
              short rcImage = this.hoPtr.roc.rcImage;
              if (rcImage == (short) 0)
                return false;
              CImage imageInfoEx = this.hoPtr.hoAdRunHeader.rhApp.imageBank.getImageInfoEx(rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY);
              this.hoPtr.hoImgWidth = (int) imageInfoEx.width;
              this.hoPtr.hoImgHeight = (int) imageInfoEx.height;
              this.hoPtr.hoImgXSpot = (int) imageInfoEx.xSpot;
              this.hoPtr.hoImgYSpot = (int) imageInfoEx.ySpot;
              return false;
            }
          }
        }
        if (vbl == 0 && this.raAnimFrameForced == 0 || !flag && this.raAnimNumberOfFrame == 0)
          return false;
        int raAnimDeltaSpeed = this.raAnimDeltaSpeed;
        if (num1 != this.raAnimSpeed)
        {
          this.raAnimSpeed = num1;
          if (raAnimDeltaSpeed == 0)
          {
            this.raAnimDelta = this.raAnimMinSpeed;
            if (this.raAnimSpeedForced != 0)
              this.raAnimDelta = this.raAnimSpeedForced - 1;
          }
          else
          {
            int num2 = this.hoPtr.roc.rcMaxSpeed - this.hoPtr.roc.rcMinSpeed;
            if (num2 == 0)
            {
              if (this.raAnimSpeedForced != 0)
              {
                int num3 = raAnimDeltaSpeed * num1 / 100 + this.raAnimMinSpeed;
                if (num3 > this.raAnimMaxSpeed)
                  num3 = this.raAnimMaxSpeed;
                this.raAnimDelta = num3;
              }
              else
                this.raAnimDelta = raAnimDeltaSpeed / 2 + this.raAnimMinSpeed;
            }
            else
            {
              int num4 = raAnimDeltaSpeed * num1 / num2 + this.raAnimMinSpeed;
              if (num4 > this.raAnimMaxSpeed)
                num4 = this.raAnimMaxSpeed;
              this.raAnimDelta = num4;
            }
          }
        }
        CAnimDir raAnimDirOffset = this.raAnimDirOffset;
        int raAnimFrameForced = this.raAnimFrameForced;
        int index5;
        if (raAnimFrameForced == 0)
        {
          if (this.raAnimDelta == 0 || this.raAnimStopped)
            return false;
          int raAnimCounter = this.raAnimCounter;
          index5 = this.raAnimFrame;
          int num5 = this.raAnimDelta;
          if ((this.hoPtr.hoAdRunHeader.rhFrame.leFlags & 32768 /*0x8000*/) != 0)
            num5 = (int) ((double) num5 * this.hoPtr.hoAdRunHeader.rh4MvtTimerCoef);
          int num6 = raAnimCounter + num5;
          while (num6 > 100)
          {
            num6 -= 100;
            ++index5;
            if (index5 >= this.raAnimNumberOfFrame)
            {
              index5 = this.raAnimRepeatLoop;
              if (this.raAnimRepeat != 0)
              {
                --this.raAnimRepeat;
                if (this.raAnimRepeat == 0)
                {
                  this.raAnimFrame = this.raAnimNumberOfFrame;
                  this.raAnimNumberOfFrame = 0;
                  if (this.raAnimForced != 0)
                  {
                    this.raAnimForced = 0;
                    this.raAnimDirForced = 0;
                    this.raAnimSpeedForced = 0;
                  }
                  if (((int) this.hoPtr.hoAdRunHeader.rhGameFlags & 512 /*0x0200*/) != 0)
                    return false;
                  if (flag)
                  {
                    this.hoPtr.roc.rcChanged = true;
                    this.hoPtr.roc.rcCheckCollides = true;
                    CImage imageInfoEx = this.hoPtr.hoAdRunHeader.rhApp.imageBank.getImageInfoEx(this.hoPtr.roc.rcImage, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY);
                    this.hoPtr.hoImgWidth = (int) imageInfoEx.width;
                    this.hoPtr.hoImgHeight = (int) imageInfoEx.height;
                    this.hoPtr.hoImgXSpot = (int) imageInfoEx.xSpot;
                    this.hoPtr.hoImgYSpot = (int) imageInfoEx.ySpot;
                  }
                  int code = -131072 | (int) this.hoPtr.hoType & (int) ushort.MaxValue;
                  this.hoPtr.hoAdRunHeader.rhEvtProg.rhCurParam0 = this.hoPtr.roa.raAnimOn;
                  return this.hoPtr.hoAdRunHeader.rhEvtProg.handle_Event(this.hoPtr, code);
                }
              }
            }
          }
          this.raAnimCounter = num6;
        }
        else
        {
          index5 = raAnimFrameForced - 1;
          if (index5 < 0)
            index5 = 0;
        }
        this.raAnimFrame = index5;
        short adFrame1 = raAnimDirOffset.adFrames[index5];
        this.hoPtr.roc.rcChanged = true;
        this.hoPtr.roc.rcCheckCollides = true;
        if ((int) adFrame1 != (int) this.hoPtr.roc.rcImage || this.raOldAngle != this.hoPtr.roc.rcAngle)
        {
          this.hoPtr.roc.rcImage = adFrame1;
          this.raOldAngle = this.hoPtr.roc.rcAngle;
          if (adFrame1 < (short) 0)
            return false;
          CImage imageInfoEx = this.hoPtr.hoAdRunHeader.rhApp.imageBank.getImageInfoEx(adFrame1, this.hoPtr.roc.rcAngle, this.hoPtr.roc.rcScaleX, this.hoPtr.roc.rcScaleY);
          this.hoPtr.hoImgWidth = (int) imageInfoEx.width;
          this.hoPtr.hoImgHeight = (int) imageInfoEx.height;
          this.hoPtr.hoImgXSpot = (int) imageInfoEx.xSpot;
          this.hoPtr.hoImgYSpot = (int) imageInfoEx.ySpot;
        }
        return false;
      }

      public bool anim_Exist(int animId)
      {
        return this.hoPtr.hoCommon.ocAnimations.ahAnimExists[animId] != (byte) 0;
      }

      public void animation_OneLoop()
      {
        if (this.raAnimRepeat != 0)
          return;
        this.raAnimRepeat = 1;
      }

      public void animation_Force(int anim)
      {
        this.raAnimForced = anim + 1;
        this.animIn(0);
      }

      public void animation_Restore()
      {
        this.raAnimForced = 0;
        this.animIn(0);
      }

      public void animDir_Force(int dir)
      {
        dir &= 31 /*0x1F*/;
        this.raAnimDirForced = dir + 1;
        this.animIn(0);
      }

      public void animDir_Restore()
      {
        this.raAnimDirForced = 0;
        this.animIn(0);
      }

      public void animSpeed_Force(int speed)
      {
        if (speed < 0)
          speed = 0;
        if (speed > 100)
          speed = 100;
        this.raAnimSpeedForced = speed + 1;
        this.animIn(0);
      }

      public void animSpeed_Restore()
      {
        this.raAnimSpeedForced = 0;
        this.animIn(0);
      }

      public void anim_Restart()
      {
        this.raAnimOn = -1;
        this.animIn(0);
      }

      public void animFrame_Force(int frame)
      {
        if (frame >= this.raAnimNumberOfFrame)
          frame = this.raAnimNumberOfFrame - 1;
        if (frame < 0)
          frame = 0;
        this.raAnimFrameForced = frame + 1;
        this.animIn(0);
      }

      public void animFrame_Restore()
      {
        this.raAnimFrameForced = 0;
        this.animIn(0);
      }

      public void anim_Appear()
      {
        this.animIn(1);
        if (this.raAnimForced == 4)
          return;
        if (this.anim_Exist(0) || this.anim_Exist(1) || this.anim_Exist(2))
        {
          this.raRoutineAnimation = 0;
          this.animation_Restore();
        }
        else
        {
          this.raRoutineAnimation = 2;
          this.hoPtr.hoAdRunHeader.init_Disappear(this.hoPtr);
        }
      }

      private void anim_Disappear()
      {
        if (((int) this.hoPtr.hoFlags & 16 /*0x10*/) != 0)
          return;
        this.animIn(1);
        if (this.raAnimForced == 5)
          return;
        this.hoPtr.hoAdRunHeader.destroy_Add((int) this.hoPtr.hoNumber);
      }
    }
}
