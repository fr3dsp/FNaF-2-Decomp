// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_MID
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_MID : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        string expressionString = rhPtr.get_ExpressionString();
        ++rhPtr.rh4CurToken;
        int startIndex = rhPtr.get_ExpressionInt();
        ++rhPtr.rh4CurToken;
        int num = rhPtr.get_ExpressionInt();
        if (startIndex < 0)
          startIndex = 0;
        if (startIndex > expressionString.Length)
          startIndex = expressionString.Length;
        if (num < 0)
          num = 0;
        if (startIndex + num > expressionString.Length)
          num = expressionString.Length - startIndex;
        rhPtr.getCurrentResult().forceString(expressionString.Substring(startIndex, startIndex + num - startIndex));
      }
    }
}
