// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_PLAYLOOPCHANNEL
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_PLAYLOOPCHANNEL : CAct
    {
      public override void execute(CRun rhPtr)
      {
        PARAM_SAMPLE evtParam = (PARAM_SAMPLE) this.evtParams[0];
        bool bPrio = evtParam.sndFlags != (short) 0;
        int eventExpressionInt1 = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[1]);
        int eventExpressionInt2 = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[2]);
        rhPtr.rhApp.soundPlayer.play(evtParam.sndHandle, eventExpressionInt2, eventExpressionInt1 - 1, bPrio);
      }
    }
}
