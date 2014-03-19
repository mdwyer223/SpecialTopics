using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public enum ItemType { ARROW, CURRENCY, POTION, CRAP };

    public struct ItemStruct
    {
        public int price;
        public bool isStackable;
        public ItemType type;

    }

    public struct GemStruct
    {
        public float duration;
        public int damage;
        public float chance;
        public GemType type;
    }

    public struct SwordStruct
    {
        public GemStruct[] gemsData;
        public int maxGems;

        public int damage;
        public int range;
        public float speed;
    }

    public struct BowStruct
    {
        public GemStruct[] gemsData;
        public int maxGems;

        public int damage;
        public int range;
        public float speed;
    }

    public struct BombStruct
    {
        public GemStruct[] gemsData;
        public int maxGems;

        public int damage;
        public int range;
        public float speed;
    }

    public class MapDataStruct
    {
        public string mapId;

        public string backgroundPath;
        public BaseSpriteStruct[] everythingData;
    }

    public struct MapStruct
    {
        public CharacterStruct playerData;
        public MapDataStruct mapData;
    }
    
    public struct BaseSpriteStruct
    {
        public string saveId;

        public Rectangle drawnRec, collisionRec;
        public Vector2 position;
        public Color color;
        public int speed;
        public int invisRecWidth;
        public int widthDifference;
        public bool isDead, isVisible;

        public bool canMoveRight, canMoveDown, canMoveLeft, canMoveUp;
    }

    public struct CharacterStruct
    {
        public BaseSpriteStruct BaseStruct;
        public ItemStruct[] inventData;
        public SwordStruct swordData;
        public BowStruct bowData;
        public BombStruct bombData;
        public int maxHP;
        public int currentHP;
        public int currency;
        public int maxBombs, maxArrows, arrowCount, bombCount;
    }
}
