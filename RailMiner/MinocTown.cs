using ScriptSDK;
using ScriptSDK.Attributes;
using ScriptSDK.Data;
using ScriptSDK.Engines;
using ScriptSDK.Gumps;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;
using StealthAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ultima;
//Serial: 7C64
//GumpID: 045D
namespace RailMiner
{
    class MinocTownMiner
    {
        Point3D _forge = new Point3D(2520, 557);
        Dictionary<int, int> _runningTally = new Dictionary<int, int>();
        private Stopwatch _timer;
        private PlayerMobile m_Player;                              //       Vector3 _secondForgeLoc = new Vector3(2445, 97);
        string _bankStoneID = "FKYSJMD";
        Serial _bankGumpID = new Serial(1);// "GTHSJMD";
        Rectangle2D _boundingBoxMine = new Rectangle2D(2554, 474, 28, 29);
        Rectangle2D _curBounding = new Rectangle2D();
        List<Point3D> _MinePaths =
                        new List<Point3D> {
                            new Point3D(2510,561),

                            new Point3D(2516,539),
                            new Point3D(2529,537),
                            new Point3D(2526,500),
                            new Point3D(2552,501),
                            new Point3D(2559,501),
                        };

        int[] tileTypes = { 1339, 1340, 1341, 1342, 1343 };

        List<StaticItemRealXY> m_UsedTiles = new List<StaticItemRealXY>();



        public MinocTownMiner()
        {
            ScriptSDK.SDK.Initialize();

            m_Player = PlayerMobile.GetPlayer();
            
        }

        internal void Loop()
        {
            if (!m_Player.Name.Equals("MinerBoB"))
                return;
            
            _timer = System.Diagnostics.Stopwatch.StartNew();
            while (true)
            {
                Logger.I("Starting Mine: ");
                var rail = _MinePaths;
                _curBounding = _boundingBoxMine;
                if(_forge.DistanceSqrt(m_Player.Location) < 20)
                    rail.ForEach(m => Pathfind(m,0));//.SmartMove(m));
                MineLoop();

                rail.Reverse();
                rail.ForEach(m => Pathfind(m, 0));
                rail.Reverse();
                if (!CheckHome(0))
                {
                    return;
                }
                m_UsedTiles.Clear();
            }


        }
        private void Use(string obj)
        {
            new Item(EasyUOHelper.ConvertToStealthID(obj)).DoubleClick();

        }
        private void Use(Serial obj)
        {
            new Item(obj).DoubleClick();

        }
        private bool Pathfind(Point3D loc, int accuracy = 0)
        {
            foreach (var x in m_Player.Movement.GetPath(loc, false, accuracy))
            {
                m_Player.Movement.Move(x, 0);
            }
            m_Player.Movement.newMoveXY((ushort)loc.X, (ushort)loc.Y, true, accuracy, true);
            if (m_Player.Location.DistanceSqrt(loc) > accuracy + 1)
                return false;
            return true;
        }



        private void Bank(int mineNumber)
        {
            int counter = 0;
            Use(_bankStoneID);
            Gump bank;
            if(!Gump.WaitForGump(GumpTypes.BANK_MENU_GUMP_TEXT, 2000,out bank))
            {
                return;//fail

            }
            bank.Buttons.First().Click();
            var bankChest = new Container(EasyUOHelper.ConvertToStealthID("GTHSJMD"));
            while (!bankChest.Valid)
                Thread.Sleep(500);
            

            foreach(var kv in ItemTypes.MiningBankables)
            Scanner.Find<Item>(kv, m_Player.Backpack.Serial).ForEach(ore => {

                counter += ore.Amount;
                    if (_runningTally.ContainsKey(ore.Color))
                        _runningTally[ore.Color] += ore.Amount;
                    else
                        _runningTally.Add(ore.Color, ore.Amount);

                if (ore.MoveItem(bankChest,ore.Amount))
                    Thread.Sleep(1500);


            });
            Logger.I("Banked " + counter + " Ore this run for mine: " + mineNumber);
            Logger.I("Totals for this session : " + _timer.Elapsed.ToString());
            foreach (var kv in _runningTally)
            {
                Logger.I("Color: " + kv.Key + " Amount: " + kv.Value);
            }
            if (Scanner.Find<Item>(EasyUOHelper.ConvertToStealthType(ItemTypes.PickAxe), m_Player.Backpack.Serial).FirstOrDefault() == null)
                CraftTool(2);
        }

        private void MineLoop()
        {
            JournalHelper.GetJournal().ClearJournal(JournalType.Journal);

            var tiles = TileReader.GetOreSpots(_curBounding.Start, _curBounding.End);

            StaticItemRealXY tile;
            while (tiles.Count(t => !m_UsedTiles.Contains(t)) > 0)
            {
                var myLoc = m_Player.Location;
                tile = tiles.Where(t => !m_UsedTiles.Contains(t)).OrderBy(t => myLoc.DistanceSqrt(t.Loc)).FirstOrDefault();

                if (!Pathfind(tile.Loc, 1))
                {
                   // continue;
                }
                if (tile.Loc.DistanceSqrt(m_Player.Location) > 2)
                    continue;
                MineLocation(tile);
                if (!CheckMiningStatus())
                    return;
            }

        }

        private void MineLocation(StaticItemRealXY tile)
        {
            bool mining = true;
            while (mining)
            {

                if (!CheckMiningStatus())
                    return;
                var tool = Scanner.Find<Item>(ItemTypes.PickAxe, m_Player.Backpack.Serial).FirstOrDefault();
                if (tool == null)
                    return;
                tool.DoubleClick();
                TargetHelper.GetTarget().WaitForTarget(5000);
                JournalHelper.GetJournal().ClearJournal(JournalType.Journal);
                TargetHelper.GetTarget().TargetTo(tile.Tile, tile.Loc);

                Thread.Sleep(1000);
                for (int i = 0; i < 25; i++)
                {
                    if (JournalHelper.GetJournal().InJournal(new string[] { "you loosen some", "you put" }))
                        break;
                    if (JournalHelper.GetJournal().InJournal(new string[] { "nothing here", "far away", "immune", "line of", "try mining", "that is too", "cannot mine" }))
                    {
                        mining = false;
                        break;
                    }

                    Thread.Sleep(500);
                }
            }
            m_UsedTiles.Add(tile);
        }

        private bool CheckHome(int mineNum)
        {
            if (m_Player.Dead)
            {
                Ress();
            }
            Logger.I("Smelting Ore");
            var oldPos = m_Player.Location;

            Pathfind(_forge,1);
            Thread.Sleep(2000);
           // if (Tools.Get2DDistance(_forge, new Vector3(UOD.CharPosX, UOD.CharPosY)) > 2)
            //    return false;
            foreach(var kv in ItemTypes.Ores)
            Scanner.Find<Item>(kv,m_Player.Backpack.Serial).ForEach(ore => { Use(ore.Serial); Thread.Sleep(1500); });
            int numOre = 0;
            foreach(var kv in ItemTypes.Ingots)
            Scanner.Find<Item>(kv, m_Player.Backpack.Serial).ForEach(ore => numOre += ore.Amount);

            Pathfind(_MinePaths[0]);
            Bank(mineNum);

           
            return true;
        }
        private void Move(int x, int y, int z, int delay)
        {
            Pathfind(new Point3D(x, y, z), delay);
        }

        private void Ress()
        {
            Thread.Sleep(5000);
            if (m_Player.Dead == false)
                return;
            Logger.I("Attempting to Ress");
            m_Player.SendText("home home home",0);
            Thread.Sleep(10000);
            Move(5146, 1098, 0, 10000);
            Move(5146, 1074, 0, 20000);
            Move(5153, 1074, 0, 20000);

            Use("SSMRJMD");
            Thread.Sleep(5000);
            Gump.ActiveGumps.Last().Buttons.First().Click();

            Thread.Sleep(5000);
            if(m_Player.Dead)
                Ress();

            Use(m_Player.Backpack.Serial);
            Move(5158, 1080, 0, 10000);
            Pathfind(_MinePaths[0]);
            if (_MinePaths[0].DistanceSqrt(m_Player.Location) > 1)
            {
                Logger.E("Stuck On Way Home from Ressing");
                throw new Exception("Stuck On Way Home from Ressing");
            }
        }

        private bool CheckMiningStatus(bool forceSmelt = false)
        {
            //check for tinker tools / ingots / pickaxes.
            //Craft / grab as needed
            var tool = Scanner.Find<Item>(ItemTypes.PickAxe, m_Player.Backpack.Serial);
            bool goingHome = false;
            if (tool == null)
            {
                Logger.I("Out of Picks, heading Home!");
                goingHome = true;
            }
 
            if (m_Player.Dead)
            {
                Logger.I("Killed in MiningLoop");
                return false;
            }
            if (goingHome)
                return false;

            return true;
        }

        private bool CraftTool(int cnt)
        {
            var tinker = Scanner.Find<Item>(ItemTypes.Tinker_Tool, new Serial(EasyUOHelper.ConvertToStealthID("GTHSJMD"))).FirstOrDefault();
            if (tinker == null)
            {
                Logger.E("Out of Tinker Tools");
                return false;
            }
            if (tinker.MoveItem(m_Player.Backpack))
            {
                var iron = Scanner.Find<Item>(ItemTypes.IngotIron, new Serial(EasyUOHelper.ConvertToStealthID("GTHSJMD"))).FirstOrDefault( t=> t.Color == 0);
                if (iron == null)
                    Logger.E("Out of Iron");
                iron.MoveItem(m_Player.Backpack, 16);
                for (int i = 0; i < cnt;)
                {
                    JournalHelper.GetJournal().ClearJournal( JournalType.Journal);
                    //UOD.Msg("_waitmenu 'Tinkering' 'Tools' 'Tools' 'pickaxe'");
                    Use(tinker.Serial);
                    //Thread.Sleep(5000);

                    Gump craft;
                    if(Gump.WaitForGump("TINKERING MENU",10000,out craft))
                    {
                        craft.Buttons.FirstOrDefault(b => b.PacketValue == 112).Click();
                        Thread.Sleep(10000);
                    }
                    if(m_Player.Dead)
                    {
                        Logger.I("Ress Triggered From Craft!");
                        return true;
                    }
                }
                tinker.MoveItem(new Serial(EasyUOHelper.ConvertToStealthID("GTHSJMD")), 1);
            }

           
            return true;
        }

      
        /*
        class SpiralOut{
protected:
    unsigned layer;
    unsigned leg;
public:
    int x, y; //read these as output from next, do not modify.
    SpiralOut():layer(1),leg(0),x(0),y(0){}
    void goNext(){
        switch(leg){
        case 0: ++x; if(x  == layer)  ++leg;                break;
        case 1: ++y; if(y  == layer)  ++leg;                break;
        case 2: --x; if(-x == layer)  ++leg;                break;
        case 3: --y; if(-y == layer){ leg = 0; ++layer; }   break;
        }
    }
};
        */
    }
}
