using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web.Services.Description;
using System.Xml.Linq;
using L0803DotNetFrameworkMVC.Models;

namespace L0803DotNetFrameworkMVC.Controllers
{
    public class L0803Controller : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
        
        // Get hello world string
        public JsonResult HelloWorld()
        {
            return Json(new { Message = "Hello World" }, JsonRequestBehavior.AllowGet);
        }

        // Get Account Data By entered account id
        [HttpPost]
        public JsonResult FetchAccountData(string accountId)
        {
            try
            {
                L0803TOTAModel model = new L0803TOTAModel();
                model.PayDatas = new List<PayablePerPeriod>();

                const string noData = "無資料";

                XDocument xdoc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/data/L0803TOTA.xml"));

                XNamespace ns = "http://www.cedar.com.tw/bluestar/";

                var blueStarElement = xdoc.Descendants(ns + "BlueStar");
                var lastTOTAElement = blueStarElement.Elements(ns + "TOTA").LastOrDefault();

                if (lastTOTAElement != null)
                {
                    int resCount = 0;

                    // model.CNAME = lastTOTAElement.Descendants(ns + "IT-TOTAL0803-CNAME").LastOrDefault().Value;
                    // add null-check
                    var cnameElemet = lastTOTAElement.Descendants(ns + "IT-TOTAL0803-CNAME").LastOrDefault();
                    model.CNAME = cnameElemet?.Value ?? noData;
                    foreach (var recElement in lastTOTAElement.Elements(ns + "rec"))
                    {
                        resCount++;
                        PayablePerPeriod pay = new PayablePerPeriod();
                        pay.SDate = recElement.Element(ns + "IT-TOTAL0803-SDATE")?.Value ?? noData;
                        pay.EDate = recElement.Element(ns + "IT-TOTAL0803-EDATE")?.Value ?? noData;
                        pay.Dycnt = recElement.Element(ns + "IT-TOTAL0803-DYCNT")?.Value ?? noData;
                        pay.Fitirt = recElement.Element(ns + "IT-TOTAL0803-FITIRT")?.Value ?? noData;
                        pay.Amt = recElement.Element(ns + "IT-TOTAL0803-AMT")?.Value ?? noData;
                        pay.Int = recElement.Element(ns + "IT-TOTAL0803-INT")?.Value ?? noData;
                        pay.Dfamt = recElement.Element(ns + "IT-TOTAL0803-DFAMT")?.Value ?? noData;
                        pay.Dfdays = recElement.Element(ns + "IT-TOTAL0803-DFDAYS")?.Value ?? noData;
                        pay.Diamt = recElement.Element(ns + "IT-TOTAL0803-DIAMT")?.Value ?? noData;
                        pay.Flag = recElement.Element(ns + "IT-TOTAL0803-FLAG")?.Value ?? noData;
                        pay.Pramt = recElement.Element(ns + "IT-TOTAL0803-PRAMT")?.Value ?? noData;
                        pay.Subtotal = recElement.Element(ns + "IT-TOTAL0803-SUBTOTAL")?.Value ?? noData;
                        pay.RecCount = resCount.ToString();
                        model.PayDatas.Add(pay);
                    }

                    // If you want to return the model to a view
                    // return View(model);

                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // 取不到資料回傳 Null
                    model.Message = "未取得資料";

                    // If you want to return the model to a view
                    // return View(model);

                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Message = $"An Error occurred: {ex.Message}" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}