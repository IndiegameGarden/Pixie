using System;
using Microsoft.Xna.Framework;

namespace Pixie1.Actors
{
    /// <summary>
    /// the motion controller (downward fall) of the missile
    /// </summary>
    public class BombMissileControl : ThingControl
    {
        public BombMissileControl()
        {
        }

        protected override void OnNextMove()
        {
            base.OnNextMove();
            TargetMove = new Vector2(0, 1f);
            IsTargetMoveDefined = true;
        }

        protected override void OnUpdate(ref TTengine.Core.UpdateParams p)
        {
            base.OnUpdate(ref p);
            AllowNextMove();
        }

    }

    /// <summary>
    /// the missile that is launched by the BombToy
    /// </summary>
    public class BombMissile: Thing
    {
        BombMissileControl control;

        public BombMissile(): base()
        {
            control = new BombMissileControl();
            DrawInfo.DrawColor = Color.DarkGoldenrod;
        }

        protected override void OnNewParent()
        {
            base.OnNewParent();
            Add(control);
        }
    }
}
