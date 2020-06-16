using System;
using System.Linq;
using ScriptSDK.Gumps;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;

namespace ScriptSDK.Samples
{
    class UseRunebook
    {
        /// <summary> Recall using Runebook.cs Class </summary>
        /// <param name="bookspot"></param>
        /// <param name="recalltype"></param>
        /// <param name="runebookserial"></param>
        /// <returns>True if no longer in same location</returns>
        public static bool Travel(Item runebookserial, int bookspot, string recalltype)
        {
            var OSIconfig = new RuneBookConfig()
            {
                ScrollOffset = 10,
                DropOffset = 200,
                DefaultOffset = 300,
                RecallOffset = 50,
                GateOffset = 100,
                SacredOffset = 75,
                Jumper = 1
            };
            Runebook MyRunebook = new Runebook(runebookserial.Serial, OSIconfig);
            MyRunebook.Recall();
            runebookserial.DoubleClick();
            var loc1 = PlayerMobile.GetPlayer().Location;// LOC before recall
            runebookserial.DoubleClick(); // Open Runebook
            StealthAPI.Stealth.Client.Wait(1000);
            Gump g;
            g = Gump.GetGump((uint)OSIGumpIDs.Runebook); //OSI
            if (g == null)
            {
                StealthAPI.Stealth.Client.AddToSystemJournal("Gump is Null");
                return false;
            }
            else
            {
                foreach (var e in g.Buttons)
                {
                    if (!e.PacketValue.Equals(OSIconfig.RecallOffset + bookspot - 1) && !e.Graphic.Released.Equals(2103) &&
                        !e.Graphic.Pressed.Equals(2104)) continue;
                    if (recalltype == "Recall")
                    {
                        GumpButton recallButton = g.Buttons.First(i => i.PacketValue.Equals(OSIconfig.RecallOffset + (bookspot - 1)));
                        recallButton.Click();
                        break;
                    }
                }
            }
            StealthAPI.Stealth.Client.Wait(false ? 2000 : 3500);
            var loc2 = PlayerMobile.GetPlayer().Location; // LOC after recall
            return loc1 != loc2; // Compare Locs to see if you moved.
        }
    }
}
