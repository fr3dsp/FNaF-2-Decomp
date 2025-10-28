// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_EXTFORCEDIR
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_EXTFORCEDIR : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        int dir = this.evtParams[0].code != (short) 29 ? rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]) : rhPtr.get_Direction(((PARAM_INT) this.evtParams[0]).value_Renamed);
        actionObjects.roa.animDir_Force(dir);
        actionObjects.roc.rcChanged = true;
      }
    }
}
