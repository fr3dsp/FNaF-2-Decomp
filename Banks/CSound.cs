// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Banks.CSound
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using RuntimeXNA.Application;
using System;

namespace RuntimeXNA.Banks
{

    public class CSound
    {
      public short handle;
      public SoundEffect sound;
      public SoundEffectInstance soundInstance;
      public int useCount;
      public bool bUninterruptible;
      public int nLoops;
      public int numSound;
      public string name;
      public int type;
      public Song song;
      public bool bSongPlaying;
      public bool bPaused;
      public long timer;
      public int frequency;
      public CRunApp application;

      public CSound(CRunApp app) => this.application = app;

      public void loadHandle()
      {
        this.handle = this.application.file.readAShort();
        this.application.file.skipBytes(5);
        int n = (int) this.application.file.readAShort();
        if (!this.application.file.bUnicode)
          this.application.file.skipBytes(n);
        else
          this.application.file.skipBytes(n * 2);
      }

      public static CSound createFromSound(CSound source)
      {
        return new CSound(source.application)
        {
          handle = source.handle,
          sound = source.sound,
          name = source.name,
          type = source.type,
          song = source.song
        };
      }

      public void load()
      {
        this.handle = this.application.file.readAShort();
        this.type = (int) this.application.file.readAByte();
        this.frequency = this.application.file.readAInt();
        this.name = this.application.file.readAString((int) this.application.file.readAShort());
        string assetName = "Snd" + this.handle.ToString("D4");
        if (this.type == 0)
          this.sound = this.application.content.Load<SoundEffect>(assetName);
        else
          this.song = this.application.content.Load<Song>(assetName);
      }

      public void play(int nl, bool bPrio, float v, float p)
      {
        this.nLoops = nl;
        if (this.nLoops == 0)
          this.nLoops = 10000000;
        if (this.type == 0)
        {
          if (this.soundInstance != null)
          {
            this.soundInstance.Stop();
            this.soundInstance.Dispose();
            this.soundInstance = (SoundEffectInstance) null;
          }
          if (this.soundInstance == null)
            this.soundInstance = this.sound.CreateInstance();
          if (this.soundInstance == null)
            return;
          this.soundInstance.Volume = v / 100f;
          this.soundInstance.Pan = p / 100f;
          this.soundInstance.Play();
          this.bUninterruptible = bPrio;
        }
        else
        {
          if (!MediaPlayer.GameHasControl)
            return;
          MediaPlayer.Stop();
          MediaPlayer.Play(this.song);
          this.bSongPlaying = true;
          this.bPaused = false;
          this.timer = this.application.timer + (long) this.getDuration();
        }
      }

      public void stop()
      {
        if (this.type == 0)
        {
          if (this.soundInstance == null)
            return;
          this.soundInstance.Stop();
          this.soundInstance.Dispose();
          this.soundInstance = (SoundEffectInstance) null;
          this.bUninterruptible = false;
        }
        else
        {
          if (!MediaPlayer.GameHasControl)
            return;
          MediaPlayer.Stop();
          this.bSongPlaying = false;
          this.bUninterruptible = false;
        }
      }

      public void setVolume(int v)
      {
        if (this.type == 0)
        {
          if (this.soundInstance == null)
            return;
          this.soundInstance.Volume = (float) v / 100f;
        }
        else
        {
          if (!MediaPlayer.GameHasControl)
            return;
          MediaPlayer.Volume = (float) v / 100f;
        }
      }

      public void setPan(int p)
      {
        if (this.type != 0 || this.soundInstance == null)
          return;
        this.soundInstance.Pan = (float) p / 100f;
      }

      public void setFrequency(int newFrequency)
      {
        double num1 = (double) newFrequency / (double) this.frequency;
        double num2 = Math.Max(Math.Min(num1 < 1.0 ? num1 * 2.0 - 2.0 : num1 - 1.0, 1.0), -1.0);
        if (this.soundInstance == null)
          return;
        this.soundInstance.Pitch = (float) num2;
      }

      public int getFrequency() => this.frequency;

      public void pause()
      {
        if (this.type == 0)
        {
          if (this.soundInstance == null)
            return;
          this.soundInstance.Pause();
        }
        else
        {
          if (!MediaPlayer.GameHasControl)
            return;
          MediaPlayer.Pause();
          this.bPaused = true;
        }
      }

      public void resume()
      {
        if (this.type == 0)
        {
          if (this.soundInstance == null)
            return;
          this.soundInstance.Resume();
        }
        else
        {
          if (!MediaPlayer.GameHasControl)
            return;
          MediaPlayer.Resume();
          this.bPaused = false;
        }
      }

      public bool isPaused()
      {
        if (this.type != 0)
          return this.bPaused;
        return this.soundInstance != null && this.soundInstance.State == SoundState.Paused;
      }

      public bool isPlaying()
      {
        if (this.type == 0)
        {
          if (this.soundInstance != null && this.soundInstance.State == SoundState.Playing)
            return true;
        }
        else if (MediaPlayer.GameHasControl && MediaPlayer.State == MediaState.Playing)
          return true;
        return false;
      }

      public int getDuration()
      {
        TimeSpan timeSpan = this.type != 0 ? this.song.Duration : this.sound.Duration;
        return timeSpan.Hours * 60 * 60 * 1000 + timeSpan.Minutes * 60 * 1000 + timeSpan.Seconds * 1000 + timeSpan.Milliseconds;
      }

      public bool checkSound()
      {
        if (this.type == 0)
        {
          if (this.soundInstance != null && this.soundInstance.State == SoundState.Stopped)
          {
            if (this.nLoops > 0)
            {
              --this.nLoops;
              if (this.nLoops > 0)
              {
                this.soundInstance.Play();
                return false;
              }
            }
            this.bUninterruptible = false;
            this.soundInstance.Dispose();
            this.soundInstance = (SoundEffectInstance) null;
            return true;
          }
        }
        else if (this.bSongPlaying && this.application.timer >= this.timer && MediaPlayer.State != MediaState.Playing && !this.bPaused)
        {
          if (this.nLoops > 0)
          {
            --this.nLoops;
            if (this.nLoops > 0)
            {
              MediaPlayer.Play(this.song);
              this.timer = this.application.timer + (long) this.getDuration();
              return false;
            }
          }
          this.bUninterruptible = false;
          return true;
        }
        return false;
      }
    }
}
