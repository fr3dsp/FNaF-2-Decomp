// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_CHOOSEZONE_OLD
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_CHOOSEZONE_OLD : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        PARAM_ZONE evtParam = (PARAM_ZONE) this.evtParams[0];
        rhPtr.rhEvtProg.count_ZoneTypeObjects(evtParam, -1, (short) 2);
        if (rhPtr.rhEvtProg.evtNSelectedObjects == 0)
          return false;
        int stop = (int) rhPtr.random((short) rhPtr.rhEvtProg.evtNSelectedObjects);
        CObject pHo = rhPtr.rhEvtProg.count_ZoneTypeObjects(evtParam, stop, (short) 2);
        rhPtr.rhEvtProg.evt_DeleteCurrent();
        rhPtr.rhEvtProg.evt_AddCurrentObject(pHo);
        return true;
      }
    }
}
