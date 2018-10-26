using MonDiabete.Class.Dependency_Services_Class;
using MonDiabete.Fichiers;
using MonDiabete.Objets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MonDiabete.Class
{
    public class ToolsGlycemie
    {
        public string FindNombreUniteInsuline(string glycemieMesure)
        {
            string Retour = "";

            string fileName = PrefsApp.fileConfigGlycemie;
            string data = DependencyService.Get<IFileReadWrite>().ReadData(fileName);
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(data);

            GlycemieInfosObjectStruct InfosGlycemie = (GlycemieInfosObjectStruct)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(GlycemieInfosObjectStruct));
            if (InfosGlycemie.GlycemieConfigRecorded == "true")
            {


                if (VariablesGlobal.NomMomentRefMesure == "matin")
                {

                    if (FindNombreUniteMatin(glycemieMesure, InfosGlycemie) != "erreur")
                    {
                        Console.Write("retour glycemie:" + Retour);
                        Retour = FindNombreUniteMatin(glycemieMesure, InfosGlycemie);
                    }
                    else
                    {
                        Retour = "erreur";
                    }

                }
                else if (VariablesGlobal.NomMomentRefMesure == "midi")
                {
                    if (FindNombreUniteMidi(glycemieMesure, InfosGlycemie) != "erreur")
                    {
                        Console.Write("retour glycemie:" + Retour);
                        Retour = FindNombreUniteMidi(glycemieMesure, InfosGlycemie);
                    }
                    else
                    {
                        Retour = "erreur";
                    }
                }
                else if (VariablesGlobal.NomMomentRefMesure == "soir")
                {
                    if (FindNombreUniteSoir(glycemieMesure, InfosGlycemie) != "erreur")
                    {
                        Console.Write("retour glycemie:" + Retour);
                        Retour = FindNombreUniteSoir(glycemieMesure, InfosGlycemie);
                    }
                    else
                    {
                        Retour = "erreur";
                    }
                }
                else
                {
                    Retour = "erreur";
                }

            }
            else
            {
                Retour = "erreur";
            }

            return Retour;
        }

        private string FindNombreUniteSoir(string glycemieMesure, GlycemieInfosObjectStruct InfosGlycemie)
        {
            string Retour = "erreur";
            try
            {
                Console.WriteLine("glycemie: " + glycemieMesure);
                int glycemieNumerique = Convert.ToInt32(glycemieMesure);
                if (glycemieNumerique < 70)
                {
                    Retour = InfosGlycemie.GlycemieMoins70Soir;
                }
                else if (glycemieNumerique >= 70 && glycemieNumerique < 101)
                {
                    Retour = InfosGlycemie.Glycemie70A100Soir;
                }
                else if (glycemieNumerique >= 101 && glycemieNumerique < 151)
                {
                    Retour = InfosGlycemie.Glycemie101A150Soir;
                }
                else if (glycemieNumerique >= 151 && glycemieNumerique < 201)
                {
                    Retour = InfosGlycemie.Glycemie151A200Soir;
                }
                else if (glycemieNumerique >= 201 && glycemieNumerique < 251)
                {
                    Retour = InfosGlycemie.Glycemie201A250Soir;
                }
                else if (glycemieNumerique >= 251 && glycemieNumerique < 301)
                {
                    Retour = InfosGlycemie.Glycemie251A300Soir;
                }
                else if (glycemieNumerique > 300)
                {
                    Retour = InfosGlycemie.GlycemiePlus300Soir;
                }
                Console.WriteLine("glycemieNumerique: " + InfosGlycemie.GlycemieMoins70Midi);
            }
            catch (Exception error)
            {
                Retour = "erreur";
                Console.WriteLine("Utlisateur non enregistré. ou erreur:" + error.Message.ToString());
            }

            return Retour;
        }

        private string FindNombreUniteMidi(string glycemieMesure, GlycemieInfosObjectStruct InfosGlycemie)
        {
            string Retour = "erreur";
            try
            {
                Console.WriteLine("glycemie: " + glycemieMesure);
                int glycemieNumerique = Convert.ToInt32(glycemieMesure);
                if (glycemieNumerique < 70)
                {
                    Retour = InfosGlycemie.GlycemieMoins70Midi;
                }
                else if (glycemieNumerique >= 70 && glycemieNumerique < 101)
                {
                    Retour = InfosGlycemie.Glycemie70A100Midi;
                }
                else if (glycemieNumerique >= 101 && glycemieNumerique < 151)
                {
                    Retour = InfosGlycemie.Glycemie101A150Midi;
                }
                else if (glycemieNumerique >= 151 && glycemieNumerique < 201)
                {
                    Retour = InfosGlycemie.Glycemie151A200Midi;
                }
                else if (glycemieNumerique >= 201 && glycemieNumerique < 251)
                {
                    Retour = InfosGlycemie.Glycemie201A250Midi;
                }
                else if (glycemieNumerique >= 251 && glycemieNumerique < 301)
                {
                    Retour = InfosGlycemie.Glycemie251A300Midi;
                }
                else if (glycemieNumerique > 300)
                {
                    Retour = InfosGlycemie.GlycemiePlus300Midi;
                }
                Console.WriteLine("glycemieNumerique: " + InfosGlycemie.GlycemieMoins70Midi);
            }
            catch (Exception error)
            {
                Retour = "erreur";
                Console.WriteLine("Utlisateur non enregistré. ou erreur:" + error.Message.ToString());
            }

            return Retour;
        }

        private string FindNombreUniteMatin(string glycemieMesure, GlycemieInfosObjectStruct InfosGlycemie)
        {

            string Retour = "erreur";
            try
            {
                Console.WriteLine("glycemie: " + glycemieMesure);
                int glycemieNumerique = Convert.ToInt32(glycemieMesure);
                if (glycemieNumerique < 70)
                {

                    Retour = InfosGlycemie.GlycemieMoins70Matin;
                }
                else if (glycemieNumerique >= 70 && glycemieNumerique < 101)
                {

                    Retour = InfosGlycemie.Glycemie70A100Matin;
                }
                else if (glycemieNumerique >= 101 && glycemieNumerique < 151)
                {

                    Retour = InfosGlycemie.Glycemie101A150Matin;
                }
                else if (glycemieNumerique >= 151 && glycemieNumerique < 201)
                {

                    Retour = InfosGlycemie.Glycemie151A200Matin;
                }
                else if (glycemieNumerique >= 201 && glycemieNumerique < 251)
                {

                    Retour = InfosGlycemie.Glycemie201A250Matin;
                }
                else if (glycemieNumerique >= 251 && glycemieNumerique < 301)
                {

                    Retour = InfosGlycemie.Glycemie251A300Matin;
                }
                else if (glycemieNumerique > 300)
                {

                    Retour = InfosGlycemie.GlycemiePlus300Matin;
                }
                //  Console.WriteLine("glycemieNumerique: " + InfosGlycemie.GlycemieMoins70Matin);
            }
            catch (Exception error)
            {
                Retour = "erreur";

                Console.WriteLine("Utlisateur non enregistré. ou erreur:" + error.Message.ToString());
            }

            return Retour;
        }


        public bool TestIfMomentFilesJsonExist()
        {
            Boolean Retour = false;

            try
            {
                Boolean IsFileConfigMatinExist = DependencyService.Get<IFileReadWrite>().IsFileExiste(PrefsApp.fileMatinGlycemie);

                if (IsFileConfigMatinExist == false || DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMatinGlycemie) == null)
                {
                    List<Matin> NewListMatin = new List<Matin>();
                    string json = JsonConvert.SerializeObject(NewListMatin, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileMatinGlycemie, json);
                }
                Retour = true;
            }
            catch (Exception)
            {
                Retour = false;
            }

            try
            {
                Boolean IsFileConfigMidiExist = DependencyService.Get<IFileReadWrite>().IsFileExiste(PrefsApp.fileMidiGlycemie);
                if (IsFileConfigMidiExist == false)
                {
                    List<Midi> NewListMidi = new List<Midi>();
                    string json = JsonConvert.SerializeObject(NewListMidi, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileMidiGlycemie, json);
                }
                Retour = true;
            }
            catch (Exception)
            {
                Retour = false;
            }


            try
            {
                Boolean IsFileConfigSoirExist = DependencyService.Get<IFileReadWrite>().IsFileExiste(PrefsApp.fileSoirGlycemie);
                if (IsFileConfigSoirExist == false)
                {
                    List<Soir> NewListSoir = new List<Soir>();
                    string json = JsonConvert.SerializeObject(NewListSoir, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileSoirGlycemie, json);
                }
                Retour = true;
            }
            catch (Exception)
            {
                Retour = false;
            }


            return Retour;
        }

    }
}
