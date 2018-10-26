using System;
using System.Collections.Generic;
using System.Text;

namespace MonDiabete.Objets
{
    public class Matin
    {
        public string DatePriseMesure { get; set; }
        public string HeurePriseMesure { get; set; }
        public string Glycemie { get; set; }
        public string Insuline { get; set; }
        public string Quand { get; set; }
        public string Export { get; set; }
        public string IdUser { get; set; }
    }

    public class Soir
    {
        public string DatePriseMesure { get; set; }
        public string HeurePriseMesure { get; set; }
        public string Glycemie { get; set; }
        public string Insuline { get; set; }
        public string Quand { get; set; }
        public string Export { get; set; }
        public string IdUser { get; set; }
    }

    public class Midi
    {
        public string DatePriseMesure { get; set; }
        public string HeurePriseMesure { get; set; }
        public string Glycemie { get; set; }
        public string Insuline { get; set; }
        public string Quand { get; set; }
        public string Export { get; set; }
        public string IdUser { get; set; }
    }

    public class RootObject
    {
        public List<Matin> Matins { get; set; }
        public List<Soir> Soirs { get; set; }
        public List<Midi> Midis { get; set; }
    }
}
