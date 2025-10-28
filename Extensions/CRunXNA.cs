// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Extensions.CRunXNA
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

//using Microsoft.Devices;
//using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RuntimeXNA.Actions;
using RuntimeXNA.Conditions;
using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using System;

namespace RuntimeXNA.Extensions
{

    internal class CRunXNA : CRunExtension
    {
      private const int CND_TRIAL = 0;
      private const int CND_BACK = 1;
      private const int CND_DEACTIVATED = 2;
      private const int CND_REACTIVATED = 3;
      private const int CND_MUSICPLAYING = 4;
      private const int CND_LAST = 5;
      private const int ACT_OPENURL = 0;
      private const int ACT_VIBRATE = 1;
      private const int ACT_SETPLAYER = 2;
      private const int ACT_SETDEVICESELECTOR = 3;
      private const int ACT_OPENDEVICESELECTOR = 4;
      private const int FLAG_SIMULATE_TRIAL = 1;
      private bool bBack;
      private int flags;

      public override int getNumberOfConditions() => 5;

      public override bool createRunObject(CFile file, CCreateObjectInfo cob, int version)
      {
        this.flags = file.readAInt();
        this.rh.rhApp.XNAObject = this.ho;
        this.bBack = false;
        return true;
      }

      public override void destroyRunObject(bool bFast) => this.rh.rhApp.XNAObject = (CExtension) null;

      public override bool condition(int num, CCndExtension cnd)
      {
        switch (num)
        {
          case 0:
            return Guide.IsTrialMode;
          case 1:
            return this.cndBackPressed();
          case 2:
            return true;
          case 3:
            return true;
          case 4:
            return MediaPlayer.State == MediaState.Playing;
          default:
            return false;
        }
      }

      public bool cndBackPressed()
      {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
          return this.bBack;
        this.bBack = true;
        return false;
      }

      public override void action(int num, CActExtension act)
      {
        switch (num)
        {
          case 0:
            this.openURL(act);
            break;
          case 1:
            this.vibrate(act);
            break;
          case 2:
            this.setPlayer(act);
            break;
          case 3:
            this.setDeviceSelector(act);
            break;
          case 4:
            this.openDeviceSelector(act);
            break;
        }
      }

      private void setDeviceSelector(CActExtension act)
      {
        switch (act.getParamExpression(this.rh, 0))
        {
          case 1:
            this.rh.deviceSelectorPlayer = PlayerIndex.One;
            break;
          case 2:
            this.rh.deviceSelectorPlayer = PlayerIndex.Two;
            break;
          case 3:
            this.rh.deviceSelectorPlayer = PlayerIndex.Three;
            break;
          case 4:
            this.rh.deviceSelectorPlayer = PlayerIndex.Four;
            break;
        }
      }

      private void openDeviceSelector(CActExtension act)
      {
      }

      private void openURL(CActExtension act)
      {
        string paramExpString = act.getParamExpString(this.rh, 0);
        //new WebBrowserTask() { URL = paramExpString }.Show();
      }

      private void vibrate(CActExtension act)
      {
        int paramExpression = act.getParamExpression(this.rh, 0);
        if (paramExpression > 5)
          return;
        //VibrateController.Default.Start(new TimeSpan(0, 0, paramExpression));
      }

      private void setPlayer(CActExtension act)
      {
      }
    }
}
