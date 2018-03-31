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
        public int x = 0, y = 0;
        public Texture2D tex = Values.plusOne;
        public Color color = Color.Green;
        public Color lerpTo = Color.White;
        public float lerpAmount = 0.0f, lerpSpeed = 0.9f;
        public bool isDone;
        public Rectangle bb;
    }
}
