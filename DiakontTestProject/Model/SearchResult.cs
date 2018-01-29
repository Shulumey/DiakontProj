using System;

namespace DiakontTestProject.Model
{
    public class SearchResult
    {
        public string Department { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int TotalFundOfSalary { get; set; }
    }
}
