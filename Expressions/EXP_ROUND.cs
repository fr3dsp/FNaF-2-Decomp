// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_ROUND
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;
using System;

namespace RuntimeXNA.Expressions
{

    public class EXP_ROUND : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        double expressionDouble = rhPtr.get_ExpressionDouble();
        double num = Math.Floor(expressionDouble);
        if (expressionDouble - num > 0.5)
          ++num;
        rhPtr.getCurrentResult().forceInt((int) num);
      }
    }
}
