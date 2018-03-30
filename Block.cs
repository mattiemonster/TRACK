using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using C3.MonoGame;

namespace TRACK
{
    public class Block
    {
        public int x = 0, y = 0;
        public float lerpSpeed = 0.05f, lerpAmount = 0f;
        public bool isDone = false;
        public Rectangle bb;
        public Color color;
        public Color fadeToColor = Color.White;
        public Random r;

        public Block(int x, int y, float lerpSpeed)
        {
            this.x = x;
            this.y = y;
            this.lerpSpeed = lerpSpeed;
            bb = new Rectangle(x, y, 40, 40);
            SetColours();
        }

        void SetColours()
        {
            r = new Random();
            int startColorBase = r.Next(1, 4);
            if (startColorBase == 1)
            {
                color = Color.Green;
            }

            if (startColorBase == 2)
            {
                color = Color.Red;
            }

            if (startColorBase == 3)
            {
                color = Color.Blue;
            }
        }

        public void Lerp()
        {
            if (lerpAmount < 1.0f)
            {
                color = Color.Lerp(color, fadeToColor, lerpSpeed);
                lerpAmount += lerpSpeed;
            } else
            {
                isDone = true;
            }
        }

        public void Update()
        {
            bb.X = x;
            bb.Y = y;
        }

        public void Render(SpriteBatch batch) => batch.FillRectangle(x, y, 40, 40, color);

    }
}
