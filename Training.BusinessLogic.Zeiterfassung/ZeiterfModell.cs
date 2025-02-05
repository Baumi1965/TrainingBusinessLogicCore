using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfModell
    {
        public int ID
        {
            get;
            set;
        }

        public int? AnzahlAutoPause
        {
            get;
            set;
        }

        public int? DauerAutoPause
        {
            get;
            set;
        }

        public int? HauptmodellID
        {
            get;
            set;
        }

        public double? Wochenstunden
        {
            get;
            set;
        }

        public bool? Mo
        {
            get;
            set;
        }

        public bool? Di
        {
            get;
            set;
        }

        public bool? Mi
        {
            get;
            set;
        }

        public bool? Do
        {
            get;
            set;
        }

        public bool? Fr
        {
            get;
            set;
        }

        public bool? Sa
        {
            get;
            set;
        }

        public bool? So
        {
            get;
            set;
        }

        public bool? Hauptmodell
        {
            get;
            set;
        }

        public bool? Submodell
        {
            get;
            set;
        }

        public DateTime? GueltigVon
        {
            get;
            set;
        }

        public DateTime? GueltigBis
        {
            get;
            set;
        }

        public string Modell
        {
            get;
            set;
        }

        public string MoBeginn
        {
            get;
            set;
        }

        public string MoEnde
        {
            get;
            set;
        }

        public string DiBeginn
        {
            get;
            set;
        }

        public string DiEnde
        {
            get;
            set;
        }

        public string MiBeginn
        {
            get;
            set;
        }

        public string MiEnde
        {
            get;
            set;
        }

        public string DoBeginn
        {
            get;
            set;
        }

        public string DoEnde
        {
            get;
            set;
        }

        public string FrBeginn
        {
            get;
            set;
        }

        public string FrEnde
        {
            get;
            set;
        }

        public string SaBeginn
        {
            get;
            set;
        }

        public string SaEnde
        {
            get;
            set;
        }

        public string SoBeginn
        {
            get;
            set;
        }

        public string SoEnde
        {
            get;
            set;
        }


        public async static Task<List<ZeiterfModell>> GetAsync()
        {
            try
            {
                if(UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                var result = await UOW.Uow._uow
                    .Query<zeiterfmodell>()
                    .ToListAsync();

                if(result == null)
                    return null;
                else
                {
                    var models = new List<ZeiterfModell>();
                    foreach(var item in result)
                    {
                        models.Add(
                            new ZeiterfModell
                            {
                                AnzahlAutoPause = item.AnzahlAutoPause,
                                ID = item.ID,
                                DauerAutoPause = item.DauerAutoPause,
                                Di = item.Di,
                                DiBeginn = item.DiBeginn,
                                DiEnde = item.DiEnde,
                                Do = item.Do,
                                DoBeginn = item.DoBeginn,
                                DoEnde = item.DoEnde,
                                Fr = item.Fr,
                                FrBeginn = item.FrBeginn,
                                FrEnde = item.FrEnde,
                                GueltigBis = item.GueltigBis,
                                GueltigVon = item.GueltigVon,
                                Hauptmodell = item.Hauptmodell,
                                HauptmodellID = item.HauptmodellID,
                                Mi = item.Mi,
                                MiBeginn = item.MiBeginn,
                                MiEnde = item.MiEnde,
                                Mo = item.Mo,
                                MoBeginn = item.MoBeginn,
                                Modell = item.Modell,
                                MoEnde = item.MoEnde,
                                Sa = item.Sa,
                                SaBeginn = item.SaBeginn,
                                SaEnde = item.SaEnde,
                                So = item.So,
                                SoBeginn = item.SoBeginn,
                                SoEnde = item.SoEnde,
                                Submodell = item.Submodell,
                                Wochenstunden = item.Wochenstunden,
                            });
                    }

                    return models;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async static Task<List<ZeiterfModell>> GetHauptmodellAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                var result = await UOW.Uow._uow
                    .Query<zeiterfmodell>()
                    .Where(x => x.Hauptmodell == true)
                    .ToListAsync();

                if (result == null)
                    return null;
                else
                {
                    var models = new List<ZeiterfModell>();
                    foreach (var item in result)
                    {
                        models.Add(
                            new ZeiterfModell
                            {
                                AnzahlAutoPause = item.AnzahlAutoPause,
                                ID = item.ID,
                                DauerAutoPause = item.DauerAutoPause,
                                Di = item.Di,
                                DiBeginn = item.DiBeginn,
                                DiEnde = item.DiEnde,
                                Do = item.Do,
                                DoBeginn = item.DoBeginn,
                                DoEnde = item.DoEnde,
                                Fr = item.Fr,
                                FrBeginn = item.FrBeginn,
                                FrEnde = item.FrEnde,
                                GueltigBis = item.GueltigBis,
                                GueltigVon = item.GueltigVon,
                                Hauptmodell = item.Hauptmodell,
                                HauptmodellID = item.HauptmodellID,
                                Mi = item.Mi,
                                MiBeginn = item.MiBeginn,
                                MiEnde = item.MiEnde,
                                Mo = item.Mo,
                                MoBeginn = item.MoBeginn,
                                Modell = item.Modell,
                                MoEnde = item.MoEnde,
                                Sa = item.Sa,
                                SaBeginn = item.SaBeginn,
                                SaEnde = item.SaEnde,
                                So = item.So,
                                SoBeginn = item.SoBeginn,
                                SoEnde = item.SoEnde,
                                Submodell = item.Submodell,
                                Wochenstunden = item.Wochenstunden,
                            });
                    }

                    return models;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<ZeiterfModell> GetById(int id)
        {
            try
            {
                if(UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                var modell = await UOW.Uow._uow
                    .Query<zeiterfmodell>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if(modell == null)
                    return null;
                else
                {
                    return new ZeiterfModell
                    {
                        AnzahlAutoPause = modell.AnzahlAutoPause,
                        ID = modell.ID,
                        DauerAutoPause = modell.DauerAutoPause,
                        Di = modell.Di,
                        DiBeginn = modell.DiBeginn,
                        DiEnde = modell.DiEnde,
                        Do = modell.Do,
                        DoBeginn = modell.DoBeginn,
                        DoEnde = modell.DoEnde,
                        Fr = modell.Fr,
                        FrBeginn = modell.FrBeginn,
                        FrEnde = modell.FrEnde,
                        GueltigBis = modell.GueltigBis,
                        GueltigVon = modell.GueltigVon,
                        Hauptmodell = modell.Hauptmodell,
                        HauptmodellID = modell.HauptmodellID,
                        Mi = modell.Mi,
                        MiBeginn = modell.MiBeginn,
                        MiEnde = modell.MiEnde,
                        Mo = modell.Mo,
                        MoBeginn = modell.MoBeginn,
                        Modell = modell.Modell,
                        MoEnde = modell.MoEnde,
                        Sa = modell.Sa,
                        SaBeginn = modell.SaBeginn,
                        SaEnde = modell.SaEnde,
                        So = modell.So,
                        SoBeginn = modell.SoBeginn,
                        SoEnde = modell.SoEnde,
                        Submodell = modell.Submodell,
                        Wochenstunden = modell.Wochenstunden
                    };
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async static Task<List<ZeiterfModell>> GetSubmodels(int id)
        {
            try
            {
                if(UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var modell = await UOW.Uow._uow
                    .Query<zeiterfmodell>()
                    .Where(x => x.Submodell == true && x.HauptmodellID == id)
                    .ToListAsync();

                if(modell == null)
                    return null;
                else
                {
                    List<ZeiterfModell> models = new List<ZeiterfModell>();
                    foreach(var model in modell)
                    {
                        models.Add(
                            new ZeiterfModell
                            {
                                AnzahlAutoPause = model.AnzahlAutoPause,
                                ID = model.ID,
                                DauerAutoPause = model.DauerAutoPause,
                                Di = model.Di,
                                DiBeginn = model.DiBeginn,
                                DiEnde = model.DiEnde,
                                Do = model.Do,
                                DoBeginn = model.DoBeginn,
                                DoEnde = model.DoEnde,
                                Fr = model.Fr,
                                FrBeginn = model.FrBeginn,
                                FrEnde = model.FrEnde,
                                GueltigBis = model.GueltigBis,
                                GueltigVon = model.GueltigVon,
                                Hauptmodell = model.Hauptmodell,
                                HauptmodellID = model.HauptmodellID,
                                Mi = model.Mi,
                                MiBeginn = model.MiBeginn,
                                MiEnde = model.MiEnde,
                                Mo = model.Mo,
                                MoBeginn = model.MoBeginn,
                                Modell = model.Modell,
                                MoEnde = model.MoEnde,
                                Sa = model.Sa,
                                SaBeginn = model.SaBeginn,
                                SaEnde = model.SaEnde,
                                So = model.So,
                                SoBeginn = model.SoBeginn,
                                SoEnde = model.SoEnde,
                                Submodell = model.Submodell,
                                Wochenstunden = model.Wochenstunden
                            });
                    }
                    return models;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async static Task AddAsync(ZeiterfModell item)
        {
            try
            {
                if(UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                zeiterfmodell zeiterfmodell = new zeiterfmodell(UOW.Uow._uow)
                {
                    Modell = item.Modell,
                    Wochenstunden = item.Wochenstunden,
                    AnzahlAutoPause = item.AnzahlAutoPause,
                    DauerAutoPause = item.DauerAutoPause,
                    GueltigBis = item.GueltigBis,
                    GueltigVon = item.GueltigVon,
                    Hauptmodell = item.Hauptmodell,
                    Submodell = item.Submodell,
                    HauptmodellID = item.HauptmodellID,
                    Mo = item.Mo,
                    MoBeginn = item.MoBeginn,
                    MoEnde = item.MoEnde,
                    Di = item.Di,
                    DiBeginn = item.DiBeginn,
                    DiEnde = item.DiEnde,
                    Mi = item.Mi,
                    MiBeginn = item.MiBeginn,
                    MiEnde = item.MiEnde,
                    Do = item.Do,
                    DoBeginn = item.DoBeginn,
                    DoEnde = item.DoEnde,
                    Fr = item.Fr,
                    FrBeginn = item.FrBeginn,
                    FrEnde = item.FrEnde,
                    Sa = item.Sa,
                    SaBeginn = item.SaBeginn,
                    SaEnde = item.SaEnde,
                    So = item.So,
                    SoBeginn = item.SoBeginn,
                    SoEnde = item.SoEnde,
                };

                await UOW.Uow.SaveAsync(); 
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async static Task UpdateAsync(int id, ZeiterfModell item)
        {
            try
            {
                if(UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow
                    .Query<zeiterfmodell>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if(result == null)
                {
                    return;
                }
                ;

                result.Modell = item.Modell;
                result.Wochenstunden = item.Wochenstunden;
                result.AnzahlAutoPause = item.AnzahlAutoPause;
                result.DauerAutoPause = item.DauerAutoPause;
                result.GueltigBis = item.GueltigBis;
                result.GueltigVon = item.GueltigVon;
                result.Hauptmodell = item.Hauptmodell;
                result.Submodell = item.Submodell;
                result.HauptmodellID = item.HauptmodellID;
                result.Mo = item.Mo;
                result.MoBeginn = item.MoBeginn;
                result.MoEnde = item.MoEnde;
                result.Di = item.Di;
                result.DiBeginn = item.DiBeginn;
                result.DiEnde = item.DiEnde;
                result.Mi = item.Mi;
                result.MiBeginn = item.MiBeginn;
                result.MiEnde = item.MiEnde;
                result.Do = item.Do;
                result.DoBeginn = item.DoBeginn;
                result.DoEnde = item.DoEnde;
                result.Fr = item.Fr;
                result.FrBeginn = item.FrBeginn;
                result.FrEnde = item.FrEnde;
                result.Sa = item.Sa;
                result.SaBeginn = item.SaBeginn;
                result.SaEnde = item.SaEnde;
                result.So = item.So;
                result.SoBeginn = item.SoBeginn;
                result.SoEnde = item.SoEnde;

                await UOW.Uow.SaveAsync(); 
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async static Task DeleteAsync(int id)
        {
            try
            {
                if(UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow
                    .Query<zeiterfmodell>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if(result == null)
                {
                    return;
                }
                ;

                await UOW.Uow._uow.DeleteAsync(result);

                var resultpause = await UOW.Uow._uow
                    .Query<zeiterfmodellpause>()
                    .Where(x => x.ModellID == id)
                    .FirstOrDefaultAsync();

                if(resultpause != null)
                {
                    await UOW.Uow._uow.DeleteAsync(resultpause);
                }

                await UOW.Uow.SaveAsync(); 
            }
            catch(Exception)
            {
                throw;
            }
        }
    }

    public class ZeiterfSubmodel
    {
        public DateTime Datum { get; set; }
        public string MoBeginn { get; set; }
        public string MoEnde { get; set; }
        public string DiBeginn { get; set; }
        public string DiEnde { get; set; }
        public string MiBeginn { get; set; }
        public string MiEnde { get; set; }
        public string DoBeginn { get; set; }
        public string DoEnde { get; set; }
        public string FrBeginn { get; set; }
        public string FrEnde { get; set; }
        public string SaBeginn { get; set; }
        public string SaEnde { get; set; }
        public string SoBeginn { get; set; }
        public string SoEnde { get; set; }

    }
}
