using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level
{
	internal class Player
	{
		private readonly uint mNickNameAddress;
		private readonly uint mExpAddress;
		private readonly uint mHPAddress;
		private readonly uint mMaxHPAddress;
		private readonly uint mStatusPointAddress;

		public Player(uint address)
		{
			var address_list = SaveData.Instance().FindAddress("NickName", address);
			if (address_list.Count > 0) mNickNameAddress = address_list[0];

			address_list = SaveData.Instance().FindAddress("Exp", address);
			if(address_list.Count > 0) mExpAddress = address_list[0] + 29;

			address_list = SaveData.Instance().FindAddress("HP", address);
			if (address_list.Count > 0) mHPAddress = address_list[0] + 101;

			address_list = SaveData.Instance().FindAddress("MaxHP", address);
			if (address_list.Count > 0) mMaxHPAddress = address_list[0] + 104;

			address_list = SaveData.Instance().FindAddress("UnusedStatusPoint", address);
			if (address_list.Count > 0) mStatusPointAddress = address_list[0] + 43;
		}

		public String Name
		{
			get
			{
				var length = (uint)SaveData.Instance().ReadNumber(mNickNameAddress + 34, 4);
				return SaveData.Instance().ReadText(mNickNameAddress + 38, length);
			}
		}

		public UInt64 Exp
		{
			get => SaveData.Instance().ReadNumber(mExpAddress, 4);
			set => SaveData.Instance().WriteNumber(mExpAddress, 4, value);
		}

		public UInt64 HP
		{
			get => SaveData.Instance().ReadNumber(mHPAddress, 8) / 1000;
			set => SaveData.Instance().WriteNumber(mHPAddress, 8, value * 1000);
		}

		public UInt64 MaxHP
		{
			get => SaveData.Instance().ReadNumber(mMaxHPAddress, 8) / 1000;
			set => SaveData.Instance().WriteNumber(mMaxHPAddress, 8, value * 1000);
		}

		public UInt64 StatusPoint
		{
			get => SaveData.Instance().ReadNumber(mStatusPointAddress, 4);
			set => SaveData.Instance().WriteNumber(mStatusPointAddress, 4, value);
		}
	}
}
