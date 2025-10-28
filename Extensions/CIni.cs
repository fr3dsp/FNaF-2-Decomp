// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Extensions.CIni
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using BinaryRead;
using RuntimeXNA.Application;
using RuntimeXNA.Services;
using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace RuntimeXNA.Extensions
{

    internal class CIni
    {
      private CRunkcini ini;
      private CArrayList strings;
      private string currentFileName;
      private short flags;

      public CIni(CRunkcini i, short f)
      {
        this.ini = i;
        this.flags = f;
      }

      public void loadFromProject()
      {
        CFile cfile = (CFile) null;
        CEmbeddedFile embeddedFile = this.ini.rh.rhApp.getEmbeddedFile(this.currentFileName);
        if (embeddedFile != null)
          cfile = embeddedFile.open();
        if (cfile == null)
        {
          string assetName = this.currentFileName;
          int length = assetName.LastIndexOf('.');
          if (length >= 0)
            assetName = assetName.Substring(0, length);
          Data data = (Data) null;
          try
          {
            data = this.ini.rh.rhApp.content.Load<Data>(assetName);
          }
          catch (Exception ex)
          {
            ex.GetType();
          }
          if (data != null)
            cfile = new CFile(data.data);
        }
        if (cfile == null)
          return;
        if (((int) this.flags & 8) != 0)
          cfile.setUnicode(false);
        while (!cfile.isEOF())
        {
          string o = cfile.readAStringEOL();
          if (o == null)
            break;
          this.strings.add((object) o);
        }
      }

      public void loadIni(string fileName)
      {
        bool flag = true;
        if (this.currentFileName != null && string.Compare(this.currentFileName, fileName, StringComparison.OrdinalIgnoreCase) == 0)
          flag = false;
        if (!flag)
          return;
        this.saveIni();
        this.strings = new CArrayList();
        this.currentFileName = fileName;
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (storeForApplication.FileExists(this.currentFileName))
          {
            using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream(this.currentFileName, FileMode.Open, storeForApplication))
            {
              using (StreamReader streamReader = new StreamReader((Stream) storageFileStream))
              {
                try
                {
                  while (true)
                  {
                    string o = streamReader.ReadLine();
                    if (o != null)
                      this.strings.add((object) o);
                    else
                      break;
                  }
                }
                catch (IOException ex)
                {
                  ex.GetType();
                }
                streamReader.Close();
                streamReader.Dispose();
              }
            }
          }
          else
            this.loadFromProject();
          storeForApplication.Dispose();
        }
      }

      public void saveIni()
      {
        if (this.strings == null || this.currentFileName == null)
          return;
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream(this.currentFileName, FileMode.Create, storeForApplication))
          {
            using (StreamWriter streamWriter = new StreamWriter((Stream) storageFileStream))
            {
              try
              {
                for (int index = 0; index < this.strings.size(); ++index)
                  streamWriter.WriteLine((string) this.strings.get(index));
              }
              catch (IOException ex)
              {
                ex.GetType();
              }
              streamWriter.Flush();
              streamWriter.Close();
              streamWriter.Dispose();
            }
          }
          storeForApplication.Dispose();
        }
      }

      private int findSection(string sectionName)
      {
        for (int index = 0; index < this.strings.size(); ++index)
        {
          string str = (string) this.strings.get(index);
          if (str[0] == '[')
          {
            int num = str.LastIndexOf(']');
            if (num >= 1 && string.Compare(str.Substring(1, num - 1), sectionName, StringComparison.OrdinalIgnoreCase) == 0)
              return index;
          }
        }
        return -1;
      }

      private int findKey(int l, string keyName)
      {
        for (; l < this.strings.size(); ++l)
        {
          string str = (string) this.strings.get(l);
          if (str[0] == '[')
            return -1;
          int length = str.IndexOf('=');
          if (length >= 0 && string.Compare(str.Substring(0, length), keyName) == 0)
            return l;
        }
        return -1;
      }

      public string getPrivateProfileString(
        string sectionName,
        string keyName,
        string defaultString,
        string fileName)
      {
        this.loadIni(fileName);
        int section = this.findSection(sectionName);
        if (section >= 0)
        {
          int key = this.findKey(section + 1, keyName);
          if (key >= 0)
          {
            string str = (string) this.strings.get(key);
            int num = str.IndexOf('=') + 1;
            while (num < str.Length && str[num] == ' ')
              ++num;
            int length = str.Length;
            while (length > num && str[length - 1] == ' ')
              --length;
            if (length > num)
              return str.Substring(num, length - num);
          }
        }
        return defaultString;
      }

      public void writePrivateProfileString(
        string sectionName,
        string keyName,
        string name,
        string fileName)
      {
        this.loadIni(fileName);
        int section = this.findSection(sectionName);
        if (section < 0)
        {
          this.strings.add((object) $"[{sectionName}]");
          this.strings.add((object) $"{keyName}={name}");
        }
        else
        {
          int key = this.findKey(section + 1, keyName);
          if (key >= 0)
          {
            string o = $"{keyName}={name}";
            this.strings.set(key, (object) o);
          }
          else
          {
            for (int index = section + 1; index < this.strings.size(); ++index)
            {
              if (((string) this.strings.get(index))[0] == '[')
              {
                string o = $"{keyName}={name}";
                this.strings.add(index, (object) o);
                return;
              }
            }
            this.strings.add((object) $"{keyName}={name}");
          }
        }
      }

      public void deleteItem(string group, string item, string iniName)
      {
        this.loadIni(iniName);
        int section = this.findSection(group);
        if (section < 0)
          return;
        int key = this.findKey(section + 1, item);
        if (key < 0)
          return;
        this.strings.remove(key);
      }

      public void deleteGroup(string group, string iniName)
      {
        this.loadIni(iniName);
        int section = this.findSection(group);
        if (section < 0)
          return;
        this.strings.remove(section);
        while (section < this.strings.size() && ((string) this.strings.get(section))[0] != '[')
          this.strings.remove(section);
      }
    }
}
