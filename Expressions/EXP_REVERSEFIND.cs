// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_REVERSEFIND
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_REVERSEFIND : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        string expressionString1 = rhPtr.get_ExpressionString();
        ++rhPtr.rh4CurToken;
        string expressionString2 = rhPtr.get_ExpressionString();
        ++rhPtr.rh4CurToken;
        int num1 = rhPtr.get_ExpressionInt();
        if (num1 > expressionString1.Length)
          num1 = expressionString1.Length;
        int num2 = -1;
        int num3;
        do
        {
          num3 = num2;
          int num4 = expressionString1.IndexOf(expressionString2, num2 + 1);
          if (num4 != -1)
            num2 = num4;
          else
            break;
        }
        while (num2 <= num1);
        rhPtr.getCurrentResult().forceInt(num3);
      }
    }
}
