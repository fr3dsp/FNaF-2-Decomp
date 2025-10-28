// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_EXTSHOOTTOWARD
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_EXTSHOOTTOWARD : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        PARAM_SHOOT evtParam = (PARAM_SHOOT) this.evtParams[0];
        CPositionInfo pInfo1 = new CPositionInfo();
        if (!evtParam.read_Position(rhPtr, 17, pInfo1))
          return;
        CPositionInfo pInfo2 = new CPositionInfo();
        if (!((CPosition) this.evtParams[1]).read_Position(rhPtr, 0, pInfo2))
          return;
        int x = pInfo2.x;
        int y = pInfo2.y;
        int dirFromPente = CRun.get_DirFromPente(x - pInfo1.x, y - pInfo1.y);
        actionObjects.shtCreate(evtParam, pInfo1.x, pInfo1.y, dirFromPente);
      }
    }
}
