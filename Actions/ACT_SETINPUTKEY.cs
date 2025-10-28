// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_SETINPUTKEY
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_SETINPUTKEY : CAct
    {
      public override void execute(CRun rhPtr)
      {
        int eventExpressionInt = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
        if (eventExpressionInt >= 8)
          return;
        int evtOi = (int) this.evtOi;
        if (evtOi >= 4)
          return;
        rhPtr.rhApp.pcCtrlKeys[evtOi * 4 + eventExpressionInt] = ((PARAM_KEY) this.evtParams[1]).key;
      }
    }
}
