// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_EXTRGBCOEF
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Expressions
{

    internal class EXP_EXTRGBCOEF : CExpOi
    {
      public override void evaluate(CRun rhPtr)
      {
        CObject expressionObjects = rhPtr.rhEvtProg.get_ExpressionObjects(this.oiList);
        if (expressionObjects == null || expressionObjects.ros == null)
        {
          rhPtr.getCurrentResult().forceInt(0);
        }
        else
        {
          int rsEffect = expressionObjects.ros.rsEffect;
          int rsEffectParam = expressionObjects.ros.rsEffectParam;
          int num = (rsEffect & 4095 /*0x0FFF*/) == 13 || (rsEffect & 4096 /*0x1000*/) != 0 ? CServices.swapRGB(rsEffectParam & 16777215 /*0xFFFFFF*/) : 16777215 /*0xFFFFFF*/;
          rhPtr.getCurrentResult().forceInt(num);
        }
      }
    }
}
