// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_EVERY
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_EVERY : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        PARAM_EVERY evtParam = (PARAM_EVERY) this.evtParams[0];
        evtParam.compteur -= rhPtr.rhTimerDelta;
        if (evtParam.compteur > 0)
          return false;
        evtParam.compteur += evtParam.delay;
        return true;
      }
    }
}
