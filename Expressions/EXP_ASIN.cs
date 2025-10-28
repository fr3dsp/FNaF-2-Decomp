// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_ASIN
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;
using System;

namespace RuntimeXNA.Expressions
{

    public class EXP_ASIN : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        double num = Math.Asin(rhPtr.get_ExpressionDouble()) * (180.0 / Math.PI);
        rhPtr.getCurrentResult().forceDouble(num);
      }
    }
}
