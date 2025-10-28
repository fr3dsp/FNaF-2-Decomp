// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.PARAM_EVERY
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.Params
{

    public class PARAM_EVERY : CParam
    {
      public int delay;
      public int compteur;

      public override void load(CRunApp app)
      {
        this.delay = app.file.readAInt();
        this.compteur = app.file.readAInt();
      }
    }
}
