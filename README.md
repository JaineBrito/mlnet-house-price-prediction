# ML.NET House Price Prediction

Idioma: **Portugues** | [English](README.en.md)

Projeto de estudo em C# com ML.NET para prever o preco de casas usando regressao.

O objetivo e praticar o fluxo completo de Machine Learning no .NET:
- carregar dados;
- treinar modelo;
- avaliar metricas;
- executar AutoML;
- salvar/carregar modelo;
- prever novos exemplos.

## Estrutura do projeto

- `mlnet-house-price-prediction`  
  Biblioteca com a logica de ML (treino, avaliacao, persistencia e predicao).
- `mlnet-house-price-prediction-console`  
  Aplicacao de console que executa o fluxo completo.

### Arquivos principais

- `mlnet-house-price-prediction-console/Program.cs`  
  Orquestra todo o processo (treino -> avaliacao -> AutoML -> salvar -> prever).
- `mlnet-house-price-prediction/ML/CasaModelTrainer.cs`  
  Contem os metodos de treino, avaliacao, AutoML e salvamento do modelo.
- `mlnet-house-price-prediction/ML/CasaModelPredictor.cs`  
  Carrega o modelo salvo e faz previsoes.
- `mlnet-house-price-prediction/Models/CasaInputData.cs`  
  Define as colunas de entrada do CSV (`Tamanho`, `Quartos`, `Preco`).
- `mlnet-house-price-prediction/Models/CasaPredictionResult.cs`  
  Define a saida da predicao (`PrecoPrevisto`, mapeado de `Score`).

## Como os dados sao lidos

O dataset CSV e carregado por `LoadFromTextFile<CasaInputData>()` com:
- cabecalho (`hasHeader: true`);
- separador virgula (`separatorChar: ','`).

Mapeamento de colunas no `CasaInputData`:
- coluna `0`: `Tamanho`;
- coluna `1`: `Quartos`;
- coluna `2`: `Preco` (valor real para treino).

## Pipeline de treino

No `CasaModelTrainer`:

1. Concatena `Tamanho` e `Quartos` em `Features`.
2. Treina regressao com `LightGbmRegression`.
3. Gera previsoes sobre os dados.
4. Calcula metricas:
   - `MAE` (erro medio absoluto);
   - `RMSE` (raiz do erro quadratico medio);
   - `R²` (coeficiente de determinacao).

## AutoML

O metodo `AvaliarMelhorAlgoritmo()` roda um experimento de regressao por 60 segundos:
- testa algoritmos automaticamente;
- escolhe o melhor `BestRun`;
- imprime nome do treinador e metricas;
- atualiza `modeloTreinado` para o melhor modelo encontrado.

## Salvar e carregar modelo

- `SalvarModelo(path)` salva o modelo treinado em arquivo `.zip`.
- `CarregarModelo(path)` recupera esse modelo para uso futuro.

Isso permite separar treino e inferencia.

## Predicao

A predicao e feita com:
- `CreatePredictionEngine<CasaInputData, CasaPredictionResult>()`
- `Predict(novaCasa)`

Exemplo do projeto:
- entrada: casa com `Tamanho = 85` e `Quartos = 3`;
- saida: `PrecoPrevisto`.

## Como executar

Na raiz do repositorio:

```bash
dotnet restore "mlnet-house-price-prediction.sln"
dotnet build "mlnet-house-price-prediction.sln" -c Debug
dotnet run --project "mlnet-house-price-prediction-console/mlnet-house-price-prediction-console.csproj"
```

## Dependencias

No projeto de biblioteca:
- `Microsoft.ML`
- `Microsoft.ML.LightGbm`
- `Microsoft.ML.AutoML`

## Observacoes de estudo

- O projeto e focado em aprendizado, nao em producao.
- Para evoluir, voce pode:
  - fazer `TrainTestSplit`;
  - usar validacao cruzada;
  - incluir mais variaveis (ex.: localizacao, idade, vagas, banheiros);
  - comparar modelos manualmente e por AutoML.