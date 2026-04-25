using System;
using Microsoft.ML.Data;


namespace MachineLearning.Models;

public class CasaInputData
{
    [LoadColumnAttribute(0)]
    public float Tamanho { get; set; }

    [LoadColumnAttribute(1)]
    public float Quartos { get; set; }

    [LoadColumnAttribute(2)]
    public float Preco { get; set; }
}
