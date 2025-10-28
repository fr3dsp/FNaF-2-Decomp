// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.PARAM_TIME
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.Params
{

    public class PARAM_TIME : CParam
    {
      public int timer;
      public int loops;

      public override void load(CRunApp app)
      {
        this.timer = app.file.readAInt();
        this.loops = app.file.readAInt();
      }
    }
}
