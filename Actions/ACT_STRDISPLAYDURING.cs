// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_STRDISPLAYDURING
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Events;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_STRDISPLAYDURING : CAct
    {
      public override void execute(CRun rhPtr)
      {
        PARAM_SHORT evtParam1 = (PARAM_SHORT) this.evtParams[1];
        int index = rhPtr.txtDoDisplay((CEvent) this, (int) evtParam1.value);
        if (index < 0)
          return;
        PARAM_TIME evtParam2 = (PARAM_TIME) this.evtParams[2];
        CObject rhObject = rhPtr.rhObjectList[index];
        rhObject.ros.rsFlash = evtParam2.timer;
        rhObject.ros.rsFlashCpt = evtParam2.timer;
      }
    }
}
