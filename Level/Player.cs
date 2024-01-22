﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level
{
	internal class Player
	{
		private readonly uint mExpAddress;
		private readonly uint mHPAddress;
		private readonly uint mMaxHPAddress;

		public Player(uint address)
		{
			var address_list = SaveData.Instance().FindAddress("Exp", address);
			if(address_list.Count > 0) mExpAddress = address_list[0] + 29;

			address_list = SaveData.Instance().FindAddress("HP", address);
			if (address_list.Count > 0) mHPAddress = address_list[0] + 101;

			address_list = SaveData.Instance().FindAddress("MaxHP", address);
			if (address_list.Count > 0) mMaxHPAddress = address_list[0] + 104;
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
	}
}