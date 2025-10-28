// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.CParamExpression
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;
using RuntimeXNA.Expressions;
using RuntimeXNA.Services;

namespace RuntimeXNA.Params
{

    public abstract class CParamExpression : CParam
    {
      public CExp[] tokens;
      public short comparaison;

      public virtual void load(CFile file)
      {
        long filePointer = (long) file.getFilePointer();
        int length = 0;
        while (true)
        {
          short num;
          do
          {
            ++length;
            if (file.readAInt() != 0)
              num = file.readAShort();
            else
              goto label_4;
          }
          while (num <= (short) 6);
          file.skipBytes((int) num - 6);
        }
    label_4:
        file.seek((int) filePointer);
        this.tokens = new CExp[length];
        for (int index = 0; index < length; ++index)
          this.tokens[index] = CExp.create(file);
      }

      public abstract override void load(CRunApp app);
    }
}
