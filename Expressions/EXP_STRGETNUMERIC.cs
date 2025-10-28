// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_STRGETNUMERIC
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Expressions
{

    public class EXP_STRGETNUMERIC : CExpOi
    {
      public override void evaluate(CRun rhPtr)
      {
        CObject expressionObjects = rhPtr.rhEvtProg.get_ExpressionObjects(this.oiList);
        if (expressionObjects == null)
        {
          rhPtr.getCurrentResult().forceInt(0);
        }
        else
        {
          CText ctext = (CText) expressionObjects;
          if (ctext.rsTextBuffer != null)
          {
            CFuncVal cfuncVal = new CFuncVal();
            switch (cfuncVal.parse(ctext.rsTextBuffer))
            {
              case 0:
                rhPtr.getCurrentResult().forceInt(cfuncVal.intValue);
                return;
              case 1:
                rhPtr.getCurrentResult().forceDouble(cfuncVal.doubleValue);
                return;
            }
          }
          rhPtr.getCurrentResult().forceInt(0);
        }
      }
    }
}
