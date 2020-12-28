using PadHelp.DataMap;
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
                    var responseResult = await ReadHttpAsync(string.Format("https://pad.skyozora.com/pets/{0}", item));
                    //取得寵物資料
                    string PetId = GetPadMSG(responseResult.ToString());

                    //開頭: https://pad.skyozora.com/images/pets/編號.png
                    //下載圖片
                    StartToDownPicture(PetId);
                }
            }
            else if (HttpAddrs_txt.Text == "" && !HttpAddrs_txt.Text.Contains("https://pad.skyozora.com/pets/"))
            {
                for (int item = 6884; item <= 6890; item++)
                {
                    
                    //下載圖片
                    StartToDownPicture(item.ToString());
                }
            }
            else
            {
                Console.WriteLine("請輸入網址");
            }
        }

        async Task<string> ReadHttpAsync(string HttpString)
        {
            string responseResult = "";
            try
            {
                HttpClient httpClient = new HttpClient();
                var responseMessage = await httpClient.GetAsync(HttpString); //發送請求

                //檢查回應的伺服器狀態StatusCode是否是200 OK
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    responseResult = responseMessage.Content.ReadAsStringAsync().Result;//取得內容
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("錯誤訊息: /n{0}", ex));
            }
            return responseResult;
        }

        string GetPadMSG(string responseResult)
        {
            PadMsg RPadMsg = new PadMsg();

            //寵物ID
            string pattern = "<h3 style=\"font-size:13px;line-height:14px; margin-top:2px\">(.*)</h3>";
            Regex regex = new Regex(pattern);
            Match match = Regex.Match(responseResult, pattern);
            //Group g = match.Groups[1];
            //Console.WriteLine(match.Groups[1]);
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
            Match match2 = Regex.Match(match.Groups[1].ToString(), pattern);
            Console.WriteLine(match2.Groups["padid"].Value);

            return match2.Groups["padid"].Value;
        }

        //下載圖片檔案至本機
        void StartToDownPicture(string PetId)
        {
            System.Net.WebClient WC = new System.Net.WebClient();
            string WebPath = string.Format("https://pad.skyozora.com/images/pets/{0}.png", PetId);
            try
            {
                System.IO.MemoryStream Ms = new System.IO.MemoryStream(WC.DownloadData(WebPath));
                Image img = Image.FromStream(Ms);
                var test = img.Tag;
                string DownLoadPIC = string.Format("../../PetsPIC/{0}.png", PetId);
                Console.WriteLine(test);
                Console.WriteLine(DownLoadPIC);
                bool HaveThisPIC = System.IO.File.Exists(DownLoadPIC);
                if (!HaveThisPIC)
                {
                    //img.Save(string.Format("{0}/{1}", DownLoadPath,"6539"));
                    img.Save(DownLoadPIC);
                    Console.WriteLine("下載成功: " + PetId);
                }
                else
                {
                    Console.WriteLine("下載失敗: " + PetId);
                }
            }
            catch (Exception)
            {
                Console.WriteLine(string.Format("無法下載:{0}", WebPath));
            }

        }

        private void Test_btn_Click(object sender, EventArgs e)
        {
            StartToDownPicture("https://pad.skyozora.com/images/pets/6539.png");
        }
    }
}
