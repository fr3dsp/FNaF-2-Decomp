// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_REPEAT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Events;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_REPEAT : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        CEventGroup rhEventGroup = rhPtr.rhEvtProg.rhEventGroup;
        if (((int) rhEventGroup.evgFlags & 4) != 0)
          return true;
        if (((int) rhEventGroup.evgFlags & 8) != 0)
          return false;
        rhEventGroup.evgInhibitCpt = (short) rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
        rhEventGroup.evgFlags |= (ushort) 4;
        return true;
      }
    }
}
