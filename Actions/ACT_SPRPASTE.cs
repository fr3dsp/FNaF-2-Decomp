// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_SPRPASTE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Actions
{

    public class ACT_SPRPASTE : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        if (actionObjects.roa != null)
          actionObjects.roa.animIn(0);
        if (actionObjects.hoLayer == 0 && actionObjects.roc.rcSprite != null)
          rhPtr.rhApp.spriteGen.activeSprite(actionObjects.roc.rcSprite, 1, (CRect) null);
        rhPtr.activeToBackdrop(actionObjects, (int) ((PARAM_SHORT) this.evtParams[0]).value, false);
      }
    }
}
