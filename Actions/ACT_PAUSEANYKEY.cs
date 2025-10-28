// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_PAUSEANYKEY
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework.Input;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_PAUSEANYKEY : CAct
    {
      public override void execute(CRun rhPtr)
      {
        rhPtr.rh4PauseKey = Keys.None;
        rhPtr.bAnyKeyDown = true;
        rhPtr.bCheckResume = true;
        rhPtr.pause();
      }
    }
}
