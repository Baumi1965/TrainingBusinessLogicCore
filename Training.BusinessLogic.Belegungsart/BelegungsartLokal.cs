using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Training.BusinessLogic.Belegungsart.ModelsLokal;
using Training.BusinessLogic.Shared;
using Training.BusinessLogic.UOW;

namespace Training.BusinessLogic.Belegungsart
{
    public class BelegungsartLokal
    {
        public static async Task<int> CountAsync()
        {
            try
            {
                if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
                {
                    Uow.ConnectLokal();
                }

                return await Uow._uowLokal.Query<belegungsartlokal>().CountAsync();

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
                Console.WriteLine($"Aktion: {aktion}, Tabelle: belegungsarten");
                Console.WriteLine($"ID: {jsonObject.ID}");
                Console.WriteLine($"Guid: {jsonObject.Guid}");

                var entityType = System.Type.GetType("Training.BusinessLogic.Belegungsart.ModelsLokal.belegungsartlokal");

                if (entityType == null)
                {
                    Console.WriteLine("Entity type not found.");
                    result.Error = true;
                    result.ErrorMessage = "Belegungsart Type nicht gefunden.";
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

    }
}