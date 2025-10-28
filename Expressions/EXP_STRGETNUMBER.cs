// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_STRGETNUMBER
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_STRGETNUMBER : CExpOi
    {
      public override void evaluate(CRun rhPtr)
      {
        CObject expressionObjects = rhPtr.rhEvtProg.get_ExpressionObjects(this.oiList);
        ++rhPtr.rh4CurToken;
        if (expressionObjects == null)
        {
          rhPtr.getCurrentResult().forceString("");
        }
        else
        {
          int index = rhPtr.get_ExpressionInt();
          CText ctext = (CText) expressionObjects;
          if (index < 0)
          {
            if (ctext.rsTextBuffer != null)
              rhPtr.getCurrentResult().forceString(ctext.rsTextBuffer);
            else
              rhPtr.getCurrentResult().forceString("");
          }
          else
          {
            if (index >= ctext.rsMaxi)
              index = ctext.rsMaxi - 1;
            CDefTexts ocObject = (CDefTexts) expressionObjects.hoCommon.ocObject;
            rhPtr.getCurrentResult().forceString(ocObject.otTexts[index].tsText);
          }
        }
      }
    }
}
