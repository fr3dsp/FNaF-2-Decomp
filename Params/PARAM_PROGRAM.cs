// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.PARAM_PROGRAM
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.Params
{

    public class PARAM_PROGRAM : CParam
    {
      public const short PRGFLAGS_WAIT = 1;
      public const short PRGFLAGS_HIDE = 2;
      public short flags;
      public string filename;
      public string command;

      public override void load(CRunApp app)
      {
        this.flags = app.file.readAShort();
        int filePointer = app.file.getFilePointer();
        this.filename = app.file.readAString();
        app.file.seek(filePointer + 260);
        this.command = app.file.readAString();
      }
    }
}
