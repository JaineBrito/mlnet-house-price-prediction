# ML.NET House Price Prediction

Language: [Portugues](README.md) | **English**

Study project in C# with ML.NET to predict house prices using regression.

The goal is to practice the full Machine Learning workflow in .NET:
- load data;
- train a model;
- evaluate metrics;
- run AutoML;
- save/load model artifacts;
- predict new examples.

## Project structure

- `mlnet-house-price-prediction`  
  Class library with ML logic (training, evaluation, persistence, and prediction).
- `mlnet-house-price-prediction-console`  
  Console app that runs the end-to-end workflow.

### Main files

- `mlnet-house-price-prediction-console/Program.cs`  
  Orchestrates the full process (train -> evaluate -> AutoML -> save -> predict).
- `mlnet-house-price-prediction/ML/CasaModelTrainer.cs`  
  Contains training, evaluation, AutoML, and model-saving methods.
- `mlnet-house-price-prediction/ML/CasaModelPredictor.cs`  
  Loads the saved model and performs predictions.
- `mlnet-house-price-prediction/Models/CasaInputData.cs`  
  Defines CSV input columns (`Tamanho`, `Quartos`, `Preco`).
- `mlnet-house-price-prediction/Models/CasaPredictionResult.cs`  
  Defines prediction output (`PrecoPrevisto`, mapped from `Score`).

## How data is loaded

The CSV dataset is loaded with `LoadFromTextFile<CasaInputData>()` using:
- header row (`hasHeader: true`);
- comma separator (`separatorChar: ','`).

Column mapping in `CasaInputData`:
- column `0`: `Tamanho`;
- column `1`: `Quartos`;
- column `2`: `Preco` (ground-truth label for training).

## Training pipeline

Inside `CasaModelTrainer`:

1. Concatenate `Tamanho` and `Quartos` into `Features`.
2. Train a regression model with `LightGbmRegression`.
3. Generate predictions on the dataset.
4. Compute metrics:
   - `MAE` (Mean Absolute Error);
   - `RMSE` (Root Mean Squared Error);
   - `R²` (coefficient of determination).

## AutoML

`AvaliarMelhorAlgoritmo()` runs a regression AutoML experiment for 60 seconds:
- tests multiple algorithms automatically;
- selects the best run (`BestRun`);
- prints trainer name and metrics;
- updates `modeloTreinado` with the best model found.

## Save and load model

- `SalvarModelo(path)` saves the trained model to a `.zip` file.
- `CarregarModelo(path)` loads that model for future use.

This allows training and inference to be separated.

## Prediction

Prediction is done with:
- `CreatePredictionEngine<CasaInputData, CasaPredictionResult>()`
- `Predict(novaCasa)`

Project example:
- input: a house with `Tamanho = 85` and `Quartos = 3`;
- output: `PrecoPrevisto`.

## How to run

From repository root:

```bash
dotnet restore "mlnet-house-price-prediction.sln"
dotnet build "mlnet-house-price-prediction.sln" -c Debug
dotnet run --project "mlnet-house-price-prediction-console/mlnet-house-price-prediction-console.csproj"
```

## Dependencies

In the class library project:
- `Microsoft.ML`
- `Microsoft.ML.LightGbm`
- `Microsoft.ML.AutoML`

## Study notes

- This project is learning-focused, not production-ready.
- To evolve it further, you can:
  - add `TrainTestSplit`;
  - use cross-validation;
  - add more features (for example: location, house age, parking spots, bathrooms);
  - compare manual model selection with AutoML results.
