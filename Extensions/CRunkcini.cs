// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Extensions.CRunkcini
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Actions;
using RuntimeXNA.Expressions;
using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using System;

namespace RuntimeXNA.Extensions
{

    internal class CRunkcini : CRunExtension
    {
      public const int INI_UTF8 = 8;
      private int saveCounter;
      private CIni ini;
      private short iniFlags;
      private string iniName;
      private string iniCurrentGroup;
      private string iniCurrentItem;

      public override int getNumberOfConditions() => 0;

      private void cleanName()
      {
        int num = this.iniName.LastIndexOf('\\');
        if (num < 0)
          num = this.iniName.LastIndexOf('/');
        if (num < 0 || num + 1 >= this.iniName.Length)
          return;
        this.iniName = this.iniName.Substring(num + 1);
      }

      public override bool createRunObject(CFile file, CCreateObjectInfo cob, int version)
      {
        this.iniFlags = file.readAShort();
        this.iniName = file.readAString();
        if (this.iniName.Length == 0)
          this.iniName = "Default.ini";
        this.cleanName();
        this.ini = new CIni(this, this.iniFlags);
        this.saveCounter = 0;
        this.iniCurrentGroup = "Group";
        this.iniCurrentItem = "Item";
        return false;
      }

      public override void destroyRunObject(bool bFast) => this.ini.saveIni();

      public override int handleRunObject()
      {
        if (this.saveCounter > 0)
        {
          --this.saveCounter;
          if (this.saveCounter <= 0)
          {
            this.saveCounter = 0;
            this.ini.saveIni();
          }
        }
        return 0;
      }

      public override void action(int num, CActExtension act)
      {
        switch (num)
        {
          case 0:
            this.SetCurrentGroup(act);
            break;
          case 1:
            this.SetCurrentItem(act);
            break;
          case 2:
            this.SetValue(act);
            break;
          case 3:
            this.SavePosition(act);
            break;
          case 4:
            this.LoadPosition(act);
            break;
          case 5:
            this.SetString(act);
            break;
          case 6:
            this.SetCurrentFile(act);
            break;
          case 7:
            this.SetValueItem(act);
            break;
          case 8:
            this.SetValueGroupItem(act);
            break;
          case 9:
            this.SetStringItem(act);
            break;
          case 10:
            this.SetStringGroupItem(act);
            break;
          case 11:
            this.DeleteItem(act);
            break;
          case 12:
            this.DeleteGroupItem(act);
            break;
          case 13:
            this.DeleteGroup(act);
            break;
        }
      }

      private void SetCurrentGroup(CActExtension act)
      {
        this.iniCurrentGroup = act.getParamExpString(this.rh, 0);
      }

      private void SetCurrentItem(CActExtension act)
      {
        this.iniCurrentItem = act.getParamExpString(this.rh, 0);
      }

      private void SetValue(CActExtension act)
      {
        this.ini.writePrivateProfileString(this.iniCurrentGroup, this.iniCurrentItem, act.getParamExpression(this.rh, 0).ToString(), this.iniName);
        this.saveCounter = 50;
      }

      private void SavePosition(CActExtension act)
      {
        CObject paramObject = act.getParamObject(this.rh, 0);
        string name = $"{paramObject.hoX.ToString()},{paramObject.hoY.ToString()}";
        this.ini.writePrivateProfileString(this.iniCurrentGroup, "pos." + paramObject.hoOiList.oilName, name, this.iniName);
        this.saveCounter = 50;
      }

      private void LoadPosition(CActExtension act)
      {
        CObject paramObject = act.getParamObject(this.rh, 0);
        string privateProfileString = this.ini.getPrivateProfileString(this.iniCurrentGroup, "pos." + paramObject.hoOiList.oilName, "X", this.iniName);
        if (string.Compare(privateProfileString, "X") == 0)
          return;
        int length = privateProfileString.IndexOf(",");
        string str1 = privateProfileString.Substring(0, length);
        string str2 = privateProfileString.Substring(length + 1);
        try
        {
          paramObject.hoX = Convert.ToInt32(str1, 10);
        }
        catch (FormatException ex)
        {
          ex.GetType();
        }
        catch (ArgumentOutOfRangeException ex)
        {
          ex.GetType();
        }
        try
        {
          paramObject.hoY = Convert.ToInt32(str2, 10);
        }
        catch (FormatException ex)
        {
          ex.GetType();
        }
        catch (ArgumentOutOfRangeException ex)
        {
          ex.GetType();
        }
        paramObject.roc.rcChanged = true;
        paramObject.roc.rcCheckCollides = true;
      }

      private void SetString(CActExtension act)
      {
        this.ini.writePrivateProfileString(this.iniCurrentGroup, this.iniCurrentItem, act.getParamExpString(this.rh, 0), this.iniName);
        this.saveCounter = 50;
      }

      private void SetCurrentFile(CActExtension act)
      {
        this.iniName = act.getParamExpString(this.rh, 0);
        this.cleanName();
      }

      private void SetValueItem(CActExtension act)
      {
        this.ini.writePrivateProfileString(this.iniCurrentGroup, act.getParamExpString(this.rh, 0), act.getParamExpression(this.rh, 1).ToString(), this.iniName);
        this.saveCounter = 50;
      }

      private void SetValueGroupItem(CActExtension act)
      {
        this.ini.writePrivateProfileString(act.getParamExpString(this.rh, 0), act.getParamExpString(this.rh, 1), act.getParamExpression(this.rh, 2).ToString(), this.iniName);
        this.saveCounter = 50;
      }

      private void SetStringItem(CActExtension act)
      {
        this.ini.writePrivateProfileString(this.iniCurrentGroup, act.getParamExpString(this.rh, 0), act.getParamExpString(this.rh, 1), this.iniName);
        this.saveCounter = 50;
      }

      private void SetStringGroupItem(CActExtension act)
      {
        this.ini.writePrivateProfileString(act.getParamExpString(this.rh, 0), act.getParamExpString(this.rh, 1), act.getParamExpString(this.rh, 2), this.iniName);
        this.saveCounter = 50;
      }

      private void DeleteItem(CActExtension act)
      {
        this.ini.deleteItem(this.iniCurrentGroup, act.getParamExpString(this.rh, 0), this.iniName);
        this.saveCounter = 50;
      }

      private void DeleteGroupItem(CActExtension act)
      {
        this.ini.deleteItem(act.getParamExpString(this.rh, 0), act.getParamExpString(this.rh, 1), this.iniName);
        this.saveCounter = 50;
      }

      private void DeleteGroup(CActExtension act)
      {
        this.ini.deleteGroup(act.getParamExpString(this.rh, 0), this.iniName);
        this.saveCounter = 50;
      }

      public override CValue expression(int num)
      {
        switch (num)
        {
          case 0:
            return this.GetValue();
          case 1:
            return this.GetString();
          case 2:
            return this.GetValueItem();
          case 3:
            return this.GetValueGroupItem();
          case 4:
            return this.GetStringItem();
          case 5:
            return this.GetStringGroupItem();
          default:
            return (CValue) null;
        }
      }

      private CValue GetValue()
      {
        string privateProfileString = this.ini.getPrivateProfileString(this.iniCurrentGroup, this.iniCurrentItem, "", this.iniName);
        int i = 0;
        if (privateProfileString.Length > 0)
        {
          try
          {
            i = Convert.ToInt32(privateProfileString, 10);
          }
          catch (FormatException ex)
          {
            ex.GetType();
          }
          catch (ArgumentOutOfRangeException ex)
          {
            ex.GetType();
          }
        }
        return new CValue(i);
      }

      private CValue GetString()
      {
        return new CValue(this.ini.getPrivateProfileString(this.iniCurrentGroup, this.iniCurrentItem, "", this.iniName));
      }

      private CValue GetValueItem()
      {
        string privateProfileString = this.ini.getPrivateProfileString(this.iniCurrentGroup, this.ho.getExpParam().getString(), "", this.iniName);
        int i = 0;
        if (privateProfileString.Length > 0)
        {
          try
          {
            i = Convert.ToInt32(privateProfileString, 10);
          }
          catch (FormatException ex)
          {
            ex.GetType();
          }
          catch (ArgumentOutOfRangeException ex)
          {
            ex.GetType();
          }
        }
        return new CValue(i);
      }

      private CValue GetValueGroupItem()
      {
        string privateProfileString = this.ini.getPrivateProfileString(this.ho.getExpParam().getString(), this.ho.getExpParam().getString(), "", this.iniName);
        int i = 0;
        if (privateProfileString.Length > 0)
        {
          try
          {
            i = Convert.ToInt32(privateProfileString, 10);
          }
          catch (FormatException ex)
          {
            ex.GetType();
          }
          catch (ArgumentOutOfRangeException ex)
          {
            ex.GetType();
          }
        }
        return new CValue(i);
      }

      private CValue GetStringItem()
      {
        return new CValue(this.ini.getPrivateProfileString(this.iniCurrentGroup, this.ho.getExpParam().getString(), "", this.iniName));
      }

      private CValue GetStringGroupItem()
      {
        return new CValue(this.ini.getPrivateProfileString(this.ho.getExpParam().getString(), this.ho.getExpParam().getString(), "", this.iniName));
      }
    }
}
