using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model_test
{
    internal class ModelOutput
    {
        [ColumnName(@"Index")]
        public float Index { get; set; }

        [ColumnName(@"Id_Act")]
        public float Id_Act { get; set; }

        [ColumnName(@"Iq_Act")]
        public float Iq_Act { get; set; }

        [ColumnName(@"PhaseAngle_SP")]
        public float PhaseAngle_SP { get; set; }

        [ColumnName(@"Torque_SP")]
        public float Torque_SP { get; set; }

        [ColumnName(@"Vd_Act")]
        public float Vd_Act { get; set; }

        [ColumnName(@"Vq_Act")]
        public float Vq_Act { get; set; }

        [ColumnName(@"Barcode_Pos")]
        public float Barcode_Pos { get; set; }

        [ColumnName(@"Barcode_Vel")]
        public float Barcode_Vel { get; set; }

        [ColumnName(@"Carriage_Center_Pos")]
        public float Carriage_Center_Pos { get; set; }

        [ColumnName(@"Carriage_Vel")]
        public float Carriage_Vel { get; set; }

        [ColumnName(@"Inductance")]
        public float Inductance { get; set; }

        [ColumnName(@"Resistance")]
        public float Resistance { get; set; }

        [ColumnName(@"Timestamp")]
        public float Timestamp { get; set; }

        [ColumnName(@"Features")]
        public float[] Features { get; set; }

        [ColumnName(@"Score")]
        public float Score { get; set; }
    }
}
