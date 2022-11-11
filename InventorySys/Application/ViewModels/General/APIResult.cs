using static Domain.Misc.EnumsData;

namespace Application.ViewModels.General
{
    public class APIResult
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ResponseCodes ResponseCode { get; set; }
        public int TotalRecords { get; set; }
    }
}
