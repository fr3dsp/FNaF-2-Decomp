// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.OI.CDefTexts
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Banks;
using RuntimeXNA.Services;

namespace RuntimeXNA.OI
{

    public class CDefTexts : CDefObject
    {
      public int otCx;
      public int otCy;
      public int otNumberOfText;
      public CDefText[] otTexts;

      public override void load(CFile file)
      {
        int filePointer = file.getFilePointer();
        file.skipBytes(4);
        this.otCx = file.readAInt();
        this.otCy = file.readAInt();
        this.otNumberOfText = file.readAInt();
        this.otTexts = new CDefText[this.otNumberOfText];
        int[] numArray = new int[this.otNumberOfText];
        for (int index = 0; index < this.otNumberOfText; ++index)
          numArray[index] = file.readAInt();
        for (int index = 0; index < this.otNumberOfText; ++index)
        {
          this.otTexts[index] = new CDefText();
          file.seek(filePointer + numArray[index]);
          this.otTexts[index].load(file);
        }
      }

      public override void enumElements(IEnum enumImages, IEnum enumFonts)
      {
        for (int index = 0; index < this.otNumberOfText; ++index)
          this.otTexts[index].enumElements(enumImages, enumFonts);
      }
    }
}
