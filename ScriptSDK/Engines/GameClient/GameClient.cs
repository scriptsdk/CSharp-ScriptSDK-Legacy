using System;
using System.Drawing;
using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Engines
{
    /// <summary>
    /// The class game client expose functions related to the stealth client.
    /// </summary>
    public class GameClient
    {
        private GameClient()
        {
            ConnectorDelay = 1000;
            DisconnectorDelay = 1000;
            ProfileChangerDelay = 1000;
        }

        private static GameClient _instance { get; set; }

        /// <summary>
        /// Stores the delay the connection system would use. Default Value is 1000 ms.
        /// </summary>
        public virtual int ConnectorDelay { get; set; }
        /// <summary>
        /// Stores the delay the disconnection system would use. Default Value is 1000 ms.
        /// </summary>
        public virtual int DisconnectorDelay { get; set; }
        /// <summary>
        /// Stores the delay for profile changing system would use. Default Value is 1000 ms.
        /// </summary>
        public virtual int ProfileChangerDelay { get; set; }

        /// <summary>
        /// Gets or sets the silentmode wich allows to control if the client should show logs or not.
        /// </summary>
        public virtual bool SilentMode
        {
            get { return Stealth.Client.GetSilentMode(); }
            set { Stealth.Client.SetSilentMode(value); }
        }

        /// <summary>
        /// Returns if account is connected.
        /// </summary>
        public virtual bool Connected
        {
            get { return Stealth.Client.GetConnectedStatus(); }
        }

        /// <summary>
        /// Gets or sets the auto reconnector mode.
        /// </summary>
        public virtual bool AutoConnect
        {
            get { return Stealth.Client.GetARStatus(); }
            set { Stealth.Client.SetARStatus(value); }
        }

        /// <summary>
        /// Returns the profile name.
        /// </summary>
        public virtual string ProfileName
        {
            get { return Stealth.Client.ProfileName(); }
        }

        /// <summary>
        /// Returns the real shardname.
        /// </summary>
        public virtual string ShardName
        {
            get { return Stealth.Client.GetShardName(); }
        }

        /// <summary>
        /// Returns the shardname selected on profil options (ie from config).
        /// </summary>
        public virtual string ProfileShardName
        {
            get { return Stealth.Client.GetProfileShardName(); }
        }

        /// <summary>
        /// Timestamp of last successful login.
        /// </summary>
        public virtual DateTime LastLogin
        {
            get { return Stealth.Client.GetConnectedTime(); }
        }

        /// <summary>
        /// Timestamp of last disconnection.
        /// </summary>
        public virtual DateTime LastLogout
        {
            get { return Stealth.Client.GetDisconnectedTime(); }
        }

        /// <summary>
        /// Returns stored proxy ip if stored in Stealth configuration.
        /// </summary>
        public virtual string ProxyIP
        {
            get { return Stealth.Client.GetProxyIP(); }
        }
        /// <summary>
        /// Returns stored proxy port if stored in Stealth configuration.
        /// </summary>
        public virtual ushort ProxyPort
        {
            get { return Stealth.Client.GetProxyPort(); }
        }
        /// <summary>
        /// Returns if stored proxy has been enabled.
        /// </summary>
        public virtual bool ActiveProxy
        {
            get { return Stealth.Client.GetUseProxy(); }
        }

        /// <summary>
        ///  Returns ShardIP\DNS 
        /// </summary>
        public virtual string ShardIP
        {
            get { return Stealth.Client.GameServerIPstring(); }
        }

        /// <summary>
        /// Returns stealth installation path.
        /// </summary>
        public virtual string StealthPath
        {
            get { return Stealth.Client.GetStealthPath(); }
        }

        /// <summary>
        /// Returns stealth profile path.
        /// </summary>
        public virtual string StealthProfilePath
        {
            get { return Stealth.Client.GetStealthProfilePath(); }
        }

        /// <summary>
        /// Returns shard path.
        /// </summary>
        public virtual string ShardPath
        {
            get { return Stealth.Client.GetShardPath(); }
        }

        /// <summary>
        /// Returns application path.
        /// </summary>
        public virtual string ScriptPath
        {
            get { return Stealth.Client.GetCurrentScriptPath(); }
        }

        /// <summary>
        /// Singleton object returns object reference to client.
        /// </summary>
        /// <returns></returns>
        public static GameClient GetClient()
        {
            return _instance ?? (_instance = new GameClient());
        }

        /// <summary>
        /// function tries to connect client to server.<br/>
        /// Optimization describes a dynamic process instead of a static delay.
        /// </summary>
        /// <param name="optimization"></param>
        /// <returns></returns>
        public virtual bool Connect(bool optimization = true)
        {
            if (Connected)
                return true;
            Stealth.Client.Connect();

            if (ConnectorDelay <= 0) return Connected;
            if (optimization)
            {
                var f = DateTime.Now.AddMilliseconds(ConnectorDelay);
                while ((!Connected) && (DateTime.Now < f))
                    Stealth.Client.Wait(25);
            }
            else
                Stealth.Client.Wait(ConnectorDelay);

            return Connected;
        }

        /// <summary>
        /// Function tries to change profile and then try to connect client to server.<br/>
        /// Optimization describes a dynamic process instead of a static delay.
        /// </summary>
        /// <param name="profilename"></param>
        /// <param name="optimization"></param>
        /// <returns></returns>
        public virtual bool Connect(string profilename, bool optimization = true)
        {
            return ChangeProfile(profilename, optimization) && Connect(optimization);
        }

        /// <summary>
        /// Function tries to disconnect character from server.<br/>
        /// Optimization describes a dynamic process instead of a static delay.
        /// </summary>
        /// <param name="optimization"></param>
        /// <returns></returns>
        public virtual bool Disconnect(bool optimization = true)
        {
            if (!Connected)
                return true;
            Stealth.Client.Disconnect();
            if (optimization)
            {
                var f = DateTime.Now.AddMilliseconds(DisconnectorDelay);
                while ((Connected) && (DateTime.Now < f))
                    Stealth.Client.Wait(25);
            }
            else
                Stealth.Client.Wait(DisconnectorDelay);

            return !Connected;
        }
        /// <summary>
        /// Function tries to change profile. Only works when client is not connected.<br/>
        /// Optimization describes a dynamic process instead of a static delay.
        /// </summary>
        /// <param name="profilename"></param>
        /// <param name="optimization"></param>
        /// <returns></returns>
        public virtual bool ChangeProfile(string profilename, bool optimization = true)
        {
            if (Connected)
                return false;
            if (ProfileName != profilename)
                Stealth.Client.ChangeProfile(profilename);

            if (optimization)
            {
                var f = DateTime.Now.AddMilliseconds(ProfileChangerDelay);
                while ((ProfileName != profilename) && (DateTime.Now < f))
                    Stealth.Client.Wait(25);
            }
            else
                Stealth.Client.Wait(ProfileChangerDelay);

            return ProfileName == profilename;
        }

        /// <summary>
        /// Function tries to disconnect account, then change profile and last but not atleast reconnect new profile.<br/>
        /// Optimization describes a dynamic process instead of a static delay.
        /// </summary>
        /// <param name="profilename"></param>
        /// <param name="optimization"></param>
        /// <returns></returns>
        public virtual bool ChangeCharacter(string profilename, bool optimization = true)
        {
            return Disconnect(optimization) && ChangeProfile(profilename, optimization) && Connect(optimization);
        }

        /// <summary>
        /// Function returns data about Stealth client.
        /// </summary>
        /// <returns></returns>
        public virtual AboutData GetInfo()
        {
            return Stealth.Client.GetStealthInfo();
        }

        /// <summary>
        /// Function sends a global message any profile runned by linked stealth client can read or parse.
        /// </summary>
        /// <param name="Msg"></param>
        public virtual void SendGlobalMessage(GameMessage Msg)
        {
            Stealth.Client.SetGlobal(Msg.Region, Msg.Name, Msg.Value);
        }

        /// <summary>
        /// Function reads a global message any profile runned by linked stealth client could had written.
        /// </summary>
        /// <param name="region"></param>
        /// <param name="regionname"></param>
        /// <returns></returns>
        public virtual GameMessage ReadGlobalMessage(VarRegion region, string regionname)
        {
            return new GameMessage
            {
                Region = region,
                Name = regionname,
                Value = Stealth.Client.GetGlobal(region, regionname)
            };
        }

        /// <summary>
        /// Plays sound file of given filepath.doesnt require the ".wav" exensíon.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public virtual bool PlaySound(string filename)
        {
            return Stealth.Client.PlayWav(filename + ".wav");
        }

        /// <summary>
        /// This function let flash the stealth icon red on system tray.
        /// </summary>
        public virtual void SetAlarm()
        {
            Stealth.Client.SetAlarm();
        }

        /// <summary>
        /// Sends text to info window box.
        /// </summary>
        /// <param name="text"></param>
        public virtual void AddToInfoWindow(string text)
        {
            Stealth.Client.FillInfoWindow(text);
        }

        /// <summary>
        /// Clears info window box.
        /// </summary>
        public virtual void ClearInfoWindow()
        {
            Stealth.Client.ClearInfoWindow();
        }

        /// <summary>
        /// Reads graphic from art.uop\mul.
        /// </summary>
        /// <param name="GraphicType"></param>
        /// <param name="GraphicColor"></param>
        /// <returns></returns>
        public virtual Bitmap GetGraphic(uint GraphicType, ushort GraphicColor)
        {
            return Stealth.Client.GetStaticArtBitmap(GraphicType, GraphicColor);
        }

        /// <summary>
        /// Sends "click on backpack" two times and records the delay. Returns true if the delay is within timeoutinMS.
        /// </summary>
        /// <param name="timeoutMS"></param>
        /// <returns></returns>
        public virtual bool CheckLag(int timeoutMS)
        {
            return Stealth.Client.CheckLag(timeoutMS);
        }

        /// <summary>
        /// Function returns if the two points are in line of sight.
        /// </summary>
        /// <param name="xf"></param>
        /// <param name="yf"></param>
        /// <param name="zf"></param>
        /// <param name="xt"></param>
        /// <param name="yt"></param>
        /// <param name="zt"></param>
        /// <param name="worldNum"></param>
        /// <returns></returns>
        public virtual bool CheckLOS(int xf, int yf, sbyte zf, int xt, int yt, sbyte zt, byte worldNum)
        {
            return Stealth.Client.CheckLOS(xf, yf, zf, xt, yt, zt, worldNum);
        }

        /// <summary>
        /// Clears catch bag.
        /// </summary>
        public virtual void UnsetCatchBag()
        {
            Stealth.Client.UnsetCatchBag();
        }

        /// <summary>
        /// Sets catch bag.
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public virtual byte SetCatchBag(uint objectId)
        {
            return Stealth.Client.SetCatchBag(objectId);
        }

        /// <summary>
        /// Returns reference to player.
        /// </summary>
        public PlayerMobile Player
        {
            get { return PlayerMobile.GetPlayer(); }
        }
    }
}