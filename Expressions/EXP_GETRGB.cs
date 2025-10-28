// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_GETRGB
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_GETRGB : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        ++rhPtr.rh4CurToken;
        int expressionInt1 = rhPtr.get_ExpressionInt();
        ++rhPtr.rh4CurToken;
        int expressionInt2 = rhPtr.get_ExpressionInt();
        ++rhPtr.rh4CurToken;
        int num = ((rhPtr.get_ExpressionInt() & (int) byte.MaxValue) << 16 /*0x10*/) + ((expressionInt2 & (int) byte.MaxValue) << 8) + (expressionInt1 & (int) byte.MaxValue);
        rhPtr.getCurrentResult().forceInt(num);
      }
    }
}
