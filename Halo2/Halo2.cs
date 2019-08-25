using System;
using System.IO;

namespace Halo2
{
	///////////////////////////////////////////////////////////////
	//					Structure: Reflexive
	///////////////////////////////////////////////////////////////
	/// <summary>
	/// Represents a Reflexive in a Halo 2 Map.
	/// </summary>
	public struct Reflexive
	{
		uint _count;
		uint _offset;
		uint _secmagic;

		/// <summary>
		/// Read the reflexive.
		/// </summary>
		public void ReadReflexive(ref BinaryReader br, uint secMagic)
		{
			_secmagic = secMagic;
			_count = br.ReadUInt32();
			_offset = br.ReadUInt32() - _secmagic;
		}

		/// <summary>
		/// Read the reflexive.
		/// </summary>
		public void ReadReflexive(ref BinaryReader br, uint secMagic, long offset, SeekOrigin origin)
		{
			br.BaseStream.Seek(offset, origin);
			ReadReflexive(ref br, secMagic);
		}
		
		/// <summary>
		/// Get/set the count of the items in the reflexive.
		/// </summary>
		public uint Count
		{
			get {
				return _count;
			}
			set {
				_count = value;
			}
		}

		/// <summary>
		/// Get/set the offset where the reflexive points to.
		/// </summary>
		public uint Offset
		{
			get {
				return _offset;
			}
			set {
				_offset = value;
			}
		}
	}

	/// <summary>
	/// Swapping an item or a reflexive?
	/// </summary>
	public enum SwapType
	{
		Reflexive,
		Item,
	}
}
