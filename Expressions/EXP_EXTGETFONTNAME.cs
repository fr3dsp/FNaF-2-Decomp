// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_EXTGETFONTNAME
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Expressions
{

    public class EXP_EXTGETFONTNAME : CExpOi
    {
      public override void evaluate(CRun rhPtr)
      {
        CObject expressionObjects = rhPtr.rhEvtProg.get_ExpressionObjects(this.oiList);
        if (expressionObjects == null)
        {
          rhPtr.getCurrentResult().forceString("");
        }
        else
        {
          CFontInfo objectFont = CRun.getObjectFont(expressionObjects);
          rhPtr.getCurrentResult().forceString(objectFont.lfFaceName);
        }
      }
    }
}
