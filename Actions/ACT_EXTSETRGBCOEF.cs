// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_EXTSETRGBCOEF
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Actions
{

    internal class ACT_EXTSETRGBCOEF : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null || actionObjects.ros == null)
          return;
        uint eventExpressionInt = (uint) rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
        bool flag = (actionObjects.ros.rsEffect & 4096 /*0x1000*/) == 0;
        actionObjects.ros.rsEffect = actionObjects.ros.rsEffect & 4095 /*0x0FFF*/ | 4096 /*0x1000*/;
        uint rsEffectParam = (uint) actionObjects.ros.rsEffectParam;
        uint num = (!flag ? rsEffectParam & 4278190080U /*0xFF000000*/ : (actionObjects.ros.rsEffectParam != -1 ? (uint) ((int) byte.MaxValue - actionObjects.ros.rsEffectParam * 2 << 24) : 4278190080U /*0xFF000000*/)) | (uint) CServices.swapRGB((int) eventExpressionInt & 16777215 /*0xFFFFFF*/);
        actionObjects.ros.rsEffectParam = (int) num;
        actionObjects.roc.rcChanged = true;
        if (actionObjects.roc.rcSprite == null)
          return;
        actionObjects.hoAdRunHeader.rhApp.spriteGen.modifSpriteEffect(actionObjects.roc.rcSprite, actionObjects.ros.rsEffect, actionObjects.ros.rsEffectParam);
      }
    }
}
