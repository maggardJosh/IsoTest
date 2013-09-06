using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
        public void jump()
        {
            if(offGroundHeight <= 0)
            yVel = 300;
        }
        float yVel = 0;
        float maxDescentSpeed = -400;
        float gravity = 600;

        Vector2 velocity = Vector2.zero;
        public override void Update()
        {

            if ( yVel < 0 && offGroundHeight <= 0)
            {
                yVel = 0;
            }
            else
            {
                yVel -= gravity * UnityEngine.Time.deltaTime;
                yVel = Math.Max(maxDescentSpeed, yVel);
            }


                this.isoX += velocity.x * UnityEngine.Time.deltaTime;
            this.isoY += velocity.y * UnityEngine.Time.deltaTime;
            this.offGroundHeight += yVel * UnityEngine.Time.deltaTime;

            velocity *= .8f;
            base.Update();
        }
	}

