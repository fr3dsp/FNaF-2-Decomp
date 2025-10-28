// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Application.CSoundPlayer
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework.Audio;
using RuntimeXNA.Banks;

namespace RuntimeXNA.Application
{

    public class CSoundPlayer
    {
      private const int NCHANNELS = 32 /*0x20*/;
      private CRunApp app;
      private CSound[] channels;
      private bool bMultipleSounds;
      private bool bOn = true;
      private int[] volumes;
      private bool[] bLocked;
      private int[] pans;
      private int mainVolume;
      private int mainPan;

      public CSoundPlayer(CRunApp a)
      {
        this.app = a;
        this.channels = new CSound[32 /*0x20*/];
        this.volumes = new int[32 /*0x20*/];
        this.pans = new int[32 /*0x20*/];
        this.bLocked = new bool[32 /*0x20*/];
        this.bOn = true;
        this.bMultipleSounds = true;
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          this.channels[index] = (CSound) null;
          this.volumes[index] = 100;
          this.pans[index] = 0;
        }
        this.mainVolume = 100;
        this.mainPan = 0;
      }

      public void reset()
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
          this.bLocked[index] = false;
      }

      public void lockChannel(int channel)
      {
        if (channel < 0 || channel >= 32 /*0x20*/)
          return;
        this.bLocked[channel] = true;
      }

      public void unlockChannel(int channel)
      {
        if (channel < 0 || channel >= 32 /*0x20*/)
          return;
        this.bLocked[channel] = false;
      }

      public void play(short handle, int nLoops, int channel, bool bPrio)
      {
        if (!this.bOn)
          return;
        CSound source = this.app.soundBank.getSoundFromHandle(handle);
        if (source == null)
          return;
        if (!this.bMultipleSounds)
        {
          channel = 0;
        }
        else
        {
          for (int index = 0; index < 32 /*0x20*/; ++index)
          {
            if (this.channels[index] == source)
            {
              source = CSound.createFromSound(source);
              break;
            }
          }
        }
        if (channel < 0)
        {
          int index = 0;
          while (index < 32 /*0x20*/ && (this.channels[index] != null || this.bLocked[index]))
            ++index;
          if (index == 32 /*0x20*/)
          {
            index = 0;
            while (index < 32 /*0x20*/ && (this.bLocked[index] || this.channels[index] == null || this.channels[index].bUninterruptible))
              ++index;
          }
          channel = index;
          if (channel >= 0 && channel < 32 /*0x20*/)
            this.volumes[channel] = this.mainVolume;
        }
        if (channel < 0 || channel >= 32 /*0x20*/)
          return;
        if (this.channels[channel] != null)
        {
          if (this.channels[channel].bUninterruptible)
            return;
          if (this.channels[channel] != source)
            this.channels[channel].stop();
        }
        this.channels[channel] = source;
        source.play(nLoops, bPrio, (float) this.volumes[channel], (float) this.pans[channel]);
      }

      public void setMultipleSounds(bool bMultiple) => this.bMultipleSounds = bMultiple;

      public void keepCurrentSounds()
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null && this.channels[index].isPlaying())
            this.app.soundBank.setToLoad(this.channels[index].handle);
        }
      }

      public void setOnOff(bool bState)
      {
        if (bState == this.bOn)
          return;
        this.bOn = bState;
        if (this.bOn)
          return;
        this.stopAllSounds();
      }

      public bool getOnOff() => this.bOn;

      public void stopAllSounds()
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null)
          {
            this.channels[index].stop();
            this.channels[index] = (CSound) null;
          }
        }
      }

      public void stopSample(short handle)
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null && (int) this.channels[index].handle == (int) handle)
          {
            this.channels[index].stop();
            this.channels[index] = (CSound) null;
          }
        }
      }

      public void stopChannel(int channel)
      {
        if (channel < 0 || channel >= 32 /*0x20*/ || this.channels[channel] == null)
          return;
        this.channels[channel].stop();
        this.channels[channel] = (CSound) null;
      }

      public bool isSamplePaused(short handle)
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null && (int) this.channels[index].handle == (int) handle)
            return this.channels[index].isPaused();
        }
        return false;
      }

      public bool isSoundPlaying()
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null)
            return this.channels[index].isPlaying();
        }
        return false;
      }

      public bool isSamplePlaying(short handle)
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null && (int) this.channels[index].handle == (int) handle)
            return this.channels[index].isPlaying();
        }
        return false;
      }

      public bool isChannelPlaying(int channel)
      {
        return channel >= 0 && channel < 32 /*0x20*/ && this.channels[channel] != null && this.channels[channel].isPlaying();
      }

      public bool isChannelPaused(int channel)
      {
        return channel >= 0 && channel < 32 /*0x20*/ && this.channels[channel] != null && this.channels[channel].isPaused();
      }

      public void pause()
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null)
            this.channels[index].pause();
        }
      }

      public void pauseChannel(int channel)
      {
        if (channel < 0 || channel >= 32 /*0x20*/ || this.channels[channel] == null)
          return;
        this.channels[channel].pause();
      }

      public void pauseSample(short handle)
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null && (int) this.channels[index].handle == (int) handle)
            this.channels[index].pause();
        }
      }

      public void resume()
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null)
            this.channels[index].resume();
        }
      }

      public void resumeChannel(int channel)
      {
        if (channel < 0 || channel >= 32 /*0x20*/ || this.channels[channel] == null)
          return;
        this.channels[channel].resume();
      }

      public void resumeSample(short handle)
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null && (int) this.channels[index].handle == (int) handle)
            this.channels[index].resume();
        }
      }

      public void setVolumeChannel(int channel, int volume)
      {
        if (volume < 0)
          volume = 0;
        if (volume > 100)
          volume = 100;
        if (channel < 0 || channel >= 32 /*0x20*/)
          return;
        this.volumes[channel] = volume;
        if (this.channels[channel] == null)
          return;
        this.channels[channel].setVolume(volume);
      }

      public void setFrequencyChannel(int channel, int frequency)
      {
        if (frequency < 0)
          frequency = 100;
        if (channel < 0 || channel >= 32 /*0x20*/ || this.channels[channel] == null)
          return;
        this.channels[channel].setFrequency(frequency);
      }

      public int getVolumeChannel(int channel)
      {
        return channel >= 0 && channel < 32 /*0x20*/ && this.channels[channel] != null ? this.volumes[channel] : 0;
      }

      public int getFrequencyChannel(int channel)
      {
        return channel >= 0 && channel < 32 /*0x20*/ && this.channels[channel] != null ? this.channels[channel].getFrequency() : 0;
      }

      public void setVolumeSample(short handle, int volume)
      {
        if (volume < 0)
          volume = 0;
        if (volume > 100)
          volume = 100;
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null && (int) this.channels[index].handle == (int) handle)
          {
            this.volumes[index] = volume;
            this.channels[index].setVolume(volume);
          }
        }
      }

      public void setFrequencySample(short handle, int frequency)
      {
        if (frequency < 0)
          frequency = 100;
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null && (int) this.channels[index].handle == (int) handle)
            this.channels[index].setFrequency(frequency);
        }
      }

      public void setMainVolume(int volume)
      {
        if (volume < 0)
          volume = 0;
        if (volume > 100)
          volume = 100;
        this.mainVolume = volume;
        SoundEffect.MasterVolume = (float) volume / 100f;
      }

      public int getMainVolume() => this.mainVolume;

      public int getMainPan() => this.mainPan;

      public void setPanChannel(int channel, int pan)
      {
        if (pan < -100)
          pan = -100;
        if (pan > 100)
          pan = 100;
        if (channel < 0 || channel >= 32 /*0x20*/)
          return;
        this.pans[channel] = pan;
        if (this.channels[channel] == null)
          return;
        this.channels[channel].setPan(pan);
      }

      public int getPanChannel(int channel)
      {
        return channel >= 0 && channel < 32 /*0x20*/ && this.channels[channel] != null ? this.pans[channel] : 0;
      }

      public void setPanSample(short handle, int pan)
      {
        if (pan < -100)
          pan = -100;
        if (pan > 100)
          pan = 100;
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null && (int) this.channels[index].handle == (int) handle)
          {
            this.pans[index] = pan;
            this.channels[index].setPan(pan);
          }
        }
      }

      public void setMainPan(int pan)
      {
        if (pan < -100)
          pan = -100;
        if (pan > 100)
          pan = 100;
        this.mainPan = pan;
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          this.pans[index] = pan;
          if (this.channels[index] != null)
            this.channels[index].setPan(pan);
        }
      }

      private int getChannel(string name)
      {
        for (int channel = 0; channel < 32 /*0x20*/; ++channel)
        {
          if (this.channels[channel] != null && string.Compare(this.channels[channel].name, name) == 0)
            return channel;
        }
        return -1;
      }

      public int getDurationChannel(int channel)
      {
        return channel >= 0 && channel < 32 /*0x20*/ && this.channels[channel] != null ? this.channels[channel].getDuration() : 0;
      }

      public int getVolumeSample(string name)
      {
        int channel = this.getChannel(name);
        return channel >= 0 ? this.volumes[channel] : 0;
      }

      public int getDurationSample(string name)
      {
        int channel = this.getChannel(name);
        return channel >= 0 ? this.channels[channel].getDuration() : 0;
      }

      public int getPanSample(string name)
      {
        int channel = this.getChannel(name);
        return channel >= 0 ? this.pans[channel] : 0;
      }

      public int getFrequencySample(string name)
      {
        int channel = this.getChannel(name);
        return channel >= 0 ? this.channels[channel].getFrequency() : 0;
      }

      public void checkSounds()
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (this.channels[index] != null && this.channels[index].checkSound())
            this.channels[index] = (CSound) null;
        }
      }
    }
}
