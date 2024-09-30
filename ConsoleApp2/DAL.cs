using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ConsoleApp2
{
    public static class DAL
    {
        private static string _connectionString = "Data Source=VSZADRCOPTPHS01;Initial Catalog=Tph2;Integrated Security=True;TrustServerCertificate=True;";

        public static List<Devices> GetDevice(int DeviceSerial)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Define stored procedure name
                string storedProcedure = "spSelectDevice";

                // Define parameters
                var parameters = new DynamicParameters();
                parameters.Add("@DeviceId", DeviceSerial);

                // Execute stored procedure and retrieve the result
                var devices = db.Query<Devices>(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure).AsList();

                return devices;
            }
        }

        public static void UpdateDevice(int deviceSerial, string ExternalRef)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Define stored procedure name
                string storedProcedure = "spUpdateExternalReference";

                // Define parameters
                var parameters = new DynamicParameters();
                parameters.Add("@DeviceId", deviceSerial);
                parameters.Add("@ExternalRef", ExternalRef);

                // Execute stored procedure and retrieve the result
                var devices = db.Execute(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
