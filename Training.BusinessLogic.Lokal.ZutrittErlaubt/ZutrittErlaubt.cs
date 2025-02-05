using DevExpress.Xpo;
using Training.BusinessLogic.UOW;
using Training.BusinessLogic.UOW.ModelsLokal;

namespace Training.BusinessLogic.Lokal.ZutrittErlaubt;

public class ZutrittErlaubt
{
    public int Id { get; set; }
    public string Barcode { get; set; }
    public Guid Guid { get; set; }
    public bool Valid { get; set; }
    public bool Eiskunstlaeufer { get; set; }
    public bool Kassa { get; set; }
    public bool Parkplatz { get; set; }
    public bool Begleitkarte { get; set; }
    public bool BegleitkarteParkplatz { get; set; }
    public DateTime TsCreated { get; set; }
    public DateTime TsModified { get; set; }

    public static async Task<ZutrittErlaubt?> GetByBarcodeAsync(string barcode)
    {
        try
        {
            if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
            {
                Uow.ConnectLokal();
            }
            
            var zutritt = await Uow._uowLokal.Query<zutritterlaubt>().FirstOrDefaultAsync(x => x.Barcode == barcode);
            if (zutritt == null)
            {
                return null;
            }

            return new ZutrittErlaubt()
            {
                Id = zutritt.ID,
                Barcode = zutritt.Barcode,
                Guid = new Guid(zutritt.Guid),
                Valid = zutritt.Valid,
                Eiskunstlaeufer = zutritt.Eiskunstlaeufer,
                Kassa = zutritt.Kassa,
                Parkplatz = zutritt.Parkplatz,
                Begleitkarte = zutritt.Begleitkarte,
                BegleitkarteParkplatz = zutritt.BegleitkarteParkplatz,
                TsCreated = zutritt.TSCreated,
                TsModified = zutritt.TSModified,
            };

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public static async Task AddAsync(string barcode, bool valid, bool eiskunstlaeufer, bool kassa, bool parkplatz, bool begleitkarte, bool begleitkarteParkplatz)
    {
        try
        {
            if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
            {
                Uow.ConnectLokal();
            }

            var zutritt = new zutritterlaubt(Uow._uowLokal);
            zutritt.Barcode = barcode;
            zutritt.Valid = valid;
            zutritt.Eiskunstlaeufer = eiskunstlaeufer;
            zutritt.Kassa = kassa;
            zutritt.Parkplatz = parkplatz;
            zutritt.Begleitkarte = begleitkarte;
            zutritt.BegleitkarteParkplatz = begleitkarteParkplatz;
            zutritt.Guid = Guid.NewGuid().ToString();
            zutritt.TSCreated = DateTime.Now;
            zutritt.TSModified = DateTime.Now;

            await Uow.SaveLokalAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    
    public static async Task UpdateAsync(string barcode, bool valid, bool eiskunstlaeufer, bool kassa, bool parkplatz, bool begleitkarte, bool begleitkarteParkplatz)
    {
        try
        {
            if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
            {
                Uow.ConnectLokal();
            }

            var zutritt = await Uow._uowLokal.Query<zutritterlaubt>().FirstOrDefaultAsync(x => x.Barcode == barcode);
            if (zutritt == null)
            {
                return;
            }

            zutritt.Valid = valid;
            zutritt.Eiskunstlaeufer = eiskunstlaeufer;
            zutritt.Kassa = kassa;
            zutritt.Parkplatz = parkplatz;
            zutritt.Begleitkarte = begleitkarte;
            zutritt.BegleitkarteParkplatz = begleitkarteParkplatz;
            zutritt.TSModified = DateTime.Now;

            await Uow.SaveLokalAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public static async Task DeleteAsync(string barcode)
    {
        try
        {
            if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
            {
                Uow.ConnectLokal();
            }

            var zutritt = await Uow._uowLokal.Query<zutritterlaubt>().FirstOrDefaultAsync(x => x.Barcode == barcode);
            if (zutritt == null)
            {
                return;
            }

            await Uow.DeleteLokalAsync(zutritt);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}