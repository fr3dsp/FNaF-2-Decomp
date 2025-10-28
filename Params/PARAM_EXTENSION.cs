// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.PARAM_EXTENSION
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.Params
{

    public class PARAM_EXTENSION : CParam
    {
      public byte[] data;

      public override void load(CRunApp app)
      {
        short num = app.file.readAShort();
        app.file.skipBytes(4);
        if (num <= (short) 6)
          return;
        this.data = new byte[(int) num - 6];
        app.file.read(this.data);
      }
    }
}
