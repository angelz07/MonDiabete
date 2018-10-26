using MonDiabete.Fichiers;
using MonDiabete.Class;
using MonDiabete.Class.Dependency_Services_Class;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using MonDiabete.Objets;
using Newtonsoft.Json;
using MonDiabete.Vues;

namespace MonDiabete.Class
{
    public class ToolsCheck
    {
        //VariablesGlobal VariablesGlobal = new VariablesGlobal();
        ConfGlycemie ConfGlycemie = new ConfGlycemie();

        public Boolean TestIfRecorded()
        {
            Boolean retour = false;
            string fileName = PrefsApp.fileUserPref;


            Boolean IsFileConfigExist = DependencyService.Get<IFileReadWrite>().IsFileExiste(fileName);
            if (IsFileConfigExist == true)
            {
                string data = DependencyService.Get<IFileReadWrite>().ReadData(fileName);
                var jsonObj = JsonConvert.DeserializeObject<JObject>(data);

                UserInfosObjectStruct infos = (UserInfosObjectStruct)JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(UserInfosObjectStruct));
                if (infos.Recorded == "true")
                {
                    VariablesGlobal.ApiKey = infos.ApiKey;
                    VariablesGlobal.DateNaissance = infos.DateNaissance;
                    VariablesGlobal.Gsm = infos.Gsm;
                    VariablesGlobal.GsmContact = infos.GsmContact;
                    VariablesGlobal.HeureMatin = infos.HeureMatin;
                    VariablesGlobal.HeureMidi = infos.HeureMidi;
                    VariablesGlobal.HeureSoir = infos.HeureSoir;
                    VariablesGlobal.Id = infos.Id;
                    VariablesGlobal.Mail = infos.Mail;
                    VariablesGlobal.MailContact = infos.MailContact;
                    VariablesGlobal.Nom = infos.Nom;
                    VariablesGlobal.NomContact = infos.NomContact;
                    VariablesGlobal.Prenom = infos.Prenom;
                    VariablesGlobal.PrenomContact = infos.PrenomContact;
                    VariablesGlobal.Recorded = infos.Recorded;

                    Console.WriteLine("VariablesGlobal.Nom dans fonction = " + VariablesGlobal.Nom);
                    retour = true;
                }
            }
            else
            {
                UserInfosObjectStruct UserInfos = new UserInfosObjectStruct
                {
                    Recorded = "",
                    Prenom = "",
                    Nom = "",
                    Mail = "",
                    Gsm = "",
                    DateNaissance = "",
                    NomContact = "",
                    PrenomContact = "",
                    GsmContact = "",
                    MailContact = "",
                    HeureMatin = "",
                    HeureMidi = "",
                    HeureSoir = ""
                };

                string json = JsonConvert.SerializeObject(UserInfos);
                DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);
            }
            return retour;
        }





        public Boolean TestIfGlycemieRecorded()
        {
            Boolean retour = false;
            string fileName = PrefsApp.fileConfigGlycemie;


            Boolean IsFileConfigExist = DependencyService.Get<IFileReadWrite>().IsFileExiste(fileName);
            if (IsFileConfigExist == true)
            {

                string data = DependencyService.Get<IFileReadWrite>().ReadData(fileName);
                var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(data);

                GlycemieInfosObjectStruct infosGlycemie = (GlycemieInfosObjectStruct)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(GlycemieInfosObjectStruct));
                if (infosGlycemie.GlycemieConfigRecorded == "true")
                {
                    ConfGlycemie.UpdateVariablesGlobalGlycemie(infosGlycemie);
                  /*  VariablesGlobal.Glycemie101A150Matin = infosGlycemie.Glycemie101A150Matin;
                    VariablesGlobal.Glycemie101A150Midi = infosGlycemie.Glycemie101A150Midi;
                    VariablesGlobal.Glycemie101A150Soir = infosGlycemie.Glycemie101A150Soir;

                    VariablesGlobal.Glycemie151A200Matin = infosGlycemie.Glycemie151A200Matin;
                    VariablesGlobal.Glycemie151A200Midi = infosGlycemie.Glycemie151A200Midi;
                    VariablesGlobal.Glycemie151A200Soir = infosGlycemie.Glycemie151A200Soir;

                    VariablesGlobal.Glycemie201A250Matin = infosGlycemie.Glycemie201A250Matin;
                    VariablesGlobal.Glycemie201A250Midi = infosGlycemie.Glycemie201A250Midi;
                    VariablesGlobal.Glycemie201A250Soir = infosGlycemie.Glycemie201A250Soir;

                    VariablesGlobal.Glycemie251A300Matin = infosGlycemie.Glycemie251A300Matin;
                    VariablesGlobal.Glycemie251A300Midi = infosGlycemie.Glycemie251A300Midi;
                    VariablesGlobal.Glycemie251A300Soir = infosGlycemie.Glycemie251A300Soir;

                    VariablesGlobal.Glycemie70A100Matin = infosGlycemie.Glycemie70A100Matin;
                    VariablesGlobal.Glycemie70A100Midi = infosGlycemie.Glycemie70A100Midi;
                    VariablesGlobal.Glycemie70A100Soir = infosGlycemie.Glycemie70A100Soir;

                    VariablesGlobal.GlycemieConfigRecorded = infosGlycemie.GlycemieConfigRecorded;

                    VariablesGlobal.GlycemieMoins70Matin = infosGlycemie.GlycemieMoins70Matin;
                    VariablesGlobal.GlycemieMoins70Midi = infosGlycemie.GlycemieMoins70Midi;
                    VariablesGlobal.GlycemieMoins70Soir = infosGlycemie.GlycemieMoins70Soir;

                    VariablesGlobal.GlycemiePlus300Matin = infosGlycemie.GlycemiePlus300Matin;
                    VariablesGlobal.GlycemiePlus300Midi = infosGlycemie.GlycemiePlus300Midi;
                    VariablesGlobal.GlycemiePlus300Soir = infosGlycemie.GlycemiePlus300Soir;
                    */
                    retour = true;
                }
            }
            else
            {
                try
                {
                    GlycemieInfosObjectStruct infosGlycemie = new GlycemieInfosObjectStruct
                    {
                        GlycemieConfigRecorded = "",

                        GlycemieMoins70Matin = "",
                        GlycemieMoins70Midi = "",
                        GlycemieMoins70Soir = "",

                        Glycemie70A100Matin = "",
                        Glycemie70A100Midi = "",
                        Glycemie70A100Soir = "",

                        Glycemie101A150Matin = "",
                        Glycemie101A150Midi = "",
                        Glycemie101A150Soir = "",

                        Glycemie151A200Matin = "",
                        Glycemie151A200Midi = "",
                        Glycemie151A200Soir = "",

                        Glycemie201A250Matin = "",
                        Glycemie201A250Midi = "",
                        Glycemie201A250Soir = "",

                        Glycemie251A300Matin = "",
                        Glycemie251A300Midi = "",
                        Glycemie251A300Soir = "",

                        GlycemiePlus300Matin = "",
                        GlycemiePlus300Midi = "",
                        GlycemiePlus300Soir = "",
                    };
                    ConfGlycemie.UpdateVariablesGlobalGlycemie(infosGlycemie);
                    string json = JsonConvert.SerializeObject(infosGlycemie);
                    DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);
                }
                catch (Exception erreur)
                {
                    Console.WriteLine("Erreur Test si Glycemie est enregistrer Ecriture Fichier : " + erreur.Message.ToString());
                }

            }
            return retour;
        }





    }
}
