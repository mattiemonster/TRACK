using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace TRACK
{
    class Values
    {
        public static Texture2D crosshair;
        public static SpriteFont titleFont;
        public static SpriteFont descriptionFont;
        public static String gameName = "TRACK";
        public static String gameOver = "GAME OVER!";
        public static Vector2 gameNamePos = new Vector2(20, 15);
        public static String gameText1 = "Hover over squares to remove them (they will turn white slowly and will disappear when removed)";
        public static Vector2 gameText1Pos = new Vector2(20, 45);
        public static String gameText1p2 = "If a square is not removed after 5 seconds then it's game over. Press space to begin";
        public static Vector2 gameText1p2Pos = new Vector2(20, 60);
        public static String gameText2 = "Press escape at any time to return to the menu, this will not save your score";
        public static Vector2 gameText2Pos = new Vector2(20, 75);
        public static String creditsText = "Created by Mattie. Uses Primitives2D Monogame class by John McDonald, license in game files.";

        public static void LoadContent(ContentManager cm)
        {
            crosshair = cm.Load<Texture2D>("Textures/Crosshair");
            titleFont = cm.Load<SpriteFont>("Fonts/TitleFont");
            descriptionFont = cm.Load<SpriteFont>("Fonts/DescriptionFont");
        }

        public static void UnloadContent()
        {
            crosshair.Dispose();
        }
    }
}
