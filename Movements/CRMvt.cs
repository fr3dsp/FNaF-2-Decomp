// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CRMvt
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Movements
{

    public class CRMvt
    {
      public const short EF_GOESINPLAYFIELD = 1;
      public const short EF_GOESOUTPLAYFIELD = 2;
      public const short EF_WRAP = 4;
      public int rmMvtNum;
      public CMove rmMovement;
      public byte rmWrapping;
      public bool rmMoveFlag;
      public int rmReverse;
      public bool rmBouncing;
      public short rmEventFlags;

      public void init(
        int nMove,
        CObject hoPtr,
        CObjectCommon ocPtr,
        CCreateObjectInfo cob,
        int forcedType)
      {
        if (this.rmMovement != null)
          this.rmMovement.kill();
        if (cob != null)
          hoPtr.roc.rcDir = cob.cobDir;
        this.rmWrapping = hoPtr.hoOiList.oilWrap;
        hoPtr.roc.rcMovementType = -1;
        if (ocPtr.ocMovements != null && nMove < ocPtr.ocMovements.nMovements)
        {
          CMoveDef move = ocPtr.ocMovements.moveList[nMove];
          this.rmMvtNum = nMove;
          if (forcedType == -1)
            forcedType = (int) move.mvType;
          hoPtr.roc.rcMovementType = forcedType;
          switch (forcedType)
          {
            case 0:
              this.rmMovement = (CMove) new CMoveStatic();
              break;
            case 1:
              this.rmMovement = (CMove) new CMoveMouse();
              break;
            case 2:
              this.rmMovement = (CMove) new CMoveRace();
              break;
            case 3:
              this.rmMovement = (CMove) new CMoveGeneric();
              break;
            case 4:
              this.rmMovement = (CMove) new CMoveBall();
              break;
            case 5:
              this.rmMovement = (CMove) new CMovePath();
              break;
            case 9:
              this.rmMovement = (CMove) new CMovePlatform();
              break;
            case 14:
              this.rmMovement = this.loadMvtExtension(hoPtr, (CMoveDefExtension) move);
              if (this.rmMovement == null)
              {
                this.rmMovement = (CMove) new CMoveStatic();
                break;
              }
              break;
          }
          hoPtr.roc.rcDir = this.dirAtStart(hoPtr, move.mvDirAtStart, hoPtr.roc.rcDir);
          this.rmMovement.init(hoPtr, move);
        }
        if (hoPtr.roc.rcMovementType != -1)
          return;
        hoPtr.roc.rcMovementType = 0;
        this.rmMovement = (CMove) new CMoveStatic();
        this.rmMovement.init(hoPtr, (CMoveDef) null);
        hoPtr.roc.rcDir = 0;
      }

      public CMove loadMvtExtension(CObject hoPtr, CMoveDefExtension mvDef)
      {
        CRunMvtExtension m = (CRunMvtExtension) null;
        if (m == null)
          return (CMove) null;
        m.init(hoPtr);
        return (CMove) new CMoveExtension(m);
      }

      public void initSimple(CObject hoPtr, int forcedType, bool bRestore)
      {
        if (this.rmMovement != null)
          this.rmMovement.kill();
        hoPtr.roc.rcMovementType = forcedType;
        switch (forcedType)
        {
          case 11:
            this.rmMovement = (CMove) new CMoveDisappear();
            CRun.bMoveChanged = true;
            break;
          case 13:
            this.rmMovement = (CMove) new CMoveBullet();
            break;
        }
        this.rmMovement.hoPtr = hoPtr;
        if (bRestore)
          return;
        this.rmMovement.init(hoPtr, (CMoveDef) null);
      }

      public void kill(bool bFast) => this.rmMovement.kill();

      public void move() => this.rmMovement.move();

      public void nextMovement(CObject hoPtr)
      {
        CObjectCommon hoCommon = hoPtr.hoCommon;
        if (hoCommon.ocMovements == null || this.rmMvtNum + 1 >= hoCommon.ocMovements.nMovements)
          return;
        this.kill(false);
        this.init(this.rmMvtNum + 1, hoPtr, hoCommon, (CCreateObjectInfo) null, -1);
      }

      public void previousMovement(CObject hoPtr)
      {
        CObjectCommon hoCommon = hoPtr.hoCommon;
        if (hoCommon.ocMovements == null || this.rmMvtNum - 1 < 0)
          return;
        this.kill(false);
        this.init(this.rmMvtNum - 1, hoPtr, hoCommon, (CCreateObjectInfo) null, -1);
      }

      public void selectMovement(CObject hoPtr, int mvt)
      {
        CObjectCommon hoCommon = hoPtr.hoCommon;
        if (hoCommon.ocMovements == null || mvt < 0 || mvt >= hoCommon.ocMovements.nMovements)
          return;
        this.kill(false);
        this.init(mvt, hoPtr, hoCommon, (CCreateObjectInfo) null, -1);
      }

      public int dirAtStart(CObject hoPtr, int dirAtStart, int dir)
      {
        if (dir < 0 || dir >= 32 /*0x20*/)
        {
          int wMax = 0;
          int num1 = dirAtStart;
          for (int index = 0; index < 32 /*0x20*/; ++index)
          {
            int num2 = num1;
            num1 >>= 1;
            if ((num2 & 1) != 0)
              ++wMax;
          }
          if (wMax == 0)
          {
            dir = 0;
          }
          else
          {
            int num3 = (int) hoPtr.hoAdRunHeader.random((short) wMax);
            int num4 = dirAtStart;
            dir = 0;
            while (true)
            {
              int num5 = num4;
              num4 >>= 1;
              if ((num5 & 1) != 0)
              {
                --num3;
                if (num3 < 0)
                  break;
              }
              ++dir;
            }
          }
        }
        return dir;
      }
    }
}
