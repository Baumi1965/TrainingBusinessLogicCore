namespace Training.BusinessLogic.TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Training TestConsole");

                UOW.UOW.XPOTrainingConnectionString = "XpoProvider = MySql; server = 185.159.122.20; user id = baum_HgX5kkM834n; password = jC7lc2$1; database = trainingsql; persist security info = true; CharSet = utf8; convertzerodatetime = True;";
                UOW.UOW.Connect();

                Console.WriteLine($"Connected to MySql Server {Environment.NewLine}");

                Task.Run(
                    async () =>
                    {
                        var result = await Spielstaetten.Spielstaetten.GetAsync();
                        if (result != null)
                        {
                            foreach (var item in result)
                            {
                                Console.WriteLine($"{item.ID} {item.Bezeichnung}");
                            }
                        }

                        Console.WriteLine();

                        var resultsettings = await Einstellungen.Einstellungen.GetAsync();
                        foreach (var item in resultsettings)
                        {
                            Console.WriteLine($"{item.Setting} {item.Value}");
                        }

                        Console.WriteLine();

                        var resultanzeige = await Gebucht.Gebucht
                            .GetForMonitorAnzeigeAsync(2, DateTime.Now, -50, -20, - 5);

                        Console.WriteLine($"{resultanzeige.Count} Eiskunstläufer anwesend");
                        
                        foreach (var item in resultanzeige)
                        {
                            Console.WriteLine($"{item.Index} {item.VName} {item.NName} {item.Typ}");
                        }


                    }).GetAwaiter().GetResult(); 

                UOW.UOW.Disconnect();
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
    }
}
