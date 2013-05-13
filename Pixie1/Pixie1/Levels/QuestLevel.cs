using System;
using System.Collections.Generic;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pixie1;
using Pixie1.Actors;
using Pixie1.Toys;

namespace Pixie1.Levels
{
    /// <summary>
    /// a level featuring a castle
    /// </summary>
    public class QuestLevel : Level
    {
        Vector2 PRINCESS_POSITION = new Vector2(557f, 281f);

        Color LEVEL_FOREGROUND_COLOR = new Color(231, 231, 248);
        Color ITEM_BLOCK_COLOR = new Color(179, 102, 27); // 179,102,27 brown: block

        int numberOfZoomOuts = 0;

        public QuestLevel()
            : base()
        {           
            // Level settings
            SCREEN_MOTION_SPEED = 8.0f;
            DEFAULT_SCALE = 15f;// 15f;
            PIXIE_STARTING_POS = new Vector2(42f, 155f); // in pixels        
            //PIXIE_STARTING_POS = PRINCESS_POSITION + new Vector2(-10f,-10f); // debug
            //PIXIE_STARTING_POS += new Vector2(200f, 4f); // debug
            //PIXIE_STARTING_POS = new Vector2(73f, 10f); // debug
            BG_STARTING_POS = new Vector2(30f, 155f); // in pixels; bg=background            
            //BG_STARTING_POS = PRINCESS_POSITION; // debug
            //BG_STARTING_POS += new Vector2(200f, 4f); // debug
            //PIXIE_STARTING_POS = new Vector2(188f, 0f); // debug, close to win pos
            //BG_STARTING_POS = new Vector2(188f, 0f); // debug
        }

        protected override void InitLevel()
        {
            base.InitLevel();

            // select bitmap bg
            Background = new LevelBackground("Level1.png");
            Background.ForegroundColor = LEVEL_FOREGROUND_COLOR;
            Background.TargetSpeed = SCREEN_MOTION_SPEED;
            Add(Background);
            Background.Target = PIXIE_STARTING_POS;
            Background.Position = BG_STARTING_POS;

            // bitmap for things/items to load
            ItemsMap = new LevelItemLoader("Level1Items.png");            
            ItemsMap.AddItems(this, ITEM_BLOCK_COLOR, typeof(Block));
        }

        protected override void InitBadPixels()
        {
            base.InitBadPixels();
            
            // boss
            Add(new Boss());

            for (int i = 0; i < 249; i++)
            {
                RedGuard bp = RedGuard.Create(); // Cloaky();
                bp.PositionAndTarget = new Vector2(RandomMath.RandomBetween(123f,720f), RandomMath.RandomBetween(9f,290f) );
                //bp.TargetSpeed = 18.0f; // TODO
                Add(bp);
                FindWalkableGround(bp);
            }

            for (int i = 0; i < 40; i++)
            {
                Servant s = Servant.Create();
                s.PositionAndTarget = new Vector2(RandomMath.RandomBetween(140f, 720f), RandomMath.RandomBetween(9f, 290f));
                Add(s);
                FindWalkableGround(s);
            }

            for (int i = 0; i < 14; i++) // XIV companions!
            {
                Companion cp = Companion.Create(); // Cloaky();
                cp.PositionAndTarget = new Vector2(RandomMath.RandomBetween(PIXIE_STARTING_POS.X - 10f, PIXIE_STARTING_POS.X + 10f), RandomMath.RandomBetween(PIXIE_STARTING_POS.Y - 6f, PIXIE_STARTING_POS.Y + 6f));
                //bp.TargetSpeed = 18.0f; // TODO
                Add(cp);
                pixie.Companions.Add(cp);
                FindWalkableGround(cp);
            }

        }

        protected void FindWalkableGround(Thing t)
        {
            // move down until on walkable area
            while(t.CollidesWithSomething(Vector2.Zero)) {
                t.PositionY += 1;
                // when unit goes off-screen, delete.
                if (t.PositionY > this.Background.Texture.Height)
                {
                    t.Delete = true;
                    break;
                }
            }
        }

        protected override void InitToys()
        {
            base.InitToys();
            Vector2 p;
            Toy t;

            InitToy(new ZoomOutToy(), 80, 144);
            InitToy(new ZoomOutToy(), 211,140);
            InitToy(new ZoomOutToy(), 413,173);
            InitToy(new PaintToy(), 309,143);
            InitToy(new PaintToy(), 307, 274);
            //InitToy(new PaintToy(), 33,154); // debug
            //InitToy(new InvisibilityToy(), 37, 154); // debug
            //InitToy(new SpeedModifyToy(2f), 41, 154); // debug
            InitToy(new InvisibilityToy(), 463,296);
            InitToy(new InvisibilityToy(), 173, 65);
            InitToy(new SpeedModifyToy(2f), 172,230);
            InitToy(new SpeedModifyToy(2f), 450, 291);
            InitToy(new ZoomOutToy(), 560,57);
        }

        protected void InitToy(Toy t, int x, int y)
        {
            t.PositionAndTarget = new Vector2(x,y);
            Add(t);
        }

        protected override void InitLevelSpecific()
        {
            Music = new GameMusic();
            Sound = new GameSound();
            Add(Music);
            Add(Sound);

            // princess
            Princess p = new Princess();
            p.PositionAndTarget = PRINCESS_POSITION;
            //p.PositionAndTarget = new Vector2(90f,158f); // debug
            Add(p);

            SubtitleText t = new SubtitleText();
            t.AddText("COMPANIONS!", 4f);
            t.AddText("Follow me! Together, we will rescue\nthe Princess!", 4f);            
            t.AddText("Squash those Red Guards...!", 4f);
            Subtitles.Show(0, t);
            t.StartTime = 1f;
            //t.DrawInfo.DrawColor = Color.LightCoral;

            t = new SubtitleText();
            t.AddText("Quest for the Pixel Princess XIV", 5f);
            t.AddText("You, Galad the Golden, must rescue the Pink Princess once more...", 5f);
            t.AddText("...from the clutches of the evil Red Guard.", 5f);
            t.AddText("The Fourteen Cobalt Companions have come to your aid!", 5f);
            t.AddText("The princess was taken into Red's Keep.", 5f);
            t.AddText("Get her out, valiant knight!", 5f);
            t.AddText("", 1f);
            t.AddText("Arrows / WASD = move, SPACE = Companion's attack, X = Use Spell", 5f);
            t.AddText("", 1f);
            t.AddText("Coding & art by Indiegame Garden (IndiegameGarden.com)", 4f);
            t.AddText("Sounds by Jute and artisticdude (opengameart.org)", 4f);
            t.AddText("Music by you-may-know-who (guess!)", 4f);
            t.AddText("Sprite by Charles Gabriel (opengameart.org)", 4f);
            t.AddText("FMOD Audio engine (c) Firelight Technologies 2004-2013 (fmod.org)", 4f);
            
            Parent.Add(t);
            t.ScaleVector = new Vector2(1f, 1f);
            t.Motion.Scale = 0.5f ;
            t.Motion.Position = new Vector2(Screen.Center.X,0.08f);
            //t.DrawInfo.Center = Vector2.Zero;
            t.StartTime = 13f;
        }

        protected override bool ScreenBorderHit()
        {
            if (numberOfZoomOuts < 0)
            {
                numberOfZoomOuts++;
                Motion.Scale /= 2.0f;
                //Motion.ScaleTarget /= 2.0f;
                //Motion.ScaleSpeed = 0.2f;
                return false;
            }
            return true;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            // adapt scroll speed to how fast pixie goes
            Background.TargetSpeed = SCREEN_MOTION_SPEED * pixie.Velocity;
        }
    }
}
