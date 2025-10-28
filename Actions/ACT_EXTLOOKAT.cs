// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_EXTLOOKAT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_EXTLOOKAT : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        CPosition evtParam = (CPosition) this.evtParams[0];
        CPositionInfo pInfo = new CPositionInfo();
        if (!evtParam.read_Position(rhPtr, 0, pInfo))
          return;
        int x = pInfo.x;
        int y = pInfo.y;
        int dir = CRun.get_DirFromPente(x - actionObjects.hoX, y - actionObjects.hoY) & 31 /*0x1F*/;
        if (actionObjects.roc.rcDir == dir)
          return;
        actionObjects.roc.rcDir = dir;
        actionObjects.roc.rcChanged = true;
        actionObjects.rom.rmMovement.setDir(dir);
      }
    }
}
