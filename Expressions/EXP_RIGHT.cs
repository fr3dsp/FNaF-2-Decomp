// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_RIGHT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_RIGHT : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        string expressionString = rhPtr.get_ExpressionString();
        ++rhPtr.rh4CurToken;
        int num = rhPtr.get_ExpressionInt();
        if (num < 0)
          num = 0;
        if (num > expressionString.Length)
          num = expressionString.Length;
        rhPtr.getCurrentResult().forceString(expressionString.Substring(expressionString.Length - num, expressionString.Length - (expressionString.Length - num)));
      }
    }
}
