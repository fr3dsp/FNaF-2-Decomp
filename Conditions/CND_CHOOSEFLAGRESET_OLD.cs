// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_CHOOSEFLAGRESET_OLD
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_CHOOSEFLAGRESET_OLD : CCnd, IChooseValue
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr) => this.evaChooseValueOld(rhPtr, (IChooseValue) this);

      public virtual bool evaluate(CObject pHo, int value_Renamed)
      {
        return pHo.rov != null && (pHo.rov.rvValueFlags & 1 << value_Renamed) == 0;
      }
    }
}
