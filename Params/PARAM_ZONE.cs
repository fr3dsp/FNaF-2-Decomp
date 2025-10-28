// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.PARAM_ZONE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.Params
{

    public class PARAM_ZONE : CParam
    {
      public short x1;
      public short y1;
      public short x2;
      public short y2;

      public override void load(CRunApp app)
      {
        this.x1 = app.file.readAShort();
        this.y1 = app.file.readAShort();
        this.x2 = app.file.readAShort();
        this.y2 = app.file.readAShort();
      }
    }
}
