// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_GROUPACTIVATED
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_GROUPACTIVATED : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        return ((int) rhPtr.rhEvtProg.events[(int) ((PARAM_GROUPOINTER) this.evtParams[0]).pointer].evgFlags & 16384 /*0x4000*/) != 0 ? this.negaFALSE() : this.negaTRUE();
      }
    }
}
