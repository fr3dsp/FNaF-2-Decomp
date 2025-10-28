// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.CExpExtension
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class CExpExtension : CExpOi
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
          CValue cvalue = ((CExtension) expressionObjects).expression((this.code >> 16 /*0x10*/ & (int) ushort.MaxValue) - 80 /*0x50*/);
          rhPtr.getCurrentResult().forceValue(cvalue);
        }
      }
    }
}
