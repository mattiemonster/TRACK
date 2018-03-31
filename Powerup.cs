using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TRACK
{
    public class Powerup
    {
        public enum PowerupType {plusSpeed=1, resetSpeed=2 };

        public int x = 0, y = 0;
        public Texture2D tex;
        public SoundEffect sound;
        public Color color = Color.Green;
        public Color lerpTo = Color.White;
        public float lerpAmount = 0.0f, lerpSpeed = 0.9f;
        public bool isDone;
        public Rectangle bb;

        public Powerup(int x, int y, PowerupType powerup)
        {
            this.x = x;
            this.y = y;
            bb = new Rectangle(x, y, tex.Width, tex.Height);
            if (powerup == PowerupType.plusSpeed)
            {
                tex = Values.plusOneSpeed;
                color = Color.Green;
            }
        }

        public void Lerp()
        {
            if (lerpAmount < 1.0f)
            {
                color = Color.Lerp(color, lerpTo, lerpSpeed);
                lerpAmount += lerpSpeed;
            }
            else
            {
                Values.plusOneSound.Play(1f, (float)Game1.r.NextDouble(), 1f);
                isDone = true;
            }
        }

        //public void Update()
        //{
        //}

        public void Render(SpriteBatch batch) => batch.Draw(tex, bb, color);

    }
}
