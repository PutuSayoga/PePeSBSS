using BackEnd.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace BackEnd.Helper
{
    public class ZensivaService : INotif
    {
        private Response Deserialize(string data)
        {
            var serializer = new XmlSerializer(typeof(Response));
            Response deserial;
            using (var reader = new StringReader(data.Trim()))
            {
                deserial = (Response)serializer.Deserialize(reader);
            }
            return deserial;
        }

        public string SendNotif(string noTelp, string text)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string userKey = "5gq0j4";
                    string passKey = "kjcugqr0j7";
                    string sUrl = string.Format("https://reguler.zenziva.net/apps/smsapi.php?userkey={0}&passkey={1}&nohp={2}&pesan={3}",
                        userKey, passKey, noTelp, text);
                    string resp = client.DownloadString(sUrl);
                    Response respObj = Deserialize(resp);
                    if(respObj.Message.Status != 0)
                    {
                        return $"Pengiriman pesan gagal ({respObj.Message.Text})";
                    }
                    else
                    {
                        return $"Pesan telah terkirim";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    [XmlRoot("response")]
    public class Response
    {
        [XmlElement("message")]
        public Message Message { get; set; }
    }
    [XmlRoot("message")]
    public class Message
    {
        [XmlElement("status")]
        public int Status { get; set; }
        [XmlElement("text")]
        public string Text { get; set; }
    }
}
