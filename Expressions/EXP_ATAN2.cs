// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_ATAN2
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;
using System;

namespace RuntimeXNA.Expressions
{

    public class EXP_ATAN2 : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        double expressionDouble1 = rhPtr.get_ExpressionDouble();
        ++rhPtr.rh4CurToken;
        double expressionDouble2 = rhPtr.get_ExpressionDouble();
        rhPtr.getCurrentResult().forceDouble(Math.Atan2(expressionDouble1, expressionDouble2) * 180.0 / Math.PI);
      }
    }
}
