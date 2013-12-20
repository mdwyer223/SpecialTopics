using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class MapDataHolderVars
    {
        public List<BaseSpriteVars> everything;

        public List<BaseSpriteVars> objects;
        public List<BaseSpriteVars> iObjects;
        public List<BaseSpriteVars> mObjects;
        //public List<SpikeTrap> spikeTraps;
        //public List<ArrowTrap> arrowTraps;

        public List<BaseSpriteVars> enemies;
        public List<BaseSpriteVars> npcs;

        //public List<TripWire> tripWires;
        //public List<Chest> chests;

        public Texture2D background;
        public Rectangle backgroundRec;
        public Vector2 position;

        public Song music;

        public string backgroundDirectory;

        //public Type type;

        private MapDataHolderVars()
        {
        }
        
        public MapDataHolderVars(MapDataHolder inMDH)
        {
            //type = inMDH.GetType();
            everything = new List<BaseSpriteVars>();
            for (int i = 0; i < inMDH.Everything.Count; i++)
            {
                everything.Add(new BaseSpriteVars(inMDH.Everything[i]));
            }

            objects = new List<BaseSpriteVars>();
            for (int i = 0; i < inMDH.AllObjects.Count; i++)
            {
                objects.Add(new BaseSpriteVars(inMDH.AllObjects[i]));
            }

            iObjects = new List<BaseSpriteVars>();
            for (int i = 0; i < inMDH.ImmovableObjects.Count; i++)
            {
                iObjects.Add(new BaseSpriteVars(inMDH.ImmovableObjects[i]));
            }

            mObjects = new List<BaseSpriteVars>();
            for (int i = 0; i < inMDH.MovableObjects.Count; i++)
            {
                mObjects.Add(new BaseSpriteVars(inMDH.MovableObjects[i]));
            }
            // spikeTraps
            // arrowTraps

            enemies = new List<BaseSpriteVars>();
            for (int i = 0; i < inMDH.Enemies.Count; i++)
            {
                enemies.Add(new BaseSpriteVars(inMDH.Enemies[i]));
            }
            // npcs
            // trip wires

            backgroundDirectory = inMDH.BackgroundDirectory;
            backgroundRec.Width = inMDH.BackgroundRec.Width;
            backgroundRec.Height = inMDH.BackgroundRec.Height;
            position = inMDH.Position;
            // music

        }

    }
}
