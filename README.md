# CommandLineTools
C#でのコマンドラインツール開発の学習

## xUnitの実行方法

```shell
# ソリューションのルートディレクトリから
cd tests/CommandLineTools.Parser.Tests
dotnet test

# より詳細な出力を見たい場合
dotnet test --logger "console;verbosity=detailed"

# 特定のテストだけ実行
dotnet test --filter "FullyQualifiedName~ValidatorTests"
```