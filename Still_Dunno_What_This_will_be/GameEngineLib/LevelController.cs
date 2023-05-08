using GameEngineLib.PowerUps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameEngineLib
{
    public static class LevelController
    {
        public static Player player;

        public static bool IsCreateWindow { get; set; }
        public static bool IsTestWindow { get; set; }
        public static bool ShowDevOutlines { get; set; }



        /// <summary>
        /// This will load a given level from the textfile location
        /// </summary>
        /// <param name="FileName">
        /// This needs to be a full string 'C:\....'
        /// </param>
        /// <returns>
        /// The canvas with the Game loaded into it.
        /// </returns>
        public static Canvas LoadLevel(string FileName)
        {
            return CreateLevel(new FileStream(FileName, FileMode.Open));
        }
        public static Canvas LoadLevel(FileStream file)
        {
            return CreateLevel(file);
        }
        private static Canvas CreateLevel(FileStream fs = null)
        {
            Canvas canva = new Canvas();
            string[] objectConcepts;
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    objectConcepts = sr.ReadLine().Split(';');
                    double left = double.Parse(objectConcepts[1]);
                    double top = double.Parse(objectConcepts[2]);
                    int height = int.Parse(objectConcepts[3]);
                    int width = int.Parse(objectConcepts[4]);
                    int movementSpeed = int.Parse(objectConcepts[5]);
                    bool hasCollisionDetection = objectConcepts[6].Trim('=') == "1";
                    GameItem gI = new GameItem();
                    if (objectConcepts[0] == "OO") gI = new GameItemCircle(left, top, width, height, hasCollisionDetection);
                    else if (objectConcepts[0] == "--") gI = new GameItemRectangle(left, top, width, height, hasCollisionDetection, movementSpeed);
                    else if (objectConcepts[0] == "EE") gI = new GameItemEndPoint(left, top, width, height, hasCollisionDetection, movementSpeed);
                    else if (objectConcepts[0] == "PP")
                    {
                        player = new Player(left, top, width, height,movementSpeed, false); //De false nog aanpasbaar maken
                        player.Parent = canva;
                    }
                    else if (objectConcepts[0].StartsWith("PUJB"))
                    {
                        PowerJumpBoost pow = new PowerJumpBoost(left, top);
                    }

                    if (!gI.IsEmpty)
                        gI.Parent = canva;
                }
            }
            return canva;
            //==Bool:  1=true, 0=false;
            //XX;LocationX;LocationY;Height;Width; --> GameItemGameOver
            //OO;LocationX;LocationY;Height;Width;MovementSpeed;==HasCollision --> circle
            //EE;LocationX;LocationY;Height;Width;MovementSpeed;==HasCollision --> EndRect
            //--;LocationX;LocationY;Height;Width;MovementSpeed;==HasCollision --> Rectanngle
            //PP;LocationX;LocationY;Height;Width;MovementSpeed;==HasCollision;==HasHealth --> PlayerController
            //PUJB;LocationX;LocationY;Height;Width;Movementspeed;==HasCollision --> Powerup JumpBoost

        }
    }
}
