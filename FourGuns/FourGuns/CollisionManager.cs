#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Handles all collision checks during playtime.
    /// </summary>
    public class CollisionManager
    {
        #region Fields
        private List<Player> players;
        private Vector2 temp;
        private List<GameObject>[,] collChecks;
        public List<GameObject>[,] collTiles;

        //stores tile size
        private int tileWidth;
        private int tileHeight;

        private int mapWidth;
        private int mapHeight;

        //used for temporary values
        private int x;
        private int y;
        #endregion

        /// <summary>
        /// Initalize the Collision Manager.
        /// </summary>
        /// <param name="playerList">the list of players ingame</param>
        public CollisionManager(List<Player> playerList)
        {
            players = playerList;
            tileWidth = TileLayer.TileWidth;
            tileHeight = TileLayer.TileHeight;
        }

        /// <summary>
        /// Runs when a mission is started to rebuild the collision array.
        /// </summary>
        /// <param name="row">map width</param>
        /// <param name="col">map height</param>
        public void SetCollisionArray(List<TileLayer> collLayers, string[] tileStrings)
        {
            //map size
            mapWidth = collLayers[0].Width;
            mapHeight = collLayers[0].Height;

            //initialize collision map
            collChecks = new List<GameObject>[mapHeight, mapWidth];
            collTiles = new List<GameObject>[mapHeight, mapWidth];

            //fill the array with lists
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    collChecks[y, x] = new List<GameObject>();
                    collTiles[y, x] = new List<GameObject>();

                    //add a static game object if there is a collidable tile
                    int textureIndex = collLayers[1].Map[y, x];
                    if (textureIndex > -1)
                        collTiles[y, x].Add(new StatGameObj( collLayers[1].TileTextures[textureIndex], new Vector2(x * tileWidth, y * tileHeight)));
                }
            }
        }

        /// <summary>
        /// Run collision checks for all relevant objects.
        /// Collision Manager 2.0
        /// </summary>
        /// <param name="mission">current mission</param>
        /// <param name="theCamera">the camera</param>
        public void Update(Mission mission, Camera2D theCamera)
        {
            ClearCollisionList();

            CameraCollision(mission, theCamera);

            PopulateCollisionList(mission, theCamera);

            //run through the map array
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    //check the objects in each tile
                    foreach (GameObject collider in collChecks[y, x])
                    {
                        foreach (GameObject collidee in collChecks[y, x])
                        {
                            //if an object isn't itself and is intersecting another object
                            if (!collider.Equals(collidee) && Intersects(collider, collidee))
                            {
                                //bullet collision
                                if (collider as Bullet != null)
                                    BulletCollision(collider, collidee, mission);
                                //actor collision
                                else if (collider as Actor != null)
                                ActorCollision(collider, collidee, mission);
                            }
                        }

                        //if an object is colliding with a collidable object
                        if (collTiles[y, x].Count > 0 && Intersects(collider, collTiles[y, x][0]))
                        {
                            //bullet collision with fully collidable tile
                            if (collider as Bullet != null)
                                BulletCollision(collider, collTiles[y, x][0], mission);
                            //actor collision
                            else if (collider as Actor != null)
                                ActorCollision(collider, collTiles[y, x][0], mission);
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// Collision checks between GameObjects.
        /// </summary>
        /// <param name="collider">the object colliding</param>
        /// <param name="collidee">object being collided with</param>
        /// <returns>true if colliding</returns>
        private bool Intersects(GameObject collider, GameObject thecollidee)
        {
            //collision with an Actor and Actor
            if (thecollidee as Actor != null && collider as Actor != null)
            {
                Actor collidee = (Actor)thecollidee;
                Actor theCollider = (Actor)collider;
                if (theCollider.Position.X + theCollider.SpriteWidth < collidee.Position.X) return false;
                if (collidee.Position.X + collidee.SpriteWidth < theCollider.Position.X) return false;
                if (theCollider.Position.Y + theCollider.SpriteHeight < collidee.Position.Y) return false;
                if (collidee.Position.Y + collidee.SpriteHeight < theCollider.Position.Y) return false;
                return true;
            }
            //collision with a StatGameObj if not an Actor
            else if (thecollidee is StatGameObj && collider as Actor == null)
            {
                StatGameObj tile = (StatGameObj)thecollidee;
                if (collider.Position.X + collider.Sprite.Width < tile.Position.X) return false;
                if (tile.Position.X + tile.CollBoxWidth < collider.Position.X) return false;
                if (collider.Position.Y + collider.Sprite.Height / 2 < tile.Position.Y) return false;
                if (tile.Position.Y + tile.CollBoxHeight < collider.Position.Y) return false;
                return true;
            }
            //collision with a StatGameObj if Actor
            else if (thecollidee is StatGameObj && collider as Actor != null)
            {
                StatGameObj tile = (StatGameObj)thecollidee;
                Actor actor = (Actor)collider;
                if (actor.Position.X + actor.SpriteWidth < tile.Position.X) return false;
                if (tile.Position.X + tile.CollBoxWidth < actor.Position.X) return false;
                if (actor.Position.Y + actor.SpriteHeight / 2 < tile.Position.Y) return false;
                if (tile.Position.Y + tile.CollBoxHeight < actor.Position.Y + actor.SpriteHeight / 2) return false;
                return true;
            }
            //collision with an Actor
            if (thecollidee as Actor != null)
            {
                Actor collidee = (Actor)thecollidee;
                if (collider.Position.X + collider.Sprite.Width < collidee.Position.X) return false;
                if (collidee.Position.X + collidee.SpriteWidth < collider.Position.X) return false;
                if (collider.Position.Y + collider.Sprite.Height < collidee.Position.Y) return false;
                if (collidee.Position.Y + collidee.SpriteHeight < collider.Position.Y) return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Run if a bullet has hit an object.
        /// </summary>
        /// <param name="bullet">the bullet</param>
        /// <param name="collidee">the object that the bullet hit</param>
        /// <param name="mission">mission</param>
        private void BulletCollision(GameObject bullet, GameObject collidee, Mission mission)
        {
            Bullet b = (Bullet)bullet;

            //kill bullet if hits fully collidable tile
            if (collidee is StatGameObj)
            {
                StatGameObj collObj = (StatGameObj)collidee;
                if (collObj.collisiontype == collision_type.fully)
                    b.isAlive = false;
            }
            //damage an enemy
            else if (collidee is Enemy)
            {
                Enemy enemy = (Enemy)collidee;
                enemy.Health -= b.damage;
                b.isAlive = false;

                //wave survival kill
                if (enemy.Health <= 0 && mission.gamemodeString == "ws")
                    mission.gameMode.Kill(players[(int)b.pIndex], enemy);

            }
            //damage a player
            else if (collidee is Player && mission.gamemodeString == "dm")
            {
                Player player = (Player)collidee;
                //bullet can't collide with it's owner
                if (player.playerIndex != b.pIndex)
                {
                    player.Health -= b.damage;
                    b.isAlive = false;
                }

                //deathmatch kill
                if (player.Health <= 0 && mission.gamemodeString == "dm")
                    mission.gameMode.Kill(players[(int)b.pIndex], player);
            }
        }

        /// <summary>
        /// Run if an Actor has hit an object.
        /// </summary>
        /// <param name="actor">the actor</param>
        /// <param name="collidee">the object that the actor hit</param>
        /// <param name="mission">mission</param>
        private void ActorCollision(GameObject theActor, GameObject collidee, Mission mission)
        {
            Actor actor = (Actor)theActor;

            //push actor back if it hits collidable tile
            if (collidee is StatGameObj)
            {
                StatGameObj collObj = (StatGameObj)collidee;

                Vector2 newPosition = actor.Position;
                float deltax = (actor.Position.X + actor.SpriteWidth/2) - (collObj.Position.X + collObj.CollBoxWidth/2);
                float deltay = (actor.Position.Y + actor.SpriteHeight/2) - (collObj.Position.Y + collObj.CollBoxHeight/2);

                if ((deltax > deltay && Math.Abs(deltax) > Math.Abs(deltay)) 
                    || (deltax < deltay && Math.Abs(deltax) > Math.Abs(deltay)))
                {
                    newPosition.X = actor.PrevPosition.X;
                }

                if ((deltax > deltay && Math.Abs(deltax) < Math.Abs(deltay)) 
                    || (deltax < deltay && Math.Abs(deltax) < Math.Abs(deltay)))
                {
                    newPosition.Y = actor.PrevPosition.Y;
                }

                if (Math.Abs(deltax) == Math.Abs(deltay))
                {
                    newPosition = actor.PrevPosition;
                }


                actor.Position = newPosition; 
            }
            //damage player if collide with enemy
            else if (collidee is Enemy && theActor is Player)
            {
                Enemy enemy = (Enemy)collidee;
                actor.Health -= enemy.MeleeDamage;
                enemy.Position = enemy.PrevPosition;
            }
        }

        /// <summary>
        /// Run every Update to fill the map array with collidable objects.
        /// </summary>
        /// <param name="mission">the mission</param>
        /// <param name="theCamera">the camera</param>
        private void PopulateCollisionList(Mission mission, Camera2D theCamera)
        {
            //add players to list
            foreach (Player player in players)
            {
                if (player.ActivePlayer)
                {
                    //wave survival death
                    if (player.Health <= 0 && mission.gamemodeString == "ws")
                        mission.gameMode.Kill(player);
                    else
                    {
                        //stop player at edge of screen
                        ScreenEdgeCollision(player, theCamera);

                        x = (int)player.Position.X / tileWidth;
                        y = (int)player.Position.Y / tileHeight;

                        //add player to tiles they are occupying
                        collChecks[y, x].Add(player);

                        if (y < mapHeight - 1)
                            collChecks[y + 1, x].Add(player);

                        if (x < mapWidth - 1)
                            collChecks[y, x + 1].Add(player);

                        if (x < mapWidth - 1 && y < mapHeight - 1)
                            collChecks[y + 1, x + 1].Add(player);

                        //go through all the guns of the shooter
                        for (int i = 0; i <= player.currentWeapons.Count - 1; i++)
                        {
                            foreach (Bullet bullet in player.currentWeapons[i].activeBullets)
                            {
                                x = (int)bullet.Position.X / tileWidth;
                                y = (int)bullet.Position.Y / tileHeight;

                                //destroy the projectile if it goes offscreen
                                if ((bullet.Position.Y < theCamera.TopLeft.Y) || (bullet.Position.Y > theCamera.BottomRight.Y) ||
                                    (bullet.Position.X < theCamera.TopLeft.X) || (bullet.Position.X > theCamera.BottomRight.X))
                                    bullet.isAlive = false;
                                else
                                    collChecks[y, x].Add(bullet);
                            }
                        }
                    }
                }
            }

            if (mission.gamemodeString == "ws")
            {
                //add enemies to list
                foreach (Enemy enemy in mission.gameMode.activeEnemies)
                {
                    MapEdgeCollision(enemy, mission);

                    x = (int)enemy.Position.X / tileWidth;
                    y = (int)enemy.Position.Y / tileHeight;

                    //add enemy to tiles they are occupying
                    collChecks[y, x].Add(enemy);

                    if (y < mapHeight - 1)
                        collChecks[y + 1, x].Add(enemy);

                    if (x < mapWidth - 1)
                        collChecks[y, x + 1].Add(enemy);

                    if (x < mapWidth - 1 && y < mapHeight - 1)
                        collChecks[y + 1, x + 1].Add(enemy);
                }
            }
        }

        /// <summary>
        /// Clears out the collision list.
        /// </summary>
        private void ClearCollisionList()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    collChecks[y, x].Clear();
                }
            }
        }

        /// <summary>
        /// Handles player collision with the edge of the screen.
        /// </summary>
        /// <param name="sprite">the player</param>
        /// <param name="theCamera">the camera</param>
        private void ScreenEdgeCollision(Actor sprite, Camera2D theCamera)
        {
            temp = sprite.Position;
            temp.X = MathHelper.Clamp(sprite.Position.X, theCamera.TopLeft.X,
                theCamera.BottomRight.X - sprite.SpriteWidth);

            temp.Y = MathHelper.Clamp(sprite.Position.Y, theCamera.TopLeft.Y,
                theCamera.BottomRight.Y - sprite.SpriteHeight);
            sprite.Position = temp;
        }

        /// <summary>
        /// Stops actors from leaving the map.
        /// </summary>
        /// <param name="sprite">the sprite</param>
        /// <param name="mission">the mission</param>
        private void MapEdgeCollision(Actor sprite, Mission mission)
        {
            temp = sprite.Position;
            temp.X = MathHelper.Clamp(sprite.Position.X, 0, mission.tileLayers[0].WidthInPixels - sprite.SpriteWidth);
            temp.Y = MathHelper.Clamp(sprite.Position.Y, 0, mission.tileLayers[0].HeightInPixels - sprite.SpriteHeight);
            sprite.Position = temp;
        }

        /// <summary>
        /// Keeps the camera inside the map.
        /// </summary>
        /// <param name="mission">current mission</param>
        /// <param name="theCamera">the camera</param>
        private void CameraCollision(Mission mission, Camera2D theCamera)
        {
            // locks the camera inside the map
            temp = theCamera.Position;
            temp.X = MathHelper.Clamp(theCamera.Position.X, FourGuns.screenWidth / 2,
                        mission.mapWidth - FourGuns.screenWidth / 2);
            temp.Y = MathHelper.Clamp(theCamera.Position.Y, FourGuns.screenHeight / 2,
                        mission.mapHeight - FourGuns.screenHeight / 2);
            theCamera.Position = temp;
        }

    }
}
