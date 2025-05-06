using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Training.BusinessLogic.Einstellungen;
using Training.BusinessLogic.ParkplatzERS.ModelsLokal;
using Training.BusinessLogic.Shared;
using Training.BusinessLogic.UOW;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.ParkplatzERS
{
    public class ParkplatzERSBerechtigtLokal
    {
        public static async Task<int> CountAsync()
        {
            try
            {
                if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
                {
                    Uow.ConnectLokal();
                }

                return await Uow._uowLokal.Query<parkplatzeisringberechtigtlokal>().CountAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        
        public static async Task<SyncResult> DoSyncAsync(string aktion, string daten)
        {
            var result = new SyncResult();
            try
            {
                if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
                {
                    Uow.ConnectLokal();
                }

                dynamic jsonObject = JsonConvert.DeserializeObject<ExpandoObject>(daten);

                // Access properties dynamically
                Console.WriteLine($"Aktion: {aktion}, Tabelle: parkplatzeisringberechtigt");
                Console.WriteLine($"ID: {jsonObject.ID}");
                Console.WriteLine($"Guid: {jsonObject.Guid}");

                var entityType = System.Type.GetType("Training.BusinessLogic.ParkplatzERS.ModelsLokal.parkplatzeisringberechtigtlokal");

                if (entityType == null)
                {
                    Console.WriteLine("Entity type not found.");
                    result.Error = true;
                    result.ErrorMessage = "ParkplatzEisringBerechtigt Type nicht gefunden.";
                    return result;
                }

                switch (aktion)
                {
                    case "I":
                    {
                        var entityInstance = Activator.CreateInstance(entityType, Uow._uowLokal);

                        // Set properties dynamically
                        foreach (var property in (IDictionary<string, object>)jsonObject)
                        {
                            if (property.Key == "ID")
                            {
                                continue;
                            }

                            var propInfo = entityType.GetProperty(property.Key);
                            if (propInfo == null)
                            {
                                continue;
                            }
                            
                            var targetType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                            object convertedValue = null;
                            if (property.Value != null)
                            {
                                convertedValue = Convert.ChangeType(property.Value, targetType);
                            }
                            propInfo.SetValue(entityInstance, convertedValue);
                        }
                        await Uow._uowLokal.SaveAsync(entityInstance);
                        break;
                    }
                    case "U":
                    {
                        var entityInstance = Uow._uowLokal.GetObjectByKey(entityType, jsonObject.Guid);

                        if (entityInstance == null)
                        {
                            Console.WriteLine("Entity not found.");
                            result.Error = true;
                            result.ErrorMessage = "Object nicht gefunden.";
                            return result;
                        }
                    
                        // Set properties dynamically
                        foreach (var property in (IDictionary<string, object>)jsonObject)
                        {                       
                            if (property.Key == "ID")
                            {
                                continue;
                            }
                        
                            var propInfo = entityType.GetProperty(property.Key);
                            if (propInfo == null)
                            {
                                continue;
                            }
                            
                            var targetType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                            object convertedValue = null;
                            if (property.Value != null)
                            {
                                convertedValue = Convert.ChangeType(property.Value, targetType);
                            }
                            propInfo.SetValue(entityInstance, convertedValue);
                        }
                        await Uow._uowLokal.SaveAsync(entityInstance);
                        break;
                    }
                    case "D":
                    {
                        var entityInstance = Uow._uowLokal.GetObjectByKey(entityType, jsonObject.Guid);

                        if (entityInstance == null)
                        {
                            Console.WriteLine("Entity not found.");
                            result.Error = true;
                            result.ErrorMessage = "Object nicht gefunden.";
                            return result;
                        }
                        await Uow._uowLokal.DeleteAsync(entityInstance);
                        break;
                    }
                }
                
                await Uow.SaveLokalAsync();
                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Error = true;
                return result;
            }
        }
        
        public static async Task<bool> CheckBarcodeAsync(string barcode)
        {
            try
            {
                if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
                {
                    Uow.ConnectLokal();
                }

                var result = await Uow._uowLokal.Query<parkplatzeisringberechtigtlokal>()
                    .FirstOrDefaultAsync(x => x.Barcode == barcode && x.Berechtigt == true);

                if (result == null)
                {
                    return false;
                }

                var einfahrt = new parkplatzeisringeinfahrtlokal(Uow._uowLokal)
                {
                    Barcode = barcode,
                    TSEin = DateTime.Now,
                    Guid = Guid.NewGuid().ToString(),
                };
                await Uow.SaveLokalAsync();
                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public static async Task<SyncResult> CheckHockeyBarcodeAsync(string barcode)
        {
            try
            {
                if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
                {
                    Uow.ConnectLokal();
                }
                var response = new SyncResult();
                
                var result = await Uow._uowLokal.Query<parkplatzeismeisterlokal>()
                    .FirstOrDefaultAsync(x => x.Barcode == barcode);

                if (result == null)
                {
                    // Barcode nicht vorhanden
                    response.Error = true;
                    response.ErrorMessage = "Ticket nicht berechtigt.";
                    return response;
                }

                if (result.TSEinfahrt != null)
                {
                    // Barcode bereits verwendet
                    response.Error = true;
                    response.ErrorMessage = "Ticket bereits verwendet.";
                    return response;
                }

                var setting = await EinstellungenLokal.GetByKeyAsync("ParkenEishockeyERS");
                var eishockeyTime = setting.Value;
                setting = await EinstellungenLokal.GetByKeyAsync("VorlaufzeitParkenEishockeyERS");
                var eishockeyVorlaufzeit = Convert.ToInt32(setting.Value);

                TimeSpan tsEishockey = new TimeSpan(Convert.ToInt32(eishockeyTime.Split(':')[0]),
                    Convert.ToInt32(eishockeyTime.Split(':')[1]), 0);
                tsEishockey = tsEishockey.Subtract(new TimeSpan(0, eishockeyVorlaufzeit, 0));

                if (DateTime.Now.TimeOfDay.CompareTo(tsEishockey) < 0)
                {
                    // Einfahrt erst ab
                    response.Error = true;
                    response.ErrorMessage = $"Parkticket {barcode} Einfahrt erst ab {tsEishockey.ToString()} gestattet";
                    return response;
                }

                result.TSEinfahrt = DateTime.Now;
                var ein = new parkplatzeisringeinfahrtlokal(Uow._uowLokal)
                {
                    Barcode = barcode,
                    TSEin = DateTime.Now,
                    Guid = Guid.NewGuid().ToString(),
                };
                await Uow.SaveLokalAsync();
                response.Error = false;
                response.ErrorMessage = string.Empty;
                return response;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}