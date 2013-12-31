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

namespace Game1
{
    /// <summary>
    /// Factory to create new game-specific entities
    /// </summary>
    public class PixieFactory
    {
        private PixieFactory(PixieGame game)
        {
            _game = game;
        }

        private static PixieFactory _instance = null;
        private PixieGame _game;
        protected Random rnd = new Random();

        public static PixieFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PixieFactory(TTGame.Instance as PixieGame);
                return _instance as PixieFactory;
            }
        }

        public Entity CreatePixie()
        {
            Entity e = TTFactory.CreateSpritelet("pixie");
            e.AddComponent(new UserControlComp());
            e.AddComponent(new TargetMotionComp());
            return e;
        }

        /// <summary>
        /// create a ball Spritelet that can be scaled
        /// </summary>
        /// <param name="radius">the relative size scaling, 1 is normal</param>
        /// <returns></returns>
        public Entity CreateBall(double radius)
        {
            Entity e = TTFactory.CreateSpritelet("paul-hardman_circle-four");
            e.AddComponent(new ScaleComp(radius));
            return e;
        }

        /// <summary>
        /// create an active ball with given position and random velocity and some weird (AI) behaviors
        /// </summary>
        /// <returns></returns>
        public Entity CreateHyperActiveBall(Vector2 pos)
        {
            var ball = CreateBall(0.08f + 0.07f * (float)rnd.NextDouble());

            // position and velocity set
            ball.GetComponent<PositionComp>().Position2D = pos;
            ball.GetComponent<VelocityComp>().Velocity = 0.2f * new Vector3((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f, 0f);

            ball.Refresh();
            return ball;

        }

        public Entity CreateMovingTextlet(Vector2 pos, string text)
        {
            var t = TTFactory.CreateTextlet(text);
            t.GetComponent<PositionComp>().Position2D = pos;
            t.GetComponent<DrawComp>().DrawColor = Color.Black;
            t.GetComponent<VelocityComp>().Velocity = 0.2f * new Vector3((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f, 0f);
            t.GetComponent<ScaleComp>().Scale = 0.5;
            return t;
        }

        public void MyScaleModifier(Entity entity, double value)
        {
            entity.GetComponent<ScaleComp>().ScaleModifier *= 0.5 + entity.GetComponent<PositionComp>().Position.X;
        }

        public void MyScaleModifier2(ScaleComp sc, double value)
        {
            sc.ScaleModifier *= value;
        }

        public void MyRotateModifier(DrawComp drawComp, double value)
        {
            drawComp.DrawRotation = (float)value;
        }
    }
}
