namespace ConsoleApp2
{
    public class Devices
    {
        public int DeviceSerial { get; set; }
        public string SiteName { get; set; }
        public string Name { get; set; }
        public int DevicePositionID { get; set; }
        public int StatusID { get; set; }
        public string ExternalRef { get; set; }
    }

    public class CSVDevices
    {
        public int? DeviceSerial { get; set; } // Nullable for handling invalid values
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public int? DevicePositionID { get; set; } // Nullable for handling 'NULL'
        public int? StatusID { get; set; } // Nullable for handling 'NULL'
        public string ExternalRef { get; set; }
    }
}
