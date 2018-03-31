using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRACK
{
    public class PlusOne
    {

        public int x = 0, y = 0;
        public Texture2D tex = Values.plusOne;
        public Color color = Color.Green;
        public Color lerpTo = Color.White;
        public float lerpAmount = 0.0f, lerpSpeed = 0.9f;
        public bool isDone;
        public Rectangle bb;

        public PlusOne(int x, int y)
        {
            this.x = x;
            this.y = y;
            bb = new Rectangle(x, y, 32, 32);
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

        public void Update()
        {
            bb.X = x;
            bb.Y = y;
        }

        public void Render(SpriteBatch batch) => batch.Draw(tex, bb, color);

    }
}
