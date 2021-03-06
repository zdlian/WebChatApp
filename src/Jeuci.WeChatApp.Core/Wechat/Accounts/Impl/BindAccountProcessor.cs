﻿using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Jeuci.WeChatApp.Common;
using Jeuci.WeChatApp.Common.Enums;
using Jeuci.WeChatApp.Wechat.Authentication;
using Jeuci.WeChatApp.Wechat.Models.Account;
using Jeuci.WeChatApp.Wechat.Policy;
using Senparc.Weixin.MP.CommonAPIs;

namespace Jeuci.WeChatApp.Wechat.Accounts.Impl
{
    public class BindAccountProcessor : IBindAccountProcessor
    {
        private readonly IWechatAuthentManager  _wechatAuthentManager;
        private readonly IRepository<UserInfo> _userRepository;

        public BindAccountProcessor(IWechatAuthentManager wechatAuthentManager, 
            IRepository<UserInfo> userRepository)
        {
            _wechatAuthentManager = wechatAuthentManager;
            _userRepository = userRepository;
        }

        public bool BindWechatAccount(JeuciAccount account,out string urlOrMsg)
        {
            //1.判断用户输入的账号是否存在
            //2.微信号是否已经被绑定

            account.SynchronWechatUserInfo(_wechatAuthentManager);

            account.SynchronUserInfo(_userRepository);

            var accountPolicy = new JeuciAccountPolicy(account);
            if (!accountPolicy.ValidAccountLegality(out urlOrMsg))
            {
                return false;
            }

            return accountPolicy.BindWechatAccount(_userRepository, out urlOrMsg);
        }

        public bool UnbindWechatAccount(JeuciAccount jeuciAccount, out string urlOrMsg)
        {
            jeuciAccount.SynchronWechatUserInfo(_wechatAuthentManager);
            jeuciAccount.SynchronUserInfo(_userRepository);
            var accountPolicy = new JeuciAccountPolicy(jeuciAccount);
            if (!accountPolicy.ValidCanUnbindAccount(out urlOrMsg))
            {
                return false;
            }

            return accountPolicy.UnBindWechatAccount(_userRepository, out urlOrMsg);
        }
    }
}