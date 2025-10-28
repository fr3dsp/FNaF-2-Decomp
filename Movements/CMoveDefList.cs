// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveDefList
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Services;

namespace RuntimeXNA.Movements
{

    public class CMoveDefList
    {
      public int nMovements;
      public CMoveDef[] moveList;

      public void load(CFile file)
      {
        int filePointer = file.getFilePointer();
        this.nMovements = file.readAInt();
        this.moveList = new CMoveDef[this.nMovements];
        for (int index = 0; index < this.nMovements; ++index)
        {
          file.seek(filePointer + 4 + 16 /*0x10*/ * index);
          int num1 = file.readAInt();
          int id = file.readAInt();
          int num2 = file.readAInt();
          int num3 = file.readAInt();
          file.seek(filePointer + num2);
          short c = file.readAShort();
          short t = file.readAShort();
          byte m = file.readByte();
          byte mo = file.readByte();
          file.skipBytes(2);
          int d = file.readAInt();
          switch (t)
          {
            case 0:
              this.moveList[index] = (CMoveDef) new CMoveDefStatic();
              break;
            case 1:
              this.moveList[index] = (CMoveDef) new CMoveDefMouse();
              break;
            case 2:
              this.moveList[index] = (CMoveDef) new CMoveDefRace();
              break;
            case 3:
              this.moveList[index] = (CMoveDef) new CMoveDefGeneric();
              break;
            case 4:
              this.moveList[index] = (CMoveDef) new CMoveDefBall();
              break;
            case 5:
              this.moveList[index] = (CMoveDef) new CMoveDefPath();
              break;
            case 9:
              this.moveList[index] = (CMoveDef) new CMoveDefPlatform();
              break;
            case 14:
              this.moveList[index] = (CMoveDef) new CMoveDefExtension();
              break;
          }
          this.moveList[index].setData(t, c, m, d, mo);
          this.moveList[index].load(file, num3 - 12);
          if (t == (short) 14)
          {
            file.seek(filePointer + num1);
            string str = file.readAString();
            string name = str.Substring(0, str.Length - 4);
            ((CMoveDefExtension) this.moveList[index]).setModuleName(name, id);
          }
        }
      }
    }
}
