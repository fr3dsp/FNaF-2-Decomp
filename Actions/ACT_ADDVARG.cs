// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_ADDVARG
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Expressions;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_ADDVARG : CAct
    {
      public override void execute(CRun rhPtr)
      {
        int num = this.evtParams[0].code != (short) 52 ? (int) ((PARAM_SHORT) this.evtParams[0]).value : rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]) - 1;
        CValue eventExpressionAny = rhPtr.get_EventExpressionAny((CParamExpression) this.evtParams[1]);
        rhPtr.rhApp.getGlobalValueAt(num).add(eventExpressionAny);
      }
    }
}
