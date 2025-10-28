// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_EXTANIMENDOF
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_EXTANIMENDOF : CCnd, IEvaExpObject, IEvaObject
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr)
      {
        if ((this.evtParams[0].code != (short) 10 ? rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]) : (int) ((PARAM_SHORT) this.evtParams[0]).value) != rhPtr.rhEvtProg.rhCurParam0)
          return false;
        rhPtr.rhEvtProg.evt_AddCurrentObject(hoPtr);
        return true;
      }

      public override bool eva2(CRun rhPtr)
      {
        return this.evtParams[0].code == (short) 10 ? this.evaObject(rhPtr, (IEvaObject) this) : this.evaExpObject(rhPtr, (IEvaExpObject) this);
      }

      public virtual bool evaExpRoutine(CObject hoPtr, int value_Renamed, short comp)
      {
        return value_Renamed == hoPtr.roa.raAnimOn && hoPtr.roa.raAnimNumberOfFrame == 0;
      }

      public virtual bool evaObjectRoutine(CObject hoPtr)
      {
        return (int) ((PARAM_SHORT) this.evtParams[0]).value == hoPtr.roa.raAnimOn && hoPtr.roa.raAnimNumberOfFrame == 0;
      }
    }
}
