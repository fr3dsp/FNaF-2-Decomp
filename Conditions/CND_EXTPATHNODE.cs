// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_EXTPATHNODE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_EXTPATHNODE : CCnd, IEvaObject
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => true;

      public override bool eva2(CRun rhPtr) => this.evaObject(rhPtr, (IEvaObject) this);

      public virtual bool evaObjectRoutine(CObject hoPtr)
      {
        return hoPtr.roc.rcMovementType == 5 && this.checkMark(hoPtr.hoAdRunHeader, hoPtr.hoMark1);
      }
    }
}
