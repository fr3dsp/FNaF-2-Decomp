// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_EXTCOLLISION
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Events;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_EXTCOLLISION : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject pHo)
      {
        CObject rhObject = rhPtr.rhObjectList[(int) rhPtr.rhEvtProg.rh1stObjectNumber];
        short evtOi = this.evtOi;
        PARAM_OBJECT evtParam = (PARAM_OBJECT) this.evtParams[0];
        short oi = evtParam.oi;
        if ((int) evtOi == (int) pHo.hoOi)
        {
          if ((int) oi != (int) rhObject.hoOi && (oi >= (short) 0 || !this.colGetList(rhPtr, evtParam.oiList, rhObject.hoOi)))
            return false;
        }
        else if ((int) oi == (int) pHo.hoOi)
        {
          if ((int) evtOi != (int) rhObject.hoOi && (evtOi >= (short) 0 || !this.colGetList(rhPtr, this.evtOiList, rhObject.hoOi)))
            return false;
        }
        else if (evtOi < (short) 0)
        {
          if (oi < (short) 0)
          {
            if (this.colGetList(rhPtr, this.evtOiList, pHo.hoOi))
            {
              if (!this.colGetList(rhPtr, evtParam.oiList, rhObject.hoOi) && (!this.colGetList(rhPtr, evtParam.oiList, pHo.hoOi) || !this.colGetList(rhPtr, this.evtOiList, rhObject.hoOi)))
                return false;
            }
            else if (!this.colGetList(rhPtr, this.evtOiList, rhObject.hoOi))
              return false;
          }
          else if ((int) oi != (int) rhObject.hoOi)
            return false;
        }
        else if (oi >= (short) 0 || (int) evtOi != (int) rhObject.hoOi)
          return false;
        if (!this.compute_NoRepeatCol((int) rhObject.hoCreationId << 16 /*0x10*/ | (int) this.evtIdentifier & (int) ushort.MaxValue, pHo))
        {
          if (((int) rhPtr.rhEvtProg.rhEventGroup.evgFlags & 2048 /*0x0800*/) == 0)
            return false;
          rhPtr.rhEvtProg.rh3DoStop = true;
        }
        if (!this.compute_NoRepeatCol((int) pHo.hoCreationId << 16 /*0x10*/ | (int) this.evtIdentifier & (int) ushort.MaxValue, rhObject))
        {
          if (((int) rhPtr.rhEvtProg.rhEventGroup.evgFlags & 2048 /*0x0800*/) == 0)
            return false;
          rhPtr.rhEvtProg.rh3DoStop = true;
        }
        rhPtr.rhEvtProg.evt_AddCurrentObject(pHo);
        rhPtr.rhEvtProg.evt_AddCurrentObject(rhObject);
        if ((int) rhObject.rom.rmMovement.rmCollisionCount == (int) rhPtr.rh3CollisionCount)
          pHo.rom.rmMovement.rmCollisionCount = rhPtr.rh3CollisionCount;
        else if ((int) pHo.rom.rmMovement.rmCollisionCount == (int) rhPtr.rh3CollisionCount)
          rhObject.rom.rmMovement.rmCollisionCount = rhPtr.rh3CollisionCount;
        return true;
      }

      public override bool eva2(CRun rhPtr) => this.isColliding(rhPtr);

      internal virtual bool colGetList(CRun rhPtr, short oiList, short lookFor)
      {
        if (oiList == (short) -1)
          return false;
        CQualToOiList qualToOi = rhPtr.rhEvtProg.qualToOiList[(int) oiList & (int) short.MaxValue];
        for (int index = 0; index < qualToOi.qoiList.Length; index += 2)
        {
          if ((int) qualToOi.qoiList[index] == (int) lookFor)
            return true;
        }
        return false;
      }
    }
}
