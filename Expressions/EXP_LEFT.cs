// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_LEFT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_LEFT : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        string expressionString = rhPtr.get_ExpressionString();
        ++rhPtr.rh4CurToken;
        int length = rhPtr.get_ExpressionInt();
        if (length < 0)
          length = 0;
        if (length > expressionString.Length)
          length = expressionString.Length;
        rhPtr.getCurrentResult().forceString(expressionString.Substring(0, length));
      }
    }
}
