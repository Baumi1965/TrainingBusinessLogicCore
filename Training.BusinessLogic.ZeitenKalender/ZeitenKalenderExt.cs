namespace Training.BusinessLogic.ZeitenKalender;

public class ZeitenKalenderExt
{
    public int Id { get; set; }
    public int ZeitenKalenderId { get; set; }
    public int SpielstaetteId { get; set; }
    public int? AnzahlFrequenzschein { get; set; }
    public int? AnzahlFrequenzscheinBegleitperson { get; set; }
    public double? WertFrequenzschein { get; set; }
    public int? AnzahlKassa { get; set; }
    public double? WertKassa { get; set; }
    public DateTime Ts { get; set; }
    public DateTime TsCreated { get; set; }
    public DateTime TsModified { get; set; }
    public required string UserCreated { get; set; }
    public required string UserModified { get; set; }
}