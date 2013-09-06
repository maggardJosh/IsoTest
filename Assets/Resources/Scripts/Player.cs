using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public class Player : FIsoSprite
	{

        public Player(FTilemap tilemap) : base ("jif", tilemap)
        {
            addAnimation(new FAnimation("idleLeftDown", new int[] { 0 }, 100, true));
            addAnimation(new FAnimation("idleRightDown", new int[] { 0 }, 100, true));
            addAnimation(new FAnimation("idleLeftUp", new int[] { 0 }, 100, true));
            addAnimation(new FAnimation("idleRightUp", new int[] { 0 }, 100, true));

            addAnimation(new FAnimation("runLeftDown", new int[] { 0 }, 100, true));
            addAnimation(new FAnimation("runRightDown", new int[] { 0 }, 100, true));
            addAnimation(new FAnimation("runLeftUp", new int[] { 0 }, 100, true));
            addAnimation(new FAnimation("runRightUp", new int[] { 0 }, 100, true));

            play("idleLeftDown");
        }
        float descentSpeed = 100;
        public override void Update()
        {
            if (offGroundHeight > 0)
                offGroundHeight -= descentSpeed * UnityEngine.Time.deltaTime;
            base.Update();
        }
	}

