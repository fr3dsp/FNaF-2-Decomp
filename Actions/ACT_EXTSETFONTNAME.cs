// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_EXTSETFONTNAME
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Actions
{

    public class ACT_EXTSETFONTNAME : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        string expressionString = rhPtr.get_EventExpressionString((CParamExpression) this.evtParams[0]);
        CFontInfo objectFont = CRun.getObjectFont(actionObjects);
        objectFont.lfFaceName = expressionString;
        CRun.setObjectFont(actionObjects, objectFont, (CRect) null);
      }
    }
}
