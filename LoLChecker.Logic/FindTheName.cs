using System;
using System.Collections.Generic;
using System.Linq;

namespace LoLChecker.Logic
{
    public class FindTheName
    {
        public String[] Teammates(String StartLobby)
        {
           return StartLobby.Split(new String[] { "joined the lobby" }, StringSplitOptions.RemoveEmptyEntries).Select(teammate=>teammate.Trim()).ToArray();

        }
    }
}
