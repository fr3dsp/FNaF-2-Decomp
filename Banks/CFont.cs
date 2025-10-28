// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Banks.CFont
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RuntimeXNA.Application;
using RuntimeXNA.Services;

namespace RuntimeXNA.Banks
{

    public class CFont
    {
      public short useCount;
      public short handle;
      public int lfHeight;
      public byte lfItalic;
      public int lfWeight;
      public string lfFaceName;
      public SpriteFont spriteFont;
      private ContentManager content;

      public void loadHandle(CFile file)
      {
        this.handle = file.readAShort();
        if (!file.bUnicode)
          file.skipBytes(41);
        else
          file.skipBytes(73);
      }

      public void load(CFile file, ContentManager c)
      {
        this.content = c;
        this.handle = file.readAShort();
        int filePointer = file.getFilePointer();
        this.lfHeight = file.readAInt();
        this.lfWeight = file.readAInt();
        this.lfItalic = file.readAByte();
        this.lfFaceName = file.readAString();
        if (!file.bUnicode)
          file.seek(filePointer + 41);
        else
          file.seek(filePointer + 73);
      }

      public CFontInfo getFontInfo()
      {
        return new CFontInfo()
        {
          lfHeight = this.lfHeight,
          lfWeight = this.lfWeight,
          lfItalic = this.lfItalic,
          lfFaceName = this.lfFaceName
        };
      }

      public static CFont createFromFontInfo(CFontInfo info, CRunApp app)
      {
        return new CFont()
        {
          content = app.content,
          lfHeight = info.lfHeight,
          lfWeight = info.lfWeight,
          lfItalic = info.lfItalic,
          lfFaceName = info.lfFaceName
        };
      }

      public SpriteFont getFont()
      {
        if (this.spriteFont == null)
        {
          string str = this.lfFaceName;
          while (true)
          {
            int length = str.IndexOf(' ');
            if (length >= 0)
              str = str.Substring(0, length) + str.Substring(length + 1);
            else
              break;
          }
          string assetName = str + this.lfHeight.ToString();
          if (this.lfWeight > 400)
            assetName += "Bold";
          if (this.lfItalic != (byte) 0)
            assetName += "Italic";
          this.spriteFont = this.content.Load<SpriteFont>(assetName);
        }
        return this.spriteFont;
      }
    }
}
