// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_EXTXAP
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_EXTXAP : CExpOi
    {
      public override void evaluate(CRun rhPtr)
      {
        int num = 0;
        CObject expressionObjects = rhPtr.rhEvtProg.get_ExpressionObjects(this.oiList);
        if (expressionObjects != null)
        {
          num = expressionObjects.hoX;
          if (expressionObjects.roa != null)
          {
            CImage imageInfoEx = rhPtr.rhApp.imageBank.getImageInfoEx(expressionObjects.roc.rcImage, expressionObjects.roc.rcAngle, expressionObjects.roc.rcScaleX, expressionObjects.roc.rcScaleY);
            if (imageInfoEx != null)
              num += (int) imageInfoEx.xAP - (int) imageInfoEx.xSpot;
          }
        }
        rhPtr.getCurrentResult().forceInt(num);
      }
    }
}
