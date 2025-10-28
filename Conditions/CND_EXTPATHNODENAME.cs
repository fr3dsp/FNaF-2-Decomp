// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_EXTPATHNODENAME
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_EXTPATHNODENAME : CCnd, IEvaObject
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr)
      {
        string expressionString = rhPtr.get_EventExpressionString((CParamExpression) this.evtParams[0]);
        return hoPtr.hoMT_NodeName != null && string.CompareOrdinal(hoPtr.hoMT_NodeName, expressionString) == 0;
      }

      public override bool eva2(CRun rhPtr) => this.evaObject(rhPtr, (IEvaObject) this);

      public virtual bool evaObjectRoutine(CObject hoPtr)
      {
        if (hoPtr.roc.rcMovementType != 5 || !this.checkMark(hoPtr.hoAdRunHeader, hoPtr.hoMark1))
          return false;
        string expressionString = hoPtr.hoAdRunHeader.get_EventExpressionString((CParamExpression) this.evtParams[0]);
        return hoPtr.hoMT_NodeName != null && string.CompareOrdinal(hoPtr.hoMT_NodeName, expressionString) == 0;
      }
    }
}
