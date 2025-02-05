using DevExpress.Xpo;
using System.Threading.Tasks;

namespace Training.BusinessLogic.UOW
{
    public class Uow
    {
        public static UnitOfWork _uow;
        public static UnitOfWork _uowEngelmann;
        public static UnitOfWork _uowKassa;
        public static UnitOfWork _uowLokal;

        public static string XpoTrainingConnectionString { get; set; }
        public static string XpoEngelmannConnectionString { get; set; }
        public static string XpoKassaConnectionString { get; set; }
        
        public static string XpoLokalConnectionString { get; set; }

        public Uow()
        {
        }
        
        #region Training
        
        public static void Connect()
        {
            SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = true;
            _uow = new UnitOfWork();
            _uow.ConnectionString = XpoTrainingConnectionString;
            _uow.Connect();
        }
        
        public static async Task SaveAsync()
        {
            await _uow.CommitChangesAsync();
        }

        public static async Task DeleteAsync(object objectToDelete)
        {
            await _uow.DeleteAsync(objectToDelete);
            await _uow.CommitChangesAsync();
        }

        public static async Task DeleteAsync(System.Collections.ICollection objects)
        {
            await _uow.DeleteAsync(objects);
            await _uow.CommitChangesAsync();
        }

        public static void Disconnect()
        {
            if (_uow == null)
            {
                return;
            }
            _uow.Disconnect();
            _uow.Dispose();
        }
        
        #endregion
        
        #region BTSTicket Kassa
        
        public static void ConnectKassa()
        {
            SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = true;
            _uowKassa = new UnitOfWork();
            _uowKassa.ConnectionString = XpoKassaConnectionString;
            _uowKassa.Connect();
        }
        
        public static async Task SaveKassaAsync()
        {
            await _uowKassa.CommitChangesAsync();
        }
        
        public static async Task DeleteKassaAsync(object objectToDelete)
        {
            await _uowKassa.DeleteAsync(objectToDelete);
            await _uowKassa.CommitChangesAsync();
        }

        public static async Task DeleteKassaAsync(System.Collections.ICollection objects)
        {
            await _uowKassa.DeleteAsync(objects);
            await _uowKassa.CommitChangesAsync();
        }
        
        public static void DisconnectKassa()
        {
            if (_uowKassa == null)
            {
                return;
            }
            _uowKassa.Disconnect();
            _uowKassa.Dispose();
        }
        
        #endregion
        
        #region Engelmann Kursanmeldung
        
        public static void ConnectEngelmann()
        {
            SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = true;
            _uowEngelmann = new UnitOfWork();
            _uowEngelmann.ConnectionString = XpoEngelmannConnectionString;
            _uowEngelmann.Connect();
        }
        
        public static async Task SaveEngelmannAsync()
        {
            await _uowEngelmann.CommitChangesAsync();
        }

        public static async Task DeleteEngelmannKassaAsync(object objectToDelete)
        {
            await _uowEngelmann.DeleteAsync(objectToDelete);
            await _uowEngelmann.CommitChangesAsync();
        }

        public static async Task DeleteEngelmannAsync(System.Collections.ICollection objects)
        {
            await _uowEngelmann.DeleteAsync(objects);
            await _uowEngelmann.CommitChangesAsync();
        }
        
        public static void DisconnectEngelmann()
        {
            if (_uowEngelmann == null)
            {
                return;
            }
            _uowEngelmann.Disconnect();
            _uowEngelmann.Dispose();
        }
        
        #endregion
        
        #region Training Lokal
        
        public static void ConnectLokal()
        {
            SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = true;
            _uowLokal = new UnitOfWork();
            _uowLokal.ConnectionString = XpoLokalConnectionString;
            _uowLokal.Connect();
        }
        
        public static async Task SaveLokalAsync()
        {
            await _uowLokal.CommitChangesAsync();
        }
        
        public static async Task DeleteLokalAsync(object objectToDelete)
        {
            await _uowLokal.DeleteAsync(objectToDelete);
            await _uowLokal.CommitChangesAsync();
        }

        public static async Task DeleteLokalAsync(System.Collections.ICollection objects)
        {
            await _uowLokal.DeleteAsync(objects);
            await _uowLokal.CommitChangesAsync();
        }
        
        public static void DisconnectLokal()
        {
            if (_uowLokal == null)
            {
                return;
            }
            _uowLokal.Disconnect();
            _uowLokal.Dispose();
        }
        
        #endregion
    }
}
