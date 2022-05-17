namespace ReckonAPI.Models
{
    public class SubmitResult
    {
        public string candidate { get; set; } = "Michael Olofernes";
        public string text { get; set; }
        public List<SubTextResult> results { get; set; }
        

    }
}
