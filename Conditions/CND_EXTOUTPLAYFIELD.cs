// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_EXTOUTPLAYFIELD
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_EXTOUTPLAYFIELD : CCnd, IEvaObject
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr)
      {
        if (((int) ((PARAM_SHORT) this.evtParams[0]).value & (int) (short) rhPtr.rhEvtProg.rhCurParam0) == 0)
          return false;
        if (this.compute_NoRepeat(hoPtr))
        {
          rhPtr.rhEvtProg.evt_AddCurrentObject(hoPtr);
          return true;
        }
        if (((int) rhPtr.rhEvtProg.rhEventGroup.evgFlags & 2048 /*0x0800*/) == 0)
          return false;
        rhPtr.rhEvtProg.rh3DoStop = true;
        return true;
      }

      public override bool eva2(CRun rhPtr) => this.evaObject(rhPtr, (IEvaObject) this);

      public virtual bool evaObjectRoutine(CObject pHo)
      {
        return ((int) pHo.rom.rmEventFlags & 2) != 0 ? this.negaTRUE() : this.negaFALSE();
      }
    }
}
