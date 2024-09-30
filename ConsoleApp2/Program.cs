using ConsoleApp2;

internal class Program
{
    private static void Main(string[] args)
    {
        var devices = CSVReader.GetDevicesFromCsv();

        foreach (var device in devices)
        {
            //Console.WriteLine($"Device Serial: {device.DeviceSerial}  SiteName:{device.Name1}  Name:{device.Name2}  DevicePositionId:{device.DevicePositionID}  StatusId:{device.StatusID}  ExternalRef:{device.ExternalRef}");
            Console.WriteLine($"Updating Device:{device.DeviceSerial} with external reference: {device.ExternalRef}");

            if (device.ExternalRef is null)
            {
                Console.WriteLine($"Skipping device: {device.DeviceSerial} invalid external ref");
                continue;
            }

            var deviceDBInfo = DAL.GetDevice(device.DeviceSerial ?? throw new Exception("InvalidDevice"));

            if (deviceDBInfo is null)
            {
                throw new Exception("Device not found");
            }

            if (deviceDBInfo.Count == 0)
            {
                continue;
            }

            if (deviceDBInfo.Count > 1)
            {
                throw new Exception("Multiple devices found");
            }



            Console.WriteLine($"Updating device:{deviceDBInfo.First().DeviceSerial}");
            DAL.UpdateDevice(deviceDBInfo.First().DeviceSerial, device.ExternalRef);
        }
    }
}