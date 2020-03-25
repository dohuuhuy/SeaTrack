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
    public class JobSOS : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello, JOb executed");
            var lstSOS = SOSService.GetSOSPending();
            if (lstSOS != null)
            {
                foreach (var item in lstSOS)
                {
                    try
                    {
                        var client = new RestClient("http://192.168.1.1");
                        var request = new RestRequest("api/item/", Method.POST);
                        request.RequestFormat = DataFormat.Json;
                        request.AddBody(new
                        {
                            MRFF = "3",
                            ID = item.DeviceID,
                            Space = "1",
                            item.Latitude,
                            ExpSN = item.DirectionSN,
                            item.Longitude,
                            ExpEW = item.DirectionEW,
                            Date = item.DateRequest.ToString("dd/MM/yyyy"),
                            Time = item.DateRequest.ToString("HH:mm:ss"),
                            item.GMT
                        });
                        request.Timeout = 30 * 1000;
                        var rs = client.Execute(request);
                        if (rs.StatusCode == HttpStatusCode.OK)
                        {
                            item.Status = 1;
                            SOSService.UpdateStatusSOSbyID(item);
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
