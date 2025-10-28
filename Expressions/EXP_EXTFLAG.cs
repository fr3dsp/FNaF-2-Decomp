// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_EXTFLAG
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_EXTFLAG : CExpOi
    {
      public override void evaluate(CRun rhPtr)
      {
        CObject expressionObjects = rhPtr.rhEvtProg.get_ExpressionObjects(this.oiList);
        ++rhPtr.rh4CurToken;
        int expressionInt = rhPtr.get_ExpressionInt();
        if (expressionObjects == null)
        {
          rhPtr.getCurrentResult().forceInt(0);
        }
        else
        {
          int num1 = expressionInt & 31 /*0x1F*/;
          if (expressionObjects.rov != null)
          {
            int num2 = 0;
            if ((1 << num1 & expressionObjects.rov.rvValueFlags) != 0)
              num2 = 1;
            rhPtr.getCurrentResult().forceInt(num2);
          }
          else
            rhPtr.getCurrentResult().forceInt(0);
        }
      }
    }
}
