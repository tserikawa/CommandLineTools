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

## 新しいコマンドプロジェクト追加手順書

### 1. プロジェクトの作成

#### 1.1 コマンド実装プロジェクト
```bash
# ソリューションルートから
cd src
dotnet new console -n CommandLineTools.{コマンド名}
cd ..
dotnet sln add src/CommandLineTools.{コマンド名}/CommandLineTools.{コマンド名}.csproj
```

#### 1.2 テストプロジェクト
```bash
cd tests
dotnet new xunit -n CommandLineTools.{コマンド名}.Tests
cd ..
dotnet sln add tests/CommandLineTools.{コマンド名}.Tests/CommandLineTools.{コマンド名}.Tests.csproj
```

### 2. 参照の追加

#### 2.1 コマンドプロジェクトへの参照追加
```bash
cd src/CommandLineTools.{コマンド名}
dotnet add reference ../CommandLineTools.Parser/CommandLineTools.Parser.csproj
```

#### 2.2 テストプロジェクトへの参照追加
```bash
cd tests/CommandLineTools.{コマンド名}.Tests
dotnet add reference ../../src/CommandLineTools.{コマンド名}/CommandLineTools.{コマンド名}.csproj
```

### 3. 基本ファイルの作成

#### 3.1 オプションクラス
`src/CommandLineTools.{コマンド名}/{コマンド名}Options.cs`
```csharp
using CommandLineTools.Parser.Attributes;

namespace CommandLineTools.{コマンド名};

public class {コマンド名}Options
{
    // コマンド固有のオプション定義
}
```

#### 3.2 Programクラス
`src/CommandLineTools.{コマンド名}/Program.cs`
```csharp
using CommandLineTools.Parser;

namespace CommandLineTools.{コマンド名};

public class Program
{
    public static void Main(string[] args)
    {
        var options = CommandLineParser.Parse<{コマンド名}Options>(args);
        
        // コマンドのロジック実装
    }
}
```

#### 3.3 テストクラス
`tests/CommandLineTools.{コマンド名}.Tests/{コマンド名}CommandTests.cs`
```csharp
namespace CommandLineTools.{コマンド名}.Tests;

public class {コマンド名}CommandTests
{
    // テスト実装
}
```

### 4. プロジェクトファイルの確認

自動生成されたcsprojファイルで以下を確認：
- TargetFramework: net9.0
- OutputType: Exe（コマンドプロジェクトのみ）
- ImplicitUsings: enable
- Nullable: enable

### 5. ビルドとテストの確認
```bash
# ソリューションルートから
dotnet build
cd tests/CommandLineTools.{コマンド名}.Tests
dotnet test
```

### 具体例：catコマンドを追加する場合

```bash
# 1. プロジェクト作成
cd src
dotnet new console -n CommandLineTools.Cat
cd ..
dotnet sln add src/CommandLineTools.Cat/CommandLineTools.Cat.csproj

cd tests
dotnet new xunit -n CommandLineTools.Cat.Tests
cd ..
dotnet sln add tests/CommandLineTools.Cat.Tests/CommandLineTools.Cat.Tests.csproj

# 2. 参照追加
cd src/CommandLineTools.Cat
dotnet add reference ../CommandLineTools.Parser/CommandLineTools.Parser.csproj
cd ../..

cd tests/CommandLineTools.Cat.Tests
dotnet add reference ../../src/CommandLineTools.Cat/CommandLineTools.Cat.csproj
cd ../..

# 3. ビルド確認
dotnet build
```

### チェックリスト
- [ ] src配下にコマンドプロジェクト作成
- [ ] tests配下にテストプロジェクト作成
- [ ] ソリューションファイルに両プロジェクトを追加
- [ ] Parser プロジェクトへの参照を追加
- [ ] テストからコマンドプロジェクトへの参照を追加
- [ ] Optionsクラスを作成
- [ ] Programクラスを作成
- [ ] テストクラスを作成
- [ ] ビルドが通ることを確認
- [ ] テストが実行できることを確認