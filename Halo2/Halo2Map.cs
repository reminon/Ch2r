using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

//////////////////////////////////////////////////////////////////
// Description:
//  Everything needed to open and edit (not yet) a Halo 2 Map.
//
// Authors:
//  XIU             - xboxwarez@fuckhotmail.com
//  D3fault_dot_Xbe - d3fault@d3fault.net
//  Dark Neo        - dark_neo@notacake.com
//  The Fantastic 6 - ??
//
//////////////////////////////////////////////////////////////////

namespace Halo2
{
	///////////////////////////////////////////////////////////////
	//						  Class: Map
	///////////////////////////////////////////////////////////////
	/// <summary>
	/// This class will give you anything you need for editing Halo 2 Maps.
	/// </summary>
	public class Map
	{
		#region Data Members
		string file;
		MapHeader header;
		MapIndex index;
		public SoundIndex sounds;
		FileStream fs;
		#endregion

		#region Tags
		public static string[][] tags = { // Thx to D3fault_dot_Xbe
			new string[]{"<fx>", "DSP Sound Effects"},
			new string[]{"adlg", "AI Dialog"},
			new string[]{"ant!", "Antenna"},
			new string[]{"bipd", "Biped"},
			new string[]{"bitm", "Bitmap"},
			new string[]{"bloc", "Destructable Object"},
			new string[]{"bsdt", "BSD Template"},
			new string[]{"char", "Character"},
			new string[]{"clwd", "Flags"},
			new string[]{"coll", "Model Collision"},
			new string[]{"colo", "Color Table"},
			new string[]{"cont", "Contrail"},
			new string[]{"crea", "Creature"},
			new string[]{"ctrl", "Control"},
			new string[]{"deca", "Decal"},
			new string[]{"DECR", "Decorator"},
			new string[]{"effe", "Effect"},
			new string[]{"egor", "Screen Effect"},
			new string[]{"eqip", "Equipment"},
			new string[]{"fog ", "Fog Cluster"},
			new string[]{"foot", "Material Effect List"},
			new string[]{"fpch", "Fog Patch"},
			new string[]{"garb", "Garbage"},
			new string[]{"gldf", "Global Lighting"},
			new string[]{"goof", "Game Settings"},
			new string[]{"hlmt", "Object Properties"},
			new string[]{"hudg", "HUD Globals"},
			new string[]{"itmc", "Item Collection"},
			new string[]{"jmad", "JMS Animation Graph"},
			new string[]{"jpt!", "Damage Effect"},
			new string[]{"lens", "Lens Flare"},
			new string[]{"ligh", "Light"},
			new string[]{"lsnd", "Looping Sound"},
			new string[]{"ltmp", "Lightmap"},
			new string[]{"mach", "Machine"},
			new string[]{"matg", "Globals"},
			new string[]{"mdlg", "Level Specific Dialog"},
			new string[]{"MGS2", "Light Volume"},
			new string[]{"mode", "Model"},
			new string[]{"mulg", "Multiplayer Globals"},
			new string[]{"nhdt", "HUD Extended"},
			new string[]{"phmo", "Physics Model"},
			new string[]{"phys", "Physics"},
			new string[]{"pmov", "Particle Physics"},
			new string[]{"pphy", "Point Physics"},
			new string[]{"proj", "Projectile"},
			new string[]{"prt3", "Particle"},
			new string[]{"PRTM", "Particle Model"},
			new string[]{"sbsp", "Structure BSP"},
			new string[]{"scen", "Scenery"},
			new string[]{"scnr", "Scenario"},
			new string[]{"sfx+", "Global Sound Effects"},
			new string[]{"shad", "Shader"},
			new string[]{"sily", "Game Setting"},
			new string[]{"skin", "UI Skin"},
			new string[]{"sky ", "Sky"},
			new string[]{"sncl", "Sound Class List"},
			new string[]{"snd!", "Sound"},
			new string[]{"snde", "Sound Environment"},
			new string[]{"snmx", "Sound Mixture"},
			new string[]{"spas", "Shader Pass"},
			new string[]{"spk!", "Dialogue Constants"},
			new string[]{"ssce", "Sound Scenery"},
			new string[]{"stem", "Shader Template"},
			new string[]{"styl", "Situation AI Style"},
			new string[]{"tdtl", "Beam Trail"},
			new string[]{"trak", "Camera Track"},
			new string[]{"udlg", "Unit Dialog"},
			new string[]{"ugh!", "Sounds"},
			new string[]{"unic", "Unicode String List"},
			new string[]{"vehc", "Vehicle Collection"},
			new string[]{"vehi", "Vehicle"},
			new string[]{"vrtx", "Vertex Shader"},
			new string[]{"weap", "Weapon"},
			new string[]{"wgit", "Widget"},
			new string[]{"wgtz", "Widget Collection"},
			new string[]{"wigl", "Widget Globals"},
		};
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Map()
		{
			file = "";
		}

		///////////////////////////////////////////////////////////////
		//						 Method: Open
		///////////////////////////////////////////////////////////////
		/// <summary>
		/// Open a Halo 2 Map file
		/// </summary>
		/// <param name="filename">The path to the file</param>
		/// <param name="silent">Set to false to show errors</param>
		/// <returns>true on succes</returns>
		public int Open(string filename, bool silent)
		{
			int result = -1;
			try 
			{
				// Create the stream to open the file in
				fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
				
				// Read header and check for valid version
				header = new MapHeader();
				if (header.ReadHeader(ref fs)) 
				{
					// Read index
					index = new MapIndex();
					index.ReadIndex(ref fs, header.IndexOffset, header.StringOffset); // Will also read tags and items
					
					// Read sounds
					sounds = new SoundIndex();
					sounds.ReadSoundList(index.ItemArray[index.ItemArray.GetUpperBound(0)], fs, SecondaryMagic);

					// Everything found and correct
					file = filename; // Save filename
					result = 1;
				} 
				else 
				{
					// Sets result to 2 to show that the file failed Index loading
					result = 2;
				}
			} catch(System.IO.IOException e) {
				// Returns 0 to show that the file failed in reading, this could be because the file is in use
				// Not really needed since it shouldn't have been changed from 0, but good to have it in anyway
				result = 0;

				// Shows the specific error
				if (!silent) {
					System.Windows.Forms.MessageBox.Show(e.Message, "File access error:");
				}
			}
			return result;
		}

		/// <summary>
		/// Returns if the file has been succesfully opened
		/// </summary>
		public bool Opened
		{
			get {
				return file != "";
			}
		}

		///////////////////////////////////////////////////////////////
		//						Method: Close
		///////////////////////////////////////////////////////////////
		/// <summary>
		/// Close the current file
		/// </summary>
		public void Close()
		{
			file = "";
			header.Close();
			header = new MapHeader();
			index.Close();
			index = new MapIndex();
			fs.Close();
		}

		/// <summary>
		/// Give read access to header
		/// </summary>
		public MapHeader Header
		{
			get {
				return header;
			}
		}

		/// <summary>
		/// Give read access to index
		/// </summary>
		public MapIndex Index
		{
			get {
				return index;
			}
		}

		/// <summary>
		/// Returns the main Magic value
		/// </summary>
		public uint Magic
		{
			get 
			{
				try 
				{
					return Index.IndexMagic - (Header.IndexOffset + 32);
				} 
				catch (Exception e) 
				{
					System.Windows.Forms.MessageBox.Show(e.Message);
					return uint.MaxValue;
				}
			}
		}

		/// <summary>
		/// Returns the secondary magic
		/// </summary>
		public uint SecondaryMagic
		{
			get {
				try {
					return Index.Item(0).MetaOffset - Header.IndexOffset - Header.MetaStart;
				} catch (Exception e) {
					System.Windows.Forms.MessageBox.Show(e.Message);
					return uint.MaxValue;
				}
			}
		}

		/// <summary>
		/// Returns the path of the file currently opened
		/// </summary>
		public string Filename
		{
			get {
				return file;
			}
		}

		/// <summary>
		/// FileStream used for the map
		/// </summary>
		public FileStream stream
		{
			get {
				return fs;
			}
		}

		/// <summary>
		/// Checksum of the map
		/// </summary>
		public uint Signature
		{
			get 
			{
				return Header.Signature;
			}
		}

		///////////////////////////////////////////////////////////////
		//					Method: GetListViewItem
		///////////////////////////////////////////////////////////////
		/// <summary>
		/// Returns a ListViewItem for this map
		/// </summary>
		/// <returns>ListViewItem</returns>
		public System.Windows.Forms.ListViewItem GetListViewItem()
		{
			System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(header.Name, 0);
			item.Tag = this;

			return item;
		}

		public void SwapItem(MapIndexItem olditem, MapIndexItem newitem, SwapType type, string newtag, bool swap)
		{
			if (type == SwapType.Item) {
				// Save old values
				uint metaoffset = olditem.MetaOffset;
				string tag = olditem.Tag;
				
				// Change them
				olditem.MetaOffset = newitem.MetaOffset;
				olditem.Tag = newitem.Tag;
				if (swap) {
					newitem.MetaOffset = metaoffset;
					newitem.Tag = tag;
				}

				// Save changes in map
				BinaryWriter bw = new BinaryWriter(stream);
				bw.BaseStream.Seek(olditem.Offset, SeekOrigin.Begin);
				bw.Write(MapIndexItem.Reverse(newtag).ToCharArray());
				bw.BaseStream.Seek(4, SeekOrigin.Current);
				bw.Write(olditem.MetaOffset);
				if (swap) {
					bw.BaseStream.Seek(newitem.Offset, SeekOrigin.Begin);
					bw.Write(MapIndexItem.Reverse(tag).ToCharArray());
					bw.BaseStream.Seek(4, SeekOrigin.Current);
					bw.Write(newitem.MetaOffset);
					Index.SetItemByIdent(newitem.Ident, newitem);
				}
				
				Index.SetItemByIdent(olditem.Ident, olditem);
			} else if (type == SwapType.Reflexive) {
				uint ident = newitem.Ident;
				string tag = newitem.Tag;
				long offset = olditem.Offset;

				// Save changes in map
				BinaryWriter bw = new BinaryWriter(stream);
				bw.BaseStream.Seek(offset, SeekOrigin.Begin);
				bw.Write(MapIndexItem.Reverse(newtag).ToCharArray());
				bw.Write(ident);
			}
		}

		///////////////////////////////////////////////////////////////
		//						Method: FixString
		///////////////////////////////////////////////////////////////
		/// <summary>
		/// Remove the zero characters from the string
		/// </summary>
		/// <param name="str">string to fix</param>
		/// <returns>string without zero chars</returns>
		public static string FixString(string str)
		{
			string result = "";
			int pos = str.IndexOf("\0");
			if (pos > 0) {
				result = str.Substring(0, pos);
			}
			return result;
		}
			
		/// <summary>
		/// Get description of tag from list
		/// </summary>
		public static string TagName(string tag)
		{
			string result = "";
			tag = tag.Replace("[", "").Replace("]", "");

			for (uint x = 0; x < tags.Length; x++) {
				if (tags[x][0] == tag) {
					result = tags[x][1];
				}
			}

			return result;
		}

		/// <summary>
		/// Check if string is a tag
		/// </summary>
		public static bool IsTag(string tag)
		{
			bool result = false;
			tag = tag.Replace("[", "").Replace("]", "");

			for (uint x = 0; x < tags.Length; x++) {
				if (tags[x][0] == tag) {
					result = true;
					break;
				}
			}

			return result;
		}

		/// <summary>
		/// The Encryptomatic Signature, thx to The Fantastic 6
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[System.Runtime.InteropServices.DllImport("FabLib.dll", EntryPoint = "H2CheckSum", CallingConvention = CallingConvention.Winapi)]
		[System.Runtime.InteropServices.PreserveSig()] public static extern int H2CheckSum (int key);

	}

	///////////////////////////////////////////////////////////////
	//						Enum: MapTypes
	///////////////////////////////////////////////////////////////
	/// <summary>
	/// A map type in Halo 2
	/// </summary>
	public enum MapTypes
	{
		SinglePlayer = 0,
		MultiPlayer = 1,
		MainMenu = 2,
		Shared = 3,
		SinglePlayerShared = 4,
	}

	///////////////////////////////////////////////////////////////
	//						Class: MapHeader
	///////////////////////////////////////////////////////////////
	/// <summary>
	/// Class to handle the Halo 2 Map Header
	/// </summary>
	public class MapHeader
	{
		#region Data Members
		string head;			 // 'daeh'
		uint version;			 // 8 for Halo 2
		uint decomp_len;		 // This used to be the size of the extracted map file apparently
								 // but now is just the size of the map file itself
		uint unknown1;			 // No idea
		uint indexoffset;		 // Offset to the start of the tag index
		uint metastart;			 // Offset to the start of meta data?
		uint unknown2;			 // No idea
		uint unknown3;			 // No idea
		string filename;		 // Name of original file if loaded in cache
		string builddate;		 // String show the builddate
		MapTypes maptype;		 // Int representing map type (there's two new map types since Halo 1)
		uint unknown4;			 // No idea
		uint unknown5;			 // Always 1

		// Added by Dark Neo
		uint namelistoffset;	 // A list of file names (256 bytes for each)
		uint namelistlength;	 // The length of this list
		uint unknown6;			 // No idea
		uint intlistoffset;		 // A list of (as yet) unknown integers
		uint compactlistoffset;	 // The same list as above but compacted without the whitespace

		byte[] unknowns;

		string mapname;			 // The name of the map
		string scenarioname;	 // Pathname of the map
		uint unknown7;			 // Always 1 - apparently
		uint stringcount;		 // Same as item count in index

		// Added by Dark Neo
		uint stringoffset;		 // Offset to path name list
		uint stringlength;		 // The length of this list
		uint stringoffsetlist;	 // A list of offsets into the path name list
		uint signature;			 // The checksum of the map
		#endregion

		public MapHeader()
		{
		}

		///////////////////////////////////////////////////////////////
		//						Method: ReadHeader
		///////////////////////////////////////////////////////////////
		/// <summary>
		/// Read in the header from the file
		/// </summary>
		/// <param name="fs">stream to read from</param>
		/// <returns>true if valid header</returns>
		public bool ReadHeader(ref FileStream fs)
		{
			try {
				// Connect a binary reader to our opened stream
				BinaryReader br = new BinaryReader(fs);

				#region Read Header
				fs.Seek(0, SeekOrigin.Begin);
				head         = new string(br.ReadChars(4));
				version      = br.ReadUInt32();
				decomp_len   = br.ReadUInt32();
				unknown1     = br.ReadUInt32();
				indexoffset  = br.ReadUInt32();
				metastart    = br.ReadUInt32();
				unknown2     = br.ReadUInt32();
				unknown3     = br.ReadUInt32();
				// Is only filled it when loaded from cache
				filename     = Map.FixString(new string(br.ReadChars(0x100)));
				builddate	 = Map.FixString(new string(br.ReadChars(0x20)));
				maptype      = (MapTypes)br.ReadUInt32();
				unknown4     = br.ReadUInt32();
				unknown5     = br.ReadUInt32();
				unknowns     = br.ReadBytes(20);

				// Added by Dark Neo, sorry about the names, I'm not 100% sure what these
				// are so I just named what I saw
				namelistoffset		= br.ReadUInt32();
				namelistlength		= br.ReadUInt32();
				unknown6			= br.ReadUInt32();
				intlistoffset		= br.ReadUInt32();
				compactlistoffset	= br.ReadUInt32();

				unknowns			= br.ReadBytes(36);
				mapname				= Map.FixString(new string(br.ReadChars(0x24)));
				scenarioname		= Map.FixString(new string(br.ReadChars(0x100)));
				unknown7			= br.ReadUInt32();
				stringcount			= br.ReadUInt32();
				stringoffset		= br.ReadUInt32();
				stringlength		= br.ReadUInt32();
				stringoffsetlist	= br.ReadUInt32();
				signature			= br.ReadUInt32();
				#endregion

				// Everything found and correct
				return (version == 8) && (head == "daeh");
			} catch (Exception e) {
				System.Windows.Forms.MessageBox.Show(e.Message);
				return false;
			}
		}

		/// <summary>
		/// Clean up the header
		/// </summary>
		public void Close()
		{
		}

		/// <summary>
		/// Return string for the Map Type
		/// </summary>
		public string MapType
		{
			get {
				string result = "";

				switch (maptype) {
					case MapTypes.SinglePlayer:
						result = "Single Player";
						break;
					case MapTypes.MultiPlayer:
						result = "Multi Player";
						break;
					case MapTypes.MainMenu:
						result = "Main Menu";
						break;
					case MapTypes.Shared:
						result = "Shared";
						break;
					case MapTypes.SinglePlayerShared:
						result = "Single Player Shared";
						break;
				}

				return result;
			}
		}

		/// <summary>
		/// Return a full name for map
		/// </summary>
		public string Name
		{
			get {
				string result = "";

				switch (maptype) {
					case MapTypes.MainMenu:
					case MapTypes.Shared:
					case MapTypes.SinglePlayerShared:
						result = MapType;
						break;
					case MapTypes.MultiPlayer:
					case MapTypes.SinglePlayer:
						result = mapname.Substring(mapname.IndexOf("_") + 1);
						result = result.Substring(0, 1).ToUpper() + result.Substring(1);
						break;
				}

				return result;
			}
		}

		public uint StringOffset
		{
			get {
				return stringoffset;
			}
		}

		public uint StringCount
		{
			get {
				return stringcount;
			}
		}

		public uint IndexOffset
		{
			get {
				return indexoffset;
			}
		}

		public uint MetaStart
		{
			get {
				return metastart;
			}
		}

		public uint Signature
		{
			get 
			{
				return signature;
			}
		}

		public uint FileLength
		{
			get
			{
				return decomp_len;
			}
		}

		public void RefreshSig(Stream fs)
		{
			BinaryReader br = new BinaryReader(fs);
			fs.Seek(0x2d0, SeekOrigin.Begin);

			signature = br.ReadUInt32();
		}

	}

	///////////////////////////////////////////////////////////////
	//						Struct: MapIndexString
	///////////////////////////////////////////////////////////////
	public struct MapIndexString
	{
		public string _filename;
		public long _offset;

		public override string ToString()
		{
			return _filename;
		}
	}

	///////////////////////////////////////////////////////////////
	//						Class: MapIndex
	///////////////////////////////////////////////////////////////
	/// <summary>
	/// Class for reading the index
	/// </summary>
	public class MapIndex
	{
		#region Data Members
		MapIndexTag[] tags;
		MapIndexItem[] items;

		uint indexmagic;
		uint tagcount;
		uint itemsmagic;
		uint scnrid;
		uint firstid;
		uint unknown1;
		uint itemcount;
		string tagstart; // 'sgat'
		#endregion

		/// <summary>
		/// Constructor of MapIndex
		/// </summary>
		public MapIndex()
		{
		}

		///////////////////////////////////////////////////////////////
		//						Method: ReadIndex
		///////////////////////////////////////////////////////////////
		/// <summary>
		/// Read the index + tags and items from the map file
		/// </summary>
		/// <param name="fs">current file stream</param>
		/// <param name="indexoffset">offset to index</param>
		/// <param name="stringoffset">offset to strings</param>
		/// <returns>true on success</returns>
		public bool ReadIndex(ref FileStream fs, uint indexoffset, uint stringoffset)
		{
			try {
				BinaryReader br = new BinaryReader(fs);

				#region Read Index
				fs.Seek(indexoffset, SeekOrigin.Begin);
				indexmagic = br.ReadUInt32();
				tagcount   = br.ReadUInt32();
				itemsmagic = br.ReadUInt32();
				scnrid     = br.ReadUInt32();
				firstid    = br.ReadUInt32();
				unknown1   = br.ReadUInt32();
				itemcount  = br.ReadUInt32();
				tagstart   = new string(br.ReadChars(4));
				#endregion

				#region Read Strings
				fs.Seek(stringoffset, SeekOrigin.Begin);
				MapIndexString[] strings = new MapIndexString[itemcount];
				for (uint x = 0; x < itemcount; x++) {
					strings[x]._offset = br.BaseStream.Position;
					strings[x]._filename = ReadString(ref br);
				}
				#endregion

				#region Read Tags
				fs.Seek(indexoffset + 32, SeekOrigin.Begin);
				tags = new MapIndexTag[tagcount];
				for (uint x = 0; x < tagcount; x++) {
					tags[x] = new MapIndexTag();
					tags[x].Read(ref br);
				}
				#endregion

				#region Read Items
				items = new MapIndexItem[itemcount];
				for (uint x = 0; x < itemcount; x++) {
					items[x] = new MapIndexItem();
					items[x].ReadIndexItem(ref br, strings[x]);
				}
				#endregion

				return true;
			} catch (Exception e) {
				System.Windows.Forms.MessageBox.Show(e.Message);
				return false;
			}
		}

		///////////////////////////////////////////////////////////////
		//					   Method: ReadString
		///////////////////////////////////////////////////////////////
		/// <summary>
		/// Read a c++ string, until '\0'
		/// </summary>
		/// <param name="br">reference to the binary reader</param>
		/// <returns>the c++ string</returns>
		string ReadString(ref BinaryReader br)
		{
			string result = "";
			char temp;
			
			try {
				while ((temp = br.ReadChar()) != '\0') {
					result = result + temp;
				}
			} catch {
				result = "";
			}

			return result;
		}

		public void Close()
		{
		}

		public MapIndexItem Item(uint index)
		{
			return items[index];
		}

		public MapIndexItem[] ItemArray
		{
			get
			{
				return items;
			}
		}

		public void SetItem(uint index, MapIndexItem item)
		{
			items[index] = item;
		}

		public void SetItemByIdent(uint ident, MapIndexItem item)
		{
			for (int i = 0; i < items.Length; i++) {
				if (items[i].Ident == ident)
					SetItem((uint)i, item);
			}
		}

		public MapIndexItem ItemByIdent(uint ident)
		{
			if (ident == 0xFFFFFFFF)
			{
				MapIndexItem item = new MapIndexItem();

				item.Filename = "Nulled Out";
				return item;
			}

			foreach (MapIndexItem checkItem in items)
			{
				if (checkItem.Ident == ident)
				{
					return checkItem;
				}
			}

			MapIndexItem unknownItem = new MapIndexItem();
			
			unknownItem.Filename = "Unknown";
			return unknownItem;
		}

		public bool IdentExists(uint ident)
		{
			foreach (MapIndexItem item in items)
			{
				if (item.Ident == ident)
				{
					return true;
				}
			}
			return false;
		}

		public uint ItemCount
		{
			get {
				return itemcount;
			}
		}

		public uint IndexMagic
		{
			get 
			{
				return indexmagic;
			}
		}

	}

	///////////////////////////////////////////////////////////////
	//						Class: MapIndexTag
	///////////////////////////////////////////////////////////////
	/// <summary>
	/// Tag in the index
	/// </summary>
	public class MapIndexTag
	{
		const uint tagcount = 3;
		string[] tagclass;

		public MapIndexTag()
		{
			tagclass = new string[tagcount];
		}

		public void Read(ref BinaryReader br)
		{
			try {
				for (uint x = 0; x < tagcount; x++) {
					// ReadChar seems to skip the 0x255 characters
					tagclass[x] = ReadTag(ref br);
				}
			} catch {}
		}

		public static string ReadTag(ref BinaryReader br)
		{
			string tag = "";
			tag += (char)br.ReadByte();
			tag += (char)br.ReadByte();
			tag += (char)br.ReadByte();
			tag += (char)br.ReadByte();
			return tag;
		}
	}

	///////////////////////////////////////////////////////////////
	//					  Class: MapIndexItem
	///////////////////////////////////////////////////////////////
	/// <summary>
	/// Item in the index
	/// </summary>
	public struct MapIndexItem
	{
		#region Data Member
		string tagclass;
		uint   ident;
		uint   metaoffset;
		uint   size;
		string filename;
		long   offset;
		long   fileoffset;
		#endregion

		///////////////////////////////////////////////////////////////
		//					Method: ReadIndexItem
		///////////////////////////////////////////////////////////////
		public void ReadIndexItem(ref BinaryReader br, MapIndexString name)
		{
			try {
				offset     = br.BaseStream.Position;
				tagclass   = Reverse(new string(br.ReadChars(4)));
				ident      = br.ReadUInt32();
				metaoffset = br.ReadUInt32();
				size       = br.ReadUInt32();
				filename   = name._filename;
				fileoffset = name._offset;
			} catch {}
		}

		public static string Reverse(string tag)
		{
			return tag.Substring(3, 1) + tag.Substring(2, 1) + tag.Substring(1, 1) + tag.Substring(0, 1);
		}

		///////////////////////////////////////////////////////////////
		//					Method: SaveIndexItem
		///////////////////////////////////////////////////////////////
		public void SaveIndexItem(ref BinaryWriter bw)
		{
			try {
				// Save item
				bw.BaseStream.Position = offset;
				bw.Write(tagclass.ToCharArray());
				bw.Write(ident);
				bw.Write(metaoffset);
				bw.Write(size);
				
				// Save filename
				//bw.BaseStream.Position = fileoffset;
				//bw.Write(filename.ToCharArray());
				//bw.Write((byte)0);
			} catch {}
		}

		///////////////////////////////////////////////////////////////
		//					   Method: SaveMeta
		///////////////////////////////////////////////////////////////
		public bool SaveMeta(FileStream stream, string metafile, uint secmagic, uint beginmeta)
		{
			bool result = false;

			try {
				// Stream to new Meta File
				FileStream fsw = new FileStream(metafile, FileMode.Create, FileAccess.Write);
				stream.Seek(metaoffset - secmagic, SeekOrigin.Begin);
				byte[] bytes = new byte[size];
				stream.Read(bytes, 0, (int)size);
				fsw.Write(bytes, 0, (int)size);
				fsw.Close();
				result = true;
			} catch {}

			if (result) {
				// Save magic + metaoffset
				try {
					XmlTextWriter xw = new XmlTextWriter(metafile + ".xml", System.Text.Encoding.UTF8);
					xw.WriteComment("Created by Ch2r");
					xw.WriteStartElement("Information");
					xw.WriteElementString("Magic", secmagic.ToString("X8"));
					xw.WriteElementString("Meta", metaoffset.ToString("X8"));
					xw.WriteElementString("MapSize", stream.Length.ToString());
					xw.WriteElementString("BeginMeta", beginmeta.ToString());
					xw.WriteEndElement();
					xw.Close();
				} catch {}
			}

			return result;
		}

		public bool InjectMeta(FileStream map, FileStream meta, uint secmagic)
		{
			bool result = false;

			try {
				// Go to the correct offset
				map.Seek(metaoffset - secmagic, SeekOrigin.Begin);
				byte[] bytes = new byte[meta.Length];

				// Read the meta file
				meta.Read(bytes, 0, (int)bytes.Length);
				map.Write(bytes, 0, (int)bytes.Length);

				// Add zeroes if new size is smaller
				if (size != meta.Length) {
					bytes = new byte[size - meta.Length];
					map.Write(bytes, 0, (int)bytes.Length);
				}
				result = true;
			} catch {}

			return result;
		}

		/// <summary>
		/// Fixes the reflexives in the item by using the old meta's xml file.
		/// </summary>
		public bool FixReflexives(string xmlfile, FileStream stream, uint secmagic)
		{
			bool reflexivefixed = false;

			// Fix reflexives
			try 
			{
				XmlTextReader xtr = new XmlTextReader(xmlfile);
				uint oldmagic = 0;
				uint oldmeta = 0;
				uint oldmapsize = 0;
				uint oldbeginmeta = 0;

				// Get old magic and meta
				while (xtr.Read()) 
				{
					if (xtr.NodeType == XmlNodeType.Element) 
					{
						if (xtr.LocalName.Equals("Magic")) 
						{
							oldmagic = uint.Parse(xtr.ReadString(), NumberStyles.HexNumber);
						} 
						else if  (xtr.LocalName.Equals("Meta")) 
						{
							oldmeta = uint.Parse(xtr.ReadString(), NumberStyles.HexNumber);
						} 
						else if  (xtr.LocalName.Equals("MapSize")) 
						{
							oldmapsize = uint.Parse(xtr.ReadString());
						} 
						else if  (xtr.LocalName.Equals("BeginMeta")) 
						{
							oldbeginmeta = uint.Parse(xtr.ReadString());
						}
					}
				}
				oldbeginmeta -= oldmagic;
				xtr.Close();

				// Read through meta and find reflexives
				BinaryReader br = new BinaryReader(stream);
				BinaryWriter bw = new BinaryWriter(stream);
				br.BaseStream.Seek(MetaOffset - secmagic, SeekOrigin.Begin);

				long endpos = MetaOffset - secmagic + Size;

				while (br.BaseStream.Position < endpos) 
				{
					uint possiblecount = br.ReadUInt32();
					if (possiblecount < 2000) 
					{
						// Check for possible offset
						uint possibleoffset = br.ReadUInt32();
						uint offset = possibleoffset - oldmagic;
						br.BaseStream.Position -= 4;
						if (offset > oldbeginmeta && offset < oldmapsize) 
						{
							// We got a reflexive, subtract old magic and add new
							offset -= oldmeta + MetaOffset;
							bw.Write(offset);
						}
					}
				}

				reflexivefixed = true;
			} catch {}

			return reflexivefixed;
		}

		public byte[] GetMeta(FileStream stream, uint secmagic)
		{
			stream.Seek(metaoffset - secmagic, SeekOrigin.Begin);
			byte[] bytes = new byte[size];
			stream.Read(bytes, 0, (int)size);
			return bytes;
		}

		public string Filename
		{
			get {
				return filename;
			}
			set {
				filename = value;
			}
		}

		public long Offset
		{
			get {
				return offset;
			}
			set {
				offset = value;
			}
		}

		public uint MetaOffset
		{
			get {
				return metaoffset;
			}
			set {
				metaoffset = value;
			}
		}

		public void SetMetaOffset(uint meta)
		{
			metaoffset = meta;
		}

		public uint Size
		{
			get {
				return size;
			}
			set {
				size = value;
			}
		}

		public uint Ident
		{
			get {
				return ident;
			}
			set {
				ident = value;
			}
		}

		public string Tag
		{
			get {
				return tagclass;
			}
			set {
				tagclass = value;
			}
		}

		public void SetTag(string tag)
		{
			tagclass = tag;
		}

	}

	///////////////////////////////////////////////////////////////
	//					Structure: Reflexive
	///////////////////////////////////////////////////////////////
	public struct MapReflexive
	{
		uint _count;
		uint _offset;

		public void ReadReflexive(ref BinaryReader br)
		{
			_count = br.ReadUInt32();
			_offset = br.ReadUInt32();
		}

		public void ReadReflexive(ref BinaryReader br, long offset, SeekOrigin origin)
		{
			br.BaseStream.Seek(offset, origin);
			ReadReflexive(ref br);
		}
		
		public uint Count
		{
			get {
				return _count;
			}
		}

		public uint Offset
		{
			get {
				return _offset;
			}
		}

	}

	///////////////////////////////////////////////////////////////
	//					Class: Sound List
	///////////////////////////////////////////////////////////////
	public class SoundIndex
	{
		public SoundOption[] soundOptions;
		public SoundPiece[] soundPieces;
		public SoundPointer[] soundPointers;

		public SoundIndex()
		{}

		public void ReadSoundList(MapIndexItem ugh, FileStream fs, uint secMagic)
		{
			try 
			{
				uint metaStart = ugh.MetaOffset - secMagic;
				fs.Seek(metaStart + 0x20, SeekOrigin.Begin);
				BinaryReader br = new BinaryReader(fs);

				// Read sound options
				MapReflexive optionReflex = new MapReflexive();
				optionReflex.ReadReflexive(ref br);

				soundOptions = new SoundOption[optionReflex.Count];

				for (int i = 0; i < optionReflex.Count; i++)
				{
					fs.Seek((optionReflex.Offset - secMagic) + (i * 12) + 8, SeekOrigin.Begin);
					soundOptions[i].index = br.ReadUInt16();
					soundOptions[i].options = br.ReadUInt16();
				}

				// Read sound pieces
				MapReflexive piecesReflex = new MapReflexive();
				piecesReflex.ReadReflexive(ref br, metaStart + 0x28, SeekOrigin.Begin);

				soundPieces = new SoundPiece[piecesReflex.Count];

				for (int i = 0; i < piecesReflex.Count; i++)
				{
					fs.Seek((piecesReflex.Offset - secMagic) + (i * 16) + 12, SeekOrigin.Begin);
					soundPieces[i].index = br.ReadUInt16();
					soundPieces[i].pieces = br.ReadUInt16();
				}

				// Read sound pointers
				MapReflexive pointersReflex = new MapReflexive();
				pointersReflex.ReadReflexive(ref br, metaStart + 0x40, SeekOrigin.Begin);

				soundPointers = new SoundPointer[pointersReflex.Count];

				for (int i = 0; i < pointersReflex.Count; i++)
				{
					fs.Seek((pointersReflex.Offset - secMagic) + (i * 12), SeekOrigin.Begin);
					soundPointers[i].offset = br.ReadUInt32();
					soundPointers[i].size = br.ReadUInt32() & 0x7FFFFFFF;
					soundPointers[i].effect = br.ReadUInt32();
				}
			} 
			catch ( System.IO.EndOfStreamException )
			{
				System.Windows.Forms.MessageBox.Show("EOS");
			}
		}
	}

	public struct SoundMeta
	{
		public ushort format;
		public ushort index;
		public ushort chunkCount;

		public void ReadMeta(FileStream fs, uint offset)
		{
			fs.Seek(offset, SeekOrigin.Begin);
			BinaryReader br = new BinaryReader(fs);

			fs.Seek(4, SeekOrigin.Current); // Moves past unknown word
			format = (ushort)br.ReadByte();

			fs.Seek(3, SeekOrigin.Current); // Moves past unknown bytes
			index = br.ReadUInt16();
			chunkCount = (ushort)br.ReadByte();
		}
	}

	public struct SoundOption
	{
		public ushort index;
		public ushort options;
	}

	public struct SoundPiece
	{
		public ushort index;
		public ushort pieces;
		public int listIndex;
		public ushort format;

		public override string ToString()
		{
			return listIndex.ToString();
		}
	}

	public struct SoundPointer
	{
		public uint offset;
		public uint size;
		public uint effect;

		public uint GetLocation()
		{
			return offset >> 30;
		}

		public uint GetActualOffset()
		{
			return offset & 0x3fffffff;
		}
	}
}
