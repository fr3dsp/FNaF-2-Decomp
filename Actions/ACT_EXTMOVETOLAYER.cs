// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_EXTMOVETOLAYER
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Frame;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;

namespace RuntimeXNA.Actions
{

    public class ACT_EXTMOVETOLAYER : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null || actionObjects.ros == null)
          return;
        int eventExpressionInt = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
        if (eventExpressionInt <= 0 || eventExpressionInt > rhPtr.rhFrame.nLayers)
          return;
        int nLayer = eventExpressionInt - 1;
        actionObjects.hoLayer = (int) (short) nLayer;
        if (actionObjects.ros == null)
          return;
        CSprite rcSprite = actionObjects.roc.rcSprite;
        if (rcSprite == null)
          return;
        rhPtr.rhApp.spriteGen.setSpriteLayer(rcSprite, nLayer);
        CLayer layer = rhPtr.rhFrame.layers[nLayer];
        ++layer.nZOrderMax;
        rcSprite.sprZOrder = layer.nZOrderMax;
        actionObjects.ros.rsZOrder = rcSprite.sprZOrder;
        if ((layer.dwOptions & 131088 /*0x020010*/) != 16 /*0x10*/)
        {
          rhPtr.rhApp.spriteGen.activeSprite(rcSprite, 1, (CRect) null);
          actionObjects.ros.obHide();
        }
        else
        {
          if (((int) actionObjects.ros.rsFlags & 32 /*0x20*/) == 0 || ((int) actionObjects.ros.rsFlags & 1) == 0 || (layer.dwOptions & 131088 /*0x020010*/) != 16 /*0x10*/)
            return;
          actionObjects.ros.obShow();
        }
      }
    }
}
