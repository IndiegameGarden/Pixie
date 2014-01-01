using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Artemis;
using Artemis.System;
using Artemis.Manager;
using Artemis.Attributes;
using TTengine.Core;
using TTengine.Comps;
using Game1.Core;
using Game1.Comps;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 2)]
    public class LevelSystem : EntityComponentProcessingSystem<DrawComp,PositionComp,LevelBackgroundComp>
    {
        protected double dt = 0;
        protected ScreenComp activeScreen = null;

        protected override void Begin()
        {
            dt = TimeSpan.FromTicks(EntityWorld.Delta).TotalSeconds;
            activeScreen = TTGame.Instance.DrawScreen;
        }

        
        public override void Process(Entity entity, DrawComp drawComp, PositionComp posComp, LevelBackgroundComp lbc)
        {
            // update drawpos
            drawComp.DrawPosition = activeScreen.ToPixels(posComp.Position + posComp.PositionModifier);
            lbc.DrawCenter = activeScreen.ToPixels(lbc.Center); // TODO check

            TTSpriteBatch sb = activeScreen.SpriteBatch;

            // draw sprite
            sb.Draw(lbc.Texture, drawComp.DrawPosition, null, drawComp.DrawColor,
                drawComp.DrawRotation, lbc.DrawCenter, drawComp.DrawScale, SpriteEffects.None, drawComp.LayerDepth);

        }
    }
}
