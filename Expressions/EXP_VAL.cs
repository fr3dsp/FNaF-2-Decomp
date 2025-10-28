// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_VAL
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Expressions
{

    public class EXP_VAL : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        string expressionString = rhPtr.get_ExpressionString();
        CFuncVal cfuncVal = new CFuncVal();
        switch (cfuncVal.parse(expressionString))
        {
          case 0:
            rhPtr.getCurrentResult().forceInt(cfuncVal.intValue);
            break;
          case 1:
            rhPtr.getCurrentResult().forceDouble(cfuncVal.doubleValue);
            break;
        }
      }
    }
}
