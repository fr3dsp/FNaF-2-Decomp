// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.PARAM_CREATE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.Params
{

    public class PARAM_CREATE : CCreate
    {
      public override void load(CRunApp app)
      {
        this.posOINUMParent = app.file.readAShort();
        this.posFlags = app.file.readAShort();
        this.posX = app.file.readAShort();
        this.posY = app.file.readAShort();
        this.posSlope = app.file.readAShort();
        this.posAngle = app.file.readAShort();
        this.posDir = app.file.readAInt();
        this.posTypeParent = app.file.readAShort();
        this.posOiList = app.file.readAShort();
        this.posLayer = app.file.readAShort();
        this.cdpHFII = app.file.readAShort();
        this.cdpOi = app.file.readAShort();
      }
    }
}
