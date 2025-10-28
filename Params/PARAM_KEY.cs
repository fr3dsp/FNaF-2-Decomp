// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.PARAM_KEY
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework.Input;
using RuntimeXNA.Application;

namespace RuntimeXNA.Params
{

    public class PARAM_KEY : CParam
    {
      public Keys key;
      public short mouseKey;

      public override void load(CRunApp app)
      {
        short pcKey = app.file.readAShort();
        this.key = CKeyConvert.getXnaKey((int) pcKey);
        this.mouseKey = pcKey;
      }
    }
}
