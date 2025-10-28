// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Services.CArrayList
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

namespace RuntimeXNA.Services
{

    public class CArrayList
    {
      private const int GROWTH_STEP = 5;
      private object[] array;
      private int numberOfEntries;

      private void getArray(int max)
      {
        if (this.array == null)
        {
          this.array = new object[max + 5];
        }
        else
        {
          if (max < this.array.Length)
            return;
          object[] objArray = new object[max + 5];
          for (int index = 0; index < this.array.Length; ++index)
            objArray[index] = this.array[index];
          this.array = objArray;
        }
      }

      public void ensureCapacity(int max) => this.getArray(max);

      public bool isEmpty() => this.numberOfEntries == 0;

      public void add(object o)
      {
        this.getArray(this.numberOfEntries);
        this.array[this.numberOfEntries++] = o;
      }

      public void add(int index, object o)
      {
        this.getArray(this.numberOfEntries);
        for (int numberOfEntries = this.numberOfEntries; numberOfEntries > index; --numberOfEntries)
          this.array[numberOfEntries] = this.array[numberOfEntries - 1];
        this.array[index] = o;
        ++this.numberOfEntries;
      }

      public object get(int index)
      {
        return this.array != null && index < this.array.Length ? this.array[index] : (object) null;
      }

      public void set(int index, object o)
      {
        if (this.array == null || index >= this.array.Length)
          return;
        this.array[index] = o;
      }

      public void insert(int index, object o)
      {
        this.getArray(this.numberOfEntries);
        for (int numberOfEntries = this.numberOfEntries; numberOfEntries > index; --numberOfEntries)
          this.array[numberOfEntries] = this.array[numberOfEntries - 1];
        this.array[index] = o;
        ++this.numberOfEntries;
      }

      public void swap(int index1, int index2)
      {
        if (this.array == null)
          return;
        object obj = this.array[index1];
        this.array[index1] = this.array[index2];
        this.array[index2] = obj;
      }

      public void swap(object o1, object o2)
      {
        if (this.array == null)
          return;
        int index1 = this.indexOf(o1);
        int index2 = this.indexOf(o2);
        if (index1 < 0 || index2 < 0)
          return;
        this.swap(index1, index2);
      }

      public void remove(int index)
      {
        if (this.array == null || index >= this.array.Length || this.numberOfEntries <= 0)
          return;
        for (int index1 = index; index1 < this.numberOfEntries - 1; ++index1)
          this.array[index1] = this.array[index1 + 1];
        --this.numberOfEntries;
        this.array[this.numberOfEntries] = (object) null;
      }

      public int indexOf(object o)
      {
        for (int index = 0; index < this.numberOfEntries; ++index)
        {
          if (this.array[index] == o)
            return index;
        }
        return -1;
      }

      public void remove(object o)
      {
        int index = this.indexOf(o);
        if (index < 0)
          return;
        this.remove(index);
      }

      public int size() => this.numberOfEntries;

      public void clear() => this.numberOfEntries = 0;
    }
}
