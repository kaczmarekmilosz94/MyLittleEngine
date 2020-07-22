using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleEngineV8
{
    public class Audio : Component
    {
        System.Media.SoundPlayer player;

        public bool PlayOnStart;

        public string AudioPath;

        public float Volume;

        public void SetPlayer()
        {
            player = new System.Media.SoundPlayer(AudioPath);

            if (PlayOnStart) Play();
        }

        public void Play()
        {
            player.Play();
        }

        public void Stop()
        {
            player.Stop();
        }
    }
}
