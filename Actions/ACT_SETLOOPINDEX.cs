// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_SETLOOPINDEX
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using System;

namespace RuntimeXNA.Actions
{

    public class ACT_SETLOOPINDEX : CAct
    {
      public override void execute(CRun rhPtr)
      {
        string expressionString = rhPtr.get_EventExpressionString((CParamExpression) this.evtParams[0]);
        if (expressionString.Length == 0)
          return;
        int eventExpressionInt = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[1]);
        for (int index = 0; index < rhPtr.rh4FastLoops.size(); ++index)
        {
          CLoop cloop = (CLoop) rhPtr.rh4FastLoops.get(index);
          if (string.Compare(cloop.name, expressionString, StringComparison.OrdinalIgnoreCase) == 0)
          {
            cloop.index = eventExpressionInt;
            break;
          }
        }
      }
    }
}
