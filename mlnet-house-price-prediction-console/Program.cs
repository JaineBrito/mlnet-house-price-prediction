using MachineLearning.ML;
using MachineLearning.Models;

ProjetoRegressao();

void ProjetoRegressao()
{
    var trainer = new CasaModelTrainer();
    
    trainer.CarregarDadosCSV(Path.Combine(AppContext.BaseDirectory, "casas_treinamento_grande.csv" ));
    trainer.TreinarModelo();
    trainer.AvaliarModelo();
    trainer.AvaliarMelhorAlgoritmo();

    var pathModelo = Path.Combine(AppContext.BaseDirectory, "modelo_treinado_regressao.zip");
    trainer.SalvarModelo(pathModelo);

    var predictor = new CasaModelPredictor();
    predictor.CarregarModelo(pathModelo);

    var casaNova = new CasaInputData()
    {
        Tamanho = 85f,
        Quartos = 3,
    };

    var resultado = predictor.Prever(casaNova);

    Console.WriteLine("O valor da casa nova é: " + resultado.PrecoPrevisto);
}

