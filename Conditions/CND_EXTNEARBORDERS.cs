// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_EXTNEARBORDERS
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_EXTNEARBORDERS : CCnd, IEvaExpObject
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr)
      {
        return this.evaExpObject(rhPtr, (IEvaExpObject) this);
      }

      public override bool eva2(CRun rhPtr) => this.evaExpObject(rhPtr, (IEvaExpObject) this);

      public virtual bool evaExpRoutine(CObject hoPtr, int bord, short comp)
      {
        int num1 = hoPtr.hoAdRunHeader.rhWindowX + bord;
        int num2 = hoPtr.hoX - hoPtr.hoImgXSpot;
        if (num2 <= num1)
          return this.negaTRUE();
        int num3 = hoPtr.hoAdRunHeader.rhWindowX + hoPtr.hoAdRunHeader.rh3WindowSx - bord;
        if (num2 + hoPtr.hoImgWidth >= num3)
          return this.negaTRUE();
        int num4 = hoPtr.hoAdRunHeader.rhWindowY + bord;
        int num5 = hoPtr.hoY - hoPtr.hoImgYSpot;
        if (num5 <= num4)
          return this.negaTRUE();
        int num6 = hoPtr.hoAdRunHeader.rhWindowY + hoPtr.hoAdRunHeader.rh3WindowSy - bord;
        return num5 + hoPtr.hoImgHeight >= num6 ? this.negaTRUE() : this.negaFALSE();
      }
    }
}
