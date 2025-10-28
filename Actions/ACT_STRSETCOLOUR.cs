// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_STRSETCOLOUR
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Actions
{

    public class ACT_STRSETCOLOUR : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        int num = this.evtParams[0].code != (short) 24 ? CServices.swapRGB(rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0])) : ((PARAM_COLOUR) this.evtParams[0]).color;
        ((CText) actionObjects).rsTextColor = num;
        actionObjects.roc.rcChanged = true;
        actionObjects.display();
      }
    }
}
