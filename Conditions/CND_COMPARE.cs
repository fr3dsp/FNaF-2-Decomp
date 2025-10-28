// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_COMPARE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Expressions;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_COMPARE : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        CValue pValue1 = new CValue();
        pValue1.forceValue(rhPtr.get_EventExpressionAny((CParamExpression) this.evtParams[0]));
        CParamExpression evtParam = (CParamExpression) this.evtParams[1];
        CValue eventExpressionAny = rhPtr.get_EventExpressionAny(evtParam);
        return CRun.compareTo(pValue1, eventExpressionAny, evtParam.comparaison);
      }
    }
}
