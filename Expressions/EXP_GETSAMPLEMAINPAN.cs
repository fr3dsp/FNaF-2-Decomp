// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_GETSAMPLEMAINPAN
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_GETSAMPLEMAINPAN : CExp
    {
      public override void evaluate(CRun rhPtr)
      {
        rhPtr.getCurrentResult().forceInt(rhPtr.rhApp.soundPlayer.getMainPan());
      }
    }
}
