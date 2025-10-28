// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_LOOPINDEX
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Actions;
using RuntimeXNA.RunLoop;
using System;

namespace RuntimeXNA.Expressions
{

    public class EXP_LOOPINDEX : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        string expressionString = rhPtr.get_ExpressionString();
        for (int index = 0; index < rhPtr.rh4FastLoops.size(); ++index)
        {
          CLoop cloop = (CLoop) rhPtr.rh4FastLoops.get(index);
          if (string.Compare(cloop.name, expressionString, StringComparison.OrdinalIgnoreCase) == 0)
          {
            rhPtr.getCurrentResult().forceInt(cloop.index);
            return;
          }
        }
        rhPtr.getCurrentResult().forceInt(0);
      }
    }
}
