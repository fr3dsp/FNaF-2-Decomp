// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_FRAMERATE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_FRAMERATE : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        int num = 0;
        for (int index = 0; index < 10; ++index)
          num += rhPtr.rh4FrameRateArray[index];
        if (num != 0)
          rhPtr.getCurrentResult().forceInt(10000 / num);
        else
          rhPtr.getCurrentResult().forceInt(0);
      }
    }
}
