// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Services.CFile
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

namespace RuntimeXNA.Services
{

    public class CFile
    {
      private byte[] data;
      public int pointer;
      public bool bUnicode;

      public CFile()
      {
      }

      public CFile(CFile file)
      {
        this.data = file.data;
        this.pointer = 0;
      }

      public CFile(byte[] dt)
      {
        this.data = dt;
        this.pointer = 0;
      }

      public CFile(CFile source, int length)
      {
        this.data = new byte[length];
        int index;
        for (index = 0; index < length; ++index)
          this.data[index] = source.data[source.pointer + index];
        source.pointer += index;
        this.bUnicode = source.bUnicode;
      }

      public bool isEOF() => this.pointer >= this.data.Length;

      public void adjustTo8()
      {
        if ((this.pointer & 7) == 0)
          return;
        this.pointer += 8 - (this.pointer & 7);
      }

      public int readUnsignedByte()
      {
        return this.pointer < this.data.Length ? (int) this.data[this.pointer++] & (int) byte.MaxValue : 0;
      }

      public short readShort() => (short) (this.readUnsignedByte() << 8 | this.readUnsignedByte());

      public byte readByte() => this.pointer < this.data.Length ? this.data[this.pointer++] : (byte) 0;

      public byte[] readArray(int size)
      {
        if (size < 0)
          size = this.data.Length;
        byte[] numArray = new byte[size];
        for (int index = 0; index < size; ++index)
          numArray[index] = this.data[this.pointer++];
        return numArray;
      }

      public int read(byte[] dest, int size)
      {
        int index;
        for (index = 0; index < size; ++index)
          dest[index] = this.data[this.pointer++];
        return index;
      }

      public int read(byte[] dest)
      {
        int index;
        for (index = 0; index < dest.Length; ++index)
          dest[index] = this.data[this.pointer++];
        return index;
      }

      public void skipBytes(int n)
      {
        if (this.pointer + n >= this.data.Length)
          n = this.data.Length - this.pointer;
        this.pointer += n;
      }

      public void skipBack(int n)
      {
        int pos = this.getFilePointer() - n;
        if (pos < 0)
          pos = 0;
        this.seek(pos);
      }

      public void seek(int pos)
      {
        if (pos >= this.data.Length)
          pos = this.data.Length;
        this.pointer = pos;
      }

      public int getFilePointer() => this.pointer;

      public void setUnicode(bool unicode) => this.bUnicode = unicode;

      public byte readAByte() => this.data[this.pointer++];

      public short readAShort()
      {
        int num = this.readUnsignedByte();
        return (short) (this.readUnsignedByte() * 256 /*0x0100*/ + num);
      }

      public char readAChar()
      {
        int num = this.readUnsignedByte();
        return (char) (this.readUnsignedByte() * 256 /*0x0100*/ + num);
      }

      public void readAChar(char[] b)
      {
        for (int index = 0; index < b.Length; ++index)
        {
          int num1 = this.readUnsignedByte();
          int num2 = this.readUnsignedByte();
          b[index] = (char) (num2 * 256 /*0x0100*/ + num1);
        }
      }

      public int readAInt()
      {
        int num1 = this.readUnsignedByte();
        int num2 = this.readUnsignedByte();
        int num3 = this.readUnsignedByte();
        return this.readUnsignedByte() * 16777216 /*0x01000000*/ + num3 * 65536 /*0x010000*/ + num2 * 256 /*0x0100*/ + num1;
      }

      public int readAColor()
      {
        int num1 = this.readUnsignedByte();
        int num2 = this.readUnsignedByte();
        int num3 = this.readUnsignedByte();
        this.readUnsignedByte();
        return num1 * 65536 /*0x010000*/ + num2 * 256 /*0x0100*/ + num3;
      }

      public float readAFloat()
      {
        int num1 = this.readUnsignedByte();
        int num2 = this.readUnsignedByte();
        int num3 = this.readUnsignedByte();
        return (float) (this.readUnsignedByte() * 16777216 /*0x01000000*/ + num3 * 65536 /*0x010000*/ + num2 * 256 /*0x0100*/ + num1) / 65536f;
      }

      public double readADouble()
      {
        int num1 = this.readUnsignedByte();
        int num2 = this.readUnsignedByte();
        int num3 = this.readUnsignedByte();
        int num4 = this.readUnsignedByte();
        int num5 = this.readUnsignedByte();
        int num6 = this.readUnsignedByte();
        int num7 = this.readUnsignedByte();
        int num8 = this.readUnsignedByte();
        long num9 = (long) num4 * 16777216L /*0x01000000*/ + (long) num3 * 65536L /*0x010000*/ + (long) num2 * 256L /*0x0100*/ + (long) num1;
        return (double) ((long) num8 * 16777216L /*0x01000000*/ + (long) num7 * 65536L /*0x010000*/ + (long) num6 * 256L /*0x0100*/ + (long) num5 << 32 /*0x20*/ | num9) / 65536.0 / 65536.0;
      }

      public string readAString(int size)
      {
        if (!this.bUnicode)
        {
          byte[] dest = new byte[size];
          this.read(dest);
          int length = 0;
          while (length < size && dest[length] != (byte) 0)
            ++length;
          char[] chArray = new char[length];
          for (int index = 0; index < length; ++index)
            chArray[index] = (char) dest[index];
          return new string(chArray, 0, length);
        }
        char[] b = new char[size];
        this.readAChar(b);
        int length1 = 0;
        while (length1 < size && b[length1] != char.MinValue)
          ++length1;
        char[] chArray1 = new char[length1];
        for (int index = 0; index < length1; ++index)
          chArray1[index] = b[index];
        return new string(chArray1, 0, length1);
      }

      public string readAString()
      {
        string str = "";
        int filePointer1 = this.getFilePointer();
        if (!this.bUnicode)
        {
          do
            ;
          while (this.readUnsignedByte() != 0);
          int filePointer2 = this.getFilePointer();
          this.seek(filePointer1);
          if (filePointer2 >= filePointer1 + 2)
          {
            int length = filePointer2 - filePointer1 - 1;
            char[] chArray = new char[length];
            for (int index = 0; index < length; ++index)
              chArray[index] = (char) this.readUnsignedByte();
            str = new string(chArray, 0, length);
          }
          this.skipBytes(1);
        }
        else
        {
          do
            ;
          while (this.readAChar() != char.MinValue);
          int filePointer3 = this.getFilePointer();
          this.seek(filePointer1);
          if (filePointer3 >= filePointer1 + 2)
          {
            int length = (filePointer3 - filePointer1 - 2) / 2;
            char[] b = new char[length];
            this.readAChar(b);
            str = new string(b, 0, length);
          }
          this.skipBytes(2);
        }
        return str;
      }

      public string readAStringEOL()
      {
        int filePointer1 = this.getFilePointer();
        string str = "";
        if (!this.bUnicode)
        {
          int num1 = this.readUnsignedByte();
          while (num1 != 10 && num1 != 13 && !this.isEOF())
            num1 = this.readUnsignedByte();
          int filePointer2 = this.getFilePointer();
          this.seek(filePointer1);
          int num2 = 1;
          if (num1 != 10 && num1 != 13)
            num2 = 0;
          if (filePointer2 > filePointer1 + num2)
          {
            int length = filePointer2 - filePointer1 - num2;
            char[] chArray = new char[length];
            for (int index = 0; index < length; ++index)
              chArray[index] = (char) this.readUnsignedByte();
            str = new string(chArray, 0, chArray.Length);
          }
          if (num1 == 10 || num1 == 13)
          {
            this.skipBytes(1);
            int num3 = this.readUnsignedByte();
            if (num1 == 10 && num3 != 13)
              this.skipBack(1);
            if (num1 == 13 && num3 != 10)
              this.skipBack(1);
          }
        }
        else
        {
          char ch1 = this.readAChar();
          while (ch1 != '\n' && ch1 != '\r' && !this.isEOF())
            ch1 = this.readAChar();
          int filePointer3 = this.getFilePointer();
          this.seek(filePointer1);
          int num = 2;
          if (ch1 != '\n' && ch1 != '\r')
            num = 0;
          if (filePointer3 > filePointer1 + num)
          {
            char[] b = new char[(filePointer3 - filePointer1 - num) / 2];
            this.readAChar(b);
            str = new string(b, 0, b.Length);
          }
          if (ch1 == '\n' || ch1 == '\r')
          {
            this.skipBytes(2);
            char ch2 = this.readAChar();
            if (ch1 == '\n' && ch2 != '\r')
              this.skipBack(2);
            if (ch1 == '\r' && ch2 != '\n')
              this.skipBack(2);
          }
        }
        return str;
      }

      public void skipAString()
      {
        if (!this.bUnicode)
        {
          do
            ;
          while (this.readUnsignedByte() != 0);
        }
        else
        {
          do
            ;
          while (this.readShort() != (short) 0);
        }
      }

      public CFontInfo readLogFont()
      {
        CFontInfo cfontInfo = new CFontInfo();
        cfontInfo.lfHeight = this.readAInt();
        if (cfontInfo.lfHeight < 0)
          cfontInfo.lfHeight = -cfontInfo.lfHeight;
        this.skipBytes(12);
        cfontInfo.lfWeight = this.readAInt();
        cfontInfo.lfItalic = this.readAByte();
        cfontInfo.lfUnderline = this.readAByte();
        cfontInfo.lfStrikeOut = this.readAByte();
        this.skipBytes(5);
        cfontInfo.lfFaceName = this.readAString(32 /*0x20*/);
        return cfontInfo;
      }

      public CFontInfo readLogFont16()
      {
        CFontInfo cfontInfo = new CFontInfo();
        cfontInfo.lfHeight = (int) this.readAShort();
        if (cfontInfo.lfHeight < 0)
          cfontInfo.lfHeight = -cfontInfo.lfHeight;
        this.skipBytes(6);
        cfontInfo.lfWeight = (int) this.readAShort();
        cfontInfo.lfItalic = this.readAByte();
        cfontInfo.lfUnderline = this.readAByte();
        cfontInfo.lfStrikeOut = this.readAByte();
        this.skipBytes(5);
        bool bUnicode = this.bUnicode;
        this.bUnicode = false;
        cfontInfo.lfFaceName = this.readAString(32 /*0x20*/);
        this.bUnicode = bUnicode;
        return cfontInfo;
      }
    }
}
