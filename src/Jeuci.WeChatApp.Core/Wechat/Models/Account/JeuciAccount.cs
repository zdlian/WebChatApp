﻿using System;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Logging;
using Jeuci.WeChatApp.Wechat.Authentication;
using Senparc.Weixin;
using Senparc.Weixin.MP.CommonAPIs;

namespace Jeuci.WeChatApp.Wechat.Models.Account
{
    public class JeuciAccount
    {
        private readonly string m_openId;
        private readonly string m_account;
        private readonly string m_password;

        /*public JeuciAccount(WechatAccount wechatAccount)
        {
            UserWechatInfo = wechatAccount;
        }*/

        public JeuciAccount(string openId)
        {
            m_openId = openId;
        }

        public JeuciAccount(string openId, string account, string userPassword) : this(openId)
        {
         //   m_openId = openId;
            m_account = account;
            m_password = userPassword;         
        }

        public bool IsBindWechat { get; }

        public bool IsBindEmail { get; }

        public string OpenId
        {
            get { return UserWechatInfo.OpenId; }
        }

        public string BindWechatAddress
        {
            get
            {
                return "/wechat/account/#/bindwechat";
            }
        }

        public UserInfo UserInfo { get; set; }

        public WechatAccount UserWechatInfo { get; private set; }

        public void SynchronWechatUserInfo(IWechatAuthentManager wechatAuthentManager)
        {
            if (string.IsNullOrEmpty(m_openId))
            {
                LogHelper.Logger.Error("OpenId为空，用户可能不是从合法的途径进入到该站点!");
                throw new Exception("OpenId不能为空，请从合法的途径进入该站点");
            }
            var userInfoResult = CommonApi.GetUserInfo(wechatAuthentManager.GetAccessToken(), m_openId);
            if (userInfoResult.errcode != ReturnCode.请求成功)
            {
                LogHelper.Logger.Error("请求用户的基本信息失败，原因:" + userInfoResult.errcode);
                throw new Exception("请求用户的基本信息失败，原因:" + userInfoResult.errcode);
            }
            UserWechatInfo = userInfoResult.MapTo<WechatAccount>();
        }
    }
}
