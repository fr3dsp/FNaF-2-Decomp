// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_MKEYDEPRESSED
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_MKEYDEPRESSED : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        switch (((PARAM_KEY) this.evtParams[0]).mouseKey)
        {
          case 1:
            if (rhPtr.mouseKey == 0)
              return this.negaTRUE();
            break;
          case 2:
            if (rhPtr.mouseKey == 2)
              return this.negaTRUE();
            break;
          case 3:
            if (rhPtr.mouseKey == 1)
              return this.negaTRUE();
            break;
        }
        return this.negaFALSE();
      }
    }
}
