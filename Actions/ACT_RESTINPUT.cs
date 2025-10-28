// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_RESTINPUT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_RESTINPUT : CAct
    {
      public override void execute(CRun rhPtr) => rhPtr.rh2InputMask[(int) this.evtOi] = byte.MaxValue;
    }
}
