// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_PLAYXRIGHT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_PLAYXRIGHT : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        int num1 = rhPtr.rhWindowX;
        if (((int) rhPtr.rh3Scrolling & 1) != 0)
          num1 = rhPtr.rh3DisplayX;
        int num2 = num1 + rhPtr.rh3WindowSx;
        if (num2 > rhPtr.rhLevelSx)
          num2 = rhPtr.rhLevelSx;
        rhPtr.getCurrentResult().forceInt(num2);
      }
    }
}
