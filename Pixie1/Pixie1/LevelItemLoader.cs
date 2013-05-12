using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    /// <summary>
    /// loads items (Toys, Things) for a level based on a bitmap that determines positions of stuff.
    /// all things are indicated by single pixels.
    /// </summary>
    public class LevelItemLoader: LevelBackground
    {
        public LevelItemLoader(string bitmapFile)
            : base(bitmapFile)
        {
        }

        public void AddItems(Level level, Color col, Type itemClass) {
            for (int x = 0; x < Texture.Width; x++)
            {
                for (int y = 0; y < Texture.Height; y++)
                {
                    Color[] data = new Color[1];
                    Texture.GetData<Color>(0, new Rectangle(x,y, 1, 1), data, 0, 1);
                    if (data[0].Equals(col))
                    {
                        Thing item = (Thing)itemClass.InvokeMember("Create", BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, null);
                        item.PositionAndTarget = new Vector2(x, y);
                        level.Add(item);
                    }
                }
            }
        }
    }
}
