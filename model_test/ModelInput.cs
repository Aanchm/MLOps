using Microsoft.ML.Data;

namespace model_test
{
    public class ModelInput
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
    }
}
