using System;
using System.Collections.Generic;
using ScriptSDK.Engines;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;
using ScriptSDK.Targets;

namespace ScriptSDK
{
#pragma warning disable 1591
    public class Test
    {
        public Test3 data { get; set; }

        public Test()
        {
            data = new Test3();
        }
    }

    public class Test3
    {
        internal Test3()
        {
            
        }
    }

    class program
    {
        [STAThread]
        static void Main()
        {
            var p = PlayerMobile.GetPlayer();
            while (!p.Skills.Healing.Value.Equals(100.0))
                p.Salute();

            List<Mobile> list = Scanner.Find<Mobile>(0x23E, 0xFFFF, 0x0, true);

            Console.WriteLine("Hello World");
            Console.ReadKey();
        }
    }
#pragma warning restore 1591
}

