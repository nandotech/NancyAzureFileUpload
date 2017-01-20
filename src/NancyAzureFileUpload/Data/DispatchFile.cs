using System;

namespace NancyAzureFileUpload.Data
{
    public class DispatchFile
    {
        public int Id { get; set; }
        public int DispatchId { get; set; }
        public string PrimaryUrl { get; set; }
        public string SecondaryUrl { get; set; }
        public string ItemType { get; set; }
        public DateTime Timestamp { get; set; }
        public string Filename { get; set; }
        public string Filetype { get; set; }
    }
}