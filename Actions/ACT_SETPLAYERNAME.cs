// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_SETPLAYERNAME
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_SETPLAYERNAME : CAct
    {
      public override void execute(CRun rhPtr)
      {
        int evtOi = (int) this.evtOi;
        if (evtOi >= 4)
          return;
        string expressionString = rhPtr.get_EventExpressionString((CParamExpression) this.evtParams[0]);
        rhPtr.rhApp.playerNames[evtOi] = expressionString;
      }
    }
}
