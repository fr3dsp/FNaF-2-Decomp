// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_GETRGBAT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Expressions
{

    public class EXP_GETRGBAT : CExpOi
    {
      public override void evaluate(CRun rhPtr)
      {
        CObject expressionObjects = rhPtr.rhEvtProg.get_ExpressionObjects(this.oiList);
        ++rhPtr.rh4CurToken;
        if (expressionObjects == null)
        {
          rhPtr.getCurrentResult().forceInt(0);
        }
        else
        {
          int expressionInt1 = rhPtr.get_ExpressionInt();
          ++rhPtr.rh4CurToken;
          int expressionInt2 = rhPtr.get_ExpressionInt();
          int num = 0;
          if (expressionObjects.roc.rcImage != (short) -1)
          {
            CImage imageFromHandle = rhPtr.rhApp.imageBank.getImageFromHandle(expressionObjects.roc.rcImage);
            if (expressionInt1 > 0 && expressionInt1 < (int) imageFromHandle.width && expressionInt2 > 0 && expressionInt2 < (int) imageFromHandle.height)
            {
              int[] data = new int[(int) imageFromHandle.width * (int) imageFromHandle.height];
              imageFromHandle.image.GetData<int>(data);
              num = CServices.swapRGB(data[expressionInt2 * (int) imageFromHandle.width + expressionInt1] & 16777215 /*0xFFFFFF*/);
            }
          }
          rhPtr.getCurrentResult().forceInt(num);
        }
      }
    }
}
