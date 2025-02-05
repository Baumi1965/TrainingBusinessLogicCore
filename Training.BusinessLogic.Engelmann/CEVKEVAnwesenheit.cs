using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;

namespace Training.BusinessLogic.Engelmann
{
    public class CEVKEVAnwesenheit
    {
        public Int32 ID { get; set; }
        public String KdNr { get; set; }
        public String Vorname { get; set; }
        public String Nachname { get; set; }
        public Int32 CEV { get; set; }
        public Int32 KEV { get; set; }
        public DateTime Datum { get; set; }
        public String Zeit { get; set; }
        public String TSAus { get; set; }
        public UInt64 Vormittag { get; set; }
        public UInt64 Nachmittag { get; set; }
        public String Typ { get; set; }

        public static async Task<List<CEVKEVAnwesenheit>> GetAnwesenheitslisteAsync(DateTime? von, DateTime? bis, bool cev, bool kev,
            bool sportler, bool trainer, bool adult)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "GET_CEVKEV_ANWESENHEITSLISTE",
                    new OperandValue(von == null ? null : von.Value.ToString("yyyyMMdd")),
                    new OperandValue(bis == null ? null : bis.Value.ToString("yyyyMMdd")),
                    new OperandValue(cev ? 1 : 0),
                    new OperandValue(kev ? 1 : 0),
                    new OperandValue(sportler ? 1 : 0),
                    new OperandValue(trainer ? 1 : 0),
                    new OperandValue(adult ? 1 : 0));


                if (result == null)
                {
                    return null;
                }

                List<CEVKEVAnwesenheit> anwesende = new List<CEVKEVAnwesenheit>();

                var resultanwesend = result.ResultSet.FirstOrDefault();
                if (resultanwesend != null)
                {
                    foreach (var item in resultanwesend.Rows)
                    {
                        string stKdNr = item.Values[1].ToString();
                        DateTime dtDatum = Convert.ToDateTime(item.Values[6]);
                        ulong inVormittag = Convert.ToUInt64(item.Values[9]);
                        ulong inNachmittag = Convert.ToUInt64(item.Values[10]);

                        var anwesend = anwesende.Where(x => x.KdNr == stKdNr && x.Datum.Date == dtDatum.Date).ToList();
                        if (anwesend.Count == 0)
                        {
                            CEVKEVAnwesenheit anw = new CEVKEVAnwesenheit();
                            anw.ID = Convert.ToInt32(item.Values[0]);
                            anw.KdNr = stKdNr;
                            anw.Vorname = item.Values[2].ToString();
                            anw.Nachname = item.Values[3].ToString();
                            anw.CEV = Convert.ToInt32(item.Values[4]);
                            anw.KEV = Convert.ToInt32(item.Values[5]);
                            anw.Datum = dtDatum;
                            anw.Zeit = item.Values[7] == null ? "" : item.Values[7].ToString();
                            anw.TSAus = item.Values[8] == null ? "" : item.Values[8].ToString();
                            anw.Vormittag = inVormittag;
                            anw.Nachmittag = inNachmittag;
                            anw.Typ = item.Values[11] == null ? "" : item.Values[11].ToString();

                            anwesende.Add(anw);
                        }
                        else
                        {
                            Boolean boVormittag = false;
                            Boolean boNachmittag = false;
                            foreach (var itemanw in anwesend)
                            {
                                if (itemanw.Vormittag == 1)
                                {
                                    boVormittag = true;
                                }

                                if (itemanw.Nachmittag == 1)
                                {
                                    boNachmittag = true;
                                }
                            }

                            if (inVormittag == 1 && inNachmittag == 0)
                            {
                                var reslst = anwesende.Where(x => x.KdNr == stKdNr && x.Datum == dtDatum && x.Zeit == item.Values[7].ToString()).FirstOrDefault();
                                if (reslst == null)
                                {
                                    CEVKEVAnwesenheit anw = new CEVKEVAnwesenheit();
                                    anw.ID = Convert.ToInt32(item.Values[0]);
                                    anw.KdNr = stKdNr;
                                    anw.Vorname = item.Values[2].ToString();
                                    anw.Nachname = item.Values[3].ToString();
                                    anw.CEV = Convert.ToInt32(item.Values[4]);
                                    anw.KEV = Convert.ToInt32(item.Values[5]);
                                    anw.Datum = dtDatum;
                                    anw.Zeit = item.Values[7] == null ? "" : item.Values[7].ToString();
                                    anw.TSAus = item.Values[8] == null ? "" : item.Values[8].ToString();
                                    anw.Nachmittag = 0;
                                    anw.Typ = item.Values[11] == null ? "" : item.Values[11].ToString();
                                    if (!boVormittag)
                                    {
                                        anw.Vormittag = 1;
                                    }
                                    else
                                    {
                                        anw.Vormittag = 0;
                                    }
                                    anwesende.Add(anw);
                                }
                            }
                            else if (inVormittag == 0 && inNachmittag == 1)
                            {
                                var reslst = anwesende.Where(x => x.KdNr == stKdNr && x.Datum == dtDatum && x.Zeit == item.Values[7].ToString()).FirstOrDefault();
                                if (reslst == null)
                                {
                                    CEVKEVAnwesenheit anw = new CEVKEVAnwesenheit();
                                    anw.ID = Convert.ToInt32(item.Values[0]);
                                    anw.KdNr = stKdNr;
                                    anw.Vorname = item.Values[2].ToString();
                                    anw.Nachname = item.Values[3].ToString();
                                    anw.CEV = Convert.ToInt32(item.Values[4]);
                                    anw.KEV = Convert.ToInt32(item.Values[5]);
                                    anw.Datum = dtDatum;
                                    anw.Zeit = item.Values[7] == null ? "" : item.Values[7].ToString();
                                    anw.TSAus = item.Values[8] == null ? "" : item.Values[8].ToString();
                                    anw.Nachmittag = 0;
                                    anw.Typ = item.Values[11] == null ? "" : item.Values[11].ToString();
                                    if (!boVormittag)
                                    {
                                        anw.Vormittag = 1;
                                    }
                                    else
                                    {
                                        anw.Vormittag = 0;
                                    }
                                    anwesende.Add(anw);
                                }

                            }
                            else if (inVormittag == 1 && inNachmittag == 1)
                            {
                                var reslst = anwesende.Where(x => x.KdNr == stKdNr && x.Datum == dtDatum && x.Zeit == item.Values[7].ToString()).FirstOrDefault();
                                if (reslst == null)
                                {
                                    CEVKEVAnwesenheit anw = new CEVKEVAnwesenheit();
                                    anw.ID = Convert.ToInt32(item.Values[0]);
                                    anw.KdNr = stKdNr;
                                    anw.Vorname = item.Values[2].ToString();
                                    anw.Nachname = item.Values[3].ToString();
                                    anw.CEV = Convert.ToInt32(item.Values[4]);
                                    anw.KEV = Convert.ToInt32(item.Values[5]);
                                    anw.Datum = dtDatum;
                                    anw.Zeit = item.Values[7] == null ? "" : item.Values[7].ToString();
                                    anw.TSAus = item.Values[8] == null ? "" : item.Values[8].ToString();
                                    anw.Nachmittag = 0;
                                    anw.Typ = item.Values[11] == null ? "" : item.Values[11].ToString();
                                    if (!boNachmittag)
                                    {
                                        anw.Nachmittag = 1;
                                    }
                                    else
                                    {
                                        anw.Nachmittag = 0;
                                    }
                                    if (!boVormittag)
                                    {
                                        anw.Vormittag = 1;
                                    }
                                    else
                                    {
                                        anw.Vormittag = 0;
                                    }
                                    anwesende.Add(anw);
                                }
                            }
                        }

                    }
                }

                return anwesende;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
