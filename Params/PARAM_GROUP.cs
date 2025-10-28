// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.PARAM_GROUP
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.Params
{

    public class PARAM_GROUP : CParam
    {
      public const short GRPFLAGS_INACTIVE = 1;
      public const short GRPFLAGS_CLOSED = 2;
      public const short GRPFLAGS_PARENTINACTIVE = 4;
      public const short GRPFLAGS_GROUPINACTIVE = 8;
      public const short GRPFLAGS_GLOBAL = 16 /*0x10*/;
      public short grpFlags;
      public short grpId;

      public override void load(CRunApp app)
      {
        this.grpFlags = app.file.readAShort();
        this.grpId = app.file.readAShort();
      }
    }
}
