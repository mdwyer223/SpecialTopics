﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Skeleton : Enemy
    {
        protected SoundEffect damaged;

        public Skeleton(Texture2D defaultTexture, float scaleFactor, int SecondsToCrossScreen, Vector2 startPosition)
            : base(defaultTexture, scaleFactor, SecondsToCrossScreen, startPosition)
        {
            ContentManager content = Game1.GameContent;

            // load in animations
            Texture2D[] left, right, forward, backward;

            left = new Texture2D[3];
            right = new Texture2D[3];
            forward = new Texture2D[3];
            backward = new Texture2D[3];

            string moveDirec = "ComputerPpl/Enemies/Skeleton/Move/";
            string attcDirec = "ComputerPpl/Enemies/Skeleton/Attack/";

            backward[0] = content.Load<Texture2D>(moveDirec + "SB");
            backward[1] = content.Load<Texture2D>(moveDirec + "SBR");
            backward[2] = content.Load<Texture2D>(moveDirec + "SBL");

            forward[0] = content.Load<Texture2D>(moveDirec + "SF");
            forward[1] = content.Load<Texture2D>(moveDirec + "SFR");
            forward[2] = content.Load<Texture2D>(moveDirec + "SFL");

            left[0] = content.Load<Texture2D>(moveDirec + "SL");
            left[1] = content.Load<Texture2D>(moveDirec + "SLR");
            left[2] = content.Load<Texture2D>(moveDirec + "SLL");

            right[0] = content.Load<Texture2D>(moveDirec + "SR");
            right[1] = content.Load<Texture2D>(moveDirec + "SRR");
            right[2] = content.Load<Texture2D>(moveDirec + "SRL");
            move = new FullAnimation(backward, forward, left, right, .2f);

            left = new Texture2D[1];
            right = new Texture2D[1];
            forward = new Texture2D[1];
            backward = new Texture2D[1];

            forward[0] = content.Load<Texture2D>(moveDirec + "SF");
            backward[0] = content.Load<Texture2D>(moveDirec + "SB");
            left[0] = content.Load<Texture2D>(moveDirec + "SL");
            right[0] = content.Load<Texture2D>(moveDirec + "SR");
            idle = new FullAnimation(backward, forward, left, right, .2f);

            left = new Texture2D[2];
            right = new Texture2D[2];
            forward = new Texture2D[2];
            backward = new Texture2D[2];

            forward[0] = content.Load<Texture2D>(attcDirec + "SFA");
            forward[1] = content.Load<Texture2D>(attcDirec + "SFA2");
            backward[0] = content.Load<Texture2D>(attcDirec + "SBA");
            backward[1] = content.Load<Texture2D>(attcDirec + "SBA2");
            left[0] = content.Load<Texture2D>(attcDirec + "SLA");
            left[1] = content.Load<Texture2D>(attcDirec + "SLA2");
            right[0] = content.Load<Texture2D>(attcDirec + "SRA");
            right[1] = content.Load<Texture2D>(attcDirec + "SRA2");
            attackAn = new FullAnimation(backward, forward, left, right, .2f);

            damaged = Game1.GameContent.Load<SoundEffect>("Music/SFX/Hit Enemy");

            this.strength = 10;
        }

        public override void damage(MapDataHolder data, int hit)
        {
            if (immunityTimer >= (IMMUNITY_TIME * 1000))
            {
                hitPoints -= hit;
                addMessage(new Message("" + hit, Color.Red));
                if (hitPoints <= 0)
                {
                    hitPoints = 0;
                    dropItem(data);
                    IsDead = true;
                    IsVisible = false;
                }
                damaged.Play();
                immunityTimer = 0;
            }

            this.knockBack();
        }
    }
}
