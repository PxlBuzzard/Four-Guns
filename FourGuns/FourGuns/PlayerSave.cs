#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    /// Saves and Loads a player.
    /// </summary>
    public class PlayerSave
    {
        Stream s;
        BinaryReader reader;
        BinaryWriter writer;
        Player loadedPlayer;

        /// <summary>
        /// Loads in a player profile
        /// </summary>
        /// <param name="p">The player loading the new profile</param>
        /// <param name="filename">The name of the profile to load</param>
        /// <param name="content">The content manager</param>
        /// <returns>A new player with the selected profile information</returns>
        public Player Load(Player p, string filename, ContentManager content)
        {
            try
            {
                //Load the file
                s = File.OpenRead(filename);
                reader = new BinaryReader(s);
                //Create a new player
                loadedPlayer = new Player(content, p.Position, p.playerIndex);
                //Loead in data
                loadedPlayer.profileName = reader.ReadString();
                string sprite_name = reader.ReadString();
                loadedPlayer.Sprite = content.Load<Texture2D>(sprite_name);
                loadedPlayer.score = reader.ReadInt32();
                loadedPlayer.coins = reader.ReadInt32();

                //load all gun hashes, not finished
                //loadedPlayer.currentWeapons.Add(Gun.WeaponFromString(content, reader.ReadString()));

                return loadedPlayer;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading: " + e.Message);
                //If something goes wrong with loading, send back the current profile
                return p;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (s != null)
                    s.Close();
            }
        }

        /// <summary>
        /// Saves player data to a new file
        /// </summary>
        /// <param name="p">The player to save</param>
        public void Save(Player p)
        {
            try
            {
                s = File.OpenWrite(p.profileName + ".sav");
                writer = new BinaryWriter(s);

                writer.Write(p.profileName);
                writer.Write(p.Sprite.Name);
                writer.Write(p.score);
                writer.Write(p.coins);
                foreach (Weapon weapon in p.currentWeapons)
                    writer.Write(weapon.CreateWeaponString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving: " + e.Message);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                else if (s != null)
                    s.Close();
            }
        }
    }
}
