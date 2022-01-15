using System.Collections.Generic;

namespace OSY.Model
{
    public class General<T>
    {
        public bool IsSuccess { get; set; }
        public T Entity { get; set; }
        public List<T> List { get; set; }
        public int TotalCount { get; set; }
        public int SumPageNumber { get; set; }
        public List<string> ValidationErrorList { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
