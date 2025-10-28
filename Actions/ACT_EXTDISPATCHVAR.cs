// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_EXTDISPATCHVAR
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_EXTDISPATCHVAR : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        int n = this.evtParams[0].code != (short) 53 ? (int) ((PARAM_SHORT) this.evtParams[0]).value : rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
        PARAM_INT evtParam = (PARAM_INT) this.evtParams[2];
        if (rhPtr.rhEvtProg.rh2ActionLoopCount == 0)
          evtParam.value_Renamed = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[1]);
        else
          ++evtParam.value_Renamed;
        if (actionObjects.rov == null)
          return;
        actionObjects.rov.getValue(n).forceInt(evtParam.value_Renamed);
      }
    }
}
