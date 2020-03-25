using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SeaTrack.Lib.DTO;
using SeaTrack.Models;

namespace SeaTrack.Controllers
{
    public class TrackDataController : ApiController
    {
        private decimal makePosition(Int32 l)
        {
            Int32 a, c;
            decimal b, d, f, h;
            a = l / 10000;
            b = l % 10000;
            c = a / 100;
            d = a % 100;
            f = b / 10000;
            h = c + ((d + f) / 60);
            return h;
        }
        [HttpPost]
        public HttpResponseMessage AddTrackData([FromBody]TrackDataRq rq)
        {
            var byteData = Enumerable.Range(0, rq.data.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(rq.data.Substring(x, 2), 16))
                     .ToArray();

            var data = new TrackData();
            data.DeviceNo = rq.imei;

            byte[] bb = Enumerable.Range(0, rq.data.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(rq.data.Substring(x, 2), 16))
                     .ToArray();
            var dataStr = System.Text.Encoding.ASCII.GetString(bb);

            int _Latitude = Convert.ToInt32(rq.data.Substring(0, 8), 16);
            int _Longitude = Convert.ToInt32(rq.data.Substring(10, 8), 16);
            data.Longitude = makePosition(_Longitude);
            data.Latitude = makePosition(_Latitude);

            if (data.Latitude <= 0 || data.Longitude < 0)
            {
                data.Latitude = rq.iridium_latitude;
                data.Longitude = rq.iridium_longtitude;
            }
            
            data.DirectionSN = System.Text.Encoding.UTF8.GetString(byteData.Skip(4).Take(1).ToArray());            
            data.DirectionEW = System.Text.Encoding.UTF8.GetString(byteData.Skip(9).Take(1).ToArray());
            data.Speed = Convert.ToInt32(rq.data.Substring(20, 2), 16);
            var HH = Convert.ToInt32(rq.data.Substring(22, 2), 16);
            var MM = Convert.ToInt32(rq.data.Substring(24, 2), 16);
            var ss = Convert.ToInt32(rq.data.Substring(26, 2), 16);

            // Error Parse Datetime
            // 
            var _TransmitTime = DateTime.ParseExact(rq.transmit_time.Substring(0,8),"yy-MM-dd", CultureInfo.InvariantCulture);
            data.TransmitTime = new DateTime(_TransmitTime.Year, _TransmitTime.Month, _TransmitTime.Day,HH,MM,ss);
            //data.TransmitTime = new DateTime(yy, mm, dd, HH, MM, ss);

            Lib.Service.TrackDataService.AddTrackData(data);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
