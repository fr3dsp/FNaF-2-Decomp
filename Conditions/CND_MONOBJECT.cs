// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_MONOBJECT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_MONOBJECT : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        bool nega = ((int) this.evtFlags2 & 1) != 0;
        PARAM_OBJECT evtParam = (PARAM_OBJECT) this.evtParams[0];
        return rhPtr.getMouseOnObjectsEDX(evtParam.oiList, nega);
      }
    }
}
