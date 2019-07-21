using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace WebApplication5.Controllers
{
    public class TalkController : ApiController
    {
        ///// <summary>
        ///// 推送消息
        ///// </summary>
        ///// <returns></returns>
        [HttpGet]
        public HttpResponseMessage SentNews()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Access-Control-Allow-Origin", "*");//如需要跨域可配置
            string data_str = "good day";//当然可以是json字符串格式
            string even = "", data = "";
            if (!string.IsNullOrWhiteSpace(data_str))
            {
                even = "event:sentMessage\n";
                data = "data:" + data_str + "\n\n";
            }
            string retry = "retry:" + 1000 + "\n";//连接断开后重连时间（毫秒），其实可以理解为轮询时间 2333...
            byte[] array = Encoding.UTF8.GetBytes(even + data + retry);
            Stream stream_result = new MemoryStream(array);
            response.Content = new StreamContent(stream_result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/event-stream");//此处一定要配置
            response.Headers.Add("Connection", "keep-alive");
            response.Headers.CacheControl = new CacheControlHeaderValue();
            response.Headers.CacheControl.NoCache = true;
            
            return response;
        }
    }
}
