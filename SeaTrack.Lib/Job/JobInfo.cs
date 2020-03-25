using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using RestSharp;
using SeaTrack.Lib.Service;

namespace SeaTrack.Lib.Job
{
    public class JobInfo : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello, JOb info");
            var lstDevice = TrackDataService.GetListDeviceStatus();
            if (lstDevice != null)
            {
                foreach (var item in lstDevice)
                {
                    try
                    {
                        var client = new RestClient("http://192.168.1.1");
                        var request = new RestRequest("api/item/", Method.POST);
                        request.RequestFormat = DataFormat.Json;
                        request.AddBody(new
                        {
                            MRFF = "1",
                            Seqno = "1",
                            ID = item.DeviceID,
                            Time = item.TransmitTime.ToString("HHmmss"),
                            item.State,
                            item.Latitude,
                            ExpSN = item.DirectionSN,
                            item.Longitude,
                            ExpEW = item.DirectionEW,
                            item.Speed,
                            DIR = "",
                            Date = item.TransmitTime.ToString("ddMMyyyy")
                        });
                        request.Timeout = 30 * 1000;
                        var rs = client.Execute(request);
                        if (rs.StatusCode == HttpStatusCode.OK)
                        {
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
            }
        }
    }
}
