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
    public class InfoController : ApiController
    {
        
        [HttpPost]
        public HttpResponseMessage Communication([FromBody]InfoDTO rq)
        {
            switch (rq.MREF)
            {
                case "IMG":
                {
                    Lib.Service.TrackDataService.AddMessage(rq.ID, rq.Message);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                case "2":
                    var rs= Lib.Service.TrackDataService.GetLastedLocation(int.Parse(rq.ID));
                    if (rs != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new
                            {
                                rq.MREF,
                                rq.Seqno,
                                rq.ID,
                                Time = rs.TransmitTime.ToString("hhMMss"),
                                State = rs.State,
                                rs.Latitude,
                                rs.Longitude,
                                rs.Speed,
                                Date = rs.TransmitTime.ToString("ddmmyyyy")
                            });
                    }
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                case "6":
                    var val = Lib.Service.TrackDataService.AddConfigTime(rq.ID,rq.OpCode,rq.Time);
                    return Request.CreateResponse(HttpStatusCode.OK, new {Results = val > 0 ? "OK" : "Fail"});
            

            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }

    }
}
