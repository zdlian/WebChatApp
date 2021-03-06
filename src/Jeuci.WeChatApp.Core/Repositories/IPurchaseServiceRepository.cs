﻿using System.Collections.Generic;
using Abp.Domain.Repositories;
using Jeuci.WeChatApp.Common.Enums;
using Jeuci.WeChatApp.Pay.Models;

namespace Jeuci.WeChatApp.Repositories
{
    public interface IPurchaseServiceRepository : IRepository
    {
        bool GeneratePayOrder(PayOrder payOrder);

        int CompleteServiceOrder(CompleteServiceOrder payOrder);

        int UpdateServiceOrder(UpdateServiceOrder order);

        IList<UserPayOrderInfo> GetNeedQueryOrderList(PayMode mobileWeb);
    }
}