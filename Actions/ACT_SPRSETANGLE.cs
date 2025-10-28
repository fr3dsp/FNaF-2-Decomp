// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_SPRSETANGLE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_SPRSETANGLE : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        int eventExpressionInt = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
        bool flag1 = false;
        if (rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[1]) != 0)
          flag1 = true;
        int num = eventExpressionInt % 360;
        if (num < 0)
          num += 360;
        bool flag2 = false;
        if (((int) actionObjects.ros.rsFlags & 16 /*0x10*/) != 0)
          flag2 = true;
        if (actionObjects.roc.rcAngle == num && flag2 == flag1)
          return;
        actionObjects.roc.rcAngle = num;
        actionObjects.ros.rsFlags &= (short) -17;
        if (flag1)
          actionObjects.ros.rsFlags |= (short) 16 /*0x10*/;
        actionObjects.roc.rcChanged = true;
        CImage imageInfoEx = actionObjects.hoAdRunHeader.rhApp.imageBank.getImageInfoEx(actionObjects.roc.rcImage, actionObjects.roc.rcAngle, actionObjects.roc.rcScaleX, actionObjects.roc.rcScaleY);
        actionObjects.hoImgWidth = (int) imageInfoEx.width;
        actionObjects.hoImgHeight = (int) imageInfoEx.height;
        actionObjects.hoImgXSpot = (int) imageInfoEx.xSpot;
        actionObjects.hoImgYSpot = (int) imageInfoEx.ySpot;
      }
    }
}
