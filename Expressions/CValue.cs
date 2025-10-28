// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.CValue
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using System;

namespace RuntimeXNA.Expressions
{

    public class CValue
    {
      public const byte TYPE_INT = 0;
      public const byte TYPE_DOUBLE = 1;
      public const byte TYPE_STRING = 2;
      public byte type;
      public int intValue;
      public double doubleValue;
      public string stringValue;

      public CValue()
      {
        this.type = (byte) 0;
        this.intValue = 0;
      }

      public CValue(CValue value)
      {
        switch (value.type)
        {
          case 0:
            this.intValue = value.intValue;
            break;
          case 1:
            this.doubleValue = value.doubleValue;
            break;
          case 2:
            this.stringValue = value.stringValue;
            break;
        }
        this.type = value.type;
      }

      public CValue(int i)
      {
        this.type = (byte) 0;
        this.intValue = i;
      }

      public CValue(double d)
      {
        this.type = (byte) 1;
        this.doubleValue = d;
      }

      public CValue(string s)
      {
        this.type = (byte) 2;
        this.stringValue = s;
      }

      public byte getType() => this.type;

      public int getInt()
      {
        switch (this.type)
        {
          case 0:
            return this.intValue;
          case 1:
            return (int) this.doubleValue;
          default:
            return 0;
        }
      }

      public double getDouble()
      {
        switch (this.type)
        {
          case 0:
            return (double) this.intValue;
          case 1:
            return this.doubleValue;
          default:
            return 0.0;
        }
      }

      public string getString() => this.type == (byte) 2 ? this.stringValue : "";

      public void forceInt(int value)
      {
        this.type = (byte) 0;
        this.intValue = value;
      }

      public void forceDouble(double value)
      {
        this.type = (byte) 1;
        this.doubleValue = value;
      }

      public void forceString(string value)
      {
        this.type = (byte) 2;
        this.stringValue = value;
      }

      public void forceValue(CValue value)
      {
        this.type = value.type;
        switch (this.type)
        {
          case 0:
            this.intValue = value.intValue;
            break;
          case 1:
            this.doubleValue = value.doubleValue;
            break;
          case 2:
            this.stringValue = value.stringValue;
            break;
        }
      }

      public void setValue(CValue value)
      {
        switch (this.type)
        {
          case 0:
            this.intValue = value.getInt();
            break;
          case 1:
            this.doubleValue = value.getDouble();
            break;
          case 2:
            this.stringValue = value.stringValue;
            break;
        }
      }

      public void getCompatibleTypes(CValue value)
      {
        if (this.type == (byte) 0 && value.type == (byte) 1)
        {
          this.convertToDouble();
        }
        else
        {
          if (this.type != (byte) 1 || value.type != (byte) 0)
            return;
          value.convertToDouble();
        }
      }

      public void convertToDouble()
      {
        if (this.type != (byte) 0)
          return;
        this.doubleValue = (double) this.intValue;
        this.type = (byte) 1;
      }

      public void convertToInt()
      {
        if (this.type != (byte) 1)
          return;
        this.intValue = (int) this.doubleValue;
        this.type = (byte) 0;
      }

      public void add(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            this.intValue += value.intValue;
            break;
          case 1:
            this.doubleValue += value.doubleValue;
            break;
          case 2:
            this.stringValue += value.stringValue;
            break;
        }
      }

      public void sub(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            this.intValue -= value.intValue;
            break;
          case 1:
            this.doubleValue -= value.doubleValue;
            break;
        }
      }

      public void negate()
      {
        switch (this.type)
        {
          case 0:
            this.intValue = -this.intValue;
            break;
          case 1:
            this.doubleValue = -this.doubleValue;
            break;
        }
      }

      public void mul(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            this.intValue *= value.intValue;
            break;
          case 1:
            this.doubleValue *= value.doubleValue;
            break;
        }
      }

      public void div(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            if (value.intValue != 0)
            {
              this.intValue /= value.intValue;
              break;
            }
            this.intValue = 0;
            break;
          case 1:
            if (value.doubleValue != 0.0)
            {
              this.doubleValue /= value.doubleValue;
              break;
            }
            this.doubleValue = 0.0;
            break;
        }
      }

      public void pow(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            this.doubleValue = Math.Pow(this.getDouble(), value.getDouble());
            this.type = (byte) 1;
            break;
          case 1:
            this.doubleValue = Math.Pow(this.doubleValue, value.doubleValue);
            break;
        }
      }

      public void mod(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            if (value.intValue == 0)
            {
              this.intValue = 0;
              break;
            }
            this.intValue %= value.intValue;
            break;
          case 1:
            if (value.doubleValue == 0.0)
            {
              this.doubleValue = 0.0;
              break;
            }
            this.doubleValue %= value.doubleValue;
            break;
        }
      }

      public void andLog(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            this.intValue &= value.intValue;
            break;
          case 1:
            this.forceInt(this.getInt() & value.getInt());
            break;
        }
      }

      public void orLog(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            this.intValue |= value.intValue;
            break;
          case 1:
            this.forceInt(this.getInt() | value.getInt());
            break;
        }
      }

      public void xorLog(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            this.intValue ^= value.intValue;
            break;
          case 1:
            this.forceInt(this.getInt() ^ value.getInt());
            break;
        }
      }

      public bool equal(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            return this.intValue == value.intValue;
          case 1:
            return this.doubleValue == value.doubleValue;
          case 2:
            return this.stringValue == value.stringValue;
          default:
            return false;
        }
      }

      public bool greater(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            return this.intValue >= value.intValue;
          case 1:
            return this.doubleValue >= value.doubleValue;
          case 2:
            return this.stringValue.CompareTo(value.stringValue) >= 0;
          default:
            return false;
        }
      }

      public bool lower(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            return this.intValue <= value.intValue;
          case 1:
            return this.doubleValue <= value.doubleValue;
          case 2:
            return this.stringValue.CompareTo(value.stringValue) <= 0;
          default:
            return false;
        }
      }

      public bool greaterThan(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            return this.intValue > value.intValue;
          case 1:
            return this.doubleValue > value.doubleValue;
          case 2:
            return this.stringValue.CompareTo(value.stringValue) > 0;
          default:
            return false;
        }
      }

      public bool lowerThan(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            return this.intValue < value.intValue;
          case 1:
            return this.doubleValue < value.doubleValue;
          case 2:
            return this.stringValue.CompareTo(value.stringValue) < 0;
          default:
            return false;
        }
      }

      public bool notEqual(CValue value)
      {
        if ((int) this.type != (int) value.type)
          this.getCompatibleTypes(value);
        switch (this.type)
        {
          case 0:
            return this.intValue != value.intValue;
          case 1:
            return this.doubleValue != value.doubleValue;
          case 2:
            return this.stringValue != value.stringValue;
          default:
            return false;
        }
      }
    }
}
