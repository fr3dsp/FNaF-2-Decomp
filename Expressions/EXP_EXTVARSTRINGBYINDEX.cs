// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_EXTVARSTRINGBYINDEX
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_EXTVARSTRINGBYINDEX : CExpOi
    {
      public override void evaluate(CRun rhPtr)
      {
        CObject expressionObjects = rhPtr.rhEvtProg.get_ExpressionObjects(this.oiList);
        ++rhPtr.rh4CurToken;
        int expressionInt = rhPtr.get_ExpressionInt();
        if (expressionObjects == null || expressionInt < 0 || expressionInt >= 10)
          rhPtr.getCurrentResult().forceString("");
        else
          rhPtr.getCurrentResult().forceString(expressionObjects.rov.getString(expressionInt));
      }
    }
}
