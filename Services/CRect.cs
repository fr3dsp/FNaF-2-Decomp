// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Services.CRect
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

namespace RuntimeXNA.Services
{

    public class CRect
    {
      public int left;
      public int top;
      public int right;
      public int bottom;

      public void load(CFile file)
      {
        this.left = file.readAInt();
        this.top = file.readAInt();
        this.right = file.readAInt();
        this.bottom = file.readAInt();
      }

      public void copyRect(CRect srce)
      {
        this.left = srce.left;
        this.right = srce.right;
        this.top = srce.top;
        this.bottom = srce.bottom;
      }

      public bool ptInRect(int x, int y)
      {
        return x >= this.left && x < this.right && y >= this.top && y < this.bottom;
      }

      public bool intersectRect(CRect rc)
      {
        return (this.left >= rc.left && this.left < rc.right || this.right >= rc.left && this.right < rc.right || rc.left >= this.left && rc.left < this.right || rc.right >= this.left && rc.right < this.right) && (this.top >= rc.top && this.top < rc.bottom || this.bottom >= rc.top && this.bottom < rc.bottom || rc.top >= this.top && rc.top < this.bottom || rc.bottom >= this.top && rc.bottom < this.bottom);
      }

      public void offsetRect(int xOffset, int yOffset)
      {
        this.left += xOffset;
        this.top += yOffset;
        this.right += xOffset;
        this.bottom += yOffset;
      }
    }
}
