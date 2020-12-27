﻿using PadHelp.DataMap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PadHelp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Read_ClickAsync(object sender, EventArgs e)
        {
            if (HttpAddrs_txt.Text != "" && !HttpAddrs_txt.Text.Contains("https://pad.skyozora.com/pets/"))
            {
                string[] InputPadid = HttpAddrs_txt.Text.Split(',');
                foreach (var item in InputPadid)
                {
                    //讀URL   HttpAddrs_txt.Text
                    ReadHttpAsync(string.Format("https://pad.skyozora.com/pets/{0}", item));
                }
            }
            else if (HttpAddrs_txt.Text != "" && HttpAddrs_txt.Text.Contains("https://pad.skyozora.com/pets/"))
            {
                for (int i = 100; i <= 110; i++)
                {
                    //讀URL   HttpAddrs_txt.Text
                    ReadHttpAsync(string.Format("https://pad.skyozora.com/pets/{0}", i));
                }
            }
            else
            {
                Console.WriteLine("請輸入網址");
            }
        }

        async void ReadHttpAsync(string HttpString)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var responseMessage = await httpClient.GetAsync(HttpString); //發送請求

                //檢查回應的伺服器狀態StatusCode是否是200 OK
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseResult = responseMessage.Content.ReadAsStringAsync().Result;//取得內容

                    //Console.WriteLine(string.Format("獲得訊息: /n{0}", responseResult));
                    //ShowMSG_txt.Text = responseResult;

                    //取得寵物資料
                    GetPadMSG(responseResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("錯誤訊息: /n{0}", ex));
            }
        }

        private void GetPadMSG(string responseResult)
        {
            PadMsg RPadMsg = new PadMsg();

            //寵物ID
            string pattern = "<h3 style=\"font-size:13px;line-height:14px; margin-top:2px\">(.*)</h3>";
            Regex regex = new Regex(pattern);
            Match match = Regex.Match(responseResult, pattern);
            //Group g = match.Groups[1];
            Console.WriteLine(match.Groups[1]);
            string result = match.Groups[1].ToString();
            if (ShowMSG_txt.Text != "")
            {
                result = string.Format("{0}\r\n{1}", ShowMSG_txt.Text, match.Groups[1]);
            }
            ShowMSG_txt.Text = result;

            //寵物圖片:
            pattern = @"[^No.]*(?<padid>.*)*[\s-$]";
            pattern = @"[^No.]*\.(?<padid>[^\s]*)(?<blank>\s*)(.*)";
            Regex regex2 = new Regex(pattern);
            Match match2 = Regex.Match(result, pattern);
            Console.WriteLine(match2.Groups["padid"].Value);
            
            //開頭: https://pad.skyozora.com/images/pets/編號.jpg
        }
    }
}
