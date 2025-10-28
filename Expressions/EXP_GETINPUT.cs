// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_GETINPUT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_GETINPUT : CExpOi
    {
      public override void evaluate(CRun rhPtr)
      {
        int oi = (int) this.oi;
        int num = 5;
        if (oi < 4)
          num = (int) rhPtr.rhApp.pcCtrlType[oi];
        if (num == 5)
          num = 0;
        rhPtr.getCurrentResult().forceInt(num);
      }
    }
}
