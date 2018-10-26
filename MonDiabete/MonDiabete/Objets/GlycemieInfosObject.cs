using System;
using System.Collections.Generic;
using System.Text;

namespace MonDiabete.Objets
{
    public struct GlycemieInfosObjectStruct
    {
        public string GlycemieConfigRecorded { get; set; }

        public string GlycemieMoins70Matin { get; set; }
        public string GlycemieMoins70Midi { get; set; }
        public string GlycemieMoins70Soir { get; set; }

        public string Glycemie70A100Matin { get; set; }
        public string Glycemie70A100Midi { get; set; }
        public string Glycemie70A100Soir { get; set; }


        public string Glycemie101A150Matin { get; set; }
        public string Glycemie101A150Midi { get; set; }
        public string Glycemie101A150Soir { get; set; }


        public string Glycemie151A200Matin { get; set; }
        public string Glycemie151A200Midi { get; set; }
        public string Glycemie151A200Soir { get; set; }


        public string Glycemie201A250Matin { get; set; }
        public string Glycemie201A250Midi { get; set; }
        public string Glycemie201A250Soir { get; set; }

        public string Glycemie251A300Matin { get; set; }
        public string Glycemie251A300Midi { get; set; }
        public string Glycemie251A300Soir { get; set; }

        public string GlycemiePlus300Matin { get; set; }
        public string GlycemiePlus300Midi { get; set; }
        public string GlycemiePlus300Soir { get; set; }
    }
}
