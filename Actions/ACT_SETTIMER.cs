// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_SETTIMER
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_SETTIMER : CAct
    {
      public override void execute(CRun rhPtr)
      {
        long num = this.evtParams[0].code != (short) 22 ? (long) ((PARAM_TIME) this.evtParams[0]).timer : (long) rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
        rhPtr.rhTimer = num;
        rhPtr.rhTimerOld = rhPtr.rhApp.timer - rhPtr.rhTimer;
        rhPtr.rhEvtProg.restartTimerEvents();
      }
    }
}
