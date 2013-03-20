#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion 

namespace FourGuns
{
    public class Zombie : Enemy
    {
        public static Random rnd;
        public PriorityQueue<Player, float> whoToChase;
        public float largestDistance;

        public Zombie() { }

        public Zombie(ContentManager theContentManager, Vector2 position)
            : base(theContentManager, position)
        {
            speedMod = .5f;
            aiming = false;
            aimingDirection = new Vector2(2, 0);
            sprite = theContentManager.Load<Texture2D>("Sprites/Enemy/ZombieSprite");
            spriteWidth = 64;    // the width of one sprite is 64 px
            spriteHeight = 64;  // the height of one sprite is 64 px
            maxHealth = 50;
            Health = maxHealth;
            meleeDamage = 1;
            rnd = new Random();
            largestDistance = 5000f;
            whoToChase = new PriorityQueue<Player, float>();
        }

        public override void Update(GameTime gametime, List<Player> players)
        {
            foreach (Player player in players)
            {
                if (player.ActivePlayer)
                {
                    if (!whoToChase.exists(player) && Vector2.Distance(player.Position, position) < largestDistance)
                    {
                        whoToChase.Enqueue(player, Vector2.Distance(player.Position, position));
                    }
                    else if (whoToChase.exists(player))
                    {
                        whoToChase.Update(player, Vector2.Distance(player.Position, position));
                    }
                }
            }

            // If there is no player to follow, stand still
            if (whoToChase.Count == 0 || Vector2.Distance(whoToChase.Peek().Position, position) > largestDistance) speed = Vector2.Zero;
            else 
            {
                speed = 3 * Vector2.Normalize((whoToChase.Peek().Position - position));
            }
            // If this enemy is moving, animate them
            if (speed != Vector2.Zero) moving = true;
            else moving = false;

            base.Update(gametime);
        }
    }
}
