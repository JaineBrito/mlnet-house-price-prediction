using System;
using Microsoft.ML.Data;

namespace MachineLearning.Models;

public class CasaPredictionResult
{
    [ColumnName("Score")]
    public float PrecoPrevisto { get; set; }

}
