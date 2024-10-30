using DevExpress.Xpo;
using System.Threading.Tasks;

namespace Training.BusinessLogic.UOW
{
    public class UOW
    {
        public static UnitOfWork uow;
        public static UnitOfWork uowEngelmann;

        public static string XPOTrainingConnectionString { get; set; }
        public static string XPOEngelmannConnectionString { get; set; }

        public UOW()
        {
        }

        public static void Connect()
        {
            DevExpress.Xpo.SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = true;
            uow = new UnitOfWork();
            uow.ConnectionString = XPOTrainingConnectionString;
            uow.Connect();
            //MySqlConnectorConnectionProvider.Register();
        }

        public static void ConnectEngelmann()
        {
            DevExpress.Xpo.SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = true;
            uowEngelmann = new UnitOfWork();
            uowEngelmann.ConnectionString = XPOEngelmannConnectionString;
            uowEngelmann.Connect();
            //MySqlConnectorConnectionProvider.Register();
        }


        public static void Disconnect()
        {
            if (uow != null)
            {
                uow.Disconnect();
                uow.Dispose();
            }
        }

        public static void DisconnectEngelmann()
        {
            if (uowEngelmann != null)
            {
                uowEngelmann.Disconnect();
                uowEngelmann.Dispose();
            }
        }

        public static async Task SaveAsync()
        {
            await uow.CommitChangesAsync();
        }

        public static async Task DeleteAsync(object theObject)
        {
            await uow.DeleteAsync(theObject);
            await uow.CommitChangesAsync();
        }

        public static async Task DeleteAsync(System.Collections.ICollection objects)
        {
            await uow.DeleteAsync(objects);
            await uow.CommitChangesAsync();
        }


        public static async Task SaveEngelmann()
        {
            await uowEngelmann.CommitChangesAsync();
        }
    }
}
