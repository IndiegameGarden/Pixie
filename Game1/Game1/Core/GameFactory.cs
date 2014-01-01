using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Artemis;
using Artemis.Interface;
using TreeSharp;
using TTengine.Core;
using TTengine.Comps;
using TTengine.Behaviors;
using TTengine.Modifiers;
using TTengine.Util;

using Game1.Comps;

namespace Game1.Core
{
    /// <summary>
    /// Factory to create new game-specific entities
    /// </summary>
    public class GameFactory
    {
        private GameFactory(PixieGame game)
        {
            _game = game;
        }

        private static GameFactory _instance = null;
        private PixieGame _game;
        protected Random rnd = new Random();

        public static GameFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameFactory(TTGame.Instance as PixieGame);
                return _instance as GameFactory;
            }
        }

        public Entity CreatePixie()
        {
            Entity e = TTFactory.CreateSpritelet("pixie");
            e.AddComponent(new UserControlComp());
            e.AddComponent(new TargetMotionComp());
            return e;
        }

        protected static Random rnd = new Random();

        public Entity CreateThing(ThingType tp, bool hasControls, string bitmap)
        {
            var e = TTFactory.CreateSpritelet(bitmap);
            var sc = e.GetComponent<SpriteComp>();
            var tc = new ThingComp(tp, Level.Current.Background, sc.Texture);
            e.AddComponent(tc);
            if (hasControls)
            {
                var tcc = new ThingControlComp();
                e.AddComponent(tcc);
            }
            tc.PassableIntensityThreshold = Level.Current.DefaultPassableIntensityThreshold;
            var textureData = new Color[tc.BoundingRectangle.Width * tc.BoundingRectangle.Height];
            sc.Texture.GetData(textureData);
            sc.Center = Vector2.Zero;

            return e;
        }

        public Entity CreateThing(ThingType tp, bool hasControls)
        {
            return CreateThing(tp, hasControls, "pixie");
        }

        public ColorCycleComp CreateColorCycling(float cyclePeriod, Color minColor, Color maxColor)
        {
            ColorCycleComp cycl = new ColorCycleComp(cyclePeriod);
            cycl.minColor = minColor;
            cycl.maxColor = maxColor;
            return cycl;
        }

        public Entity CreateSubtitle(string text, Color color)
        {
            var e = CreateSubtitle(new SubtitleText(text));
            e.GetComponent<DrawComp>().DrawColor = color;
            return e;
        }

        public Entity CreateSubtitle(SubtitleText stComp)
        {
            var e = TTFactory.CreateDrawlet();
            e.AddComponent(stComp);
            e.Refresh();
            return e;
        }

        /*
        public Entity CreateLevelet(Level lev)
        {
            var e = TTFactory.CreateDrawlet();
            e.AddComponent(new ScaleComp());
            e.AddComponent(new ThingComp(ThingType.OTHER, null, lev.Background.Texture));
            e.AddComponent(new ScriptComp());
            e.AddComponent(lev.Background);
            e.Refresh();
            return e;
        }
         */
        
    }
}
