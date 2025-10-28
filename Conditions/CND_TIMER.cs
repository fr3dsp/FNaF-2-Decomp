// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_TIMER
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_TIMER : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr)
      {
        if (((int) this.evtFlags & 2) != 0)
          return false;
        long timer = (long) ((PARAM_TIME) this.evtParams[0]).timer;
        if (rhPtr.rhTimer < timer)
          return false;
        this.evtFlags |= (byte) 2;
        return true;
      }

      public override bool eva2(CRun rhPtr) => false;
    }
}
