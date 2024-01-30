using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level
{
	internal class Item
	{
		private readonly uint mNameAddress;
		private readonly uint mCountAddress;

		public Item(uint address)
		{
			var address_list = SaveData.Instance().FindAddress("NameProperty", address);
			if (address_list.Count > 0) mNameAddress = address_list[0] + 22;
			address_list = SaveData.Instance().FindAddress("StackCount", address);
			if (address_list.Count > 0) mCountAddress = address_list[0] + 36;
		}

		public String Name
		{
			get
			{
				var length = (uint)SaveData.Instance().ReadNumber(mNameAddress, 4);
				return SaveData.Instance().ReadText(mNameAddress + 4, length);
			}
		}

		public UInt64 Count
		{
			get => SaveData.Instance().ReadNumber(mCountAddress, 4);
			set => SaveData.Instance().WriteNumber(mCountAddress, 4, value);
		}
	}
}
