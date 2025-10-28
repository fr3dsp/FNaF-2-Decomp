// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_EXTSETFONTSIZE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Actions
{

    public class ACT_EXTSETFONTSIZE : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        int eventExpressionInt1 = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
        int eventExpressionInt2 = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[1]);
        CFontInfo objectFont = CRun.getObjectFont(actionObjects);
        int lfHeight = objectFont.lfHeight;
        objectFont.lfHeight = eventExpressionInt1;
        if (eventExpressionInt2 == 0)
        {
          CRun.setObjectFont(actionObjects, objectFont, (CRect) null);
        }
        else
        {
          CRect pNewSize = new CRect();
          float num = 1f;
          if (lfHeight != 0)
            num = (float) eventExpressionInt1 / (float) lfHeight;
          pNewSize.right = (int) ((double) actionObjects.hoImgWidth * (double) num);
          pNewSize.bottom = (int) ((double) actionObjects.hoImgHeight * (double) num);
          pNewSize.left = 0;
          pNewSize.top = 0;
          CRun.setObjectFont(actionObjects, objectFont, pNewSize);
        }
      }
    }
}
