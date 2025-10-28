// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_FLOATTOSTRING
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;
using System.Text;

namespace RuntimeXNA.Expressions
{

    public class EXP_FLOATTOSTRING : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        double expressionDouble = rhPtr.get_ExpressionDouble();
        ++rhPtr.rh4CurToken;
        if (rhPtr.get_ExpressionInt() < 1)
          ;
        ++rhPtr.rh4CurToken;
        int expressionInt = rhPtr.get_ExpressionInt();
        string str = expressionDouble.ToString();
        StringBuilder stringBuilder = new StringBuilder();
        int num1 = str.IndexOf('.');
        if (num1 >= 0)
        {
          int index = num1 + 1;
          while (index < str.Length && str[index] == '0')
            ++index;
          if (index == str.Length)
            num1 = -1;
        }
        int index1 = 0;
        if (num1 >= 0)
        {
          if (expressionDouble < 0.0)
          {
            stringBuilder.Append("-");
            ++index1;
          }
          for (; index1 < num1; ++index1)
            stringBuilder.Append(str[index1]);
          if (expressionInt > 0)
          {
            stringBuilder.Append(".");
            int num2 = index1 + 1;
            for (int index2 = 0; index2 < expressionInt && index2 + num2 < str.Length; ++index2)
              stringBuilder.Append(str[num2 + index2]);
          }
          else if (expressionInt < 0)
          {
            stringBuilder.Append(".");
            for (int index3 = index1 + 1; index3 < str.Length; ++index3)
              stringBuilder.Append(str[index3]);
          }
        }
        else
        {
          for (; index1 < str.Length && str[index1] != '.'; ++index1)
            stringBuilder.Append(str[index1]);
          if (expressionInt > 0)
          {
            stringBuilder.Append(".");
            for (int index4 = 0; index4 < expressionInt; ++index4)
              stringBuilder.Append("0");
          }
        }
        rhPtr.getCurrentResult().forceString(new string(stringBuilder.ToString().ToCharArray()));
      }
    }
}
