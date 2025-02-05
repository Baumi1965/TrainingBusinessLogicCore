using System.Diagnostics;
using Training.BusinessLogic.Engelmann;
using Training.BusinessLogic.Kassa.Spielstaette;

namespace Training.BusinessLogic.TestConsole
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Training TestConsole");

                UOW.Uow.XpoTrainingConnectionString = "XpoProvider = MySql; server = 185.159.122.20; user id = baum_HgX5kkM834n; password = jC7lc2$1; database = trainingsqltest; persist security info = true; CharSet = utf8; convertzerodatetime = True;";
                UOW.Uow.Connect();

                Console.WriteLine($"Connected to MySql Server {Environment.NewLine}");

                UOW.Uow.XpoKassaConnectionString = "XpoProvider = MySql; server = 185.159.122.20; user id = baum_HgX5kkM834n; password = jC7lc2$1; database = btsticket; persist security info = true; CharSet = utf8; convertzerodatetime = True;";

                UOW.Uow.XpoLokalConnectionString = "XpoProvider = MySql; server = localhost; user id = root; password = !wuzihasi_91; database = trainingsqllokal; persist security info = true; CharSet = utf8; convertzerodatetime = True;";

                using var cts = new CancellationTokenSource();
                var keyListenerTask = Task.Run(() => CheckForKeyPress(cts));
                
                Task.Run(
                    async () =>
                    {
 
                        
                        // var resultzeit =
                        //     await Zeiterfassung.ZeiterfJahresuebersicht.Get("227", new DateTime(2024, 9, 1, 0, 0, 0));
                        // if (resultzeit != null)
                        // {
                        //     foreach (var item in resultzeit)
                        //     {
                        //         Console.WriteLine($"{item.Monat}/{item.Jahr} {item.Sollstunden} {item.GeleisteteStunden}");
                        //     }
                        // }

                        var stopwatch = new Stopwatch();

                        while (!cts.Token.IsCancellationRequested)
                        {
                            stopwatch.Start();
                            Console.WriteLine($"Sync GetNewAsync started {stopwatch.ElapsedMilliseconds} ms");

                        
                            await Sync.Sync.GetNewAsync(2);
                        
                            Console.WriteLine($"Sync GetNewAsync finished {stopwatch.ElapsedMilliseconds} ms");
                            Console.WriteLine();
                            stopwatch.Stop();
                            stopwatch.Reset();

                            await Task.Delay(5000);
                        }
                        await keyListenerTask;
                        // var result = await Spielstaetten.Spielstaetten.GetByBtsTicketIdAsync(3);
                        // if (result != null)
                        // {
                        //     foreach (var item in result)
                        //     {
                        //         Console.WriteLine($"{item.ID} {item.Bezeichnung} {item.BtsTicketId}");
                        //     }
                        // }
                        //
                        // Console.WriteLine();
                        //
                        // var resultk = await KassaSpielstaette.GetBySpielstaetteIdAsync(3);
                        // if (resultk != null)
                        // {
                        //     Console.WriteLine($"{resultk.Id} {resultk.SpName} {resultk.Sp}");
                        // }
                        //
                        // Console.WriteLine();                        
                        
                        //
                        // var resultsettings = await Einstellungen.Einstellungen.GetAsync();
                        // foreach (var item in resultsettings)
                        // {
                        //     Console.WriteLine($"{item.Setting} {item.Value}");
                        // }
                        //
                        // Console.WriteLine();
                        //
                        // var resultanzeige = await Gebucht.Gebucht
                        //     .GetForMonitorAnzeigeAsync(2, DateTime.Now, -50, -20, - 5);
                        //
                        // Console.WriteLine($"{resultanzeige.Count} Eiskunstläufer anwesend");
                        //
                        // foreach (var item in resultanzeige)
                        // {
                        //     Console.WriteLine($"{item.Index} {item.VName} {item.NName} {item.Typ}");
                        // }
                        //
                        // Console.WriteLine();
                        // var resultcevkev = await Training.BusinessLogic.Engelmann.CEVKEVAnwesenheit.GetAnwesenheitslisteAsync(DateTime.Now, DateTime.Now,true,true,true,true,true);
                        // foreach (var item in resultcevkev)
                        // {
                        //     Console.WriteLine($"{item.ID} {item.KdNr}");                            
                        // }
                        //
                        // Console.WriteLine();
                    }).GetAwaiter().GetResult(); 

                UOW.Uow.Disconnect();
                Console.WriteLine($"{Environment.NewLine}Disonnected from MySql Server");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
        
        static void CheckForKeyPress(CancellationTokenSource cts)
        {
            while (!cts.Token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        Console.WriteLine("\nBeenden...");
                        cts.Cancel();
                        break;
                    }
                }

                Thread.Sleep(100);
            }
        }
    }
}
