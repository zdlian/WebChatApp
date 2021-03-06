﻿using System.Collections.Generic;

namespace Jeuci.WeChatApp.Lottery.Models
{
    public class ServerPriceInfo
    {
        public string ServiceName { get; set; }

        public string UserName { get; set; }

        public IList<ServerPrice> ServerPrices { get; set; }

        public IList<string> DescriptionList { get; set; }
    }
}