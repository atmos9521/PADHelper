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

        private async void Test_btn_ClickAsync(object sender, EventArgs e)
        {
            var responseResult = await ReadHttpAsync(string.Format("https://pad.skyozora.com/pets/{0}", 3161));
            //id + 日文名稱
            string pattern = "<h3 style=\"font-size:13px;line-height:14px; margin-top:2px\">(.*)</h3>";
            Regex regex = new Regex(pattern);
            Match match = Regex.Match(responseResult, pattern);
            string result = match.Groups[1].ToString();

            //中文名稱
            pattern = "<h2 style=\"font-size:20px; line-height:20px; margin-top: 2px; margin-bottom:7px\">(.*)</h2>";
            match = Regex.Match(responseResult, pattern);
            var name = match.Groups[1];

            //星數
            pattern = @"★(.*)★";
            match = Regex.Match(responseResult, pattern);
            var start = match.Groups[0];

            //type
            pattern = @"★(.*)★";
            match = Regex.Match(responseResult, pattern);
            var type = match.Groups[0];

            //顯示用
            //if (ShowMSG_txt.Text != "")
            //{
            //    result = string.Format("{0}\r\n{1}", ShowMSG_txt.Text, match.Groups[1]);
            //    pattern = @"[^No.]*(?<padid>.*)*[\s-$]";
            //    pattern = @"[^No.]*\.(?<padid>[^\s]*)(?<blank>\s*)(.*)";
            //}
            ShowMSG_txt.Text = string.Format("中文name: {0}\r\n" +
                                             "star: {1}"
                                             , name, start);
        }

        private async void Awake_btn_ClickAsync(object sender, EventArgs e)
        {
            var responseResult = await ReadHttpAsync(@"https://pad.skyozora.com/skill/%E8%A6%BA%E9%86%92%E6%8A%80%E8%83%BD%E4%B8%80%E8%A6%BD/");
            
            string [] patternArray = { "<td><a href=\"", "</td></tr><tr>" };
            string[] results = responseResult.Split(patternArray,StringSplitOptions.None);
            string pattern = "skill/(?<skillpath>.*)\\\" class=\\\"" +
                             "tooltip\\\" title=\\\"(?<skillid>.*)\\\">" +
                             "<img src=\\\"(?<skillpic>.*)\\\"></a></td>.*</a></td><td>" +
                             "(?<skilleffect>.*)";
            int count = 1;
            foreach (var item in results)
            {
                Match match = Regex.Match(item, pattern);
                if (match.Groups.Count != 1)
                {
                    Console.WriteLine("------------"+count+"--------------");
                    Console.WriteLine(match.Groups["skillpath"].Value);
                    Console.WriteLine(match.Groups["skillpic"].Value);//https://pad.skyozora.com/
                    Console.WriteLine(match.Groups["skilleffect"].Value);
                    count++;
                }
            }
        }

        private void Awake_btn_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Console.WriteLine("最小化");
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                Console.WriteLine("最大化");
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string item = "aaaassssdfe1561658fefeaaass58d";
            string pattern = "ssd(?<test>.*)e156";
            Match match = Regex.Match(item, pattern);
            Console.WriteLine(match.Groups["test"].Value);
        }
    }
}
