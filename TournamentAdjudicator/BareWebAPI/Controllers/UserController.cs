﻿using TournamentAdjudicator.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Cryptography;

namespace TournamentAdjudicator.Controllers
{
    public class UserController : ApiController
    {

        public static List<Player> Players { get; set; }
        public static int players = 0;
        public static System.Timers.Timer UserTimer = new System.Timers.Timer(10000);

        [HttpGet]
        public IHttpActionResult GetUser()
        {
            if (players < 4 && !Gameplay.Game_Started)
            { 
               
                //Secure hash
                byte[] randBytes;
                randBytes = new byte[100];

                // Create a new RNGCryptoServiceProvider.
                System.Security.Cryptography.RNGCryptoServiceProvider rand =
                     new System.Security.Cryptography.RNGCryptoServiceProvider();

                // Fill the buffer with random bytes.
                rand.GetBytes(randBytes);

                MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] hash = md5.ComputeHash(randBytes);

                string stringhash = Convert.ToBase64String(hash);


                int newid = ++players;
                Player newplayer = new Player { ID = newid, Hash = stringhash };
                try
                {
                    List<Player> tempList = new List<Player>();
                    if (Players != null) {
                        tempList = Players;
                    }
                    tempList.Add(newplayer);
                    Players = tempList;
                }
                catch { }

                return Ok(newplayer);
            }else{
                return NotFound();
            }
            

        }

    }

     

}
