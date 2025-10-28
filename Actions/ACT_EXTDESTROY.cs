// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_EXTDESTROY
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_EXTDESTROY : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        if (actionObjects.hoType == (short) 3)
        {
          if (((int) ((CText) actionObjects).rsHidden & 4) != 0)
          {
            actionObjects.ros.obHide();
            actionObjects.ros.rsFlags &= (short) -33;
            actionObjects.hoFlags |= (short) 8192 /*0x2000*/;
          }
          else
          {
            actionObjects.hoFlags |= (short) 1;
            rhPtr.destroy_Add((int) actionObjects.hoNumber);
          }
        }
        else
        {
          if (((int) actionObjects.hoFlags & 1) != 0)
            return;
          actionObjects.hoFlags |= (short) 1;
          if ((actionObjects.hoOEFlags & 32 /*0x20*/) != 0 || (actionObjects.hoOEFlags & 512 /*0x0200*/) != 0)
          {
            rhPtr.init_Disappear(actionObjects);
          }
          else
          {
            actionObjects.hoCallRoutine = false;
            rhPtr.destroy_Add((int) actionObjects.hoNumber);
          }
        }
      }
    }
}
