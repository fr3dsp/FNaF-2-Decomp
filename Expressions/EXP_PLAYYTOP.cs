// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_PLAYYTOP
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_PLAYYTOP : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        int num = rhPtr.rhWindowY;
        if (((int) rhPtr.rh3Scrolling & 1) != 0)
          num = rhPtr.rh3DisplayY;
        if (num < 0)
          num = 0;
        rhPtr.getCurrentResult().forceInt(num);
      }
    }
}
