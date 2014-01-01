using System;
using Microsoft.Xna.Framework;
using TTengine.Core;

namespace Game1.Actors
{
    /// <summary>
    /// the motion controller (downward fall) of the missile
    /// </summary>
    public class BombMissileControl : ThingControl
    {
        public BombMissileControl()
        {
            MoveDeltaTime = 5f;
        }

        protected override void OnNextMove()
        {
            base.OnNextMove();
            TargetMove = BombMissile.MISSILE_DIRECTION;
            IsTargetMoveDefined = true;
        }

    }

    /// <summary>
    /// the missile that is launched by the BombToy
    /// </summary>
    public class BombMissile: ThingComp
    {
        public static Vector2 MISSILE_DIRECTION = new Vector2(0, 1f);
        public int ExplosionRange = 5;
        public bool IsExploding = false;

        BombMissileControl control;

        public BombMissile(): base()
        {
            IsCollisionFree = false;
            Velocity = 5f;
            control = new BombMissileControl();
            DrawInfo.DrawColor = Color.DarkGoldenrod;
        }

        protected override void OnNewParent()
        {
            base.OnNewParent();
            Add(control);
        }

        public void StartExplode()
        {
            IsExploding = true;
            Duration = SimTime + 3.0f; // explosion duration
        }

        void Explode()
        {
            Vector2 pos = Target;
            int posX = (int) Math.Round(pos.X);
            int posY = (int) Math.Round(pos.Y);
            LevelBackgroundComp bg = Level.Current.Background;
            Vector2 pixPos;
            for (int x = posX - ExplosionRange; x <= posX + ExplosionRange; x++)            
            {
                for (int y = posY - ExplosionRange; y <= posY + ExplosionRange; y++)
                {
                    pixPos.X = x;
                    pixPos.Y = y;
                    bg.SetPixel(pixPos, RandomMath.RandomColor());
                }
            }
        }

        void OnExploding()
        {
            Vector2 pos = Target;
            LevelBackgroundComp bg = Level.Current.Background;
            Vector2 pixPos;
            pixPos.X = pos.X + RandomMath.RandomBetween(-ExplosionRange, ExplosionRange);
            pixPos.Y = pos.Y + RandomMath.RandomBetween(-ExplosionRange, ExplosionRange); 
            bg.SetPixel(pixPos, RandomMath.RandomColor());
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            if (!IsExploding && TargetMove.LengthSquared() > 0) {
                if (CollidesWithSomething(TargetMove))
                {
                    // EXPLODE !!!
                    StartExplode();
                }
            }

            if (IsExploding)
            {
                OnExploding();
            }
        }
    }
}
