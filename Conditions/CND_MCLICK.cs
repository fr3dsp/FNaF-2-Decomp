// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_MCLICK
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_MCLICK : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr)
      {
        return (int) ((PARAM_SHORT) this.evtParams[0]).value == (int) (short) rhPtr.rhEvtProg.rhCurParam0;
      }

      public override bool eva2(CRun rhPtr)
      {
        return (int) ((PARAM_SHORT) this.evtParams[0]).value == (int) rhPtr.rhEvtProg.rh2CurrentClick;
      }
    }
}
