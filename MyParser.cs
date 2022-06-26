using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace VKR_Abrashkov_V_V
{
    internal class MyParser
    {
        private readonly DB db = new DB();

        public string RenewParse()
        {
            var str = new StringBuilder();
            str.Append("Авокадо 1шт - ").Append(Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/avokado-1sht-5ka.html")).Append("\n");
            str.Append("Апельсины 1кг - ").Append(Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/apelsiny-1kg-5ka.html")).Append("\n");
            str.Append("Бедро индейки 1кг - ").Append(Parse5("https://pyaterochkaakcii.ru/katalog/myaso-i-ptica/bedro-indilayt-indeyki-1kg-5ka.html")).Append("\n");
            str.Append("Курс евро - ").Append(ParseCur("RY0101095000946869")).Append("\n");
            str.Append("Курс доллара - ").Append(ParseCur("RY0101095000839420")).Append("\n");
            str.Append("Курс фунта - ").Append(ParseCur("RY0101095000839173")).Append("\n");
            str.Append("Курс йены - ").Append(ParseCur("RY0101095000851534")).Append("\n");
            str.Append("Курс юаня - ").Append(ParseCur("RY01010950002456041")).Append("\n");
            return str.ToString();
        }

        public void addPrice()
        {
            var item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/kapusta-rannyaya-belokochannaya-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/kartofel-ranniy-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/morkov-mytaya-1-upakovka-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/ogurcy-korotkoplodnye-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/redis-krasnyy-500g-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/svekla-mytaya-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/tomaty-rozovye-700g-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/yabloko-krasnoe-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/avokado-1sht-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));

            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/arbuzy-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/apelsiny-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/baklazhan-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/vinograd-krasnyy-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/granat-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/griby-shampinony-500g-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
            item = Parse5("https://pyaterochkaakcii.ru/katalog/ovoshchi-i-frukty/grusha-sezonnaya-1kg-5ka.html");
            db.updPyat(string.Format("{0:C2}", item.Item2), item.Item1.Substring(11));
        }

        public (string, string) Parse5(string url)
        {
            using (HttpClientHandler hdl = new HttpClientHandler { AllowAutoRedirect = false, AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.None })
            {
                using (var clnt = new HttpClient(hdl))
                {
                    using (var resp = clnt.GetAsync(url).Result)
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var html = resp.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(html))
                            {
                                var doc = new HtmlAgilityPack.HtmlDocument();
                                doc.LoadHtml(html);

                                var res = doc.DocumentNode.Descendants("div").FirstOrDefault(o => o.GetAttributeValue("class", "") == "description").InnerText;

                                var res2 = doc.DocumentNode.Descendants("div").FirstOrDefault(o => o.GetAttributeValue("class", "") == "price").InnerText;
                                if (res != null)
                                {
                                    var price = new StringBuilder(res2.Trim());

                                    var i = res.IndexOf("Код товара");
                                    var s = res.Substring(10, i + "Код товара".Length + 8);
                                    var li = s.Split('\n');
                                    return (string.Format(li[5].Trim()), price.Remove(0, 21).ToString());
                                }
                            }
                        }
                    }
                }
            }
            return ("", "");
        }

        public string getAllCur(bool internet = true)
        {
            var str = new StringBuilder();
            if (internet)
            {
                str.Append("Курс евро EUR/RUB €/₽\n\t").Append(ParseCur("RY0101095000946869")).Append("\n");
                str.Append("__________________________________\n");
                str.Append("Курс доллара USD/RUB $/₽\n\t").Append(ParseCur("RY0101095000839420")).Append("\n");
                str.Append("__________________________________\n");
                str.Append("Курс фунта GBP/RUB £/₽\n\t").Append(ParseCur("RY0101095000839173")).Append("\n");
                str.Append("__________________________________\n");
                str.Append("Курс йены JPY/RUB ¥/₽\n\t").Append(ParseCur("RY0101095000851534")).Append("\n");
                str.Append("__________________________________\n");
                str.Append("Курс юаня CNY/RUB ¥/₽\n\t").Append(ParseCur("RY01010950002456041")).Append("\n");
            }
            else
            {
                str.Append("Курс евро EUR/RUB €/₽\n\t").Append("62.63").Append("\n");
                str.Append("__________________________________\n");
                str.Append("Курс доллара USD/RUB $/₽\n\t").Append("58.57").Append("\n");
                str.Append("__________________________________\n");
                str.Append("Курс фунта GBP/RUB £/₽\n\t").Append("73.73").Append("\n");
                str.Append("__________________________________\n");
                str.Append("Курс йены JPY/RUB ¥/₽\n\t").Append("0.45").Append("\n");
                str.Append("__________________________________\n");
                str.Append("Курс юаня CNY/RUB ¥/₽\n\t").Append("8.80").Append("\n");
            }
            return str.ToString();
        }

        public string ParseCur(string s)
        {
            var str = new StringBuilder(@"data-room=""");
            str.Append(s);
            str.Append(@""" style=""display: inline;""><span class=""push-data "" data-format=""minimumFractionDigits:4;maximumFractionDigits:4"" data-animation="""">(\d+\,\d{4})</span> </div></td>");
            var wc = new WebClient();
            try
            {
                string resp = wc.DownloadString("https://www.finanz.ru/valyuty/v-realnom-vremeni");
                string sell = System.Text.RegularExpressions.Regex.Match(resp, str.ToString()).Groups[1].Value;
                return sell;
            }
            catch { return ""; }
        }
    }
}