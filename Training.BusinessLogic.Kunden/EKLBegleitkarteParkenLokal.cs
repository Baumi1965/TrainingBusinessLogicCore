using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Training.BusinessLogic.Kunden.ModelsLokal;
using Training.BusinessLogic.Shared;
using Training.BusinessLogic.UOW;

namespace Training.BusinessLogic.Kunden
{
    public class EKLBegleitkarteParkenLokal
    {
        public static async Task<SyncResult> DoSyncAsync(string aktion, string daten)
        {
            var result = new SyncResult();
            var prop = "";
            try
            {
                if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
                {
                    Uow.ConnectLokal();
                }

                dynamic jsonObject = JsonConvert.DeserializeObject<ExpandoObject>(daten);

                // Access properties dynamically
                Console.WriteLine($"Aktion: {aktion}, Tabelle: eklbegleitkarteparken");
                Console.WriteLine($"ID: {jsonObject.ID}");
                Console.WriteLine($"Guid: {jsonObject.Guid}");

                var entityType = System.Type.GetType("Training.BusinessLogic.Kunden.ModelsLokal.eklbegleitkarteparkenlokal");

                if (entityType == null)
                {
                    Console.WriteLine("Entity type not found.");
                    result.Error = true;
                    result.ErrorMessage = "EKLBegleitkarte Type nicht gefunden.";
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
                            prop = property.Key;
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
                            prop = property.Key;                     
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
                result.Property = prop;
                return result;
            }
        }

        public static async Task<Response> AddAsync(string barcode, int location, DateTime now)
        {
            try
            {
                if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
                {
                    Uow.ConnectLokal();
                }
                
                var response = new Response
                {
                    Status = true,
                    Message = string.Empty,
                    Id = null,
                };
                
                var result = await Uow._uowLokal.Query<eklbegleitkartelokal>().Where(x => x.Barcode == barcode).FirstOrDefaultAsync();
                if (result == null) 
                {
                    response.Id = null;
                    response.Message = "Ticket wurde nicht aktiviert";
                    response.Status = false;
                }
                else
                {
                    if (!result.Parkplatz)
                    {
                        response.Id = result.ID;
                        response.Message = "Ticket nicht für das Parken berechtigt";
                        response.Status = false;
                    }
                    else
                    {
                        var resultparken = await Uow._uowLokal.Query<eklbegleitkarteparkenlokal>()
                            .Where(x => x.EKLBegleitkarteEintrittID == result.ID && x.Valid == true)
                            .OrderByDescending(x => x.TSParken)
                            .FirstOrDefaultAsync();

                        if (resultparken == null || resultparken.TSParken.Date.CompareTo(now.Date) == 0)
                        {
                            response.Id = result.ID;
                            response.Message = string.Empty;
                            response.Status = true;
                        }
                        else
                        {
                            response.Id = result.ID;
                            response.Message = $"Ticket wurde am {resultparken.TSParken.Date:dd.MM.yyyy} bereits verwendet";
                            response.Status = false;
                        }
                    }
                }
                
                var parken = new eklbegleitkarteparkenlokal(Uow._uowLokal)
                {
                    EKLBegleitkarteEintrittID = response.Id,
                    Message = response.Message,
                    TSParken = DateTime.Now,
                    Valid = response.Status,
                    Location = location,
                };
                
                await Uow.SaveLokalAsync();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
    
    
}