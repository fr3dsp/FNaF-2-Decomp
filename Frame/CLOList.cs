// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Frame.CLOList
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;
using RuntimeXNA.OI;

namespace RuntimeXNA.Frame
{

    public class CLOList
    {
      public CLO[] list;
      public short[] handleToIndex;
      public int nIndex;
      private int loFranIndex;

      public void load(CRunApp app)
      {
        this.nIndex = app.file.readAInt();
        this.list = new CLO[this.nIndex];
        short length = 0;
        for (int index = 0; index < this.nIndex; ++index)
        {
          this.list[index] = new CLO();
          this.list[index].load(app.file);
          if ((int) this.list[index].loHandle + 1 > (int) length)
            length = (short) ((int) this.list[index].loHandle + 1);
          COI oiFromHandle = app.OIList.getOIFromHandle(this.list[index].loOiHandle);
          this.list[index].loType = oiFromHandle.oiType;
        }
        this.handleToIndex = new short[(int) length];
        for (int index = 0; index < this.nIndex; ++index)
          this.handleToIndex[(int) this.list[index].loHandle] = (short) index;
      }

      public CLO getLOFromIndex(short index) => this.list[(int) index];

      public CLO getLOFromHandle(short handle)
      {
        return (int) handle < this.handleToIndex.Length ? this.list[(int) this.handleToIndex[(int) handle]] : (CLO) null;
      }

      public CLO next_LevObj()
      {
        if (this.loFranIndex < this.nIndex)
        {
          do
          {
            CLO clo = this.list[this.loFranIndex++];
            if (clo.loType >= (short) 2)
              return clo;
          }
          while (this.loFranIndex < this.nIndex);
        }
        return (CLO) null;
      }

      public CLO first_LevObj()
      {
        this.loFranIndex = 0;
        return this.next_LevObj();
      }
    }
}
