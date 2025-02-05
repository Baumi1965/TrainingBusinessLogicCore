namespace Training.BusinessLogic.Shared
{
    public class SyncResult
    {
        public bool Error { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
        public string Property { get; set; } = string.Empty;
        
        public SyncResult()
        {
        }
    }    
}
