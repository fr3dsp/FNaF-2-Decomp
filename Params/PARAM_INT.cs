// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.PARAM_INT
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.Params
{

    public class PARAM_INT : CParam
    {
      public int value_Renamed;
      public int value2;

      public override void load(CRunApp app)
      {
        this.value_Renamed = app.file.readAInt();
        this.value2 = 0;
      }
    }
}
