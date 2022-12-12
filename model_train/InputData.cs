using Microsoft.ML.Data;


namespace model_train
{
    public class InputData
    {
        [LoadColumn(0)]
        public float AvgAreaIncome { get; set; }

        [LoadColumn(1)]
        public float AvgAreaHouseAge { get; set; }

        [LoadColumn(2)]
        public float AvgAreaNumberRooms { get; set; }

        [LoadColumn(3)]
        public float AvgAreaNumberBedrooms { get; set; }

        [LoadColumn(4)]
        public float AreaPopulation { get; set; }

        [LoadColumn(5)]
        public float Price { get; set; }

    }
}
