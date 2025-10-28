// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_CDISPLAY
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_CDISPLAY : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CPosition evtParam = (CPosition) this.evtParams[0];
        CPositionInfo pInfo = new CPositionInfo();
        evtParam.read_Position(rhPtr, 0, pInfo);
        rhPtr.setDisplay(pInfo.x, pInfo.y, pInfo.layer, 3);
      }
    }
}
