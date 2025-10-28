// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Services.CChunk
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

namespace RuntimeXNA.Services
{

    internal class CChunk
    {
      public const short CHUNK_LAST = 32639;
      public short chID;
      public short chFlags;
      public int chSize;

      public short readHeader(CFile file)
      {
        this.chID = file.readAShort();
        this.chFlags = file.readAShort();
        this.chSize = file.readAInt();
        return this.chID;
      }

      public void skipChunk(CFile file) => file.skipBytes(this.chSize);
    }
}
