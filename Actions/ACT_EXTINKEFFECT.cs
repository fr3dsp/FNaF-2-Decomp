// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_EXTINKEFFECT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_EXTINKEFFECT : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null || actionObjects.ros == null)
          return;
        PARAM_2SHORTS evtParam = (PARAM_2SHORTS) this.evtParams[0];
        int effect = (int) evtParam.value1;
        int effectParam = (int) evtParam.value2;
        if (effect != 1)
          effectParam = 0;
        actionObjects.ros.modifSpriteEffect(effect, effectParam);
      }
    }
}
