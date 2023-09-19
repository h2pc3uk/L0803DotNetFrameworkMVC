using System.Collections.Generic;

namespace L0803DotNetFrameworkMVC.Models
{
    // L0803TOTA資料模型
    public class L0803TOTAModel
    {
        public string CNAME { get; set; }  //帳戶中文名稱
        public List<PayablePerPeriod> PayDatas { get; set; } //每期應繳資訊
        public string Message { get; set; } = string.Empty; //視需要回傳訊息


    }

    // 每期應繳金額
    public class PayablePerPeriod
    {
        public string SDate { get; set; }
        public string EDate { get; set; }
        public string Dycnt { get; set; }
        public string Fitirt { get; set; }
        public string Amt { get; set; }
        public string Int { get; set; }
        public string Dfamt { get; set; }
        public string Dfdays { get; set; }
        public string Diamt { get; set; }
        public string Flag { get; set; }
        public string Pramt { get; set; }
        public string Subtotal { get; set; }
        public string RecCount { get; set; } // 記錄或傳回該筆期數
    }
}