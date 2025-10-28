// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_MOUSEWHEELDELTA
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_MOUSEWHEELDELTA : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        int num = 0;
        if (rhPtr.rh4MouseWheelDelta < (short) 0)
          num = 120;
        else if (rhPtr.rh4MouseWheelDelta > (short) 0)
          num = -120;
        rhPtr.getCurrentResult().forceInt(num);
      }
    }
}
