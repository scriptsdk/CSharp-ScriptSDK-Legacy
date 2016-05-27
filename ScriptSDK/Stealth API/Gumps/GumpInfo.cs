using System.IO;
using System.Text;
#pragma warning disable 1591

namespace StealthAPI
{
    internal struct GumpInfo : IDeserialized
    {
        public uint Serial { get; private set; }
        public uint GumpID { get; private set; }
        public ushort X { get; private set; }
        public ushort Y { get; private set; }
        public int Pages { get; private set; }
        public bool NoMove { get; private set; }
        public bool NoResize { get; private set; }
        public bool NoDispose { get; private set; }
        public bool NoClose { get; private set; }

        public Group[] Groups { get; private set; }
        public EndGroup[] EndGroups { get; private set; }
        public GumpButton[] GumpButtons { get; private set; }
        public ButtonTileArt[] ButtonTileArts { get; private set; }
        public CheckBox[] CheckBoxes { get; private set; }
        public CheckerTrans[] CheckerTrans { get; private set; }
        public CroppedText[] CroppedText { get; private set; }
        public GumpPic[] GumpPics { get; private set; }
        public GumpPicTiled[] GumpPicTiled { get; private set; }
        public RadioButton[] RadioButtons { get; private set; }

        public ResizePic[] ResizePics { get; private set; }
        public GumpText[] GumpText { get; private set; }
        public TextEntry[] TextEntries { get; private set; }
        public string[] Text { get; private set; }
        public TextEntryLimited[] TextEntriesLimited { get; private set; }
        public TilePic[] TilePics { get; private set; }
        public TilePicture[] TilePicHue { get; private set; }
        public Tooltip[] Tooltips { get; private set; }
        public HtmlGump[] HtmlGump { get; private set; }
        public XmfHTMLGump[] XmfHtmlGump { get; private set; }
        public XmfHTMLGumpColor[] XmfHTMLGumpColor { get; private set; }
        public XmfHTMLTok[] XmfHTMLTok { get; private set; }
        public ItemProperty[] ItemProperties { get; private set; }

        public void Deserialize(BinaryReader br)
        {
                Serial = br.ReadUInt32();
                GumpID = br.ReadUInt32();
                X = br.ReadUInt16();
                Y = br.ReadUInt16();
                Pages = br.ReadInt32();
                NoMove = br.ReadBoolean();
                NoResize = br.ReadBoolean();
                NoDispose = br.ReadBoolean();
                NoClose = br.ReadBoolean();

                Groups = DeserializeArray<Group>(br);
                EndGroups = DeserializeArray<EndGroup>(br);
                GumpButtons = DeserializeArray<GumpButton>(br);
                ButtonTileArts = DeserializeArray<ButtonTileArt>(br);
                CheckBoxes = DeserializeArray<CheckBox>(br);
                CheckerTrans = DeserializeArray<CheckerTrans>(br);
                CroppedText = DeserializeArray<CroppedText>(br);
                GumpPics = DeserializeArray<GumpPic>(br);
                GumpPicTiled = DeserializeArray<GumpPicTiled>(br);
                RadioButtons = DeserializeArray<RadioButton>(br);
                ResizePics = DeserializeArray<ResizePic>(br);
                GumpText = DeserializeArray<GumpText>(br);
                TextEntries = DeserializeArray<TextEntry>(br);

                var len = br.ReadUInt16(); // text
                if (len > 0)
                {
                    Text = new string[len];
                    for (var i = 0; i < len; i++)
                    {
                        var paramLength = br.ReadUInt32();
                        var msg = Encoding.Unicode.GetString(br.ReadBytes((int)paramLength * sizeof(char)), 0, (int)paramLength * sizeof(char));
                        Text[i] = msg;
                    }
                }

                TextEntriesLimited = DeserializeArray<TextEntryLimited>(br);
                TilePics = DeserializeArray<TilePic>(br);
                TilePicHue = DeserializeArray<TilePicture>(br);
                Tooltips = DeserializeArray<Tooltip>(br);
                HtmlGump = DeserializeArray<HtmlGump>(br);
                XmfHtmlGump = DeserializeArray<XmfHTMLGump>(br);
                XmfHTMLGumpColor = DeserializeArray<XmfHTMLGumpColor>(br);

                XmfHTMLTok = DeserializeArray<XmfHTMLTok>(br);
                ItemProperties = DeserializeArray<ItemProperty>(br);
        }

        private T[] DeserializeArray<T>(BinaryReader br)
        {
            var result = new T[0];
            var len = br.ReadUInt16(); 
            if (len > 0)
            {
                result = new T[len];
                for (var i = 0; i < len; i++)
                    result[i] = br.MarshalToObject<T>();
            }

            return result;
        }
    }

}
