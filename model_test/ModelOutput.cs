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
        [ColumnName(@"AvgAreaIncome")]
        public float AvgAreaIncome { get; set; }

        [ColumnName(@"AvgAreaHouseAge")]
        public float AvgAreaHouseAge { get; set; }

        [ColumnName(@"AvgAreaNumberRooms")]
        public float AvgAreaNumberRooms { get; set; }

        [ColumnName(@"AvgAreaNumberBedrooms")]
        public float AvgAreaNumberBedrooms { get; set; }

        [ColumnName(@"AreaPopulation")]
        public float AreaPopulation { get; set; }

        [ColumnName(@"Price")]
        public float Price { get; set; }

        [ColumnName(@"Features")]
        public float[] Features { get; set; }

        [ColumnName(@"Score")]
        public float Score { get; set; }
    }
}
