#!/bin/bash

# 新しいコマンドプロジェクトを追加するスクリプト
# 使用方法: ./add-command.sh <コマンド名>

# 引数チェック
if [ $# -eq 0 ]; then
    echo "エラー: コマンド名を指定してください"
    echo "使用方法: $0 <コマンド名>"
    echo "例: $0 Cat"
    exit 1
fi

COMMAND_NAME=$1
COMMAND_NAME_LOWER=$(echo "$COMMAND_NAME" | tr '[:upper:]' '[:lower:]')

echo "=== CommandLineTools.$COMMAND_NAME プロジェクトを作成します ==="

# 現在のディレクトリがソリューションルートかチェック
if [ ! -f "CommandLineTools.sln" ]; then
    echo "エラー: CommandLineTools.sln が見つかりません。"
    echo "ソリューションのルートディレクトリで実行してください。"
    exit 1
fi

# 1. コマンド実装プロジェクトの作成
echo ""
echo "1. コマンド実装プロジェクトを作成しています..."
cd src
dotnet new console -n "CommandLineTools.$COMMAND_NAME" --framework net9.0
cd ..
dotnet sln add "src/CommandLineTools.$COMMAND_NAME/CommandLineTools.$COMMAND_NAME.csproj"

# 2. テストプロジェクトの作成
echo ""
echo "2. テストプロジェクトを作成しています..."
cd tests
dotnet new xunit -n "CommandLineTools.$COMMAND_NAME.Tests" --framework net9.0
cd ..
dotnet sln add "tests/CommandLineTools.$COMMAND_NAME.Tests/CommandLineTools.$COMMAND_NAME.Tests.csproj"

# 3. 参照の追加
echo ""
echo "3. プロジェクト参照を追加しています..."

# コマンドプロジェクトへのParser参照
cd "src/CommandLineTools.$COMMAND_NAME"
dotnet add reference "../CommandLineTools.Parser/CommandLineTools.Parser.csproj"
cd ../..

# テストプロジェクトへのコマンド参照
cd "tests/CommandLineTools.$COMMAND_NAME.Tests"
dotnet add reference "../../src/CommandLineTools.$COMMAND_NAME/CommandLineTools.$COMMAND_NAME.csproj"
cd ../..

# 4. 基本ファイルの作成
echo ""
echo "4. 基本ファイルを作成しています..."

# Optionsクラスの作成
cat > "src/CommandLineTools.$COMMAND_NAME/${COMMAND_NAME}Options.cs" << EOF
using CommandLineTools.Parser.Attributes;

namespace CommandLineTools.$COMMAND_NAME;

public class ${COMMAND_NAME}Options
{
    // TODO: コマンド固有のオプションを定義してください
    // 例:
    // [Flag("verbose", ShortName = "v")]
    // public bool Verbose { get; set; }
    
    // [RemainingArguments(Help = "Input files")]
    // public string[] Files { get; set; } = Array.Empty<string>();
}
EOF

# Programクラスの作成
cat > "src/CommandLineTools.$COMMAND_NAME/Program.cs" << EOF
using CommandLineTools.Parser;

namespace CommandLineTools.$COMMAND_NAME;

public class Program
{
    public static void Main(string[] args)
    {
        var options = CommandLineParser.Parse<${COMMAND_NAME}Options>(args);
        
        // TODO: コマンドのロジックを実装してください
        Console.WriteLine("$COMMAND_NAME command is not implemented yet.");
    }
}
EOF

# テストクラスの作成
cat > "tests/CommandLineTools.$COMMAND_NAME.Tests/${COMMAND_NAME}CommandTests.cs" << EOF
namespace CommandLineTools.$COMMAND_NAME.Tests;

public class ${COMMAND_NAME}CommandTests
{
    [Fact]
    public void Test_BasicFunctionality()
    {
        // TODO: テストを実装してください
        Assert.True(true, "This test needs an implementation");
    }
}
EOF

# Class1.cs と UnitTest1.cs を削除（自動生成されるため）
rm -f "src/CommandLineTools.$COMMAND_NAME/Class1.cs"
rm -f "tests/CommandLineTools.$COMMAND_NAME.Tests/UnitTest1.cs"

# 5. ビルドとテストの確認
echo ""
echo "5. ビルドを確認しています..."
dotnet build

if [ $? -eq 0 ]; then
    echo ""
    echo "✅ プロジェクトの作成が完了しました！"
    echo ""
    echo "作成されたファイル:"
    echo "  - src/CommandLineTools.$COMMAND_NAME/"
    echo "    - ${COMMAND_NAME}Options.cs"
    echo "    - Program.cs"
    echo "  - tests/CommandLineTools.$COMMAND_NAME.Tests/"
    echo "    - ${COMMAND_NAME}CommandTests.cs"
    echo ""
    echo "次のステップ:"
    echo "  1. src/CommandLineTools.$COMMAND_NAME/${COMMAND_NAME}Options.cs にオプションを定義"
    echo "  2. src/CommandLineTools.$COMMAND_NAME/Program.cs にロジックを実装"
    echo "  3. tests/CommandLineTools.$COMMAND_NAME.Tests/${COMMAND_NAME}CommandTests.cs にテストを追加"
else
    echo ""
    echo "❌ ビルドに失敗しました。エラーを確認してください。"
    exit 1
fi