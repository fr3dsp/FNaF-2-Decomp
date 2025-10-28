// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_EXTFACING
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_EXTFACING : CCnd, IEvaExpObject, IEvaObject
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        return this.evtParams[0].code == (short) 29 ? this.evaObject(rhPtr, (IEvaObject) this) : this.evaExpObject(rhPtr, (IEvaExpObject) this);
      }

      public virtual bool evaObjectRoutine(CObject hoPtr)
      {
        int valueRenamed = ((PARAM_INT) this.evtParams[0]).value_Renamed;
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if ((1 << index & valueRenamed) != 0 && hoPtr.roc.rcDir == index)
            return this.negaTRUE();
        }
        return this.negaFALSE();
      }

      public virtual bool evaExpRoutine(CObject hoPtr, int value_Renamed, short comp)
      {
        value_Renamed &= 31 /*0x1F*/;
        return hoPtr.roc.rcDir == value_Renamed ? this.negaTRUE() : this.negaFALSE();
      }
    }
}
