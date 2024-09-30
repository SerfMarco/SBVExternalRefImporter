using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace ConsoleApp2
{
    public static class CSVReader
    {
        public static List<CSVDevices> GetDevicesFromCsv()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", // Set the delimiter to semicolon
                HeaderValidated = null, // Disable header validation if headers are not critical
            };

            var devices = new List<CSVDevices>();

            using (var reader = new StreamReader("SBV - Trinity Mapping Tool Data.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                // Apply the custom mapping
                csv.Context.RegisterClassMap<CSVDevicesMap>();

                // Automatically map CSV rows to CSVDevices objects
                devices = new List<CSVDevices>(csv.GetRecords<CSVDevices>());
            }

            return devices;
        }
    }

    public class CSVDevicesMap : ClassMap<CSVDevices>
    {
        public CSVDevicesMap()
        {
            Map(m => m.DeviceSerial)
                .Name("DeviceSerial")
                .TypeConverter<NullableIntConverter>(); // Use custom converter
            Map(m => m.Name1)
                .Name("Name1");
            Map(m => m.Name2)
                .Name("Name2"); // This assumes that 'Name' appears twice in CSV; adjust if needed
            Map(m => m.DevicePositionID)
                .Name("DevicePositionID")
                .TypeConverter<NullableIntConverter>(); // Use custom converter
            Map(m => m.StatusID)
                .Name("StatusID")
                .TypeConverter<NullableIntConverter>(); // Use custom converter
            Map(m => m.ExternalRef)
                .Name("ExternalRef");
        }
    }



    public class NullableIntConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text) || text.Equals("NULL", StringComparison.OrdinalIgnoreCase))
            {
                return null; // Return null for invalid or missing values
            }

            // Try to convert to integer, return null if conversion fails
            if (int.TryParse(text, out var result))
            {
                return result;
            }

            return null;
        }
    }

    public class NullableDoubleConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text) || text.Equals("NULL", StringComparison.OrdinalIgnoreCase))
            {
                return null; // Return null for invalid or missing values
            }

            // Try to convert to double, return null if conversion fails
            if (double.TryParse(text, out var result))
            {
                return result;
            }

            return null;
        }
    }

}

