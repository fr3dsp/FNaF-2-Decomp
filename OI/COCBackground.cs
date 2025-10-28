// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.OI.COCBackground
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Services;

namespace RuntimeXNA.OI
{

    internal class COCBackground : COC
    {
      public short ocImage;

      public override void load(CFile file, short type)
      {
        file.skipBytes(4);
        this.ocObstacleType = file.readAShort();
        this.ocColMode = file.readAShort();
        this.ocCx = file.readAInt();
        this.ocCy = file.readAInt();
        this.ocImage = file.readAShort();
      }

      public override void enumElements(IEnum enumImages, IEnum enumFonts)
      {
        if (enumImages == null)
          return;
        short num = enumImages.enumerate(this.ocImage);
        if (num == (short) -1)
          return;
        this.ocImage = num;
      }
    }
}
