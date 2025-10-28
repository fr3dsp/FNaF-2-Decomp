// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_COMPAREG
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Expressions;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_COMPAREG : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        int num = this.evtParams[0].code != (short) 52 ? (int) ((PARAM_SHORT) this.evtParams[0]).value : rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]) - 1;
        return CRun.compareTo(new CValue(rhPtr.rhApp.getGlobalValueAt(num)), rhPtr.get_EventExpressionAny((CParamExpression) this.evtParams[1]), ((CParamExpression) this.evtParams[1]).comparaison);
      }
    }
}
